package com.utilidades.ps.api;

import org.ini4j.Wini;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import com.utilidades.ps.configuracion.ConfiguracionIni;

import java.io.IOException;

@RestController
@RequestMapping("/api/configuracion")
public class ApiConfiguracionIni {

    private ConfiguracionIni configuracion;

    public ApiConfiguracionIni() throws IOException {
        try {
            this.configuracion = new ConfiguracionIni("/opt/archivo.ini");
        } catch (IOException e) {
            throw new RuntimeException("Error al cargar el archivo de configuración: " + e.getMessage());
        }
    }

    @GetMapping
    public ResponseEntity<?> getConfiguracion() {
        try {
            Wini ini = configuracion.obtenerConfiguracion();
            return ResponseEntity.ok(ini);
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR)
                    .body("Error al obtener la configuración: " + e.getMessage());
        }
    }

    @PostMapping
    public ResponseEntity<?> actualizarConfiguracion(@RequestParam String DIAINICIAL, @RequestParam String DIAFINAL, @RequestParam String REPROCESO) {
        try {
            configuracion.actualizarConfiguracion(DIAINICIAL, DIAFINAL, REPROCESO);
            return ResponseEntity.ok("Configuración actualizada correctamente.");
        } catch (IOException e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR)
                    .body("Error al guardar la configuración: " + e.getMessage());
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST)
                    .body("Datos no válidos: " + e.getMessage());
        }
    }
}
