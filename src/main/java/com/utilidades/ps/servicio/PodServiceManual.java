package com.utilidades.ps.servicio;

import io.fabric8.kubernetes.api.model.apps.Deployment;
import io.fabric8.openshift.client.OpenShiftClient;
import io.fabric8.kubernetes.api.model.Pod;
import io.fabric8.kubernetes.client.*;
import org.springframework.stereotype.Service;
import java.io.IOException;
import java.util.*;
import java.util.stream.Collectors;
import org.slf4j.*;
import org.springframework.web.servlet.mvc.method.annotation.SseEmitter;

@Service
public class PodServiceManual {

    private static final Logger logger = LoggerFactory.getLogger(PodServiceManual.class);
    private final TokenService tokenService;

    public PodServiceManual(TokenService tokenService) {
        this.tokenService = tokenService;
    }

    private String namespace = System.getenv("NAMESPACE_PROJECT");

    public void scaleDownPods(
            String usuario,
            String password,
            String servidor,
            String opcion,
            SseEmitter emitter) throws IOException {
        emitter.send(
                SseEmitter.event().name("message").data("Iniciando proceso de detención de APP por: " + usuario));
        logger.info("Iniciando proceso de detención de APP por: " + usuario);
        List<String> listaDePodsDown = seleccionarListaPods(opcion, emitter);
        try (OpenShiftClient openShiftClient = createOpenShiftClient(usuario, password, servidor, emitter)) {
            escalarPods(openShiftClient, listaDePodsDown, 0, emitter);
            openShiftClient.close();
        } catch (Exception e) {
            emitter.send(SseEmitter.event().name("error")
                    .data("Fallo durante el proceso de detención de APP: " + e.getMessage()));
            logger.error("Fallo durante el proceso de detención de APP: {}", e.getMessage(), e);
        }
        emitter.send(SseEmitter.event().name("message").data("APP detenido con éxito."));
        logger.info("APP detenido con éxito.");
        emitter.send(SseEmitter.event().name("message").data("Proceso completado"));
        logger.info("Proceso completado");
    }

    public void scaleUpPodsInBlocks(
            String usuario,
            String password,
            String servidor,
            String opcion,
            SseEmitter emitter) throws IOException {
        emitter.send(
                SseEmitter.event().name("message").data("Iniciando proceso de arranque de APP por: " + usuario));
        logger.info("Iniciando proceso de arranque de APP por: " + usuario);

        int BLOCK_SIZE = 0;
        if (opcion.equals("1")) {
            BLOCK_SIZE = 4;
            logger.info(": : : : OPCION: " + opcion);
            logger.info(": : : :BLOCK_SIZE: " + BLOCK_SIZE);
        } else if (opcion.equals("2")) {
            BLOCK_SIZE = 1;
            logger.info(": : : : OPCION: " + opcion);
            logger.info(": : : :BLOCK_SIZE: " + BLOCK_SIZE);
        }

        List<String> listaDePodsUp = seleccionarListaPods(opcion, emitter);

        try (OpenShiftClient openShiftClient = createOpenShiftClient(usuario, password, servidor, emitter)) {
            for (int i = 0; i < listaDePodsUp.size(); i += BLOCK_SIZE) {
                List<String> currentBlock = listaDePodsUp.subList(i, Math.min(i + BLOCK_SIZE, listaDePodsUp.size()));
                emitter.send(SseEmitter.event().name("message")
                        .data("Arrancando APP: activando bloque de pods - " + currentBlock));
                logger.info("Arrancando APP: activando bloque de pods - {}", currentBlock);
                escalarPods(openShiftClient, currentBlock, 1, emitter);
                boolean allReady;
                do {
                    allReady = checkPodsReady(openShiftClient, currentBlock, emitter);
                    if (!allReady) {
                        emitter.send(SseEmitter.event().name("message").data(
                                "Asegurando que todos los pods del bloque estén operativos antes de continuar..."));
                        logger.info("Asegurando que todos los pods del bloque estén operativos antes de continuar...");
                        Thread.sleep(10000); // Espera 10 segundos antes de volver a verificar
                    }
                } while (!allReady);
                if (i + BLOCK_SIZE < listaDePodsUp.size()) {
                    if (opcion.equals("1")) {
                        logger.info(": : : :OPCION: " + opcion);
                        emitter.send(SseEmitter.event().name("message").data(
                                "Bloque de pods activo. Pausa de 2 minutos antes de continuar con el siguiente bloque..."));
                        logger.info(
                                "Bloque de pods activo. Pausa de 2 minutos antes de continuar con el siguiente bloque...");
                        Thread.sleep(120000); // 120000 milisegundos = 2 minutos
                    } else if (opcion.equals("2")) {
                        logger.info(": : : :OPCION: " + opcion);
                        emitter.send(SseEmitter.event().name("message").data(
                                "Bloque de pods activo. Pausa de 2 minutos antes de continuar con el siguiente bloque..."));
                        logger.info(
                                "Bloque de pods activo. Pausa de 1 minutos antes de continuar con el siguiente bloque...");
                        Thread.sleep(60000); // 60000 milisegundos = 1 minutos
                    }
                }
            }
            openShiftClient.close();
        } catch (Exception e) {
            emitter.send(
                    SseEmitter.event().name("error").data("Fallo durante el arranque de APP: " + e.getMessage()));
            logger.error("Fallo durante el arranque de APP: {}", e.getMessage(), e);
        }
        emitter.send(SseEmitter.event().name("message").data("Sistema arrancado y operativo."));
        logger.info("Sistema arrancado y operativo.");
        logger.info("Proceso completado");
        emitter.send(SseEmitter.event().name("message").data("Proceso completado"));
    }

