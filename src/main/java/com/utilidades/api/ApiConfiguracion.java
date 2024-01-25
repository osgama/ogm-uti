package com.utilidades.api;
 
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.util.Properties;

import com.utilidades.model.*;

@RestController
@RequestMapping("/api/configuracion")
public class ApiConfiguracion {

    private static final String RUTA_ARCHIVO_INI = "ruta/del/archivo.ini";

    @GetMapping
    public ResponseEntity<?> obtenerConfiguracion() {
        try {
            Properties prop = new Properties();
            Configuracion configuracion = new Configuracion();

            try (FileInputStream input = new FileInputStream(RUTA_ARCHIVO_INI)) {
                prop.load(input);
                configuracion.setParametro1(prop.getProperty("FECHAINICIAL"));
                configuracion.setParametro2(prop.getProperty("FECHAFINAL"));
                configuracion.setParametro3(prop.getProperty("REPROCESO"));
            }

            return ResponseEntity.ok(configuracion);

        } catch (IOException e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("Error al leer el archivo: " + e.getMessage());
        }
    }

    @PostMapping
    public ResponseEntity<?> modificarConfiguracion(@RequestBody Configuracion configuracion) {
        try {
            Properties prop = new Properties();
            try (FileInputStream input = new FileInputStream(RUTA_ARCHIVO_INI)) {
                prop.load(input);
            }

            prop.setProperty("FECHAINICIAL", configuracion.getParametro1());
            prop.setProperty("FECHAFINAL", configuracion.getParametro2());
            prop.setProperty("REPROCESO", configuracion.getParametro3());

            try (FileOutputStream output = new FileOutputStream(RUTA_ARCHIVO_INI)) {
                prop.store(output, null);
            }

            return ResponseEntity.ok("Configuración actualizada correctamente");

        } catch (IOException e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("Error al actualizar el archivo: " + e.getMessage());
        }
    }
}

