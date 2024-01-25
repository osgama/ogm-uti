using System; 
using VB6 = Microsoft.VisualBasic.Compatibility.VB6.Support; 

namespace Masivos
{
	partial class frmProcMasivo
	{
	
#region "Upgrade Support "
        public static frmProcMasivo m_vb6FormDefInstance;
		private static bool m_InitializingDefInstance;
		public static frmProcMasivo DefInstance
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
		public frmProcMasivo():base(){
			//AIS-Bug 9202 FSABORIO
			if (m_vb6FormDefInstance == null || m_vb6FormDefInstance.IsDisposed)
			{
				if (m_InitializingDefInstance)
				{
					m_vb6FormDefInstance = this;
				} else
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
			InitializeLabel2();
			//This form is an MDI child.
			//This code simulates the VB6 
			// functionality of automatically
			// loading and showing an MDI
			// child's parent.
			this.MdiParent = Masivos.MDIMasivos.DefInstance;
			Masivos.MDIMasivos.DefInstance.Show();
		}
	public static frmProcMasivo CreateInstance()
	{
			frmProcMasivo theInstance = new frmProcMasivo();
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
	public  System.Windows.Forms.ToolStripMenuItem mnuLeeArchivo;
	public  System.Windows.Forms.ToolStripSeparator mnuSep1;
	public  System.Windows.Forms.ToolStripMenuItem mnuSalir;
	public  System.Windows.Forms.ToolStripMenuItem mnuProcMasivo;
	public  System.Windows.Forms.MenuStrip MainMenu1;
	public  AxMSComctlLib.AxProgressBar prg_proceso;
	public  AxMSComDlg.AxCommonDialog dlgArchivoAbrir;
	public  System.Windows.Forms.Button cmdSalir;
	public  System.Windows.Forms.TextBox txtTipoTram;
	public  System.Windows.Forms.TextBox txtTipoEntidad;
	public  System.Windows.Forms.TextBox txtFamiliaProducto;
	public  System.Windows.Forms.TextBox txtEntidadOrigen;
	public  System.Windows.Forms.TextBox txtTipoSolicitud;
	private  System.Windows.Forms.Label _Label2_11;
	private  System.Windows.Forms.Label _Label2_13;
	private  System.Windows.Forms.Label _Label2_12;
	private  System.Windows.Forms.Label _Label2_10;
	private  System.Windows.Forms.Label _Label2_8;
	public  System.Windows.Forms.GroupBox fraInfoTramite;
	public  System.Windows.Forms.TextBox txtSolRechazadas;
	public  System.Windows.Forms.TextBox txtSolAceptadas;
	public  System.Windows.Forms.TextBox txtFolFinal;
	public  System.Windows.Forms.TextBox txtFolInicial;
	public  System.Windows.Forms.TextBox txtNumSolicitudes;
	public  System.Windows.Forms.TextBox txtRemesa;
	public  System.Windows.Forms.TextBox txtArchivo;
	public  System.Windows.Forms.Label lblGrabado;
	private  System.Windows.Forms.Label _Label2_6;
	private  System.Windows.Forms.Label _Label2_5;
	private  System.Windows.Forms.Label _Label2_4;
	private  System.Windows.Forms.Label _Label2_3;
	private  System.Windows.Forms.Label _Label2_2;
	private  System.Windows.Forms.Label _Label2_1;
	private  System.Windows.Forms.Label _Label2_0;
	public  System.Windows.Forms.GroupBox fraInfoArchivo;
	public  System.Windows.Forms.Timer Timer1;
	public  System.Windows.Forms.GroupBox Frame1;
	public  System.Windows.Forms.Button cmdLeeArchivo;
	public  System.Windows.Forms.ComboBox cboTipoTram;
	private  System.Windows.Forms.Label _Label1_1;
	public  System.Windows.Forms.GroupBox fraDatosFolio;
	public System.Windows.Forms.Label[] Label1 = new System.Windows.Forms.Label[2];
	public System.Windows.Forms.Label[] Label2 = new System.Windows.Forms.Label[14];
	private Artinsoft.VB6.Gui.CommandButtonHelper commandButtonHelper1;
	//NOTE: The following procedure is required by the Windows Form Designer
	//It can be modified using the Windows Form Designer.
	//Do not modify it using the code editor.
	[System.Diagnostics.DebuggerStepThrough]
	 private void  InitializeComponent()
	{
        this.components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProcMasivo));
        this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
        this.cmdSalir = new System.Windows.Forms.Button();
        this.cmdLeeArchivo = new System.Windows.Forms.Button();
        this.MainMenu1 = new System.Windows.Forms.MenuStrip();
        this.mnuProcMasivo = new System.Windows.Forms.ToolStripMenuItem();
        this.mnuLeeArchivo = new System.Windows.Forms.ToolStripMenuItem();
        this.mnuSep1 = new System.Windows.Forms.ToolStripSeparator();
        this.mnuSalir = new System.Windows.Forms.ToolStripMenuItem();
        this.dummy1 = new System.Windows.Forms.ToolStripMenuItem();
        this.dummy2 = new System.Windows.Forms.ToolStripMenuItem();
        this.prg_proceso = new AxMSComctlLib.AxProgressBar();
        this.dlgArchivoAbrir = new AxMSComDlg.AxCommonDialog();
        this.fraInfoTramite = new System.Windows.Forms.GroupBox();
        this.txtTipoTram = new System.Windows.Forms.TextBox();
        this.txtTipoEntidad = new System.Windows.Forms.TextBox();
        this.txtFamiliaProducto = new System.Windows.Forms.TextBox();
        this.txtEntidadOrigen = new System.Windows.Forms.TextBox();
        this.txtTipoSolicitud = new System.Windows.Forms.TextBox();
        this._Label2_11 = new System.Windows.Forms.Label();
        this._Label2_13 = new System.Windows.Forms.Label();
        this._Label2_12 = new System.Windows.Forms.Label();
        this._Label2_10 = new System.Windows.Forms.Label();
        this._Label2_8 = new System.Windows.Forms.Label();
        this.fraInfoArchivo = new System.Windows.Forms.GroupBox();
        this.txtSolRechazadas = new System.Windows.Forms.TextBox();
        this.txtSolAceptadas = new System.Windows.Forms.TextBox();
        this.txtFolFinal = new System.Windows.Forms.TextBox();
        this.txtFolInicial = new System.Windows.Forms.TextBox();
        this.txtNumSolicitudes = new System.Windows.Forms.TextBox();
        this.txtRemesa = new System.Windows.Forms.TextBox();
        this.txtArchivo = new System.Windows.Forms.TextBox();
        this.lblGrabado = new System.Windows.Forms.Label();
        this._Label2_6 = new System.Windows.Forms.Label();
        this._Label2_5 = new System.Windows.Forms.Label();
        this._Label2_4 = new System.Windows.Forms.Label();
        this._Label2_3 = new System.Windows.Forms.Label();
        this._Label2_2 = new System.Windows.Forms.Label();
        this._Label2_1 = new System.Windows.Forms.Label();
        this._Label2_0 = new System.Windows.Forms.Label();
        this.Timer1 = new System.Windows.Forms.Timer(this.components);
        this.fraDatosFolio = new System.Windows.Forms.GroupBox();
        this.Frame1 = new System.Windows.Forms.GroupBox();
        this.cboTipoTram = new System.Windows.Forms.ComboBox();
        this._Label1_1 = new System.Windows.Forms.Label();
        this.commandButtonHelper1 = new Artinsoft.VB6.Gui.CommandButtonHelper(this.components);
        this.formHelper1 = new Artinsoft.VB6.Gui.FormHelper(this.components);
        this.MainMenu1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.prg_proceso)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dlgArchivoAbrir)).BeginInit();
        this.fraInfoTramite.SuspendLayout();
        this.fraInfoArchivo.SuspendLayout();
        this.fraDatosFolio.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.commandButtonHelper1)).BeginInit();
        this.SuspendLayout();
        // 
        // cmdSalir
        // 
        this.cmdSalir.BackColor = System.Drawing.SystemColors.Control;
        this.commandButtonHelper1.SetCorrectEventsBehavior(this.cmdSalir, true);
        this.cmdSalir.Cursor = System.Windows.Forms.Cursors.Default;
        this.commandButtonHelper1.SetDisabledPicture(this.cmdSalir, null);
        this.commandButtonHelper1.SetDownPicture(this.cmdSalir, null);
        this.cmdSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.cmdSalir.ForeColor = System.Drawing.SystemColors.ControlText;
        this.cmdSalir.Image = ((System.Drawing.Image)(resources.GetObject("cmdSalir.Image")));
        this.cmdSalir.Location = new System.Drawing.Point(424, 346);
        this.commandButtonHelper1.SetMaskColor(this.cmdSalir, System.Drawing.Color.Silver);
        this.cmdSalir.Name = "cmdSalir";
        this.cmdSalir.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.cmdSalir.Size = new System.Drawing.Size(57, 57);
        this.commandButtonHelper1.SetStyle(this.cmdSalir, 1);
        this.cmdSalir.TabIndex = 14;
        this.cmdSalir.Tag = "PROCESO";
        this.cmdSalir.Text = "&Salir";
        this.cmdSalir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
        this.cmdSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
        this.ToolTip1.SetToolTip(this.cmdSalir, "Salir del sistema");
        this.cmdSalir.UseVisualStyleBackColor = false;
        this.cmdSalir.Click += new System.EventHandler(this.cmdSalir_Click);
        // 
        // cmdLeeArchivo
        // 
        this.cmdLeeArchivo.BackColor = System.Drawing.SystemColors.Control;
        this.commandButtonHelper1.SetCorrectEventsBehavior(this.cmdLeeArchivo, true);
        this.cmdLeeArchivo.Cursor = System.Windows.Forms.Cursors.Default;
        this.commandButtonHelper1.SetDisabledPicture(this.cmdLeeArchivo, null);
        this.commandButtonHelper1.SetDownPicture(this.cmdLeeArchivo, null);
        this.cmdLeeArchivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.cmdLeeArchivo.ForeColor = System.Drawing.SystemColors.ControlText;
        this.cmdLeeArchivo.Image = ((System.Drawing.Image)(resources.GetObject("cmdLeeArchivo.Image")));
        this.cmdLeeArchivo.Location = new System.Drawing.Point(366, 15);
        this.commandButtonHelper1.SetMaskColor(this.cmdLeeArchivo, System.Drawing.Color.Silver);
        this.cmdLeeArchivo.Name = "cmdLeeArchivo";
        this.cmdLeeArchivo.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.cmdLeeArchivo.Size = new System.Drawing.Size(41, 41);
        this.commandButtonHelper1.SetStyle(this.cmdLeeArchivo, 1);
        this.cmdLeeArchivo.TabIndex = 1;
        this.cmdLeeArchivo.Tag = "PROCESO";
        this.cmdLeeArchivo.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
        this.cmdLeeArchivo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
        this.ToolTip1.SetToolTip(this.cmdLeeArchivo, "Leer y procesar el archivo de promotora");
        this.cmdLeeArchivo.UseVisualStyleBackColor = false;
        this.cmdLeeArchivo.Enter += new System.EventHandler(this.cmdLeeArchivo_Enter);
        this.cmdLeeArchivo.Click += new System.EventHandler(this.cmdLeeArchivo_Click);
        // 
        // MainMenu1
        // 
        this.MainMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuProcMasivo,
            this.dummy1,
            this.dummy2});
        this.MainMenu1.Location = new System.Drawing.Point(0, 0);
        this.MainMenu1.Name = "MainMenu1";
        this.MainMenu1.Size = new System.Drawing.Size(505, 24);
        this.MainMenu1.TabIndex = 28;
        this.MainMenu1.Visible = false;
        // 
        // mnuProcMasivo
        // 
        this.mnuProcMasivo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLeeArchivo,
            this.mnuSep1,
            this.mnuSalir});
        this.mnuProcMasivo.MergeAction = System.Windows.Forms.MergeAction.Replace;
        this.mnuProcMasivo.MergeIndex = 0;
        this.mnuProcMasivo.Name = "mnuProcMasivo";
        this.mnuProcMasivo.Size = new System.Drawing.Size(125, 20);
        this.mnuProcMasivo.Tag = "";
        this.mnuProcMasivo.Text = "Procesamiento Masivo";
        // 
        // mnuLeeArchivo
        // 
        this.mnuLeeArchivo.Name = "mnuLeeArchivo";
        this.mnuLeeArchivo.Size = new System.Drawing.Size(145, 22);
        this.mnuLeeArchivo.Tag = "";
        this.mnuLeeArchivo.Text = "Leer Archivo";
        this.mnuLeeArchivo.Click += new System.EventHandler(this.mnuLeeArchivo_Click);
        // 
        // mnuSep1
        // 
        this.mnuSep1.Name = "mnuSep1";
        this.mnuSep1.Size = new System.Drawing.Size(142, 6);
        this.mnuSep1.Tag = "";
        // 
        // mnuSalir
        // 
        this.mnuSalir.Name = "mnuSalir";
        this.mnuSalir.Size = new System.Drawing.Size(145, 22);
        this.mnuSalir.Tag = "";
        this.mnuSalir.Text = "Salir";
        this.mnuSalir.Click += new System.EventHandler(this.mnuSalir_Click);
        // 
        // dummy1
        // 
        this.dummy1.MergeAction = System.Windows.Forms.MergeAction.Replace;
        this.dummy1.MergeIndex = 1;
        this.dummy1.Name = "dummy1";
        this.dummy1.Size = new System.Drawing.Size(59, 20);
        this.dummy1.Text = "dummy1";
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
        // prg_proceso
        // 
        this.prg_proceso.Location = new System.Drawing.Point(6, 411);
        this.prg_proceso.Name = "prg_proceso";
        this.prg_proceso.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("prg_proceso.OcxState")));
        this.prg_proceso.Size = new System.Drawing.Size(493, 16);
        this.prg_proceso.TabIndex = 15;
        this.prg_proceso.Tag = "";
        // 
        // dlgArchivoAbrir
        // 
        this.dlgArchivoAbrir.Enabled = true;
        this.dlgArchivoAbrir.Location = new System.Drawing.Point(416, 280);
        this.dlgArchivoAbrir.Name = "dlgArchivoAbrir";
        this.dlgArchivoAbrir.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("dlgArchivoAbrir.OcxState")));
        this.dlgArchivoAbrir.Size = new System.Drawing.Size(32, 32);
        this.dlgArchivoAbrir.TabIndex = 16;
        this.dlgArchivoAbrir.Tag = "";
        // 
        // fraInfoTramite
        // 
        this.fraInfoTramite.BackColor = System.Drawing.SystemColors.Control;
        this.fraInfoTramite.Controls.Add(this.txtTipoTram);
        this.fraInfoTramite.Controls.Add(this.txtTipoEntidad);
        this.fraInfoTramite.Controls.Add(this.txtFamiliaProducto);
        this.fraInfoTramite.Controls.Add(this.txtEntidadOrigen);
        this.fraInfoTramite.Controls.Add(this.txtTipoSolicitud);
        this.fraInfoTramite.Controls.Add(this._Label2_11);
        this.fraInfoTramite.Controls.Add(this._Label2_13);
        this.fraInfoTramite.Controls.Add(this._Label2_12);
        this.fraInfoTramite.Controls.Add(this._Label2_10);
        this.fraInfoTramite.Controls.Add(this._Label2_8);
        this.fraInfoTramite.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.fraInfoTramite.ForeColor = System.Drawing.Color.Blue;
        this.fraInfoTramite.Location = new System.Drawing.Point(3, 253);
        this.fraInfoTramite.Name = "fraInfoTramite";
        this.fraInfoTramite.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.fraInfoTramite.Size = new System.Drawing.Size(401, 153);
        this.fraInfoTramite.TabIndex = 27;
        this.fraInfoTramite.TabStop = false;
        this.fraInfoTramite.Tag = "";
        // 
        // txtTipoTram
        // 
        this.txtTipoTram.AcceptsReturn = true;
        this.txtTipoTram.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
        this.txtTipoTram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.txtTipoTram.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtTipoTram.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtTipoTram.ForeColor = System.Drawing.Color.Maroon;
        this.txtTipoTram.Location = new System.Drawing.Point(120, 24);
        this.txtTipoTram.MaxLength = 35;
        this.txtTipoTram.Name = "txtTipoTram";
        this.txtTipoTram.ReadOnly = true;
        this.txtTipoTram.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtTipoTram.Size = new System.Drawing.Size(273, 20);
        this.txtTipoTram.TabIndex = 9;
        this.txtTipoTram.TabStop = false;
        this.txtTipoTram.Tag = "";
        // 
        // txtTipoEntidad
        // 
        this.txtTipoEntidad.AcceptsReturn = true;
        this.txtTipoEntidad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
        this.txtTipoEntidad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.txtTipoEntidad.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtTipoEntidad.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtTipoEntidad.ForeColor = System.Drawing.Color.Maroon;
        this.txtTipoEntidad.Location = new System.Drawing.Point(120, 48);
        this.txtTipoEntidad.MaxLength = 35;
        this.txtTipoEntidad.Name = "txtTipoEntidad";
        this.txtTipoEntidad.ReadOnly = true;
        this.txtTipoEntidad.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtTipoEntidad.Size = new System.Drawing.Size(273, 20);
        this.txtTipoEntidad.TabIndex = 10;
        this.txtTipoEntidad.TabStop = false;
        this.txtTipoEntidad.Tag = "";
        // 
        // txtFamiliaProducto
        // 
        this.txtFamiliaProducto.AcceptsReturn = true;
        this.txtFamiliaProducto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
        this.txtFamiliaProducto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.txtFamiliaProducto.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtFamiliaProducto.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtFamiliaProducto.ForeColor = System.Drawing.Color.Maroon;
        this.txtFamiliaProducto.Location = new System.Drawing.Point(120, 96);
        this.txtFamiliaProducto.MaxLength = 35;
        this.txtFamiliaProducto.Name = "txtFamiliaProducto";
        this.txtFamiliaProducto.ReadOnly = true;
        this.txtFamiliaProducto.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtFamiliaProducto.Size = new System.Drawing.Size(273, 20);
        this.txtFamiliaProducto.TabIndex = 12;
        this.txtFamiliaProducto.TabStop = false;
        this.txtFamiliaProducto.Tag = "";
        // 
        // txtEntidadOrigen
        // 
        this.txtEntidadOrigen.AcceptsReturn = true;
        this.txtEntidadOrigen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
        this.txtEntidadOrigen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.txtEntidadOrigen.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtEntidadOrigen.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtEntidadOrigen.ForeColor = System.Drawing.Color.Maroon;
        this.txtEntidadOrigen.Location = new System.Drawing.Point(120, 72);
        this.txtEntidadOrigen.MaxLength = 35;
        this.txtEntidadOrigen.Name = "txtEntidadOrigen";
        this.txtEntidadOrigen.ReadOnly = true;
        this.txtEntidadOrigen.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtEntidadOrigen.Size = new System.Drawing.Size(273, 20);
        this.txtEntidadOrigen.TabIndex = 11;
        this.txtEntidadOrigen.TabStop = false;
        this.txtEntidadOrigen.Tag = "";
        // 
        // txtTipoSolicitud
        // 
        this.txtTipoSolicitud.AcceptsReturn = true;
        this.txtTipoSolicitud.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
        this.txtTipoSolicitud.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.txtTipoSolicitud.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtTipoSolicitud.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtTipoSolicitud.ForeColor = System.Drawing.Color.Maroon;
        this.txtTipoSolicitud.Location = new System.Drawing.Point(120, 120);
        this.txtTipoSolicitud.MaxLength = 35;
        this.txtTipoSolicitud.Name = "txtTipoSolicitud";
        this.txtTipoSolicitud.ReadOnly = true;
        this.txtTipoSolicitud.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtTipoSolicitud.Size = new System.Drawing.Size(273, 20);
        this.txtTipoSolicitud.TabIndex = 13;
        this.txtTipoSolicitud.TabStop = false;
        this.txtTipoSolicitud.Tag = "";
        // 
        // _Label2_11
        // 
        this._Label2_11.BackColor = System.Drawing.SystemColors.Control;
        this._Label2_11.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label2_11.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label2_11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label2_11.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label2_11.Location = new System.Drawing.Point(8, 120);
        this._Label2_11.Name = "_Label2_11";
        this._Label2_11.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label2_11.Size = new System.Drawing.Size(105, 17);
        this._Label2_11.TabIndex = 32;
        this._Label2_11.Tag = "";
        this._Label2_11.Text = "Tipo de Solicitud:";
        // 
        // _Label2_13
        // 
        this._Label2_13.BackColor = System.Drawing.SystemColors.Control;
        this._Label2_13.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label2_13.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label2_13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label2_13.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label2_13.Location = new System.Drawing.Point(8, 24);
        this._Label2_13.Name = "_Label2_13";
        this._Label2_13.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label2_13.Size = new System.Drawing.Size(105, 17);
        this._Label2_13.TabIndex = 31;
        this._Label2_13.Tag = "";
        this._Label2_13.Text = "Tipo de Trámite:";
        // 
        // _Label2_12
        // 
        this._Label2_12.BackColor = System.Drawing.SystemColors.Control;
        this._Label2_12.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label2_12.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label2_12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label2_12.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label2_12.Location = new System.Drawing.Point(8, 48);
        this._Label2_12.Name = "_Label2_12";
        this._Label2_12.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label2_12.Size = new System.Drawing.Size(97, 17);
        this._Label2_12.TabIndex = 30;
        this._Label2_12.Tag = "";
        this._Label2_12.Text = "Tipo de Entidad:";
        // 
        // _Label2_10
        // 
        this._Label2_10.BackColor = System.Drawing.SystemColors.Control;
        this._Label2_10.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label2_10.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label2_10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label2_10.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label2_10.Location = new System.Drawing.Point(8, 72);
        this._Label2_10.Name = "_Label2_10";
        this._Label2_10.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label2_10.Size = new System.Drawing.Size(89, 17);
        this._Label2_10.TabIndex = 29;
        this._Label2_10.Tag = "";
        this._Label2_10.Text = "Entidad Origen:";
        // 
        // _Label2_8
        // 
        this._Label2_8.BackColor = System.Drawing.SystemColors.Control;
        this._Label2_8.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label2_8.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label2_8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label2_8.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label2_8.Location = new System.Drawing.Point(8, 96);
        this._Label2_8.Name = "_Label2_8";
        this._Label2_8.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label2_8.Size = new System.Drawing.Size(105, 17);
        this._Label2_8.TabIndex = 28;
        this._Label2_8.Tag = "";
        this._Label2_8.Text = "Familia Producto:";
        // 
        // fraInfoArchivo
        // 
        this.fraInfoArchivo.BackColor = System.Drawing.SystemColors.Control;
        this.fraInfoArchivo.Controls.Add(this.txtSolRechazadas);
        this.fraInfoArchivo.Controls.Add(this.txtSolAceptadas);
        this.fraInfoArchivo.Controls.Add(this.txtFolFinal);
        this.fraInfoArchivo.Controls.Add(this.txtFolInicial);
        this.fraInfoArchivo.Controls.Add(this.txtNumSolicitudes);
        this.fraInfoArchivo.Controls.Add(this.txtRemesa);
        this.fraInfoArchivo.Controls.Add(this.txtArchivo);
        this.fraInfoArchivo.Controls.Add(this.lblGrabado);
        this.fraInfoArchivo.Controls.Add(this._Label2_6);
        this.fraInfoArchivo.Controls.Add(this._Label2_5);
        this.fraInfoArchivo.Controls.Add(this._Label2_4);
        this.fraInfoArchivo.Controls.Add(this._Label2_3);
        this.fraInfoArchivo.Controls.Add(this._Label2_2);
        this.fraInfoArchivo.Controls.Add(this._Label2_1);
        this.fraInfoArchivo.Controls.Add(this._Label2_0);
        this.fraInfoArchivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.fraInfoArchivo.ForeColor = System.Drawing.Color.Blue;
        this.fraInfoArchivo.Location = new System.Drawing.Point(3, 67);
        this.fraInfoArchivo.Name = "fraInfoArchivo";
        this.fraInfoArchivo.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.fraInfoArchivo.Size = new System.Drawing.Size(497, 185);
        this.fraInfoArchivo.TabIndex = 18;
        this.fraInfoArchivo.TabStop = false;
        this.fraInfoArchivo.Tag = "";
        this.fraInfoArchivo.Text = "Información del Archivo ";
        // 
        // txtSolRechazadas
        // 
        this.txtSolRechazadas.AcceptsReturn = true;
        this.txtSolRechazadas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
        this.txtSolRechazadas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.txtSolRechazadas.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtSolRechazadas.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtSolRechazadas.ForeColor = System.Drawing.Color.Maroon;
        this.txtSolRechazadas.Location = new System.Drawing.Point(416, 120);
        this.txtSolRechazadas.MaxLength = 35;
        this.txtSolRechazadas.Name = "txtSolRechazadas";
        this.txtSolRechazadas.ReadOnly = true;
        this.txtSolRechazadas.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtSolRechazadas.Size = new System.Drawing.Size(73, 20);
        this.txtSolRechazadas.TabIndex = 8;
        this.txtSolRechazadas.TabStop = false;
        this.txtSolRechazadas.Tag = "";
        this.txtSolRechazadas.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        // 
        // txtSolAceptadas
        // 
        this.txtSolAceptadas.AcceptsReturn = true;
        this.txtSolAceptadas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
        this.txtSolAceptadas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.txtSolAceptadas.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtSolAceptadas.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtSolAceptadas.ForeColor = System.Drawing.Color.Maroon;
        this.txtSolAceptadas.Location = new System.Drawing.Point(168, 120);
        this.txtSolAceptadas.MaxLength = 35;
        this.txtSolAceptadas.Name = "txtSolAceptadas";
        this.txtSolAceptadas.ReadOnly = true;
        this.txtSolAceptadas.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtSolAceptadas.Size = new System.Drawing.Size(73, 20);
        this.txtSolAceptadas.TabIndex = 7;
        this.txtSolAceptadas.TabStop = false;
        this.txtSolAceptadas.Tag = "";
        this.txtSolAceptadas.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        // 
        // txtFolFinal
        // 
        this.txtFolFinal.AcceptsReturn = true;
        this.txtFolFinal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
        this.txtFolFinal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.txtFolFinal.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtFolFinal.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtFolFinal.ForeColor = System.Drawing.Color.Maroon;
        this.txtFolFinal.Location = new System.Drawing.Point(336, 88);
        this.txtFolFinal.MaxLength = 35;
        this.txtFolFinal.Name = "txtFolFinal";
        this.txtFolFinal.ReadOnly = true;
        this.txtFolFinal.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtFolFinal.Size = new System.Drawing.Size(153, 20);
        this.txtFolFinal.TabIndex = 6;
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
        this.txtFolInicial.Location = new System.Drawing.Point(88, 88);
        this.txtFolInicial.MaxLength = 35;
        this.txtFolInicial.Name = "txtFolInicial";
        this.txtFolInicial.ReadOnly = true;
        this.txtFolInicial.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtFolInicial.Size = new System.Drawing.Size(153, 20);
        this.txtFolInicial.TabIndex = 5;
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
        this.txtNumSolicitudes.Location = new System.Drawing.Point(336, 56);
        this.txtNumSolicitudes.MaxLength = 35;
        this.txtNumSolicitudes.Name = "txtNumSolicitudes";
        this.txtNumSolicitudes.ReadOnly = true;
        this.txtNumSolicitudes.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtNumSolicitudes.Size = new System.Drawing.Size(153, 20);
        this.txtNumSolicitudes.TabIndex = 4;
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
        this.txtRemesa.Location = new System.Drawing.Point(88, 56);
        this.txtRemesa.MaxLength = 35;
        this.txtRemesa.Name = "txtRemesa";
        this.txtRemesa.ReadOnly = true;
        this.txtRemesa.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtRemesa.Size = new System.Drawing.Size(153, 20);
        this.txtRemesa.TabIndex = 3;
        this.txtRemesa.TabStop = false;
        this.txtRemesa.Tag = "";
        this.txtRemesa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        // 
        // txtArchivo
        // 
        this.txtArchivo.AcceptsReturn = true;
        this.txtArchivo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
        this.txtArchivo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.txtArchivo.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtArchivo.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtArchivo.ForeColor = System.Drawing.Color.Maroon;
        this.txtArchivo.Location = new System.Drawing.Point(88, 24);
        this.txtArchivo.MaxLength = 0;
        this.txtArchivo.Name = "txtArchivo";
        this.txtArchivo.ReadOnly = true;
        this.txtArchivo.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtArchivo.Size = new System.Drawing.Size(401, 20);
        this.txtArchivo.TabIndex = 2;
        this.txtArchivo.TabStop = false;
        this.txtArchivo.Tag = "";
        // 
        // lblGrabado
        // 
        this.lblGrabado.BackColor = System.Drawing.SystemColors.Control;
        this.lblGrabado.Cursor = System.Windows.Forms.Cursors.Default;
        this.lblGrabado.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this.lblGrabado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblGrabado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
        this.lblGrabado.Location = new System.Drawing.Point(6, 160);
        this.lblGrabado.Name = "lblGrabado";
        this.lblGrabado.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.lblGrabado.Size = new System.Drawing.Size(481, 17);
        this.lblGrabado.TabIndex = 26;
        this.lblGrabado.Tag = "";
        this.lblGrabado.Text = "Grabado Automático al existir: 25 % de Solicitudes Aceptadas";
        this.lblGrabado.TextAlign = System.Drawing.ContentAlignment.TopCenter;
        // 
        // _Label2_6
        // 
        this._Label2_6.BackColor = System.Drawing.SystemColors.Control;
        this._Label2_6.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label2_6.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label2_6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label2_6.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label2_6.Location = new System.Drawing.Point(264, 128);
        this._Label2_6.Name = "_Label2_6";
        this._Label2_6.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label2_6.Size = new System.Drawing.Size(145, 17);
        this._Label2_6.TabIndex = 25;
        this._Label2_6.Tag = "";
        this._Label2_6.Text = "Solicitudes Rechazadas:";
        // 
        // _Label2_5
        // 
        this._Label2_5.BackColor = System.Drawing.SystemColors.Control;
        this._Label2_5.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label2_5.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label2_5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label2_5.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label2_5.Location = new System.Drawing.Point(8, 128);
        this._Label2_5.Name = "_Label2_5";
        this._Label2_5.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label2_5.Size = new System.Drawing.Size(145, 17);
        this._Label2_5.TabIndex = 24;
        this._Label2_5.Tag = "";
        this._Label2_5.Text = "Solicitudes Aceptadas:";
        // 
        // _Label2_4
        // 
        this._Label2_4.BackColor = System.Drawing.SystemColors.Control;
        this._Label2_4.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label2_4.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label2_4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label2_4.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label2_4.Location = new System.Drawing.Point(264, 96);
        this._Label2_4.Name = "_Label2_4";
        this._Label2_4.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label2_4.Size = new System.Drawing.Size(73, 17);
        this._Label2_4.TabIndex = 23;
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
        this._Label2_3.Location = new System.Drawing.Point(8, 96);
        this._Label2_3.Name = "_Label2_3";
        this._Label2_3.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label2_3.Size = new System.Drawing.Size(89, 17);
        this._Label2_3.TabIndex = 22;
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
        this._Label2_2.Location = new System.Drawing.Point(264, 56);
        this._Label2_2.Name = "_Label2_2";
        this._Label2_2.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label2_2.Size = new System.Drawing.Size(65, 33);
        this._Label2_2.TabIndex = 21;
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
        this._Label2_1.Location = new System.Drawing.Point(8, 56);
        this._Label2_1.Name = "_Label2_1";
        this._Label2_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label2_1.Size = new System.Drawing.Size(65, 17);
        this._Label2_1.TabIndex = 20;
        this._Label2_1.Tag = "";
        this._Label2_1.Text = "Remesa:";
        // 
        // _Label2_0
        // 
        this._Label2_0.BackColor = System.Drawing.SystemColors.Control;
        this._Label2_0.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label2_0.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label2_0.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label2_0.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label2_0.Location = new System.Drawing.Point(8, 24);
        this._Label2_0.Name = "_Label2_0";
        this._Label2_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label2_0.Size = new System.Drawing.Size(73, 17);
        this._Label2_0.TabIndex = 19;
        this._Label2_0.Tag = "";
        this._Label2_0.Text = "Archivo:";
        // 
        // Timer1
        // 
        this.Timer1.Enabled = true;
        this.Timer1.Interval = 1000;
        // 
        // fraDatosFolio
        // 
        this.fraDatosFolio.BackColor = System.Drawing.SystemColors.Control;
        this.fraDatosFolio.Controls.Add(this.Frame1);
        this.fraDatosFolio.Controls.Add(this.cmdLeeArchivo);
        this.fraDatosFolio.Controls.Add(this.cboTipoTram);
        this.fraDatosFolio.Controls.Add(this._Label1_1);
        this.fraDatosFolio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.fraDatosFolio.ForeColor = System.Drawing.Color.Blue;
        this.fraDatosFolio.Location = new System.Drawing.Point(3, 1);
        this.fraDatosFolio.Name = "fraDatosFolio";
        this.fraDatosFolio.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.fraDatosFolio.Size = new System.Drawing.Size(497, 65);
        this.fraDatosFolio.TabIndex = 16;
        this.fraDatosFolio.TabStop = false;
        this.fraDatosFolio.Tag = "";
        this.fraDatosFolio.Text = "Procesamiento Masivo de Solicitudes ";
        // 
        // Frame1
        // 
        this.Frame1.BackColor = System.Drawing.SystemColors.Control;
        this.Frame1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Frame1.ForeColor = System.Drawing.SystemColors.ControlText;
        this.Frame1.Location = new System.Drawing.Point(104, 64);
        this.Frame1.Name = "Frame1";
        this.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.Frame1.Size = new System.Drawing.Size(345, 1);
        this.Frame1.TabIndex = 33;
        this.Frame1.TabStop = false;
        this.Frame1.Tag = "";
        this.Frame1.Text = "Frame1";
        // 
        // cboTipoTram
        // 
        this.cboTipoTram.BackColor = System.Drawing.SystemColors.Window;
        this.cboTipoTram.Cursor = System.Windows.Forms.Cursors.Default;
        this.cboTipoTram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cboTipoTram.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.cboTipoTram.ForeColor = System.Drawing.Color.Blue;
        this.cboTipoTram.Location = new System.Drawing.Point(120, 24);
        this.cboTipoTram.Name = "cboTipoTram";
        this.cboTipoTram.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.cboTipoTram.Size = new System.Drawing.Size(233, 21);
        this.cboTipoTram.TabIndex = 0;
        this.cboTipoTram.Tag = "PROCESO";
        this.cboTipoTram.SelectedIndexChanged += new System.EventHandler(this.cboTipoTram_SelectedIndexChanged);
        this.cboTipoTram.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboTipoTram_KeyPress);
        this.cboTipoTram.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboTipoTram_KeyDown);
        // 
        // _Label1_1
        // 
        this._Label1_1.BackColor = System.Drawing.SystemColors.Control;
        this._Label1_1.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label1_1.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label1_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label1_1.ForeColor = System.Drawing.Color.Black;
        this._Label1_1.Location = new System.Drawing.Point(16, 24);
        this._Label1_1.Name = "_Label1_1";
        this._Label1_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label1_1.Size = new System.Drawing.Size(105, 17);
        this._Label1_1.TabIndex = 17;
        this._Label1_1.Tag = "";
        this._Label1_1.Text = "Tipo de Trámite:";
        // 
        // frmProcMasivo
        // 
        this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
        this.AutoScroll = true;
        this.BackColor = System.Drawing.SystemColors.Control;
        this.ClientSize = new System.Drawing.Size(503, 431);
        this.ControlBox = false;
        this.Controls.Add(this.prg_proceso);
        this.Controls.Add(this.dlgArchivoAbrir);
        this.Controls.Add(this.cmdSalir);
        this.Controls.Add(this.fraInfoTramite);
        this.Controls.Add(this.fraInfoArchivo);
        this.Controls.Add(this.fraDatosFolio);
        this.Controls.Add(this.MainMenu1);
        this.Cursor = System.Windows.Forms.Cursors.Default;
        this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.Location = new System.Drawing.Point(95, 134);
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.formHelper1.SetMoveable(this, false);
        this.Name = "frmProcMasivo";
        this.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
        this.Tag = "";
        this.Text = "C753 - ARIES Módulo de Procesamiento Masivo";
        this.Closed += new System.EventHandler(this.frmProcMasivo_Closed);
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmProcMasivo_FormClosing);
        this.MainMenu1.ResumeLayout(false);
        this.MainMenu1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.prg_proceso)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dlgArchivoAbrir)).EndInit();
        this.fraInfoTramite.ResumeLayout(false);
        this.fraInfoTramite.PerformLayout();
        this.fraInfoArchivo.ResumeLayout(false);
        this.fraInfoArchivo.PerformLayout();
        this.fraDatosFolio.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.commandButtonHelper1)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

	}
	void  InitializeLabel1()
	{
			this.Label1[1] = _Label1_1;
	}
	void  InitializeLabel2()
	{
			this.Label2[11] = _Label2_11;
			this.Label2[13] = _Label2_13;
			this.Label2[12] = _Label2_12;
			this.Label2[10] = _Label2_10;
			this.Label2[8] = _Label2_8;
			this.Label2[6] = _Label2_6;
			this.Label2[5] = _Label2_5;
			this.Label2[4] = _Label2_4;
			this.Label2[3] = _Label2_3;
			this.Label2[2] = _Label2_2;
			this.Label2[1] = _Label2_1;
			this.Label2[0] = _Label2_0;
	}
#endregion 

        private System.Windows.Forms.ToolStripMenuItem dummy1;
        private System.Windows.Forms.ToolStripMenuItem dummy2;
        private Artinsoft.VB6.Gui.FormHelper formHelper1;
}
}