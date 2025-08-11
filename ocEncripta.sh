#!/usr/bin/env bash
# make-oc-password-blob.sh
# Cifra un password con AES-256-CBC + PBKDF2 (OpenSSL) usando una clave en archivo,
# y genera:
#   - ./oc_pass.enc.b64           (blob base64)
#   - ./oc_password_snippet.txt   (línea "PASSWORD=ENC:...").
# Con --show imprime:  COPIA ESTO: PASSWORD=ENC:<base64>

set -euo pipefail
umask 077

KEY_FILE="/run/secrets/oc_secret_key"
OUT_B64="./oc_pass.enc.b64"
SNIPPET="./oc_password_snippet.txt"
PASSWORD=""
SHOW=0

log(){ printf '%s\n' "$*" >&2; }
die(){ log "ERROR: $*"; exit 1; }

usage(){
  cat >&2 <<EOF
Uso:
  $(basename "$0") [--password <pwd>] [--key-file <ruta>] [--out <blob.b64>] [--snippet <snippet.txt>] [--show]

Opciones:
  --password <pwd>     Password en claro (si no se pasa, se solicitará oculto)
  --key-file <ruta>    Ruta de la clave (default: $KEY_FILE). Se crea si no existe
  --out <archivo>      Salida del blob base64 (default: $OUT_B64)
  --snippet <archivo>  Salida de la línea "PASSWORD=ENC:..." (default: $SNIPPET)
  --show               IMPRIME en pantalla "COPIA ESTO: PASSWORD=ENC:<base64>" (riesgo de exposición)
  -h, --help           Ayuda
EOF
}

# --- Parseo ---
while [[ $# -gt 0 ]]; do
  case "$1" in
    --password) PASSWORD="${2:-}"; shift 2 ;;
    --key-file) KEY_FILE="${2:-}"; shift 2 ;;
    --out)      OUT_B64="${2:-}"; shift 2 ;;
    --snippet)  SNIPPET="${2:-}"; shift 2 ;;
    --show)     SHOW=1; shift ;;
    -h|--help)  usage; exit 0 ;;
    *) die "Opción no reconocida: $1" ;;
  esac
done

# --- Requisitos ---
command -v openssl >/dev/null 2>&1 || die "Se requiere 'openssl' en el PATH"

# --- Password ---
if [[ -z "${PASSWORD}" ]]; then
  read -rs -p "Password a cifrar (no se mostrará): " PASSWORD
  echo
  [[ -z "${PASSWORD}" ]] && die "Password vacío"
fi

# --- Clave: crea si no existe ---
if [[ ! -f "$KEY_FILE" ]]; then
  install -d -m 700 "$(dirname "$KEY_FILE")"
  openssl rand -base64 48 > "$KEY_FILE"
  chmod 600 "$KEY_FILE"
  log "Clave creada en: $KEY_FILE"
else
  chmod 600 "$KEY_FILE" 2>/dev/null || true
  log "Usando clave existente: $KEY_FILE"
fi

# --- Cifrar (silenciando xtrace por seguridad) ---
prev_xtrace_state="$(set -o | awk '/xtrace/ {print $2}')"
set +x
BLOB="$(printf '%s' "$PASSWORD" | openssl enc -aes-256-cbc -salt -pbkdf2 -md sha256 -a -pass file:"$KEY_FILE")"
unset PASSWORD
[[ "$prev_xtrace_state" == "on" ]] && set -x || true

# --- Guardar archivos ---
printf '%s\n' "$BLOB" > "$OUT_B64"
printf 'PASSWORD=ENC:%s\n' "$BLOB" > "$SNIPPET"
chmod 600 "$OUT_B64" "$SNIPPET"

# --- Mensajes finales ---
log "Archivo blob base64: $OUT_B64"
log "Snippet listo para INI: $SNIPPET"

if [[ $SHOW -eq 1 ]]; then
  # ¡Esto imprime el secreto! Úsalo a conciencia.
  printf 'COPIA ESTO: PASSWORD=ENC:%s\n' "$BLOB"
else
  log ""
  log "Para ver qué pegar en el INI sin abrir el blob, puedes mostrar el snippet así:"
  log "  cat \"$SNIPPET\""
fi

log ""
log "Próximos pasos:"
log "1) Pega la línea del snippet en la sección del ambiente en tu INI."
log "2) En el servidor de ejecución, exporta la ruta de la clave:"
log "   export SECRET_KEY_FILE=\"$KEY_FILE\""
log "3) Ejecuta tu reinicio:"
log "   ./oc-restart-from-ini.sh <env>"
