package com.utilidades.api;

import java.io.*;
import java.util.*;
import org.slf4j.*;
import java.text.SimpleDateFormat;
import org.springframework.http.*;
import org.springframework.web.bind.annotation.*;
import com.utilidades.model.*;
import com.utilidades.servicio.*;

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
        System.out.println(": : : : INICIA BUSQUEDA DE ACHIVO(S) EN AMBIENTE: " + envir);
        String directorio = request.getDirectorio();
        String filtro = request.getFiltro();
        String tipo = request.getTipo();
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

        int ArchivosEncontrados = 0;

        File folder = new File(directorio);
        if (!folder.exists() || !folder.isDirectory()) {
            HttpHeaders headersFolder = new HttpHeaders();
            headersFolder.add("X-Error-Message", "El directorio especificado no existe o no es valido.");
            logger.info(": : : : ERROR EN headersFolder: " + headersFolder);
            return ResponseEntity.badRequest().headers(headersFolder).build();
        } else {
            File[] files = folder.listFiles();
            Arrays.sort(files, Comparator.comparing(File::lastModified).reversed());
            List<String> matchingFiles = new ArrayList<>();
            if (files != null) {
                for (File file : files) {
                    if (file.isFile()
                            && (filtro.equals("*")
                            || file.getName().toLowerCase().contains(filtro.toLowerCase()))) {
                        String detalle = getFormattedFileDetails(file);
                        matchingFiles.add(detalle);
                        ArchivosEncontrados++;
                    }
                }
                logger.info(": : : : ArchivosEncontrados: " + ArchivosEncontrados);
                logger.info(": : : : Filtro: " + filtro);
                logger.info(": : : : TERMINA BUSQUEDA DE ACHIVO(S) EN AMBIENTE: " +envir);
                return new ResponseEntity<>(matchingFiles, HttpStatus.OK);
            }
            logger.error(": : : : ERROR EN BUSQUEDA DE ACHIVO(S) EN AMBIENTE: " + envir);
            return new ResponseEntity<>(HttpStatus.OK);
        }
    }

    private String getFormattedFileDetails(File file) {
        String nombre = file.getName();
        String tamano = getFormattedFileSize(file.length());
        String fechaModificacion = new SimpleDateFormat("dd/MM/yyyy - HH:mm:ss").format(new Date(file.lastModified()));
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
        return String.format("%.2f %s", fileSize, units[unitIndex]);
    }
}