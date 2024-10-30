# Configura la salida en UTF-8 para manejar correctamente caracteres especiales en la consola
# Forzar la codificación de entrada y salida en UTF-8
$OutputEncoding = [System.Text.Encoding]::UTF8
$PSDefaultParameterValues['Out-File:Encoding'] = 'utf8'

# Forzar que la entrada de la consola use UTF-8
[Console]::InputEncoding = [System.Text.Encoding]::UTF8
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8

# Define un archivo de log global para todas las operaciones
$globalLogFile = "$PSScriptRoot\global_clone_log.txt"

# Añade un separador de operaciones con asteriscos
Add-Content -Path $globalLogFile -Value "**************************************************"
Add-Content -Path $globalLogFile -Value "$(Get-Date) - Iniciando nueva ejecucion del script."

# Obtiene la ruta donde esta ubicado el script
$scriptPath = $PSScriptRoot

# Obtiene la ruta del perfil del usuario actual (para Documentos\Codigo)
$userProfile = [System.Environment]::GetFolderPath('UserProfile')
$defaultDestinationPath = "$userProfile\Documents\Codigo"

# Guarda el directorio de inicio para volver a el despues de cada clonacion
$originalDirectory = Get-Location

# Lista de nombres de repositorios
$reposNames = @("dhalia", "camelia")

# Funcion para clonar repositorios en una rama especifica y en una ruta especificada
function ClonarRepositorios {
    param (
        [string]$repoName,
        [string]$destinationPath, # Ruta de destino personalizada o predeterminada
        [ref]$totalClonados,
        [ref]$totalEliminados,
        [ref]$totalErrores
    )

    try {
        # Definir la carpeta principal del repositorio y luego la subcarpeta de la rama
        $repoParentFolder = "$destinationPath\$repoName"
        $repoBranchFolder = "$repoParentFolder\$folderBranch"  # Se usa "dev", "uat", o "master" para la carpeta

        # Verifica si la carpeta de destino padre existe, si no, la crea
        if (-not (Test-Path -Path $repoParentFolder)) {
            Write-Host "**************************************************"
            Write-Host "*  Creando carpeta principal para $repoName...   *"
            Write-Host "**************************************************"
            New-Item -Path $repoParentFolder -ItemType Directory
            Add-Content -Path $globalLogFile -Value "$(Get-Date) - Carpeta principal '$repoParentFolder' creada."
        }

        # Si la subcarpeta de la rama ya existe, la eliminamos
        if (Test-Path -Path $repoBranchFolder) {
            Write-Host "**************************************************"
            Write-Host "*  La carpeta $repoBranchFolder ya existe, eliminando...  *"
            Write-Host "**************************************************" -ForegroundColor Yellow
            Remove-Item -Recurse -Force $repoBranchFolder
            Add-Content -Path $globalLogFile -Value "$(Get-Date) - Carpeta de rama $repoBranchFolder eliminada."
            $totalEliminados.Value++
        }

        # Crear la subcarpeta de la rama
        New-Item -Path $repoBranchFolder -ItemType Directory
        Add-Content -Path $globalLogFile -Value "$(Get-Date) - Carpeta de rama $repoBranchFolder creada."

        # Cambia el directorio a la carpeta de la rama para hacer el clon dentro
        Set-Location -Path $repoBranchFolder

        # Verifica si el archivo de repositorios existe en la misma ruta que el script
        $repoFile = "$scriptPath\repos_$repoName.txt"
        if (-not (Test-Path -Path $repoFile)) {
            throw "El archivo de repositorios $repoFile no existe."
        }
        if ((Get-Content $repoFile).Count -eq 0) {
            throw "El archivo de repositorios $repoFile esta vacio."
        }

        # Lee el archivo de repositorios y clona cada repositorio en la carpeta actual
        $reposUrls = Get-Content $repoFile
        foreach ($repoUrl in $reposUrls) {
            $repoNameInUrl = $repoUrl.Split("/")[-1].Replace(".git", "")  # Extrae el nombre del repo de la URL

            try {
                # Intenta clonar el repositorio en la rama especificada
                Write-Host "**************************************************"
                Write-Host "*  Clonando el repositorio: $repoNameInUrl en la rama $branch  *"
                Write-Host "**************************************************" -ForegroundColor Cyan

                git clone -b $branch $repoUrl
                if ($LASTEXITCODE -ne 0) {
                    Write-Host "**************************************************"
                    Write-Host "*  Rama $branch no encontrada. Clonando master por defecto  *"
                    Write-Host "**************************************************" -ForegroundColor Yellow
                    git clone -b master $repoUrl
                }
                $totalClonados.Value++
                Add-Content -Path $globalLogFile -Value "$(Get-Date) - Repositorio $repoNameInUrl clonado en la rama $branch."
            }
            catch {
                Write-Host "**************************************************"
                Write-Host "*  Error al clonar el repositorio: $repoNameInUrl  *"
                Write-Host "**************************************************" -ForegroundColor Red
                Add-Content -Path $globalLogFile -Value "$(Get-Date) - Error al clonar $repoNameInUrl en la rama $branch ${_}"
                $totalErrores.Value++
            }
        }

        # Vuelve al directorio original
        Set-Location -Path $originalDirectory
    }
    catch {
        # Captura cualquier error general y proporciona detalles adicionales
        Write-Host "**************************************************"
        Write-Host "*  Ocurrio un error en el script: $_  *" -ForegroundColor Red
        Write-Host "*  Detalles del error:                            *"
        Write-Host "*  Mensaje: $($_.Exception.Message)               *"
        Write-Host "*  Linea: $($_.InvocationInfo.ScriptLineNumber)   *"
        Write-Host "*  Archivo: $($_.InvocationInfo.ScriptName)       *"
        Write-Host "**************************************************"
        
        # Guardar en logs el detalle del error
        Add-Content -Path $globalLogFile -Value "$(Get-Date) - Error en ClonarRepositorios: $_"
        Add-Content -Path $globalLogFile -Value "Mensaje: $($_.Exception.Message)"
        Add-Content -Path $globalLogFile -Value "Linea: $($_.InvocationInfo.ScriptLineNumber)"
        Add-Content -Path $globalLogFile -Value "Archivo: $($_.InvocationInfo.ScriptName)"
        
        $totalErrores.Value++
    }
}

