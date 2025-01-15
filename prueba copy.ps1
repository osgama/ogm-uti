# Configuración de salida en UTF-8 para caracteres especiales
$OutputEncoding = [System.Text.Encoding]::UTF8
$PSDefaultParameterValues['Out-File:Encoding'] = 'utf8'
[Console]::InputEncoding = [System.Text.Encoding]::UTF8
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8

# Definir archivo global de log
$globalLogFile = "$PSScriptRoot\global_clone_log.txt"

# Función para mostrar una línea divisoria de 80 asteriscos
function MostrarLinea {
    Write-Host ("*" * 80)
}

# Función para mostrar un mensaje sin bordes adicionales
function MostrarMensaje {
    param (
        [string]$mensaje1,
        [string]$mensaje2 = "",
        [string]$mensaje3 = ""
    )
    MostrarLinea
    Write-Host $mensaje1
    if ($mensaje2 -ne "") { Write-Host $mensaje2 }
    if ($mensaje3 -ne "") { Write-Host $mensaje3 }
    MostrarLinea
    Write-Host "`n"  # Agrega una línea en blanco después del mensaje
}

# Bienvenida
MostrarMensaje -mensaje1 "Bienvenido al script de clonación"

# Añade un separador de operaciones en el log
Add-Content -Path $globalLogFile -Value ("*" * 80)
Add-Content -Path $globalLogFile -Value "$(Get-Date) - Iniciando nueva ejecución del script."
Write-Host "`n"  # Agrega un espacio después de la bienvenida

# Obtiene la ruta donde está ubicado el script y la ruta de destino
$scriptPath = $PSScriptRoot
$userProfile = [System.Environment]::GetFolderPath('UserProfile')
$defaultDestinationPath = "$userProfile\Documents\Codigo"
$originalDirectory = Get-Location

# Nombre del repositorio por defecto
$repoName = "camelia"

