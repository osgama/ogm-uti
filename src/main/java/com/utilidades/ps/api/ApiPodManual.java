package com.utilidades.ps.api;

import org.springframework.web.servlet.mvc.method.annotation.SseEmitter;
import org.springframework.web.bind.annotation.*;
import org.slf4j.*;
import java.io.*;

import com.utilidades.ps.servicio.PodServiceManual;

@RestController
@RequestMapping("/api")
public class ApiPodManual {

    private static final Logger logger = LoggerFactory.getLogger(ApiPodManual.class);
    private final PodServiceManual podServiceManual;

    public ApiPodManual(PodServiceManual podServiceManual) {
        this.podServiceManual = podServiceManual;
    }

    @GetMapping("/StopPods")
    public SseEmitter scaleDownPods(
            @RequestParam String usuario,
            @RequestParam String password,
            @RequestParam String servidor,
            @RequestParam String opcion) {
        SseEmitter emitter = new SseEmitter(Long.MAX_VALUE);
        new Thread(() -> {
            try {
                podServiceManual.scaleDownPods(usuario, password, servidor, opcion, emitter);
                emitter.complete();
            } catch (Exception e) {
                try {
                    emitter.send(SseEmitter.event().name("error").data("Error deteniendo pods: " + e.getMessage()));
                } catch (IOException ex) {
                    ex.printStackTrace();
                }
                emitter.completeWithError(e);
            }
        }).start();
        return emitter;
    }

    @GetMapping("/StartPods")
    public SseEmitter scaleUpPods(
            @RequestParam String usuario,
            @RequestParam String password,
            @RequestParam String servidor,
            @RequestParam String opcion) {
        SseEmitter emitter = new SseEmitter(Long.MAX_VALUE);
        new Thread(() -> {
            try {
                podServiceManual.scaleUpPodsInBlocks(usuario, password, servidor, opcion, emitter);
                emitter.complete();
            } catch (Exception e) {
                try {
                    emitter.send(SseEmitter.event().name("error").data("Error iniciando pods: " + e.getMessage()));
                } catch (IOException ex) {
                    ex.printStackTrace();
                }
                emitter.completeWithError(e);
            }
        }).start();
        return emitter;
    }

    @GetMapping("/DeleteCompletedPods")
    public SseEmitter deleteCompletedPods(
            @RequestParam String usuario,
            @RequestParam String password,
            @RequestParam String servidor) {
        SseEmitter emitter = new SseEmitter(Long.MAX_VALUE);
        new Thread(() -> {
            try {
                podServiceManual.deleteCompletedPods(usuario, password, servidor, emitter);
                emitter.complete();
            } catch (Exception e) {
                try {
                    emitter.send(SseEmitter.event().name("error").data("Error eliminando pods completados: " + e.getMessage()));
                } catch (IOException ex) {
                    ex.printStackTrace();
                }
                emitter.completeWithError(e);
            }
        }).start();
        return emitter;
    }
}