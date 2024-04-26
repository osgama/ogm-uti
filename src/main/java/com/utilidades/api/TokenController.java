package com.utilidades.api;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import com.utilidades.servicio.TokenService;

@RestController
@RequestMapping("/api")
public class TokenController {

    @Autowired
    private TokenService tokenService;

    @GetMapping("/get-token")
    public ResponseEntity<String> getToken(@RequestParam String server,
                                           @RequestParam String username,
                                           @RequestParam String password) {
        try {
            String token = tokenService.getToken(server, username, password);
            return ResponseEntity.ok(token);
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("Failed to obtain token: " + e.getMessage());
        }
    }
}
