package com.utilidades.servicio;

import org.springframework.stereotype.Service;
import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.util.Arrays;
import java.util.List;
import java.util.stream.Collectors;

@Service
public class PodServiceOC {

    private static final int BLOCK_SIZE = 4;
    private List<String> listaDePods = Arrays.asList("podName1", "podName2", "podName3", "podName16");
    private static final String NAMESPACE = "tu-namespace";

    public void scaleDownPods(String servidor, String usuario, String token) throws Exception {
        executeOcLogin(servidor, usuario, token);
        listaDePods.forEach(podName -> {
            try {
                executeOcCommand(Arrays.asList("oc", "scale", "dc/" + podName, "--replicas=0", "-n", NAMESPACE));
            } catch (Exception e) {
                e.printStackTrace();
            }
        });
    }

    public void scaleUpPodsInBlocks(String servidor, String usuario, String token) throws Exception {
        executeOcLogin(servidor, usuario, token);
        for (int i = 0; i < listaDePods.size(); i += BLOCK_SIZE) {
            List<String> currentBlock = listaDePods.subList(i, Math.min(i + BLOCK_SIZE, listaDePods.size()));
            currentBlock.forEach(podName -> {
                try {
                    executeOcCommand(Arrays.asList("oc", "scale", "dc/" + podName, "--replicas=1", "-n", NAMESPACE));
                } catch (Exception e) {
                    e.printStackTrace();
                }
            });
            boolean allReady;
            do {
                allReady = checkPodsReady(currentBlock, servidor, usuario, token);
                if (!allReady) {
                    System.out.println("Esperando a que los pods estén listos...");
                    Thread.sleep(10000); // Espera 10 segundos antes de volver a verificar
                }
            } while (!allReady);
        }
    }

    private void executeOcLogin(String servidor, String usuario, String token) throws Exception {
        List<String> loginCommand = Arrays.asList("oc", "login", servidor, "--token=" + token,
                "--insecure-skip-tls-verify=true");
        executeOcCommand(loginCommand); // Utiliza executeOcCommand para ejecutar el login
    }

    private String executeOcCommand(List<String> commands) throws Exception {
        ProcessBuilder processBuilder = new ProcessBuilder(commands);
        Process process = processBuilder.start();

        try (BufferedReader reader = new BufferedReader(new InputStreamReader(process.getInputStream()))) {
            String output = reader.lines().collect(Collectors.joining("\n"));
            process.waitFor();
            if (process.exitValue() != 0) {
                throw new RuntimeException("El comando oc falló.");
            }
            return output;
        }

    }

    private boolean checkPodsReady(List<String> podNames, String servidor, String usuario, String token)
            throws Exception {
        executeOcLogin(servidor, usuario, token); // Asegura que estés autenticado

        for (String podName : podNames) {
            List<String> command = Arrays.asList("oc", "get", "pods", "-n", NAMESPACE,
                    "--field-selector=status.phase!=Running", "-l", "name=" + podName, "-o",
                    "custom-columns=STATUS:.status.phase", "--no-headers");
            String output = executeOcCommand(command);

            if (!output.isEmpty() && !output.trim().isEmpty()) {
                return false;
            }
        }

        return true; // Todos los pods están en estado Running
    }
}
