package com.utilidades.ps.servicio;

import org.springframework.stereotype.Service;
import java.io.*;
import org.slf4j.*;

@Service
public class TokenService {

    private static final Logger logger = LoggerFactory.getLogger(TokenService.class);

    public String getToken(String server, String username, String password) throws IOException, InterruptedException {
        logger.info(": : : : INICIA OBTENCION DE TOKEN");
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
            logger.error(": : : : ERROR OBTENIENDO EL TOKEN");
            error.append(s);
        }
    
        int exitVal = process.waitFor();
        if (exitVal == 0) {
            logger.info(": : : : TOKEN OBTENIDO CON EXITO");
            return token.toString().trim();
        } else {
            logger.error(": : : : FALLO AL OBTENER EL TOKEN");
            throw new RuntimeException("FALLO AL OBTENER EL TOKEN: " + error.toString());
        }
    } 
}