package com.utilidades.api;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import com.utilidades.servicio.CryptoService;

@RestController
@RequestMapping("/api/crypto")
public class CryptoController {

    @Autowired
    private CryptoService cryptoService;

    @PostMapping("/encrypt")
    public ResponseEntity<?> encrypt(@RequestBody String data) {
        try {
            return ResponseEntity.ok(cryptoService.encrypt(data));
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("Error en la encriptación: " + e.getMessage());
        }
    }
    
    @PostMapping("/decrypt")
    public ResponseEntity<?> decrypt(@RequestBody String encryptedData) {
        try {
            return ResponseEntity.ok(cryptoService.decrypt(encryptedData));
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("Error en la desencriptación: " + e.getMessage());
        }
    }
    
}
