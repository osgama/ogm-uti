namespace Masivos
{
    partial class frmErroresValidacion
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
            this.mtbStatus = new System.Windows.Forms.MaskedTextBox();
            this.mtbProceso = new System.Windows.Forms.MaskedTextBox();
            this.mtbRemesa = new System.Windows.Forms.MaskedTextBox();
            this.dgvErroresValidacion = new System.Windows.Forms.DataGridView();
            this.NumRem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Proc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.camp2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.err2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.camp3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.err3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.camp4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.err4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.camp5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.err5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnArchivo = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgvErroresValidacion)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mtbStatus
            // 
            this.mtbStatus.Location = new System.Drawing.Point(598, 31);
            this.mtbStatus.Name = "mtbStatus";
            this.mtbStatus.ReadOnly = true;
            this.mtbStatus.Size = new System.Drawing.Size(129, 20);
            this.mtbStatus.TabIndex = 6;
            // 
            // mtbProceso
            // 
            this.mtbProceso.Location = new System.Drawing.Point(394, 31);
            this.mtbProceso.Name = "mtbProceso";
            this.mtbProceso.ReadOnly = true;
            this.mtbProceso.Size = new System.Drawing.Size(129, 20);
            this.mtbProceso.TabIndex = 4;
            // 
            // mtbRemesa
            // 
            this.mtbRemesa.Location = new System.Drawing.Point(92, 31);
            this.mtbRemesa.Name = "mtbRemesa";
            this.mtbRemesa.ReadOnly = true;
            this.mtbRemesa.Size = new System.Drawing.Size(193, 20);
            this.mtbRemesa.TabIndex = 2;
            // 
            // dgvErroresValidacion
            // 
            this.dgvErroresValidacion.AllowUserToAddRows = false;
            this.dgvErroresValidacion.AllowUserToDeleteRows = false;
            this.dgvErroresValidacion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvErroresValidacion.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvErroresValidacion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvErroresValidacion.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NumRem,
            this.Proc,
            this.Stat,
            this.camp2,
            this.err2,
            this.camp3,
            this.err3,
            this.camp4,
            this.err4,
            this.camp5,
            this.err5});
            this.dgvErroresValidacion.Location = new System.Drawing.Point(12, 91);
            this.dgvErroresValidacion.Name = "dgvErroresValidacion";
            this.dgvErroresValidacion.ReadOnly = true;
            this.dgvErroresValidacion.RowHeadersVisible = false;
            this.dgvErroresValidacion.Size = new System.Drawing.Size(768, 441);
            this.dgvErroresValidacion.TabIndex = 7;
            // 
            // NumRem
            // 
            this.NumRem.HeaderText = "Folio";
            this.NumRem.Name = "NumRem";
            this.NumRem.ReadOnly = true;
            this.NumRem.Width = 54;
            // 
            // Proc
            // 
            this.Proc.HeaderText = "Campo1";
            this.Proc.Name = "Proc";
            this.Proc.ReadOnly = true;
            this.Proc.Width = 71;
            // 
            // Stat
            // 
            this.Stat.HeaderText = "Error1";
            this.Stat.Name = "Stat";
            this.Stat.ReadOnly = true;
            this.Stat.Width = 60;
            // 
            // camp2
            // 
            this.camp2.HeaderText = "Campo2";
            this.camp2.Name = "camp2";
            this.camp2.ReadOnly = true;
            this.camp2.Width = 71;
            // 
            // err2
            // 
            this.err2.HeaderText = "Error2";
            this.err2.Name = "err2";
            this.err2.ReadOnly = true;
            this.err2.Width = 60;
            // 
            // camp3
            // 
            this.camp3.HeaderText = "Campo3";
            this.camp3.Name = "camp3";
            this.camp3.ReadOnly = true;
            this.camp3.Width = 71;
            // 
            // err3
            // 
            this.err3.HeaderText = "Error3";
            this.err3.Name = "err3";
            this.err3.ReadOnly = true;
            this.err3.Width = 60;
            // 
            // camp4
            // 
            this.camp4.HeaderText = "Campo4";
            this.camp4.Name = "camp4";
            this.camp4.ReadOnly = true;
            this.camp4.Width = 71;
            // 
            // err4
            // 
            this.err4.HeaderText = "Error4";
            this.err4.Name = "err4";
            this.err4.ReadOnly = true;
            this.err4.Width = 60;
            // 
            // camp5
            // 
            this.camp5.HeaderText = "Campo5";
            this.camp5.Name = "camp5";
            this.camp5.ReadOnly = true;
            this.camp5.Width = 71;
            // 
            // err5
            // 
            this.err5.HeaderText = "Error5";
            this.err5.Name = "err5";
            this.err5.ReadOnly = true;
            this.err5.Width = 60;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.mtbStatus);
            this.groupBox1.Controls.Add(this.mtbProceso);
            this.groupBox1.Controls.Add(this.mtbRemesa);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(768, 73);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(30, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Remesa:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(545, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Status:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(331, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Proceso:";
            // 
            // btnSalir
            // 
            this.btnSalir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalir.Location = new System.Drawing.Point(610, 538);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(170, 23);
            this.btnSalir.TabIndex = 9;
            this.btnSalir.Text = "&Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnArchivo
            // 
            this.btnArchivo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnArchivo.Location = new System.Drawing.Point(292, 538);
            this.btnArchivo.Name = "btnArchivo";
            this.btnArchivo.Size = new System.Drawing.Size(312, 23);
            this.btnArchivo.TabIndex = 8;
            this.btnArchivo.Text = "Generar Archivo";
            this.btnArchivo.UseVisualStyleBackColor = true;
            this.btnArchivo.Click += new System.EventHandler(this.btnArchivo_Click);
            // 
            // frmErroresValidacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.btnArchivo);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvErroresValidacion);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "frmErroresValidacion";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "C753 Masivos - Validación Remesa - Errores";
            this.Load += new System.EventHandler(this.frmErrores_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvErroresValidacion)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox mtbStatus;
        private System.Windows.Forms.MaskedTextBox mtbProceso;
        private System.Windows.Forms.MaskedTextBox mtbRemesa;
        private System.Windows.Forms.DataGridView dgvErroresValidacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumRem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Proc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stat;
        private System.Windows.Forms.DataGridViewTextBoxColumn camp2;
        private System.Windows.Forms.DataGridViewTextBoxColumn err2;
        private System.Windows.Forms.DataGridViewTextBoxColumn camp3;
        private System.Windows.Forms.DataGridViewTextBoxColumn err3;
        private System.Windows.Forms.DataGridViewTextBoxColumn camp4;
        private System.Windows.Forms.DataGridViewTextBoxColumn err4;
        private System.Windows.Forms.DataGridViewTextBoxColumn camp5;
        private System.Windows.Forms.DataGridViewTextBoxColumn err5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnArchivo;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}