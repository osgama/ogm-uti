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
        Config config = new ConfigBuilder()
                .withPassword(token)
                .withMasterUrl(servidor)
                .build();

        KubernetesClient kubernetesClient = new KubernetesClientBuilder().withConfig(config).build();
        return kubernetesClient.adapt(OpenShiftClient.class);
    }

    public void scaleDownPods(String token, String servidor) throws Exception {
        try (OpenShiftClient openShiftClient = createOpenShiftClient(token, servidor)) {
            listaDePods.forEach(podName -> {
                DeploymentConfig dc = openShiftClient.deploymentConfigs().inNamespace(NAMESPACE).withName(podName)
                        .get();
                if (dc != null && dc.getSpec() != null) {
                    openShiftClient.deploymentConfigs().inNamespace(NAMESPACE).withName(podName).scale(0);
                }
            });
        }
    }

    public void scaleUpPodsInBlocks(String token, String servidor) throws Exception {
        try (OpenShiftClient openShiftClient = createOpenShiftClient(token, servidor)) {
            for (int i = 0; i < listaDePods.size(); i += BLOCK_SIZE) {
                List<String> currentBlock = listaDePods.subList(i, Math.min(i + BLOCK_SIZE, listaDePods.size()));
                currentBlock.forEach(podName -> {
                    DeploymentConfig dc = openShiftClient.deploymentConfigs().inNamespace(NAMESPACE).withName(podName)
                            .get();
                    if (dc != null && dc.getSpec() != null) {
                        openShiftClient.deploymentConfigs().inNamespace(NAMESPACE).withName(podName).scale(1);
                    }
                });

                // Espera a que todos los pods estén listos antes de continuar
                boolean allReady;
                do {
                    allReady = checkPodsReady(openShiftClient, currentBlock);
                    Thread.sleep(10000); // Espera 10 segundos antes de volver a verificar
                } while (!allReady);
                // Espera 2 minutos antes de avanzar al siguiente bloque de pods
                System.out.println(
                        "Todos los pods del bloque actual están listos. Esperando 2 minutos antes de continuar con el siguiente bloque.");
                Thread.sleep(120000); // 120000 milisegundos = 2 minutos
            }
        }
    }

    private boolean checkPodsReady(OpenShiftClient openShiftClient, List<String> podNames) {
        return podNames.stream()
                .allMatch(podName -> openShiftClient.pods().inNamespace(NAMESPACE).withLabel("name", podName).list()
                        .getItems().stream()
                        .allMatch(pod -> "Running".equals(pod.getStatus().getPhase())));
    }
}
