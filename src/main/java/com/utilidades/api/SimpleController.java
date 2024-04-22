package com.utilidades.api;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import com.utilidades.servicio.CryptoService;

@RestController
public class SimpleController {

    private final CryptoService cryptoService;

    public SimpleController(CryptoService cryptoService) {
        this.cryptoService = cryptoService;
    }

    @GetMapping("/prueba")
    public String saludo(@RequestParam String servidor, @RequestParam String usuario, @RequestParam String password) throws Exception {

         String pwd = cryptoService.decrypt(password);

        return "Clave desencriptada: " + pwd;
    }
}
