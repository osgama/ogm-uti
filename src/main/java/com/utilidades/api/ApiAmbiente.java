package com.utilidades.api;

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
public class ApiAmbiente {

    @GetMapping("/api/ambiente")
    public ResponseEntity<String> obtenerAmbiente() {
        String ambiente = System.getenv("ENVIRONMENT");
        if (ambiente != null && !ambiente.isEmpty()) {
            String valor;

            switch (ambiente) {
                case "dev":
                    valor = "DEV";
                    break;
                case "uat":
                    valor = "UAT";
                    break;
                case "prod":
                    valor = "PROD";
                    break;
                case "cob":
                    valor = "COB";
                    break;
                default:
                    valor = "";
                    break;
            }
            return ResponseEntity.ok(valor);
        } else {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body("SINAMBIENTE");
        }
    }
}