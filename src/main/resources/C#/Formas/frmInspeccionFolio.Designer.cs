namespace Masivos
{
    partial class frmInspeccionFolio
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
            this.dataGridFolios = new System.Windows.Forms.DataGridView();
            this.Folio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Inspeccionado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnTerminarInspeccion = new System.Windows.Forms.Button();
            this.btnInspeccionFolio = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbConsInspeccion = new System.Windows.Forms.TextBox();
            this.lbConsInsp = new System.Windows.Forms.Label();
            this.tbProcesoStatus = new System.Windows.Forms.TextBox();
            this.tbRemesa = new System.Windows.Forms.TextBox();
            this.lbProcesoStatus = new System.Windows.Forms.Label();
            this.lbRemesa = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridFolios)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridFolios
            // 
            this.dataGridFolios.AllowUserToAddRows = false;
            this.dataGridFolios.AllowUserToDeleteRows = false;
            this.dataGridFolios.AllowUserToResizeRows = false;
            this.dataGridFolios.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridFolios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridFolios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Folio,
            this.Nombre,
            this.Inspeccionado});
            this.dataGridFolios.Location = new System.Drawing.Point(12, 58);
            this.dataGridFolios.Name = "dataGridFolios";
            this.dataGridFolios.ReadOnly = true;
            this.dataGridFolios.RowHeadersVisible = false;
            this.dataGridFolios.RowTemplate.Height = 24;
            this.dataGridFolios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridFolios.Size = new System.Drawing.Size(757, 261);
            this.dataGridFolios.TabIndex = 0;
            this.dataGridFolios.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridFolios_CellValueChanged);
            this.dataGridFolios.CurrentCellChanged += new System.EventHandler(this.dataGridFolios_CurrentCellChanged);
            // 
            // Folio
            // 
            this.Folio.HeaderText = "Folio Preimpreso";
            this.Folio.Name = "Folio";
            this.Folio.ReadOnly = true;
            this.Folio.Width = 140;
            // 
            // Nombre
            // 
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.Name = "Nombre";
            this.Nombre.ReadOnly = true;
            this.Nombre.Width = 340;
            // 
            // Inspeccionado
            // 
            this.Inspeccionado.HeaderText = "Inspeccionado";
            this.Inspeccionado.Name = "Inspeccionado";
            this.Inspeccionado.ReadOnly = true;
            this.Inspeccionado.Width = 250;
            // 
            // btnTerminarInspeccion
            // 
            this.btnTerminarInspeccion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTerminarInspeccion.Enabled = false;
            this.btnTerminarInspeccion.Location = new System.Drawing.Point(448, 325);
            this.btnTerminarInspeccion.Name = "btnTerminarInspeccion";
            this.btnTerminarInspeccion.Size = new System.Drawing.Size(123, 31);
            this.btnTerminarInspeccion.TabIndex = 2;
            this.btnTerminarInspeccion.Text = "&Terminar Inspección";
            this.btnTerminarInspeccion.UseVisualStyleBackColor = true;
            this.btnTerminarInspeccion.Click += new System.EventHandler(this.btnTerminarInspeccion_Click);
            // 
            // btnInspeccionFolio
            // 
            this.btnInspeccionFolio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnInspeccionFolio.Location = new System.Drawing.Point(208, 325);
            this.btnInspeccionFolio.Name = "btnInspeccionFolio";
            this.btnInspeccionFolio.Size = new System.Drawing.Size(123, 31);
            this.btnInspeccionFolio.TabIndex = 1;
            this.btnInspeccionFolio.Text = "&Inspeccionar Folio";
            this.btnInspeccionFolio.UseVisualStyleBackColor = true;
            this.btnInspeccionFolio.Click += new System.EventHandler(this.btnInspeccionFolio_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbConsInspeccion);
            this.groupBox1.Controls.Add(this.lbConsInsp);
            this.groupBox1.Controls.Add(this.tbProcesoStatus);
            this.groupBox1.Controls.Add(this.tbRemesa);
            this.groupBox1.Controls.Add(this.lbProcesoStatus);
            this.groupBox1.Controls.Add(this.lbRemesa);
            this.groupBox1.Location = new System.Drawing.Point(12, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(757, 48);
            this.groupBox1.TabIndex = 49;
            this.groupBox1.TabStop = false;
            // 
            // tbConsInspeccion
            // 
            this.tbConsInspeccion.Location = new System.Drawing.Point(593, 15);
            this.tbConsInspeccion.Name = "tbConsInspeccion";
            this.tbConsInspeccion.ReadOnly = true;
            this.tbConsInspeccion.Size = new System.Drawing.Size(42, 20);
            this.tbConsInspeccion.TabIndex = 53;
            // 
            // lbConsInsp
            // 
            this.lbConsInsp.AutoSize = true;
            this.lbConsInsp.Location = new System.Drawing.Point(456, 18);
            this.lbConsInsp.Name = "lbConsInsp";
            this.lbConsInsp.Size = new System.Drawing.Size(124, 13);
            this.lbConsInsp.TabIndex = 52;
            this.lbConsInsp.Text = "Consecutivo Inspección:";
            // 
            // tbProcesoStatus
            // 
            this.tbProcesoStatus.Location = new System.Drawing.Point(351, 15);
            this.tbProcesoStatus.Name = "tbProcesoStatus";
            this.tbProcesoStatus.ReadOnly = true;
            this.tbProcesoStatus.Size = new System.Drawing.Size(76, 20);
            this.tbProcesoStatus.TabIndex = 51;
            // 
            // tbRemesa
            // 
            this.tbRemesa.Location = new System.Drawing.Point(105, 15);
            this.tbRemesa.Name = "tbRemesa";
            this.tbRemesa.ReadOnly = true;
            this.tbRemesa.Size = new System.Drawing.Size(138, 20);
            this.tbRemesa.TabIndex = 50;
            // 
            // lbProcesoStatus
            // 
            this.lbProcesoStatus.AutoSize = true;
            this.lbProcesoStatus.Location = new System.Drawing.Point(259, 18);
            this.lbProcesoStatus.Name = "lbProcesoStatus";
            this.lbProcesoStatus.Size = new System.Drawing.Size(84, 13);
            this.lbProcesoStatus.TabIndex = 49;
            this.lbProcesoStatus.Text = "Proceso/Status:";
            // 
            // lbRemesa
            // 
            this.lbRemesa.AutoSize = true;
            this.lbRemesa.Location = new System.Drawing.Point(6, 18);
            this.lbRemesa.Name = "lbRemesa";
            this.lbRemesa.Size = new System.Drawing.Size(89, 13);
            this.lbRemesa.TabIndex = 47;
            this.lbRemesa.Text = "Número Remesa:";
            // 
            // frmInspeccionFolio
            // 
            this.ClientSize = new System.Drawing.Size(781, 368);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnTerminarInspeccion);
            this.Controls.Add(this.btnInspeccionFolio);
            this.Controls.Add(this.dataGridFolios);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(789, 395);
            this.Name = "frmInspeccionFolio";
            this.ShowIcon = false;
            this.Text = "C753 Masivos - Inspección de Folios";
            this.Load += new System.EventHandler(this.frmInspeccionFolio_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridFolios)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridFolios;
        private System.Windows.Forms.Button btnTerminarInspeccion;
        private System.Windows.Forms.Button btnInspeccionFolio;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbConsInspeccion;
        private System.Windows.Forms.Label lbConsInsp;
        private System.Windows.Forms.TextBox tbProcesoStatus;
        private System.Windows.Forms.TextBox tbRemesa;
        private System.Windows.Forms.Label lbProcesoStatus;
        private System.Windows.Forms.Label lbRemesa;
        private System.Windows.Forms.DataGridViewTextBoxColumn Folio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Inspeccionado;
    }
}