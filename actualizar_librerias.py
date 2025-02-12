import subprocess
import pkg_resources

def actualizar_librerias():
    # Obtener la lista de todas las librerías instaladas
    paquetes = [dist.project_name for dist in pkg_resources.working_set]
    
    for paquete in paquetes:
        try:
            print(f"Actualizando {paquete}...")
            subprocess.check_call(["pip", "install", "--upgrade", paquete])
        except subprocess.CalledProcessError as e:
            print(f"Error al actualizar {paquete}: {e}")

if __name__ == "__main__":
    actualizar_librerias()
