package com.utilidades.model;

public class ListaArchivosRequest {

    private String directorio;
    private String filtro;
    private String tipo;

    public String getDirectorio() {
        return directorio;
    }
    public void setDirectorio(String directorio) {
        this.directorio = directorio;
    }

    public String getFiltro() {
        return filtro;
    }
    public void setFiltro(String filtro) {
        this.filtro = filtro;
    }
    
    public String getTipo() {
        return tipo;
    }
    public void setTipo(String tipo) {
        this.tipo = tipo;
    }
}