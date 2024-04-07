package com.utilidades.servicio;

import io.fabric8.openshift.client.OpenShiftClient;
import io.fabric8.kubernetes.client.KubernetesClient;
import io.fabric8.kubernetes.client.KubernetesClientBuilder;
import io.fabric8.openshift.api.model.DeploymentConfig;
import org.springframework.stereotype.Service;
import java.util.Arrays;
import java.util.List;

import io.fabric8.kubernetes.client.Config;
import io.fabric8.kubernetes.client.ConfigBuilder;

@Service
public class PodService {

    private static final int BLOCK_SIZE = 4;
    private List<String> listaDePods = Arrays.asList("podName1", "podName2", "podName3", "podName16");
    private static final String NAMESPACE = "tu-namespace";


    private OpenShiftClient createOpenShiftClient(String token, String servidor) {
        System.out.println("Intentando crear cliente de OpenShift...");

        try {
            Config config = new ConfigBuilder()
                    .withOauthToken(token) // Se recomienda usar withOauthToken para tokens de acceso
                    .withMasterUrl(servidor)
                    .withTrustCerts(true) // Considera las implicaciones de seguridad de esta opción
                    .build();

            KubernetesClient kubernetesClient = new KubernetesClientBuilder().withConfig(config).build();
            kubernetesClient.namespaces().list();

            System.out.println("Cliente de OpenShift creado y validado exitosamente.");
            return kubernetesClient.adapt(OpenShiftClient.class);
        } catch (Exception e) {
            System.err.println("Error al crear el cliente de OpenShift: " + e.getMessage());
            throw new RuntimeException("Error al establecer conexión con OpenShift: " + e.getMessage(), e);
        }
    }

    public void scaleDownPods(String token, String servidor) throws Exception {
        System.out.println("Iniciando el escalado hacia abajo de los pods...");
        try (OpenShiftClient openShiftClient = createOpenShiftClient(token, servidor)) {
            listaDePods.forEach(podName -> {
                try {
                    DeploymentConfig dc = openShiftClient.deploymentConfigs().inNamespace(NAMESPACE).withName(podName).get();
                    if (dc != null && dc.getSpec() != null) {
                        openShiftClient.deploymentConfigs().inNamespace(NAMESPACE).withName(podName).scale(0);
                        System.out.println("Pod " + podName + " escalado a 0 exitosamente.");
                    }
                } catch (Exception e) {
                    System.err.println("Error al escalar el pod " + podName + ": " + e.getMessage());
                }
            });
        } catch (Exception e) {
            System.err.println("Error al intentar escalar los pods hacia abajo: " + e.getMessage());
        }
        System.out.println("Escalado hacia abajo de pods completado.");
    }

    public void scaleUpPodsInBlocks(String token, String servidor) throws Exception {
        System.out.println("Iniciando el escalado hacia arriba de los pods en bloques...");
        try (OpenShiftClient openShiftClient = createOpenShiftClient(token, servidor)) {
            for (int i = 0; i < listaDePods.size(); i += BLOCK_SIZE) {
                List<String> currentBlock = listaDePods.subList(i, Math.min(i + BLOCK_SIZE, listaDePods.size()));
                System.out.println("Escalando el siguiente bloque de pods: " + currentBlock);
                currentBlock.forEach(podName -> {
                    try {
                        DeploymentConfig dc = openShiftClient.deploymentConfigs().inNamespace(NAMESPACE).withName(podName).get();
                        if (dc != null && dc.getSpec() != null) {
                            openShiftClient.deploymentConfigs().inNamespace(NAMESPACE).withName(podName).scale(1);
                            System.out.println("Pod " + podName + " escalado a 1 exitosamente.");
                        }
                    } catch (Exception e) {
                        System.err.println("Error al escalar el pod " + podName + ": " + e.getMessage());
                    }
                });

                boolean allReady;
                do {
                    allReady = checkPodsReady(openShiftClient, currentBlock);
                    if (!allReady) {
                        System.out.println("Esperando a que todos los pods del bloque actual estén listos...");
                        Thread.sleep(10000); // Espera 10 segundos antes de volver a verificar
                    }
                } while (!allReady);

                System.out.println("Todos los pods del bloque actual están listos. Esperando 2 minutos antes de continuar con el siguiente bloque.");
                Thread.sleep(120000); // 120000 milisegundos = 2 minutos
            }
        } catch (Exception e) {
            System.err.println("Error durante el escalado hacia arriba de los pods: " + e.getMessage());
        }
        System.out.println("Escalado hacia arriba de pods en bloques completado.");
    }

    private boolean checkPodsReady(OpenShiftClient openShiftClient, List<String> podNames) {
        return podNames.stream()
                .allMatch(podName -> {
                    try {
                        return openShiftClient.pods().inNamespace(NAMESPACE).withLabel("name", podName).list().getItems().stream().allMatch(pod -> "Running".equals(pod.getStatus().getPhase()));
                    } catch (Exception e) {
                        System.err.println("Error al verificar si los pods están listos: " + e.getMessage());
                        return false;
                    }
                });
    }
}