    public void deleteCompletedPods(
            String usuario,
            String password,
            String servidor,
            SseEmitter emitter) throws IOException {

        try (OpenShiftClient openShiftClient = createOpenShiftClient(usuario, password, servidor, emitter)) {
            List<Pod> completedPods = openShiftClient.pods().inNamespace(namespace)
                    .withField("status.phase", "Succeeded").list().getItems();

            for (Pod pod : completedPods) {
                openShiftClient.pods().inNamespace(namespace).withName(pod.getMetadata().getName()).delete();
                emitter.send(SseEmitter.event().name("message")
                        .data("Deployment del pod " + pod.getMetadata().getName() + " ha sido eliminado."));
                logger.info("Deployment del pod {} ha sido eliminado.", pod.getMetadata().getName());
            }

            logger.info("Todos los deployments de los pods completados han sido eliminados.", usuario);
            emitter.send(SseEmitter.event().name("message")
                    .data("Todos los deployments de los pods completados han sido eliminados."));
            emitter.send(SseEmitter.event().name("message").data("Proceso completado"));
            logger.info("Proceso completado");
            openShiftClient.close();
        } catch (Exception e) {
            emitter.send(SseEmitter.event().name("error")
                    .data("Error al eliminar deployments de los pods completados: " + e.getMessage()));
            logger.error("Error al eliminar deployments de los pods completados: {}", e.getMessage(), e);
            throw e;
        }
    }

