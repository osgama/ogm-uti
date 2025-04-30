package com.utilidades.ps.api;

import org.slf4j.*;
import java.io.*;
import java.util.*;
import java.util.zip.*; // Para compresión sin encriptación
import java.text.SimpleDateFormat;
import org.springframework.http.*;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;
import org.springframework.web.servlet.mvc.method.annotation.StreamingResponseBody;
import net.lingala.zip4j.ZipFile; // Para compresión con encriptación
import net.lingala.zip4j.model.ZipParameters;
import net.lingala.zip4j.model.enums.*;
import com.utilidades.ps.model.*;
import com.utilidades.ps.servicio.*;

@RestController
@RequestMapping("/api")
public class ApiArchivos {

    private static final Logger logger = LoggerFactory.getLogger(ApiArchivos.class);
    private final DetalleArchivos detalleArchivos;
    private static final int BUFFER_SIZE = 16384; // Buffer para la transmisión de archivos

    public ApiArchivos(DetalleArchivos detalleArchivos) {
        this.detalleArchivos = detalleArchivos;
    }

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
    
        String directorioFinal = "";
        String directoriotmp = "";
    
        String baseDirectorio = System.getenv("BASE_DIRECTORIO_LOGS");

        if (tipo.equals("1")) {
            directoriotmp = detalleArchivos.getDirectorio(Integer.parseInt(directorio));
            logger.info(": : : : directoriotmp " + directoriotmp);
            logger.info("BASE DIRECTORIO: " + baseDirectorio);
        
            if (baseDirectorio != null && directoriotmp != null) {
                directorioFinal = baseDirectorio + directoriotmp;
                logger.info(": : : : Directorio Final construido: " + directorioFinal);
            } else {
                logger.error("Base del directorio o directoriotmp es null");
                return ResponseEntity.badRequest().build();
            }
        } else if (tipo.equals("2")) {
            logger.info("Directorio recibido: " + directorio);
            if (baseDirectorio != null && directorio != null) {
                directorioFinal = baseDirectorio + directorio;
                logger.info(": : : : Directorio Final construido: " + directorioFinal);
            } else {
                logger.error("Base del directorio o directorio es null");
                return ResponseEntity.badRequest().build();
            }
        } else {
            logger.error(": : : : Directorio no válido");
            return ResponseEntity.badRequest().build();
        }
        
    
        File file = new File(directorioFinal, archivo);
        if (!file.exists() || !file.isFile()) {
            logger.error(": : : : ERROR EN DESCARGA DE ARCHIVO");
            logger.error("Archivo no válido para descarga directa");
            return ResponseEntity.notFound().build();
        }
    
        long fileSize = file.length();
        final long ONE_HUNDRED_MB = 104_857_600L;
    
