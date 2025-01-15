#!/bin/bash

# Variables de rutas
RUTA_BASE="/opt/miapp"
RUTA_TMP="$RUTA_BASE/tmp"
RUTA_PROPERTIES="$RUTA_BASE/properties"
TAR_ARCHIVO_NUEVO="$RUTA_TMP/configuracion.tar"
TAR_ARCHIVO_RESPALDO="$RUTA_TMP/backup_properties.tar"
LOG_FILE="$RUTA_TMP/ajusta_configuracion.log"
DAYS_TO_KEEP=7  # Días para mantener respaldos

# Inicializar log
echo "$(date): Iniciando ajuste de configuración." >> "$LOG_FILE"

# Validar el argumento recibido
if [[ $# -ne 1 ]]; then
    echo "Uso: $0 <1 para instalar | 0 para rollback>" | tee -a "$LOG_FILE"
    exit 1
fi

ACCION=$1

# Función para limpiar respaldos antiguos
limpiar_respaldos_antiguos() {
    echo "Eliminando respaldos de más de $DAYS_TO_KEEP días..." | tee -a "$LOG_FILE"
    find "$RUTA_TMP" -name "backup_properties*.tar" -type f -mtime +$DAYS_TO_KEEP -exec rm -f {} \;
    if [[ $? -eq 0 ]]; then
        echo "Limpieza de respaldos antiguos completada." | tee -a "$LOG_FILE"
    else
        echo "Advertencia: No se pudo completar la limpieza de algunos respaldos." | tee -a "$LOG_FILE"
    fi
}

if [[ "$ACCION" -eq 1 ]]; then
    echo "Realizando respaldo de properties actuales..." | tee -a "$LOG_FILE"
    
    # Crear respaldo de properties actuales
    if [[ -d "$RUTA_PROPERTIES" ]]; then
        tar -cvf "$TAR_ARCHIVO_RESPALDO" -C "$RUTA_BASE" properties >> "$LOG_FILE" 2>&1
        if [[ $? -ne 0 ]]; then
            echo "Error: No se pudo crear el archivo de respaldo $TAR_ARCHIVO_RESPALDO." | tee -a "$LOG_FILE"
            exit 1
        fi
    else
        echo "Advertencia: No existe la carpeta $RUTA_PROPERTIES para respaldar." | tee -a "$LOG_FILE"
    fi

    # Limpiar respaldos antiguos
    limpiar_respaldos_antiguos

    # Verificar si el archivo TAR de nuevas properties existe
    if [[ ! -f "$TAR_ARCHIVO_NUEVO" ]]; then
        echo "Error: No se encontró el archivo $TAR_ARCHIVO_NUEVO con las nuevas properties." | tee -a "$LOG_FILE"
        exit 1
    fi

    # Descomprimir el archivo TAR de nuevas properties
    echo "Descomprimiendo $TAR_ARCHIVO_NUEVO en $RUTA_TMP..." | tee -a "$LOG_FILE"
    tar -xvf "$TAR_ARCHIVO_NUEVO" -C "$RUTA_TMP" >> "$LOG_FILE" 2>&1
    if [[ $? -ne 0 ]]; then
        echo "Error: Fallo al descomprimir el archivo $TAR_ARCHIVO_NUEVO." | tee -a "$LOG_FILE"
        exit 1
    fi

    # Detectar ambiente y copiar las nuevas properties
    HOSTNAME=$(hostname)
    ENV_DIR=""
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

    RUTA_AMBIENTE="$RUTA_TMP/$ENV_DIR/properties"
    if [[ ! -d "$RUTA_AMBIENTE" ]]; then
        echo "Error: No se encontró la carpeta de properties $ENV_DIR en $RUTA_TMP." | tee -a "$LOG_FILE"
        exit 1
    fi

    echo "Copiando nuevas properties desde $RUTA_AMBIENTE a $RUTA_PROPERTIES..." | tee -a "$LOG_FILE"
    mkdir -p "$RUTA_PROPERTIES"
    cp -r "$RUTA_AMBIENTE/"* "$RUTA_PROPERTIES/" >> "$LOG_FILE" 2>&1
    if [[ $? -ne 0 ]]; then
        echo "Error: Fallo al copiar las nuevas properties." | tee -a "$LOG_FILE"
        exit 1
    fi

    ACCION_DESC="Instalación"
    echo "Proceso de instalación completado exitosamente." | tee -a "$LOG_FILE"

elif [[ "$ACCION" -eq 0 ]]; then
    if [[ -f "$TAR_ARCHIVO_RESPALDO" ]]; then
        echo "Restaurando properties desde el respaldo $TAR_ARCHIVO_RESPALDO..." | tee -a "$LOG_FILE"
        tar -xvf "$TAR_ARCHIVO_RESPALDO" -C "$RUTA_BASE" >> "$LOG_FILE" 2>&1
        if [[ $? -ne 0 ]]; then
            echo "Error: Fallo al restaurar las properties desde el archivo $TAR_ARCHIVO_RESPALDO." | tee -a "$LOG_FILE"
            exit 1
        fi
        ACCION_DESC="Rollback"
        echo "Rollback completado exitosamente." | tee -a "$LOG_FILE"
    else
        echo "Error: No se encontró un archivo de respaldo $TAR_ARCHIVO_RESPALDO para realizar el rollback." | tee -a "$LOG_FILE"
        exit 1
    fi
else
    echo "Error: Opción inválida. Use 1 para instalar o 0 para rollback." | tee -a "$LOG_FILE"
    exit 1
fi

# 5. Crear el archivo de validación con la fecha y acción realizada
VALIDACION_FILE="$RUTA_BASE/properties/validacion-$ENV_DIR.txt"
echo "Creando archivo de validación: $VALIDACION_FILE" | tee -a "$LOG_FILE"
echo "Acción: $ACCION_DESC" > "$VALIDACION_FILE"
echo "Fecha: $(date)" >> "$VALIDACION_FILE"
echo "Ambiente: $ENV_DIR" >> "$VALIDACION_FILE"
if [[ $? -eq 0 ]]; then
    echo "Archivo de validación creado correctamente." | tee -a "$LOG_FILE"
else
    echo "Error: Fallo al crear el archivo de validación $VALIDACION_FILE." | tee -a "$LOG_FILE"
    exit 1
fi

# 6. Limpiar archivos temporales solo si todo el proceso fue exitoso
echo "Limpieza de archivos temporales en $RUTA_TMP..." | tee -a "$LOG_FILE"
rm -f "$TAR_ARCHIVO_NUEVO"
if [[ $? -eq 0 ]]; then
    echo "Limpieza completada exitosamente." | tee -a "$LOG_FILE"
else
    echo "Advertencia: Hubo un problema al limpiar archivos temporales." | tee -a "$LOG_FILE"
fi

echo "$(date): Proceso completado correctamente para el ambiente $ENV_DIR." | tee -a "$LOG_FILE"
