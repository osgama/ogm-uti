# Configura la salida en UTF-8 para manejar correctamente caracteres especiales en la consola
$OutputEncoding = [System.Text.Encoding]::UTF8
$PSDefaultParameterValues['Out-File:Encoding'] = 'utf8'
[Console]::InputEncoding = [System.Text.Encoding]::UTF8
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8

# Define un archivo de log global para todas las operaciones
$globalLogFile = "$PSScriptRoot\global_clone_log.txt"

# Mensaje de bienvenida
Write-Host "╔════════════════════════════════════════════════╗" -ForegroundColor Blue
Write-Host "║       Bienvenido al script de clonacion        ║" -ForegroundColor Blue
Write-Host "╠════════════════════════════════════════════════╣" -ForegroundColor Blue

# Añade un separador de operaciones con asteriscos en el log
Add-Content -Path $globalLogFile -Value "**************************************************"
Add-Content -Path $globalLogFile -Value "$(Get-Date) - Iniciando nueva ejecucion del script."

# Obtiene la ruta donde esta ubicado el script y la ruta de destino
$scriptPath = $PSScriptRoot
$userProfile = [System.Environment]::GetFolderPath('UserProfile')
$defaultDestinationPath = "$userProfile\Documents\Codigo"
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
        $repoParentFolder = "$destinationPath\$repoName"
        $repoBranchFolder = "$repoParentFolder\$folderBranch"  # Se usa "dev", "uat", o "master" para la carpeta

        # Verifica si la carpeta de destino padre existe, si no, la crea
        if (-not (Test-Path -Path $repoParentFolder)) {
            Write-Host "**************************************************" -ForegroundColor Cyan
            Write-Host "*  Creando carpeta principal para $repoName...   *" -ForegroundColor Cyan
            Write-Host "**************************************************" -ForegroundColor Cyan
            New-Item -Path $repoParentFolder -ItemType Directory
            Add-Content -Path $globalLogFile -Value "$(Get-Date) - Carpeta principal '$repoParentFolder' creada."
        }

        # Si la subcarpeta de la rama ya existe, la eliminamos
        if (Test-Path -Path $repoBranchFolder) {
            Write-Host "**************************************************" -ForegroundColor Yellow
            Write-Host "*  La carpeta $repoBranchFolder ya existe, eliminando...  *" -ForegroundColor Yellow
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
            $progress = [math]::Round(($reposUrls.IndexOf($repoUrl) + 1) / $reposUrls.Count * 100)
            Write-Progress -Activity "Clonando repositorios" -Status "$repoNameInUrl ($progress% completado)" -PercentComplete $progress

            try {
                Write-Host "**************************************************" -ForegroundColor Cyan
                Write-Host "*  Clonando el repositorio: $repoNameInUrl en la rama $branch  *" -ForegroundColor Cyan
                Write-Host "**************************************************" -ForegroundColor Cyan

                git clone -b $branch $repoUrl
                if ($LASTEXITCODE -ne 0) {
                    Write-Host "**************************************************" -ForegroundColor Yellow
                    Write-Host "*  Rama $branch no encontrada. Clonando master por defecto  *" -ForegroundColor Yellow
                    Write-Host "**************************************************" -ForegroundColor Yellow
                    git clone -b master $repoUrl
                }
                $totalClonados.Value++
                Add-Content -Path $globalLogFile -Value "$(Get-Date) - Repositorio $repoNameInUrl clonado en la rama $branch."
            }
            catch {
                Write-Host "**************************************************" -ForegroundColor Red
                Write-Host "*  Error al clonar el repositorio: $repoNameInUrl  *" -ForegroundColor Red
                Write-Host "**************************************************" -ForegroundColor Red
                Add-Content -Path $globalLogFile -Value "$(Get-Date) - Error al clonar $repoNameInUrl en la rama $branch ${_}"
                $totalErrores.Value++
            }
        }
        Set-Location -Path $originalDirectory
    }
    catch {
        # Detalles del error general en ClonarRepositorios
        Write-Host "**************************************************" -ForegroundColor Red
        Write-Host "*  Ocurrio un error en el script: $_  *" -ForegroundColor Red
        Write-Host "*  Detalles del error:                            *" -ForegroundColor Red
        Write-Host "*  Mensaje: $($_.Exception.Message)               *" -ForegroundColor Red
        Write-Host "*  Linea: $($_.InvocationInfo.ScriptLineNumber)   *" -ForegroundColor Red
        Write-Host "*  Archivo: $($_.InvocationInfo.ScriptName)       *" -ForegroundColor Red
        Write-Host "**************************************************" -ForegroundColor Red

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
    Write-Host "╔════════════════════════════════════════════════╗" -ForegroundColor Green
    Write-Host "*        Resumen de la operacion para $repoName        *" -ForegroundColor Green
    Write-Host "╠════════════════════════════════════════════════╣" -ForegroundColor Green
    Write-Host "*  Repositorios clonados: $($totalClonados.Value)                 *" -ForegroundColor Green
    Write-Host "*  Carpetas eliminadas: $($totalEliminados.Value)                 *" -ForegroundColor Yellow
    Write-Host "*  Errores encontrados: $($totalErrores.Value)                    *" -ForegroundColor Red
    Write-Host "╚════════════════════════════════════════════════╝" -ForegroundColor Green

    Add-Content -Path $globalLogFile -Value "**************************************************"
    Add-Content -Path $globalLogFile -Value "$(Get-Date) - Resumen para '$repoName':"
    Add-Content -Path $globalLogFile -Value "Repositorios clonados: $($totalClonados.Value)"
    Add-Content -Path $globalLogFile -Value "Carpetas eliminadas: $($totalEliminados.Value)"
    Add-Content -Path $globalLogFile -Value "Errores encontrados: $($totalErrores.Value)"
}

