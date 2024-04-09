package com.utilidades.api;

import java.io.IOException;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.servlet.mvc.method.annotation.SseEmitter;

@RestController
public class PruebaEventos {

    @GetMapping("/streamScaleDownPods")
    public SseEmitter scaleDownPods(@RequestParam String token, @RequestParam String servidor, @RequestParam String opcion) {
        final SseEmitter emitter = new SseEmitter(Long.MAX_VALUE);
        new Thread(() -> {
            try {
               // Simular el inicio del proceso
               emitter.send(SseEmitter.event().name("message").data("Iniciando simulación de detención de pods..."));

               // Simular la realización de operaciones, como la detención de pods
               Thread.sleep(1000); // Espera de 1 segundo para simular la operación
               emitter.send(SseEmitter.event().name("message").data("Deteniendo podName1..."));

               Thread.sleep(1000);
               emitter.send(SseEmitter.event().name("message").data("Deteniendo podName2..."));

               Thread.sleep(1000);
               emitter.send(SseEmitter.event().name("message").data("Deteniendo podName3..."));

               // Simular el fin del proceso
               Thread.sleep(1000);
               emitter.send(SseEmitter.event().name("message").data("Proceso completado"));
               emitter.complete();
            } catch (Exception e) {
                try {
                    emitter.send(SseEmitter.event().name("error").data("Error desconocido"));
                } catch (IOException ioException) {
                    ioException.printStackTrace();
                } finally {
                    emitter.completeWithError(e);
                }
            }
        }).start();
        return emitter;
    }

    @GetMapping("/streamScaleUpPods")
    public SseEmitter scaleUpPods(@RequestParam String token, @RequestParam String servidor, @RequestParam String opcion) {
        final SseEmitter emitter = new SseEmitter(Long.MAX_VALUE);
        new Thread(() -> {
            try {
                // Simulación de inicio de Pods
                emitter.send(SseEmitter.event().name("message").data("Iniciando simulación de arranque de pods..."));

                // Simular operaciones, como el arranque de pods
                Thread.sleep(1000);
                emitter.send(SseEmitter.event().name("message").data("Iniciando podName1..."));

                Thread.sleep(1000);
                emitter.send(SseEmitter.event().name("message").data("Iniciando podName2..."));

                Thread.sleep(1000);
                emitter.send(SseEmitter.event().name("message").data("Iniciando podName3..."));

                Thread.sleep(1000);
                emitter.send(SseEmitter.event().name("message").data("Simulación completada. Pods iniciados."));

                emitter.complete();
            } catch (Exception e) {
                try {
                    emitter.send(SseEmitter.event().name("error").data("Error desconocido"));
                } catch (IOException ioException) {
                    ioException.printStackTrace();
                } finally {
                    emitter.completeWithError(e);
                }
            }
        }).start();
        return emitter;
    }
}
