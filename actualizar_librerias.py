import subprocess
import importlib.metadata
import json

def actualizar_librerias():
    resumen = {}

    # Obtener la lista de todas las librerías instaladas
    paquetes = [dist.metadata['Name'] for dist in importlib.metadata.distributions()]

    for paquete in paquetes:
        try:
            print(f"Actualizando {paquete}...")
            subprocess.check_call(["pip", "install", "--upgrade", paquete])
            nueva_version = importlib.metadata.version(paquete)
            resumen[paquete] = nueva_version
        except subprocess.CalledProcessError as e:
            print(f"Error al actualizar {paquete}: {e}")
            resumen[paquete] = "Error"

    # Mostrar el resumen final
    print("\n--- Resumen de actualizaciones ---")
    for paquete, version in resumen.items():
        print(f"{paquete}: {version}")

    # Ejecutar pipdeptree para verificar dependencias
    print("\n--- Dependencias del entorno (pipdeptree) ---")
    try:
        output = subprocess.check_output(["pipdeptree", "--json"], text=True)
        dependencias = json.loads(output)
        for paquete in dependencias:
            print(f"{paquete['package']['key']} -> {', '.join(dep['package_name'] for dep in paquete.get('dependencies', []))}")
    except subprocess.CalledProcessError:
        print("pipdeptree no está instalado. Ejecútalo manualmente con: pip install pipdeptree")

if __name__ == "__main__":
    actualizar_librerias()
