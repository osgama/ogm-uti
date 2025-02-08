import os
import json
import subprocess
import ttkbootstrap as ttk
from tkinter import StringVar, messagebox, Frame, Text, Toplevel
from ttkbootstrap import Window

CONFIG_FILE = "config.json"


def load_config():
    if os.path.exists(CONFIG_FILE):
        with open(CONFIG_FILE, "r") as f:
            return json.load(f)
    return {"servers": []}


def save_config(config):
    with open(CONFIG_FILE, "w") as f:
        json.dump(config, f, indent=4)


def execute_command(command):
    try:
        result = subprocess.run(command, shell=True, capture_output=True, text=True)
        return result.stdout if result.returncode == 0 else result.stderr
    except Exception as e:
        return str(e)


class ModernApp:
    def __init__(self, root):
        self.root = root
        self.root.title("Ultra Modern App")
        self.root.geometry("900x600")
        self.config = load_config()
        self.selected_server = StringVar()
        self.tooltips = []
        self.create_ui()

    def create_ui(self):
        # Barra lateral con servidores
        self.sidebar = ttk.Frame(self.root, width=150, style="secondary.TFrame")
        self.sidebar.pack(side="left", fill="y")

        ttk.Label(self.sidebar, text="Servidores", font=("Arial", 12, "bold")).pack(pady=10)

        self.servers_buttons = {}
        for server in ["Compilador", "DEV", "SIT", "UAT"]:
            self.servers_buttons[server] = ttk.Button(self.sidebar, text=server, bootstyle="primary",
                                                      command=lambda s=server: self.set_server(s))
            self.servers_buttons[server].pack(fill="x", pady=5, padx=10)

        self.config_btn = ttk.Button(self.sidebar, text="⚙", bootstyle="link", command=self.show_config)
        self.config_btn.pack(side="bottom", padx=10, pady=5)

        # Barra de opciones superior (sin texto adicional)
        self.topbar = ttk.Frame(self.root, padding=10, style="light.TFrame")
        self.topbar.pack(side="top", fill="x")

        # Consola de salida
        self.log_frame = Frame(self.root)
        self.log_frame.pack(side="bottom", fill="x", padx=10, pady=10)

        self.log_box = Text(self.log_frame, height=8, width=100)
        self.log_box.pack(fill="both", expand=True)

    def set_server(self, server):
        """Selecciona un servidor y actualiza la barra de opciones"""
        self.selected_server.set(server)
        self.update_topbar()

    def update_topbar(self):
        """Actualiza la barra superior con las opciones del servidor seleccionado"""
        for widget in self.topbar.winfo_children():
            widget.destroy()

        menu_options = {
            "Compilador": [("🧹", "Limpiar Compilación")],
            "Otros": [
                ("⛔", "Detener TODO"), ("✅", "Iniciar TODO"),
                ("🔴", "Detener Línea"), ("🟢", "Iniciar Línea"),
                ("⛔", "Detener BATCH"), ("✅", "Iniciar BATCH"),
                ("🔴", "Detener UDP/CLC"), ("🟢", "Iniciar UDP/CLC"),
                ("🔴", "Detener WEB"), ("🟢", "Iniciar WEB"),
                ("🔄", "Reiniciar Utilidades"), ("🔍", "Validar Servicios"),
                ("🧹", "Limpiar Instalación")
            ]
        }

        options = menu_options["Compilador"] if self.selected_server.get() == "Compilador" else menu_options["Otros"]

        for icon, option in options:
            btn = ttk.Button(self.topbar, text=f"{icon}", bootstyle="success",
                             command=lambda cmd=f"echo {option} ejecutado": self.run_command(cmd))
            btn.pack(side="left", padx=5)

            # Agregar tooltip
            self.add_tooltip(btn, option)

        # Botón para limpiar logs
        clear_btn = ttk.Button(self.topbar, text="🗑", bootstyle="danger", command=self.clear_logs)
        clear_btn.pack(side="right", padx=10)
        self.add_tooltip(clear_btn, "Limpiar Consola")

    def add_tooltip(self, widget, text):
        """Agrega un tooltip cuando el mouse pasa sobre un botón"""
        tooltip = Toplevel(self.root)
        tooltip.withdraw()
        tooltip.overrideredirect(True)
        tooltip_label = ttk.Label(tooltip, text=text, background="black", foreground="white", padding=3)
        tooltip_label.pack()

        def enter(event):
            x, y, _, _ = widget.bbox("insert")
            x_root = widget.winfo_rootx() + x
            y_root = widget.winfo_rooty() + y + 30
            tooltip.geometry(f"+{x_root}+{y_root}")
            tooltip.deiconify()

        def leave(event):
            tooltip.withdraw()

        widget.bind("<Enter>", enter)
        widget.bind("<Leave>", leave)

        self.tooltips.append(tooltip)

    def show_config(self):
        config_win = Toplevel(self.root)
        config_win.title("Configuración")
        config_win.geometry("400x400")
        config_win.resizable(False, False)

        ttk.Label(config_win, text="📡 Servidores Configurados:").pack()
        self.servers_list = ttk.Combobox(config_win, values=self.config.get("servers", []))
        self.servers_list.pack()

        ttk.Label(config_win, text="➕ Agregar Nuevo Servidor:").pack()
        self.new_server_entry = ttk.Entry(config_win)
        self.new_server_entry.pack()

        ttk.Button(config_win, text="✅ Agregar", bootstyle="success", command=self.add_server).pack(pady=5)

        ttk.Label(config_win, text="👤 Usuario Global:").pack()
        self.user_entry = ttk.Entry(config_win)
        self.user_entry.insert(0, self.config.get("user", ""))
        self.user_entry.pack()

        ttk.Label(config_win, text="🔑 Contraseña Global:").pack()
        self.password_var = StringVar()
        self.password_entry = ttk.Entry(config_win, show="*", textvariable=self.password_var)
        self.password_entry.pack()

        self.show_password_btn = ttk.Checkbutton(config_win, text="👁 Mostrar Contraseña", command=self.toggle_password)
        self.show_password_btn.pack()

        ttk.Button(config_win, text="💾 Guardar", bootstyle="primary", command=self.save_settings).pack(pady=10)

    def add_server(self):
        server = self.new_server_entry.get().strip()
        if server and server not in self.config["servers"]:
            self.config["servers"].append(server)
            save_config(self.config)
            messagebox.showinfo("Configuración", "Servidor agregado.")

    def toggle_password(self):
        if self.password_entry.cget('show') == '*':
            self.password_entry.config(show='')
        else:
            self.password_entry.config(show='*')

    def save_settings(self):
        self.config["user"] = self.user_entry.get()
        self.config["password"] = self.password_var.get()
        save_config(self.config)
        messagebox.showinfo("Configuración", "Configuración guardada.")

    def run_command(self, command):
        """Ejecuta un comando y lo muestra en la consola de logs"""
        command_output = execute_command(command)
        self.log_box.insert("end", f"{command_output}\n")
        self.log_box.see("end")

    def clear_logs(self):
        """Limpia la consola de logs"""
        self.log_box.delete("1.0", "end")


if __name__ == "__main__":
    root = Window(themename="superhero")
    app = ModernApp(root)
    root.mainloop()