# Funcion para mostrar el resumen separado por repositorio
function MostrarResumenPorRepo {
    param (
        [string]$repoName,
        [ref]$totalClonados,
        [ref]$totalEliminados,
        [ref]$totalErrores
    )

    # Mostrar en consola
    Write-Host "**************************************************"
    Write-Host "*          Resumen de la operacion para $repoName   *"
    Write-Host "**************************************************"
    Write-Host "*  Repositorios clonados: $($totalClonados.Value)                   *"
    Write-Host "*  Carpetas eliminadas: $($totalEliminados.Value)                   *"
    Write-Host "*  Errores encontrados: $($totalErrores.Value)                      *"
    Write-Host "**************************************************" -ForegroundColor Green

    # Guardar el resumen en los logs
    Add-Content -Path $globalLogFile -Value "**************************************************"
    Add-Content -Path $globalLogFile -Value "$(Get-Date) - Resumen para '$repoName':"
    Add-Content -Path $globalLogFile -Value "Repositorios clonados: $($totalClonados.Value)"
    Add-Content -Path $globalLogFile -Value "Carpetas eliminadas: $($totalEliminados.Value)"
    Add-Content -Path $globalLogFile -Value "Errores encontrados: $($totalErrores.Value)"
    Add-Content -Path $globalLogFile -Value "**************************************************"
}

