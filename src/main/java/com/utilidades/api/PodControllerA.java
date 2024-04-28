package com.utilidades.api;

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;

@RestController
public class PodControllerA {

    private static final Logger logger = LoggerFactory.getLogger(PodControllerA.class);

    @GetMapping("/scaleUp")
    public ResponseEntity<String> scaleUpPods(@RequestParam String option, @RequestParam String server, @RequestParam String user, @RequestParam String password) {
        String[] command = {"bash", "/path/to/openshift_operations.sh", server, user, password, "scale-up", option};
        return runScript(command);
    }

    @GetMapping("/scaleDown")
    public ResponseEntity<String> scaleDownPods(@RequestParam String option, @RequestParam String server, @RequestParam String user, @RequestParam String password) {
        String[] command = {"bash", "/path/to/openshift_operations.sh", server, user, password, "scale-down", option};
        return runScript(command);
    }

    @GetMapping("/deleteCompleted")
    public ResponseEntity<String> deleteCompletedPods(@RequestParam String server, @RequestParam String user, @RequestParam String password) {
        String[] command = {"bash", "/path/to/openshift_operations.sh", server, user, password, "delete-completed", ""};
        return runScript(command);
    }

    private ResponseEntity<String> runScript(String[] command) {
        ProcessBuilder processBuilder = new ProcessBuilder(command);
        StringBuilder output = new StringBuilder();

        try {
            Process process = processBuilder.start();
            BufferedReader reader = new BufferedReader(new InputStreamReader(process.getInputStream()));
            BufferedReader errorReader = new BufferedReader(new InputStreamReader(process.getErrorStream()));

            String line;
            while ((line = reader.readLine()) != null) {
                logger.info(line);
                output.append(line).append("\n");
            }
            while ((line = errorReader.readLine()) != null) {
                logger.error(line);
                output.append("ERROR: ").append(line).append("\n");
            }

            int exitVal = process.waitFor();
            if (exitVal != 0) {
                logger.error("Error al ejecutar el script con código de salida: {}", exitVal);
                return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR)
                                     .body("Error en la ejecución del script con código de salida: " + exitVal + "\n" + output.toString());
            }
        } catch (IOException | InterruptedException e) {
            logger.error("Error al ejecutar el proceso: {}", e.getMessage(), e);
            Thread.currentThread().interrupt();
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR)
                                 .body("Error al ejecutar el proceso: " + e.getMessage());
        }

        return ResponseEntity.ok("Proceso completado correctamente:\n" + output.toString());
    }
}

