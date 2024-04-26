#!/bin/bash

# Parámetros: servidor, usuario, contraseña
OC_SERVER="$1"
USERNAME="$2"
PASSWORD="$3"

# Login y obtención del token
oc login $OC_SERVER --username=$USERNAME --password=$PASSWORD --insecure-skip-tls-verify=true
TOKEN=$(oc whoami -t)

# Imprime el token
echo $TOKEN

# Cerrar la sesión
oc logout
