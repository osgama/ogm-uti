package com.utilidades.model;

public class ListaArchivosRequest {

    private String directorio;
    private String parametro;

    public String getDirectorio() {
        return directorio;
    }
    public void setDirectorio(String directorio) {
        this.directorio = directorio;
    }
    public String getParametro() {
        return parametro;
    }
    public void setParametro(String parametro) {
        this.parametro = parametro;
    }
}