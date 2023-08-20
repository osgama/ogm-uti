package com.utilidades.api;

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
public class ApiAmbiente {

    @GetMapping("/api/ambiente")
    public ResponseEntity<String> obtenerAmbiente() {
       // String ambiente = System.getenv("MI_AMBIENTE");
       String ambiente = "dev";
        if (ambiente != null && !ambiente.isEmpty()) {
            String valor;

            switch (ambiente) {
                case "dev":
                    valor = "DEV";
                    break;
                case "uat":
                    valor = "UAT";
                    break;
                default:
                    valor = "SINAMBIENTE";
                    break;
            }

            return ResponseEntity.ok(valor);
        } else {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body("SINAMBIENTE");
        }
    }
}