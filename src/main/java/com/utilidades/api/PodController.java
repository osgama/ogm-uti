package com.utilidades.api;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import com.utilidades.servicio.PodService;

import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.Parameter;
import io.swagger.v3.oas.annotations.media.Content;
import io.swagger.v3.oas.annotations.media.Schema;
import io.swagger.v3.oas.annotations.responses.ApiResponse;
import io.swagger.v3.oas.annotations.tags.Tag;

@Tag(name = "Pod Management", description = "Operaciones para gestionar Pods")
@RestController
public class PodController {

    private final PodService podService;

    public PodController(PodService podService) {
        this.podService = podService;
    }

    @GetMapping("/StopPods")
    @Operation(summary = "Detiene una serie de pods",
    description = "Detiene los pods especificados según los parámetros proporcionados.",
    responses = {
        @ApiResponse(responseCode = "200",description = "Deteniendo pods, en progreso...", 
                     content = @Content(schema = @Schema(implementation = String.class)))
    })
    public String scaleDownPods(@Parameter(description = "Token de autenticación") @RequestParam String token,
            @Parameter(description = "Nombre del servidor") @RequestParam String servidor,
            @Parameter(description = "Opción de selección de pods") @RequestParam String opcion) {
        try {
            podService.scaleDownPods(token, servidor, opcion);
        } catch (Exception e) {
            e.printStackTrace();
        }
        return "Deteniendo pods, en progreso...";
    }

    @GetMapping("/StartPods")
    @Operation(summary = "Inicia una serie de pods",
    description = "Inicia los pods especificados según los parámetros proporcionados.",
    responses = {
        @ApiResponse(responseCode = "200",description = "Inicia pods, en progreso...", 
                     content = @Content(schema = @Schema(implementation = String.class)))
    })
    public String scaleUpPodsInBlocks(@Parameter(description = "Token de autenticación") @RequestParam String token,
            @Parameter(description = "Nombre del servidor") @RequestParam String servidor,
            @Parameter(description = "Opción de selección de pods") @RequestParam String opcion) {

        try {
            podService.scaleUpPodsInBlocks(token, servidor, opcion);
        } catch (Exception e) {
            e.printStackTrace();
        }
        return "Iniciando pods, en progreso...";
    }
}
