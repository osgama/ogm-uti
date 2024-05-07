package com.utilidades.api;

import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class PasswordController {

    @GetMapping("/api/getPassword")
    public ResponseEntity<?> getPassword(@RequestParam String nickname) {
        try {
            // Simulación de búsqueda de contraseña (reemplazar con lógica real)
            String password = findPasswordByNickname(nickname);
            if (password != null) {
                return ResponseEntity.ok().body("{ \"password\": \"" + password + "\" }");
            } else {
                // Si no se encuentra el password, se devuelve un error 404
                return ResponseEntity.notFound().build();
            }
        } catch (Exception e) {
            // Manejo de cualquier otra excepción
            return ResponseEntity.internalServerError().body("{ \"error\": \"Error al procesar la solicitud.\" }");
        }
    }

    // Simulación de un método para buscar la contraseña en la base de datos
    private String findPasswordByNickname(String nickname) {
        // Aquí deberías implementar la lógica de búsqueda real, por ahora es solo una simulación
        if ("user1".equals(nickname)) {
            return "password123";
        } else if ("user2".equals(nickname)) {
            return "abc123";
        }
        return null;
    }
}