# Bucle principal del menu
while ($true) {
    try {
        # Muestra el menu de opciones con asteriscos bien alineados
        Write-Host "**************************************************"
        Write-Host "*  Selecciona una opcion para clonar o descargar repositorios  *"
        Write-Host "**************************************************"
        Write-Host "*  1. Clonar repositorios en 'dhalia'                        *"
        Write-Host "*  2. Clonar repositorios en 'camelia'                       *"
        Write-Host "*  3. Clonar todos los repositorios (dhalia y camelia)       *"
        Write-Host "*  4. Descargar ambos repositorios en carpeta personalizada  *"
        Write-Host "*  5. Restablecer logs (borrar contenido de log)             *"
        Write-Host "*  6. Salir                                                  *"
        Write-Host "**************************************************"
        $opcion = Read-Host "Introduce una opcion"

        # Solo seleccionamos la rama si vamos a clonar repositorios
        if ($opcion -eq "1" -or $opcion -eq "2" -or $opcion -eq "3" -or $opcion -eq "4") {
            # Menu de seleccion de rama
            Write-Host "**************************************************"
            Write-Host "*  Selecciona la rama que deseas clonar:          *"
            Write-Host "**************************************************"
            Write-Host "*  1. master                                      *"
            Write-Host "*  2. release/dev                                 *"
            Write-Host "*  3. release/uat                                 *"
            Write-Host "**************************************************"
            $ramaSeleccionada = Read-Host "Introduce una opcion (1-3)"

            # Asigna la rama seleccionada segun la entrada del usuario
            switch ($ramaSeleccionada) {
                "1" { $branch = "master"; $folderBranch = "master" }
                "2" { $branch = "release/dev"; $folderBranch = "dev" }    # Usamos "release/dev" para Git, pero "dev" para la carpeta
                "3" { $branch = "release/uat"; $folderBranch = "uat" }    # Usamos "release/uat" para Git, pero "uat" para la carpeta
                default {
                    Write-Host "Opcion no valida. Se seleccionara 'master' por defecto."
                    $branch = "master"
                    $folderBranch = "master"
                }
            }
        }

        # Restablecemos los contadores para cada operacion
        $totalClonados = [ref]0
        $totalEliminados = [ref]0
        $totalErrores = [ref]0

        # Validacion de la opcion ingresada
        if ($opcion -eq "1" -or $opcion -eq "2") {
            # Clona en la ruta predeterminada
            ClonarRepositorios -repoName $reposNames[$opcion - 1] -destinationPath $defaultDestinationPath `
                               -totalClonados $totalClonados -totalEliminados $totalEliminados -totalErrores $totalErrores
        } elseif ($opcion -eq "3") {
            # Clona ambos repositorios en la ruta predeterminada
            foreach ($repo in $reposNames) {
                ClonarRepositorios -repoName $repo -destinationPath $defaultDestinationPath `
                                   -totalClonados $totalClonados -totalEliminados $totalEliminados -totalErrores $totalErrores
            }
        } elseif ($opcion -eq "4") {
            # Solicita el nombre de la subcarpeta al usuario
            $nombreCarpeta = Read-Host "Introduce el nombre de la carpeta"
            $rutaPersonalizada = "$defaultDestinationPath\$nombreCarpeta"

            # Descarga ambos repositorios automaticamente en la carpeta personalizada
            foreach ($repo in $reposNames) {
                ClonarRepositorios -repoName $repo -destinationPath $rutaPersonalizada `
                                   -totalClonados $totalClonados -totalEliminados $totalEliminados -totalErrores $totalErrores
            }
        } elseif ($opcion -eq "5") {
            # Restablecer logs (borrar todo el contenido del log)
            Clear-Content -Path $globalLogFile
            Write-Host "**************************************************"
            Write-Host "*  Los logs han sido restablecidos.              *"
            Write-Host "**************************************************"
            Add-Content -Path $globalLogFile -Value "$(Get-Date) - Logs restablecidos."
        } elseif ($opcion -eq "6") {
            Write-Host "Saliendo del script..." -ForegroundColor Yellow
            Add-Content -Path $globalLogFile -Value "$(Get-Date) - Script finalizado."
            break  # Salir del bucle
        } else {
            Write-Host "* Opcion no valida. Elige 1, 2, 3, 4, 5 o 6. *" -ForegroundColor Red
        }

    } catch {
        # Maneja cualquier error general del bucle principal
        Write-Host "**************************************************"
        Write-Host "*  Error general: $_  *" -ForegroundColor Red
        Write-Host "*  Revise los logs para mas detalles.             *"
        Write-Host "**************************************************"
        Add-Content -Path $globalLogFile -Value "$(Get-Date) - Error en bucle principal: $_"
    }

    # Agregar un espacio antes de volver a mostrar el menu
    Write-Host "`n"
}
