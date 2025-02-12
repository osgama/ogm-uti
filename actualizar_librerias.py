import subprocess
import importlib.metadata

def actualizar_librerias():
    # Obtener la lista de todas las librerías instaladas
    paquetes = [dist.metadata['Name'] for dist in importlib.metadata.distributions()]
    
    for paquete in paquetes:
        try:
            print(f"Actualizando {paquete}...")
            subprocess.check_call(["pip", "install", "--upgrade", paquete])
        except subprocess.CalledProcessError as e:
            print(f"Error al actualizar {paquete}: {e}")

if __name__ == "__main__":
    actualizar_librerias()
