package com.utilidades.ps.api;

import org.slf4j.*;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import java.util.*;
import java.util.stream.Collectors;

@RestController
public class ApiRecuperaPassword {

    private static final Logger logger = LoggerFactory.getLogger(ApiRecuperaPassword.class);

    @GetMapping("/api/getPasswords")
    public ResponseEntity<?> getPasswords() {
        logger.info(": : : : INICIA RECUPERACIÓN DE TODOS LOS PASSWORDS");

        try {
            List<Map<String, String>> userPasswords = obtenerUsuariosYPasswords();

            if (userPasswords.isEmpty()) {
                logger.info(": : : : : : : : : : NO HAY DATOS DE USUARIOS O CONTRASEÑAS");
                return ResponseEntity.notFound().build();
            }

            logger.info(": : : : : : : : : : TERMINA RECUPERACIÓN DE PASSWORDS");
            return ResponseEntity.ok().body(userPasswords);
        } catch (Exception e) {
            logger.error("Error al procesar la solicitud de recuperación de passwords.", e);
            return ResponseEntity.internalServerError()
                    .body(Collections.singletonMap("error", "Error al procesar la solicitud."));
        }
    }

    private List<Map<String, String>> obtenerUsuariosYPasswords() {
        String usuariosEnv = System.getenv("USER");
        String nicknamesEnv = System.getenv("NICKNAMES");

        if (usuariosEnv == null || nicknamesEnv == null) {
            logger.error("Las variables de entorno USER o NICKNAMES no están configuradas.");
            throw new IllegalArgumentException("Variables de entorno USER o NICKNAMES no configuradas.");
        }

        List<String> usuarios = Arrays.stream(usuariosEnv.split(","))
                .map(String::trim)
                .collect(Collectors.toList());

        List<String> nicknames = Arrays.stream(nicknamesEnv.split(","))
                .map(String::trim)
                .collect(Collectors.toList());

        if (usuarios.size() != nicknames.size()) {
            logger.error("La cantidad de usuarios y nicknames no coincide.");
            throw new IllegalStateException("El número de usuarios y nicknames debe coincidir.");
        }

        List<Map<String, String>> userPasswords = new ArrayList<>();
        for (int i = 0; i < usuarios.size(); i++) {
            Map<String, String> userPassword = new HashMap<>();
            userPassword.put("usuario", usuarios.get(i));

            // Obtener contraseña con reintentos
            String password = obtenerPasswordConReintentos(nicknames.get(i));
            userPassword.put("password", password);

            userPasswords.add(userPassword);
        }

        return userPasswords;
    }

    private String obtenerPasswordConReintentos(String nickname) {
        int reintentos = 3;
        for (int intento = 1; intento <= reintentos; intento++) {
            try {
                // Simulación de recuperación desde CyberArk
                logger.info("Intento {} para obtener contraseña de {}", intento, nickname);
                return "password" + intento; // Simular recuperación exitosa
            } catch (Exception e) {
                logger.warn("Error al recuperar contraseña en intento {}: {}", intento, e.getMessage());
            }
        }
        logger.error("Fallaron todos los intentos para obtener la contraseña de {}", nickname);
        return "SIN PASSWORD"; // Fallback si todos los intentos fallan
    }
}
