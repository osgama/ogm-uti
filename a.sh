#!/bin/bash

# Variables
BASE_DIR="/opt/middleware/certs"
DEST_DIR="$BASE_DIR/uno"
TAR_FILE="certificados.tar"
TEMP_DIR="/tmp/certificados_temp"
HOSTNAME=$(hostname)
ENV_DIR=""

# Detectar ambiente basado en hostname
if [[ "$HOSTNAME" == *"dev"* || "$HOSTNAME" == *"DEV"* ]]; then
    ENV_DIR="DEV"
elif [[ "$HOSTNAME" == *"uat"* || "$HOSTNAME" == *"UAT"* ]]; then
    ENV_DIR="UAT"
elif [[ "$HOSTNAME" == *"prod"* || "$HOSTNAME" == *"PROD"* || "$HOSTNAME" == *"PROD"* || "$HOSTNAME" == *"PROD"*]]; then
    ENV_DIR="PROD"
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

# Crear un directorio temporal para descomprimir el TAR
mkdir -p "$TEMP_DIR"
if [[ $? -ne 0 ]]; then
    echo "Error: No se pudo crear el directorio temporal $TEMP_DIR."
    exit 1
fi

# Descomprimir el archivo TAR en el directorio temporal
echo "Descomprimiendo $TAR_FILE en $TEMP_DIR..."
tar -xf "$TAR_FILE" -C "$TEMP_DIR"

if [[ $? -ne 0 ]]; then
    echo "Error: Falló la descompresión del archivo $TAR_FILE."
    rm -rf "$TEMP_DIR"
    exit 1
fi

# Verificar si la carpeta del ambiente existe en el TAR
if [[ ! -d "$TEMP_DIR/$ENV_DIR" ]]; then
    echo "Error: La carpeta $ENV_DIR no se encontró en el archivo TAR."
    rm -rf "$TEMP_DIR"
    exit 1
fi

# Copiar el contenido de la carpeta correspondiente al ambiente al destino
echo "Copiando archivos de $ENV_DIR a $DEST_DIR..."
cp -r "$TEMP_DIR/$ENV_DIR/"* "$DEST_DIR/"

if [[ $? -eq 0 ]]; then
    echo "Archivos copiados exitosamente."
else
    echo "Error: No se pudieron copiar los archivos."
    rm -rf "$TEMP_DIR"
    exit 1
fi

# Otorgar permisos 775 a la carpeta y su contenido
echo "Otorgando permisos 775 a $DEST_DIR y su contenido..."
chmod -R 775 "$DEST_DIR"

if [[ $? -eq 0 ]]; then
    echo "Permisos aplicados correctamente."
else
    echo "Error: No se pudieron aplicar los permisos."
    rm -rf "$TEMP_DIR"
    exit 1
fi

# Limpiar: eliminar el directorio temporal y carpetas innecesarias
echo "Limpiando archivos temporales..."
rm -rf "$TEMP_DIR"

echo "Proceso completado exitosamente."
