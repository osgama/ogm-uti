namespace Masivos
{
    partial class frmArriboRemesas
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbPromotora = new System.Windows.Forms.ComboBox();
            this.cmbEmpCaptura = new System.Windows.Forms.ComboBox();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.txbArchControl = new System.Windows.Forms.TextBox();
            this.btnArchControl = new System.Windows.Forms.Button();
            this.opnfdArchivoControl = new System.Windows.Forms.OpenFileDialog();
            this.lbError = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpFechaAceptacion = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.dtpFechaIngreso = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbPromocion = new System.Windows.Forms.ComboBox();
            this.dtpFechaProceso = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbEntidad = new System.Windows.Forms.ComboBox();
            this.cmbTipoEntidad = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(77, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = " Empresa &Promotora:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(88, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = " Empresa &Captura:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(85, 244);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "&Archivo de Control:";
            // 
            // cmbPromotora
            // 
            this.cmbPromotora.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPromotora.FormattingEnabled = true;
            this.cmbPromotora.Location = new System.Drawing.Point(188, 28);
            this.cmbPromotora.Name = "cmbPromotora";
            this.cmbPromotora.Size = new System.Drawing.Size(271, 21);
            this.cmbPromotora.TabIndex = 2;
            // 
            // cmbEmpCaptura
            // 
            this.cmbEmpCaptura.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmpCaptura.FormattingEnabled = true;
            this.cmbEmpCaptura.Location = new System.Drawing.Point(188, 55);
            this.cmbEmpCaptura.Name = "cmbEmpCaptura";
            this.cmbEmpCaptura.Size = new System.Drawing.Size(271, 21);
            this.cmbEmpCaptura.TabIndex = 4;
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.Location = new System.Drawing.Point(6, 328);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(453, 28);
            this.btnRegistrar.TabIndex = 20;
            this.btnRegistrar.Text = "&Registrar";
            this.btnRegistrar.UseVisualStyleBackColor = true;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // txbArchControl
            // 
            this.txbArchControl.Location = new System.Drawing.Point(188, 241);
            this.txbArchControl.Multiline = true;
            this.txbArchControl.Name = "txbArchControl";
            this.txbArchControl.ReadOnly = true;
            this.txbArchControl.Size = new System.Drawing.Size(271, 20);
            this.txbArchControl.TabIndex = 18;
            // 
            // btnArchControl
            // 
            this.btnArchControl.Location = new System.Drawing.Point(358, 267);
            this.btnArchControl.Name = "btnArchControl";
            this.btnArchControl.Size = new System.Drawing.Size(101, 23);
            this.btnArchControl.TabIndex = 19;
            this.btnArchControl.Text = "Examinar";
            this.btnArchControl.UseVisualStyleBackColor = true;
            this.btnArchControl.Click += new System.EventHandler(this.btnArchControl_Click);
            // 
            // opnfdArchivoControl
            // 
            this.opnfdArchivoControl.FileOk += new System.ComponentModel.CancelEventHandler(this.opnfdArchivoControl_FileOk);
            // 
            // lbError
            // 
            this.lbError.AutoSize = true;
            this.lbError.Location = new System.Drawing.Point(52, 13);
            this.lbError.Name = "lbError";
            this.lbError.Size = new System.Drawing.Size(0, 13);
            this.lbError.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpFechaAceptacion);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.btnRegistrar);
            this.groupBox1.Controls.Add(this.dtpFechaIngreso);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cmbPromocion);
            this.groupBox1.Controls.Add(this.dtpFechaProceso);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cmbEntidad);
            this.groupBox1.Controls.Add(this.cmbTipoEntidad);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnArchControl);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbPromotora);
            this.groupBox1.Controls.Add(this.txbArchControl);
            this.groupBox1.Controls.Add(this.cmbEmpCaptura);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(465, 362);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Arribo de Remesas";
            // 
            // dtpFechaAceptacion
            // 
            this.dtpFechaAceptacion.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaAceptacion.Location = new System.Drawing.Point(188, 215);
            this.dtpFechaAceptacion.Name = "dtpFechaAceptacion";
            this.dtpFechaAceptacion.Size = new System.Drawing.Size(101, 20);
            this.dtpFechaAceptacion.TabIndex = 16;
            this.dtpFechaAceptacion.ValueChanged += new System.EventHandler(this.dtpFechaAceptacion_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 218);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(163, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Fecha de Aceptación de Crédito:";
            // 
            // dtpFechaIngreso
            // 
            this.dtpFechaIngreso.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIngreso.Location = new System.Drawing.Point(188, 189);
            this.dtpFechaIngreso.Name = "dtpFechaIngreso";
            this.dtpFechaIngreso.Size = new System.Drawing.Size(101, 20);
            this.dtpFechaIngreso.TabIndex = 14;
            this.dtpFechaIngreso.ValueChanged += new System.EventHandler(this.dtpFechaIngreso_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(44, 192);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(138, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Fecha de Ingreso a Crédito:";
            // 
            // cmbPromocion
            // 
            this.cmbPromocion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPromocion.FormattingEnabled = true;
            this.cmbPromocion.Location = new System.Drawing.Point(188, 136);
            this.cmbPromocion.Name = "cmbPromocion";
            this.cmbPromocion.Size = new System.Drawing.Size(271, 21);
            this.cmbPromocion.TabIndex = 10;
            // 
            // dtpFechaProceso
            // 
            this.dtpFechaProceso.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaProceso.Location = new System.Drawing.Point(188, 163);
            this.dtpFechaProceso.Name = "dtpFechaProceso";
            this.dtpFechaProceso.Size = new System.Drawing.Size(101, 20);
            this.dtpFechaProceso.TabIndex = 12;
            this.dtpFechaProceso.ValueChanged += new System.EventHandler(this.dtpFechaProceso_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(122, 139);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "P&romoción:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(85, 166);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Fecha de Proceso:";
            // 
            // cmbEntidad
            // 
            this.cmbEntidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEntidad.FormattingEnabled = true;
            this.cmbEntidad.Location = new System.Drawing.Point(188, 109);
            this.cmbEntidad.Name = "cmbEntidad";
            this.cmbEntidad.Size = new System.Drawing.Size(271, 21);
            this.cmbEntidad.TabIndex = 8;
            // 
            // cmbTipoEntidad
            // 
            this.cmbTipoEntidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoEntidad.FormattingEnabled = true;
            this.cmbTipoEntidad.Location = new System.Drawing.Point(188, 82);
            this.cmbTipoEntidad.Name = "cmbTipoEntidad";
            this.cmbTipoEntidad.Size = new System.Drawing.Size(271, 21);
            this.cmbTipoEntidad.TabIndex = 6;
            this.cmbTipoEntidad.SelectedIndexChanged += new System.EventHandler(this.cmbTipoEntidad_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(102, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "&Entidad Origen:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(63, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "&Tipo de Entidad Origen:";
            // 
            // frmArriboRemesas
            // 
            this.AcceptButton = this.btnRegistrar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 385);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbError);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(497, 412);
            this.MinimizeBox = false;
            this.Name = "frmArriboRemesas";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "C753 Masivos - Arribo Remesas";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmArriboRemesas_FormClosing);
            this.Load += new System.EventHandler(this.frmArriboRemesas_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbPromotora;
        private System.Windows.Forms.ComboBox cmbEmpCaptura;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.TextBox txbArchControl;
        private System.Windows.Forms.Button btnArchControl;
        private System.Windows.Forms.OpenFileDialog opnfdArchivoControl;
        private System.Windows.Forms.Label lbError;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbEntidad;
        private System.Windows.Forms.ComboBox cmbTipoEntidad;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbPromocion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpFechaProceso;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpFechaIngreso;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpFechaAceptacion;
        private System.Windows.Forms.Label label9;
    }
}

