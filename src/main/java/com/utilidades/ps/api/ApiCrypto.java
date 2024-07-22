package com.utilidades.ps.api;

import java.net.URLEncoder;
import java.nio.charset.StandardCharsets;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.*;
import org.springframework.web.bind.annotation.*;
import com.utilidades.ps.servicio.CryptoService;

@RestController
@RequestMapping("/api/crypto")
public class ApiCrypto {

    @Autowired
    private CryptoService cryptoService;

    @PostMapping("/encrypt")
    public ResponseEntity<?> encrypt(@RequestBody String data) {
        try {
            return ResponseEntity.ok(cryptoService.encrypt(data));
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR)
                    .body("Error en la encriptación: " + e.getMessage());
        }
    }

    @PostMapping("/decrypt")
    public ResponseEntity<?> decrypt(@RequestBody String encryptedData) {
        try {
            return ResponseEntity.ok(cryptoService.decrypt(encryptedData));
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR)
                    .body("Error en la desencriptación: " + e.getMessage());
        }
    }

    @PostMapping("/urlEncode")
    public ResponseEntity<?> urlEncode(@RequestBody String encryptedData) {
        try {
            String urlEncoded = URLEncoder.encode(encryptedData, StandardCharsets.UTF_8.toString());
            return ResponseEntity.ok(urlEncoded);
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR)
                    .body("Error en la codificación URL: " + e.getMessage());
        }
    }
}