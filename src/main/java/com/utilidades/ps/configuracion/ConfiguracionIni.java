package com.utilidades.ps.configuracion;
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

    public void actualizarConfiguracion(String DIAINICIAL, String DIAFINAL, String REPROCESO) throws IOException {
        ini.put("Reprosys", "DIAINICIAL", DIAINICIAL);
        ini.put("Reprosys", "DIAFINAL", DIAFINAL);
        ini.put("Reprosys", "REPROCESO", REPROCESO);
        ini.store();
    }

    public Wini obtenerConfiguracion() {
        return ini;
    }
}*/