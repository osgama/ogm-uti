namespace Masivos
{
    partial class frmInspeccionCampos
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
            this.dataGridCampos = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbProcesoStatus = new System.Windows.Forms.TextBox();
            this.lbProcesoStatus = new System.Windows.Forms.Label();
            this.tbPreimpreso = new System.Windows.Forms.TextBox();
            this.lbPreimpreso = new System.Windows.Forms.Label();
            this.tbRemesa = new System.Windows.Forms.TextBox();
            this.lbRemesa = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.registro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.consecutivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idCampo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.campo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valor = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.valorOriginal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCampos)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridCampos
            // 
            this.dataGridCampos.AllowUserToAddRows = false;
            this.dataGridCampos.AllowUserToDeleteRows = false;
            this.dataGridCampos.AllowUserToResizeRows = false;
            this.dataGridCampos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridCampos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridCampos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.registro,
            this.consecutivo,
            this.idCampo,
            this.campo,
            this.valor,
            this.valorOriginal});
            this.dataGridCampos.Location = new System.Drawing.Point(12, 66);
            this.dataGridCampos.Name = "dataGridCampos";
            this.dataGridCampos.RowHeadersVisible = false;
            this.dataGridCampos.RowTemplate.Height = 24;
            this.dataGridCampos.Size = new System.Drawing.Size(768, 459);
            this.dataGridCampos.TabIndex = 0;
            this.dataGridCampos.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridCampos_CellValidated);
            this.dataGridCampos.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridCampos_CellValidating);
            this.dataGridCampos.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridCampos_EditingControlShowing);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tbProcesoStatus);
            this.groupBox1.Controls.Add(this.lbProcesoStatus);
            this.groupBox1.Controls.Add(this.tbPreimpreso);
            this.groupBox1.Controls.Add(this.lbPreimpreso);
            this.groupBox1.Controls.Add(this.tbRemesa);
            this.groupBox1.Controls.Add(this.lbRemesa);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(768, 48);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // tbProcesoStatus
            // 
            this.tbProcesoStatus.Location = new System.Drawing.Point(623, 17);
            this.tbProcesoStatus.Name = "tbProcesoStatus";
            this.tbProcesoStatus.ReadOnly = true;
            this.tbProcesoStatus.Size = new System.Drawing.Size(59, 20);
            this.tbProcesoStatus.TabIndex = 11;
            this.tbProcesoStatus.Tag = "00";
            // 
            // lbProcesoStatus
            // 
            this.lbProcesoStatus.AutoSize = true;
            this.lbProcesoStatus.Location = new System.Drawing.Point(535, 20);
            this.lbProcesoStatus.Name = "lbProcesoStatus";
            this.lbProcesoStatus.Size = new System.Drawing.Size(79, 13);
            this.lbProcesoStatus.TabIndex = 10;
            this.lbProcesoStatus.Tag = "00";
            this.lbProcesoStatus.Text = "Proceso Status";
            this.lbProcesoStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbPreimpreso
            // 
            this.tbPreimpreso.Location = new System.Drawing.Point(374, 17);
            this.tbPreimpreso.Name = "tbPreimpreso";
            this.tbPreimpreso.ReadOnly = true;
            this.tbPreimpreso.Size = new System.Drawing.Size(110, 20);
            this.tbPreimpreso.TabIndex = 9;
            this.tbPreimpreso.Tag = "00";
            // 
            // lbPreimpreso
            // 
            this.lbPreimpreso.AutoSize = true;
            this.lbPreimpreso.Location = new System.Drawing.Point(304, 20);
            this.lbPreimpreso.Name = "lbPreimpreso";
            this.lbPreimpreso.Size = new System.Drawing.Size(59, 13);
            this.lbPreimpreso.TabIndex = 8;
            this.lbPreimpreso.Tag = "00";
            this.lbPreimpreso.Text = "Preimpreso";
            this.lbPreimpreso.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbRemesa
            // 
            this.tbRemesa.Location = new System.Drawing.Point(104, 17);
            this.tbRemesa.Name = "tbRemesa";
            this.tbRemesa.ReadOnly = true;
            this.tbRemesa.Size = new System.Drawing.Size(167, 20);
            this.tbRemesa.TabIndex = 7;
            this.tbRemesa.Tag = "00";
            // 
            // lbRemesa
            // 
            this.lbRemesa.AutoSize = true;
            this.lbRemesa.Location = new System.Drawing.Point(52, 20);
            this.lbRemesa.Name = "lbRemesa";
            this.lbRemesa.Size = new System.Drawing.Size(46, 13);
            this.lbRemesa.TabIndex = 6;
            this.lbRemesa.Tag = "00";
            this.lbRemesa.Text = "Remesa";
            this.lbRemesa.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.Location = new System.Drawing.Point(335, 531);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(197, 23);
            this.btnGuardar.TabIndex = 2;
            this.btnGuardar.Text = "&Guardar Inspección";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalir.Location = new System.Drawing.Point(583, 531);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(197, 23);
            this.btnSalir.TabIndex = 3;
            this.btnSalir.Text = "&Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // registro
            // 
            this.registro.HeaderText = "Registro";
            this.registro.Name = "registro";
            this.registro.ReadOnly = true;
            this.registro.Width = 90;
            // 
            // consecutivo
            // 
            this.consecutivo.HeaderText = "Cons.";
            this.consecutivo.Name = "consecutivo";
            this.consecutivo.ReadOnly = true;
            this.consecutivo.Width = 60;
            // 
            // idCampo
            // 
            this.idCampo.HeaderText = "ID Campo";
            this.idCampo.Name = "idCampo";
            this.idCampo.ReadOnly = true;
            this.idCampo.Width = 90;
            // 
            // campo
            // 
            this.campo.HeaderText = "Campo";
            this.campo.Name = "campo";
            this.campo.ReadOnly = true;
            this.campo.Width = 210;
            // 
            // valor
            // 
            this.valor.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.valor.DropDownWidth = 10;
            this.valor.HeaderText = "Valor";
            this.valor.MaxDropDownItems = 10;
            this.valor.Name = "valor";
            this.valor.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.valor.Width = 310;
            // 
            // valorOriginal
            // 
            this.valorOriginal.HeaderText = "Valor Original";
            this.valorOriginal.Name = "valorOriginal";
            this.valorOriginal.Visible = false;
            // 
            // frmInspeccionCampos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridCampos);
            this.MinimumSize = new System.Drawing.Size(800, 593);
            this.Name = "frmInspeccionCampos";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inspección Remesa - Inspección de Campos";
            this.Load += new System.EventHandler(this.frmInspeccionCampos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCampos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridCampos;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbProcesoStatus;
        private System.Windows.Forms.Label lbProcesoStatus;
        private System.Windows.Forms.TextBox tbPreimpreso;
        private System.Windows.Forms.Label lbPreimpreso;
        private System.Windows.Forms.TextBox tbRemesa;
        private System.Windows.Forms.Label lbRemesa;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.DataGridViewTextBoxColumn registro;
        private System.Windows.Forms.DataGridViewTextBoxColumn consecutivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn idCampo;
        private System.Windows.Forms.DataGridViewTextBoxColumn campo;
        private System.Windows.Forms.DataGridViewComboBoxColumn valor;
        private System.Windows.Forms.DataGridViewTextBoxColumn valorOriginal;

    }
}