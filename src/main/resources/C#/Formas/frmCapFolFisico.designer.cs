namespace Masivos
{
    partial class frmCapFolFisico
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
            this.btnAceptarCaptFisico = new System.Windows.Forms.Button();
            this.mtbFolios = new System.Windows.Forms.MaskedTextBox();
            this.mtbRemesa = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAceptarCaptFisico
            // 
            this.btnAceptarCaptFisico.Location = new System.Drawing.Point(250, 41);
            this.btnAceptarCaptFisico.Name = "btnAceptarCaptFisico";
            this.btnAceptarCaptFisico.Size = new System.Drawing.Size(125, 28);
            this.btnAceptarCaptFisico.TabIndex = 3;
            this.btnAceptarCaptFisico.Text = "&Aceptar";
            this.btnAceptarCaptFisico.UseVisualStyleBackColor = true;
            this.btnAceptarCaptFisico.Click += new System.EventHandler(this.button1_Click);
            // 
            // mtbFolios
            // 
            this.mtbFolios.Location = new System.Drawing.Point(137, 46);
            this.mtbFolios.Name = "mtbFolios";
            this.mtbFolios.PromptChar = ' ';
            this.mtbFolios.Size = new System.Drawing.Size(83, 20);
            this.mtbFolios.TabIndex = 2;
            this.mtbFolios.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mtbFolios_KeyPress);
            // 
            // mtbRemesa
            // 
            this.mtbRemesa.Location = new System.Drawing.Point(137, 13);
            this.mtbRemesa.Name = "mtbRemesa";
            this.mtbRemesa.ReadOnly = true;
            this.mtbRemesa.Size = new System.Drawing.Size(238, 20);
            this.mtbRemesa.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(28, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Número Remesa:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Número de &Folios:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.mtbRemesa);
            this.groupBox1.Controls.Add(this.mtbFolios);
            this.groupBox1.Controls.Add(this.btnAceptarCaptFisico);
            this.groupBox1.Location = new System.Drawing.Point(10, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(381, 87);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // frmCapFolFisico
            // 
            this.AcceptButton = this.btnAceptarCaptFisico;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 104);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(408, 131);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(408, 131);
            this.Name = "frmCapFolFisico";
            this.ShowIcon = false;
            this.Text = "C753 Masivos - Validación Remesa - Captura Físico";
            this.Load += new System.EventHandler(this.frmCapFolFisico_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAceptarCaptFisico;
        private System.Windows.Forms.MaskedTextBox mtbFolios;
        private System.Windows.Forms.MaskedTextBox mtbRemesa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}