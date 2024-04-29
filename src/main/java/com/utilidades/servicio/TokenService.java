package com.utilidades.servicio;

import org.springframework.stereotype.Service;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;

@Service
public class TokenService {

    public String getToken(String server, String username, String password) throws IOException, InterruptedException {
        String command = String.format("/app/get-token.sh %s %s %s", server, username, password);
        Process process = Runtime.getRuntime().exec(command);
        BufferedReader stdInput = new BufferedReader(new InputStreamReader(process.getInputStream()));
        BufferedReader stdError = new BufferedReader(new InputStreamReader(process.getErrorStream()));
    
        // Lee la salida del comando
        String s;
        StringBuilder token = new StringBuilder();
        while ((s = stdInput.readLine()) != null) {
            token.append(s);
        }
    
        // Lee cualquier error del comando
        StringBuilder error = new StringBuilder();
        while ((s = stdError.readLine()) != null) {
            error.append(s);
        }
    
        int exitVal = process.waitFor();
        if (exitVal == 0) {
            return token.toString().trim();
        } else {
            throw new RuntimeException("Failed to obtain token: " + error.toString());
        }
    }
    
    
}
