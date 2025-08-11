Código	Significado	¿Cuándo ocurre? (ejemplos)
0	Éxito total	STOP→START completado, todos los deployments OK
10	Error de uso/configuración	Falta el argumento <env>; no se puede leer el INI; faltan OC_SERVER/USERNAME/PASSWORD; DEPLOYMENTS vacío; valores no numéricos en settings; ROLLOUT_MODE inválido; oc no está en PATH
11	Fallo de login	oc login falla (credenciales/servidor TLS etc.)
20	Error al detener (STOP)	Falla oc scale … --replicas=0 en algún deployment
21	Timeout esperando 0 réplicas	Algún deployment no baja a 0 dentro de TIMEOUT_DOWN
30	Error al iniciar (START)	Modo simple: oc scale … --replicas=1 falla; Modo redeploy: oc rollout restart o scale 1 falla
31	Timeout de rollout al subir	oc rollout status no llega a “available” dentro de TIMEOUT_UP



Ejemplos rápidos:
./oc-restart-from-ini.sh sit; echo $?        # imprime el rc
rc=$?; echo "rc=$rc"                         # guardarlo y luego imprimir


Control de flujo minimal:
./oc-restart-from-ini.sh prod && echo "OK" || echo "FAIL rc=$?"



# Te pide el password oculto y guarda archivos; NO imprime el secreto
./make-oc-password-blob.sh

# Pasa el password por argumento y muestra en pantalla la línea para pegar (imprime secreto)
./make-oc-password-blob.sh --password 'MiP@ssw0rd!' --show

# Personalizando rutas de salida y la clave
./make-oc-password-blob.sh \
  --key-file /run/secrets/oc_secret_key \
  --out ./prod_pass.enc.b64 \
  --snippet ./prod_password_snippet.txt \
  --show




*******************************************************


El flujo final queda así:

Generas el blob y la clave con make-oc-password-blob.sh.

Pegas el valor al INI como:

PASSWORD=ENC:AAAA...tu_base64...ZZZ
Copias el archivo de clave al server donde correrá el job en la ruta acordada (ej. /run/secrets/oc_secret_key).

En el servidor de ejecución:

Asegura permisos:


sudo install -d -m 700 /run/secrets
sudo cp oc_secret_key /run/secrets/oc_secret_key
sudo chmod 600 /run/secrets/oc_secret_key
Exporta la variable antes de ejecutar (o configúrala en el job de Autosys):

export SECRET_KEY_FILE=/run/secrets/oc_secret_key
Ejecuta tu script:


./oc-restart-from-ini.sh sit
Mini check rápido (opcional)
Para validar que el descifrado y el login funcionan sin hacer el reinicio completo:


export SECRET_KEY_FILE=/run/secrets/oc_secret_key
# Extrae server/usuario/password del INI como ya hace tu script:
# (o simplemente corre el script y cancela después del login)
./oc-restart-from-ini.sh sit && echo "OK" || echo "FAIL $?"

Notas útiles
Puedes usar la misma clave para todos los ambientes (más simple) o distintas (más segregación). Si cambias la clave, recuerda regenerar el blob ENC: con esa clave.
El script ya evita imprimir secretos aunque uses LOG_VERBOSITY=DEBUG.
Si falta SECRET_KEY_FILE o no tiene permisos correctos, el script falla con rc=10 (config) antes de intentar el login.
Con eso estás listo: valor ENC en el INI + archivo de clave en la ruta = todo funciona en runtime.