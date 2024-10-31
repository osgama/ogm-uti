#!/bin/bash

# Archivo con la estructura de directorios
archivo_estructura="estructura_directorios.txt"

# Directorio raíz donde se replicará la estructura
directorio_base="/ruta/destino"

# Validar si el archivo de estructura existe
if [[ ! -f "$archivo_estructura" ]]; then
    echo "El archivo $archivo_estructura no existe."
    exit 1
fi

# Leer cada línea (ruta de directorio) del archivo de estructura
while IFS= read -r linea; do
    # Generar la ruta en el destino concatenando con el directorio_base
    ruta_destino="$directorio_base${linea#/ruta/al}"  # Eliminar parte de la ruta original

    # Verificar si el directorio existe en el destino
    if [[ ! -d "$ruta_destino" ]]; then
        echo "Creando directorio: $ruta_destino"
        mkdir -p "$ruta_destino"
    else
        echo "El directorio ya existe: $ruta_destino"
    fi
done < "$archivo_estructura"

echo "Estructura de directorios replicada exitosamente."
