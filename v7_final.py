import os
import json
import paramiko
from cryptography.fernet import Fernet
import ttkbootstrap as ttk
from tkinter import messagebox, Toplevel, Label, StringVar
from ttkbootstrap import Progressbar


# Configuración global
CONFIG_FILE = os.path.join(os.path.expanduser("~/Documents/sftp/"), "config.json")
KEY_FILE = "key.key"
BASE_DIR = os.path.expanduser("~/Documents/sftp/")


# Generar clave de cifrado si no existe
def generate_key():
    if not os.path.exists(KEY_FILE):
        key = Fernet.generate_key()
        with open(KEY_FILE, "wb") as key_file:
            key_file.write(key)

def load_key():
    return open(KEY_FILE, "rb").read()

def encrypt_password(password):
    key = load_key()
    fernet = Fernet(key)
    return fernet.encrypt(password.encode()).decode()

def decrypt_password(encrypted_password):
    key = load_key()
    fernet = Fernet(key)
    return fernet.decrypt(encrypted_password.encode()).decode()

# Cargar configuración
def load_config():
    if os.path.exists(CONFIG_FILE):
        with open(CONFIG_FILE, "r") as f:
            return json.load(f)
    return {}

# Guardar configuración
def save_config(config):
    os.makedirs(BASE_DIR, exist_ok=True)
    with open(CONFIG_FILE, "w") as f:
        json.dump(config, f, indent=4)

