package com.utilidades.ps.api;

import org.slf4j.*;
import java.io.*;
import java.nio.channels.*;
import java.nio.file.*;
import java.util.*;
import org.springframework.http.*;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.servlet.mvc.method.annotation.StreamingResponseBody;

import com.utilidades.ps.servicio.*;
import net.lingala.zip4j.ZipFile;
import net.lingala.zip4j.model.ZipParameters;
import net.lingala.zip4j.model.enums.*;

@RestController
public class ApiDescargaArchivos {

    private static final Logger logger = LoggerFactory.getLogger(ApiDescargaArchivos.class);
    private static final int BUFFER_SIZE = 8192;

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

        String directorioFinal = obtenerDirectorioFinal(directorio, tipo);
        if (directorioFinal == null) {
            logger.error(": : : : Directorio no válido");
            return ResponseEntity.badRequest().body(null);
        }

        File file = new File(directorioFinal, archivo);
        if (file.exists()) {
            if ("PROD".equalsIgnoreCase(envir)) {
                // En PROD, todos los archivos deben estar en un ZIP protegido con contraseña
                return downloadFilesAsZip(directorio, Collections.singletonList(archivo), tipo, null);
            } else {
                // Modo normal para otros ambientes
                StreamingResponseBody stream = outputStream -> {
                    try (FileChannel fileChannel = FileChannel.open(file.toPath(), StandardOpenOption.READ)) {
                        fileChannel.transferTo(0, fileChannel.size(), Channels.newChannel(outputStream));
                    } catch (IOException e) {
                        logger.error("Error al transmitir el archivo", e);
                        throw new UncheckedIOException(e);
                    }
                };

                HttpHeaders headers = new HttpHeaders();
                headers.setContentType(MediaType.APPLICATION_OCTET_STREAM);
                headers.setContentDisposition(ContentDisposition.attachment().filename(file.getName()).build());
                logger.info(": : : : TERMINA DESCARGA DE ARCHIVO EN AMBIENTE: " + envir);
                return new ResponseEntity<>(stream, headers, HttpStatus.OK);
            }
        } else {
            logger.error(": : : : ERROR EN DESCARGA DE ARCHIVO EN AMBIENTE " + envir);
            return ResponseEntity.notFound().build();
        }
    }

    @PostMapping("/api/descargar-zip")
    public ResponseEntity<StreamingResponseBody> downloadFilesAsZip(
            @RequestParam String directorio,
            @RequestParam("archivos") List<String> archivosSeleccionados,
            @RequestParam(required = false) String password,
            @RequestParam String tipo) {

        String envir = System.getenv("ENVIRONMENT");
        logger.info(": : : : INICIA CREACIÓN DE ZIP EN AMBIENTE: " + envir);

        String directorioFinal = obtenerDirectorioFinal(directorio, tipo);
        if (directorioFinal == null) {
            logger.error(": : : : Directorio no válido");
            return ResponseEntity.badRequest().body(null);
        }

        List<File> archivosParaComprimir = new ArrayList<>();
        for (String nombreArchivo : archivosSeleccionados) {
            File file = new File(directorioFinal, nombreArchivo);
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
            File tempZip = File.createTempFile("archivos", ".zip");
            try (ZipFile zipFile = new ZipFile(tempZip)) {
                ZipParameters parameters = new ZipParameters();
                parameters.setCompressionMethod(CompressionMethod.DEFLATE);
                parameters.setCompressionLevel(CompressionLevel.NORMAL);

                if ("PROD".equalsIgnoreCase(envir) && password != null) {
                    // Si es PROD, añadimos la contraseña
                    parameters.setEncryptFiles(true);
                    parameters.setEncryptionMethod(EncryptionMethod.AES);
                    parameters.setAesKeyStrength(AesKeyStrength.KEY_STRENGTH_256);
                    zipFile.setPassword(password.toCharArray());
                }

                for (File file : archivosParaComprimir) {
                    zipFile.addFile(file, parameters);
                }
                // Enviar el ZIP resultante como StreamingResponseBody
                logger.info(": : : : Archivos Comprimidos y enviados.");

                try (InputStream is = new FileInputStream(tempZip)) {
                    byte[] buffer = new byte[BUFFER_SIZE];
                    int bytesRead;
                    while ((bytesRead = is.read(buffer)) != -1) {
                        outputStream.write(buffer, 0, bytesRead);
                    }
                }
            } catch (IOException e) {
                logger.error(": : : : ERROR EN CREACIÓN DE ZIP EN AMBIENTE: " + envir, e);
                throw new UncheckedIOException(e);
            } finally {
                if (tempZip.exists()) {
                    tempZip.delete();
                }
            }
        };

        HttpHeaders headers = new HttpHeaders();
        headers.setContentType(MediaType.APPLICATION_OCTET_STREAM);
        headers.setContentDisposition(generarNombreZip(directorio, tipo, envir));

        logger.info(": : : : TERMINA CREACIÓN DE ZIP EN AMBIENTE: " + envir);
        return new ResponseEntity<>(stream, headers, HttpStatus.OK);
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

    private ContentDisposition generarNombreZip(String directorio, String tipo, String envir) {
        if ("1".equals(tipo)) {
            Set<String> valoresPermitidos = new HashSet<>();
            for (int i = 7; i <= 21; i++) {
                valoresPermitidos.add(String.valueOf(i));
            }
            if (valoresPermitidos.contains(directorio)) {
                String nombretmp = detalleArchivos.getNombreArchivo(Integer.parseInt(directorio));
                return ContentDisposition.attachment().filename("Logs-" + nombretmp + "-" + envir + ".zip").build();
            } else {
                return ContentDisposition.attachment().filename("Archivos-" + envir + ".zip").build();
            }
        } else if ("2".equals(tipo)) {
            return ContentDisposition.attachment().filename("Archivos-" + envir + ".zip").build();
        } else {
            logger.info(": : : : Nombre Archivo No Valido");
            return ContentDisposition.attachment().filename("Archivo-Desconocido.zip").build();
        }
    }
}
