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

    @GetMapping("/StopPods")
    public String scaleDownPods(@RequestParam String servidor, @RequestParam String usuario, @RequestParam String token) {
        try {
            podService.scaleDownPods(servidor, usuario,token);
        } catch (Exception e) {
            e.printStackTrace();
        }
        return "Deteniendo pods, en progreso...";
    }

    @GetMapping("/StartPods")
    public String scaleUpPodsInBlocks(@RequestParam String servidor, @RequestParam String usuario, @RequestParam String token) {

        try {
            podService.scaleUpPodsInBlocks(servidor, usuario, token);
        } catch (Exception e) {
            e.printStackTrace();
        }
        return "Iniciando pods, en progreso...";
    }
}
