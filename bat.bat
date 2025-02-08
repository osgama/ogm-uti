@echo off
CHCP 65001 > NUL

:: Configuración de colores
set "color_texto=0B"  :: Primer plano amarillo claro, segundo plano negro

:inicio
cls
:: Aplicar configuración de colores
color %color_texto%

ECHO **************************************************
ECHO *          v3.0 Script de Administración         *
ECHO *                  %DATE% %TIME%                 *
ECHO **************************************************
ECHO **************************************************
ECHO *                  Menú de Login                 *
ECHO **************************************************
ECHO *   1. Compilador                                *
ECHO *   2. DEV                                       *
ECHO *   3. SIT                                       *
ECHO *   4. UAT                                       *
ECHO *   5. Salir Completamente                       *
ECHO **************************************************

set /p opcion_login=Seleccione una opción de login (1-5):

rem Validación de entrada
if %opcion_login% lss 1 goto inicio
if %opcion_login% gtr 5 goto inicio

if %opcion_login%==1 goto login1
if %opcion_login%==2 goto login2
if %opcion_login%==3 goto login3
if %opcion_login%==4 goto login4
if %opcion_login%==5 goto salir

:login1
cls
    ECHO **************************************************
    ECHO *              Conectándose Compilador           *
    ECHO **************************************************
    ECHO *  User: %USERNAME%                              *
    ECHO *  Server: MISERVER                              *
    ECHO **************************************************
    oc login --server=https://api:6443 -u=%USERNAME% -p=
    ECHO **************************************************
    timeout 3 > NUL
goto menu_principaldos

:login2
cls
    ECHO **************************************************
    ECHO *                Conectándose DEV                *
    ECHO **************************************************
    ECHO *  User: %USERNAME%                              *
    ECHO *  Server: MISERVER                              *
    ECHO **************************************************
    oc login --server=https://api:6443 -u=%USERNAME% -p=
    ECHO **************************************************
    timeout 3 > NUL
goto menu_principal

:login3
cls
    ECHO **************************************************
    ECHO *                Conectándose SIT                *
    ECHO **************************************************
    ECHO *  User: %USERNAME%                              *
    ECHO *  Server: MISERVER                              *
    ECHO **************************************************
    oc login --server=https://api:6443 -u=%USERNAME% -p=
    ECHO **************************************************
    timeout 3 > NUL
goto menu_principal

:login4
cls
    ECHO **************************************************
    ECHO *                Conectándose UAT                *
    ECHO **************************************************
    ECHO *  User: %USERNAME%                              *
    ECHO *  Server: MISERVER                              *
    ECHO **************************************************
    oc login --server=https://api:6443 -u=%USERNAME% -p=
    ECHO **************************************************
    timeout 3 > NUL
goto menu_principal

:salir
cls
ECHO **************************************************
ECHO *                  Saliendo, ¡Adios!             *
ECHO **************************************************
timeout 2 > NUL
exit

:menu_principal
cls
    ECHO **************************************************
    ECHO *                     MENÚ                      *
    ECHO **************************************************
    ECHO *   1.  Detener TODO                            *
    ECHO *   2.  Iniciar TODO                            *
    ECHO *   3.  Detener Línea                           *
    ECHO *   4.  Iniciar Línea                           *
    ECHO *   5.  Detener BATCH                           *
    ECHO *   6.  Iniciar BATCH                           *
    ECHO *   7.  Detener UDP/CLC                         *
    ECHO *   8.  Iniciar UDP/CLC                         *
    ECHO *   9.  Detener WEB                             *
    ECHO *  10.  Iniciar WEB                             *
    ECHO *  11.  Reiniciar Utilidades                    *
    ECHO *  12.  Validar Servicios                       *
    ECHO *  13.  Limpiar Instalación                     *
    ECHO *  14.  Regresar al Menú principal              *
    ECHO *  15.  Salir Completamente                     *
    ECHO **************************************************

set /p opcion_menu=Seleccione una opción (1-15):

rem Validación de entrada
if %opcion_menu% lss 1 goto menu_principal
if %opcion_menu% gtr 15 goto menu_principal

