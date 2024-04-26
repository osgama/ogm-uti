package com.utilidades.servicio;

import org.springframework.stereotype.Service;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;

@Service
public class TokenService {

    public String getToken(String server, String username, String password) throws IOException, InterruptedException {
        String command = String.format("./get-token.sh %s %s %s", server, username, password);
        Process process = Runtime.getRuntime().exec(command);
        BufferedReader reader = new BufferedReader(new InputStreamReader(process.getInputStream()));
        
        String line;
        StringBuilder output = new StringBuilder();
        while ((line = reader.readLine()) != null) {
            output.append(line + "\n");
        }

        int exitVal = process.waitFor();
        if (exitVal == 0) {
            return output.toString().trim();  // Asume que el script imprime el token directamente
        } else {
            throw new RuntimeException("Error in script execution: " + output.toString());
        }
    }
}
