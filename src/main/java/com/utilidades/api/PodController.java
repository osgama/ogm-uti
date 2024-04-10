package com.utilidades.api;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import com.utilidades.servicio.PodService;
import org.springframework.web.servlet.mvc.method.annotation.SseEmitter;
import java.io.IOException;

@RestController
public class PodController {

    private final PodService podService;

    public PodController(PodService podService) {
        this.podService = podService;
    }

    @GetMapping("/ScaleDownPods")
    public SseEmitter scaleDownPods(@RequestParam String token, @RequestParam String servidor, @RequestParam String opcion) {
        final SseEmitter emitter = new SseEmitter(Long.MAX_VALUE);
        Thread thread = new Thread(() -> {
            try {
                podService.scaleDownPods(token, servidor, opcion, emitter);
                emitter.complete();
            } catch (Exception e) {
                try {
                    emitter.send(SseEmitter.event().name("error").data("Error al detener el sistema: " + e.getMessage()));
                } catch (IOException ioException) {
                    ioException.printStackTrace();
                }
                emitter.completeWithError(e);
            }
        });
        thread.start();
        return emitter;
    }

    @GetMapping("/ScaleUpPods")
    public SseEmitter scaleUpPodsInBlocks(@RequestParam String token, @RequestParam String servidor, @RequestParam String opcion) {
        final SseEmitter emitter = new SseEmitter(Long.MAX_VALUE);
        Thread thread = new Thread(() -> {
            try {
                podService.scaleUpPodsInBlocks(token, servidor, opcion, emitter);
                emitter.complete();
            } catch (Exception e) {
                try {
                    emitter.send(SseEmitter.event().name("error").data("Error al iniciar el sistema: " + e.getMessage()));
                } catch (IOException ioException) {
                    ioException.printStackTrace();
                }
                emitter.completeWithError(e);
            }
        });
        thread.start();
        return emitter;
    }
}