if %opcion_menu%==1 goto submenu1
if %opcion_menu%==2 goto submenu2
if %opcion_menu%==3 goto submenu3
if %opcion_menu%==4 goto submenu4
if %opcion_menu%==5 goto submenu5
if %opcion_menu%==6 goto submenu6
if %opcion_menu%==7 goto submenu7
if %opcion_menu%==8 goto submenu8
if %opcion_menu%==9 goto submenu9
if %opcion_menu%==10 goto submenu10
if %opcion_menu%==11 goto submenu11
if %opcion_menu%==12 goto submenu12
if %opcion_menu%==13 goto submenu13
if %opcion_menu%==14 goto inicio
if %opcion_menu%==15 goto salir

:submenu1
cls
    ECHO **************************************************
    ECHO *                 Deteniendo todo                *
    ECHO **************************************************

    oc scale deployment webapp --replicas=0
    oc scale deployment budp --replicas=0
    oc scale deployment catws --replicas=0
    oc scale deployment citiws --replicas=0
    oc scale deployment secws --replicas=0
    oc scale deployment isiw --replicas=0
    oc scale deployment cusws --replicas=0
    oc scale deployment linws --replicas=0
    oc scale deployment linws --replicas=0
    oc scale deployment batchws --replicas=0
    oc scale deployment movlinws --replicas=0
    oc scale deployment clc --replicas=0
    oc scale deployment oca --replicas=0
    oc scale deployment cat --replicas=0
    oc scale deployment cob --replicas=0
    oc scale deployment inter --replicas=0
    oc scale deployment cerws --replicas=0
    oc scale deployment fms --replicas=0

    PAUSE
goto menu_principal

:submenu2
cls
    ECHO **************************************************
    ECHO *                 Iniciando todo                 *
    ECHO **************************************************

    ECHO INICIANDO: UDP, CATWS, CITIWS, SECWS e ISIWS
    oc scale deployment udp --replicas=1
    oc scale deployment catws --replicas=1
    oc scale deployment citiws --replicas=1
    oc scale deployment secws --replicas=1
    oc scale deployment isiw --replicas=1
    timeout 180 > NUL

    ECHO INICIANDO: CUSWS, LNSWS, LINWS, BATCHWS, MOVLINWS y CLC
    oc scale deployment cusws --replicas=1
    oc scale deployment lnsws --replicas=1
    oc scale deployment linws --replicas=1
    oc scale deployment batchws --replicas=1
    oc scale deployment movlinws --replicas=1
    oc scale deployment clc --replicas=1
    timeout 180 > NUL

    ECHO INICIANDO: OCA, CAT, COB, INTER, CERWS
    oc scale deployment oca --replicas=1
    oc scale deployment cat --replicas=1
    oc scale deployment cob --replicas=1
    oc scale deployment inter --replicas=1
    oc scale deployment cerws --replicas=1
    timeout 180 > NUL

    ECHO INICIANDO: WEBAPP, FMS
    oc scale deployment webapp --replicas=1
    oc scale deployment fms --replicas=1

    PAUSE
goto menu_principal

:submenu3
cls
    ECHO **************************************************
    ECHO *                Deteniendo línea                *
    ECHO **************************************************

    oc scale deployment webapp --replicas=0
    oc scale deployment udp --replicas=0
    oc scale deployment catws --replicas=0
    oc scale deployment citiws --replicas=0
    oc scale deployment secws --replicas=0
    oc scale deployment isiw --replicas=0
    oc scale deployment cusws --replicas=0
    oc scale deployment lnsws --replicas=0
    oc scale deployment linws --replicas=0
    oc scale deployment movlinws --replicas=0
    oc scale deployment clc --replicas=0
    oc scale deployment cerws --replicas=0
    oc scale deployment fms --replicas=0

    PAUSE
goto menu_principal

:submenu4
    cls
    ECHO **************************************************
    ECHO *                Iniciando línea                 *
    ECHO **************************************************

    ECHO INICIANDO: UDP, CATWS, WS, SECWS e ISIWS
    oc scale deployment udp --replicas=1
    oc scale deployment catws --replicas=1
    oc scale deployment ws --replicas=1
    oc scale deployment secws --replicas=1
    oc scale deployment isiw --replicas=1
    timeout 180 > NUL

    ECHO INICIANDO: CUSWS, LNSWS, LINWS, MOVLINWS y CLC
    oc scale deployment cusws --replicas=1
    oc scale deployment lnsws --replicas=1
    oc scale deployment linws --replicas=1
    oc scale deployment movlinws --replicas=1
    oc scale deployment clc --replicas=1
    timeout 180 > NUL

    ECHO INICIANDO: CERWS, WEBAPP, FMS
    oc scale deployment cerws --replicas=1
    oc scale deployment webapp --replicas=1
    oc scale deployment fms --replicas=1

    PAUSE
