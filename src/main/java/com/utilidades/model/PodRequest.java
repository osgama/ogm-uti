package com.utilidades.model;

public class PodRequest {
    
    private String usuario;
    private String password;
    private String servidor;
    private String opcion;

    public String getUsuario() {
        return usuario;
    }
    public void setUsuario(String usuario) {
        this.usuario = usuario;
    }
    public String getPassword() {
        return password;
    }
    public void setPassword(String password) {
        this.password = password;
    }
    public String getServidor() {
        return servidor;
    }
    public void setServidor(String servidor) {
        this.servidor = servidor;
    }
    public String getOpcion() {
        return opcion;
    }
    public void setOpcion(String opcion) {
        this.opcion = opcion;
    }
}