    public OpenShiftClient createOpenShiftClient(
            String usuario,
            String password,
            String servidor,
            SseEmitter emitter) throws IOException {

        try {
            String servidorSeleccionado;
            Map<String, String> servidorClusterMap = Map.of(
                    "dev-server", System.getenv("CLUSTER1"),
                    "sit-server", System.getenv("CLUSTER2"),
                    "uat-server", System.getenv("CLUSTER1"),
                    "prod-server", System.getenv("CLUSTER1"),
                    "cob-server", System.getenv("CLUSTER2")
            );
            if (servidorClusterMap.containsKey(servidor)) {
                servidorSeleccionado = servidorClusterMap.get(servidor);
                String msg = "Seleccionando servidor de " + servidor.toUpperCase();
                emitter.send(SseEmitter.event().name("message").data(msg));
                logger.info(msg);
            } else {
                String errorMsg = "Opción de servidor inválida: " + servidor;
                emitter.send(SseEmitter.event().name("error").data(errorMsg));
                logger.error(errorMsg);
                throw new IllegalArgumentException(errorMsg);
            }

            emitter.send(SseEmitter.event().name("message").data("Obteniendo token, por favor espere..."));
            logger.info("Obteniendo token, por favor espere...");

            String token = tokenService.getToken(servidorSeleccionado, usuario, password);

            if (token != null && !token.isEmpty()) {
                emitter.send(SseEmitter.event().name("message").data("TOKEN OBTENIDO CON EXITO"));
                logger.info("TOKEN OBTENIDO CON EXITO");
            } else {
                emitter.send(SseEmitter.event().name("error").data("ERROR OBTENIENDO EL TOKEN"));
                logger.error("ERROR OBTENIENDO EL TOKEN");
            }

            emitter.send(SseEmitter.event().name("message")
                    .data("Conectando con el servidor " + servidor + " de OpenShift para gestión de APP..."));
            logger.info("Conectando con el servidor " + servidor + " de OpenShift para gestión de APP...");

            Config config = new ConfigBuilder()
                    .withOauthToken(token)
                    .withMasterUrl(servidorSeleccionado)
                    .withTrustCerts(true)
                    .build();
            KubernetesClient kubernetesClient = new KubernetesClientBuilder().withConfig(config).build();

            emitter.send(SseEmitter.event().name("message")
                    .data("Validando conexión con el servidor " + servidor + " de OpenShift..."));
            logger.info("Validando conexión con el servidor " + servidor + " de OpenShift...");

            List<Pod> completedPods = kubernetesClient.pods().inNamespace(namespace)
                    .withField("status.phase", "Running").list().getItems();

            if (completedPods.size() < 0) {
                String errorMsg = "Validación con el servidor " + servidor + " de OpenShift erronea...";
                emitter.send(SseEmitter.event().name("message").data(errorMsg));
                emitter.send(SseEmitter.event().name("error").data(errorMsg));
                logger.error(errorMsg);
                throw new IllegalArgumentException(errorMsg);
            } else {
                emitter.send(SseEmitter.event().name("message")
                        .data("Validación con el servidor " + servidor + " de OpenShift OK ..."));
                logger.info("Validación con el servidor " + servidor + " de OpenShift OK ...");
            }

            emitter.send(SseEmitter.event().name("message")
                    .data("Conexión exitosa con el servidor " + servidor
                            + " de OpenShift. Preparado para gestionar APP."));
            logger.info(
                    "Conexión exitosa con el servidor " + servidor + " de OpenShift. Preparado para gestionar APP.");

            return kubernetesClient.adapt(OpenShiftClient.class);

        } catch (Exception e) {
            emitter.send(SseEmitter.event().name("error")
                    .data("Fallo al conectar con OpenShift para gestión APP: " + e.getMessage()));
            logger.error("Fallo al conectar con OpenShift para gestión APP: {}", e.getMessage(), e);
            throw new RuntimeException("Fallo al conectar con OpenShift: " + e.getMessage(), e);
        }
    }

    private void escalarPods(OpenShiftClient openShiftClient, List<String> podNames, int replicas, SseEmitter emitter)
            throws IOException {
        for (String podName : podNames) {
            try {
                Deployment deployment = openShiftClient.apps().deployments()
                        .inNamespace(namespace)
                        .withName(podName)
                        .get();
                if (deployment != null && deployment.getSpec() != null) {
                    openShiftClient.apps().deployments()
                            .inNamespace(namespace)
                            .withName(podName)
                            .scale(replicas);
                    emitter.send(SseEmitter.event().name("message")
                            .data("Pod " + podName + " ajustado a " + replicas + " instancias."));
                    logger.info("Pod {} ajustado a {} instancias.", podName, replicas);
                }
            } catch (Exception e) {
                emitter.send(SseEmitter.event().name("error")
                        .data("Fallo al ajustar las instancias del pod " + podName + ": " + e.getMessage()));
                logger.error("Fallo al ajustar las instancias del pod {}: {}", podName, e.getMessage(), e);
            }
        }
    }

