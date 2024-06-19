package com.utilidades.servicio;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.stereotype.Service;
import com.utilidades.model.OperacionDto;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

@Service
public class ConsultasService {

    @Autowired
    private JdbcTemplate jdbcTemplate;

    public ResponseEntity<?> ejecutarOperacion(OperacionDto operacionDto) {
        if ("consulta".equals(operacionDto.getTipoOperacion())) {
            String consultaSql = (String) operacionDto.getParametros().get("consultaSql");
            try {
                List<Map<String, Object>> resultado = jdbcTemplate.queryForList(consultaSql);
                return ResponseEntity.ok().body(resultado);
            } catch (Exception e) {
                e.printStackTrace();
                Map<String, String> errorResponse = new HashMap<>();             
                errorResponse.put("mensaje", "Error al ejecutar consulta: " + e.getMessage());
                return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(errorResponse);
            }
        } else if ("actualizacion".equals(operacionDto.getTipoOperacion())) {
            String updateSql = (String) operacionDto.getParametros().get("updateSql");
            try {
                int filasAfectadas = jdbcTemplate.update(updateSql);
                Map<String, String> successResponse = new HashMap<>();
                successResponse.put("mensaje", "Operación de actualización ejecutada con éxito, filas afectadas: " + filasAfectadas);
                return ResponseEntity.ok().body(successResponse);
            } catch (Exception e) {
                e.printStackTrace();
                Map<String, String> errorResponse = new HashMap<>();
                errorResponse.put("mensaje", "Error al ejecutar actualización: " + e.getMessage());
                return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(errorResponse);
            }

        } else {
            Map<String, String> badRequestResponse = new HashMap<>();
            badRequestResponse.put("mensaje", "Tipo de operación desconocido");
            return ResponseEntity.badRequest().body(badRequestResponse);
        }
    }
}