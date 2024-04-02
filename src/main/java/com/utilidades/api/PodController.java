package com.utilidades.api;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.servlet.mvc.method.annotation.SseEmitter;
import com.utilidades.servicio.PodService;

@RestController
public class PodController {

    private final PodService podService;

    public PodController(PodService podService) {
        this.podService = podService;
    }

    @GetMapping("/scaleDown")
    public SseEmitter scaleDownPods(@RequestParam String usuario, @RequestParam String token, @RequestParam String servidor) {
        final SseEmitter emitter = new SseEmitter();
        new Thread(() -> {
            try {
                podService.scaleDownPods(emitter, usuario, token, servidor);
            } catch (Exception e) {
                emitter.completeWithError(e);
            }
        }).start();
        return emitter;
    }

    @GetMapping("/scaleUpInBlocks")
    public SseEmitter scaleUpPodsInBlocks(@RequestParam String usuario, @RequestParam String token, @RequestParam String servidor) {
        final SseEmitter emitter = new SseEmitter();
        new Thread(() -> {
            try {
                podService.scaleUpPodsInBlocks(emitter, usuario, token, servidor);
            } catch (Exception e) {
                emitter.completeWithError(e);
            }
        }).start();
        return emitter;
    }
}
