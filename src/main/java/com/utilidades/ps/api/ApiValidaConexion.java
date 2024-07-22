package com.utilidades.ps.api;

import com.utilidades.ps.model.*;
import com.utilidades.ps.servicio.*;
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