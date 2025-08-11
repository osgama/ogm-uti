#!/usr/bin/env bash
# Reinicio de Deployments vía oc leyendo /opt/miapp/Autosys/oc-envs.ini
# Uso:    ./oc-restart-from-ini.sh <dev|sit|uat|prod|cob>
# RCs:    0 OK; 10 cfg/uso; 11 login; 20 scale STOP; 21 wait STOP; 30 scale START; 31 rollout START

set -uo pipefail

INI="/opt/miapp/Autosys/oc-envs.ini"

# ---------- Helpers INI ----------
ini_get(){ # ini_get <section> <key> <file>
  awk -F= -v sec="[$1]" -v key="$2" '
    $0==sec { f=1; next }
    /^\[/ { f=0 }
    f && $1==key { sub(/^[^=]+= */,""); print; exit }
  ' "$3"
}
settings_get(){ # settings_get <env> <key> <file>
  local v; v="$(ini_get "$1" "$2" "$3")"
  [[ -z "$v" ]] && v="$(ini_get settings "$2" "$3")"
  printf '%s' "$v"
}
isnum(){ [[ "$1" =~ ^[0-9]+$ ]]; }

# ---------- Args ----------
[[ $# -ne 1 ]] && { echo "Uso: $0 <dev|sit|uat|prod|cob>" >&2; exit 10; }
ENV="$1"
[[ -r "$INI" ]] || { echo "No se puede leer $INI" >&2; exit 10; }

# ---------- Credenciales/endpoint ----------
OC_SERVER="$(ini_get "$ENV" OC_SERVER "$INI")"
USERNAME="$(ini_get "$ENV" USERNAME  "$INI")"
PASSWORD_RAW="$(ini_get "$ENV" PASSWORD  "$INI")"
[[ -z "$OC_SERVER" || -z "$USERNAME" || -z "$PASSWORD_RAW" ]] && { echo "Faltan OC_SERVER/USERNAME/PASSWORD para [$ENV]" >&2; exit 10; }

# ---------- Settings ----------
BLOCK_SIZE="$(settings_get "$ENV" BLOCK_SIZE "$INI")";     [[ -z "$BLOCK_SIZE" ]] && BLOCK_SIZE=4
BLOCK_DELAY="$(settings_get "$ENV" BLOCK_DELAY "$INI")";   [[ -z "$BLOCK_DELAY" ]] && BLOCK_DELAY=120
TIMEOUT_DOWN="$(settings_get "$ENV" TIMEOUT_DOWN "$INI")"; [[ -z "$TIMEOUT_DOWN" ]] && TIMEOUT_DOWN=180
TIMEOUT_UP="$(settings_get "$ENV" TIMEOUT_UP "$INI")";     [[ -z "$TIMEOUT_UP" ]] && TIMEOUT_UP=600
SLEEP_INT="$(settings_get "$ENV" SLEEP_INT "$INI")";       [[ -z "$SLEEP_INT" ]] && SLEEP_INT=3
ROLLOUT_MODE="$(settings_get "$ENV" ROLLOUT_MODE "$INI")"; [[ -z "$ROLLOUT_MODE" ]] && ROLLOUT_MODE="simple"

LOG_DIR="$(settings_get "$ENV" LOG_DIR "$INI")";           [[ -z "$LOG_DIR" ]] && LOG_DIR="/var/log/miapp"
LOG_VERBOSITY="$(settings_get "$ENV" LOG_VERBOSITY "$INI")"; [[ -z "$LOG_VERBOSITY" ]] && LOG_VERBOSITY="INFO"
COLLECT_DIAG="$(settings_get "$ENV" COLLECT_DIAG "$INI")"; [[ -z "$COLLECT_DIAG" ]] && COLLECT_DIAG="true"

# SECRET_KEY_FILE: del INI (ambiente > settings). Env puede override si quieres.
SECRET_KEY_FILE_CFG="$(settings_get "$ENV" SECRET_KEY_FILE "$INI")"
SECRET_KEY_FILE="${SECRET_KEY_FILE:-$SECRET_KEY_FILE_CFG}"
[[ -z "$SECRET_KEY_FILE" ]] && { echo "SECRET_KEY_FILE no definido para [$ENV] ni en [settings]" >&2; exit 10; }

# Namespace: env -> settings -> pods
NAMESPACE="$(settings_get "$ENV" NAMESPACE "$INI")"
[[ -z "$NAMESPACE" ]] && NAMESPACE="$(ini_get pods NAMESPACE "$INI")"

# ---------- Deployments ----------
DEPLOY_LINE="$(ini_get pods DEPLOYMENTS "$INI")"
IFS=',' read -r -a DEPLOYMENTS <<< "$DEPLOY_LINE"
for i in "${!DEPLOYMENTS[@]}"; do DEPLOYMENTS[$i]="$(echo "${DEPLOYMENTS[$i]}" | xargs)"; done
[[ ${#DEPLOYMENTS[@]} -eq 0 ]] && { echo "DEPLOYMENTS vacío en [pods]" >&2; exit 10; }

# ---------- Validaciones ----------
for n in "$BLOCK_SIZE" "$BLOCK_DELAY" "$TIMEOUT_DOWN" "$TIMEOUT_UP" "$SLEEP_INT"; do
  isnum "$n" || { echo "Parámetro no numérico en INI: $n" >&2; exit 10; }
done
[[ "$ROLLOUT_MODE" == "simple" || "$ROLLOUT_MODE" == "redeploy" ]] || { echo "ROLLOUT_MODE debe ser simple|redeploy" >&2; exit 10; }
command -v oc >/dev/null 2>&1 || { echo "No se encontró 'oc' en PATH" >&2; exit 10; }
if [[ "$PASSWORD_RAW" == ENC:* ]]; then
  command -v openssl >/dev/null 2>&1 || { echo "Se requiere 'openssl' para descifrar PASSWORD=ENC:" >&2; exit 10; }
  [[ -r "$SECRET_KEY_FILE" ]] || { echo "No puedo leer SECRET_KEY_FILE=$SECRET_KEY_FILE" >&2; exit 10; }
fi

# ---------- Logging ----------
RUN_ID="$(date +%Y%m%dT%H%M%S)_$$"
mkdir -p "$LOG_DIR" >/dev/null 2>&1 || true
LOG_FILE="$LOG_DIR/oc-restart_${ENV}_${RUN_ID}.log"

ts(){ date +'%Y-%m-%dT%H:%M:%S%z'; }
_log(){ local lvl="$1"; shift; local msg="$*"; printf '%s [%s] %s\n' "$(ts)" "$lvl" "$msg" | tee -a "$LOG_FILE" >&2; }
logi(){ _log INFO "$*"; }
logw(){ _log WARN "$*"; }
loge(){ _log ERROR "$*"; }

# DEBUG: evita exponer secretos; activamos set -x sólo tras el login
DEBUG_TRACE=0; [[ "$LOG_VERBOSITY" == "DEBUG" ]] && DEBUG_TRACE=1

# ---------- KUBECONFIG temporal ----------
KUBECONFIG="$(mktemp)"; export KUBECONFIG
chmod 600 "$KUBECONFIG"
cleanup(){
  rc=$?
  logi "Fin (rc=$rc). Log: $LOG_FILE"
  oc logout >/dev/null 2>&1 || true
  rm -f "$KUBECONFIG"
  exit $rc
}
trap cleanup EXIT

# ---------- Login seguro (descifrado previo) ----------
logi "Inicio runId=$RUN_ID env=$(echo "$ENV" | tr a-z A-Z) server=$OC_SERVER ns=${NAMESPACE:-'(context default)'}"
logi "Settings: BLOCK_SIZE=$BLOCK_SIZE BLOCK_DELAY=${BLOCK_DELAY}s TIMEOUT_DOWN=${TIMEOUT_DOWN}s TIMEOUT_UP=${TIMEOUT_UP}s SLEEP_INT=${SLEEP_INT}s ROLLOUT_MODE=$ROLLOUT_MODE"
logi "oc client: $(oc version --client=true --short 2>/dev/null || echo 'desconocido')"

# Silenciar xtrace antes de manejar secretos
prev_xtrace_state="$(set -o | awk '/xtrace/ {print $2}')"; set +x

if [[ "$PASSWORD_RAW" == ENC:* ]]; then
  PASS_B64="${PASSWORD_RAW#ENC:}"
  PASSWORD_PLAIN="$(printf '%s' "$PASS_B64" | openssl enc -aes-256-cbc -d -a -pbkdf2 -md sha256 -pass file:"$SECRET_KEY_FILE")" || {
    loge "Fallo al descifrar PASSWORD (ENC:)"
    unset PASS_B64 PASSWORD_PLAIN
    [[ "$prev_xtrace_state" == "on" && $DEBUG_TRACE -eq 1 ]] && set -x
    exit 10
  }
else
  PASSWORD_PLAIN="$PASSWORD_RAW"
fi

if ! oc login "$OC_SERVER" --username="$USERNAME" --password="$PASSWORD_PLAIN" --insecure-skip-tls-verify=true >/dev/null 2>&1; then
  loge "oc login falló"
  unset PASSWORD_PLAIN PASS_B64
  [[ "$prev_xtrace_state" == "on" && $DEBUG_TRACE -eq 1 ]] && set -x
  exit 11
fi
unset PASSWORD_PLAIN PASS_B64
# Reactiva xtrace si procede
[[ $DEBUG_TRACE -eq 1 ]] && set -x

oc whoami >/dev/null 2>&1 && logi "whoami=$(oc whoami 2>/dev/null)"

NS_ARG=()
[[ -n "$NAMESPACE" ]] && NS_ARG=(-n "$NAMESPACE")

# ---------- Funciones ----------
scale_to(){ local replicas="$1"; shift; oc "${NS_ARG[@]}" scale "$@" --replicas="$replicas"; }
wait_down(){
  local res="$1" t=0 desired ready available
  while :; do
    desired="$(oc "${NS_ARG[@]}" get "$res" -o jsonpath='{.spec.replicas}' 2>/dev/null || echo "")"
    ready="$(oc "${NS_ARG[@]}"   get "$res" -o jsonpath='{.status.readyReplicas}' 2>/dev/null || echo "")"
    available="$(oc "${NS_ARG[@]}" get "$res" -o jsonpath='{.status.availableReplicas}' 2>/dev/null || echo "")"
    [[ "$desired" == "0" && "${ready:-0}" == "0" && "${available:-0}" == "0" ]] && return 0
    (( t+=SLEEP_INT, t >= TIMEOUT_DOWN )) && return 1
    sleep "$SLEEP_INT"
  done
}
rollout_up(){ local res="$1"; oc "${NS_ARG[@]}" rollout status "$res" --timeout="${TIMEOUT_UP}s"; }
collect_diag(){
  local res="$1"; [[ "$COLLECT_DIAG" != "true" ]] && return 0
  logw "Recolectando diagnósticos de $res"
  {
    echo "----- oc describe $res -----"; oc "${NS_ARG[@]}" describe "$res" || true
    echo; echo "----- Últimos eventos (50) -----"; oc "${NS_ARG[@]}" get events --sort-by=.lastTimestamp | tail -n 50 || true
  } >>"$LOG_FILE" 2>&1
}
start_item(){
  local res="$1"
  if [[ "$ROLLOUT_MODE" == "redeploy" ]]; then
    oc "${NS_ARG[@]}" rollout restart "$res" || { loge "Fallo en rollout restart: $res"; collect_diag "$res"; exit 30; }
    scale_to 1 "$res"                        || { loge "Fallo al escalar a 1 (redeploy): $res"; collect_diag "$res"; exit 30; }
    oc "${NS_ARG[@]}" rollout status "$res" --timeout="${TIMEOUT_UP}s" || { loge "Timeout en rollout (redeploy): $res"; collect_diag "$res"; exit 31; }
  else
    scale_to 1 "$res" || { loge "Fallo al escalar a 1: $res"; collect_diag "$res"; exit 30; }
    rollout_up "$res" || { loge "Timeout en rollout: $res"; collect_diag "$res"; exit 31; }
  fi
}

ok_count=0

# ---------- STOP: todos -> 0 ----------
logi "[STOP] Escalando ${#DEPLOYMENTS[@]} deployments → 0"
for d in "${DEPLOYMENTS[@]}"; do
  [[ -z "$d" ]] && continue
  logi " - $d -> 0"
  scale_to 0 "$d" || { loge "Fallo al escalar a 0: $d"; collect_diag "$d"; exit 20; }
done
for d in "${DEPLOYMENTS[@]}"; do
  [[ -z "$d" ]] && continue
  logi "   esperando $d en 0…"
  wait_down "$d" || { loge "Timeout esperando 0 replicas: $d"; collect_diag "$d"; exit 21; }
done

# ---------- START por bloques ----------
logi "[START] Subiendo en bloques de $BLOCK_SIZE con pausa de ${BLOCK_DELAY}s entre bloques"
count=0; block=1; declare -a block_items=()

flush_block(){
  local -a arr=( "$@" ); [[ ${#arr[@]} -eq 0 ]] && return 0
  logi "  > Bloque #$block (${#arr[@]})"
  if [[ "$ROLLOUT_MODE" == "simple" ]]; then
    for r in "${arr[@]}"; do logi "    + $r -> 1"; scale_to 1 "$r" || { loge "Fallo al escalar a 1: $r"; collect_diag "$r"; exit 30; }; done
    for r in "${arr[@]}"; do logi "      esperando rollout: $r"; rollout_up "$r" || { loge "Timeout en rollout: $r"; collect_diag "$r"; exit 31; }; (( ok_count++ )); done
  else
    for r in "${arr[@]}"; do logi "    + redeploy $r"; start_item "$r"; (( ok_count++ )); done
  fi
  logi "  < Bloque #$block OK"; (( block++ ))
}

total=${#DEPLOYMENTS[@]}
for idx in "${!DEPLOYMENTS[@]}"; do
  d="${DEPLOYMENTS[$idx]}"; [[ -z "$d" ]] && continue
  block_items+=( "$d" ); (( count++ ))
  if (( count % BLOCK_SIZE == 0 )); then
    flush_block "${block_items[@]}"; block_items=()
    if (( idx < total-1 )); then logi "  .. esperando ${BLOCK_DELAY}s antes del siguiente bloque"; sleep "$BLOCK_DELAY"; fi
  fi
done
(( ${#block_items[@]} > 0 )) && flush_block "${block_items[@]}"

# ---------- Resumen ----------
logi "Resumen: ok=${ok_count} fail=0"
echo "OK: Reinicio completado (STOP+START) para ${#DEPLOYMENTS[@]} deployments. Log: $LOG_FILE"
exit 0
