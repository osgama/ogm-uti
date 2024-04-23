package com.utilidades.configuracion;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.util.List;
import java.util.ArrayList;

public class OpenShiftUtils {

    // Método para construir la base del comando de conexión y autenticación
    public static List<String> buildBaseCommand(String servidor, String usuario, String password, String namespace) {
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
        if (namespace != null && !namespace.isEmpty()) {
            baseCommand.add("--namespace=" + namespace);
        }
        return baseCommand;
    }

    // Método para realizar el login a OpenShift
    public static boolean login(String servidor, String usuario, String password) {
        List<String> command = new ArrayList<>();
        command.add("oc");
        command.add("login");
        command.add("--server=" + servidor);
        command.add("--username=" + usuario);
        command.add("--password=" + password);
        command.add("--insecure-skip-tls-verify=true");

        try {
            String output = executeOCCommand(command);
            return output.contains("Login successful") || output.contains("Logged into");
        } catch (Exception e) {
            System.err.println("Error during OpenShift login: " + e.getMessage());
            return false;
        }
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

