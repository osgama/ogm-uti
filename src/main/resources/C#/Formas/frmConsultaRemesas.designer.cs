namespace Masivos
{
    partial class frmConsultaRemesas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnEjecutar = new System.Windows.Forms.Button();
            this.tbRemesa = new System.Windows.Forms.TextBox();
            this.lbRemesa = new System.Windows.Forms.Label();
            this.lbFechaRemesa = new System.Windows.Forms.Label();
            this.dtpFechaRemesa = new System.Windows.Forms.DateTimePicker();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.lstOpciones = new System.Windows.Forms.ListBox();
            this.lstConsultas = new System.Windows.Forms.ListBox();
            this.tbRespuestas = new System.Windows.Forms.TextBox();
            this.lbArchivo = new System.Windows.Forms.Label();
            this.tbArchivo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.Location = new System.Drawing.Point(224, 183);
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(146, 29);
            this.btnEjecutar.TabIndex = 7;
            this.btnEjecutar.Text = "&Ejecutar";
            this.btnEjecutar.UseVisualStyleBackColor = true;
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // tbRemesa
            // 
            this.tbRemesa.AcceptsReturn = true;
            this.tbRemesa.Location = new System.Drawing.Point(349, 126);
            this.tbRemesa.MaxLength = 22;
            this.tbRemesa.Name = "tbRemesa";
            this.tbRemesa.Size = new System.Drawing.Size(183, 20);
            this.tbRemesa.TabIndex = 4;
            this.tbRemesa.Visible = false;
            this.tbRemesa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbRemesa_KeyPress);
            // 
            // lbRemesa
            // 
            this.lbRemesa.AutoSize = true;
            this.lbRemesa.Location = new System.Drawing.Point(294, 129);
            this.lbRemesa.Name = "lbRemesa";
            this.lbRemesa.Size = new System.Drawing.Size(49, 13);
            this.lbRemesa.TabIndex = 3;
            this.lbRemesa.Text = "&Remesa:";
            this.lbRemesa.Visible = false;
            // 
            // lbFechaRemesa
            // 
            this.lbFechaRemesa.AutoSize = true;
            this.lbFechaRemesa.Location = new System.Drawing.Point(372, 156);
            this.lbFechaRemesa.Name = "lbFechaRemesa";
            this.lbFechaRemesa.Size = new System.Drawing.Size(40, 13);
            this.lbFechaRemesa.TabIndex = 5;
            this.lbFechaRemesa.Text = "&Fecha:";
            this.lbFechaRemesa.Visible = false;
            // 
            // dtpFechaRemesa
            // 
            this.dtpFechaRemesa.CustomFormat = "yyyy/MM/dd";
            this.dtpFechaRemesa.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRemesa.Location = new System.Drawing.Point(418, 152);
            this.dtpFechaRemesa.Name = "dtpFechaRemesa";
            this.dtpFechaRemesa.Size = new System.Drawing.Size(114, 20);
            this.dtpFechaRemesa.TabIndex = 6;
            this.dtpFechaRemesa.Visible = false;
            this.dtpFechaRemesa.ValueChanged += new System.EventHandler(this.dtpFechaRemesa_ValueChanged);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(442, 226);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(90, 62);
            this.btnLimpiar.TabIndex = 10;
            this.btnLimpiar.Text = "&Limpiar Log";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(386, 183);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(146, 29);
            this.btnSalir.TabIndex = 8;
            this.btnSalir.Text = "&Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // lstOpciones
            // 
            this.lstOpciones.FormattingEnabled = true;
            this.lstOpciones.Items.AddRange(new object[] {
            "CONSULTAS",
            "RETRANSMISIÓN DE ARCHIVO"});
            this.lstOpciones.Location = new System.Drawing.Point(12, 49);
            this.lstOpciones.Name = "lstOpciones";
            this.lstOpciones.Size = new System.Drawing.Size(206, 43);
            this.lstOpciones.TabIndex = 0;
            this.lstOpciones.SelectedIndexChanged += new System.EventHandler(this.lstOpciones_SelectedIndexChanged);
            // 
            // lstConsultas
            // 
            this.lstConsultas.FormattingEnabled = true;
            this.lstConsultas.Location = new System.Drawing.Point(224, 12);
            this.lstConsultas.Name = "lstConsultas";
            this.lstConsultas.Size = new System.Drawing.Size(308, 108);
            this.lstConsultas.TabIndex = 1;
            this.lstConsultas.SelectedIndexChanged += new System.EventHandler(this.lstConsultas_SelectedIndexChanged);
            // 
            // tbRespuestas
            // 
            this.tbRespuestas.BackColor = System.Drawing.SystemColors.Window;
            this.tbRespuestas.Location = new System.Drawing.Point(12, 226);
            this.tbRespuestas.Multiline = true;
            this.tbRespuestas.Name = "tbRespuestas";
            this.tbRespuestas.ReadOnly = true;
            this.tbRespuestas.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbRespuestas.Size = new System.Drawing.Size(424, 62);
            this.tbRespuestas.TabIndex = 9;
            // 
            // lbArchivo
            // 
            this.lbArchivo.AutoSize = true;
            this.lbArchivo.Location = new System.Drawing.Point(240, 62);
            this.lbArchivo.Name = "lbArchivo";
            this.lbArchivo.Size = new System.Drawing.Size(103, 13);
            this.lbArchivo.TabIndex = 2;
            this.lbArchivo.Text = "&Nombre del Archivo:";
            this.lbArchivo.Visible = false;
            // 
            // tbArchivo
            // 
            this.tbArchivo.Location = new System.Drawing.Point(349, 59);
            this.tbArchivo.MaxLength = 8;
            this.tbArchivo.Name = "tbArchivo";
            this.tbArchivo.Size = new System.Drawing.Size(183, 20);
            this.tbArchivo.TabIndex = 3;
            this.tbArchivo.Visible = false;
            // 
            // frmConsultaRemesas
            // 
            this.AcceptButton = this.btnEjecutar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 301);
            this.Controls.Add(this.lbArchivo);
            this.Controls.Add(this.tbArchivo);
            this.Controls.Add(this.tbRespuestas);
            this.Controls.Add(this.lstConsultas);
            this.Controls.Add(this.lstOpciones);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.dtpFechaRemesa);
            this.Controls.Add(this.lbFechaRemesa);
            this.Controls.Add(this.lbRemesa);
            this.Controls.Add(this.tbRemesa);
            this.Controls.Add(this.btnEjecutar);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(552, 328);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(552, 328);
            this.Name = "frmConsultaRemesas";
            this.ShowIcon = false;
            this.Text = "C753 Masivos - Consulta de Remesa";
            this.Load += new System.EventHandler(this.frmConsultaRemesas_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEjecutar;
        private System.Windows.Forms.TextBox tbRemesa;
        private System.Windows.Forms.Label lbRemesa;
        private System.Windows.Forms.Label lbFechaRemesa;
        private System.Windows.Forms.DateTimePicker dtpFechaRemesa;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.ListBox lstOpciones;
        private System.Windows.Forms.ListBox lstConsultas;
        private System.Windows.Forms.TextBox tbRespuestas;
        private System.Windows.Forms.Label lbArchivo;
        private System.Windows.Forms.TextBox tbArchivo;
    }
}