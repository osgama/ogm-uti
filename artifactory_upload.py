import os
import tarfile
import requests
from requests.auth import HTTPBasicAuth
from time import sleep

# Configuración de Artifactory
artifactory_url = "https://tuservidorartifactory.com/artifactory"
repo_name = "tu-repositorio"
base_path = "rutabase"
username_artifactory = "tu_usuario_artifactory"
password_artifactory = "tu_contraseña_artifactory"

# Configuración de Udeploy
udeploy_url = "https://tuservidorudeploy.com"
udeploy_component = "nombre_del_componente_en_udeploy"
username_udeploy = "tu_usuario_udeploy"
password_udeploy = "tu_contraseña_udeploy"

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

# Función para empaquetar la carpeta en un archivo .tar
def create_tar(folder_path, output_path):
    with tarfile.open(output_path, "w") as tar:
        tar.add(folder_path, arcname=os.path.basename(folder_path))
    print(f"Archivo .tar creado en {output_path}")

# Función para subir el archivo a Artifactory
def upload_to_artifactory(file_path, artifactory_url, repo_name, base_path, version, folder_name):
    file_name = os.path.basename(file_path)
    target_path = f"{artifactory_url}/{repo_name}/{base_path}/{folder_name}/{version}/{file_name}"

    with open(file_path, 'rb') as file_to_upload:
        response = requests.put(
            target_path,
            data=file_to_upload,
            auth=HTTPBasicAuth(username_artifactory, password_artifactory)
        )

    if response.status_code == 201:
        print(f"Archivo {file_path} subido exitosamente a {target_path}")
        return target_path  # Devuelve la URL del archivo en Artifactory
    else:
        print(f"Error al subir el archivo: {response.status_code} - {response.text}")
        return None

# Función para importar la versión en Udeploy
def import_version_to_udeploy(component, version_name, artifactory_path):
    url = f"{udeploy_url}/cli/version/importVersion"
    headers = {'Content-Type': 'application/json'}
    data = {
        "component": component,
        "version": version_name,
        "sourceConfigPlugin": "Artifactory",
        "properties": {
            "artifactPath": artifactory_path  # Ruta completa del artefacto en Artifactory
        }
    }

    response = requests.post(url, json=data, headers=headers, auth=HTTPBasicAuth(username_udeploy, password_udeploy))
    if response.status_code == 200:
        print(f"Versión {version_name} importada correctamente en Udeploy.")
    else:
        print(f"Error al importar la versión en Udeploy: {response.status_code} - {response.text}")

# Función principal para empaquetar, subir e importar la versión
def package_and_upload_folders(selection):
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
            version = get_next_version(artifactory_url, repo_name, base_path, folder_name, username_artifactory, password_artifactory)
            
            # Definir el archivo .tar de salida
            output_tar_path = os.path.join(os.getcwd(), f"{folder_name}.tar")

            # Crear el archivo .tar
            create_tar(folder_path, output_tar_path)

            # Subir el archivo .tar a Artifactory
            artifactory_path = upload_to_artifactory(output_tar_path, artifactory_url, repo_name, base_path, version, folder_name)
            if not artifactory_path:
                print("Error en la subida a Artifactory, abortando.")
                continue

            # Importar la versión en Udeploy
            import_version_to_udeploy(udeploy_component, version, artifactory_path)

            # Eliminar el archivo .tar local
            os.remove(output_tar_path)
            print(f"Archivo temporal {output_tar_path} eliminado.")
        
        except Exception as e:
            print(f"No se pudo procesar la carpeta {folder_name} debido a un error: {e}")

# Ejecución del script
selection = input("Ingresa el número de selección (1-4) para las carpetas a empaquetar, subir y registrar en Udeploy: ")
package_and_upload_folders(selection)
