package com.utilidades.api;
 
import org.ini4j.Wini;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import java.io.IOException;

import com.utilidades.configuracion.*;

@RestController
public class ConfiguracionController {

    private ConfiguracionIni configuracion;

    public ConfiguracionController() throws IOException {
        this.configuracion = new ConfiguracionIni("ruta/a/tu/archivo.ini");
    }

    @GetMapping("/configuracion")
    public Wini getConfiguracion() {
        return configuracion.obtenerConfiguracion();
    }

    @PostMapping("/configuracion")
    public void actualizarConfiguracion(@RequestParam String fechainicial, @RequestParam String fechafinal, @RequestParam String parametro) throws IOException {
        configuracion.actualizarConfiguracion(fechainicial, fechafinal, parametro);
    }
}

