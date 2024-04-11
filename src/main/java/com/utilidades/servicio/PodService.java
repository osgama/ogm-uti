package com.utilidades.servicio;

import io.fabric8.openshift.client.OpenShiftClient;
import io.fabric8.kubernetes.client.KubernetesClient;
import io.fabric8.kubernetes.client.KubernetesClientBuilder;
import io.fabric8.openshift.api.model.DeploymentConfig;
import org.springframework.stereotype.Service;
import org.springframework.web.servlet.mvc.method.annotation.SseEmitter;
import java.io.IOException;
import java.util.Arrays;
import java.util.List;
import io.fabric8.kubernetes.client.Config;
import io.fabric8.kubernetes.client.ConfigBuilder;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

@Service
public class PodService {

    private static final Logger logger = LoggerFactory.getLogger(PodService.class);
    private static final int BLOCK_SIZE = 4;
    private List<String> listaDePodsA = Arrays.asList("podName1", "podName2", "podName3", "podName16");
    private List<String> listaDePodsB = Arrays.asList("podName5", "podName6", "podName7", "podName8");
    private static final String NAMESPACE = "tu-namespace";

    private OpenShiftClient createOpenShiftClient(String token, String servidor, SseEmitter emitter) throws IOException {
        try {
            emitter.send(SseEmitter.event().name("message").data("Conectando con el servicio de OpenShift para gestión del sistema..."));
            logger.info("Conectando con el servicio de OpenShift para gestión del sistema...");
            Config config = new ConfigBuilder()
                    .withOauthToken(token)
                    .withMasterUrl(servidor)
                    .withTrustCerts(true)
                    .build();
            KubernetesClient kubernetesClient = new KubernetesClientBuilder().withConfig(config).build();
            kubernetesClient.namespaces().list();
            emitter.send(SseEmitter.event().name("message").data("Conexión exitosa con OpenShift. Preparado para gestionar el sistema."));
            logger.info("Conexión exitosa con OpenShift. Preparado para gestionar el sistema.");
            return kubernetesClient.adapt(OpenShiftClient.class);
        } catch (Exception e) {
            emitter.send(SseEmitter.event().name("error").data("Fallo al conectar con OpenShift para gestión del sistema: " + e.getMessage()));
            logger.error("Fallo al conectar con OpenShift para gestión del sistema: {}", e.getMessage(), e);
            throw new RuntimeException("Fallo al conectar con OpenShift: " + e.getMessage(), e);
        }
    }

    public void scaleDownPods(String token, String servidor, String opcion, SseEmitter emitter) throws IOException {
        emitter.send(SseEmitter.event().name("message").data("Iniciando proceso de detención del sistema..."));
        logger.info("Iniciando proceso de detención del sistema...");
        List<String> listaDePodsDown = seleccionarListaPods(opcion, emitter);
        try (OpenShiftClient openShiftClient = createOpenShiftClient(token, servidor, emitter)) {
            escalarPods(openShiftClient, listaDePodsDown, 0, emitter);
        } catch (Exception e) {
            emitter.send(SseEmitter.event().name("error").data("Fallo durante el proceso de detención del sistema: " + e.getMessage()));
            logger.error("Fallo durante el proceso de detención del sistema: {}", e.getMessage(), e);
        }
        emitter.send(SseEmitter.event().name("message").data("Sistema detenido con éxito."));
        logger.info("Sistema detenido con éxito.");
    }

