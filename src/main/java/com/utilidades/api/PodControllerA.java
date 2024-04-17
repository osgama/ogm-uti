package com.utilidades.api;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.http.ResponseEntity;
import com.utilidades.servicio.PodServiceA;

@RestController
public class PodControllerA {

    private static final Logger logger = LoggerFactory.getLogger(PodControllerA.class);
    private final PodServiceA podServiceA;

    public PodControllerA(PodServiceA podServiceA) {
        this.podServiceA = podServiceA;
    }

    @GetMapping("/ScaleDownPods")
    public ResponseEntity<String> scaleDownPods(@RequestParam String servidor, @RequestParam String usuario,
            @RequestParam String pwd, @RequestParam String opcion) {
        try {
            podServiceA.scaleDownPods(servidor, usuario, pwd, opcion);
            return ResponseEntity.ok("Detención de pods iniciada para la opción: " + opcion);
        } catch (Exception e) {
            logger.error("Error deteniendo pods: {}", e.getMessage(), e);
            return ResponseEntity.internalServerError().body("Error deteniendo pods: " + e.getMessage());
        }
    }

    @GetMapping("/ScaleUpPods")
    public ResponseEntity<String> scaleUpPodsInBlocks(@RequestParam String servidor, @RequestParam String usuario,
            @RequestParam String pwd, @RequestParam String opcion) {
        try {
            podServiceA.scaleUpPodsInBlocks(servidor, usuario, pwd, opcion);
            return ResponseEntity.ok("Inicio de pods completado para la opción: " + opcion);
        } catch (Exception e) {
            logger.error("Error iniciando pods: {}", e.getMessage(), e);
            return ResponseEntity.internalServerError().body("Error iniciando pods: " + e.getMessage());
        }
    }

    @GetMapping("/DeleteCompletedPods")
    public ResponseEntity<String> deleteCompletedPods(@RequestParam String servidor, @RequestParam String usuario,
            @RequestParam String pwd) {
        try {
            podServiceA.deleteCompletedPods(servidor, usuario, pwd);
            return ResponseEntity.ok("Eliminación de pods completados exitosa.");
        } catch (Exception e) {
            logger.error("Error eliminando pods completados: {}", e.getMessage(), e);
            return ResponseEntity.internalServerError().body("Error eliminando pods completados: " + e.getMessage());
        }
    }

}
