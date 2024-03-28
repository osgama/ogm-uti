package com.utilidades.servicio;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.stereotype.Service;
import com.utilidades.model.OperacionDto;
import java.util.List;
import java.util.Map;

@Service
public class OperacionesService {

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
                return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR)
                        .body(Map.of("mensaje", "Error al ejecutar consulta: " + e.getMessage()));
            }
        } else if ("actualizacion".equals(operacionDto.getTipoOperacion())) {
            String updateSql = (String) operacionDto.getParametros().get("updateSql");
            try {
                int filasAfectadas = jdbcTemplate.update(updateSql);
                return ResponseEntity.ok().body(Map.of("mensaje", "Operación de actualización ejecutada con éxito, filas afectadas: " + filasAfectadas));
            } catch (Exception e) {
                e.printStackTrace();
                return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(Map.of("mensaje", "Error al ejecutar actualización: " + e.getMessage()));
            }

        } else {
            return ResponseEntity.badRequest().body(Map.of("mensaje", "Tipo de operación desconocido"));
        }
    }
}
