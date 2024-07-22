package com.utilidades.ps.api;

import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import org.slf4j.*;
import com.utilidades.ps.servicio.PodServiceAutomatico;

@RestController
public class ApiPodAutomatico {

    private static final Logger logger = LoggerFactory.getLogger(ApiPodAutomatico.class);
    private final PodServiceAutomatico podServiceAutomatico;

    public ApiPodAutomatico(PodServiceAutomatico podServiceAutomatico) {
        this.podServiceAutomatico = podServiceAutomatico;
    }

    @GetMapping("/StopPA")
    public ResponseEntity<String> scaleDownPods(
            @RequestParam String servidor,
            @RequestParam String usuario,
            @RequestParam String password,
            @RequestParam String opcion) {
        try {
            podServiceAutomatico.scaleDownPods(usuario, password, servidor, opcion);
            return ResponseEntity.ok("Detención de pods completada.");
        } catch (Exception e) {
            logger.error("Error deteniendo pods: {}", e.getMessage(), e);
            return ResponseEntity.internalServerError().body("Error deteniendo pods, favor de revisar las logs.");
        }
    }

    @GetMapping("/StartPA")
    public ResponseEntity<String> scaleUpPodsInBlocks(
            @RequestParam String servidor,
            @RequestParam String usuario,
            @RequestParam String password,
            @RequestParam String opcion) {
        try {
            podServiceAutomatico.scaleUpPodsInBlocks(usuario, password, servidor, opcion);
            return ResponseEntity.ok("Inicio de pods completado.");
        } catch (Exception e) {
            logger.error("Error iniciando pods: {}", e.getMessage(), e);
            return ResponseEntity.internalServerError().body("Error iniciando pods, favor de revisar las logs.");
        }
    }

    @GetMapping("/DeleteCPA")
    public ResponseEntity<String> deleteCompletedPods(
            @RequestParam String servidor,
            @RequestParam String usuario,
            @RequestParam String password) {
        try {
            podServiceAutomatico.deleteCompletedPods(usuario, password, servidor);
            return ResponseEntity.ok("Eliminación de deployments de pods completados terminada.");
        } catch (Exception e) {
            logger.error("Error eliminando deployments de pods completados: {}", e.getMessage(), e);
            return ResponseEntity.internalServerError().body("Error eliminando deployments de pods completados, favor de revisar las logs.");
        }
    }
}