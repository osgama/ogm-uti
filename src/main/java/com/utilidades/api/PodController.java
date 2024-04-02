package com.utilidades.api;

import org.springframework.web.servlet.mvc.method.annotation.SseEmitter;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;
import java.io.IOException;

import com.utilidades.servicio.PodService;

@RestController
public class PodController {

    private final PodService podService;

    public PodController(PodService podService) {
        this.podService = podService;
    }

    @GetMapping("/scaleDown")
    public SseEmitter scaleDownPods() {
        final SseEmitter emitter = new SseEmitter();
        new Thread(() -> {
            try {
                podService.scaleDownPods(emitter);
            } catch (IOException e) {
                emitter.completeWithError(e);
            }
        }).start();
        return emitter;
    }

    @GetMapping("/scaleUpInBlocks")
    public SseEmitter scaleUpPodsInBlocks() {
        final SseEmitter emitter = new SseEmitter();
        new Thread(() -> {
            try {
                podService.scaleUpPodsInBlocks(emitter);
            } catch (Exception e) {
                emitter.completeWithError(e);
            }
        }).start();
        return emitter;
    }
}
