package com.utilidades.servicio;

import io.fabric8.openshift.api.model.DeploymentConfig;
import io.fabric8.openshift.client.OpenShiftClient;
import io.fabric8.kubernetes.api.model.Pod;
import io.fabric8.kubernetes.client.*;
import org.springframework.stereotype.Service;
import java.io.IOException;
import java.util.*;
import java.util.stream.Collectors;
import org.slf4j.*;

@Service
public class PodServiceA {

    private static final Logger logger = LoggerFactory.getLogger(PodServiceA.class);
    private final TokenService tokenService;
    private final CryptoService cryptoService;

    public PodServiceA(TokenService tokenService, CryptoService cryptoService) {
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
            waitForPodsToTerminate(openShiftClient, listaDePodsDown);
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
                                "Bloque de pods activo. Pausa de 2 minutos antes de continuar con el siguiente bloque...");
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
            if (servidor.equals("dev-server")) {
                logger.info("Seleccionando servidor de DEV");
                servidorSeleccionado = System.getProperty("CLUSTER1");

            } else if (servidor.equals("dev-server")) {
                logger.info("Seleccionando servidor de DEV");
                servidorSeleccionado = System.getProperty("CLUSTER1");

            } else if (servidor.equals("dev-server")) {
                logger.info("Seleccionando servidor de DEV");
                servidorSeleccionado = System.getProperty("CLUSTER1");

            } else if (servidor.equals("dev-server")) {
                logger.info("Seleccionando servidor de DEV");
                servidorSeleccionado = System.getProperty("CLUSTER2");

            } else {
                logger.error("Opción de servidor inválida: {}", servidor);
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

            List<Pod> completePods = kubernetesClient.pods().inNamespace(namespace)
                    .withField("status.phase", "Running").list().getItems();

            if (completePods.size() < 0) {
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
                DeploymentConfig dc = openShiftClient.deploymentConfigs()
                        .inNamespace(namespace)
                        .withName(podName)
                        .get();
                if (dc != null && dc.getSpec() != null) {
                    openShiftClient.deploymentConfigs()
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
        return podNames.stream().allMatch(podName -> {
            try {
                return openShiftClient.pods()
                        .inNamespace(namespace)
                        .withLabel("name", podName)
                        .list()
                        .getItems()
                        .stream()
                        .allMatch(pod -> "Running".equals(pod.getStatus().getPhase()));
            } catch (Exception e) {
                logger.error("Fallo al comprobar la operatividad de los pods: {}", e.getMessage(), e);
                return false;
            }
        });
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

    private void waitForPodsToTerminate(OpenShiftClient openShiftClient, List<String> podNames)
            throws IOException {
        boolean allTerminated = false;
        int retryCount = 0;
        while (!allTerminated && retryCount < 20) {
            try {
                Thread.sleep(20000);
                allTerminated = podNames.stream().allMatch(podName -> {
                    List<Pod> pods = openShiftClient.pods()
                            .inNamespace(namespace)
                            .withLabel("name", podName)
                            .list()
                            .getItems();
                    return pods.isEmpty()
                            || pods.stream().allMatch(pod -> "Terminating".equals(pod.getStatus().getPhase()));
                });
                logger.info("Revisión {} de terminación de pods." + retryCount);
            } catch (InterruptedException e) {
                logger.error("Interrupción durante la espera de la terminación de los pods.");
                Thread.currentThread().interrupt();
                return;
            } catch (Exception e) {
                logger.error("Fallor al comprobar el estado de terminación de los pods: {}", e.getMessage(), e);
                return;
            }
        }
        if (!allTerminated) {                
            logger.error("Algunos pods no se han detenido correctamente después de los intentos máximos");
        }
    }
}