    private boolean checkPodsReady(OpenShiftClient openShiftClient, List<String> podNames, SseEmitter emitter)
            throws IOException {
        for (String podName : podNames) {
            try {
                Deployment deployment = openShiftClient.apps().deployments()
                        .inNamespace(namespace)
                        .withName(podName)
                        .get();

                if (deployment == null || deployment.getSpec() == null || deployment.getSpec().getSelector() == null) {
                    emitter.send(SseEmitter.event().name("error")
                            .data("No se pudo obtener el Deployment o su configuración de selector para: " + podName));
                    logger.error("No se pudo obtener el Deployment o su configuración de selector para: {}", podName);
                    return false;
                }

                Map<String, String> matchLabels = deployment.getSpec().getSelector().getMatchLabels();
                if (matchLabels == null || matchLabels.isEmpty()) {
                    emitter.send(SseEmitter.event().name("error")
                            .data("El Deployment no tiene matchLabels definidos: " + podName));
                    logger.error("El Deployment no tiene matchLabels definidos: {}", podName);
                    return false;
                }

                List<Pod> pods = openShiftClient.pods()
                        .inNamespace(namespace)
                        .withLabels(matchLabels)
                        .list()
                        .getItems();

                for (Pod pod : pods) {
                    String podNameActual = pod.getMetadata().getName();
                    String podPhase = pod.getStatus().getPhase();
                    logger.info("Verificando pod: {} - Estado: {}", podNameActual, podPhase);
                    if (!"Running".equalsIgnoreCase(podPhase)) {
                        return false;
                    }
                }

            } catch (Exception e) {
                emitter.send(SseEmitter.event().name("error")
                        .data("Fallo al comprobar la operatividad de los pods para " + podName + ": " + e.getMessage()));
                logger.error("Fallo al comprobar la operatividad de los pods para {}: {}", podName, e.getMessage(), e);
                return false;
            }
        }
        return true;
    }


    private List<String> seleccionarListaPods(String opcion, SseEmitter emitter) throws IOException {
        String envVar = (opcion.equals("1")) ? "LISTAA" : "LISTAB";
        emitter.send(SseEmitter.event().name("message")
                .data("Seleccionando configuración de pods para el arranque/detención: Grupo "
                        + (opcion.equals("1") ? "A" : "B")));
        logger.info("Seleccionando configuración de pods para el arranque/detención: Grupo {}",
                (opcion.equals("1") ? "A" : "B"));
        List<String> podsList = loadPodListFromEnv(envVar, emitter);
        String podNamesElegidos = String.join(", ", podsList);
        emitter.send(SseEmitter.event().name("message").data("Pods cargados: " + podNamesElegidos));
        logger.info("Pods cargados para el grupo {}: {}", (opcion.equals("1") ? "A" : "B"), podNamesElegidos);
        return podsList;
    }

    private List<String> loadPodListFromEnv(String envVar, SseEmitter emitter) throws IOException {
        String podsEnv = System.getenv(envVar);
        if (podsEnv != null && !podsEnv.isEmpty()) {
            return Arrays.stream(podsEnv.split(","))
                    .map(String::trim)
                    .collect(Collectors.toList());
        } else {
            String errorMessage = "No se pudo recuperar la lista de pods de la variable de entorno: " + envVar;
            emitter.send(SseEmitter.event().name("error").data(errorMessage));
            logger.error(errorMessage);
            throw new IllegalArgumentException(errorMessage);
        }
    }
}