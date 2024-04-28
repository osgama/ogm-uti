#!/bin/bash

# Configuración inicial
SERVIDOR="$1"
USUARIO="$2"
PWD="$3"
OPERACION="$4" # scale-up, scale-down, delete-completed
OPCION="$5" # A o B, para seleccionar la lista de pods
BLOCK_SIZE=4 # Tamaño del bloque para el escalado hacia arriba

# Conexión
echo "Iniciando sesión en OpenShift..."
oc login "$SERVIDOR" --username="$USUARIO" --password="$PWD" --insecure-skip-tls-verify=true
echo "Sesión iniciada correctamente."

# Función para cargar lista de pods desde una variable de entorno
function cargar_lista_pods {
    local env_var_name="VALORESLISTA$OPCION" # Ajusta para VALORESLISTAA o VALORESLISTAB
    IFS=',' read -r -a pods <<< "$(echo ${!env_var_name})"
    # Limpiar espacios en blanco de los nombres de los pods
    pods=("${pods[@]/# /}")  # Quita espacios al inicio
    pods=("${pods[@]/% /}")  # Quita espacios al final
    echo "Lista de pods cargada para la opción $OPCION: ${pods[*]}"
}

# Escalar pods hacia arriba en bloques
function escalar_pods_en_bloques {
    local replicas=1
    echo "Iniciando escalado hacia arriba en bloques de tamaño $BLOCK_SIZE..."
    local total_pods=${#pods[@]}
    local i=0
    while [ $i -lt $total_pods ]; do
        local end=$((i + BLOCK_SIZE))
        if [ $end -gt $total_pods ]; then
            end=$total_pods
        fi
        local current_block=("${pods[@]:i:BLOCK_SIZE}")
        echo "Escalar los siguientes pods: ${current_block[*]} a $replicas replicas..."
        for pod_name in "${current_block[@]}"; do
            oc scale dc/$pod_name --replicas=$replicas -n tu-namespace
        done
        echo "Esperando que los pods del bloque actual estén completamente activos..."
        while true; do
            local all_ready=true
            for pod_name in "${current_block[@]}"; do
                local status=$(oc get pod -l name=$pod_name -n tu-namespace -o jsonpath="{.items[*].status.phase}")
                if [[ "$status" != "Running" ]]; then
                    all_ready=false
                    break
                fi
            done
            if [ "$all_ready" = true ]; then
                echo "Todos los pods del bloque están activos."
                break
            else
                echo "Esperando a que los pods estén listos..."
                sleep 10
            fi
        done
        i=$((i + BLOCK_SIZE))
        if [ $i -lt $total_pods ]; then
            echo "Pausa de 2 minutos antes de continuar con el siguiente bloque..."
            sleep 120
        fi
    done
    echo "Escalado hacia arriba completado."
}

# Escalar todos los pods hacia abajo
function escalar_pods_hacia_abajo {
    local replicas=0
    echo "Iniciando escalado hacia abajo para todos los pods..."
    for pod_name in "${pods[@]}"; do
        oc scale dc/$pod_name --replicas=$replicas -n tu-namespace
    done
    echo "Esperando que todos los pods hayan terminado..."
    local all_terminated=false
    while [ "$all_terminated" = false ]; do
        all_terminated=true
        for pod_name in "${pods[@]}"; do
            local status=$(oc get pod -l name=$pod_name -n tu-namespace -o jsonpath="{.items[*].status.phase}")
            if [[ "$status" != "Terminated" ]]; then
                all_terminated=false
                echo "Esperando la terminación de $pod_name..."
                sleep 10
                break
            fi
        done
    done
    echo "Todos los pods han sido terminados correctamente."
}

# Eliminar pods completados
function eliminar_pods_completados {
    echo "Iniciando la eliminación de pods completados..."
    local completed_pods=$(oc get pods -n tu-namespace --field-selector=status.phase=Completed -o jsonpath="{.items[*].metadata.name}")
    for pod in $completed_pods; do
        oc delete pod $pod -n tu-namespace
        echo "Pod $pod eliminado."
    done
    echo "Eliminación de pods completados finalizada."
}

# Cargar la lista de pods basada en la opción
cargar_lista_pods

case $OPERACION in
    "scale-down")
        escalar_pods_hacia_abajo
        ;;
    "scale-up")
        escalar_pods_en_bloques
        ;;
    "delete-completed")
        eliminar_pods_completados
        ;;
    *)
        echo "Operación no reconocida"
        exit 1
        ;;
esac