goto menu_principal

:submenu5
cls
    ECHO **************************************************
    ECHO *           Deteniendo batch ISILOANS            *
    ECHO **************************************************

    oc scale deployment batchws --replicas=0
    oc scale deployment oca --replicas=0
    oc scale deployment cat --replicas=0
    oc scale deployment cob --replicas=0
    oc scale deployment inter --replicas=0

    PAUSE
goto menu_principal

:submenu6
    cls
    ECHO **************************************************
    ECHO *                 Iniciando batch                *
    ECHO **************************************************

    ECHO INICIANDO: BATCHWS, OCA, CAT, COB e INTER
    oc scale deployment batchws --replicas=1
    oc scale deployment oca --replicas=1
    oc scale deployment cat --replicas=1
    oc scale deployment cob --replicas=1
    oc scale deployment inter --replicas=1

    PAUSE
goto menu_principal

:submenu7
cls
    ECHO **************************************************
    ECHO *                 Deteniendo CLC                 *
    ECHO **************************************************

    oc scale deployment udp --replicas=0
    oc scale deployment clc --replicas=0

    PAUSE
goto menu_principal

:submenu8
cls
    ECHO **************************************************
    ECHO *                 Iniciando CLC                  *
    ECHO **************************************************

    ECHO INICIANDO: UDP
    oc scale deployment udp --replicas=1
    timeout 180 > NUL

    ECHO INICIANDO: CLC
    oc scale deployment clc --replicas=1

    PAUSE
goto menu_principal

:submenu9
cls
    ECHO **************************************************
    ECHO *                 Deteniendo WEB                 *
    ECHO **************************************************

    oc scale deployment webapp --replicas=0

    AUSE
goto menu_principal

:submenu10
cls
    ECHO **************************************************
    ECHO *                 Iniciando WEB                  *
    ECHO **************************************************

    ECHO INICIANDO: WEB
    oc scale deployment webapp --replicas=1

    PAUSE
goto menu_principal

:submenu11
cls
    ECHO **************************************************
    ECHO *            Reiniciando Utilidades              *
    ECHO **************************************************

    oc scale deployment isi-utilidades-dev --replicas=0
    timeout 60 > NUL
    oc scale deployment isi-utilidades-dev --replicas=1

    PAUSE
goto menu_principal

:submenu12
cls
    ECHO **************************************************
    ECHO *             Validando Servicios                *
    ECHO **************************************************

    oc get pods

    PAUSE
goto menu_principal

:submenu13
cls
    ECHO **************************************************
    ECHO *             Limpiando Instalación              *
    ECHO **************************************************

    oc delete pods --field-selector=status.phase=Succeeded

    PAUSE
goto menu_principal

:submenu14
cls
    ECHO Regresando al menú de login...
    timeout 2 > NUL
goto menu_principal

:menu_principaldos
cls
    ECHO **************************************************
    ECHO *                     MENÚ                       *
    ECHO **************************************************
    ECHO *   1. Limpiar Compilacion                       *
    ECHO *   2. Regresar al Menú principal                *
    ECHO *   3. Salir Completamente                       *
    ECHO **************************************************

set /p opcion_menudos=Seleccione una opción (1-3):

rem Validación de entrada
if %opcion_menudos% lss 1 goto menu_principaldos
if %opcion_menudos% gtr 3 goto menu_principaldos

if %opcion_menudos%==1 goto submenusd1
if %opcion_menudos%==2 goto inicio
if %opcion_menudos%==3 goto salir

:submenudos1
cls
    ECHO **************************************************
    ECHO *            Limpiando Compilaciones             *
    ECHO **************************************************

    oc delete build --all

    PAUSE
goto menu_principaldos

:salir
cls
    ECHO **************************************************
    ECHO *                Saliendo, ¡Adios!               *
    ECHO **************************************************
timeout 2 > NUL
exit