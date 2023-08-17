package com.utilidades.api;

import java.io.*;
import java.util.*;
import java.text.SimpleDateFormat;
import org.springframework.http.*;
import org.springframework.web.bind.annotation.*;

import com.utilidades.model.*;

@RestController
public class ApiListaDetalle {

    @PostMapping("/api/lista")
    public ResponseEntity<List<String>> listFiles(@RequestBody ListaArchivosRequest request) {

        String envir = System.getenv("ENVIRONMENT");
        System.out.println(": : : : INICIA BUSQUEDA DE ACHIVO(S) EN AMBIENTE: " + envir);

        String directorio = request.getDirectorio();
        String parametro = request.getParametro();

        int ArchivosEncontrados = 0;

        File folder = new File(directorio);
        if (!folder.exists() || !folder.isDirectory()) {
            HttpHeaders headersFolder = new HttpHeaders();
            headersFolder.add("X-Error-Message", "El directorio especificado no existe o no es valido.");
            System.out.println(": : : : ERROR EN headersFolder: " + headersFolder);
            return ResponseEntity.badRequest().headers(headersFolder).build();
        } else {
            File[] files = folder.listFiles();
            List<String> matchingFiles = new ArrayList<>();
            if (files != null) {
                for (File file : files) {
                    if (parametro.equals("*") || file.getName().contains(parametro)) {
                        String detalle = getFormattedFileDetails(file);
                        matchingFiles.add(detalle);
                        ArchivosEncontrados++;
                    }
                }
                System.out.println(": : : : ArchivosEncontrados: " + ArchivosEncontrados);
                System.out.println(": : : : TERMINA BUSQUEDA DE ACHIVO(S) EN AMBIENTE: " + envir);
                return new ResponseEntity<>(matchingFiles, HttpStatus.OK);
            }
            System.out.println(": : : : ERROR EN BUSQUEDA DE ACHIVO(S) EN AMBIENTE: " + envir);
            return new ResponseEntity<>(HttpStatus.OK);
        }
    }

    private String getFormattedFileDetails(File file) {
        String nombre = file.getName();
        String tamano = getFormattedFileSize(file.length());
        String fechaModificacion = new SimpleDateFormat("dd-MM-yyyy HH:mm:ss").format(new Date(file.lastModified()));
        return nombre + "||" + tamano + "||" + fechaModificacion;
    }

    private String getFormattedFileSize(long size) {
        String[] units = { "B", "KB", "MB", "GB", "TB" };
        int unitIndex = 0;
        double fileSize = size;

        while (fileSize >= 1024 && unitIndex < units.length - 1) {
            fileSize /= 1024;
            unitIndex++;
        }
        return String.format("%.2f%s", fileSize, units[unitIndex]);
    }
}