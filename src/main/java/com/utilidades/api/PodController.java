package com.utilidades.api;
/* 
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import com.utilidades.servicio.PodService;
import org.springframework.web.servlet.mvc.method.annotation.SseEmitter;
import java.io.IOException;

@RestController
public class PodController {

    private static final Logger logger = LoggerFactory.getLogger(PodController.class);
    private final PodService podService;

    public PodController(PodService podService) {
        this.podService = podService;
    }

    @GetMapping("/ScaleDownPods")
    public SseEmitter scaleDownPods(@RequestParam String token, @RequestParam String servidor,
            @RequestParam String opcion) {
        final SseEmitter emitter = new SseEmitter(Long.MAX_VALUE);
        Thread thread = new Thread(() -> {
            try {
                podService.scaleDownPods(token, servidor, opcion, emitter);
                emitter.complete();
                logger.info("Detención completada para la opción: {}", opcion);
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

    @GetMapping("/ScaleUpPods")
    public SseEmitter scaleUpPodsInBlocks(@RequestParam String token, @RequestParam String servidor,
            @RequestParam String opcion) {
        final SseEmitter emitter = new SseEmitter(Long.MAX_VALUE);
        Thread thread = new Thread(() -> {
            try {
                podService.scaleUpPodsInBlocks(token, servidor, opcion, emitter);
                emitter.complete();
                logger.info("Inicio completado para la opción: {}", opcion);
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

    @GetMapping("/DeleteCompletedPods")
    public SseEmitter deleteCompletedPods(@RequestParam String token, @RequestParam String servidor) {
        final SseEmitter emitter = new SseEmitter(Long.MAX_VALUE);
        Thread thread = new Thread(() -> {
            try {
                podService.deleteCompletedPods(token, servidor, emitter);
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

}*/
