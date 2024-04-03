package com.utilidades.api;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import com.utilidades.servicio.PodService;
@RestController
public class PodController {

    private final PodService podService;

    public PodController(PodService podService) {
        this.podService = podService;
    }

    @GetMapping("/scaleDown")
    public String scaleDownPods(@RequestParam String token, @RequestParam String servidor) {
        try {
            podService.scaleDownPods(token, servidor);
        } catch (Exception e) {
            e.printStackTrace();
        }
        return "Scaling down in progress...";
    }

    @GetMapping("/scaleUpInBlocks")
    public String scaleUpPodsInBlocks(@RequestParam String token, @RequestParam String servidor) {

        try {
            podService.scaleUpPodsInBlocks(token, servidor);
        } catch (Exception e) {
            e.printStackTrace();
        }
        return "Scaling up in progress...";
    }
}