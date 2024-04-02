package com.utilidades.servicio;

import org.springframework.web.servlet.mvc.method.annotation.SseEmitter;
import io.fabric8.kubernetes.api.model.apps.DeploymentBuilder;
import io.fabric8.kubernetes.api.model.apps.Deployment;
import io.fabric8.kubernetes.client.KubernetesClient;
import org.springframework.stereotype.Service;
import java.util.concurrent.TimeUnit;
import java.io.IOException;
import java.util.Arrays;
import java.util.List;


@Service
public class PodService {

    private final KubernetesClient kubernetesClient;
    private static final int BLOCK_SIZE = 4;
    private List<String> listaDePods = Arrays.asList("podName1", "podName2", "podName3", "podName16");

    public PodService(KubernetesClient kubernetesClient) {
        this.kubernetesClient = kubernetesClient;
    }

    public void scaleDownPods(SseEmitter emitter) throws IOException {
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
                
                try {
                    emitter.send(SseEmitter.event().name("scale-down").data("Scaled down " + podName));
                } catch (IOException e) {
                    // Log the error or handle it
                }
            }
        });
        emitter.complete();
    }

    public void scaleUpPodsInBlocks(SseEmitter emitter) throws IOException, InterruptedException {
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

                    try {
                        emitter.send(SseEmitter.event().name("scale-up").data("Scaled up " + podName));
                    } catch (IOException e) {
                        // Log the error or handle it
                    }
                }
            });

            boolean allReady;
            do {
                allReady = checkPodsReady(currentBlock);
                if (!allReady) {
                    TimeUnit.SECONDS.sleep(10);
                }
            } while (!allReady);

            emitter.send(SseEmitter.event().name("block-complete").data("Completed scaling block ending with " + currentBlock.get(currentBlock.size() - 1)));
        }
        emitter.complete();
    }

    private boolean checkPodsReady(List<String> podNames) {
        return podNames.stream().allMatch(podName ->
                kubernetesClient.pods().inNamespace("tu-namespace").withLabel("name", podName).list().getItems().stream().allMatch(pod ->
                        "Running".equals(pod.getStatus().getPhase())));
    }
}
