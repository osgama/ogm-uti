namespace Masivos
{
    partial class frmInspeccionRemesas
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnCambios = new System.Windows.Forms.Button();
            this.btnInspeccion = new System.Windows.Forms.Button();
            this.btnEnviaAries = new System.Windows.Forms.Button();
            this.dataGridRemesas = new System.Windows.Forms.DataGridView();
            this.remesa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.procesoStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnActualizar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridRemesas)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCambios
            // 
            this.btnCambios.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCambios.Enabled = false;
            this.btnCambios.Location = new System.Drawing.Point(130, 431);
            this.btnCambios.Name = "btnCambios";
            this.btnCambios.Size = new System.Drawing.Size(110, 31);
            this.btnCambios.TabIndex = 2;
            this.btnCambios.Text = "Ver &Cambios";
            this.btnCambios.UseVisualStyleBackColor = true;
            this.btnCambios.Click += new System.EventHandler(this.btnCambios_Click);
            // 
            // btnInspeccion
            // 
            this.btnInspeccion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnInspeccion.Enabled = false;
            this.btnInspeccion.Location = new System.Drawing.Point(12, 431);
            this.btnInspeccion.Name = "btnInspeccion";
            this.btnInspeccion.Size = new System.Drawing.Size(110, 31);
            this.btnInspeccion.TabIndex = 1;
            this.btnInspeccion.Text = "&Inspeccion";
            this.btnInspeccion.UseVisualStyleBackColor = true;
            this.btnInspeccion.Click += new System.EventHandler(this.btnInspeccion_Click);
            // 
            // btnEnviaAries
            // 
            this.btnEnviaAries.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEnviaAries.Enabled = false;
            this.btnEnviaAries.Location = new System.Drawing.Point(248, 431);
            this.btnEnviaAries.Name = "btnEnviaAries";
            this.btnEnviaAries.Size = new System.Drawing.Size(110, 31);
            this.btnEnviaAries.TabIndex = 3;
            this.btnEnviaAries.Text = "Enviar &Aries";
            this.btnEnviaAries.UseVisualStyleBackColor = true;
            this.btnEnviaAries.Click += new System.EventHandler(this.btnEnviaAries_Click);
            // 
            // dataGridRemesas
            // 
            this.dataGridRemesas.AccessibleRole = System.Windows.Forms.AccessibleRole.Grip;
            this.dataGridRemesas.AllowUserToAddRows = false;
            this.dataGridRemesas.AllowUserToDeleteRows = false;
            this.dataGridRemesas.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridRemesas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridRemesas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridRemesas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.remesa,
            this.procesoStatus,
            this.descripcion});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridRemesas.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridRemesas.Location = new System.Drawing.Point(12, 12);
            this.dataGridRemesas.MultiSelect = false;
            this.dataGridRemesas.Name = "dataGridRemesas";
            this.dataGridRemesas.ReadOnly = true;
            this.dataGridRemesas.RowHeadersVisible = false;
            this.dataGridRemesas.RowTemplate.Height = 24;
            this.dataGridRemesas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridRemesas.Size = new System.Drawing.Size(466, 406);
            this.dataGridRemesas.TabIndex = 0;
            this.dataGridRemesas.CurrentCellChanged += new System.EventHandler(this.dataGridRemesas_CurrentCellChanged);
            // 
            // remesa
            // 
            this.remesa.HeaderText = "Número Remesa";
            this.remesa.Name = "remesa";
            this.remesa.ReadOnly = true;
            this.remesa.Width = 130;
            // 
            // procesoStatus
            // 
            this.procesoStatus.HeaderText = "Proceso, Status";
            this.procesoStatus.Name = "procesoStatus";
            this.procesoStatus.ReadOnly = true;
            this.procesoStatus.Width = 60;
            // 
            // descripcion
            // 
            this.descripcion.HeaderText = "Descripción";
            this.descripcion.Name = "descripcion";
            this.descripcion.ReadOnly = true;
            this.descripcion.Width = 273;
            // 
            // btnActualizar
            // 
            this.btnActualizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnActualizar.Location = new System.Drawing.Point(368, 431);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(110, 31);
            this.btnActualizar.TabIndex = 4;
            this.btnActualizar.Text = "Ac&tualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // frmInspeccionRemesas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 474);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.dataGridRemesas);
            this.Controls.Add(this.btnEnviaAries);
            this.Controls.Add(this.btnCambios);
            this.Controls.Add(this.btnInspeccion);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(498, 501);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(498, 501);
            this.Name = "frmInspeccionRemesas";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "C753 Masivos - Inspección Remesas";
            this.Load += new System.EventHandler(this.frmInspeccionRemesas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridRemesas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCambios;
        private System.Windows.Forms.Button btnInspeccion;
        private System.Windows.Forms.Button btnEnviaAries;
        private System.Windows.Forms.DataGridView dataGridRemesas;
        private System.Windows.Forms.DataGridViewTextBoxColumn remesa;
        private System.Windows.Forms.DataGridViewTextBoxColumn procesoStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcion;
        private System.Windows.Forms.Button btnActualizar;
    }
}