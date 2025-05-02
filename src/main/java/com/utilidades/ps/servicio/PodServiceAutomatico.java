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

@Service
public class PodServiceAutomatico {

    private static final Logger logger = LoggerFactory.getLogger(PodServiceAutomatico.class);
    private final TokenService tokenService;
    private final CryptoService cryptoService;

    public PodServiceAutomatico(TokenService tokenService, CryptoService cryptoService) {
        this.tokenService = tokenService;
        this.cryptoService = cryptoService;
    }

    private String namespace = System.getenv("NAMESPACE_PROJECT");

    public void scaleDownPods(
            String username,
            String password,
            String servidor,
            String opcion) throws IOException {
        logger.info("Iniciando proceso de detención de APP por: " + username);
        List<String> listaDePodsDown = seleccionarListaPods(opcion);
        try (OpenShiftClient openShiftClient = createOpenShiftClient(username, password, servidor)) {
            escalarPods(openShiftClient, listaDePodsDown, 0);
            openShiftClient.close();
        } catch (Exception e) {
            logger.error("Fallo durante el proceso de detención de APP: {}", e.getMessage(), e);
        }
        logger.info("APP detenido con éxito.");
        logger.info("Proceso completado");
    }

    public void scaleUpPodsInBlocks(
            String username,
            String password,
            String servidor,
            String opcion) throws IOException {

        logger.info("Iniciando proceso de arranque de APP por: " + username);
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

        List<String> listaDePodsUp = seleccionarListaPods(opcion);
        try (OpenShiftClient openShiftClient = createOpenShiftClient(username, password, servidor)) {
            for (int i = 0; i < listaDePodsUp.size(); i += BLOCK_SIZE) {
                List<String> currentBlock = listaDePodsUp.subList(i, Math.min(i + BLOCK_SIZE, listaDePodsUp.size()));
                logger.info("Arrancando APP: activando bloque de pods - {}", currentBlock);
                escalarPods(openShiftClient, currentBlock, 1);
                boolean allReady;
                do {
                    allReady = checkPodsReady(openShiftClient, currentBlock);
                    if (!allReady) {
                        logger.info("Asegurando que todos los pods del bloque estén operativos antes de continuar...");
                        Thread.sleep(10000); // Espera 10 segundos antes de volver a verificar
                    }
                } while (!allReady);
                if (i + BLOCK_SIZE < listaDePodsUp.size()) {
                    if (opcion.equals("1")) {
                        logger.info(": : : :OPCION: " + opcion);
                        logger.info(
                                "Bloque de pods activo. Pausa de 2 minutos antes de continuar con el siguiente bloque...");
                        Thread.sleep(120000); // 120000 milisegundos = 2 minutos
                    } else if (opcion.equals("2")) {
                        logger.info(": : : :OPCION: " + opcion);
                        logger.info(
                                "Bloque de pods activo. Pausa de 1 minutos antes de continuar con el siguiente bloque...");
                        Thread.sleep(60000); // 60000 milisegundos = 1 minutos
                    }
                }
            }
            openShiftClient.close();
        } catch (Exception e) {
            logger.error("Fallo durante el arranque de APP: {}", e.getMessage(), e);
        }
        logger.info("Sistema arrancado y operativo.");
        logger.info("Proceso completado");
    }

    public void deleteCompletedPods(
            String username,
            String password,
            String servidor) throws IOException {

        try (OpenShiftClient openShiftClient = createOpenShiftClient(username, password, servidor)) {
            List<Pod> completedPods = openShiftClient.pods().inNamespace(namespace)
                    .withField("status.phase", "Succeeded").list().getItems();
            for (Pod pod : completedPods) {
                openShiftClient.pods().inNamespace(namespace).withName(pod.getMetadata().getName()).delete();
                logger.info("Deployment del pod {} ha sido eliminado.", pod.getMetadata().getName());
            }
            logger.info("Todos los deployments de los pods completados han sido eliminados.", username);
            logger.info("Proceso completado");
            openShiftClient.close();
        } catch (Exception e) {
            logger.error("Error al eliminar deployments de los pods completados: {}", e.getMessage(), e);
            throw e;
        }
    }

