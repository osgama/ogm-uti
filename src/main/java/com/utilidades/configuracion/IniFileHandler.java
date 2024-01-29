package com.utilidades.configuracion;

import java.io.*;
import java.util.LinkedHashMap;
import java.util.Map;

public class IniFileHandler {

    public static Map<String, String> leerArchivoIni(String rutaArchivo) throws IOException {
        Map<String, String> propiedades = new LinkedHashMap<>();
        try (BufferedReader reader = new BufferedReader(new FileReader(rutaArchivo))) {
            String line;
            while ((line = reader.readLine()) != null) {
                if (line.contains("=")) {
                    int index = line.indexOf("=");
                    String key = line.substring(0, index).trim();
                    String value = line.substring(index + 1).trim();
                    propiedades.put(key, value);
                }
            }
        }
        return propiedades;
    }

    public static void escribirArchivoIni(String rutaArchivo, Map<String, String> propiedades) throws IOException {
        try (BufferedWriter writer = new BufferedWriter(new FileWriter(rutaArchivo))) {
            for (Map.Entry<String, String> entry : propiedades.entrySet()) {
                writer.write(entry.getKey() + "=" + entry.getValue());
                writer.newLine();
            }
        }
    }
}
