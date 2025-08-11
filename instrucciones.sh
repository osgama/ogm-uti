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