    public void scaleUpPodsInBlocks(String token, String servidor, String opcion, SseEmitter emitter) throws IOException {
        emitter.send(SseEmitter.event().name("message").data("Iniciando proceso de arranque del sistema..."));
        logger.info("Iniciando proceso de arranque del sistema...");
        List<String> listaDePodsUp = seleccionarListaPods(opcion, emitter);
        try (OpenShiftClient openShiftClient = createOpenShiftClient(token, servidor, emitter)) {
            for (int i = 0; i < listaDePodsUp.size(); i += BLOCK_SIZE) {
                List<String> currentBlock = listaDePodsUp.subList(i, Math.min(i + BLOCK_SIZE, listaDePodsUp.size()));
                emitter.send(SseEmitter.event().name("message").data("Arrancando sistema: activando bloque de pods - " + currentBlock));
                logger.info("Arrancando sistema: activando bloque de pods - {}", currentBlock);
                escalarPods(openShiftClient, currentBlock, 1, emitter);
                boolean allReady;
                do {
                    allReady = checkPodsReady(openShiftClient, currentBlock, emitter);
                    if (!allReady) {
                        emitter.send(SseEmitter.event().name("message").data("Asegurando que todos los pods del bloque estén operativos antes de continuar..."));
                        logger.info("Asegurando que todos los pods del bloque estén operativos antes de continuar...");
                        Thread.sleep(10000); // Espera 10 segundos antes de volver a verificar
                    }
                } while (!allReady);
                emitter.send(SseEmitter.event().name("message").data("Bloque de pods activo. Pausa de 2 minutos antes de continuar con el siguiente bloque..."));
                logger.info("Bloque de pods activo. Pausa de 2 minutos antes de continuar con el siguiente bloque...");
                Thread.sleep(120000); // 120000 milisegundos = 2 minutos
            }
        } catch (Exception e) {
            emitter.send(SseEmitter.event().name("error").data("Fallo durante el arranque del sistema: " + e.getMessage()));
            logger.error("Fallo durante el arranque del sistema: {}", e.getMessage(), e);
        }
        emitter.send(SseEmitter.event().name("message").data("Sistema arrancado y operativo."));
        logger.info("Sistema arrancado y operativo.");
    }

    private void escalarPods(OpenShiftClient openShiftClient, List<String> podNames, int replicas, SseEmitter emitter) throws IOException {
        for (String podName : podNames) {
            try {
                DeploymentConfig dc = openShiftClient.deploymentConfigs()
                        .inNamespace(NAMESPACE)
                        .withName(podName)
                        .get();
                if (dc != null && dc.getSpec() != null) {
                    openShiftClient.deploymentConfigs()
                            .inNamespace(NAMESPACE)
                            .withName(podName)
                            .scale(replicas);
                    emitter.send(SseEmitter.event().name("message").data("Pod " + podName + " ajustado a " + replicas + " instancias."));
                    logger.info("Pod {} ajustado a {} instancias.", podName, replicas);
                }
            } catch (Exception e) {
                emitter.send(SseEmitter.event().name("error").data("Fallo al ajustar las instancias del pod " + podName + ": " + e.getMessage()));
                logger.error("Fallo al ajustar las instancias del pod {}: {}", podName, e.getMessage(), e);
            }
        }
    }

    private boolean checkPodsReady(OpenShiftClient openShiftClient, List<String> podNames, SseEmitter emitter) throws IOException {
        return podNames.stream().allMatch(podName -> {
            try {
                return openShiftClient.pods()
                        .inNamespace(NAMESPACE)
                        .withLabel("name", podName)
                        .list()
                        .getItems()
                        .stream()
                        .allMatch(pod -> "Running".equals(pod.getStatus().getPhase()));
            } catch (Exception e) {
                try {
                    emitter.send(SseEmitter.event().name("error").data("Fallo al comprobar la operatividad de los pods: " + e.getMessage()));
                    logger.error("Fallo al comprobar la operatividad de los pods: {}", e.getMessage(), e);
                } catch (IOException ioException) {
                    ioException.printStackTrace();
                }
                return false;
            }
        });
    }

    private List<String> seleccionarListaPods(String opcion, SseEmitter emitter) throws IOException {
        switch (opcion) {
            case "1":
                emitter.send(SseEmitter.event().name("message").data("Seleccionando configuración de pods para el arranque/detención: Grupo A."));
                logger.info("Seleccionando configuración de pods para el arranque/detención: Grupo A.");
                return listaDePodsA;
            case "2":
                emitter.send(SseEmitter.event().name("message").data("Seleccionando configuración de pods para el arranque/detención: Grupo B."));
                logger.info("Seleccionando configuración de pods para el arranque/detención: Grupo B.");
                return listaDePodsB;
            default:
                emitter.send(SseEmitter.event().name("error").data("Opción de configuración inválida: " + opcion));
                logger.error("Opción de configuración inválida: {}", opcion);
                throw new IllegalArgumentException("Opción de configuración inválida: " + opcion);
        }
    }
}
