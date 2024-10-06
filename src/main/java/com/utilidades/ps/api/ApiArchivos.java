package com.utilidades.ps.api;

import org.slf4j.*;
import java.io.*;
import java.util.*;
import java.nio.file.*;
import java.nio.channels.*;
import java.text.SimpleDateFormat;
import org.springframework.http.*;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.servlet.mvc.method.annotation.*;
import net.lingala.zip4j.ZipFile;
import net.lingala.zip4j.model.enums.*;
import net.lingala.zip4j.model.ZipParameters;
import com.utilidades.ps.model.*;
import com.utilidades.ps.servicio.*;

@RestController
@RequestMapping("/api")
public class ApiArchivos {

    private static final Logger logger = LoggerFactory.getLogger(ApiArchivos.class);
    private final DetalleArchivos detalleArchivos;

    public ApiArchivos(DetalleArchivos detalleArchivos) {
        this.detalleArchivos = detalleArchivos;
    }

    private static final int BUFFER_SIZE = 16384; // Aumentar el buffer a 16 KB

    @PostMapping("/lista")
    public ResponseEntity<List<String>> listFiles(@RequestBody ListaArchivosRequest request) {

        String envir = System.getenv("ENVIRONMENT");
        logger.info(": : : : INICIA BUSQUEDA DE ARCHIVO(S) EN AMBIENTE: {}", envir);

        String directorioFinal = obtenerDirectorioFinal(request.getDirectorio(), request.getTipo());
        if (directorioFinal == null) {
            logger.error(": : : : Directorio no válido");
            return ResponseEntity.badRequest().header("X-Error-Message", "El directorio especificado no es válido.")
                    .build();
        }

        File folder = new File(directorioFinal);
        if (!folder.exists() || !folder.isDirectory()) {
            logger.error(": : : : El directorio especificado no existe o no es válido: {}", directorioFinal);
            return ResponseEntity.badRequest()
                    .header("X-Error-Message", "El directorio especificado no existe o no es válido.").build();
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

    @GetMapping("/descargar")
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

    @PostMapping("/descargar-zip")
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
            nombreArchivo = nombreArchivo.replaceAll("[\"\\[\\]]", "").trim();
            File file = new File(directorioFinal, nombreArchivo);
        
            if (!file.exists()) {
                logger.error("El archivo no existe: {}", file.getAbsolutePath());
                return ResponseEntity.notFound().build();
            } else {
                logger.info("Archivo encontrado: {}", file.getAbsolutePath());
                archivosParaComprimir.add(file);
            }
        }
        
        if (archivosParaComprimir.isEmpty()) {
            return ResponseEntity.notFound().build();
        }

        StreamingResponseBody stream = outputStream -> {
            // Genera el nombre del archivo zip, verificando que getFilename() no sea nulo
            String nombreZip = Optional.ofNullable(generarNombreZip(directorio, tipo, envir).getFilename())
                                       .orElse("Archivo-Desconocido");

            // Crea el archivo temporal con el nombre generado (sin la extensión .zip)
            File tempZip = new File("/opt/middleware", nombreZip);

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
        return "%.2f %s".formatted(fileSize, units[unitIndex]);
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
