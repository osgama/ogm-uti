package com.utilidades.api;

import org.springframework.web.servlet.mvc.method.annotation.SseEmitter;
import org.springframework.web.bind.annotation.*;
import org.slf4j.*;
import java.io.*;

import com.utilidades.model.PodRequest;
import com.utilidades.servicio.PodService;

@RestController
public class ApiPodManual {

    private static final Logger logger = LoggerFactory.getLogger(ApiPodManual.class);
    private final PodService podService;

    public ApiPodManual(PodService podService) {
        this.podService = podService;
    }

    @PostMapping("/StopPods")
    public SseEmitter scaleDownPods(@RequestBody PodRequest request) {
        final SseEmitter emitter = new SseEmitter(Long.MAX_VALUE);
        Thread thread = new Thread(() -> {
            try {
                podService.scaleDownPods(
                        request.getUsuario(),
                        request.getPassword(),
                        request.getServidor(),
                        request.getOpcion(),
                        emitter);
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

    @PostMapping("/StartPods")
    public SseEmitter scaleUpPodsInBlocks(@RequestBody PodRequest request) {
        final SseEmitter emitter = new SseEmitter(Long.MAX_VALUE);
        Thread thread = new Thread(() -> {
            try {
                podService.scaleUpPodsInBlocks(
                        request.getUsuario(),
                        request.getPassword(),
                        request.getServidor(),
                        request.getOpcion(),
                        emitter);
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
                podService.deleteCompletedPods(
                        request.getUsuario(),
                        request.getPassword(),
                        request.getServidor(),
                        emitter);
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
}