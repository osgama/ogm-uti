@ECHO OFF
CHCP 65001 > NUL

:menu_inicio
CLS
ECHO * * * * * * * * * * * * * * * * * * * * * * * * * *
ECHO *               Menú de Inicio                    *
ECHO * * * * * * * * * * * * * * * * * * * * * * * * * *
ECHO * 1. Servidor DEV                                 *
ECHO * 2. Servidor UAT                                 *
ECHO * 3. Salir                                        *
ECHO * * * * * * * * * * * * * * * * * * * * * * * * * *
CHOICE /c 123 /n /m "Seleccione una opción: "

REM Captura el valor seleccionado en la variable "seleccion"
SET seleccion=%errorlevel%

IF %seleccion%==1 GOTO servidor
IF %seleccion%==2 GOTO servidor
IF %seleccion%==3 GOTO salir
ECHO Opción no válida. Por favor, seleccione una opción válida.
PAUSE
GOTO menu_inicio

:servidor
CLS
IF %seleccion%==1 (
    ECHO * * * * * * * * * * * * * * * * * * * * * * * * * * *
    ECHO *             Conectándose al Servidor DEV          *
    ECHO * * * * * * * * * * * * * * * * * * * * * * * * * * *
    REM Agrega aquí el código para conectarse al servidor DEV
) ELSE (
    ECHO * * * * * * * * * * * * * * * * * * * * * * * * * * *
    ECHO *             Conectándose al Servidor UAT          *
    ECHO * * * * * * * * * * * * * * * * * * * * * * * * * * *
    REM Agrega aquí el código para conectarse al servidor UAT
)

:submenuserver
CLS
ECHO * * * * * * * * * * * * * * * * * * * * * * * * * * * *
ECHO *                    Start/Stop                       *
ECHO * * * * * * * * * * * * * * * * * * * * * * * * * * * *
ECHO * 1. Detener todo                                     *
ECHO * 2. Detener CLC                                      *
ECHO * 3. Iniciar todo                                     *
ECHO * 4. Iniciar CLC                                      *
ECHO * 5. Validar Servicios                                *
ECHO * 6. Regresar al Menú principal                       *
ECHO * * * * * * * * * * * * * * * * * * * * * * * * * * * *
CHOICE /c 123456 /n /m "Seleccione una opción: "
SET seleccion=%errorlevel%

IF %seleccion%==1 (
    ECHO * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    ECHO *             Deteniendo todo en el Servidor          *
    ECHO * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    REM Agrega aquí el código para detener todo en el servidor
    PAUSE
    GOTO submenuserver
)
IF %seleccion%==2 (
    ECHO * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    ECHO *             Deteniendo CLC en el Servidor           *
    ECHO * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    REM Agrega aquí el código para detener CLC en el servidor
    PAUSE
    GOTO submenuserver
)
IF %seleccion%==3 (
    ECHO * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    ECHO *             Iniciando todo en el Servidor           *
    ECHO * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    REM Agrega aquí el código para iniciar todo en el servidor
    PAUSE
    GOTO submenuserver
)
IF %seleccion%==4 (
    ECHO * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    ECHO *             Iniciando CLC en el Servidor            *
    ECHO * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    REM Agrega aquí el código para iniciar CLC en el servidor
    PAUSE
    GOTO submenuserver
)
IF %seleccion%==5 (
    ECHO * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    ECHO *                Validando Servicios                  *
    ECHO * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    REM Agrega aquí el código para Validar Servicios 
    PAUSE
    GOTO submenuserver
)
IF %seleccion%==6 GOTO menu_inicio
ECHO Opción no válida. Por favor, seleccione una opción válida.
PAUSE
GOTO submenuserver

:salir
CLS
ECHO * * * * * * * * * * * * * * * * * * * * * * * * * * * *
ECHO *                 Saliendo, ¡Adios!                   *
ECHO * * * * * * * * * * * * * * * * * * * * * * * * * * * *
EXIT /b


    0 = Negro       8 = Gris
    1 = Azul        9 = Azul claro
    2 = Verde       A = Verde claro
    3 = Aguamarina  B = Aguamarina claro
    4 = Rojo        C = Rojo claro
    5 = Púrpura     D = Púrpura claro
    6 = Amarillo    E = Amarillo claro
    7 = Blanco      F = Blanco brillante