# Bucle principal del menu
while ($true) {
    try {
        Write-Host "╔════════════════════════════════════════════════╗" -ForegroundColor Blue
        Write-Host "*  Selecciona una opcion para clonar o descargar repositorios  *" -ForegroundColor Blue
        Write-Host "╠════════════════════════════════════════════════╣" -ForegroundColor Blue
        Write-Host "*  1. Clonar repositorios en 'dhalia'                        *" -ForegroundColor White
        Write-Host "*  2. Clonar repositorios en 'camelia'                       *" -ForegroundColor White
        Write-Host "*  3. Clonar todos los repositorios (dhalia y camelia)       *" -ForegroundColor White
        Write-Host "*  4. Descargar ambos repositorios en carpeta personalizada  *" -ForegroundColor White
        Write-Host "*  5. Restablecer logs (borrar contenido de log)             *" -ForegroundColor White
        Write-Host "*  6. Salir                                                  *" -ForegroundColor White
        Write-Host "╚════════════════════════════════════════════════╝" -ForegroundColor Blue
        $opcion = Read-Host "Introduce una opcion"

        if ($opcion -in @("1", "2", "3", "4")) {
            Write-Host "╔════════════════════════════════════════════════╗" -ForegroundColor Blue
            Write-Host "*  Selecciona la rama que deseas clonar:          *" -ForegroundColor Blue
            Write-Host "╠════════════════════════════════════════════════╣" -ForegroundColor Blue
            Write-Host "*  1. master                                      *" -ForegroundColor White
            Write-Host "*  2. release/dev                                 *" -ForegroundColor White
            Write-Host "*  3. release/uat                                 *" -ForegroundColor White
            Write-Host "╚════════════════════════════════════════════════╝" -ForegroundColor Blue
            $ramaSeleccionada = Read-Host "Introduce una opcion (1-3)"

            switch ($ramaSeleccionada) {
                "1" { $branch = "master"; $folderBranch = "master" }
                "2" { $branch = "release/dev"; $folderBranch = "dev" }
                "3" { $branch = "release/uat"; $folderBranch = "uat" }
                default { $branch = "master"; $folderBranch = "master" }
            }
        }

        $totalClonados = [ref]0; $totalEliminados = [ref]0; $totalErrores = [ref]0

        if ($opcion -eq "1" -or $opcion -eq "2") {
            ClonarRepositorios -repoName $reposNames[$opcion - 1] -destinationPath $defaultDestinationPath `
                               -totalClonados $totalClonados -totalEliminados $totalEliminados -totalErrores $totalErrores
            MostrarResumenPorRepo -repoName $reposNames[$opcion - 1] -totalClonados $totalClonados `
                                   -totalEliminados $totalEliminados -totalErrores $totalErrores
        } elseif ($opcion -eq "3") {
            foreach ($repo in $reposNames) {
                ClonarRepositorios -repoName $repo -destinationPath $defaultDestinationPath `
                                   -totalClonados $totalClonados -totalEliminados $totalEliminados -totalErrores $totalErrores
                MostrarResumenPorRepo -repoName $repo -totalClonados $totalClonados `
                                       -totalEliminados $totalEliminados -totalErrores $totalErrores
            }
        } elseif ($opcion -eq "4") {
            $nombreCarpeta = Read-Host "Introduce el nombre de la carpeta"
            $rutaPersonalizada = "$defaultDestinationPath\$nombreCarpeta"
            foreach ($repo in $reposNames) {
                ClonarRepositorios -repoName $repo -destinationPath $rutaPersonalizada `
                                   -totalClonados $totalClonados -totalEliminados $totalEliminados -totalErrores $totalErrores
                MostrarResumenPorRepo -repoName $repo -totalClonados $totalClonados `
                                       -totalEliminados $totalEliminados -totalErrores $totalErrores
            }
        } elseif ($opcion -eq "5") {
            Clear-Content -Path $globalLogFile
            Write-Host "**************************************************" -ForegroundColor Yellow
            Write-Host "*  Los logs han sido restablecidos.              *" -ForegroundColor Yellow
            Write-Host "**************************************************" -ForegroundColor Yellow
            Add-Content -Path $globalLogFile -Value "$(Get-Date) - Logs restablecidos."
        } elseif ($opcion -eq "6") {
            Write-Host "╔════════════════════════════════════════════════╗" -ForegroundColor Yellow
            Write-Host "*           Gracias por usar el script.          *" -ForegroundColor Yellow
            Write-Host "*              Hasta luego!                      *" -ForegroundColor Yellow
            Write-Host "╚════════════════════════════════════════════════╝" -ForegroundColor Yellow
            Add-Content -Path $globalLogFile -Value "$(Get-Date) - Script finalizado."
            break
        } else {
            Write-Host "* Opcion no valida. Elige 1, 2, 3, 4, 5 o 6. *" -ForegroundColor Red
        }
    }
    catch {
        Write-Host "**************************************************" -ForegroundColor Red
        Write-Host "*  Error general: $_  *" -ForegroundColor Red
        Write-Host "*  Revise los logs para mas detalles.             *" -ForegroundColor Red
        Write-Host "**************************************************" -ForegroundColor Red
        Add-Content -Path $globalLogFile -Value "$(Get-Date) - Error en bucle principal: $_"
    }
    Write-Host "`n"
}
