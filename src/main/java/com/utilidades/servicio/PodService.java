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
@Service
public class PodService {

    private static final int BLOCK_SIZE = 4;
    private List<String> listaDePodsA = Arrays.asList("podName1", "podName2", "podName3", "podName16");
    private List<String> listaDePodsB = Arrays.asList("podName5", "podName6", "podName7", "podName8");
    private static final String NAMESPACE = "tu-namespace";

    private OpenShiftClient createOpenShiftClient(String token, String servidor, SseEmitter emitter) throws IOException {
        try {
            emitter.send(SseEmitter.event().name("message").data("Intentando crear cliente de OpenShift..."));
            Config config = new ConfigBuilder()
                    .withOauthToken(token)
                    .withMasterUrl(servidor)
                    .withTrustCerts(true)
                    .build();
            KubernetesClient kubernetesClient = new KubernetesClientBuilder().withConfig(config).build();
            kubernetesClient.namespaces().list();
            emitter.send(SseEmitter.event().name("message").data("Cliente de OpenShift creado y validado exitosamente."));
            return kubernetesClient.adapt(OpenShiftClient.class);
        } catch (Exception e) {
            emitter.send(SseEmitter.event().name("error").data("Error al crear el cliente de OpenShift: " + e.getMessage()));
            throw new RuntimeException("Error al establecer conexión con OpenShift: " + e.getMessage(), e);
        }
    }

    public void scaleDownPods(String token, String servidor, String opcion, SseEmitter emitter) throws IOException {
        emitter.send(SseEmitter.event().name("message").data("Iniciando el escalado hacia abajo de los pods..."));
        List<String> listaDePodsDown = seleccionarListaPods(opcion, emitter);
        try (OpenShiftClient openShiftClient = createOpenShiftClient(token, servidor, emitter)) {
            escalarPods(openShiftClient, listaDePodsDown, 0, emitter);
        } catch (Exception e) {
            emitter.send(SseEmitter.event().name("error").data("Error al intentar escalar los pods hacia abajo: " + e.getMessage()));
        }
        emitter.send(SseEmitter.event().name("message").data("Escalado hacia abajo de pods completado."));
    }

    public void scaleUpPodsInBlocks(String token, String servidor, String opcion, SseEmitter emitter) throws IOException {
        emitter.send(SseEmitter.event().name("message").data("Iniciando el escalado hacia arriba de los pods en bloques..."));
        List<String> listaDePodsUp = seleccionarListaPods(opcion, emitter);
        try (OpenShiftClient openShiftClient = createOpenShiftClient(token, servidor, emitter)) {
            for (int i = 0; i < listaDePodsUp.size(); i += BLOCK_SIZE) {
                List<String> currentBlock = listaDePodsUp.subList(i, Math.min(i + BLOCK_SIZE, listaDePodsUp.size()));
                emitter.send(SseEmitter.event().name("message").data("Escalando el siguiente bloque de pods: " + currentBlock));
                escalarPods(openShiftClient, currentBlock, 1, emitter);
                boolean allReady;
                do {
                    allReady = checkPodsReady(openShiftClient, currentBlock, emitter);
                    if (!allReady) {
                        emitter.send(SseEmitter.event().name("message").data("Esperando a que todos los pods del bloque actual estén listos..."));
                        Thread.sleep(10000); // Espera 10 segundos antes de volver a verificar
                    }
                } while (!allReady);
                emitter.send(SseEmitter.event().name("message").data("Todos los pods del bloque actual están listos. Esperando 2 minutos antes de continuar con el siguiente bloque."));
                Thread.sleep(120000); // 120000 milisegundos = 2 minutos
            }
        } catch (Exception e) {
            emitter.send(SseEmitter.event().name("error").data("Error durante el escalado hacia arriba de los pods: " + e.getMessage()));
        }
        emitter.send(SseEmitter.event().name("message").data("Proceso completado"));
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
                    emitter.send(SseEmitter.event().name("message").data("Pod " + podName + " escalado a " + replicas + " exitosamente."));
                }
            } catch (Exception e) {
                emitter.send(SseEmitter.event().name("error").data("Error al escalar el pod " + podName + ": " + e.getMessage()));
            }
        }
        emitter.send(SseEmitter.event().name("message").data("Proceso completado"));
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
                    emitter.send(SseEmitter.event().name("error").data("Error al verificar si los pods están listos: " + e.getMessage()));
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
                emitter.send(SseEmitter.event().name("message").data("Usando lista de Pods opción A."));
                return listaDePodsA;
            case "2":
                emitter.send(SseEmitter.event().name("message").data("Usando lista de Pods opción B."));
                return listaDePodsB;
            default:
                emitter.send(SseEmitter.event().name("error").data("Opción no válida proporcionada: " + opcion));
                throw new IllegalArgumentException("Opción no válida proporcionada: " + opcion);
        }
    }
}
