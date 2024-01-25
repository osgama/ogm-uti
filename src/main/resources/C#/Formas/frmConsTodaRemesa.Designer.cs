using System;
using VB6 = Microsoft.VisualBasic.Compatibility.VB6.Support;

namespace Masivos
{
    partial class frmConsTodaRemesa
    {

        #region "Upgrade Support "
        public static frmConsTodaRemesa m_vb6FormDefInstance;
        private static bool m_InitializingDefInstance;
        public static frmConsTodaRemesa DefInstance
        {
            get
            {
                if (m_vb6FormDefInstance == null || m_vb6FormDefInstance.IsDisposed)
                {
                    m_InitializingDefInstance = true;
                    m_vb6FormDefInstance = CreateInstance();
                    m_vb6FormDefInstance.Closed += new EventHandler(m_vb6FormDefInstance.ReleaseResources);
                    m_InitializingDefInstance = false;
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
        public frmConsTodaRemesa()
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
            InitializeLabel1();
            //This form is an MDI child.
            //This code simulates the VB6 
            // functionality of automatically
            // loading and showing an MDI
            // child's parent.
            this.MdiParent = Masivos.MDIMasivos.DefInstance;
            Masivos.MDIMasivos.DefInstance.Show();
            Form_Initialize_Renamed();
        }
        public static frmConsTodaRemesa CreateInstance()
        {
            frmConsTodaRemesa theInstance = new frmConsTodaRemesa();
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
        public System.Windows.Forms.ToolStripMenuItem mnuGenArchivo;
        public System.Windows.Forms.ToolStripMenuItem mnuLimpiar;
        public System.Windows.Forms.ToolStripSeparator mnuSpe1;
        public System.Windows.Forms.ToolStripMenuItem mnuSalir;
        public System.Windows.Forms.ToolStripMenuItem mnuDecFolios;
        public System.Windows.Forms.MenuStrip MainMenu1;
        public System.Windows.Forms.Button cmdLimpiar;
        public System.Windows.Forms.Button cmdSalir;
        public System.Windows.Forms.Button cmdGenArch;
        public System.Windows.Forms.GroupBox Frame3;
        public System.Windows.Forms.TextBox txtClaveRemesa;
        private System.Windows.Forms.Label _Label1_0;
        public System.Windows.Forms.GroupBox Frame5;
        public AxTrueDBGrid80.AxTDBGrid tdbInfRemesa;
        public System.Windows.Forms.Label lblTotal;
        public System.Windows.Forms.GroupBox Frame2;
        public System.Windows.Forms.Label[] Label1 = new System.Windows.Forms.Label[1];
        private Artinsoft.VB6.Gui.CommandButtonHelper commandButtonHelper1;
        //NOTE: The following procedure is required by the Windows Form Designer
        //It can be modified using the Windows Form Designer.
        //Do not modify it using the code editor.
        [System.Diagnostics.DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConsTodaRemesa));
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmdLimpiar = new System.Windows.Forms.Button();
            this.cmdSalir = new System.Windows.Forms.Button();
            this.cmdGenArch = new System.Windows.Forms.Button();
            this.MainMenu1 = new System.Windows.Forms.MenuStrip();
            this.mnuDecFolios = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGenArchivo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLimpiar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSpe1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSalir = new System.Windows.Forms.ToolStripMenuItem();
            this.dummy1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dummy2 = new System.Windows.Forms.ToolStripMenuItem();
            this.Frame3 = new System.Windows.Forms.GroupBox();
            this.Frame5 = new System.Windows.Forms.GroupBox();
            this.txtClaveRemesa = new System.Windows.Forms.TextBox();
            this._Label1_0 = new System.Windows.Forms.Label();
            this.Frame2 = new System.Windows.Forms.GroupBox();
            this.tdbInfRemesa = new AxTrueDBGrid80.AxTDBGrid();
            this.lblTotal = new System.Windows.Forms.Label();
            this.commandButtonHelper1 = new Artinsoft.VB6.Gui.CommandButtonHelper(this.components);
            this.formHelper1 = new Artinsoft.VB6.Gui.FormHelper(this.components);
            this.MainMenu1.SuspendLayout();
            this.Frame3.SuspendLayout();
            this.Frame5.SuspendLayout();
            this.Frame2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tdbInfRemesa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandButtonHelper1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdLimpiar
            // 
            this.cmdLimpiar.BackColor = System.Drawing.SystemColors.Control;
            this.commandButtonHelper1.SetCorrectEventsBehavior(this.cmdLimpiar, true);
            this.cmdLimpiar.Cursor = System.Windows.Forms.Cursors.Default;
            this.commandButtonHelper1.SetDisabledPicture(this.cmdLimpiar, null);
            this.commandButtonHelper1.SetDownPicture(this.cmdLimpiar, null);
            this.cmdLimpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLimpiar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdLimpiar.Image = ((System.Drawing.Image)(resources.GetObject("cmdLimpiar.Image")));
            this.cmdLimpiar.Location = new System.Drawing.Point(316, 16);
            this.commandButtonHelper1.SetMaskColor(this.cmdLimpiar, System.Drawing.Color.Silver);
            this.cmdLimpiar.Name = "cmdLimpiar";
            this.cmdLimpiar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdLimpiar.Size = new System.Drawing.Size(60, 52);
            this.commandButtonHelper1.SetStyle(this.cmdLimpiar, 1);
            this.cmdLimpiar.TabIndex = 2;
            this.cmdLimpiar.Tag = "";
            this.cmdLimpiar.Text = "Limpiar";
            this.cmdLimpiar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdLimpiar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ToolTip1.SetToolTip(this.cmdLimpiar, "Limpiar la información");
            this.cmdLimpiar.UseVisualStyleBackColor = false;
            this.cmdLimpiar.Click += new System.EventHandler(this.cmdLimpiar_Click);
            // 
            // cmdSalir
            // 
            this.cmdSalir.BackColor = System.Drawing.SystemColors.Control;
            this.commandButtonHelper1.SetCorrectEventsBehavior(this.cmdSalir, true);
            this.cmdSalir.Cursor = System.Windows.Forms.Cursors.Default;
            this.commandButtonHelper1.SetDisabledPicture(this.cmdSalir, null);
            this.commandButtonHelper1.SetDownPicture(this.cmdSalir, null);
            this.cmdSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSalir.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdSalir.Image = ((System.Drawing.Image)(resources.GetObject("cmdSalir.Image")));
            this.cmdSalir.Location = new System.Drawing.Point(388, 16);
            this.commandButtonHelper1.SetMaskColor(this.cmdSalir, System.Drawing.Color.Silver);
            this.cmdSalir.Name = "cmdSalir";
            this.cmdSalir.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdSalir.Size = new System.Drawing.Size(60, 52);
            this.commandButtonHelper1.SetStyle(this.cmdSalir, 1);
            this.cmdSalir.TabIndex = 3;
            this.cmdSalir.Tag = "";
            this.cmdSalir.Text = "Salir";
            this.cmdSalir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ToolTip1.SetToolTip(this.cmdSalir, "Salir de la aplicación");
            this.cmdSalir.UseVisualStyleBackColor = false;
            this.cmdSalir.Click += new System.EventHandler(this.cmdSalir_Click);
            // 
            // cmdGenArch
            // 
            this.cmdGenArch.BackColor = System.Drawing.SystemColors.Control;
            this.commandButtonHelper1.SetCorrectEventsBehavior(this.cmdGenArch, true);
            this.cmdGenArch.Cursor = System.Windows.Forms.Cursors.Default;
            this.commandButtonHelper1.SetDisabledPicture(this.cmdGenArch, null);
            this.commandButtonHelper1.SetDownPicture(this.cmdGenArch, null);
            this.cmdGenArch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGenArch.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdGenArch.Image = ((System.Drawing.Image)(resources.GetObject("cmdGenArch.Image")));
            this.cmdGenArch.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdGenArch.Location = new System.Drawing.Point(244, 16);
            this.commandButtonHelper1.SetMaskColor(this.cmdGenArch, System.Drawing.Color.Silver);
            this.cmdGenArch.Name = "cmdGenArch";
            this.cmdGenArch.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdGenArch.Size = new System.Drawing.Size(63, 52);
            this.commandButtonHelper1.SetStyle(this.cmdGenArch, 1);
            this.cmdGenArch.TabIndex = 1;
            this.cmdGenArch.Tag = "";
            this.cmdGenArch.Text = "Gen.Arch.";
            this.cmdGenArch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ToolTip1.SetToolTip(this.cmdGenArch, "Guardar la información en un archivo de texto");
            this.cmdGenArch.UseVisualStyleBackColor = false;
            this.cmdGenArch.Click += new System.EventHandler(this.cmdGenArch_Click);
            // 
            // MainMenu1
            // 
            this.MainMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDecFolios,
            this.dummy1,
            this.dummy2});
            this.MainMenu1.Location = new System.Drawing.Point(0, 0);
            this.MainMenu1.Name = "MainMenu1";
            this.MainMenu1.Size = new System.Drawing.Size(694, 24);
            this.MainMenu1.TabIndex = 8;
            this.MainMenu1.Visible = false;
            // 
            // mnuDecFolios
            // 
            this.mnuDecFolios.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGenArchivo,
            this.mnuLimpiar,
            this.mnuSpe1,
            this.mnuSalir});
            this.mnuDecFolios.MergeAction = System.Windows.Forms.MergeAction.Replace;
            this.mnuDecFolios.MergeIndex = 0;
            this.mnuDecFolios.Name = "mnuDecFolios";
            this.mnuDecFolios.Size = new System.Drawing.Size(140, 20);
            this.mnuDecFolios.Tag = "";
            this.mnuDecFolios.Text = "&Consulta Toda la Remesa";
            // 
            // mnuGenArchivo
            // 
            this.mnuGenArchivo.Name = "mnuGenArchivo";
            this.mnuGenArchivo.Size = new System.Drawing.Size(159, 22);
            this.mnuGenArchivo.Tag = "";
            this.mnuGenArchivo.Text = "&Genera Archivo";
            this.mnuGenArchivo.Click += new System.EventHandler(this.mnuGenArchivo_Click);
            // 
            // mnuLimpiar
            // 
            this.mnuLimpiar.Name = "mnuLimpiar";
            this.mnuLimpiar.Size = new System.Drawing.Size(159, 22);
            this.mnuLimpiar.Tag = "";
            this.mnuLimpiar.Text = "&Limpiar";
            this.mnuLimpiar.Click += new System.EventHandler(this.mnuLimpiar_Click);
            // 
            // mnuSpe1
            // 
            this.mnuSpe1.Name = "mnuSpe1";
            this.mnuSpe1.Size = new System.Drawing.Size(156, 6);
            this.mnuSpe1.Tag = "";
            // 
            // mnuSalir
            // 
            this.mnuSalir.Name = "mnuSalir";
            this.mnuSalir.Size = new System.Drawing.Size(159, 22);
            this.mnuSalir.Tag = "";
            this.mnuSalir.Text = "&Salir";
            this.mnuSalir.Click += new System.EventHandler(this.mnuSalir_Click);
            // 
            // dummy1
            // 
            this.dummy1.MergeAction = System.Windows.Forms.MergeAction.Replace;
            this.dummy1.MergeIndex = 1;
            this.dummy1.Name = "dummy1";
            this.dummy1.Size = new System.Drawing.Size(53, 20);
            this.dummy1.Text = "dummy";
            this.dummy1.Visible = false;
            // 
            // dummy2
            // 
            this.dummy2.MergeAction = System.Windows.Forms.MergeAction.Replace;
            this.dummy2.MergeIndex = 2;
            this.dummy2.Name = "dummy2";
            this.dummy2.Size = new System.Drawing.Size(59, 20);
            this.dummy2.Text = "dummy2";
            this.dummy2.Visible = false;
            // 
            // Frame3
            // 
            this.Frame3.BackColor = System.Drawing.SystemColors.Control;
            this.Frame3.Controls.Add(this.cmdLimpiar);
            this.Frame3.Controls.Add(this.cmdSalir);
            this.Frame3.Controls.Add(this.cmdGenArch);
            this.Frame3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Frame3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Frame3.Location = new System.Drawing.Point(8, 285);
            this.Frame3.Name = "Frame3";
            this.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Frame3.Size = new System.Drawing.Size(684, 75);
            this.Frame3.TabIndex = 7;
            this.Frame3.TabStop = false;
            this.Frame3.Tag = "";
            // 
            // Frame5
            // 
            this.Frame5.BackColor = System.Drawing.SystemColors.Control;
            this.Frame5.Controls.Add(this.txtClaveRemesa);
            this.Frame5.Controls.Add(this._Label1_0);
            this.Frame5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Frame5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Frame5.Location = new System.Drawing.Point(8, 1);
            this.Frame5.Name = "Frame5";
            this.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Frame5.Size = new System.Drawing.Size(684, 49);
            this.Frame5.TabIndex = 5;
            this.Frame5.TabStop = false;
            this.Frame5.Tag = "";
            // 
            // txtClaveRemesa
            // 
            this.txtClaveRemesa.AcceptsReturn = true;
            this.txtClaveRemesa.BackColor = System.Drawing.SystemColors.Window;
            this.txtClaveRemesa.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtClaveRemesa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClaveRemesa.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtClaveRemesa.Location = new System.Drawing.Point(159, 16);
            this.txtClaveRemesa.MaxLength = 18;
            this.txtClaveRemesa.Name = "txtClaveRemesa";
            this.txtClaveRemesa.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtClaveRemesa.Size = new System.Drawing.Size(185, 20);
            this.txtClaveRemesa.TabIndex = 0;
            this.txtClaveRemesa.Tag = "";
            this.txtClaveRemesa.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtClaveRemesa.Enter += new System.EventHandler(this.txtClaveRemesa_Enter);
            this.txtClaveRemesa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtClaveRemesa_KeyPress);
            // 
            // _Label1_0
            // 
            this._Label1_0.BackColor = System.Drawing.SystemColors.Control;
            this._Label1_0.Cursor = System.Windows.Forms.Cursors.Default;
            this._Label1_0.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._Label1_0.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._Label1_0.ForeColor = System.Drawing.Color.Blue;
            this._Label1_0.Location = new System.Drawing.Point(16, 16);
            this._Label1_0.Name = "_Label1_0";
            this._Label1_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._Label1_0.Size = new System.Drawing.Size(145, 17);
            this._Label1_0.TabIndex = 6;
            this._Label1_0.Tag = "";
            this._Label1_0.Text = "Clave de la Remesa:";
            // 
            // Frame2
            // 
            this.Frame2.BackColor = System.Drawing.SystemColors.Control;
            this.Frame2.Controls.Add(this.tdbInfRemesa);
            this.Frame2.Controls.Add(this.lblTotal);
            this.Frame2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Frame2.ForeColor = System.Drawing.Color.Blue;
            this.Frame2.Location = new System.Drawing.Point(8, 53);
            this.Frame2.Name = "Frame2";
            this.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Frame2.Size = new System.Drawing.Size(684, 229);
            this.Frame2.TabIndex = 4;
            this.Frame2.TabStop = false;
            this.Frame2.Tag = "";
            this.Frame2.Text = "Información de la Remesa";
            // 
            // tdbInfRemesa
            // 
            this.tdbInfRemesa.Location = new System.Drawing.Point(3, 21);
            this.tdbInfRemesa.Name = "tdbInfRemesa";
            this.tdbInfRemesa.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("tdbInfRemesa.OcxState")));
            this.tdbInfRemesa.Size = new System.Drawing.Size(677, 187);
            this.tdbInfRemesa.TabIndex = 9;
            this.tdbInfRemesa.Tag = "";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.BackColor = System.Drawing.SystemColors.Control;
            this.lblTotal.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblTotal.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTotal.Location = new System.Drawing.Point(12, 210);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblTotal.Size = new System.Drawing.Size(179, 13);
            this.lblTotal.TabIndex = 8;
            this.lblTotal.Tag = "";
            this.lblTotal.Text = "Total de folios en la remesa: 0";
            // 
            // frmConsTodaRemesa
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(700, 374);
            this.ControlBox = false;
            this.Controls.Add(this.Frame3);
            this.Controls.Add(this.Frame5);
            this.Controls.Add(this.Frame2);
            this.Controls.Add(this.MainMenu1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(10, 29);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.formHelper1.SetMoveable(this, false);
            this.Name = "frmConsTodaRemesa";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "";
            this.Text = "Consulta Toda la Remesa";
            this.Closed += new System.EventHandler(this.frmConsTodaRemesa_Closed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmConsTodaRemesa_FormClosing);
            this.MainMenu1.ResumeLayout(false);
            this.MainMenu1.PerformLayout();
            this.Frame3.ResumeLayout(false);
            this.Frame5.ResumeLayout(false);
            this.Frame5.PerformLayout();
            this.Frame2.ResumeLayout(false);
            this.Frame2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tdbInfRemesa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandButtonHelper1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        void InitializeLabel1()
        {
            this.Label1[0] = _Label1_0;
        }
        #endregion

        private System.Windows.Forms.ToolStripMenuItem dummy1;
        private System.Windows.Forms.ToolStripMenuItem dummy2;
        private Artinsoft.VB6.Gui.FormHelper formHelper1;

    }
}