using System; 
using VB6 = Microsoft.VisualBasic.Compatibility.VB6.Support; 

namespace Masivos
{
	partial class MDIMasivos
	{
	
#region "Upgrade Support "
		private static MDIMasivos m_vb6FormDefInstance;
		private static bool m_InitializingDefInstance;
		public static MDIMasivos DefInstance
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
		public MDIMasivos():base(){
			//AIS-Bug 9202 FSABORIO
			if (m_vb6FormDefInstance == null || m_vb6FormDefInstance.IsDisposed)
			{
				m_vb6FormDefInstance = this;
			}
			//This call is required by the Windows Form Designer.
			InitializeComponent();
			MDIForm_Initialize_Renamed();
		}
	public static MDIMasivos CreateInstance()
	{
			MDIMasivos theInstance = new MDIMasivos();
			theInstance.Form_Load();
			return theInstance;
	}
	private void  ReleaseResources( object eventSender,  System.EventArgs eventArgs)
	{
			Dispose(true);
			m_vb6FormDefInstance = null;
			//AIS-Bug 8753 FSABORIO
			Artinsoft.VB6.Utils.MemoryHelper.ReleaseMemory();
	}
	//Form overrides dispose to clean up the component list.
	[System.Diagnostics.DebuggerNonUserCode]
	 protected   override  void  Dispose( bool Disposing)
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
	public  System.Windows.Forms.ToolStripMenuItem mnuProcesar;
	public  System.Windows.Forms.ToolStripMenuItem mnuStatusRemesa;
	public  System.Windows.Forms.ToolStripMenuItem mnuProcesamientoMasivo;
	public  System.Windows.Forms.ToolStripMenuItem mnuConsRemesa;
	public  System.Windows.Forms.ToolStripMenuItem mnuConsFolio;
	public  System.Windows.Forms.ToolStripMenuItem mnuPredictaminacion;
	public  System.Windows.Forms.ToolStripMenuItem mnuFirmaS041;
	public  System.Windows.Forms.MenuStrip MainMenu1;
	public  AxMSWinsockLib.AxWinsock Wskprincipal;
	public  System.Windows.Forms.Timer tmrFTPVC;
	public  Acceso.UsrCtlAcceso OleAcceso;
	public  System.Windows.Forms.Timer tmrFtp;
	public  System.Windows.Forms.Label lblVersion;
	public  System.Windows.Forms.Panel picVersion;
	public  System.Windows.Forms.Label Label1;
	public  AxMSComctlLib.AxToolbar tlbMasivos;
	public  AxMSComctlLib.AxImageList ImageList1;
        public System.Windows.Forms.Timer tmrHoraBarraStatus;
	//NOTE: The following procedure is required by the Windows Form Designer
	//It can be modified using the Windows Form Designer.
	//Do not modify it using the code editor.
	//[System.Diagnostics.DebuggerStepThrough]
	 private void  InitializeComponent()
	{
        this.components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDIMasivos));
        this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
        this.MainMenu1 = new System.Windows.Forms.MenuStrip();
        this.mnuProcesamientoMasivo = new System.Windows.Forms.ToolStripMenuItem();
        this.mnuProcesar = new System.Windows.Forms.ToolStripMenuItem();
        this.mnuStatusRemesa = new System.Windows.Forms.ToolStripMenuItem();
        this.mnuPredictaminacion = new System.Windows.Forms.ToolStripMenuItem();
        this.mnuConsRemesa = new System.Windows.Forms.ToolStripMenuItem();
        this.mnuConsFolio = new System.Windows.Forms.ToolStripMenuItem();
        this.mnuFirmaS041 = new System.Windows.Forms.ToolStripMenuItem();
        this.consultaRemesaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.arriboRemesaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.validaciónRemesaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.inspecciónRemesaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.consultaRemesaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        this.Wskprincipal = new AxMSWinsockLib.AxWinsock();
        this.tmrFTPVC = new System.Windows.Forms.Timer(this.components);
        this.OleAcceso = new Acceso.UsrCtlAcceso();
        this.tmrFtp = new System.Windows.Forms.Timer(this.components);
        this.tlbMasivos = new AxMSComctlLib.AxToolbar();
        this.picVersion = new System.Windows.Forms.Panel();
        this.lblVersion = new System.Windows.Forms.Label();
        this.Label1 = new System.Windows.Forms.Label();
        this.ImageList1 = new AxMSComctlLib.AxImageList();
        this.tmrHoraBarraStatus = new System.Windows.Forms.Timer(this.components);
        this.pnlEstado = new System.Windows.Forms.StatusStrip();
        this.pnlMensajes = new System.Windows.Forms.ToolStripStatusLabel();
        this.pnlUsuario = new System.Windows.Forms.ToolStripStatusLabel();
        this.pnlHora = new System.Windows.Forms.ToolStripStatusLabel();
        this.pnlFecha = new System.Windows.Forms.ToolStripStatusLabel();
        this.MainMenu1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.Wskprincipal)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.tlbMasivos)).BeginInit();
        this.picVersion.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.ImageList1)).BeginInit();
        this.pnlEstado.SuspendLayout();
        this.SuspendLayout();
        // 
        // MainMenu1
        // 
        this.MainMenu1.BackColor = System.Drawing.SystemColors.ButtonFace;
        this.MainMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuProcesamientoMasivo,
            this.mnuPredictaminacion,
            this.mnuFirmaS041,
            this.consultaRemesaToolStripMenuItem});
        this.MainMenu1.Location = new System.Drawing.Point(0, 0);
        this.MainMenu1.Name = "MainMenu1";
        this.MainMenu1.Size = new System.Drawing.Size(632, 24);
        this.MainMenu1.TabIndex = 4;
        // 
        // mnuProcesamientoMasivo
        // 
        this.mnuProcesamientoMasivo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuProcesar,
            this.mnuStatusRemesa});
        this.mnuProcesamientoMasivo.MergeAction = System.Windows.Forms.MergeAction.Remove;
        this.mnuProcesamientoMasivo.Name = "mnuProcesamientoMasivo";
        this.mnuProcesamientoMasivo.Size = new System.Drawing.Size(125, 20);
        this.mnuProcesamientoMasivo.Tag = "";
        this.mnuProcesamientoMasivo.Text = "&Procesamiento Masivo";
        // 
        // mnuProcesar
        // 
        this.mnuProcesar.Name = "mnuProcesar";
        this.mnuProcesar.Size = new System.Drawing.Size(233, 22);
        this.mnuProcesar.Tag = "";
        this.mnuProcesar.Text = "&Procesamiento Masivo";
        this.mnuProcesar.Click += new System.EventHandler(this.mnuProcesar_Click);
        // 
        // mnuStatusRemesa
        // 
        this.mnuStatusRemesa.Name = "mnuStatusRemesa";
        this.mnuStatusRemesa.Size = new System.Drawing.Size(233, 22);
        this.mnuStatusRemesa.Tag = "";
        this.mnuStatusRemesa.Text = "&Consulta Estatus de la Remesa";
        this.mnuStatusRemesa.Click += new System.EventHandler(this.mnuStatusRemesa_Click);
        // 
        // mnuPredictaminacion
        // 
        this.mnuPredictaminacion.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuConsRemesa,
            this.mnuConsFolio});
        this.mnuPredictaminacion.MergeAction = System.Windows.Forms.MergeAction.Remove;
        this.mnuPredictaminacion.Name = "mnuPredictaminacion";
        this.mnuPredictaminacion.Size = new System.Drawing.Size(99, 20);
        this.mnuPredictaminacion.Tag = "";
        this.mnuPredictaminacion.Text = "Predictaminación";
        // 
        // mnuConsRemesa
        // 
        this.mnuConsRemesa.Name = "mnuConsRemesa";
        this.mnuConsRemesa.Size = new System.Drawing.Size(206, 22);
        this.mnuConsRemesa.Tag = "";
        this.mnuConsRemesa.Text = "Consulta Toda la Remesa";
        this.mnuConsRemesa.Click += new System.EventHandler(this.mnuConsRemesa_Click);
        // 
        // mnuConsFolio
        // 
        this.mnuConsFolio.Name = "mnuConsFolio";
        this.mnuConsFolio.Size = new System.Drawing.Size(206, 22);
        this.mnuConsFolio.Tag = "";
        this.mnuConsFolio.Text = "Consulta por Folio";
        this.mnuConsFolio.Click += new System.EventHandler(this.mnuConsFolio_Click);
        // 
        // mnuFirmaS041
        // 
        this.mnuFirmaS041.MergeAction = System.Windows.Forms.MergeAction.Remove;
        this.mnuFirmaS041.Name = "mnuFirmaS041";
        this.mnuFirmaS041.Size = new System.Drawing.Size(83, 20);
        this.mnuFirmaS041.Tag = "";
        this.mnuFirmaS041.Text = "Firma al S041";
        this.mnuFirmaS041.Click += new System.EventHandler(this.mnuFirmaS041_Click);
        // 
        // consultaRemesaToolStripMenuItem
        // 
        this.consultaRemesaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arriboRemesaToolStripMenuItem,
            this.validaciónRemesaToolStripMenuItem,
            this.inspecciónRemesaToolStripMenuItem,
            this.consultaRemesaToolStripMenuItem1});
        this.consultaRemesaToolStripMenuItem.Name = "consultaRemesaToolStripMenuItem";
        this.consultaRemesaToolStripMenuItem.Size = new System.Drawing.Size(102, 20);
        this.consultaRemesaToolStripMenuItem.Text = "Consulta &Remesa";
        // 
        // arriboRemesaToolStripMenuItem
        // 
        this.arriboRemesaToolStripMenuItem.Name = "arriboRemesaToolStripMenuItem";
        this.arriboRemesaToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
        this.arriboRemesaToolStripMenuItem.Text = "Arribo Remesa";
        this.arriboRemesaToolStripMenuItem.Click += new System.EventHandler(this.arriboRemesaToolStripMenuItem_Click);
        // 
        // validaciónRemesaToolStripMenuItem
        // 
        this.validaciónRemesaToolStripMenuItem.Name = "validaciónRemesaToolStripMenuItem";
        this.validaciónRemesaToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
        this.validaciónRemesaToolStripMenuItem.Text = "Validación Remesa";
        this.validaciónRemesaToolStripMenuItem.Click += new System.EventHandler(this.validacionRemesaToolStripMenuItem_Click);
        // 
        // inspecciónRemesaToolStripMenuItem
        // 
        this.inspecciónRemesaToolStripMenuItem.Name = "inspecciónRemesaToolStripMenuItem";
        this.inspecciónRemesaToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
        this.inspecciónRemesaToolStripMenuItem.Text = "Inspección Remesa";
        this.inspecciónRemesaToolStripMenuItem.Click += new System.EventHandler(this.inspeccionRemesaToolStripMenuItem_Click);
        // 
        // consultaRemesaToolStripMenuItem1
        // 
        this.consultaRemesaToolStripMenuItem1.Name = "consultaRemesaToolStripMenuItem1";
        this.consultaRemesaToolStripMenuItem1.Size = new System.Drawing.Size(177, 22);
        this.consultaRemesaToolStripMenuItem1.Text = "Consulta Remesa";
        this.consultaRemesaToolStripMenuItem1.Click += new System.EventHandler(this.consultaRemesaToolStripMenuItem1_Click);
        // 
        // Wskprincipal
        // 
        this.Wskprincipal.Enabled = true;
        this.Wskprincipal.Location = new System.Drawing.Point(8, 152);
        this.Wskprincipal.Name = "Wskprincipal";
        this.Wskprincipal.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("Wskprincipal.OcxState")));
        this.Wskprincipal.Size = new System.Drawing.Size(28, 28);
        this.Wskprincipal.TabIndex = 1;
        this.Wskprincipal.Tag = "";
        // 
        // tmrFTPVC
        // 
        this.tmrFTPVC.Interval = 60000;
        this.tmrFTPVC.Tick += new System.EventHandler(this.tmrFTPVC_Tick);
        // 
        // OleAcceso
        // 
        this.OleAcceso.CargaCatalogos = true;
        this.OleAcceso.Debugger = false;
        this.OleAcceso.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.OleAcceso.Location = new System.Drawing.Point(3, 99);
        this.OleAcceso.Name = "OleAcceso";
        this.OleAcceso.Nomina = "00000000";
        this.OleAcceso.NominaRequerida = false;
        this.OleAcceso.Size = new System.Drawing.Size(38, 35);
        this.OleAcceso.TabIndex = 0;
        this.OleAcceso.Tag = "";
        this.OleAcceso.TransaccionAutorizacion = Acceso.UsrCtlAcceso.TipoTransaccionAutorizacion.TransaccionDefault;
        this.OleAcceso.Visible = false;
        this.OleAcceso.ErrorConexion += new Acceso.UsrCtlAcceso.ErrorConexionHandler(this.OleAcceso_ErrorConexion);
        this.OleAcceso.ConexionExitosa += new Acceso.UsrCtlAcceso.ConexionExitosaHandler(this.OleAcceso_ConexionExitosa);
        this.OleAcceso.CargarCatalogos += new Acceso.UsrCtlAcceso.CargarCatalogosHandler(this.OleAcceso_CargarCatalogos);
        // 
        // tmrFtp
        // 
        this.tmrFtp.Interval = 30000;
        this.tmrFtp.Tick += new System.EventHandler(this.tmrFtp_Tick);
        // 
        // tlbMasivos
        // 
        this.tlbMasivos.Dock = System.Windows.Forms.DockStyle.Top;
        this.tlbMasivos.Location = new System.Drawing.Point(0, 24);
        this.tlbMasivos.Name = "tlbMasivos";
        this.tlbMasivos.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("tlbMasivos.OcxState")));
        this.tlbMasivos.Size = new System.Drawing.Size(632, 28);
        this.tlbMasivos.TabIndex = 0;
        this.tlbMasivos.Tag = "";
        this.tlbMasivos.ButtonClick += new AxMSComctlLib.IToolbarEvents_ButtonClickEventHandler(this.tlbMasivos_ButtonClick);
        // 
        // picVersion
        // 
        this.picVersion.BackColor = System.Drawing.SystemColors.Control;
        this.picVersion.Controls.Add(this.lblVersion);
        this.picVersion.Controls.Add(this.Label1);
        this.picVersion.Cursor = System.Windows.Forms.Cursors.Default;
        this.picVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.picVersion.Location = new System.Drawing.Point(322, 27);
        this.picVersion.Name = "picVersion";
        this.picVersion.Size = new System.Drawing.Size(330, 22);
        this.picVersion.TabIndex = 3;
        this.picVersion.TabStop = true;
        this.picVersion.Tag = "";
        // 
        // lblVersion
        // 
        this.lblVersion.AutoSize = true;
        this.lblVersion.BackColor = System.Drawing.SystemColors.Control;
        this.lblVersion.Cursor = System.Windows.Forms.Cursors.Default;
        this.lblVersion.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblVersion.ForeColor = System.Drawing.SystemColors.ControlText;
        this.lblVersion.Location = new System.Drawing.Point(37, 6);
        this.lblVersion.Name = "lblVersion";
        this.lblVersion.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.lblVersion.Size = new System.Drawing.Size(91, 13);
        this.lblVersion.TabIndex = 4;
        this.lblVersion.Tag = "";
        this.lblVersion.Text = "VERSIÓN 6.01";
        // 
        // Label1
        // 
        this.Label1.AutoSize = true;
        this.Label1.BackColor = System.Drawing.SystemColors.Control;
        this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
        this.Label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
        this.Label1.Location = new System.Drawing.Point(37, 6);
        this.Label1.Name = "Label1";
        this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.Label1.Size = new System.Drawing.Size(81, 13);
        this.Label1.TabIndex = 2;
        this.Label1.Tag = "";
        this.Label1.Text = "Versión 4.03.01";
        // 
        // ImageList1
        // 
        this.ImageList1.Enabled = true;
        this.ImageList1.Location = new System.Drawing.Point(3, 57);
        this.ImageList1.Name = "ImageList1";
        this.ImageList1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("ImageList1.OcxState")));
        this.ImageList1.Size = new System.Drawing.Size(38, 38);
        this.ImageList1.TabIndex = 2;
        this.ImageList1.Tag = "";
        // 
        // tmrHoraBarraStatus
        // 
        this.tmrHoraBarraStatus.Interval = 1000;
        this.tmrHoraBarraStatus.Tick += new System.EventHandler(this.tmrHoraBarraStatus_Tick);
        // 
        // pnlEstado
        // 
        this.pnlEstado.AutoSize = false;
        this.pnlEstado.BackColor = System.Drawing.SystemColors.Control;
        this.pnlEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.pnlEstado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pnlMensajes,
            this.pnlUsuario,
            this.pnlHora,
            this.pnlFecha});
        this.pnlEstado.Location = new System.Drawing.Point(0, 397);
        this.pnlEstado.Name = "pnlEstado";
        this.pnlEstado.Size = new System.Drawing.Size(632, 19);
        this.pnlEstado.TabIndex = 1;
        // 
        // pnlMensajes
        // 
        this.pnlMensajes.AutoSize = false;
        this.pnlMensajes.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                    | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                    | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
        this.pnlMensajes.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
        this.pnlMensajes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
        this.pnlMensajes.Margin = new System.Windows.Forms.Padding(0, 2, 1, 0);
        this.pnlMensajes.Name = "pnlMensajes";
        this.pnlMensajes.Size = new System.Drawing.Size(433, 17);
        this.pnlMensajes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // pnlUsuario
        // 
        this.pnlUsuario.AutoSize = false;
        this.pnlUsuario.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                    | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                    | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
        this.pnlUsuario.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
        this.pnlUsuario.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
        this.pnlUsuario.Margin = new System.Windows.Forms.Padding(0, 2, 1, 0);
        this.pnlUsuario.Name = "pnlUsuario";
        this.pnlUsuario.Size = new System.Drawing.Size(206, 17);
        this.pnlUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // pnlHora
        // 
        this.pnlHora.AutoSize = false;
        this.pnlHora.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                    | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                    | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
        this.pnlHora.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
        this.pnlHora.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
        this.pnlHora.Margin = new System.Windows.Forms.Padding(0, 2, 1, 0);
        this.pnlHora.Name = "pnlHora";
        this.pnlHora.Size = new System.Drawing.Size(60, 17);
        this.pnlHora.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // pnlFecha
        // 
        this.pnlFecha.AutoSize = false;
        this.pnlFecha.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                    | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                    | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
        this.pnlFecha.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
        this.pnlFecha.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
        this.pnlFecha.Margin = new System.Windows.Forms.Padding(0, 2, 1, 0);
        this.pnlFecha.Name = "pnlFecha";
        this.pnlFecha.Size = new System.Drawing.Size(76, 17);
        this.pnlFecha.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // MDIMasivos
        // 
        this.BackColor = System.Drawing.SystemColors.AppWorkspace;
        this.ClientSize = new System.Drawing.Size(632, 416);
        this.Controls.Add(this.pnlEstado);
        this.Controls.Add(this.picVersion);
        this.Controls.Add(this.OleAcceso);
        this.Controls.Add(this.tlbMasivos);
        this.Controls.Add(this.Wskprincipal);
        this.Controls.Add(this.ImageList1);
        this.Controls.Add(this.MainMenu1);
        this.Cursor = System.Windows.Forms.Cursors.Default;
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.IsMdiContainer = true;
        this.Location = new System.Drawing.Point(-9, 69);
        this.MainMenuStrip = this.MainMenu1;
        this.Name = "MDIMasivos";
        this.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
        this.Tag = "";
        this.Text = "S753 ARIES - Procesamiento Masivo y Predictaminación";
        this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MDIMasivos_FormClosing);
        this.MainMenu1.ResumeLayout(false);
        this.MainMenu1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.Wskprincipal)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.tlbMasivos)).EndInit();
        this.picVersion.ResumeLayout(false);
        this.picVersion.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.ImageList1)).EndInit();
        this.pnlEstado.ResumeLayout(false);
        this.pnlEstado.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

	}
#endregion 

    private System.Windows.Forms.ToolStripStatusLabel pnlMensajes;
        private System.Windows.Forms.ToolStripStatusLabel pnlUsuario;
        private System.Windows.Forms.ToolStripStatusLabel pnlHora;
        private System.Windows.Forms.ToolStripStatusLabel pnlFecha;
        public System.Windows.Forms.StatusStrip pnlEstado;
        private System.Windows.Forms.ToolStripMenuItem consultaRemesaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem arriboRemesaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem validaciónRemesaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inspecciónRemesaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem consultaRemesaToolStripMenuItem1;
}
}