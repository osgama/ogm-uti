import os
import json
import subprocess
from cryptography.fernet import Fernet
import ttkbootstrap as ttk
from tkinter import StringVar, messagebox, Frame, Text, Toplevel, simpledialog
from ttkbootstrap import Window

# Configuración de rutas
BASE_DIR = os.path.expanduser("~/Documents/my_app/")
CONFIG_FILE = os.path.join(BASE_DIR, "config.json")
KEY_FILE = os.path.join(BASE_DIR, "key.key")


# Generar clave de cifrado si no existe
def generate_key():
    if not os.path.exists(KEY_FILE):
        os.makedirs(BASE_DIR, exist_ok=True)
        key = Fernet.generate_key()
        with open(KEY_FILE, "wb") as key_file:
            key_file.write(key)


# Cargar clave de cifrado
def load_key():
    with open(KEY_FILE, "rb") as key_file:
        return key_file.read()


# Encriptar y desencriptar la contraseña
def encrypt_password(password):
    key = load_key()
    fernet = Fernet(key)
    return fernet.encrypt(password.encode()).decode()


def decrypt_password(encrypted_password):
    key = load_key()
    fernet = Fernet(key)
    try:
        return fernet.decrypt(encrypted_password.encode()).decode()
    except Exception:
        return ""


# Cargar y guardar configuración
def load_config():
    if os.path.exists(CONFIG_FILE):
        with open(CONFIG_FILE, "r") as f:
            return json.load(f)
    return {"servers": [], "user": "", "password": ""}


def save_config(config):
    os.makedirs(BASE_DIR, exist_ok=True)
    with open(CONFIG_FILE, "w") as f:
        json.dump(config, f, indent=4)


def execute_command(command):
    """Ejecuta un comando en el sistema y devuelve su salida o error."""
    try:
        result = subprocess.run(command, shell=True, capture_output=True, text=True)
        if result.returncode == 0:
            return result.stdout
        return result.stderr
    except Exception as e:
        return str(e)


