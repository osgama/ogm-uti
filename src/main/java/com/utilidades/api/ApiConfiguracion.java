package com.utilidades.api;
 
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import java.io.IOException;
import java.util.Map;

import com.utilidades.model.*;
import com.utilidades.configuracion.*;;

@RestController
@RequestMapping("/api/configuracion")
public class ApiConfiguracion {

    private static final String RUTA_ARCHIVO_INI = "ruta/del/archivo.ini";

    @GetMapping
    public ResponseEntity<?> obtenerConfiguracion() {
        try {
            Map<String, String> propiedades = IniFileHandler.leerArchivoIni(RUTA_ARCHIVO_INI);
            Configuracion configuracion = new Configuracion();
            configuracion.setParametro1(propiedades.get("parametro1"));
            configuracion.setParametro2(propiedades.get("parametro2"));
            configuracion.setParametro3(propiedades.get("parametro3"));
            return ResponseEntity.ok(configuracion);
        } catch (IOException e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("Error al leer el archivo: " + e.getMessage());
        }
    }

    @PostMapping
    public ResponseEntity<?> modificarConfiguracion(@RequestBody Configuracion configuracion) {
        try {
            Map<String, String> propiedades = IniFileHandler.leerArchivoIni(RUTA_ARCHIVO_INI);
            propiedades.put("parametro1", configuracion.getParametro1());
            propiedades.put("parametro2", configuracion.getParametro2());
            propiedades.put("parametro3", configuracion.getParametro3());
            IniFileHandler.escribirArchivoIni(RUTA_ARCHIVO_INI, propiedades);
            return ResponseEntity.ok("Configuración actualizada correctamente");
        } catch (IOException e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("Error al actualizar el archivo: " + e.getMessage());
        }
    }
}

