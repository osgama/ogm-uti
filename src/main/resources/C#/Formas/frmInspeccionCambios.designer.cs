namespace Masivos
{
    partial class frmInspeccionCambios
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridCambios = new System.Windows.Forms.DataGridView();
            this.NumRem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.campo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cambio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelRemesa = new System.Windows.Forms.Label();
            this.tbRemesa = new System.Windows.Forms.TextBox();
            this.tbProceso = new System.Windows.Forms.TextBox();
            this.labelProcesoStatus = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCambios)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridCambios
            // 
            this.dataGridCambios.AllowUserToAddRows = false;
            this.dataGridCambios.AllowUserToDeleteRows = false;
            this.dataGridCambios.AllowUserToResizeColumns = false;
            this.dataGridCambios.AllowUserToResizeRows = false;
            this.dataGridCambios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridCambios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NumRem,
            this.campo,
            this.cambio});
            this.dataGridCambios.Location = new System.Drawing.Point(12, 56);
            this.dataGridCambios.MultiSelect = false;
            this.dataGridCambios.Name = "dataGridCambios";
            this.dataGridCambios.ReadOnly = true;
            this.dataGridCambios.RowHeadersVisible = false;
            this.dataGridCambios.RowHeadersWidth = 6;
            this.dataGridCambios.RowTemplate.Height = 24;
            this.dataGridCambios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridCambios.Size = new System.Drawing.Size(768, 373);
            this.dataGridCambios.TabIndex = 0;
            // 
            // NumRem
            // 
            this.NumRem.HeaderText = "Folio";
            this.NumRem.Name = "NumRem";
            this.NumRem.ReadOnly = true;
            this.NumRem.Width = 200;
            // 
            // campo
            // 
            this.campo.HeaderText = "Campo";
            this.campo.Name = "campo";
            this.campo.ReadOnly = true;
            // 
            // cambio
            // 
            this.cambio.HeaderText = "Cambio";
            this.cambio.Name = "cambio";
            this.cambio.ReadOnly = true;
            this.cambio.Width = 440;
            // 
            // labelRemesa
            // 
            this.labelRemesa.AutoSize = true;
            this.labelRemesa.Location = new System.Drawing.Point(12, 20);
            this.labelRemesa.Name = "labelRemesa";
            this.labelRemesa.Size = new System.Drawing.Size(49, 13);
            this.labelRemesa.TabIndex = 0;
            this.labelRemesa.Text = "Remesa:";
            // 
            // tbRemesa
            // 
            this.tbRemesa.Location = new System.Drawing.Point(67, 17);
            this.tbRemesa.Name = "tbRemesa";
            this.tbRemesa.ReadOnly = true;
            this.tbRemesa.Size = new System.Drawing.Size(159, 20);
            this.tbRemesa.TabIndex = 1;
            // 
            // tbProceso
            // 
            this.tbProceso.Location = new System.Drawing.Point(347, 17);
            this.tbProceso.Name = "tbProceso";
            this.tbProceso.ReadOnly = true;
            this.tbProceso.Size = new System.Drawing.Size(78, 20);
            this.tbProceso.TabIndex = 3;
            // 
            // labelProcesoStatus
            // 
            this.labelProcesoStatus.AutoSize = true;
            this.labelProcesoStatus.Location = new System.Drawing.Point(257, 20);
            this.labelProcesoStatus.Name = "labelProcesoStatus";
            this.labelProcesoStatus.Size = new System.Drawing.Size(84, 13);
            this.labelProcesoStatus.TabIndex = 29;
            this.labelProcesoStatus.Text = "Proceso/Status:";
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(648, 435);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(132, 23);
            this.btnSalir.TabIndex = 30;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // frmInspeccionCambios
            // 
            this.AcceptButton = this.btnSalir;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 470);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.tbProceso);
            this.Controls.Add(this.labelProcesoStatus);
            this.Controls.Add(this.tbRemesa);
            this.Controls.Add(this.labelRemesa);
            this.Controls.Add(this.dataGridCambios);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 497);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 497);
            this.Name = "frmInspeccionCambios";
            this.ShowIcon = false;
            this.Text = "Inspección Remesas - Bitacora Cambios";
            this.Load += new System.EventHandler(this.frmInspeccionCambios_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCambios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridCambios;
        private System.Windows.Forms.Label labelRemesa;
        private System.Windows.Forms.TextBox tbRemesa;
        private System.Windows.Forms.TextBox tbProceso;
        private System.Windows.Forms.Label labelProcesoStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumRem;
        private System.Windows.Forms.DataGridViewTextBoxColumn campo;
        private System.Windows.Forms.DataGridViewTextBoxColumn cambio;
        private System.Windows.Forms.Button btnSalir;
    }
}