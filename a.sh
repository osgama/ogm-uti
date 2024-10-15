#!/bin/bash

# Variables
BASE_DIR="/opt/middleware/certs"
DEST_DIR="$BASE_DIR/uno"
HOSTNAME=$(hostname)
TAR_FILE=""

# Detectar ambiente basado en hostname
if [[ "$HOSTNAME" == *"dev"* || "$HOSTNAME" == *"DEV"* ]]; then
    TAR_FILE="DEV.tar"
elif [[ "$HOSTNAME" == *"uat"* || "$HOSTNAME" == *"UAT"* ]]; then
    TAR_FILE="UAT.tar"
elif [[ "$HOSTNAME" == *"prod"* || "$HOSTNAME" == *"PROD"* ]]; then
    TAR_FILE="PROD.tar"
else
    echo "Error: Ambiente no reconocido en el hostname."
    exit 1
fi

# Verificar que la ruta base existe
if [[ ! -d "$BASE_DIR" ]]; then
    echo "Error: La ruta base $BASE_DIR no existe."
    exit 1
fi

# Crear la carpeta si no existe
if [[ ! -d "$DEST_DIR" ]]; then
    echo "Creando carpeta $DEST_DIR..."
    mkdir -p "$DEST_DIR"
    if [[ $? -ne 0 ]]; then
        echo "Error: No se pudo crear el directorio $DEST_DIR."
        exit 1
    fi
fi

# Verificar si el archivo TAR existe
if [[ ! -f "$TAR_FILE" ]]; then
    echo "Error: El archivo $TAR_FILE no existe en el directorio actual."
    exit 1
fi

# Descomprimir el archivo TAR en el directorio destino
echo "Descomprimiendo $TAR_FILE en $DEST_DIR..."
tar -xf "$TAR_FILE" -C "$DEST_DIR"

if [[ $? -eq 0 ]]; then
    echo "Descompresión completada exitosamente."
else
    echo "Error: Falló la descompresión de $TAR_FILE."
    exit 1
fi

# Otorgar permisos 775 a la carpeta y su contenido
echo "Otorgando permisos 775 a $DEST_DIR y su contenido..."
chmod -R 775 "$DEST_DIR"

if [[ $? -eq 0 ]]; then
    echo "Permisos aplicados correctamente."
else
    echo "Error: No se pudieron aplicar los permisos."
    exit 1
fi

echo "Proceso completado exitosamente."
