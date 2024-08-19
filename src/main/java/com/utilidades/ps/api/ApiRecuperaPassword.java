package com.utilidades.ps.api;

import org.slf4j.*;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;


@RestController
public class ApiRecuperaPassword {

    private static final Logger logger = LoggerFactory.getLogger(ApiRecuperaPassword.class);

    @GetMapping("/api/getPassword")
    public ResponseEntity<?> getPassword(@RequestParam String nickname) {
        String ambiente = System.getenv("ENVIRONMENT");
        String password = "";
        String usuario = "";
        
        if (nickname.equals("1")){
            usuario = "usuario1";
            nickname = "nickname1";

        } else if (nickname.equals("2")) {
            usuario = "usuario2";
            nickname = "nickname2";

        } else if (nickname.equals("3")) {
            usuario = "usuario3";
            nickname = "nickname3";

        } else if (nickname.equals("4") && ambiente.equals("dev")) {
            usuario = "usuario4";
            nickname = "nickname4";

        } else if (nickname.equals("4") && ambiente.equals("dev")) {
            usuario = "usuario4";
            nickname = "nickname4";

        } else {
            logger.info(": : : : NICKNAME NO ENCONTRADO");
            logger.info(": : : : USUARIO NO ENCONTRADO");
        }

        logger.info(": : : : INICIA RECUPERACION DE PASSWORD");
        logger.info(": : : : NICKNAME: " + nickname);
        logger.info(": : : : USUARIO: " + usuario);

        try {
            password = findPasswordByNickname(nickname);
            if (password != null) {
                logger.info(": : : : : : : : : : TERMINA RECUPERACION DE PASSWORD");
                return ResponseEntity.ok().body("{ \"password\": \"" + password + "\" }");
            } else {
                logger.info(": : : : : : : : : : ERROR AL OBTENER PASSWORD");
                return ResponseEntity.notFound().build();
            }
        } catch (Exception e) {
            return ResponseEntity.internalServerError().body("{ \"error\": \"Error al procesar la solicitud.\" }");
        }
    }

    private String findPasswordByNickname(String nickname) {
        if ("user1".equals(nickname)) {
            return "password123";
        } else if ("user2".equals(nickname)) {
            return "abc123";
        }
        return null;
    }
}