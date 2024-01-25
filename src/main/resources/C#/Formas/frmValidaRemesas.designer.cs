namespace Masivos
{
    partial class frmValidaRemesas
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
            this.dgvValRem = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCapt = new System.Windows.Forms.Button();
            this.btnError = new System.Windows.Forms.Button();
            this.btnValida = new System.Windows.Forms.Button();
            this.lbEstatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvValRem)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvValRem
            // 
            this.dgvValRem.AllowUserToAddRows = false;
            this.dgvValRem.AllowUserToDeleteRows = false;
            this.dgvValRem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvValRem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dgvValRem.Location = new System.Drawing.Point(12, 2);
            this.dgvValRem.MultiSelect = false;
            this.dgvValRem.Name = "dgvValRem";
            this.dgvValRem.ReadOnly = true;
            this.dgvValRem.RowHeadersVisible = false;
            this.dgvValRem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvValRem.Size = new System.Drawing.Size(464, 407);
            this.dgvValRem.TabIndex = 0;
            this.dgvValRem.CurrentCellChanged += new System.EventHandler(this.dgvValRem_CurrentCellChanged);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Numero de Remesa";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 150;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Proceso";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 150;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Estatus";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 150;
            // 
            // btnCapt
            // 
            this.btnCapt.Location = new System.Drawing.Point(351, 415);
            this.btnCapt.Name = "btnCapt";
            this.btnCapt.Size = new System.Drawing.Size(125, 32);
            this.btnCapt.TabIndex = 3;
            this.btnCapt.Text = "Captura Fisico";
            this.btnCapt.UseVisualStyleBackColor = true;
            this.btnCapt.Click += new System.EventHandler(this.btnCapt_Click);
            // 
            // btnError
            // 
            this.btnError.Location = new System.Drawing.Point(184, 415);
            this.btnError.Name = "btnError";
            this.btnError.Size = new System.Drawing.Size(125, 32);
            this.btnError.TabIndex = 2;
            this.btnError.Text = "Ver Errores";
            this.btnError.UseVisualStyleBackColor = true;
            this.btnError.Click += new System.EventHandler(this.btnError_Click);
            // 
            // btnValida
            // 
            this.btnValida.Location = new System.Drawing.Point(12, 415);
            this.btnValida.Name = "btnValida";
            this.btnValida.Size = new System.Drawing.Size(125, 32);
            this.btnValida.TabIndex = 1;
            this.btnValida.Text = "Validar";
            this.btnValida.UseVisualStyleBackColor = true;
            this.btnValida.Click += new System.EventHandler(this.btnValida_Click);
            // 
            // lbEstatus
            // 
            this.lbEstatus.AutoSize = true;
            this.lbEstatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEstatus.ForeColor = System.Drawing.Color.Red;
            this.lbEstatus.Location = new System.Drawing.Point(12, 387);
            this.lbEstatus.Name = "lbEstatus";
            this.lbEstatus.Size = new System.Drawing.Size(119, 13);
            this.lbEstatus.TabIndex = 4;
            this.lbEstatus.Text = "Seleccione Remesa";
            // 
            // frmValidaRemesas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 459);
            this.Controls.Add(this.btnCapt);
            this.Controls.Add(this.btnError);
            this.Controls.Add(this.btnValida);
            this.Controls.Add(this.dgvValRem);
            this.Controls.Add(this.lbEstatus);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(496, 486);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(496, 457);
            this.Name = "frmValidaRemesas";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "C753 Masivos - Validación Remesa";
            this.Load += new System.EventHandler(this.frmValidaRemesas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvValRem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvValRem;
        private System.Windows.Forms.Button btnCapt;
        private System.Windows.Forms.Button btnError;
        private System.Windows.Forms.Button btnValida;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.Label lbEstatus;
    }
}

