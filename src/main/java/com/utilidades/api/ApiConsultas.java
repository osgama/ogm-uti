package com.utilidades.api;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import com.utilidades.model.OperacionDto;
import com.utilidades.servicio.*;

@RestController
@RequestMapping("/api")
public class ApiConsultas {

 @Autowired
    private ConsultasService consultasService;

    @PostMapping("/ejecutar")
    public ResponseEntity<?> ejecutarOperacion(@RequestBody OperacionDto operacionDto) {
        Object resultado = consultasService.ejecutarOperacion(operacionDto);
        return ResponseEntity.ok().body(resultado);
    }
}