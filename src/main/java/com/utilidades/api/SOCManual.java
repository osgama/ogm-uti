package com.utilidades.api;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;
import com.utilidades.servicio.PodService;
import org.springframework.web.servlet.mvc.method.annotation.SseEmitter;
import java.io.IOException;

@RestController
public class SOCManual {

    private static final Logger logger = LoggerFactory.getLogger(SOCManual.class);
    private final PodService podService;

    public SOCManual(PodService podService) {
        this.podService = podService;
    }

    @PostMapping("/ScaleDownPods")
    public SseEmitter scaleDownPods(@RequestBody PodRequest request) {
        final SseEmitter emitter = new SseEmitter(Long.MAX_VALUE);
        Thread thread = new Thread(() -> {
            try {
                podService.scaleDownPods(request.getUsuario(), request.getPassword(), request.getServidor(), request.getOpcion(), emitter);
                emitter.complete();
                logger.info("Detención completada para la opción: {}", request.getOpcion());
            } catch (Exception e) {
                String errorMsg = "Error deteniendo pods: " + e.getMessage();
                try {
                    emitter.send(SseEmitter.event().name("error").data(errorMsg));
                    logger.error(errorMsg, e);
                } catch (IOException ioException) {
                    logger.error("Error enviando mensaje de error: {}", ioException.getMessage(), ioException);
                }
                emitter.completeWithError(e);
            }
        });
        thread.start();
        return emitter;
    }

    @PostMapping("/ScaleUpPods")
    public SseEmitter scaleUpPodsInBlocks(@RequestBody PodRequest request) {
        final SseEmitter emitter = new SseEmitter(Long.MAX_VALUE);
        Thread thread = new Thread(() -> {
            try {
                podService.scaleUpPodsInBlocks(request.getUsuario(), request.getPassword(), request.getServidor(), request.getOpcion(), emitter);
                emitter.complete();
                logger.info("Inicio completado para la opción: {}", request.getOpcion());
            } catch (Exception e) {
                String errorMsg = "Error iniciando pods: " + e.getMessage();
                try {
                    emitter.send(SseEmitter.event().name("error").data(errorMsg));
                    logger.error(errorMsg, e);
                } catch (IOException ioException) {
                    logger.error("Error enviando mensaje de error: {}", ioException.getMessage(), ioException);
                }
                emitter.completeWithError(e);
            }
        });
        thread.start();
        return emitter;
    }

    @PostMapping("/DeleteCompletedPods")
    public SseEmitter deleteCompletedPods(@RequestBody PodRequest request) {
        final SseEmitter emitter = new SseEmitter(Long.MAX_VALUE);
        Thread thread = new Thread(() -> {
            try {
                podService.deleteCompletedPods(request.getUsuario(), request.getPassword(), request.getServidor(), emitter);
                emitter.complete();
                logger.info("Eliminación de pods completados exitosa.");
            } catch (Exception e) {
                String errorMsg = "Error eliminando pods completados: " + e.getMessage();
                try {
                    emitter.send(SseEmitter.event().name("error").data(errorMsg));
                    logger.error(errorMsg, e);
                } catch (IOException ioException) {
                    logger.error("Error enviando mensaje de error: {}", ioException.getMessage(), ioException);
                }
                emitter.completeWithError(e);
            }
        });
        thread.start();
        return emitter;
    }

    public static class PodRequest {
        private String usuario;
        private String password;
        private String servidor;
        private String opcion;

        // Getters y Setters
        public String getUsuario() { return usuario; }
        public void setUsuario(String usuario) { this.usuario = usuario; }

        public String getPassword() { return password; }
        public void setPassword(String password) { this.password = password; }

        public String getServidor() { return servidor; }
        public void setServidor(String servidor) { this.servidor = servidor; }

        public String getOpcion() { return opcion; }
        public void setOpcion(String opcion) { this.opcion = opcion; }
    }
}
