package com.utilidades.api;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import com.utilidades.servicio.PodServiceOC;

@RestController
public class PodControllerOC {

    private final PodServiceOC podServiceOC;

    public PodControllerOC(PodServiceOC podServiceOC) {
        this.podServiceOC = podServiceOC;
    }

    @GetMapping("/StopPods")
    public String scaleDownPods(@RequestParam String servidor, @RequestParam String usuario, @RequestParam String token) {
        try {
            podServiceOC.scaleDownPods(servidor, usuario, token);
            return "Deteniendo pods, en progreso...";
        } catch (Exception e) {
            e.printStackTrace();
            return "Error al detener los pods: " + e.getMessage();
        }
    }

    @GetMapping("/StartPods")
    public String scaleUpPodsInBlocks(@RequestParam String servidor, @RequestParam String usuario, @RequestParam String token) {
        try {
            podServiceOC.scaleUpPodsInBlocks(servidor, usuario, token);
            return "Iniciando pods, en progreso...";
        } catch (Exception e) {
            e.printStackTrace();
            return "Error al iniciar los pods: " + e.getMessage();
        }
    }
}
