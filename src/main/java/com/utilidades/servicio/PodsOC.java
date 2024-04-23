package com.utilidades.servicio;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.utilidades.configuracion.OpenShiftUtils;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.stream.Collectors;

public class PodsOC {

    private static final Logger logger = LoggerFactory.getLogger(PodsOC.class);
    private static final int BLOCK_SIZE = 4;
    private static final String NAMESPACE = "tu-namespace";

    public void scaleDownPods(String servidor, String usuario, String pwd, String opcion) {
        List<String> baseCommand = OpenShiftUtils.buildBaseCommand(servidor, usuario, pwd);
        List<String> podsToScaleDown = seleccionarListaPods(opcion);
        podsToScaleDown.forEach(pod -> {
            List<String> command = new ArrayList<>(baseCommand);
            command.addAll(Arrays.asList("scale", "--replicas=0", "dc/" + pod));
            try {
                String output = OpenShiftUtils.executeOCCommand(command);
                logger.info("Pod scaled down: {}", output);
            } catch (Exception e) {
                logger.error("Error scaling down pod: {}", pod, e);
            }
        });
    }

    public void scaleUpPodsInBlocks(String servidor, String usuario, String pwd, String opcion) {
        List<String> baseCommand = OpenShiftUtils.buildBaseCommand(servidor, usuario, pwd);
        List<String> podsToScaleUp = seleccionarListaPods(opcion);
        for (int i = 0; i < podsToScaleUp.size(); i += BLOCK_SIZE) {
            List<String> currentBlock = podsToScaleUp.subList(i, Math.min(i + BLOCK_SIZE, podsToScaleUp.size()));
            currentBlock.forEach(pod -> {
                List<String> command = new ArrayList<>(baseCommand);
                command.addAll(Arrays.asList("scale", "--replicas=1", "dc/" + pod));
                try {
                    String output = OpenShiftUtils.executeOCCommand(command);
                    logger.info("Pod scaled up: {}", output);
                } catch (Exception e) {
                    logger.error("Error scaling up pod: {}", pod, e);
                }
            });

            boolean allReady;
            do {
                allReady = checkPodsReady(servidor, usuario, pwd, currentBlock);
                if (!allReady) {
                    logger.info("Not all pods are ready. Waiting for 10 seconds before retrying...");
                    try {
                        Thread.sleep(10000); // Espera 10 segundos antes de volver a verificar
                    } catch (InterruptedException e) {
                        Thread.currentThread().interrupt(); // restore interrupted status
                        logger.error("Thread interrupted while waiting.", e);
                        return; // optional: you may choose to stop the method if interrupted
                    }
                }
            } while (!allReady);

            if (i + BLOCK_SIZE < podsToScaleUp.size()) {
                logger.info("Block of pods is active. Waiting 2 minutes before continuing to the next block...");
                try {
                    Thread.sleep(120000); // Espera 2 minutos antes de continuar
                } catch (InterruptedException e) {
                    Thread.currentThread().interrupt(); // restore interrupted status
                    logger.error("Thread interrupted while sleeping between blocks.", e);
                    return; // optional: you may choose to stop the method if interrupted
                }
            }
        }
    }

    public void deleteCompletedPods(String servidor, String usuario, String pwd) {
        List<String> baseCommand = OpenShiftUtils.buildBaseCommand(servidor, usuario, pwd);
        List<String> command = new ArrayList<>(baseCommand);
        command.addAll(Arrays.asList("delete", "pod", "-l status.phase=Completed"));
        try {
            String output = OpenShiftUtils.executeOCCommand(command);
            logger.info("Completed pods deleted: {}", output);
        } catch (Exception e) {
            logger.error("Error deleting completed pods", e);
        }
    }

    private boolean checkPodsReady(String servidor, String usuario, String pwd, List<String> podNames) {
        List<String> baseCommand = OpenShiftUtils.buildBaseCommand(servidor, usuario, pwd);
        for (String podName : podNames) {
            List<String> command = new ArrayList<>(baseCommand);
            command.addAll(Arrays.asList("get", "pod", podName, "-o=jsonpath={.status.phase}"));
            try {
                String output = OpenShiftUtils.executeOCCommand(command);
                if (!output.trim().equals("Running")) {
                    logger.info("Pod {} is not ready, current status: {}", podName, output);
                    return false;
                }
            } catch (Exception e) {
                logger.error("Error checking status of pod {}: {}", podName, e.getMessage(), e);
                return false;
            }
        }
        return true;
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
}
