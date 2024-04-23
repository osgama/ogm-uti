package com.utilidades.api;


import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import com.utilidades.servicio.PodsOC;

@RestController
public class PodAutomatico {

    private static final Logger logger = LoggerFactory.getLogger(PodAutomatico.class);
    private final PodsOC podsOC;

    public PodAutomatico(PodsOC podsOC) {
        this.podsOC = podsOC;
    }

    @GetMapping("/ScaleDownPods")
    public ResponseEntity<String> scaleDownPods(@RequestParam String servidor, @RequestParam String usuario,
                                                @RequestParam String pwd, @RequestParam String opcion) {
        if (!podsOC.login(servidor, usuario, pwd)) {
            logger.warn("Authentication failed for user {}", usuario);
            return ResponseEntity.status(HttpStatus.UNAUTHORIZED).body("Failed to authenticate with OpenShift.");
        }
        try {
            podsOC.scaleDownPods(servidor, usuario, pwd, opcion);
            return ResponseEntity.ok("Detención de pods iniciada para la opción: " + opcion);
        } catch (Exception e) {
            logger.error("Error deteniendo pods: {}", e.getMessage(), e);
            return ResponseEntity.internalServerError().body("Error deteniendo pods: " + e.getMessage());
        }
    }

    @GetMapping("/ScaleUpPods")
    public ResponseEntity<String> scaleUpPodsInBlocks(@RequestParam String servidor, @RequestParam String usuario,
                                                     @RequestParam String pwd, @RequestParam String opcion) {
        if (!podsOC.login(servidor, usuario, pwd)) {
            logger.warn("Authentication failed for user {}", usuario);
            return ResponseEntity.status(HttpStatus.UNAUTHORIZED).body("Failed to authenticate with OpenShift.");
        }
        try {
            podsOC.scaleUpPodsInBlocks(servidor, usuario, pwd, opcion);
            return ResponseEntity.ok("Inicio de pods completado para la opción: " + opcion);
        } catch (Exception e) {
            logger.error("Error iniciando pods: {}", e.getMessage(), e);
            return ResponseEntity.internalServerError().body("Error iniciando pods: " + e.getMessage());
        }
    }

    @GetMapping("/DeleteCompletedPods")
    public ResponseEntity<String> deleteCompletedPods(@RequestParam String servidor, @RequestParam String usuario,
                                                     @RequestParam String pwd) {
        if (!podsOC.login(servidor, usuario, pwd)) {
            logger.warn("Authentication failed for user {}", usuario);
            return ResponseEntity.status(HttpStatus.UNAUTHORIZED).body("Failed to authenticate with OpenShift.");
        }
        try {
            podsOC.deleteCompletedPods(servidor, usuario, pwd);
            return ResponseEntity.ok("Eliminación de pods completados exitosa.");
        } catch (Exception e) {
            logger.error("Error eliminando pods completados: {}", e.getMessage(), e);
            return ResponseEntity.internalServerError().body("Error eliminando pods completados: " + e.getMessage());
        }
    }
}

