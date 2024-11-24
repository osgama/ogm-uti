package com.utilidades.ps.configuracion;

import org.ini4j.Wini;

import java.io.File;
import java.io.IOException;

public class ConfiguracionIni {
    private Wini ini;
    private final String rutaArchivo;

    public ConfiguracionIni(String rutaArchivo) throws IOException {
        this.rutaArchivo = rutaArchivo;
        cargarIni();
    }

    private void cargarIni() throws IOException {
        try {
            File file = new File(rutaArchivo);
            if (!file.exists()) {
                throw new IOException("El archivo de configuración no existe en la ruta: " + rutaArchivo);
            }
            ini = new Wini(file);
        } catch (IOException e) {
            throw new IOException("Error al cargar el archivo de configuración: " + e.getMessage());
        }
    }

    public void actualizarConfiguracion(String DIAINICIAL, String DIAFINAL, String REPROCESO) throws IOException {
        try {
            ini.put("ReproSys", "DIAINICIAL", DIAINICIAL);
            ini.put("ReproSys", "DIAFINAL", DIAFINAL);
            ini.put("ReproSys", "REPROCESO", REPROCESO);
            ini.store();
        } catch (IOException e) {
            throw new IOException("Error al guardar los cambios en el archivo de configuración: " + e.getMessage());
        }
    }

    public Wini obtenerConfiguracion() {
        try {
            return ini;
        } catch (Exception e) {
            throw new RuntimeException("Error al obtener la configuración: " + e.getMessage());
        }
    }
}
