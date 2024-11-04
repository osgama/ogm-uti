#!/bin/bash

# Variables de rutas
RUTA_BASE="/opt/miapp"
RUTA_TMP="$RUTA_BASE/tmp"
TAR_ARCHIVO="$RUTA_TMP/configuracion.tar"  # Nombre actualizado del archivo .tar
LOG_FILE="$RUTA_TMP/ajusta_configuracion.log"

# Inicializar log
echo "$(date): Iniciando ajuste de configuración." >> "$LOG_FILE"

# Obtener el hostname y definir el ambiente (dev, uat, prod)
HOSTNAME=$(hostname)
ENV_DIR=""

# Detectar ambiente basado en el hostname
if [[ "$HOSTNAME" == *"dev"* || "$HOSTNAME" == *"DEV"* ]]; then
    ENV_DIR="dev"
elif [[ "$HOSTNAME" == *"uat"* || "$HOSTNAME" == *"UAT"* ]]; then
    ENV_DIR="uat"
elif [[ "$HOSTNAME" == *"prod"* || "$HOSTNAME" == *"PROD"* ]]; then
    ENV_DIR="prod"
else
    echo "Error: Ambiente no reconocido en el hostname." | tee -a "$LOG_FILE"
    exit 1
fi

# 1. Verificar si el archivo .tar existe
if [[ ! -f "$TAR_ARCHIVO" ]]; then
    echo "Error: El archivo $TAR_ARCHIVO no se encuentra. Asegúrate de que esté disponible en $RUTA_TMP." | tee -a "$LOG_FILE"
    exit 1
fi

# 2. Descomprimir el .tar en la ruta temporal
echo "Descomprimiendo el archivo $TAR_ARCHIVO en $RUTA_TMP..." | tee -a "$LOG_FILE"
tar -xvf "$TAR_ARCHIVO" -C "$RUTA_TMP" >> "$LOG_FILE" 2>&1
if [[ $? -ne 0 ]]; then
    echo "Error: Fallo al descomprimir el archivo $TAR_ARCHIVO." | tee -a "$LOG_FILE"
    exit 1
fi

# 3. Verificar si la carpeta del ambiente existe en la ruta temporal
RUTA_AMBIENTE="$RUTA_TMP/$ENV_DIR"
if [[ ! -d "$RUTA_AMBIENTE" ]]; then
    echo "Error: La carpeta del ambiente $ENV_DIR no existe en $RUTA_TMP." | tee -a "$LOG_FILE"
    exit 1
fi

# 4. Copiar todo el contenido del ambiente a la ruta base
echo "Copiando todo el contenido de $RUTA_AMBIENTE a $RUTA_BASE..." | tee -a "$LOG_FILE"
cp -r "$RUTA_AMBIENTE/"* "$RUTA_BASE/" >> "$LOG_FILE" 2>&1
if [[ $? -ne 0 ]]; then
    echo "Error: Fallo al copiar el contenido de $RUTA_AMBIENTE a $RUTA_BASE." | tee -a "$LOG_FILE"
    exit 1
fi

# 5. Verificar la copia de configuraciones mediante el archivo de validación dinámico
VALIDACION_FILE="$RUTA_BASE/properties/validacion-$ENV_DIR.txt"
if [[ -f "$VALIDACION_FILE" ]]; then
    echo "Validación exitosa: Configuración de $ENV_DIR copiada correctamente." | tee -a "$LOG_FILE"
else
    echo "Error: Archivo de validación $VALIDACION_FILE no encontrado. Verifica la copia de configuraciones." | tee -a "$LOG_FILE"
    exit 1
fi

# 6. Limpiar archivos y carpetas temporales si el proceso fue exitoso
echo "Limpiando archivos temporales en $RUTA_TMP..." | tee -a "$LOG_FILE"
rm -rf "$RUTA_TMP/dev" "$RUTA_TMP/uat" "$RUTA_TMP/prod" "$TAR_ARCHIVO" >> "$LOG_FILE" 2>&1
if [[ $? -eq 0 ]]; then
    echo "Limpieza completada exitosamente." | tee -a "$LOG_FILE"
else
    echo "Advertencia: Hubo un problema al limpiar archivos temporales." | tee -a "$LOG_FILE"
fi

echo "$(date): Proceso completado correctamente." | tee -a "$LOG_FILE"