    public OpenShiftClient createOpenShiftClient(
            String username,
            String password,
            String servidor) throws IOException {

        try {
            String servidorSeleccionado = "";
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
                logger.info(msg);
            } else {
                String errorMsg = "Opción de servidor inválida: " + servidor;
                logger.error(errorMsg);
                throw new IllegalArgumentException(errorMsg);
            }

            logger.info("Obteniendo token, por favor espere...");
            String pwdDecrypt = cryptoService.decrypt(password);
            String token = tokenService.getToken(servidorSeleccionado, username, pwdDecrypt);

            if (token != null && !token.isEmpty()) {
                logger.info("TOKEN OBTENIDO CON EXITO");
            } else {
                logger.error("ERROR OBTENIENDO EL TOKEN");
            }

            logger.info("Conectando con el servidor " + servidor + " de OpenShift para gestión de APP...");

            Config config = new ConfigBuilder()
                    .withOauthToken(token)
                    .withMasterUrl(servidorSeleccionado)
                    .withTrustCerts(true)
                    .build();
            KubernetesClient kubernetesClient = new KubernetesClientBuilder().withConfig(config).build();

            logger.info("Validando conexión con el servidor " + servidor + " de OpenShift...");

            List<Pod> completedPods = kubernetesClient.pods().inNamespace(namespace)
                    .withField("status.phase", "Running").list().getItems();

            if (completedPods.size() < 0) {
                String errorMsg = "Validación con el servidor " + servidor + " de OpenShift erronea...";
                logger.error(errorMsg);
                throw new IllegalArgumentException(errorMsg);
            } else {
                logger.info("Validación con el servidor " + servidor + " de OpenShift OK ...");
            }

            logger.info("Conexión exitosa con el servidor " + servidor
                    + " de OpenShift. Preparado para gestionar APP.");
            return kubernetesClient.adapt(OpenShiftClient.class);
        } catch (Exception e) {
            logger.error("Fallo al conectar con OpenShift para gestión APP: {}", e.getMessage(), e);
            throw new RuntimeException("Fallo al conectar con OpenShift: " + e.getMessage(), e);
        }
    }

    private void escalarPods(OpenShiftClient openShiftClient, List<String> podNames, int replicas)
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
                    logger.info("Pod {} ajustado a {} instancias.", podName, replicas);
                }
            } catch (Exception e) {
                logger.error("Fallo al ajustar las instancias del pod {}: {}", podName, e.getMessage(), e);
            }
        }
    }

    private boolean checkPodsReady(OpenShiftClient openShiftClient, List<String> podNames)
            throws IOException {
        for (String podName : podNames) {
            try {
                Deployment deployment = openShiftClient.apps().deployments()
                        .inNamespace(namespace)
                        .withName(podName)
                        .get();

                if (deployment == null || deployment.getSpec() == null || deployment.getSpec().getSelector() == null) {
                    logger.error("No se pudo obtener el Deployment o su configuración de selector para: {}", podName);
                    return false;
                }

                Map<String, String> matchLabels = deployment.getSpec().getSelector().getMatchLabels();
                if (matchLabels == null || matchLabels.isEmpty()) {
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
                logger.error("Fallo al comprobar la operatividad de los pods para {}: {}", podName, e.getMessage(), e);
                return false;
            }
        }
        return true;
    }


    private List<String> seleccionarListaPods(String opcion) throws IOException {
        String envVar = (opcion.equals("1")) ? "LISTAA" : "LISTAB";
        logger.info("Seleccionando configuración de pods para el arranque/detención: Grupo {}",
                (opcion.equals("1") ? "A" : "B"));
        List<String> podsList = loadPodListFromEnv(envVar);
        String podNamesElegidos = String.join(", ", podsList);
        logger.info("Pods cargados para el grupo {}: {}", (opcion.equals("1") ? "A" : "B"), podNamesElegidos);
        return podsList;
    }

    private List<String> loadPodListFromEnv(String envVar) throws IOException {
        String podsEnv = System.getenv(envVar);
        if (podsEnv != null && !podsEnv.isEmpty()) {
            return Arrays.stream(podsEnv.split(","))
                    .map(String::trim)
                    .collect(Collectors.toList());
        } else {
            String errorMessage = "No se pudo recuperar la lista de pods de la variable de entorno: " + envVar;
            logger.error(errorMessage);
            throw new IllegalArgumentException(errorMessage);
        }
    }
}