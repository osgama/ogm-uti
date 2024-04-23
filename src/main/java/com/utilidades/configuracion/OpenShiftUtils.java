package com.utilidades.configuracion;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.util.List;
import java.util.ArrayList;

public class OpenShiftUtils {

    // Método para construir la base del comando de conexión
    public static List<String> buildBaseCommand(String servidor, String usuario, String password) {
        List<String> baseCommand = new ArrayList<>();
        baseCommand.add("oc");
        if (servidor != null && !servidor.isEmpty()) {
            baseCommand.add("--server=" + servidor);
        }
        if (usuario != null && !usuario.isEmpty()) {
            baseCommand.add("--username=" + usuario);
        }
        if (password != null && !password.isEmpty()) {
            baseCommand.add("--password=" + password);
        }
        return baseCommand;
    }

    // Método para ejecutar comandos de OC
    public static String executeOCCommand(List<String> commands) throws Exception {
        ProcessBuilder processBuilder = new ProcessBuilder(commands);
        processBuilder.redirectErrorStream(true);

        Process process = processBuilder.start();
        StringBuilder output = new StringBuilder();
        try (BufferedReader reader = new BufferedReader(
                new InputStreamReader(process.getInputStream()))) {
            String line;
            while ((line = reader.readLine()) != null) {
                output.append(line).append("\n");
            }
            process.waitFor();
        }

        return output.toString();
    }
}

