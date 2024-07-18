package com.utilidades.api;

import com.utilidades.model.*;
import com.utilidades.servicio.*;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api/conexion")
public class ApiValidaConexion {

    @Autowired
    private DatabaseValidationService validationService;

    @GetMapping("/valida")
    public ConnectionResponse validateConnection(@RequestParam int option) {
        return validationService.validateConnection(option);
    }
}