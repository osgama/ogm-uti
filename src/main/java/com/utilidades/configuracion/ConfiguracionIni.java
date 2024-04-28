package com.utilidades.configuracion;
/*
import org.ini4j.Wini;
import java.io.File;
import java.io.IOException;

public class ConfiguracionIni {
    private Wini ini;
    private String rutaArchivo;

    public ConfiguracionIni(String rutaArchivo) throws IOException {
        this.rutaArchivo = rutaArchivo;
        cargarIni();
    }

    private void cargarIni() throws IOException {
        ini = new Wini(new File(rutaArchivo));
    }

    public void actualizarConfiguracion(String fechainicial, String fechafinal, String parametro) throws IOException {
        ini.put("DEFAULT", "fechainicial", fechainicial);
        ini.put("DEFAULT", "fechafinal", fechafinal);
        ini.put("DEFAULT", "parametro", parametro);
        ini.store();
    }

    public Wini obtenerConfiguracion() {
        return ini;
    }
}*/