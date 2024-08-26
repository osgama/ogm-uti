package com.utilidades.ps.api;

import java.io.*;
import java.util.*;
import org.slf4j.*;
import java.text.SimpleDateFormat;
import org.springframework.http.*;
import org.springframework.web.bind.annotation.*;
import com.utilidades.ps.model.*;
import com.utilidades.ps.servicio.*;

@RestController
public class ApiListaDetalle {

    private static final Logger logger = LoggerFactory.getLogger(ApiListaDetalle.class);

    private final DetalleArchivos detalleArchivos;

    public ApiListaDetalle(DetalleArchivos detalleArchivos) {
        this.detalleArchivos = detalleArchivos;
    }

    @PostMapping("/api/lista")
    public ResponseEntity<List<String>> listFiles(@RequestBody ListaArchivosRequest request) {

        String envir = System.getenv("ENVIRONMENT");
        logger.info(": : : : INICIA BUSQUEDA DE ARCHIVO(S) EN AMBIENTE: {}", envir);

        String directorioFinal = obtenerDirectorioFinal(request.getDirectorio(), request.getTipo());
        if (directorioFinal == null) {
            logger.error(": : : : Directorio no válido");
            return ResponseEntity.badRequest().header("X-Error-Message", "El directorio especificado no es válido.").build();
        }

        File folder = new File(directorioFinal);
        if (!folder.exists() || !folder.isDirectory()) {
            logger.error(": : : : El directorio especificado no existe o no es válido: {}", directorioFinal);
            return ResponseEntity.badRequest().header("X-Error-Message", "El directorio especificado no existe o no es válido.").build();
        }

        File[] files = folder.listFiles();
        if (files == null || files.length == 0) {
            logger.info(": : : : No se encontraron archivos en el directorio: {}", directorioFinal);
            return ResponseEntity.ok(Collections.emptyList());
        }

        Arrays.sort(files, Comparator.comparingLong(File::lastModified).reversed());
        List<String> matchingFiles = new ArrayList<>();
        String filtro = request.getFiltro().toLowerCase();
        int archivosEncontrados = 0;

        for (File file : files) {
            if (file.isFile() && (filtro.equals("*") || file.getName().toLowerCase().contains(filtro))) {
                matchingFiles.add(getFormattedFileDetails(file));
                archivosEncontrados++;
            }
        }

        logger.info(": : : : Archivos encontrados: {}", archivosEncontrados);
        logger.info(": : : : Filtro: {}", filtro);
        logger.info(": : : : TERMINA BUSQUEDA DE ARCHIVO(S) EN AMBIENTE: {}", envir);
        return ResponseEntity.ok(matchingFiles);
    }

    private String obtenerDirectorioFinal(String directorio, String tipo) {
        if ("1".equals(tipo)) {
            String directoriotmp = detalleArchivos.getDirectorio(Integer.parseInt(directorio));
            return System.getenv("BASE_DIRECTORIO_LOGS") + directoriotmp;
        } else if ("2".equals(tipo)) {
            return System.getenv("BASE_DIRECTORIO_LOGS") + directorio;
        }
        return null;
    }

    private String getFormattedFileDetails(File file) {
        String nombre = file.getName();
        String tamano = getFormattedFileSize(file.length());
        String fechaModificacion = new SimpleDateFormat("dd/MM/yyyy - HH:mm:ss").format(new Date(file.lastModified()));
        return String.join("||", nombre, tamano, fechaModificacion);
    }

    private String getFormattedFileSize(long size) {
        String[] units = { "B", "KB", "MB", "GB", "TB" };
        int unitIndex = 0;
        double fileSize = size;

        while (fileSize >= 1024 && unitIndex < units.length - 1) {
            fileSize /= 1024;
            unitIndex++;
        }
        return String.format("%.2f %s", fileSize, units[unitIndex]);
    }
}
