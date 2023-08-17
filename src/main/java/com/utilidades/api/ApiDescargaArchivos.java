package com.utilidades.api;

import java.io.*;
import java.util.*;
import java.util.zip.*;
import java.nio.file.*;
import org.springframework.http.*;
import org.springframework.web.bind.annotation.*;

@RestController
public class ApiDescargaArchivos {

    @GetMapping("/api/descargar")
    public ResponseEntity<byte[]> downloadFile(@RequestParam("directorio") String directorio, @RequestParam("archivo") String archivo) {

        String envir = System.getenv("ENVIRONMENT");
        System.out.println(": : : : INICIA DESCARGA DE ARCHIVO EN AMBIENTE: " + envir);

        File file = new File(directorio, archivo);
        if (file.exists()) {
            try {
                byte[] archivoBytes = Files.readAllBytes(file.toPath());
                HttpHeaders headers = new HttpHeaders();
                headers.setContentType(MediaType.APPLICATION_OCTET_STREAM);
                headers.setContentDisposition(ContentDisposition.attachment().filename(file.getName()).build());

                System.out.println(": : : : TERMINA DESCARGA DE ARCHIVO EN AMBIENTE: " + envir);
                return new ResponseEntity<>(archivoBytes, headers, HttpStatus.OK);
            } catch (IOException e) {
                System.out.println(": : : : ERROR EN DESCARGA DE ARCHIVO EN AMBIENTE: " + envir);
                return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).build();
            }
        } else {
            return ResponseEntity.notFound().build();
        }
    }

    @GetMapping("/api/descargar-zip")
    public ResponseEntity<byte[]> downloadFilesAsZip(@RequestParam("directorio") String directorio,@RequestParam("archivos") List<String> archivosSeleccionados) {
        List<File> archivosParaComprimir = new ArrayList<>();

        String envir = System.getenv("ENVIRONMENT");
        System.out.println(": : : : INICIA CREACIÓN DE ZIP EN AMBIENTE: " + envir);
        int ArchivosComprimidos = 0;

        if (envir.equals("dev")) {
            envir = "DEV";
        } else if (envir.equals("UAT")) {
            envir = "UAT";
        }

        for (String nombreArchivo : archivosSeleccionados) {
            File file = new File(directorio, nombreArchivo);
            if (file.exists()) {
                archivosParaComprimir.add(file);
            } else {
                return ResponseEntity.notFound().build();
            }
        }
        if (archivosParaComprimir.isEmpty()) {
            return ResponseEntity.notFound().build();
        }
        // Comprimir y descargar los archivos en un ZIP
        try {
            ByteArrayOutputStream baos = new ByteArrayOutputStream();
            ZipOutputStream zos = new ZipOutputStream(baos);

            for (File file : archivosParaComprimir) {
                FileInputStream fis = new FileInputStream(file);
                ZipEntry zipEntry = new ZipEntry(file.getName());
                zos.putNextEntry(zipEntry);

                byte[] buffer = new byte[1024];
                int length;
                while ((length = fis.read(buffer)) >= 0) {
                    zos.write(buffer, 0, length);
                }
                fis.close();
                zos.closeEntry();
                ArchivosComprimidos++;
            }

            zos.finish();
            byte[] zipBytes = baos.toByteArray();
            System.out.println(": : : : ArchivosComprimidos: " + ArchivosComprimidos);

            HttpHeaders headers = new HttpHeaders();
            headers.setContentType(MediaType.APPLICATION_OCTET_STREAM);
            headers.setContentDisposition(ContentDisposition.attachment().filename("Archivos.zip").build());
            System.out.println(": : : : TERMINA CREACIÓN DE ZIP EN AMBIENTE: " + envir);
            return new ResponseEntity<>(zipBytes, headers, HttpStatus.OK);
        } catch (IOException e) {
            System.out.println(": : : : ERROR EN CREACIÓN DE ZIP EN AMBIENTE: " + envir);
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).build();
        }
    }
}