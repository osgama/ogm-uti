package com.utilidades.servicio;

import org.springframework.stereotype.Service;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;

@Service
public class TokenService {

    public String getToken(String server, String username, String password) throws IOException, InterruptedException {
        for (int i = 0; i < 3; i++) {
            String command = String.format("/app/get-token.sh %s %s %s", server, username, password);
            Process process = Runtime.getRuntime().exec(command);
            BufferedReader reader = new BufferedReader(new InputStreamReader(process.getInputStream()));
            String token = reader.readLine();
            int exitVal = process.waitFor();
    
            if (exitVal == 0 && token != null && !token.isEmpty() && !token.contains("failed")) {
                return token.trim();
            } else {
                Thread.sleep(5000);  // Espera 5 segundos antes de intentar de nuevo
            }
        }
        throw new RuntimeException("Failed to obtain a valid token after several attempts.");
    }
    
}
