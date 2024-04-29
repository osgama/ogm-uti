#!/bin/bash

# Parámetros: servidor, usuario, contraseña
OC_SERVER="$1"
USERNAME="$2"
PASSWORD="$3"

# Intenta obtener y validar el token hasta tres veces
for i in {1..3}; do
    # Login y redirige la salida estándar y de error a /dev/null
    oc login $OC_SERVER --username=$USERNAME --password=$PASSWORD --insecure-skip-tls-verify=true > /dev/null 2>&1

    # Obtiene el token y lo imprime
    TOKEN=$(oc whoami -t)

    # Introduce un retraso para permitir la propagación del token
    sleep 5  # Retraso de 5 segundos

    # Verifica la validez del token intentando una operación simple
    oc version --token=$TOKEN > /dev/null 2>&1
    if [ $? -eq 0 ]; then
        echo $TOKEN  # Solo imprime el token si la verificación es exitosa
        exit 0  # Sale del script con éxito
    fi

    # Opcionalmente, espera antes del siguiente intento
    sleep 5
done

# Si llega aquí, significa que todos los intentos fallaron
echo "Token verification failed after 3 attempts" >&2
exit 1

# Cerrar la sesión, redirigiendo toda la salida a /dev/null
oc logout > /dev/null 2>&1