        if (fileSize > ONE_HUNDRED_MB) {
            logger.info(": : : : Archivo grande detectado, aplicando compresión.");
    
            List<File> archivosParaComprimir = List.of(file);
            String nombreZip = archivo + ".zip";
    
            return comprimirYTransmitirZip(archivosParaComprimir, null, null, nombreZip);
        } else {
            logger.info(": : : : Archivo pequeño detectado, transmisión directa.");
            return descargarArchivoDirecto(file, null, archivo);
        }
    }
    
    

    @PostMapping("/descargar-zip")
    public ResponseEntity<StreamingResponseBody> downloadFilesAsZip(
            @RequestParam String directorio,
            @RequestParam("archivos") List<String> archivosSeleccionados,
            @RequestParam(required = false) String password,
            @RequestParam String tipo) {

        String envir = System.getenv("ENVIRONMENT");
        logger.info(": : : : INICIA PROCESO DE DESCARGA EN AMBIENTE: " + envir);

        String directorioFinal = obtenerDirectorioFinal(directorio, tipo);
        if (directorioFinal == null) {
            logger.error(": : : : Directorio no válido");
            return ResponseEntity.badRequest().body(null);
        }

        List<File> archivosParaDescargar = new ArrayList<>();
        long totalSize = 0;

        if (archivosSeleccionados == null || archivosSeleccionados.isEmpty()) {
            logger.warn("No se proporcionaron archivos para comprimir");
            return ResponseEntity.badRequest().body(null);
        }

        for (String nombreArchivo : archivosSeleccionados) {
            nombreArchivo = nombreArchivo.replaceAll("[\"\\[\\]]", "").trim();
            File file = new File(directorioFinal, nombreArchivo);

            if (!file.exists()) {
                logger.error("El archivo no existe: {}", file.getAbsolutePath());
                return ResponseEntity.notFound().build();
            } else {
                archivosParaDescargar.add(file);
                totalSize += file.length(); // Acumular el tamaño total de los archivos seleccionados
            }
        }

        // Genera el nombre del ZIP utilizando el método generarNombreZip
        ContentDisposition nombreZip = generarNombreZip(directorio, tipo, envir);

        // Verifica si el tamaño total excede 100 MB (100 MB = 104,857,600 bytes)
        final long ONE_HUNDRED_MB = 104_857_600L;

        // Si el tamaño total es mayor a 100 MB, comprimimos a ZIP
        if (totalSize > ONE_HUNDRED_MB) {
            logger.info("El tamaño total de los archivos es mayor a 100 MB, se comprimirá en un ZIP.");
            return comprimirYTransmitirZip(archivosParaDescargar, password, envir, nombreZip.getFilename());
        } else {
            logger.info("El tamaño total es menor a 100 MB, se enviarán los archivos comprimidos en un ZIP.");
            return comprimirYTransmitirZip(archivosParaDescargar, password, envir, nombreZip.getFilename());
        }        
    }

    // Método para comprimir y transmitir el archivo ZIP
    private ResponseEntity<StreamingResponseBody> comprimirYTransmitirZip(List<File> archivosParaComprimir,
            String password, String envir, String nombreZip) {
        // Si el ambiente es PROD y tenemos una contraseña, utilizamos Zip4j para
        // encriptar el ZIP
        if ("PROD".equalsIgnoreCase(envir) && password != null) {
            return comprimirConEncriptacion(archivosParaComprimir, password, envir, nombreZip);
        }

        // Para otros entornos, usamos `ZipOutputStream` para compresión sin
        // encriptación
        StreamingResponseBody stream = outputStream -> {
            try (ZipOutputStream zipOut = new ZipOutputStream(outputStream)) {
                for (File file : archivosParaComprimir) {
                    try (FileInputStream fis = new FileInputStream(file)) {
                        ZipEntry zipEntry = new ZipEntry(file.getName());
                        zipOut.putNextEntry(zipEntry);

                        byte[] buffer = new byte[BUFFER_SIZE]; // Buffer para transmitir en chunks
                        int length;
                        while ((length = fis.read(buffer)) >= 0) {
                            zipOut.write(buffer, 0, length); // Enviar chunks al cliente
                            outputStream.flush(); // Forzar el envío inmediato de cada chunk
                        }
                        zipOut.closeEntry();
                    }
                }
                zipOut.finish(); // Termina la compresión
            } catch (IOException e) {
                logger.error(": : : : ERROR AL COMPRIMIR Y TRANSMITIR ARCHIVO GRANDE", e);
                throw new ResponseStatusException(HttpStatus.INTERNAL_SERVER_ERROR, "Fallo al generar ZIP", e);
            }
        };

        HttpHeaders headers = new HttpHeaders();
        headers.setContentType(MediaType.APPLICATION_OCTET_STREAM);
        headers.setContentDisposition(ContentDisposition.attachment().filename(nombreZip).build());

        return new ResponseEntity<>(stream, headers, HttpStatus.OK);

    }

    // Método para comprimir archivos con encriptación usando Zip4j
    private ResponseEntity<StreamingResponseBody> comprimirConEncriptacion(List<File> archivosParaComprimir,
            String password, String envir, String nombreZip) {
        StreamingResponseBody stream = outputStream -> {
            File tempZip = File.createTempFile("archivos_encriptados", ".zip");
            try (ZipFile zipFile = new ZipFile(tempZip)) {
                ZipParameters parameters = new ZipParameters();
                parameters.setCompressionMethod(CompressionMethod.DEFLATE);
                parameters.setEncryptFiles(true); // Encriptar archivos
                parameters.setEncryptionMethod(EncryptionMethod.AES);
                parameters.setAesKeyStrength(AesKeyStrength.KEY_STRENGTH_256);
                zipFile.setPassword(password.toCharArray());

                for (File file : archivosParaComprimir) {
                    zipFile.addFile(file, parameters);
                }

                // Transmitir el archivo ZIP encriptado en chunks
                try (InputStream is = new FileInputStream(tempZip)) {
                    byte[] buffer = new byte[BUFFER_SIZE]; // Buffer de chunks
                    int bytesRead;
                    while ((bytesRead = is.read(buffer)) != -1) {
                        outputStream.write(buffer, 0, bytesRead); // Enviar chunks al cliente
                        outputStream.flush(); // Forzar el envío inmediato de cada chunk
                    }
                }
            } catch (IOException e) {
                logger.error(": : : : ERROR AL COMPRIMIR Y TRANSMITIR ZIP ENCRIPTADO", e);
                throw new UncheckedIOException(e);
            } finally {
                if (tempZip.exists()) {
                    tempZip.delete(); // Eliminar archivo temporal después de la transmisión
                }
            }
        };

        HttpHeaders headers = new HttpHeaders();
        headers.setContentType(MediaType.APPLICATION_OCTET_STREAM);
        headers.setContentDisposition(ContentDisposition.attachment().filename(nombreZip).build());

        return new ResponseEntity<>(stream, headers, HttpStatus.OK);
    }

    // Método para descargar un archivo individual directamente
    private ResponseEntity<StreamingResponseBody> descargarArchivoDirecto(File file, String envir,
            String nombreArchivo) {
        StreamingResponseBody stream = outputStream -> {
            try (FileInputStream fis = new FileInputStream(file)) {
                byte[] buffer = new byte[BUFFER_SIZE];
                int bytesRead;
                while ((bytesRead = fis.read(buffer)) != -1) {
                    outputStream.write(buffer, 0, bytesRead); // Se transmite directamente al cliente
                }
            } catch (IOException e) {
                logger.error(": : : : ERROR AL DESCARGAR ARCHIVO DIRECTO", e);
                throw new UncheckedIOException(e);
            }
        };

        HttpHeaders headers = new HttpHeaders();
        headers.setContentType(MediaType.APPLICATION_OCTET_STREAM);
        headers.setContentDisposition(ContentDisposition.attachment().filename(nombreArchivo).build());

        logger.info(": : : : SE ENVÍA EL ARCHIVO INDIVIDUAL DIRECTAMENTE");
        return new ResponseEntity<>(stream, headers, HttpStatus.OK);
    }

    private String obtenerDirectorioFinal(String directorio, String tipo) {
        String base = System.getenv("BASE_DIRECTORIO_LOGS");

        if (base == null || base.isBlank()) {
            logger.error("Variable de entorno BASE_DIRECTORIO_LOGS no está definida");
            return null;
        }

        if ("1".equals(tipo)) {
            String directoriotmp = detalleArchivos.getDirectorio(Integer.parseInt(directorio));
            if (directoriotmp == null || directoriotmp.isBlank()) {
                logger.error("El método detalleArchivos.getDirectorio() devolvió null o vacío para id: {}", directorio);
                return null;
            }
            return base + directoriotmp;

        } else if ("2".equals(tipo)) {
            if (directorio == null || directorio.isBlank()) {
                logger.error("El parámetro directorio es null o vacío para tipo 2");
                return null;
            }
            return base + directorio;

        } else {
            logger.error("Tipo de directorio no válido: {}", tipo);
            return null;
        }
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

    private ContentDisposition generarNombreZip(String directorio, String tipo, String envir) {
        if ("1".equals(tipo)) {
            Set<String> valoresPermitidos = new HashSet<>();
            int inicio = "prod".equalsIgnoreCase(envir) ? 7 : 1;
            for (int i = inicio; i <= 23; i++) {
                valoresPermitidos.add(String.valueOf(i));
            }
            if (valoresPermitidos.contains(directorio)) {
                String nombretmp = detalleArchivos.getNombreArchivo(Integer.parseInt(directorio));
                if (nombretmp == null || nombretmp.isBlank()) {
                    logger.warn("El nombre obtenido para el directorio {} es null o vacío", directorio);
                    return ContentDisposition.attachment().filename("Archivos-" + envir + ".zip").build();
                }
                nombretmp = nombretmp.replaceAll("[\\\\/:*?\"<>|]", "_");

                return ContentDisposition.attachment().filename("Logs-" + nombretmp + "-" + envir + ".zip").build();
            } else {
                return ContentDisposition.attachment().filename("Archivos-" + envir + ".zip").build();
            }
        } else if ("2".equals(tipo)) {
            return ContentDisposition.attachment().filename("Archivos-" + envir + ".zip").build();
        } else {
            logger.warn(": : : : Tipo no válido para generar nombre de archivo ZIP: {}", tipo);
            return ContentDisposition.attachment().filename("Archivo-Desconocido.zip").build();
        }
    }
}