class ModernApp:
    def __init__(self, root):
        self.root = root
        self.root.title("Ultra Modern App")
        self.root.geometry("900x600")
        self.config = load_config()
        self.selected_server = StringVar()
        self.create_ui()

    def create_ui(self):
        # Barra lateral con servidores
        self.sidebar = ttk.Frame(self.root, width=150, style="secondary.TFrame")
        self.sidebar.pack(side="left", fill="y")

        ttk.Label(self.sidebar, text="Servidores", font=("Arial", 12, "bold")).pack(pady=10)
        self.update_sidebar()  # Inicializa la barra con los servidores actuales

        self.config_btn = ttk.Button(self.sidebar, text="⚙", bootstyle="link", command=self.show_config)
        self.config_btn.pack(side="bottom", padx=10, pady=5)

        # Barra de opciones superior
        self.topbar = ttk.Frame(self.root, padding=10, style="light.TFrame")
        self.topbar.pack(side="top", fill="x")

        # Consola de salida
        self.log_frame = Frame(self.root)
        self.log_frame.pack(side="bottom", fill="x", padx=10, pady=10)

        self.log_box = Text(self.log_frame, height=8, width=100)
        self.log_box.pack(fill="both", expand=True)

    def update_sidebar(self):
        """Actualiza la barra lateral con los servidores configurados"""
        for widget in self.sidebar.winfo_children()[1:-1]:  # No borra el label ni el botón de configuración
            widget.destroy()

        for server in self.config["servers"]:
            server_name = server["name"]
            environment = server["environment"]
            button_text = f"{server_name} ({environment})"
            ttk.Button(self.sidebar, text=button_text, bootstyle="primary",
                       command=lambda s=server: self.set_server(s)).pack(fill="x", pady=5, padx=10)

    def set_server(self, server):
        """Selecciona un servidor y actualiza la barra de opciones"""
        self.selected_server.set(f"{server['name']} ({server['environment']})")
        self.update_topbar(server)

    def update_topbar(self, server):
        """Actualiza la barra superior con las opciones del servidor seleccionado"""
        for widget in self.topbar.winfo_children():
            widget.destroy()

        menu_options = {
            "Compilador": [("🧹", "Limpiar Compilación")],
            "Todos": [
                ("⛔", "Detener TODO"), ("✅", "Iniciar TODO"),
                ("🔴", "Detener Línea"), ("🟢", "Iniciar Línea"),
                ("⛔", "Detener BATCH"), ("✅", "Iniciar BATCH"),
                ("🔴", "Detener UDP/CLC"), ("🟢", "Iniciar UDP/CLC"),
                ("🔴", "Detener WEB"), ("🟢", "Iniciar WEB"),
                ("🔄", "Reiniciar Utilidades"), ("🔍", "Validar Servicios"),
                ("🧹", "Limpiar Instalación")
            ]
        }

        options = menu_options["Compilador"] if server["type"] == "Compilador" else menu_options["Todos"]

        for icon, option in options:
            btn = ttk.Button(self.topbar, text=f"{icon}", bootstyle="success",
                             command=lambda cmd=f"echo {option} ejecutado": self.run_command(cmd))
            btn.pack(side="left", padx=5)

    def show_config(self):
        config_win = Toplevel(self.root)
        config_win.title("Configuración")
        config_win.geometry("500x500")
        config_win.resizable(False, False)

        ttk.Label(config_win, text="📡 Servidores Configurados:", font=("Arial", 12, "bold")).pack(pady=5)
        self.servers_list = ttk.Combobox(config_win,
                                         values=[f"{s['name']} ({s['environment']})" for s in self.config["servers"]])
        self.servers_list.pack()

        ttk.Button(config_win, text="✏️ Editar Servidor", bootstyle="info", command=self.edit_server).pack(pady=5)
        ttk.Button(config_win, text="➕ Agregar Nuevo Servidor", bootstyle="success", command=self.add_server).pack(
            pady=10)

        ttk.Label(config_win, text="👤 Usuario Global:").pack(pady=5)
        self.user_entry = ttk.Entry(config_win)
        self.user_entry.insert(0, self.config.get("user", ""))
        self.user_entry.pack()

        ttk.Label(config_win, text="🔑 Contraseña Global:").pack(pady=5)
        self.password_var = StringVar(value=decrypt_password(self.config.get("password", "")))
        self.password_entry = ttk.Entry(config_win, textvariable=self.password_var, show="*")
        self.password_entry.pack()

        ttk.Checkbutton(config_win, text="👁 Mostrar Contraseña", command=self.toggle_password).pack()
        ttk.Button(config_win, text="💾 Guardar Configuración", bootstyle="primary", command=self.save_settings).pack(
            pady=10)

    def edit_server(self):
        """Edita un servidor seleccionado en la lista"""
        selected_server = self.servers_list.get()
        if not selected_server:
            messagebox.showerror("Error", "Seleccione un servidor para editar.")
            return

        for server in self.config["servers"]:
            if f"{server['name']} ({server['environment']})" == selected_server:
                server["name"] = simpledialog.askstring("Editar Servidor", "Ingrese el nuevo nombre del servidor:",
                                                        initialvalue=server["name"])
                server["environment"] = simpledialog.askstring("Editar Ambiente", "Ingrese el nuevo ambiente:",
                                                               initialvalue=server["environment"])
                server_type = messagebox.askyesno("Tipo de Servidor",
                                                  "¿Desea habilitar todas las opciones (Sí) o solo las de compilador (No)?")
                server["type"] = "Todos" if server_type else "Compilador"
                save_config(self.config)
                self.update_sidebar()
                messagebox.showinfo("Configuración", "Servidor editado exitosamente.")
                return

    def add_server(self):
        """Agrega un nuevo servidor con detalles adicionales"""
        server_name = simpledialog.askstring("Nuevo Servidor", "Ingrese el nombre del servidor:")
        if not server_name:
            return

        environment = simpledialog.askstring("Ambiente", "Ingrese el ambiente (Compilador, DEV, SIT, UAT):")
        if not environment:
            return

        server_type = messagebox.askyesno("Tipo de Servidor",
                                          "¿Desea habilitar todas las opciones (Sí) o solo las de compilador (No)?")
        server_type = "Todos" if server_type else "Compilador"

        new_server = {
            "name": server_name,
            "environment": environment,
            "type": server_type
        }

        self.config["servers"].append(new_server)
        save_config(self.config)
        self.update_sidebar()
        messagebox.showinfo("Configuración", "Servidor agregado exitosamente.")

    def toggle_password(self):
        if self.password_entry.cget('show') == '*':
            self.password_entry.config(show='')
        else:
            self.password_entry.config(show='*')

    def save_settings(self):
        self.config["user"] = self.user_entry.get()
        self.config["password"] = encrypt_password(self.password_var.get())
        save_config(self.config)
        messagebox.showinfo("Configuración", "Configuración guardada exitosamente.")
        self.update_sidebar()

    def run_command(self, command):
        """Limpia la consola y ejecuta un comando, mostrando la nueva salida"""
        self.log_box.delete("1.0", "end")
        command_output = execute_command(command)
        self.log_box.insert("end", f"{command_output}\n")
        self.log_box.see("end")


if __name__ == "__main__":
    generate_key()
    root = Window(themename="superhero")
    app = ModernApp(root)
    root.mainloop()
