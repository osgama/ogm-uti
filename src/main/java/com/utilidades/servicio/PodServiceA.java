package com.utilidades.servicio;

import io.fabric8.openshift.client.OpenShiftClient;
import io.fabric8.kubernetes.client.KubernetesClient;
import io.fabric8.kubernetes.client.KubernetesClientBuilder;
import io.fabric8.openshift.api.model.DeploymentConfig;
import org.springframework.stereotype.Service;
import java.util.Arrays;
import java.util.List;
import java.util.stream.Collectors;

import io.fabric8.kubernetes.api.model.Pod;
import io.fabric8.kubernetes.client.Config;
import io.fabric8.kubernetes.client.ConfigBuilder;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

@Service
public class PodServiceA {

    private static final Logger logger = LoggerFactory.getLogger(PodServiceA.class);
    private static final int BLOCK_SIZE = 4;
    private static final String NAMESPACE = "tu-namespace";

    private OpenShiftClient createOpenShiftClient(String servidor, String usuario, String pwd) {
        logger.info("Conectando con el servicio de OpenShift para gestión del sistema...");
        Config config = new ConfigBuilder()
                .withMasterUrl(servidor)
                .withProxyUsername(usuario)
                .withProxyPassword(pwd)
                .withTrustCerts(true)
                .build();
        KubernetesClient kubernetesClient = new KubernetesClientBuilder().withConfig(config).build();
        logger.info("Conexión exitosa con OpenShift. Preparado para gestionar el sistema.");
        return kubernetesClient.adapt(OpenShiftClient.class);
    }

    public void scaleDownPods(String servidor, String usuario, String pwd, String opcion) {
        logger.info("Iniciando proceso de detención del sistema...");
        List<String> listaDePodsDown = seleccionarListaPods(opcion);
        try (OpenShiftClient openShiftClient = createOpenShiftClient(servidor, usuario, pwd)) {
            escalarPods(openShiftClient, listaDePodsDown, 0);
            waitForPodsToTerminate(openShiftClient, listaDePodsDown);
        }
        logger.info("Todos los pods han sido detenidos correctamente.");
    }

    public void scaleUpPodsInBlocks(String servidor, String usuario, String pwd, String opcion) {
        logger.info("Iniciando proceso de arranque del sistema...");
        List<String> listaDePodsUp = seleccionarListaPods(opcion);
        try (OpenShiftClient openShiftClient = createOpenShiftClient(servidor, usuario, pwd)) {
            for (int i = 0; i < listaDePodsUp.size(); i += BLOCK_SIZE) {
                List<String> currentBlock = listaDePodsUp.subList(i, Math.min(i + BLOCK_SIZE, listaDePodsUp.size()));
                logger.info("Arrancando sistema: activando bloque de pods - {}", currentBlock);
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
                    logger.info("Bloque de pods activo. Pausa de 2 minutos antes de continuar con el siguiente bloque...");
                    Thread.sleep(120000); // 120000 milisegundos = 2 minutos
                }
            }
        } catch (InterruptedException e) {
            Thread.currentThread().interrupt();
            logger.error("Interrupción durante el arranque del sistema.", e);
        }
        logger.info("Sistema arrancado y operativo.");
    }

    public void deleteCompletedPods(String servidor, String usuario, String pwd) {
        try (OpenShiftClient openShiftClient = createOpenShiftClient(servidor, usuario, pwd)) {
            List<Pod> completedPods = openShiftClient.pods().inNamespace(NAMESPACE)
                .withField("status.phase", "Completed").list().getItems();

            for (Pod pod : completedPods) {
                openShiftClient.pods().inNamespace(NAMESPACE).withName(pod.getMetadata().getName()).delete();
                logger.info("Pod {} eliminado.", pod.getMetadata().getName());
            }
            logger.info("Todos los pods completados han sido eliminados.");
        }
    }

    private void escalarPods(OpenShiftClient openShiftClient, List<String> podNames, int replicas) {
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
                    logger.info("Pod {} ajustado a {} instancias.", podName, replicas);
                }
            } catch (Exception e) {
                logger.error("Fallo al ajustar las instancias del pod {}: {}", podName, e.getMessage(), e);
            }
        }
    }

    private boolean checkPodsReady(OpenShiftClient openShiftClient, List<String> podNames) {
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
                logger.error("Fallo al comprobar la operatividad de los pods: {}", e.getMessage(), e);
                return false;
            }
        });
    }

    private List<String> seleccionarListaPods(String opcion) {
        String envVar = (opcion.equals("1")) ? "VALORESLISTAA" : "VALORESLISTAB";
        logger.info("Seleccionando configuración de pods para el arranque/detención: Grupo {}",
                (opcion.equals("1") ? "A" : "B"));
        return loadPodListFromEnv(envVar);
    }

    private List<String> loadPodListFromEnv(String envVar) {
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

    private void waitForPodsToTerminate(OpenShiftClient openShiftClient, List<String> podNames) {
        boolean allTerminated = false;
        int retryCount = 0;
        while (!allTerminated && retryCount < 10) {  // Puedes ajustar el número máximo de reintentos
            try {
                Thread.sleep(20000);  // Espera 10 segundos antes de volver a verificar
                allTerminated = podNames.stream().allMatch(podName -> {
                    List<Pod> pods = openShiftClient.pods()
                            .inNamespace(NAMESPACE)
                            .withLabel("name", podName)
                            .list()
                            .getItems();
                    return pods.isEmpty() || pods.stream().allMatch(pod -> "Terminated".equals(pod.getStatus().getPhase()));
                });
                logger.info("Revisión {} de terminación de pods.", ++retryCount);
            } catch (InterruptedException e) {
                Thread.currentThread().interrupt();
                logger.error("Interrupción durante la espera de la terminación de los pods.", e);
                return;
            } catch (Exception e) {
                logger.error("Fallo al comprobar el estado de terminación de los pods: {}", e.getMessage(), e);
                return;
            }
        }
        if (!allTerminated) {
            logger.warn("Algunos pods no se han detenido correctamente después de los intentos máximos.");
        }
    }

    public boolean login(String servidor, String usuario, String pwd) {
        // TODO Auto-generated method stub
        throw new UnsupportedOperationException("Unimplemented method 'login'");
    }
}
