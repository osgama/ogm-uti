package com.utilidades.api;

import org.slf4j.*;
import java.io.*;
import java.util.*;
import java.util.zip.*;
import org.springframework.http.*;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.servlet.mvc.method.annotation.StreamingResponseBody;

import com.utilidades.servicio.*;

@RestController
public class ApiDescargaArchivos {

    private static final Logger logger = LoggerFactory.getLogger(ApiDescargaArchivos.class);

    private final DetalleArchivos detalleArchivos;

    public ApiDescargaArchivos(DetalleArchivos detalleArchivos) {
        this.detalleArchivos = detalleArchivos;
    }

    @GetMapping("/api/descargar")
    public ResponseEntity<StreamingResponseBody> downloadFile(
            @RequestParam String directorio,
            @RequestParam String archivo,
            @RequestParam String tipo) {

        String envir = System.getenv("ENVIRONMENT");
        logger.info(": : : : INICIA DESCARGA DE ARCHIVO EN AMBIENTE: " + envir);

        String directorioFinal = "";
        String directoriotmp = "";

        if (tipo.equals("1")) {
            directoriotmp = detalleArchivos.getDirectorio(Integer.parseInt(directorio));
            directorioFinal = System.getenv("BASE_DIRECTORIO_LOGS" + directoriotmp);
            logger.info(": : : : Directorio " + directorioFinal);
        } else if (tipo.equals("2")) {
            directorioFinal = System.getenv("BASE_DIRECTORIO_LOGS" + directorio);
            logger.info(": : : : Directorio " + directorioFinal);
        } else {
            logger.error(": : : : Directorio no valido ");
        }

        File file = new File(directorio, archivo);
        if (file.exists()) {
            StreamingResponseBody stream = outputStream -> {
                try (InputStream inputStream = new FileInputStream(file)) {
                    byte[] buffer = new byte[1024];
                    int byteRead;
                    while ((byteRead = inputStream.read(buffer)) != -1) {
                        outputStream.write(buffer, 0, byteRead);
                    }
                } catch (IOException e) {
                    throw new UncheckedIOException(e);
                }
            };

            HttpHeaders headers = new HttpHeaders();
            headers.setContentType(MediaType.APPLICATION_OCTET_STREAM);
            headers.setContentDisposition(ContentDisposition.attachment().filename(file.getName()).build());
            logger.info(": : : : TERMINA DESCARGA DE ARCHIVO EN AMBIENTE: " + envir);
            return new ResponseEntity<>(stream, headers, HttpStatus.OK);

        } else {
            logger.error(": : : : ERROR EN DESCARGA DE ARCHIVO EN AMBIENTE " + envir);
            return ResponseEntity.notFound().build();
        }
    }

    @GetMapping("/api/descargar-zip")
    public ResponseEntity<StreamingResponseBody> downloadFilesAsZip(
            @RequestParam String directorio,
            @RequestParam("archivos") List<String> archivosSeleccionados,
            @RequestParam String tipo) {
        List<File> archivosParaComprimir = new ArrayList<>();

        String envir = System.getenv("ENVIRONMENT");
        System.out.println(": : : : INICIA CREACIÓN DE ZIP EN AMBIENTE: " + envir);
        String directorioFinal = "";
        String directoriotmp = "";
        String nombretmp = "";

        HttpHeaders headers = new HttpHeaders();
        headers.setContentType(MediaType.APPLICATION_OCTET_STREAM);

        if (tipo.equals("1")) {
            directoriotmp = detalleArchivos.getDirectorio(Integer.parseInt(directorio));
            directorioFinal = System.getenv("BASE_DIRECTORIO_LOGS" + directoriotmp);
            logger.info(": : : : Directorio " + directorioFinal);
        } else if (tipo.equals("2")) {
            directorioFinal = System.getenv("BASE_DIRECTORIO_LOGS" + directorio);
            logger.info(": : : : Directorio " + directorioFinal);
        } else {
            logger.error(": : : : Directorio no valido ");
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

        StreamingResponseBody stream = outputStream -> {
            try (ZipOutputStream zos = new ZipOutputStream(outputStream)) {
                int ArchivosComprimidos = 0;
                byte[] buffer = new byte[1024];
                for (File file : archivosParaComprimir) {
                    try (FileInputStream fis = new FileInputStream(file)) {
                        zos.putNextEntry(new ZipEntry(file.getName()));
                        int length;
                        while ((length = fis.read(buffer)) >= 0) {
                            zos.write(buffer, 0, length);
                        }
                        zos.closeEntry();
                        ArchivosComprimidos++;
                    }
                }
                zos.finish();
                logger.info(": : : : ArchivosComprimidos: " + ArchivosComprimidos);
            } catch (IOException e) {
                logger.error(": : : : 1 ERROR EN CREACIÓN DE ZIP EN AMBIENTE: ", envir);
                throw new UncheckedIOException(e);
            }
        };

        if (tipo.equals("1")) {
            Set<String> valoresPermitidos = new HashSet<>();
            for (int i = 7; i <= 21; i++) {
                valoresPermitidos.add(String.valueOf(i));
            }
            if (valoresPermitidos.contains(directorio)) {
                nombretmp = detalleArchivos.getNombreArchivo(Integer.parseInt(directorio));
                logger.info(": : : : Archivo Logs " + nombretmp);
                headers.setContentDisposition(
                        ContentDisposition.attachment().filename("Logs-" + nombretmp + "-" + envir + ".zip").build());
            } else {
                logger.info(": : : : Archivo Normal");
                headers.setContentDisposition(
                        ContentDisposition.attachment().filename("Archivos-" + envir + ".zip").build());
            }
        } else if (tipo.equals("2")) {
            headers.setContentDisposition(
                    ContentDisposition.attachment().filename("Archivos-" + envir + ".zip").build());
            logger.info(": : : : Nombre Archivo Generico");
        } else {
            logger.info(": : : : Nombre Archivo No Valido");
        }
        try {
            logger.info(": : : : TERMINA CREACIÓN DE ZIP EN AMBIENTE: " + envir);
            return new ResponseEntity<>(stream, headers, HttpStatus.OK);
        } catch (Exception e) {
            logger.info(": : : : 2 ERROR EN CREACIÓN DE ZIP EN AMBIENTE: " + envir);
            return new ResponseEntity<>(stream, headers, HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }
}