class SFTPClientApp:
    def __init__(self, root):
        self.root = root
        self.root.title("SFTP v1.1")
        self.root.geometry("800x500")

        # Cargar configuración
        self.config = load_config()

        # Crear carpetas base
        self.create_folders()

        # Crear interfaz
        self.create_ui()

    def create_folders(self):
        folders = ["Evidencias", "Permisos", "Generales", "ParaEnviar", "Enviados"]
        for folder in folders:
            os.makedirs(os.path.join(BASE_DIR, folder), exist_ok=True)

    def connect_sftp(self):
        try:
            self.config = load_config()
            password = decrypt_password(self.config.get("password", ""))
            transport = paramiko.Transport((self.config["server"], int(self.config.get("port", 22))))
            transport.connect(username=self.config["user"], password=password)
            return paramiko.SFTPClient.from_transport(transport)
        except Exception as e:
            messagebox.showerror("Error", f"No se pudo conectar al SFTP: {e}")
            return None

    def check_sftp_connection(self):
        sftp = self.connect_sftp()
        if sftp:
            sftp.close()
            return True
        return False

    def create_ui(self):
        self.style = ttk.Style(theme="darkly")

        # Barra lateral izquierda
        self.sidebar = ttk.Frame(self.root, width=60, style="secondary.TFrame")
        self.sidebar.pack(side="left", fill="both", padx=0, pady=0)

        # Botones con solo iconos
        self.download_btn = ttk.Button(self.sidebar, text="📥", bootstyle="info", command=self.show_downloads)
        self.upload_btn = ttk.Button(self.sidebar, text="📤", bootstyle="success", command=self.show_uploads)
        self.config_btn = ttk.Button(self.sidebar, text="⚙", bootstyle="link", command=self.show_config)

        # Posicionar botones
        self.download_btn.pack(fill="x", pady=0, ipadx=10, ipady=10)
        self.upload_btn.pack(fill="x", pady=0, ipadx=10, ipady=10)
        self.config_btn.pack(side="bottom", fill="x", pady=0, ipadx=10, ipady=10)

        # Vincular eventos para tooltips (🔹 Debe ir aquí, después de crear los botones)
        self.download_btn.bind("<Enter>", lambda e: self.show_tooltip(e, "Descargas"))
        self.download_btn.bind("<Leave>", self.hide_tooltip)
        self.upload_btn.bind("<Enter>", lambda e: self.show_tooltip(e, "Envíos"))
        self.upload_btn.bind("<Leave>", self.hide_tooltip)
        self.config_btn.bind("<Enter>", lambda e: self.show_tooltip(e, "Configuración"))
        self.config_btn.bind("<Leave>", self.hide_tooltip)

        # Panel de contenido
        self.content_frame = ttk.Frame(self.root, padding=20)
        self.content_frame.pack(fill='both', expand=True, padx=25, pady=15)
        self.show_downloads()

    def show_tooltip(self, event, text):
        self.tooltip = Toplevel(self.root)
        self.tooltip.wm_overrideredirect(True)
        self.tooltip.geometry(f"+{event.x_root + 10}+{event.y_root + 10}")
        label = Label(self.tooltip, text=text, background="black", foreground="white", padx=5, pady=2)
        label.pack()

    def hide_tooltip(self, event):
        if hasattr(self, "tooltip"):
            self.tooltip.destroy()

    def toggle_password(self):
        if self.password_entry.cget('show') == '*':
            self.password_entry.config(show='')
        else:
            self.password_entry.config(show='*')

    def save_settings(self):
        config = {
            "server": self.server_entry.get(),
            "port": int(self.port_entry.get()),
            "user": self.user_entry.get(),
            "password": encrypt_password(self.password_var.get()),
            "prefix": self.prefix_entry.get()
        }
        save_config(config)
        messagebox.showinfo("Configuración", "Configuración guardada con éxito.")

    def download_all(self):
        sftp = self.connect_sftp()
        if not sftp:
            return
        files = sftp.listdir()
        progress = Progressbar(self.content_frame, mode="determinate", maximum=len(files))
        progress.pack(pady=10, fill='x')
        for index, file in enumerate(files):
            progress['value'] = index + 1
            self.root.update_idletasks()
            clean_name = file.replace(self.config.get("prefix", ""), "")
            if "evidencia" in file.lower():
                dest_folder = "Evidencias"
            elif "permiso" in file.lower():
                dest_folder = "Permisos"
            else:
                dest_folder = "Generales"
            sftp.get(file, os.path.join(BASE_DIR, dest_folder, clean_name))
        messagebox.showinfo("Descarga", "Descarga completada.")

    def upload_all(self):
        sftp = self.connect_sftp()
        if not sftp:
            return
        files = os.listdir("ParaEnviar")
        progress = Progressbar(self.content_frame, mode="determinate", maximum=len(files))
        progress.pack(pady=10, fill='x')

        prefix = self.config.get("prefix", "").strip()
        if not prefix:
            messagebox.showerror("Error", "El prefijo no está configurado en el JSON.")
            return
        for index, file in enumerate(files):
            progress['value'] = index + 1
            self.root.update_idletasks()
            filename = file if file.startswith(prefix) else f"{prefix}{file}"
            sftp.put(os.path.join("ParaEnviar", file), filename)
            os.rename(os.path.join("ParaEnviar", file), os.path.join("Enviados", filename))


    def show_downloads(self):
        for widget in self.content_frame.winfo_children():
            widget.destroy()
        ttk.Label(self.content_frame, text="Archivos Disponibles en SFTP", font=("Arial", 14)).pack()

        # Lista de archivos
        self.file_list = ttk.Treeview(self.content_frame, columns=("#1"), show='headings')
        self.file_list.heading("#1", text="Nombre del Archivo")
        self.file_list.pack(fill='both', expand=True, padx=10, pady=5)

        # Botón de descarga
        ttk.Button(self.content_frame, text="Descargar Todo", command=self.download_all).pack(pady=10)

    def show_uploads(self):
        for widget in self.content_frame.winfo_children():
            widget.destroy()
        ttk.Label(self.content_frame, text="Archivos para Enviar", font=("Arial", 14)).pack()

        # Verificar si la carpeta "ParaEnviar/" existe
        upload_dir = os.path.join(BASE_DIR, "ParaEnviar")
        os.makedirs(upload_dir, exist_ok=True)

        # Lista de archivos en "ParaEnviar/"
        files = [f for f in os.listdir(upload_dir) if os.path.isfile(os.path.join(upload_dir, f))]

        print(f"Archivos encontrados en ParaEnviar: {files}")  # 🔹 Depuración en consola

        if not files:
            ttk.Label(self.content_frame, text="No hay archivos para enviar.", font=("Arial", 12)).pack()
            return

        # Crear lista de archivos
        self.upload_list = ttk.Treeview(self.content_frame, columns=("#1"), show='headings')
        self.upload_list.heading("#1", text="Nombre del Archivo")
        self.upload_list.pack(fill='both', expand=True, padx=10, pady=5)

        # Agregar archivos a la lista
        for file in files:
            if file.lower().endswith(".zip"):  # Asegurar que son .zip
                self.upload_list.insert("", "end", values=(file,))

        # Botón para enviar todo
        ttk.Button(self.content_frame, text="Enviar Todo", command=self.upload_all).pack(pady=10)

    def show_config(self):
        config_win = ttk.Toplevel(self.root)
        config_win.title("Configuración SFTP")
        config_win.geometry("300x350")
        config_win.resizable(False, False)

        # Cargar configuración actual
        self.config = load_config()

        ttk.Label(config_win, text="Servidor:").pack()
        self.server_entry = ttk.Entry(config_win)
        self.server_entry.insert(0, self.config.get("server", ""))  # 🔹 Cargar valor guardado
        self.server_entry.pack()

        ttk.Label(config_win, text="Puerto:").pack()
        self.port_entry = ttk.Entry(config_win)
        self.port_entry.insert(0, self.config.get("port", ""))  # 🔹 Cargar valor guardado
        self.port_entry.pack()

        ttk.Label(config_win, text="Usuario:").pack()
        self.user_entry = ttk.Entry(config_win)
        self.user_entry.insert(0, self.config.get("user", ""))  # 🔹 Cargar valor guardado
        self.user_entry.pack()

        ttk.Label(config_win, text="Contraseña:").pack()
        self.password_var = StringVar()
        decrypted_password = decrypt_password(self.config.get("password", "")) if self.config.get("password") else ""
        self.password_entry = ttk.Entry(config_win, show="*", textvariable=self.password_var)
        self.password_entry.insert(0, decrypted_password)  # 🔹 Cargar contraseña desencriptada
        self.password_entry.pack()

        self.show_password = ttk.Checkbutton(config_win, text="Mostrar contraseña", command=self.toggle_password)
        self.show_password.pack()

        ttk.Label(config_win, text="Prefijo de archivos:").pack()
        self.prefix_entry = ttk.Entry(config_win)
        self.prefix_entry.insert(0, self.config.get("prefix", ""))  # 🔹 Cargar prefijo guardado
        self.prefix_entry.pack()

        ttk.Button(config_win, text="Guardar", command=self.save_settings).pack(pady=10)


if __name__ == "__main__":
    generate_key()
    root = ttk.Window(themename="superhero")
    app = SFTPClientApp(root)
    root.mainloop()