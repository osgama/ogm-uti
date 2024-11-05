import os
import tarfile
import requests
from requests.auth import HTTPBasicAuth
from time import sleep

# Configuración de Artifactory
artifactory_url = "https://tuservidorartifactory.com/artifactory"
repo_name = "tu-repositorio"
base_path = "rutabase"  # Ruta base en Artifactory
username = "tu_usuario"
password = "tu_contraseña"

# Carpeta raíz que contiene `properties`, `sql`, `otros`
root_folder = "C:/ruta/a/la/raiz"

# Opciones de carpetas
folders_options = {
    "1": ["properties"],
    "2": ["sql"],
    "3": ["otros"],
    "4": ["properties", "sql"]
}

# Intentos máximos y tiempo de espera entre intentos en caso de fallo de red
MAX_RETRIES = 3
RETRY_DELAY = 5  # segundos

# Función para obtener la siguiente versión en Artifactory de forma segura con reintentos
def get_next_version(artifactory_url, repo_name, base_path, folder_name, username, password):
    url = f"{artifactory_url}/{repo_name}/{base_path}/{folder_name}/"
    attempts = 0

    while attempts < MAX_RETRIES:
        try:
            response = requests.get(url, auth=HTTPBasicAuth(username, password))
            
            if response.status_code == 200:
                # Obtener versiones existentes y calcular la próxima
                versions = [int(v[1:]) for v in response.json() if v.startswith("v") and v[1:].isdigit()]
                next_version = f"v{max(versions) + 1}" if versions else "v1"
                return next_version
            
            elif response.status_code == 404:
                # Si no existe la carpeta en Artifactory, comenzamos con v1
                return "v1"
            else:
                print(f"Error al obtener versiones para {folder_name}: {response.status_code} - {response.text}")
                attempts += 1
                sleep(RETRY_DELAY)  # Esperar antes de reintentar

        except requests.exceptions.RequestException as e:
            print(f"Problema de red al obtener versiones para {folder_name}: {e}")
            attempts += 1
            sleep(RETRY_DELAY)

    # Si todos los intentos fallan, no continuar y lanzar excepción
    raise Exception(f"No se pudo obtener la versión de {folder_name} después de {MAX_RETRIES} intentos. Abortando subida.")

# Paso 1: Crear un archivo .tar para cada carpeta seleccionada
def create_tar(folder_path, output_path):
    with tarfile.open(output_path, "w") as tar:
        tar.add(folder_path, arcname=os.path.basename(folder_path))
    print(f"Archivo .tar creado en {output_path}")

# Paso 2: Subir el archivo a Artifactory
def upload_to_artifactory(file_path, artifactory_url, repo_name, base_path, version, folder_name, username, password):
    file_name = os.path.basename(file_path)
    target_path = f"{artifactory_url}/{repo_name}/{base_path}/{folder_name}/{version}/{file_name}"

    with open(file_path, 'rb') as file_to_upload:
        response = requests.put(
            target_path,
            data=file_to_upload,
            auth=HTTPBasicAuth(username, password)
        )

    if response.status_code == 201:
        print(f"Archivo {file_path} subido exitosamente a {target_path}")
    else:
        print(f"Error al subir el archivo {file_name} a {target_path}: {response.status_code} - {response.text}")

# Paso 3: Empaquetar y subir las carpetas seleccionadas
def package_and_upload_folders(selection, root_folder, artifactory_url, repo_name, base_path, username, password):
    # Obtener carpetas según la selección
    folders_to_process = folders_options.get(selection, [])
    
    if not folders_to_process:
        print("Selección no válida. Usa valores entre 1 y 4.")
        return

    for folder_name in folders_to_process:
        folder_path = os.path.join(root_folder, folder_name)

        if not os.path.isdir(folder_path):
            print(f"La carpeta {folder_path} no existe. Saltando...")
            continue

        try:
            # Obtener la siguiente versión específica de la carpeta
            version = get_next_version(artifactory_url, repo_name, base_path, folder_name, username, password)
            
            # Definir el archivo .tar de salida
            output_tar_path = os.path.join(root_folder, f"{folder_name}.tar")

            # Crear el archivo .tar
            create_tar(folder_path, output_tar_path)

            # Subir el archivo .tar a Artifactory
            upload_to_artifactory(output_tar_path, artifactory_url, repo_name, base_path, version, folder_name, username, password)

            # Eliminar el archivo .tar local
            os.remove(output_tar_path)
            print(f"Archivo temporal {output_tar_path} eliminado.")
        
        except Exception as e:
            print(f"No se pudo procesar la carpeta {folder_name} debido a un error: {e}")

# Ejecución: solicita al usuario la selección de carpetas
selection = input("Ingresa el número de selección (1-4) para las carpetas a empaquetar y subir: ")
package_and_upload_folders(selection, root_folder, artifactory_url, repo_name, base_path, username, password)
