package com.utilidades.ps.api;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api")
public class ApiAmbiente {

    @Value("${info.app.name}")
    private String appName;

    @Value("${info.app.version}")
    private String appVersion;

    @GetMapping("/ambiente")
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

    @GetMapping("/app-name")
    public ResponseEntity<String> obtenerNombreApp() {
        return ResponseEntity.ok(appName);
    }


    @GetMapping("/app-version")
    public ResponseEntity<String> obtenerVersionApp() {
        return ResponseEntity.ok(appVersion);
    }
}