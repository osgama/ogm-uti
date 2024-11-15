# Mapeo de nombres antiguos a nombres nuevos
$renameMap = @{
    "clasica.css"               = "neutral-light-v1.css"
    "clasicav1.css"             = "neutral-light-v2.css"
    "clasicav2.css"             = "neutral-light-v3.css"
    "clasicav3.css"             = "neutral-light-v4.css"
    "clasicav4.css"             = "neutral-light-muted-v1.css"
    "clasicav5.css"             = "neutral-light-muted-v2.css"
    "clasicav6.css"             = "neutral-light-muted-v3.css"
    "clasicav7.css"             = "neutral-bright-accent-v1.css"
    "clasicav8.css"             = "neutral-bright-accent-v2.css"
}

# Renombrar los archivos en el directorio actual
foreach ($oldName in $renameMap.Keys) {
    $newName = $renameMap[$oldName]
    if (Test-Path $oldName) {
        Rename-Item -Path $oldName -NewName $newName
        Write-Host "Renombrado: $oldName -> $newName"
    } else {
        Write-Host "Archivo no encontrado: $oldName"
    }
}
