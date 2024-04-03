package com.utilidades.servicio;

import io.fabric8.kubernetes.api.model.apps.Deployment;
import io.fabric8.kubernetes.api.model.apps.DeploymentBuilder;
import io.fabric8.kubernetes.client.ConfigBuilder;
import io.fabric8.kubernetes.client.KubernetesClient;
import io.fabric8.kubernetes.client.KubernetesClientBuilder;
import org.springframework.stereotype.Service;
import java.util.Arrays;
import java.util.List;
import java.util.concurrent.TimeUnit;

@Service
public class PodService {

    private static final int BLOCK_SIZE = 4;
    private List<String> listaDePods = Arrays.asList("podName1", "podName2", "podName3", "podName16");

    public PodService() {
    }

    public void scaleDownPods(String token, String servidor) throws Exception {
        KubernetesClient kubernetesClient = new KubernetesClientBuilder()
                .withConfig(new ConfigBuilder()
                        .withMasterUrl(servidor)
                        .withOauthToken(token)
                        .build())
                .build();

        try {
            listaDePods.forEach(podName -> {
                Deployment deployment = kubernetesClient.apps().deployments()
                        .inNamespace("tu-namespace")
                        .withName(podName)
                        .get();

                if (deployment != null && deployment.getSpec() != null) {
                    kubernetesClient.apps().deployments().inNamespace("tu-namespace").withName(podName)
                            .patch(new DeploymentBuilder(deployment)
                                    .editSpec()
                                    .withReplicas(0)
                                    .endSpec()
                                    .build());
                }
            });
        } finally {
            kubernetesClient.close();
        }
    }

    public void scaleUpPodsInBlocks(String token, String servidor) throws Exception {
        KubernetesClient kubernetesClient = new KubernetesClientBuilder()
                .withConfig(new ConfigBuilder()
                        .withMasterUrl(servidor)
                        .withOauthToken(token)
                        .build())
                .build();

        try {
            for (int i = 0; i < listaDePods.size(); i += BLOCK_SIZE) {
                List<String> currentBlock = listaDePods.subList(i, Math.min(i + BLOCK_SIZE, listaDePods.size()));
                currentBlock.forEach(podName -> {
                    Deployment deployment = kubernetesClient.apps().deployments()
                            .inNamespace("tu-namespace")
                            .withName(podName)
                            .get();

                    if (deployment != null && deployment.getSpec() != null) {
                        kubernetesClient.apps().deployments().inNamespace("tu-namespace").withName(podName)
                                .patch(new DeploymentBuilder(deployment)
                                        .editSpec()
                                        .withReplicas(1)
                                        .endSpec()
                                        .build());
                    }
                });

                boolean allReady;
                do {
                    allReady = checkPodsReady(kubernetesClient, currentBlock);
                    if (!allReady) {
                        TimeUnit.SECONDS.sleep(10);
                    }
                } while (!allReady);
        
            }
        } finally {
            kubernetesClient.close();
        }
    }

    private boolean checkPodsReady(KubernetesClient kubernetesClient, List<String> podNames) {
        return podNames.stream().allMatch(podName ->
                kubernetesClient.pods().inNamespace("tu-namespace").withLabel("name", podName).list().getItems().stream().allMatch(pod ->
                        "Running".equals(pod.getStatus().getPhase())));
    }
}