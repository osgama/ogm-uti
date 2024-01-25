using System;
using VB6 = Microsoft.VisualBasic.Compatibility.VB6.Support;

namespace Masivos
{
    partial class frmDecFolios
    {

        #region "Upgrade Support "
        public static frmDecFolios m_vb6FormDefInstance;
        private static bool m_InitializingDefInstance;
        public static frmDecFolios DefInstance
        {
            get
            {
                try
                {
                    if (m_vb6FormDefInstance == null || m_vb6FormDefInstance.IsDisposed)
                    {
                        m_InitializingDefInstance = true;
                        m_vb6FormDefInstance = CreateInstance();
                        m_vb6FormDefInstance.Closed += new EventHandler(m_vb6FormDefInstance.ReleaseResources);
                        m_InitializingDefInstance = false;
                    }
                }
                catch (Exception error)
                {
                    Console.WriteLine(error.StackTrace);
                }
                return m_vb6FormDefInstance;
            }
            set
            {
                m_vb6FormDefInstance = value;
            }
        }

        #endregion
        #region "Windows Form Designer generated code "
        public frmDecFolios()
            : base()
        {
            //AIS-Bug 9202 FSABORIO
            if (m_vb6FormDefInstance == null || m_vb6FormDefInstance.IsDisposed)
            {
                if (m_InitializingDefInstance)
                {
                    m_vb6FormDefInstance = this;
                }
                else
                {
                    try
                    {
                        //For the start-up form, the first instance created is the default instance.
                        if (System.Reflection.Assembly.GetExecutingAssembly().EntryPoint.DeclaringType == this.GetType())
                        {
                            m_vb6FormDefInstance = this;
                        }
                    }
                    catch
                    {
                    }
                }
            }
            //This call is required by the Windows Form Designer.
            InitializeComponent();
            InitializeLabel2();
            //This form is an MDI child.
            //This code simulates the VB6 
            // functionality of automatically
            // loading and showing an MDI
            // child's parent.
            this.MdiParent = Masivos.MDIMasivos.DefInstance;
            Masivos.MDIMasivos.DefInstance.Show();
        }
        public static frmDecFolios CreateInstance()
        {
            frmDecFolios theInstance = new frmDecFolios();
            theInstance.Form_Load();
            //The MDI form in the VB6 project had its
            //AutoShowChildren property set to True
            //To simulate the VB6 behavior, we need to
            //automatically Show the form whenever it
            //is loaded.  If you do not want this behavior
            //then delete the following line of code
            //UPGRADE_TODO: (2018) Remove the next line of code to stop form from automatically showing.
            theInstance.Show();
            return theInstance;
        }
        private void ReleaseResources(object eventSender, System.EventArgs eventArgs)
        {
            Dispose(true);
            m_vb6FormDefInstance = null;
            //AIS-Bug 8753 FSABORIO
            Artinsoft.VB6.Utils.MemoryHelper.ReleaseMemory();
        }
        //Form overrides dispose to clean up the component list.
        [System.Diagnostics.DebuggerNonUserCode]
        protected override void Dispose(bool Disposing)
        {
            if (Disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(Disposing);
        }
        //Required by the Windows Form Designer
        private System.ComponentModel.IContainer components;
        public System.Windows.Forms.ToolTip ToolTip1;
        public System.Windows.Forms.ToolStripMenuItem mnuAceptar;
        public System.Windows.Forms.ToolStripSeparator mnuSep1;
        public System.Windows.Forms.ToolStripMenuItem mnuCancelar;
        public System.Windows.Forms.ToolStripMenuItem mnuDecFolios;
        public System.Windows.Forms.MenuStrip MainMenu1;
        public System.Windows.Forms.TextBox txtFolFinal;
        public System.Windows.Forms.TextBox txtFolInicial;
        public System.Windows.Forms.TextBox txtNumSolicitudes;
        public System.Windows.Forms.TextBox txtRemesa;
        private System.Windows.Forms.Label _Label2_4;
        private System.Windows.Forms.Label _Label2_3;
        private System.Windows.Forms.Label _Label2_2;
        private System.Windows.Forms.Label _Label2_1;
        public System.Windows.Forms.GroupBox Frame3;
        public System.Windows.Forms.Button cmdCancelar;
        public System.Windows.Forms.Button cmdAceptar;
        public System.Windows.Forms.GroupBox Frame4;
        public AxTrueDBGrid80.AxTDBGrid tdbFoliosRemesa;
        public System.Windows.Forms.ComboBox cboCausaDec;
        public System.Windows.Forms.GroupBox cboRef;
        public System.Windows.Forms.Label[] Label2 = new System.Windows.Forms.Label[5];
        private Artinsoft.VB6.Gui.CommandButtonHelper commandButtonHelper1;
        //NOTE: The following procedure is required by the Windows Form Designer
        //It can be modified using the Windows Form Designer.
        //Do not modify it using the code editor.
        [System.Diagnostics.DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDecFolios));
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmdCancelar = new System.Windows.Forms.Button();
            this.cmdAceptar = new System.Windows.Forms.Button();
            this.MainMenu1 = new System.Windows.Forms.MenuStrip();
            this.mnuDecFolios = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAceptar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuCancelar = new System.Windows.Forms.ToolStripMenuItem();
            this.Frame3 = new System.Windows.Forms.GroupBox();
            this.txtFolFinal = new System.Windows.Forms.TextBox();
            this.txtFolInicial = new System.Windows.Forms.TextBox();
            this.txtNumSolicitudes = new System.Windows.Forms.TextBox();
            this.txtRemesa = new System.Windows.Forms.TextBox();
            this._Label2_4 = new System.Windows.Forms.Label();
            this._Label2_3 = new System.Windows.Forms.Label();
            this._Label2_2 = new System.Windows.Forms.Label();
            this._Label2_1 = new System.Windows.Forms.Label();
            this.Frame4 = new System.Windows.Forms.GroupBox();
            this.cboRef = new System.Windows.Forms.GroupBox();
            this.tdbFoliosRemesa = new AxTrueDBGrid80.AxTDBGrid();
            this.cboCausaDec = new System.Windows.Forms.ComboBox();
            this.commandButtonHelper1 = new Artinsoft.VB6.Gui.CommandButtonHelper(this.components);
            this.MainMenu1.SuspendLayout();
            this.Frame3.SuspendLayout();
            this.Frame4.SuspendLayout();
            this.cboRef.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tdbFoliosRemesa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandButtonHelper1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdCancelar
            // 
            this.cmdCancelar.BackColor = System.Drawing.SystemColors.Control;
            this.commandButtonHelper1.SetCorrectEventsBehavior(this.cmdCancelar, true);
            this.cmdCancelar.Cursor = System.Windows.Forms.Cursors.Default;
            this.commandButtonHelper1.SetDisabledPicture(this.cmdCancelar, null);
            this.commandButtonHelper1.SetDownPicture(this.cmdCancelar, null);
            this.cmdCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancelar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdCancelar.Image = ((System.Drawing.Image)(resources.GetObject("cmdCancelar.Image")));
            this.cmdCancelar.Location = new System.Drawing.Point(316, 15);
            this.commandButtonHelper1.SetMaskColor(this.cmdCancelar, System.Drawing.Color.Silver);
            this.cmdCancelar.Name = "cmdCancelar";
            this.cmdCancelar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdCancelar.Size = new System.Drawing.Size(59, 49);
            this.commandButtonHelper1.SetStyle(this.cmdCancelar, 1);
            this.cmdCancelar.TabIndex = 3;
            this.cmdCancelar.Tag = "";
            this.cmdCancelar.Text = "Cancelar";
            this.cmdCancelar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ToolTip1.SetToolTip(this.cmdCancelar, "Cancelar el proceso");
            this.cmdCancelar.UseVisualStyleBackColor = false;
            this.cmdCancelar.Click += new System.EventHandler(this.cmdCancelar_Click);
            // 
            // cmdAceptar
            // 
            this.cmdAceptar.BackColor = System.Drawing.SystemColors.Control;
            this.commandButtonHelper1.SetCorrectEventsBehavior(this.cmdAceptar, true);
            this.cmdAceptar.Cursor = System.Windows.Forms.Cursors.Default;
            this.commandButtonHelper1.SetDisabledPicture(this.cmdAceptar, null);
            this.commandButtonHelper1.SetDownPicture(this.cmdAceptar, null);
            this.cmdAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAceptar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdAceptar.Image = ((System.Drawing.Image)(resources.GetObject("cmdAceptar.Image")));
            this.cmdAceptar.Location = new System.Drawing.Point(236, 15);
            this.commandButtonHelper1.SetMaskColor(this.cmdAceptar, System.Drawing.Color.Silver);
            this.cmdAceptar.Name = "cmdAceptar";
            this.cmdAceptar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdAceptar.Size = new System.Drawing.Size(59, 49);
            this.commandButtonHelper1.SetStyle(this.cmdAceptar, 1);
            this.cmdAceptar.TabIndex = 2;
            this.cmdAceptar.Tag = "";
            this.cmdAceptar.Text = "Aceptar";
            this.cmdAceptar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdAceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ToolTip1.SetToolTip(this.cmdAceptar, "Aceptar el proceso");
            this.cmdAceptar.UseVisualStyleBackColor = false;
            this.cmdAceptar.Click += new System.EventHandler(this.cmdAceptar_Click);
            // 
            // MainMenu1
            // 
            this.MainMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDecFolios});
            this.MainMenu1.Location = new System.Drawing.Point(0, 0);
            this.MainMenu1.Name = "MainMenu1";
            this.MainMenu1.Size = new System.Drawing.Size(653, 24);
            this.MainMenu1.TabIndex = 5;
            // 
            // mnuDecFolios
            // 
            this.mnuDecFolios.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAceptar,
            this.mnuSep1,
            this.mnuCancelar});
            this.mnuDecFolios.Name = "mnuDecFolios";
            this.mnuDecFolios.Size = new System.Drawing.Size(121, 20);
            this.mnuDecFolios.Tag = "";
            this.mnuDecFolios.Text = "&Declinación por Folios";
            // 
            // mnuAceptar
            // 
            this.mnuAceptar.Name = "mnuAceptar";
            this.mnuAceptar.Size = new System.Drawing.Size(127, 22);
            this.mnuAceptar.Tag = "";
            this.mnuAceptar.Text = "&Aceptar";
            this.mnuAceptar.Click += new System.EventHandler(this.mnuAceptar_Click);
            // 
            // mnuSep1
            // 
            this.mnuSep1.Name = "mnuSep1";
            this.mnuSep1.Size = new System.Drawing.Size(124, 6);
            this.mnuSep1.Tag = "";
            // 
            // mnuCancelar
            // 
            this.mnuCancelar.Name = "mnuCancelar";
            this.mnuCancelar.Size = new System.Drawing.Size(127, 22);
            this.mnuCancelar.Tag = "";
            this.mnuCancelar.Text = "&Cancelar";
            this.mnuCancelar.Click += new System.EventHandler(this.mnuCancelar_Click);
            // 
            // Frame3
            // 
            this.Frame3.BackColor = System.Drawing.SystemColors.Control;
            this.Frame3.Controls.Add(this.txtFolFinal);
            this.Frame3.Controls.Add(this.txtFolInicial);
            this.Frame3.Controls.Add(this.txtNumSolicitudes);
            this.Frame3.Controls.Add(this.txtRemesa);
            this.Frame3.Controls.Add(this._Label2_4);
            this.Frame3.Controls.Add(this._Label2_3);
            this.Frame3.Controls.Add(this._Label2_2);
            this.Frame3.Controls.Add(this._Label2_1);
            this.Frame3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Frame3.ForeColor = System.Drawing.Color.Blue;
            this.Frame3.Location = new System.Drawing.Point(9, 24);
            this.Frame3.Name = "Frame3";
            this.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Frame3.Size = new System.Drawing.Size(635, 89);
            this.Frame3.TabIndex = 4;
            this.Frame3.TabStop = false;
            this.Frame3.Tag = "";
            this.Frame3.Text = "Información de la Remesa";
            // 
            // txtFolFinal
            // 
            this.txtFolFinal.AcceptsReturn = true;
            this.txtFolFinal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtFolFinal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFolFinal.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFolFinal.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFolFinal.ForeColor = System.Drawing.Color.Maroon;
            this.txtFolFinal.Location = new System.Drawing.Point(342, 56);
            this.txtFolFinal.MaxLength = 35;
            this.txtFolFinal.Name = "txtFolFinal";
            this.txtFolFinal.ReadOnly = true;
            this.txtFolFinal.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtFolFinal.Size = new System.Drawing.Size(153, 20);
            this.txtFolFinal.TabIndex = 8;
            this.txtFolFinal.TabStop = false;
            this.txtFolFinal.Tag = "";
            this.txtFolFinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtFolInicial
            // 
            this.txtFolInicial.AcceptsReturn = true;
            this.txtFolInicial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtFolInicial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFolInicial.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFolInicial.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFolInicial.ForeColor = System.Drawing.Color.Maroon;
            this.txtFolInicial.Location = new System.Drawing.Point(96, 56);
            this.txtFolInicial.MaxLength = 35;
            this.txtFolInicial.Name = "txtFolInicial";
            this.txtFolInicial.ReadOnly = true;
            this.txtFolInicial.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtFolInicial.Size = new System.Drawing.Size(153, 20);
            this.txtFolInicial.TabIndex = 7;
            this.txtFolInicial.TabStop = false;
            this.txtFolInicial.Tag = "";
            this.txtFolInicial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtNumSolicitudes
            // 
            this.txtNumSolicitudes.AcceptsReturn = true;
            this.txtNumSolicitudes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtNumSolicitudes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNumSolicitudes.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNumSolicitudes.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumSolicitudes.ForeColor = System.Drawing.Color.Maroon;
            this.txtNumSolicitudes.Location = new System.Drawing.Point(342, 21);
            this.txtNumSolicitudes.MaxLength = 35;
            this.txtNumSolicitudes.Name = "txtNumSolicitudes";
            this.txtNumSolicitudes.ReadOnly = true;
            this.txtNumSolicitudes.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtNumSolicitudes.Size = new System.Drawing.Size(153, 20);
            this.txtNumSolicitudes.TabIndex = 6;
            this.txtNumSolicitudes.TabStop = false;
            this.txtNumSolicitudes.Tag = "";
            this.txtNumSolicitudes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtRemesa
            // 
            this.txtRemesa.AcceptsReturn = true;
            this.txtRemesa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtRemesa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemesa.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtRemesa.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemesa.ForeColor = System.Drawing.Color.Maroon;
            this.txtRemesa.Location = new System.Drawing.Point(96, 27);
            this.txtRemesa.MaxLength = 35;
            this.txtRemesa.Name = "txtRemesa";
            this.txtRemesa.ReadOnly = true;
            this.txtRemesa.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtRemesa.Size = new System.Drawing.Size(153, 20);
            this.txtRemesa.TabIndex = 5;
            this.txtRemesa.TabStop = false;
            this.txtRemesa.Tag = "";
            this.txtRemesa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // _Label2_4
            // 
            this._Label2_4.BackColor = System.Drawing.SystemColors.Control;
            this._Label2_4.Cursor = System.Windows.Forms.Cursors.Default;
            this._Label2_4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._Label2_4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._Label2_4.ForeColor = System.Drawing.SystemColors.ControlText;
            this._Label2_4.Location = new System.Drawing.Point(272, 64);
            this._Label2_4.Name = "_Label2_4";
            this._Label2_4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._Label2_4.Size = new System.Drawing.Size(73, 17);
            this._Label2_4.TabIndex = 12;
            this._Label2_4.Tag = "";
            this._Label2_4.Text = "Folio Final:";
            // 
            // _Label2_3
            // 
            this._Label2_3.BackColor = System.Drawing.SystemColors.Control;
            this._Label2_3.Cursor = System.Windows.Forms.Cursors.Default;
            this._Label2_3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._Label2_3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._Label2_3.ForeColor = System.Drawing.SystemColors.ControlText;
            this._Label2_3.Location = new System.Drawing.Point(16, 63);
            this._Label2_3.Name = "_Label2_3";
            this._Label2_3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._Label2_3.Size = new System.Drawing.Size(89, 17);
            this._Label2_3.TabIndex = 11;
            this._Label2_3.Tag = "";
            this._Label2_3.Text = "Folio Inicial:";
            // 
            // _Label2_2
            // 
            this._Label2_2.BackColor = System.Drawing.SystemColors.Control;
            this._Label2_2.Cursor = System.Windows.Forms.Cursors.Default;
            this._Label2_2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._Label2_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._Label2_2.ForeColor = System.Drawing.SystemColors.ControlText;
            this._Label2_2.Location = new System.Drawing.Point(272, 24);
            this._Label2_2.Name = "_Label2_2";
            this._Label2_2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._Label2_2.Size = new System.Drawing.Size(65, 33);
            this._Label2_2.TabIndex = 10;
            this._Label2_2.Tag = "";
            this._Label2_2.Text = "No. de Solicitudes:";
            // 
            // _Label2_1
            // 
            this._Label2_1.BackColor = System.Drawing.SystemColors.Control;
            this._Label2_1.Cursor = System.Windows.Forms.Cursors.Default;
            this._Label2_1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._Label2_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._Label2_1.ForeColor = System.Drawing.SystemColors.ControlText;
            this._Label2_1.Location = new System.Drawing.Point(16, 24);
            this._Label2_1.Name = "_Label2_1";
            this._Label2_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._Label2_1.Size = new System.Drawing.Size(65, 17);
            this._Label2_1.TabIndex = 9;
            this._Label2_1.Tag = "";
            this._Label2_1.Text = "Remesa:";
            // 
            // Frame4
            // 
            this.Frame4.BackColor = System.Drawing.SystemColors.Control;
            this.Frame4.Controls.Add(this.cmdCancelar);
            this.Frame4.Controls.Add(this.cmdAceptar);
            this.Frame4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Frame4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Frame4.Location = new System.Drawing.Point(8, 344);
            this.Frame4.Name = "Frame4";
            this.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Frame4.Size = new System.Drawing.Size(635, 73);
            this.Frame4.TabIndex = 1;
            this.Frame4.TabStop = false;
            this.Frame4.Tag = "";
            // 
            // cboRef
            // 
            this.cboRef.BackColor = System.Drawing.SystemColors.Control;
            this.cboRef.Controls.Add(this.tdbFoliosRemesa);
            this.cboRef.Controls.Add(this.cboCausaDec);
            this.cboRef.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboRef.ForeColor = System.Drawing.Color.Blue;
            this.cboRef.Location = new System.Drawing.Point(8, 120);
            this.cboRef.Name = "cboRef";
            this.cboRef.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cboRef.Size = new System.Drawing.Size(635, 225);
            this.cboRef.TabIndex = 0;
            this.cboRef.TabStop = false;
            this.cboRef.Tag = "";
            this.cboRef.Text = "Folios de la Remesa";
            // 
            // tdbFoliosRemesa
            // 
            this.tdbFoliosRemesa.Location = new System.Drawing.Point(8, 15);
            this.tdbFoliosRemesa.Name = "tdbFoliosRemesa";
            this.tdbFoliosRemesa.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("tdbFoliosRemesa.OcxState")));
            this.tdbFoliosRemesa.Size = new System.Drawing.Size(613, 203);
            this.tdbFoliosRemesa.TabIndex = 14;
            this.tdbFoliosRemesa.Tag = "";
            this.tdbFoliosRemesa.KeyPressEvent += new AxTrueDBGrid80.TrueDBGridEvents_KeyPressEventHandler(this.tdbFoliosRemesa_KeyPressEvent);
            this.tdbFoliosRemesa.RowColChange += new AxTrueDBGrid80.TrueDBGridEvents_RowColChangeEventHandler(this.tdbFoliosRemesa_RowColChange);
            // 
            // cboCausaDec
            // 
            this.cboCausaDec.BackColor = System.Drawing.SystemColors.Window;
            this.cboCausaDec.Cursor = System.Windows.Forms.Cursors.Default;
            this.cboCausaDec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCausaDec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCausaDec.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboCausaDec.Location = new System.Drawing.Point(348, 30);
            this.cboCausaDec.Name = "cboCausaDec";
            this.cboCausaDec.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cboCausaDec.Size = new System.Drawing.Size(112, 21);
            this.cboCausaDec.TabIndex = 13;
            this.cboCausaDec.Tag = "";
            // 
            // frmDecFolios
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(653, 423);
            this.ControlBox = false;
            this.Controls.Add(this.Frame3);
            this.Controls.Add(this.Frame4);
            this.Controls.Add(this.cboRef);
            this.Controls.Add(this.MainMenu1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(10, 29);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDecFolios";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "";
            this.Text = "Declinación por Folios";
            this.Closed += new System.EventHandler(this.frmDecFolios_Closed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDecFolios_FormClosing);
            this.MainMenu1.ResumeLayout(false);
            this.MainMenu1.PerformLayout();
            this.Frame3.ResumeLayout(false);
            this.Frame3.PerformLayout();
            this.Frame4.ResumeLayout(false);
            this.cboRef.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tdbFoliosRemesa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandButtonHelper1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        void InitializeLabel2()
        {
            this.Label2[4] = _Label2_4;
            this.Label2[3] = _Label2_3;
            this.Label2[2] = _Label2_2;
            this.Label2[1] = _Label2_1;
        }
        #endregion
    }
}