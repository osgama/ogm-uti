package com.utilidades.ps.model;

import java.util.Map;

public class OperacionDto {
    
    private String tipoOperacion;
    private Map<String, Object> parametros;

    public String getTipoOperacion() {
        return tipoOperacion;
    }

    public void setTipoOperacion(String tipoOperacion) {
        this.tipoOperacion = tipoOperacion;
    }

    public Map<String, Object> getParametros() {
        return parametros;
    }

    public void setParametros(Map<String, Object> parametros) {
        this.parametros = parametros;
    }
}