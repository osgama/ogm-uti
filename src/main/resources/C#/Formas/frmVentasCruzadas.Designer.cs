using System; 
using VB6 = Microsoft.VisualBasic.Compatibility.VB6.Support; 

namespace Masivos
{
	partial class frmVentasCruzadas
	{
	
#region "Upgrade Support "
        public static frmVentasCruzadas m_vb6FormDefInstance;
		private static bool m_InitializingDefInstance;
		public static frmVentasCruzadas DefInstance
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
		public frmVentasCruzadas():base(){
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
			InitializeLabel2();
			//This form is an MDI child.
			//This code simulates the VB6 
			// functionality of automatically
			// loading and showing an MDI
			// child's parent.
			this.MdiParent = Masivos.MDIMasivos.DefInstance;
			Masivos.MDIMasivos.DefInstance.Show();
		}
	public static frmVentasCruzadas CreateInstance()
	{
			frmVentasCruzadas theInstance = new frmVentasCruzadas();
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
	public  System.Windows.Forms.TextBox txtNumRegError;
	public  System.Windows.Forms.Label labConArhError;
	public  System.Windows.Forms.Label labNumRegError;
	public  System.Windows.Forms.GroupBox fraValArchivo;
	public  System.Windows.Forms.Button cmdEnviar;
	public  System.Windows.Forms.Button cmdValidar;
	public  AxMSComctlLib.AxProgressBar prg_proceso;
	public  AxMSComDlg.AxCommonDialog dlgArchivoAbrir;
	public  System.Windows.Forms.Button cmdSalir;
	public  System.Windows.Forms.Timer Timer1;
	public  System.Windows.Forms.TextBox txtArchProce;
	public  System.Windows.Forms.GroupBox Frame1;
	public  System.Windows.Forms.Button cmdAbrirArchivo;
	public  System.Windows.Forms.Label Label1;
	public  System.Windows.Forms.GroupBox fraDatosFolio;
	public  System.Windows.Forms.TextBox txtTrailer;
	public  System.Windows.Forms.TextBox txtHeader;
	public  System.Windows.Forms.TextBox txtRegTipo5;
	public  System.Windows.Forms.TextBox txtRegTipo4;
	public  System.Windows.Forms.TextBox txtRegTipo3;
	public  System.Windows.Forms.TextBox txtRegTipo2;
	public  System.Windows.Forms.TextBox txtRegTipo1;
	public  System.Windows.Forms.TextBox txtNumRegistros;
	public  System.Windows.Forms.TextBox txtFecha;
	public  System.Windows.Forms.TextBox txtArchivo;
	private  System.Windows.Forms.Label _Label2_9;
	private  System.Windows.Forms.Label _Label2_8;
	private  System.Windows.Forms.Label _Label2_7;
	private  System.Windows.Forms.Label _Label2_6;
	private  System.Windows.Forms.Label _Label2_5;
	private  System.Windows.Forms.Label _Label2_4;
	private  System.Windows.Forms.Label _Label2_3;
	private  System.Windows.Forms.Label _Label2_2;
	private  System.Windows.Forms.Label _Label2_1;
	private  System.Windows.Forms.Label _Label2_0;
	public  System.Windows.Forms.GroupBox fraInfoArchivo;
	public System.Windows.Forms.Label[] Label2 = new System.Windows.Forms.Label[10];
	private Artinsoft.VB6.Gui.CommandButtonHelper commandButtonHelper1;
	//NOTE: The following procedure is required by the Windows Form Designer
	//It can be modified using the Windows Form Designer.
	//Do not modify it using the code editor.
	[System.Diagnostics.DebuggerStepThrough]
	 private void  InitializeComponent()
	{
        this.components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVentasCruzadas));
        this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
        this.cmdEnviar = new System.Windows.Forms.Button();
        this.cmdValidar = new System.Windows.Forms.Button();
        this.cmdAbrirArchivo = new System.Windows.Forms.Button();
        this.fraValArchivo = new System.Windows.Forms.GroupBox();
        this.txtNumRegError = new System.Windows.Forms.TextBox();
        this.labConArhError = new System.Windows.Forms.Label();
        this.labNumRegError = new System.Windows.Forms.Label();
        this.prg_proceso = new AxMSComctlLib.AxProgressBar();
        this.dlgArchivoAbrir = new AxMSComDlg.AxCommonDialog();
        this.cmdSalir = new System.Windows.Forms.Button();
        this.Timer1 = new System.Windows.Forms.Timer(this.components);
        this.fraDatosFolio = new System.Windows.Forms.GroupBox();
        this.txtArchProce = new System.Windows.Forms.TextBox();
        this.Frame1 = new System.Windows.Forms.GroupBox();
        this.Label1 = new System.Windows.Forms.Label();
        this.fraInfoArchivo = new System.Windows.Forms.GroupBox();
        this.txtTrailer = new System.Windows.Forms.TextBox();
        this.txtHeader = new System.Windows.Forms.TextBox();
        this.txtRegTipo5 = new System.Windows.Forms.TextBox();
        this.txtRegTipo4 = new System.Windows.Forms.TextBox();
        this.txtRegTipo3 = new System.Windows.Forms.TextBox();
        this.txtRegTipo2 = new System.Windows.Forms.TextBox();
        this.txtRegTipo1 = new System.Windows.Forms.TextBox();
        this.txtNumRegistros = new System.Windows.Forms.TextBox();
        this.txtFecha = new System.Windows.Forms.TextBox();
        this.txtArchivo = new System.Windows.Forms.TextBox();
        this._Label2_9 = new System.Windows.Forms.Label();
        this._Label2_8 = new System.Windows.Forms.Label();
        this._Label2_7 = new System.Windows.Forms.Label();
        this._Label2_6 = new System.Windows.Forms.Label();
        this._Label2_5 = new System.Windows.Forms.Label();
        this._Label2_4 = new System.Windows.Forms.Label();
        this._Label2_3 = new System.Windows.Forms.Label();
        this._Label2_2 = new System.Windows.Forms.Label();
        this._Label2_1 = new System.Windows.Forms.Label();
        this._Label2_0 = new System.Windows.Forms.Label();
        this.commandButtonHelper1 = new Artinsoft.VB6.Gui.CommandButtonHelper(this.components);
        this.fraValArchivo.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.prg_proceso)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dlgArchivoAbrir)).BeginInit();
        this.fraDatosFolio.SuspendLayout();
        this.fraInfoArchivo.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.commandButtonHelper1)).BeginInit();
        this.SuspendLayout();
        // 
        // cmdEnviar
        // 
        this.cmdEnviar.BackColor = System.Drawing.SystemColors.Control;
        this.commandButtonHelper1.SetCorrectEventsBehavior(this.cmdEnviar, true);
        this.cmdEnviar.Cursor = System.Windows.Forms.Cursors.Default;
        this.commandButtonHelper1.SetDisabledPicture(this.cmdEnviar, null);
        this.commandButtonHelper1.SetDownPicture(this.cmdEnviar, null);
        this.cmdEnviar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.cmdEnviar.ForeColor = System.Drawing.SystemColors.ControlText;
        this.cmdEnviar.Image = ((System.Drawing.Image)(resources.GetObject("cmdEnviar.Image")));
        this.cmdEnviar.Location = new System.Drawing.Point(216, 360);
        this.commandButtonHelper1.SetMaskColor(this.cmdEnviar, System.Drawing.Color.Silver);
        this.cmdEnviar.Name = "cmdEnviar";
        this.cmdEnviar.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.cmdEnviar.Size = new System.Drawing.Size(57, 57);
        this.commandButtonHelper1.SetStyle(this.cmdEnviar, 1);
        this.cmdEnviar.TabIndex = 2;
        this.cmdEnviar.Tag = "";
        this.cmdEnviar.Text = "Enviar";
        this.cmdEnviar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
        this.cmdEnviar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
        this.ToolTip1.SetToolTip(this.cmdEnviar, "Seleccionar Archivo");
        this.cmdEnviar.UseVisualStyleBackColor = false;
        this.cmdEnviar.Click += new System.EventHandler(this.cmdEnviar_Click);
        // 
        // cmdValidar
        // 
        this.cmdValidar.BackColor = System.Drawing.SystemColors.Control;
        this.commandButtonHelper1.SetCorrectEventsBehavior(this.cmdValidar, true);
        this.cmdValidar.Cursor = System.Windows.Forms.Cursors.Default;
        this.commandButtonHelper1.SetDisabledPicture(this.cmdValidar, null);
        this.commandButtonHelper1.SetDownPicture(this.cmdValidar, null);
        this.cmdValidar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.cmdValidar.ForeColor = System.Drawing.SystemColors.ControlText;
        this.cmdValidar.Image = ((System.Drawing.Image)(resources.GetObject("cmdValidar.Image")));
        this.cmdValidar.Location = new System.Drawing.Point(144, 360);
        this.commandButtonHelper1.SetMaskColor(this.cmdValidar, System.Drawing.Color.Silver);
        this.cmdValidar.Name = "cmdValidar";
        this.cmdValidar.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.cmdValidar.Size = new System.Drawing.Size(57, 57);
        this.commandButtonHelper1.SetStyle(this.cmdValidar, 1);
        this.cmdValidar.TabIndex = 1;
        this.cmdValidar.Tag = "";
        this.cmdValidar.Text = "Validar";
        this.cmdValidar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
        this.cmdValidar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
        this.ToolTip1.SetToolTip(this.cmdValidar, "Seleccionar Archivo");
        this.cmdValidar.UseVisualStyleBackColor = false;
        this.cmdValidar.Click += new System.EventHandler(this.cmdValidar_Click);
        // 
        // cmdAbrirArchivo
        // 
        this.cmdAbrirArchivo.BackColor = System.Drawing.SystemColors.Control;
        this.commandButtonHelper1.SetCorrectEventsBehavior(this.cmdAbrirArchivo, true);
        this.cmdAbrirArchivo.Cursor = System.Windows.Forms.Cursors.Default;
        this.commandButtonHelper1.SetDisabledPicture(this.cmdAbrirArchivo, null);
        this.commandButtonHelper1.SetDownPicture(this.cmdAbrirArchivo, null);
        this.cmdAbrirArchivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.cmdAbrirArchivo.ForeColor = System.Drawing.SystemColors.ControlText;
        this.cmdAbrirArchivo.Image = ((System.Drawing.Image)(resources.GetObject("cmdAbrirArchivo.Image")));
        this.cmdAbrirArchivo.Location = new System.Drawing.Point(440, 16);
        this.commandButtonHelper1.SetMaskColor(this.cmdAbrirArchivo, System.Drawing.Color.Silver);
        this.cmdAbrirArchivo.Name = "cmdAbrirArchivo";
        this.cmdAbrirArchivo.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.cmdAbrirArchivo.Size = new System.Drawing.Size(41, 41);
        this.commandButtonHelper1.SetStyle(this.cmdAbrirArchivo, 1);
        this.cmdAbrirArchivo.TabIndex = 0;
        this.cmdAbrirArchivo.Tag = "";
        this.cmdAbrirArchivo.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
        this.cmdAbrirArchivo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
        this.ToolTip1.SetToolTip(this.cmdAbrirArchivo, "Seleccionar Archivo");
        this.cmdAbrirArchivo.UseVisualStyleBackColor = false;
        this.cmdAbrirArchivo.Click += new System.EventHandler(this.cmdAbrirArchivo_Click);
        // 
        // fraValArchivo
        // 
        this.fraValArchivo.BackColor = System.Drawing.SystemColors.Control;
        this.fraValArchivo.Controls.Add(this.txtNumRegError);
        this.fraValArchivo.Controls.Add(this.labConArhError);
        this.fraValArchivo.Controls.Add(this.labNumRegError);
        this.fraValArchivo.Enabled = false;
        this.fraValArchivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.fraValArchivo.ForeColor = System.Drawing.Color.Blue;
        this.fraValArchivo.Location = new System.Drawing.Point(8, 312);
        this.fraValArchivo.Name = "fraValArchivo";
        this.fraValArchivo.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.fraValArchivo.Size = new System.Drawing.Size(497, 41);
        this.fraValArchivo.TabIndex = 26;
        this.fraValArchivo.TabStop = false;
        this.fraValArchivo.Tag = "";
        this.fraValArchivo.Text = "Validación de Archivo";
        // 
        // txtNumRegError
        // 
        this.txtNumRegError.AcceptsReturn = true;
        this.txtNumRegError.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
        this.txtNumRegError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.txtNumRegError.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtNumRegError.Enabled = false;
        this.txtNumRegError.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtNumRegError.ForeColor = System.Drawing.Color.Maroon;
        this.txtNumRegError.Location = new System.Drawing.Point(208, 16);
        this.txtNumRegError.MaxLength = 9;
        this.txtNumRegError.Name = "txtNumRegError";
        this.txtNumRegError.ReadOnly = true;
        this.txtNumRegError.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtNumRegError.Size = new System.Drawing.Size(113, 20);
        this.txtNumRegError.TabIndex = 28;
        this.txtNumRegError.TabStop = false;
        this.txtNumRegError.Tag = "";
        this.txtNumRegError.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        // 
        // labConArhError
        // 
        this.labConArhError.BackColor = System.Drawing.SystemColors.Control;
        this.labConArhError.Cursor = System.Windows.Forms.Cursors.Default;
        this.labConArhError.Enabled = false;
        this.labConArhError.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this.labConArhError.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.labConArhError.ForeColor = System.Drawing.SystemColors.ControlText;
        this.labConArhError.Location = new System.Drawing.Point(352, 8);
        this.labConArhError.Name = "labConArhError";
        this.labConArhError.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.labConArhError.Size = new System.Drawing.Size(137, 25);
        this.labConArhError.TabIndex = 29;
        this.labConArhError.Tag = "";
        this.labConArhError.Text = "Consultar archivo de errores para ver detalles.";
        // 
        // labNumRegError
        // 
        this.labNumRegError.BackColor = System.Drawing.SystemColors.Control;
        this.labNumRegError.Cursor = System.Windows.Forms.Cursors.Default;
        this.labNumRegError.Enabled = false;
        this.labNumRegError.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this.labNumRegError.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.labNumRegError.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
        this.labNumRegError.Location = new System.Drawing.Point(24, 16);
        this.labNumRegError.Name = "labNumRegError";
        this.labNumRegError.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.labNumRegError.Size = new System.Drawing.Size(185, 17);
        this.labNumRegError.TabIndex = 27;
        this.labNumRegError.Tag = "";
        this.labNumRegError.Text = "No. de Registros con ERROR:";
        // 
        // prg_proceso
        // 
        this.prg_proceso.Location = new System.Drawing.Point(8, 424);
        this.prg_proceso.Name = "prg_proceso";
        this.prg_proceso.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("prg_proceso.OcxState")));
        this.prg_proceso.Size = new System.Drawing.Size(493, 16);
        this.prg_proceso.TabIndex = 22;
        this.prg_proceso.Tag = "";
        // 
        // dlgArchivoAbrir
        // 
        this.dlgArchivoAbrir.Enabled = true;
        this.dlgArchivoAbrir.Location = new System.Drawing.Point(456, 320);
        this.dlgArchivoAbrir.Name = "dlgArchivoAbrir";
        this.dlgArchivoAbrir.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("dlgArchivoAbrir.OcxState")));
        this.dlgArchivoAbrir.Size = new System.Drawing.Size(32, 32);
        this.dlgArchivoAbrir.TabIndex = 27;
        this.dlgArchivoAbrir.Tag = "";
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
        this.cmdSalir.Location = new System.Drawing.Point(336, 360);
        this.commandButtonHelper1.SetMaskColor(this.cmdSalir, System.Drawing.Color.Silver);
        this.cmdSalir.Name = "cmdSalir";
        this.cmdSalir.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.cmdSalir.Size = new System.Drawing.Size(57, 57);
        this.commandButtonHelper1.SetStyle(this.cmdSalir, 1);
        this.cmdSalir.TabIndex = 3;
        this.cmdSalir.Tag = "";
        this.cmdSalir.Text = "&Salir";
        this.cmdSalir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
        this.cmdSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
        this.cmdSalir.UseVisualStyleBackColor = false;
        this.cmdSalir.Click += new System.EventHandler(this.cmdSalir_Click);
        // 
        // Timer1
        // 
        this.Timer1.Enabled = true;
        this.Timer1.Interval = 1000;
        // 
        // fraDatosFolio
        // 
        this.fraDatosFolio.BackColor = System.Drawing.SystemColors.Control;
        this.fraDatosFolio.Controls.Add(this.txtArchProce);
        this.fraDatosFolio.Controls.Add(this.Frame1);
        this.fraDatosFolio.Controls.Add(this.cmdAbrirArchivo);
        this.fraDatosFolio.Controls.Add(this.Label1);
        this.fraDatosFolio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.fraDatosFolio.ForeColor = System.Drawing.Color.Blue;
        this.fraDatosFolio.Location = new System.Drawing.Point(8, 8);
        this.fraDatosFolio.Name = "fraDatosFolio";
        this.fraDatosFolio.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.fraDatosFolio.Size = new System.Drawing.Size(497, 65);
        this.fraDatosFolio.TabIndex = 6;
        this.fraDatosFolio.TabStop = false;
        this.fraDatosFolio.Tag = "";
        this.fraDatosFolio.Text = "Transferencia de Archivo de Ventas Cruzadas";
        // 
        // txtArchProce
        // 
        this.txtArchProce.AcceptsReturn = true;
        this.txtArchProce.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
        this.txtArchProce.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.txtArchProce.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtArchProce.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtArchProce.ForeColor = System.Drawing.Color.Maroon;
        this.txtArchProce.Location = new System.Drawing.Point(16, 32);
        this.txtArchProce.MaxLength = 0;
        this.txtArchProce.Name = "txtArchProce";
        this.txtArchProce.ReadOnly = true;
        this.txtArchProce.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtArchProce.Size = new System.Drawing.Size(401, 20);
        this.txtArchProce.TabIndex = 5;
        this.txtArchProce.TabStop = false;
        this.txtArchProce.Tag = "";
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
        this.Frame1.TabIndex = 21;
        this.Frame1.TabStop = false;
        this.Frame1.Tag = "";
        this.Frame1.Text = "Frame1";
        // 
        // Label1
        // 
        this.Label1.BackColor = System.Drawing.SystemColors.Control;
        this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
        this.Label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
        this.Label1.Location = new System.Drawing.Point(16, 16);
        this.Label1.Name = "Label1";
        this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.Label1.Size = new System.Drawing.Size(121, 25);
        this.Label1.TabIndex = 23;
        this.Label1.Tag = "";
        this.Label1.Text = "Selecciona Archivo:";
        // 
        // fraInfoArchivo
        // 
        this.fraInfoArchivo.BackColor = System.Drawing.SystemColors.Control;
        this.fraInfoArchivo.Controls.Add(this.txtTrailer);
        this.fraInfoArchivo.Controls.Add(this.txtHeader);
        this.fraInfoArchivo.Controls.Add(this.txtRegTipo5);
        this.fraInfoArchivo.Controls.Add(this.txtRegTipo4);
        this.fraInfoArchivo.Controls.Add(this.txtRegTipo3);
        this.fraInfoArchivo.Controls.Add(this.txtRegTipo2);
        this.fraInfoArchivo.Controls.Add(this.txtRegTipo1);
        this.fraInfoArchivo.Controls.Add(this.txtNumRegistros);
        this.fraInfoArchivo.Controls.Add(this.txtFecha);
        this.fraInfoArchivo.Controls.Add(this.txtArchivo);
        this.fraInfoArchivo.Controls.Add(this._Label2_9);
        this.fraInfoArchivo.Controls.Add(this._Label2_8);
        this.fraInfoArchivo.Controls.Add(this._Label2_7);
        this.fraInfoArchivo.Controls.Add(this._Label2_6);
        this.fraInfoArchivo.Controls.Add(this._Label2_5);
        this.fraInfoArchivo.Controls.Add(this._Label2_4);
        this.fraInfoArchivo.Controls.Add(this._Label2_3);
        this.fraInfoArchivo.Controls.Add(this._Label2_2);
        this.fraInfoArchivo.Controls.Add(this._Label2_1);
        this.fraInfoArchivo.Controls.Add(this._Label2_0);
        this.fraInfoArchivo.Enabled = false;
        this.fraInfoArchivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.fraInfoArchivo.ForeColor = System.Drawing.Color.Blue;
        this.fraInfoArchivo.Location = new System.Drawing.Point(8, 88);
        this.fraInfoArchivo.Name = "fraInfoArchivo";
        this.fraInfoArchivo.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.fraInfoArchivo.Size = new System.Drawing.Size(497, 209);
        this.fraInfoArchivo.TabIndex = 7;
        this.fraInfoArchivo.TabStop = false;
        this.fraInfoArchivo.Tag = "";
        this.fraInfoArchivo.Text = "Información del Archivo ";
        // 
        // txtTrailer
        // 
        this.txtTrailer.AcceptsReturn = true;
        this.txtTrailer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
        this.txtTrailer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.txtTrailer.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtTrailer.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtTrailer.ForeColor = System.Drawing.Color.Maroon;
        this.txtTrailer.Location = new System.Drawing.Point(456, 24);
        this.txtTrailer.MaxLength = 2;
        this.txtTrailer.Name = "txtTrailer";
        this.txtTrailer.ReadOnly = true;
        this.txtTrailer.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtTrailer.Size = new System.Drawing.Size(33, 20);
        this.txtTrailer.TabIndex = 33;
        this.txtTrailer.TabStop = false;
        this.txtTrailer.Tag = "";
        this.txtTrailer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        // 
        // txtHeader
        // 
        this.txtHeader.AcceptsReturn = true;
        this.txtHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
        this.txtHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.txtHeader.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtHeader.ForeColor = System.Drawing.Color.Maroon;
        this.txtHeader.Location = new System.Drawing.Point(360, 24);
        this.txtHeader.MaxLength = 2;
        this.txtHeader.Name = "txtHeader";
        this.txtHeader.ReadOnly = true;
        this.txtHeader.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtHeader.Size = new System.Drawing.Size(33, 20);
        this.txtHeader.TabIndex = 32;
        this.txtHeader.TabStop = false;
        this.txtHeader.Tag = "";
        this.txtHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        // 
        // txtRegTipo5
        // 
        this.txtRegTipo5.AcceptsReturn = true;
        this.txtRegTipo5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
        this.txtRegTipo5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.txtRegTipo5.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtRegTipo5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtRegTipo5.ForeColor = System.Drawing.Color.Maroon;
        this.txtRegTipo5.Location = new System.Drawing.Point(360, 152);
        this.txtRegTipo5.MaxLength = 9;
        this.txtRegTipo5.Name = "txtRegTipo5";
        this.txtRegTipo5.ReadOnly = true;
        this.txtRegTipo5.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtRegTipo5.Size = new System.Drawing.Size(113, 20);
        this.txtRegTipo5.TabIndex = 25;
        this.txtRegTipo5.TabStop = false;
        this.txtRegTipo5.Tag = "";
        this.txtRegTipo5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        // 
        // txtRegTipo4
        // 
        this.txtRegTipo4.AcceptsReturn = true;
        this.txtRegTipo4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
        this.txtRegTipo4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.txtRegTipo4.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtRegTipo4.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtRegTipo4.ForeColor = System.Drawing.Color.Maroon;
        this.txtRegTipo4.Location = new System.Drawing.Point(360, 128);
        this.txtRegTipo4.MaxLength = 9;
        this.txtRegTipo4.Name = "txtRegTipo4";
        this.txtRegTipo4.ReadOnly = true;
        this.txtRegTipo4.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtRegTipo4.Size = new System.Drawing.Size(113, 20);
        this.txtRegTipo4.TabIndex = 20;
        this.txtRegTipo4.TabStop = false;
        this.txtRegTipo4.Tag = "";
        this.txtRegTipo4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        // 
        // txtRegTipo3
        // 
        this.txtRegTipo3.AcceptsReturn = true;
        this.txtRegTipo3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
        this.txtRegTipo3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.txtRegTipo3.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtRegTipo3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtRegTipo3.ForeColor = System.Drawing.Color.Maroon;
        this.txtRegTipo3.Location = new System.Drawing.Point(360, 104);
        this.txtRegTipo3.MaxLength = 9;
        this.txtRegTipo3.Name = "txtRegTipo3";
        this.txtRegTipo3.ReadOnly = true;
        this.txtRegTipo3.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtRegTipo3.Size = new System.Drawing.Size(113, 20);
        this.txtRegTipo3.TabIndex = 19;
        this.txtRegTipo3.TabStop = false;
        this.txtRegTipo3.Tag = "";
        this.txtRegTipo3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        // 
        // txtRegTipo2
        // 
        this.txtRegTipo2.AcceptsReturn = true;
        this.txtRegTipo2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
        this.txtRegTipo2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.txtRegTipo2.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtRegTipo2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtRegTipo2.ForeColor = System.Drawing.Color.Maroon;
        this.txtRegTipo2.Location = new System.Drawing.Point(360, 80);
        this.txtRegTipo2.MaxLength = 9;
        this.txtRegTipo2.Name = "txtRegTipo2";
        this.txtRegTipo2.ReadOnly = true;
        this.txtRegTipo2.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtRegTipo2.Size = new System.Drawing.Size(113, 20);
        this.txtRegTipo2.TabIndex = 18;
        this.txtRegTipo2.TabStop = false;
        this.txtRegTipo2.Tag = "";
        this.txtRegTipo2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        // 
        // txtRegTipo1
        // 
        this.txtRegTipo1.AcceptsReturn = true;
        this.txtRegTipo1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
        this.txtRegTipo1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.txtRegTipo1.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtRegTipo1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtRegTipo1.ForeColor = System.Drawing.Color.Maroon;
        this.txtRegTipo1.Location = new System.Drawing.Point(360, 56);
        this.txtRegTipo1.MaxLength = 9;
        this.txtRegTipo1.Name = "txtRegTipo1";
        this.txtRegTipo1.ReadOnly = true;
        this.txtRegTipo1.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtRegTipo1.Size = new System.Drawing.Size(113, 20);
        this.txtRegTipo1.TabIndex = 17;
        this.txtRegTipo1.TabStop = false;
        this.txtRegTipo1.Tag = "";
        this.txtRegTipo1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        // 
        // txtNumRegistros
        // 
        this.txtNumRegistros.AcceptsReturn = true;
        this.txtNumRegistros.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
        this.txtNumRegistros.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.txtNumRegistros.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtNumRegistros.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtNumRegistros.ForeColor = System.Drawing.Color.Maroon;
        this.txtNumRegistros.Location = new System.Drawing.Point(360, 176);
        this.txtNumRegistros.MaxLength = 9;
        this.txtNumRegistros.Name = "txtNumRegistros";
        this.txtNumRegistros.ReadOnly = true;
        this.txtNumRegistros.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtNumRegistros.Size = new System.Drawing.Size(113, 21);
        this.txtNumRegistros.TabIndex = 16;
        this.txtNumRegistros.TabStop = false;
        this.txtNumRegistros.Tag = "";
        this.txtNumRegistros.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        // 
        // txtFecha
        // 
        this.txtFecha.AcceptsReturn = true;
        this.txtFecha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
        this.txtFecha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.txtFecha.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtFecha.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtFecha.ForeColor = System.Drawing.Color.Maroon;
        this.txtFecha.Location = new System.Drawing.Point(208, 24);
        this.txtFecha.MaxLength = 8;
        this.txtFecha.Name = "txtFecha";
        this.txtFecha.ReadOnly = true;
        this.txtFecha.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtFecha.Size = new System.Drawing.Size(89, 20);
        this.txtFecha.TabIndex = 15;
        this.txtFecha.TabStop = false;
        this.txtFecha.Tag = "";
        this.txtFecha.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        // 
        // txtArchivo
        // 
        this.txtArchivo.AcceptsReturn = true;
        this.txtArchivo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
        this.txtArchivo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.txtArchivo.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtArchivo.Enabled = false;
        this.txtArchivo.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtArchivo.ForeColor = System.Drawing.Color.Maroon;
        this.txtArchivo.Location = new System.Drawing.Point(56, 24);
        this.txtArchivo.MaxLength = 12;
        this.txtArchivo.Name = "txtArchivo";
        this.txtArchivo.ReadOnly = true;
        this.txtArchivo.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtArchivo.Size = new System.Drawing.Size(97, 20);
        this.txtArchivo.TabIndex = 4;
        this.txtArchivo.TabStop = false;
        this.txtArchivo.Tag = "";
        this.txtArchivo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        // 
        // _Label2_9
        // 
        this._Label2_9.BackColor = System.Drawing.SystemColors.Control;
        this._Label2_9.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label2_9.Enabled = false;
        this._Label2_9.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label2_9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label2_9.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label2_9.Location = new System.Drawing.Point(408, 24);
        this._Label2_9.Name = "_Label2_9";
        this._Label2_9.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label2_9.Size = new System.Drawing.Size(49, 17);
        this._Label2_9.TabIndex = 31;
        this._Label2_9.Tag = "";
        this._Label2_9.Text = "Trailer:";
        // 
        // _Label2_8
        // 
        this._Label2_8.BackColor = System.Drawing.SystemColors.Control;
        this._Label2_8.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label2_8.Enabled = false;
        this._Label2_8.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label2_8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label2_8.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label2_8.Location = new System.Drawing.Point(312, 24);
        this._Label2_8.Name = "_Label2_8";
        this._Label2_8.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label2_8.Size = new System.Drawing.Size(49, 17);
        this._Label2_8.TabIndex = 30;
        this._Label2_8.Tag = "";
        this._Label2_8.Text = "Header:";
        // 
        // _Label2_7
        // 
        this._Label2_7.BackColor = System.Drawing.SystemColors.Control;
        this._Label2_7.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label2_7.Enabled = false;
        this._Label2_7.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label2_7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label2_7.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label2_7.Location = new System.Drawing.Point(8, 152);
        this._Label2_7.Name = "_Label2_7";
        this._Label2_7.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label2_7.Size = new System.Drawing.Size(345, 17);
        this._Label2_7.TabIndex = 24;
        this._Label2_7.Tag = "";
        this._Label2_7.Text = "Registros Tipo 05 (Borrado x Rango de Folios).................. :";
        // 
        // _Label2_6
        // 
        this._Label2_6.BackColor = System.Drawing.SystemColors.Control;
        this._Label2_6.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label2_6.Enabled = false;
        this._Label2_6.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label2_6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label2_6.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label2_6.Location = new System.Drawing.Point(8, 128);
        this._Label2_6.Name = "_Label2_6";
        this._Label2_6.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label2_6.Size = new System.Drawing.Size(369, 17);
        this._Label2_6.TabIndex = 14;
        this._Label2_6.Tag = "";
        this._Label2_6.Text = "Registros Tipo 04 (Borrado x Rango de Fecha de Promo.).. :";
        // 
        // _Label2_5
        // 
        this._Label2_5.BackColor = System.Drawing.SystemColors.Control;
        this._Label2_5.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label2_5.Enabled = false;
        this._Label2_5.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label2_5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label2_5.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label2_5.Location = new System.Drawing.Point(8, 104);
        this._Label2_5.Name = "_Label2_5";
        this._Label2_5.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label2_5.Size = new System.Drawing.Size(345, 17);
        this._Label2_5.TabIndex = 13;
        this._Label2_5.Tag = "";
        this._Label2_5.Text = "Registros Tipo 03 (Borrado x Clave de Promoción)............. :";
        // 
        // _Label2_4
        // 
        this._Label2_4.BackColor = System.Drawing.SystemColors.Control;
        this._Label2_4.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label2_4.Enabled = false;
        this._Label2_4.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label2_4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label2_4.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label2_4.Location = new System.Drawing.Point(8, 80);
        this._Label2_4.Name = "_Label2_4";
        this._Label2_4.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label2_4.Size = new System.Drawing.Size(345, 17);
        this._Label2_4.TabIndex = 12;
        this._Label2_4.Tag = "";
        this._Label2_4.Text = "Registros Tipo 02 (Borrado x Número de Folio).................. :";
        // 
        // _Label2_3
        // 
        this._Label2_3.BackColor = System.Drawing.SystemColors.Control;
        this._Label2_3.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label2_3.Enabled = false;
        this._Label2_3.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label2_3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label2_3.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label2_3.Location = new System.Drawing.Point(8, 56);
        this._Label2_3.Name = "_Label2_3";
        this._Label2_3.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label2_3.Size = new System.Drawing.Size(345, 17);
        this._Label2_3.TabIndex = 11;
        this._Label2_3.Tag = "";
        this._Label2_3.Text = "Registros Tipo 01 (Alta de Información)............................ :";
        // 
        // _Label2_2
        // 
        this._Label2_2.BackColor = System.Drawing.SystemColors.Control;
        this._Label2_2.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label2_2.Enabled = false;
        this._Label2_2.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label2_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label2_2.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label2_2.Location = new System.Drawing.Point(8, 176);
        this._Label2_2.Name = "_Label2_2";
        this._Label2_2.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label2_2.Size = new System.Drawing.Size(345, 17);
        this._Label2_2.TabIndex = 10;
        this._Label2_2.Tag = "";
        this._Label2_2.Text = "No. Total de Registros .................................................. :";
        // 
        // _Label2_1
        // 
        this._Label2_1.BackColor = System.Drawing.SystemColors.Control;
        this._Label2_1.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label2_1.Enabled = false;
        this._Label2_1.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label2_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label2_1.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label2_1.Location = new System.Drawing.Point(168, 24);
        this._Label2_1.Name = "_Label2_1";
        this._Label2_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label2_1.Size = new System.Drawing.Size(65, 17);
        this._Label2_1.TabIndex = 9;
        this._Label2_1.Tag = "";
        this._Label2_1.Text = "Fecha:";
        // 
        // _Label2_0
        // 
        this._Label2_0.BackColor = System.Drawing.SystemColors.Control;
        this._Label2_0.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label2_0.Enabled = false;
        this._Label2_0.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label2_0.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label2_0.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label2_0.Location = new System.Drawing.Point(8, 24);
        this._Label2_0.Name = "_Label2_0";
        this._Label2_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label2_0.Size = new System.Drawing.Size(73, 17);
        this._Label2_0.TabIndex = 8;
        this._Label2_0.Tag = "";
        this._Label2_0.Text = "Archivo:";
        // 
        // frmVentasCruzadas
        // 
        this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
        this.AutoScroll = true;
        this.BackColor = System.Drawing.SystemColors.Control;
        this.ClientSize = new System.Drawing.Size(505, 444);
        this.Controls.Add(this.fraValArchivo);
        this.Controls.Add(this.cmdEnviar);
        this.Controls.Add(this.cmdValidar);
        this.Controls.Add(this.prg_proceso);
        this.Controls.Add(this.dlgArchivoAbrir);
        this.Controls.Add(this.cmdSalir);
        this.Controls.Add(this.fraDatosFolio);
        this.Controls.Add(this.fraInfoArchivo);
        this.Cursor = System.Windows.Forms.Cursors.Default;
        this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.Location = new System.Drawing.Point(95, 115);
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "frmVentasCruzadas";
        this.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
        this.Tag = "";
        this.Text = "Ventas Cruzadas";
        this.Closed += new System.EventHandler(this.frmVentasCruzadas_Closed);
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmVentasCruzadas_FormClosing);
        this.fraValArchivo.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.prg_proceso)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dlgArchivoAbrir)).EndInit();
        this.fraDatosFolio.ResumeLayout(false);
        this.fraInfoArchivo.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.commandButtonHelper1)).EndInit();
        this.ResumeLayout(false);

	}
	void  InitializeLabel2()
	{
			this.Label2[9] = _Label2_9;
			this.Label2[8] = _Label2_8;
			this.Label2[7] = _Label2_7;
			this.Label2[6] = _Label2_6;
			this.Label2[5] = _Label2_5;
			this.Label2[4] = _Label2_4;
			this.Label2[3] = _Label2_3;
			this.Label2[2] = _Label2_2;
			this.Label2[1] = _Label2_1;
			this.Label2[0] = _Label2_0;
	}
#endregion 
}
}