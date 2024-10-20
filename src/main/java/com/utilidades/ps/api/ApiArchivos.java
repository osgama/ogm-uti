package com.utilidades.ps.api;

import org.slf4j.*;
import java.io.*;
import java.util.*;
import java.util.zip.*; // Para compresión sin encriptación
import java.text.SimpleDateFormat;
import org.springframework.http.*;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.servlet.mvc.method.annotation.StreamingResponseBody;
import net.lingala.zip4j.ZipFile;  // Para compresión con encriptación
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
    
        // Verifica si el tamaño total excede 1 GB (1 GB = 1_073_741_824 bytes)
        final long ONE_GB = 1_073_741_824L;
    
        // Si el tamaño total es mayor a 1 GB, comprimimos a ZIP
        if (totalSize > ONE_GB) {
            logger.info("El tamaño total de los archivos es mayor a 1 GB, se comprimirá en un ZIP.");
            return comprimirYTransmitirZip(archivosParaDescargar, password, envir, nombreZip.getFilename());
        } else {
            // Si el tamaño es menor, se transmite directamente
            if (archivosParaDescargar.size() == 1) {
                return descargarArchivoDirecto(archivosParaDescargar.get(0), envir, nombreZip.getFilename());
            } else {
                logger.info("El tamaño total es menor a 1 GB, se enviarán los archivos individualmente.");
                return comprimirYTransmitirZip(archivosParaDescargar, password, envir, nombreZip.getFilename());
            }
        }
    }
    
    // Método para comprimir y transmitir el archivo ZIP
    private ResponseEntity<StreamingResponseBody> comprimirYTransmitirZip(List<File> archivosParaComprimir, String password, String envir, String nombreZip) {
        // Si el ambiente es PROD y tenemos una contraseña, utilizamos Zip4j para encriptar el ZIP
        if ("PROD".equalsIgnoreCase(envir) && password != null) {
            return comprimirConEncriptacion(archivosParaComprimir, password, envir, nombreZip);
        }
    
        // Para otros entornos, usamos `ZipOutputStream` para compresión sin encriptación
        StreamingResponseBody stream = outputStream -> {
            try (ZipOutputStream zipOut = new ZipOutputStream(outputStream)) {
                for (File file : archivosParaComprimir) {
                    try (FileInputStream fis = new FileInputStream(file)) {
                        ZipEntry zipEntry = new ZipEntry(file.getName());
                        zipOut.putNextEntry(zipEntry);
    
                        byte[] bytes = new byte[BUFFER_SIZE];
                        int length;
                        while ((length = fis.read(bytes)) >= 0) {
                            zipOut.write(bytes, 0, length);
                        }
                        zipOut.closeEntry();
                    }
                }
                zipOut.finish();
            } catch (IOException e) {
                logger.error(": : : : ERROR AL COMPRIMIR LOS ARCHIVOS EN EL ZIP", e);
                throw new UncheckedIOException(e);
            }
        };
    
        HttpHeaders headers = new HttpHeaders();
        headers.setContentType(MediaType.APPLICATION_OCTET_STREAM);
        headers.setContentDisposition(ContentDisposition.attachment().filename(nombreZip).build());
    
        logger.info(": : : : SE TERMINA DE COMPRIMIR Y ENVIAR LOS ARCHIVOS EN EL ZIP");
        return new ResponseEntity<>(stream, headers, HttpStatus.OK);
    }
    
    // Método para comprimir archivos con encriptación usando Zip4j
    private ResponseEntity<StreamingResponseBody> comprimirConEncriptacion(List<File> archivosParaComprimir, String password, String envir, String nombreZip) {
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
    
                // Transmitir el ZIP al cliente
                try (InputStream is = new FileInputStream(tempZip)) {
                    byte[] buffer = new byte[BUFFER_SIZE];
                    int bytesRead;
                    while ((bytesRead = is.read(buffer)) != -1) {
                        outputStream.write(buffer, 0, bytesRead); // Transmitir el archivo ZIP encriptado
                    }
                }
            } catch (IOException e) {
                logger.error(": : : : ERROR AL COMPRIMIR Y ENVIAR ZIP ENCRIPTADO", e);
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
    
        logger.info(": : : : SE TERMINA DE COMPRIMIR Y ENVIAR LOS ARCHIVOS ENCRIPTADOS");
        return new ResponseEntity<>(stream, headers, HttpStatus.OK);
    }
    
    // Método para descargar un archivo individual directamente
    private ResponseEntity<StreamingResponseBody> descargarArchivoDirecto(File file, String envir, String nombreArchivo) {
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