# Función para clonar repositorios
function ClonarRepositorios {
    param (
        [string]$repoName,
        [string]$destinationPath,
        [ref]$totalClonados,
        [ref]$totalEliminados,
        [ref]$totalErrores
    )

    try {
        $repoParentFolder = "$destinationPath\$repoName"
        $repoBranchFolder = "$repoParentFolder\$folderBranch"

        # Crear carpeta principal si no existe
        if (-not (Test-Path -Path $repoParentFolder)) {
            MostrarMensaje -mensaje1 "Creando carpeta principal para $repoName..."
            New-Item -Path $repoParentFolder -ItemType Directory
            Add-Content -Path $globalLogFile -Value "$(Get-Date) - Carpeta principal '$repoParentFolder' creada."
            Write-Host "`n"  # Espacio después de crear la carpeta
        }

        # Borrar carpeta de rama si existe
        if (Test-Path -Path $repoBranchFolder) {
            MostrarMensaje -mensaje1 "La carpeta $repoBranchFolder ya existe, eliminando..."
            Remove-Item -Recurse -Force $repoBranchFolder
            Add-Content -Path $globalLogFile -Value "$(Get-Date) - Carpeta de rama $repoBranchFolder eliminada."
            $totalEliminados.Value++
            Write-Host "`n"  # Espacio después de eliminar la carpeta
        }

        # Crear carpeta de rama
        New-Item -Path $repoBranchFolder -ItemType Directory
        Add-Content -Path $globalLogFile -Value "$(Get-Date) - Carpeta de rama $repoBranchFolder creada."
        Set-Location -Path $repoBranchFolder
        Write-Host "`n"  # Espacio después de crear la carpeta de la rama

        # Verificar existencia y contenido de archivo de repositorios
        $repoFile = "$scriptPath\repos_$repoName.txt"
        if (-not (Test-Path -Path $repoFile)) {
            throw "El archivo de repositorios $repoFile no existe."
        }
        if ((Get-Content $repoFile).Count -eq 0) {
            throw "El archivo de repositorios $repoFile esta vacio."
        }

        # Leer y clonar repositorios
        $reposUrls = Get-Content $repoFile
        foreach ($repoUrl in $reposUrls) {
            $repoNameInUrl = $repoUrl.Split("/")[-1].Replace(".git", "")
            $progress = [math]::Round(($reposUrls.IndexOf($repoUrl) + 1) / $reposUrls.Count * 100)
            Write-Progress -Activity "Clonando repositorios" -Status ("{0} ({1}% completado)" -f $repoNameInUrl, $progress) -PercentComplete $progress

            try {
                MostrarMensaje -mensaje1 "Clonando el repositorio:" -mensaje2 "$repoNameInUrl en la rama $branch"
                git clone -b $branch $repoUrl

                if ($LASTEXITCODE -ne 0) {
                    MostrarMensaje -mensaje1 "Rama $branch no encontrada. Clonando master por defecto"
                    git clone -b master $repoUrl
                }
                $totalClonados.Value++
                Add-Content -Path $globalLogFile -Value "$(Get-Date) - Repositorio $repoNameInUrl clonado en la rama $branch."
            }
            catch {
                MostrarMensaje -mensaje1 "Error al clonar el repositorio:" -mensaje2 "$repoNameInUrl"
                Add-Content -Path $globalLogFile -Value "$(Get-Date) - Error al clonar $repoNameInUrl en la rama $branch ${_}"
                $totalErrores.Value++
            }
        }
        Set-Location -Path $originalDirectory
    }
    catch {
        MostrarMensaje -mensaje1 "Ocurrio un error en el script:" -mensaje2 "Detalles del error:" -mensaje3 "Mensaje: $($_.Exception.Message), Línea: $($_.InvocationInfo.ScriptLineNumber), Archivo: $($_.InvocationInfo.ScriptName)"
        Add-Content -Path $globalLogFile -Value "$(Get-Date) - Error en ClonarRepositorios: $_"
        Add-Content -Path $globalLogFile -Value "Mensaje: $($_.Exception.Message)"
        Add-Content -Path $globalLogFile -Value "Linea: $($_.InvocationInfo.ScriptLineNumber)"
        Add-Content -Path $globalLogFile -Value "Archivo: $($_.InvocationInfo.ScriptName)"
        $totalErrores.Value++
    }
    Write-Host "`n"  # Espacio después de finalizar la clonación
}

# Inicia el flujo del script directamente con selección de rama
try {
    MostrarMensaje -mensaje1 "Selecciona la rama que deseas clonar"
    Write-Host "1. master" -ForegroundColor White
    Write-Host "2. release/dev" -ForegroundColor White
    Write-Host "3. release/uat" -ForegroundColor White
    MostrarLinea
    $ramaSeleccionada = Read-Host "Introduce una opción (1-3)"

    switch ($ramaSeleccionada) {
        "1" { $branch = "master"; $folderBranch = "master" }
        "2" { $branch = "release/dev"; $folderBranch = "dev" }
        "3" { $branch = "release/uat"; $folderBranch = "uat" }
        default { $branch = "master"; $folderBranch = "master" }
    }

    $totalClonados = [ref]0; $totalEliminados = [ref]0; $totalErrores = [ref]0
    ClonarRepositorios -repoName $repoName -destinationPath $defaultDestinationPath `
                       -totalClonados $totalClonados -totalEliminados $totalEliminados -totalErrores $totalErrores

    MostrarMensaje -mensaje1 "Resumen de la operación"
    Write-Host "Repositorios clonados: $($totalClonados.Value)" -ForegroundColor Green
    Write-Host "Carpetas eliminadas: $($totalEliminados.Value)" -ForegroundColor Yellow
    Write-Host "Errores encontrados: $($totalErrores.Value)" -ForegroundColor Red
} catch {
    MostrarMensaje -mensaje1 "Error general:" -mensaje2 "$_"
    Add-Content -Path $globalLogFile -Value "$(Get-Date) - Error en flujo principal: $_"
}