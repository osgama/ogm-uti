using System; 
using VB6 = Microsoft.VisualBasic.Compatibility.VB6.Support; 

namespace Masivos
{
	partial class frmConsRemesa
	{
	
#region "Upgrade Support "
        public static frmConsRemesa m_vb6FormDefInstance;
		private static bool m_InitializingDefInstance;
		public static frmConsRemesa DefInstance
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
		public frmConsRemesa():base(){
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
	public static frmConsRemesa CreateInstance()
	{
			frmConsRemesa theInstance = new frmConsRemesa();
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
	public  System.Windows.Forms.ToolStripMenuItem mnuProcesar;
	public  System.Windows.Forms.ToolStripMenuItem mnuLimpiar;
	public  System.Windows.Forms.ToolStripSeparator mnuSep1;
	public  System.Windows.Forms.ToolStripMenuItem mnuSalir;
	public  System.Windows.Forms.ToolStripMenuItem mnuConsultaEstatus;
	public  System.Windows.Forms.MenuStrip MainMenu1;
	public  System.Windows.Forms.TextBox txtEstatus;
	public  System.Windows.Forms.MaskedTextBox mskFecEstatus;
	private  System.Windows.Forms.Label _Label2_2;
	private  System.Windows.Forms.Label _Label1_1;
	public  System.Windows.Forms.GroupBox Frame2;
	public  System.Windows.Forms.Button cmdProcesar;
	public  System.Windows.Forms.Button cmdSalir;
	public  System.Windows.Forms.Button cmdLimpiar;
	public  System.Windows.Forms.GroupBox Frame3;
	public  System.Windows.Forms.TextBox txtFolFinal;
	public  System.Windows.Forms.TextBox txtFolInicial;
	public  System.Windows.Forms.TextBox txtArchivo;
	public  System.Windows.Forms.MaskedTextBox mskFecProceso;
	private  System.Windows.Forms.Label _Label2_1;
	private  System.Windows.Forms.Label _Label2_4;
	private  System.Windows.Forms.Label _Label2_3;
	private  System.Windows.Forms.Label _Label2_0;
	public  System.Windows.Forms.GroupBox fraInfoArchivo;
	public  System.Windows.Forms.TextBox txtClaveRemesa;
	private  System.Windows.Forms.Label _Label1_0;
	public  System.Windows.Forms.GroupBox Frame1;
	public System.Windows.Forms.Label[] Label1 = new System.Windows.Forms.Label[2];
	public System.Windows.Forms.Label[] Label2 = new System.Windows.Forms.Label[5];
	private Artinsoft.VB6.Gui.CommandButtonHelper commandButtonHelper1;
	//NOTE: The following procedure is required by the Windows Form Designer
	//It can be modified using the Windows Form Designer.
	//Do not modify it using the code editor.
	[System.Diagnostics.DebuggerStepThrough]
	 private void  InitializeComponent()
	{
        this.components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConsRemesa));
        this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
        this.cmdProcesar = new System.Windows.Forms.Button();
        this.cmdSalir = new System.Windows.Forms.Button();
        this.cmdLimpiar = new System.Windows.Forms.Button();
        this.MainMenu1 = new System.Windows.Forms.MenuStrip();
        this.mnuConsultaEstatus = new System.Windows.Forms.ToolStripMenuItem();
        this.mnuProcesar = new System.Windows.Forms.ToolStripMenuItem();
        this.mnuLimpiar = new System.Windows.Forms.ToolStripMenuItem();
        this.mnuSep1 = new System.Windows.Forms.ToolStripSeparator();
        this.mnuSalir = new System.Windows.Forms.ToolStripMenuItem();
        this.dummy1 = new System.Windows.Forms.ToolStripMenuItem();
        this.dummy2 = new System.Windows.Forms.ToolStripMenuItem();
        this.Frame2 = new System.Windows.Forms.GroupBox();
        this.txtEstatus = new System.Windows.Forms.TextBox();
        this.mskFecEstatus = new System.Windows.Forms.MaskedTextBox();
        this._Label2_2 = new System.Windows.Forms.Label();
        this._Label1_1 = new System.Windows.Forms.Label();
        this.Frame3 = new System.Windows.Forms.GroupBox();
        this.fraInfoArchivo = new System.Windows.Forms.GroupBox();
        this.txtFolFinal = new System.Windows.Forms.TextBox();
        this.txtFolInicial = new System.Windows.Forms.TextBox();
        this.txtArchivo = new System.Windows.Forms.TextBox();
        this.mskFecProceso = new System.Windows.Forms.MaskedTextBox();
        this._Label2_1 = new System.Windows.Forms.Label();
        this._Label2_4 = new System.Windows.Forms.Label();
        this._Label2_3 = new System.Windows.Forms.Label();
        this._Label2_0 = new System.Windows.Forms.Label();
        this.Frame1 = new System.Windows.Forms.GroupBox();
        this.txtClaveRemesa = new System.Windows.Forms.TextBox();
        this._Label1_0 = new System.Windows.Forms.Label();
        this.commandButtonHelper1 = new Artinsoft.VB6.Gui.CommandButtonHelper(this.components);
        this.formHelper1 = new Artinsoft.VB6.Gui.FormHelper(this.components);
        this.MainMenu1.SuspendLayout();
        this.Frame2.SuspendLayout();
        this.Frame3.SuspendLayout();
        this.fraInfoArchivo.SuspendLayout();
        this.Frame1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.commandButtonHelper1)).BeginInit();
        this.SuspendLayout();
        // 
        // cmdProcesar
        // 
        this.cmdProcesar.BackColor = System.Drawing.SystemColors.Control;
        this.commandButtonHelper1.SetCorrectEventsBehavior(this.cmdProcesar, true);
        this.cmdProcesar.Cursor = System.Windows.Forms.Cursors.Default;
        this.commandButtonHelper1.SetDisabledPicture(this.cmdProcesar, null);
        this.commandButtonHelper1.SetDownPicture(this.cmdProcesar, null);
        this.cmdProcesar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.cmdProcesar.ForeColor = System.Drawing.SystemColors.ControlText;
        this.cmdProcesar.Image = ((System.Drawing.Image)(resources.GetObject("cmdProcesar.Image")));
        this.cmdProcesar.Location = new System.Drawing.Point(92, 17);
        this.commandButtonHelper1.SetMaskColor(this.cmdProcesar, System.Drawing.Color.Silver);
        this.cmdProcesar.Name = "cmdProcesar";
        this.cmdProcesar.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.cmdProcesar.Size = new System.Drawing.Size(57, 51);
        this.commandButtonHelper1.SetStyle(this.cmdProcesar, 1);
        this.cmdProcesar.TabIndex = 8;
        this.cmdProcesar.Tag = "";
        this.cmdProcesar.Text = "Procesar";
        this.cmdProcesar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
        this.cmdProcesar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
        this.ToolTip1.SetToolTip(this.cmdProcesar, "Actualizar la prioridad de la remesa");
        this.cmdProcesar.UseVisualStyleBackColor = false;
        this.cmdProcesar.Click += new System.EventHandler(this.cmdProcesar_Click);
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
        this.cmdSalir.Location = new System.Drawing.Point(225, 18);
        this.commandButtonHelper1.SetMaskColor(this.cmdSalir, System.Drawing.Color.Silver);
        this.cmdSalir.Name = "cmdSalir";
        this.cmdSalir.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.cmdSalir.Size = new System.Drawing.Size(52, 52);
        this.commandButtonHelper1.SetStyle(this.cmdSalir, 1);
        this.cmdSalir.TabIndex = 10;
        this.cmdSalir.Tag = "";
        this.cmdSalir.Text = "Salir";
        this.cmdSalir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
        this.cmdSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
        this.ToolTip1.SetToolTip(this.cmdSalir, "Salir de este módulo");
        this.cmdSalir.UseVisualStyleBackColor = false;
        this.cmdSalir.Click += new System.EventHandler(this.cmdSalir_Click);
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
        this.cmdLimpiar.Location = new System.Drawing.Point(159, 18);
        this.commandButtonHelper1.SetMaskColor(this.cmdLimpiar, System.Drawing.Color.Silver);
        this.cmdLimpiar.Name = "cmdLimpiar";
        this.cmdLimpiar.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.cmdLimpiar.Size = new System.Drawing.Size(52, 52);
        this.commandButtonHelper1.SetStyle(this.cmdLimpiar, 1);
        this.cmdLimpiar.TabIndex = 9;
        this.cmdLimpiar.Tag = "";
        this.cmdLimpiar.Text = "Limpiar";
        this.cmdLimpiar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
        this.cmdLimpiar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
        this.ToolTip1.SetToolTip(this.cmdLimpiar, "Limpiar campos");
        this.cmdLimpiar.UseVisualStyleBackColor = false;
        this.cmdLimpiar.Click += new System.EventHandler(this.cmdLimpiar_Click);
        // 
        // MainMenu1
        // 
        this.MainMenu1.BackColor = System.Drawing.SystemColors.ControlLight;
        this.MainMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuConsultaEstatus,
            this.dummy1,
            this.dummy2});
        this.MainMenu1.Location = new System.Drawing.Point(0, 0);
        this.MainMenu1.Name = "MainMenu1";
        this.MainMenu1.Size = new System.Drawing.Size(364, 24);
        this.MainMenu1.TabIndex = 19;
        this.MainMenu1.Visible = false;
        // 
        // mnuConsultaEstatus
        // 
        this.mnuConsultaEstatus.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuProcesar,
            this.mnuLimpiar,
            this.mnuSep1,
            this.mnuSalir});
        this.mnuConsultaEstatus.MergeAction = System.Windows.Forms.MergeAction.Replace;
        this.mnuConsultaEstatus.MergeIndex = 0;
        this.mnuConsultaEstatus.Name = "mnuConsultaEstatus";
        this.mnuConsultaEstatus.Size = new System.Drawing.Size(167, 20);
        this.mnuConsultaEstatus.Tag = "";
        this.mnuConsultaEstatus.Text = "&Consulta Estatus de la Remesa";
        // 
        // mnuProcesar
        // 
        this.mnuProcesar.Name = "mnuProcesar";
        this.mnuProcesar.Size = new System.Drawing.Size(127, 22);
        this.mnuProcesar.Tag = "";
        this.mnuProcesar.Text = "&Procesar";
        this.mnuProcesar.Click += new System.EventHandler(this.mnuProcesar_Click);
        // 
        // mnuLimpiar
        // 
        this.mnuLimpiar.Name = "mnuLimpiar";
        this.mnuLimpiar.Size = new System.Drawing.Size(127, 22);
        this.mnuLimpiar.Tag = "";
        this.mnuLimpiar.Text = "&Limpiar";
        this.mnuLimpiar.Click += new System.EventHandler(this.mnuLimpiar_Click);
        // 
        // mnuSep1
        // 
        this.mnuSep1.Name = "mnuSep1";
        this.mnuSep1.Size = new System.Drawing.Size(124, 6);
        this.mnuSep1.Tag = "";
        // 
        // mnuSalir
        // 
        this.mnuSalir.Name = "mnuSalir";
        this.mnuSalir.Size = new System.Drawing.Size(127, 22);
        this.mnuSalir.Tag = "";
        this.mnuSalir.Text = "&Salir";
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
        // Frame2
        // 
        this.Frame2.BackColor = System.Drawing.SystemColors.Control;
        this.Frame2.Controls.Add(this.txtEstatus);
        this.Frame2.Controls.Add(this.mskFecEstatus);
        this.Frame2.Controls.Add(this._Label2_2);
        this.Frame2.Controls.Add(this._Label1_1);
        this.Frame2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Frame2.ForeColor = System.Drawing.SystemColors.ControlText;
        this.Frame2.Location = new System.Drawing.Point(0, 53);
        this.Frame2.Name = "Frame2";
        this.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.Frame2.Size = new System.Drawing.Size(361, 73);
        this.Frame2.TabIndex = 18;
        this.Frame2.TabStop = false;
        this.Frame2.Tag = "";
        // 
        // txtEstatus
        // 
        this.txtEstatus.AcceptsReturn = true;
        this.txtEstatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
        this.txtEstatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.txtEstatus.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtEstatus.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtEstatus.ForeColor = System.Drawing.Color.Maroon;
        this.txtEstatus.Location = new System.Drawing.Point(128, 16);
        this.txtEstatus.MaxLength = 35;
        this.txtEstatus.Name = "txtEstatus";
        this.txtEstatus.ReadOnly = true;
        this.txtEstatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtEstatus.Size = new System.Drawing.Size(217, 20);
        this.txtEstatus.TabIndex = 2;
        this.txtEstatus.TabStop = false;
        this.txtEstatus.Tag = "";
        // 
        // mskFecEstatus
        // 
        this.mskFecEstatus.AllowPromptAsInput = false;
        this.mskFecEstatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
        this.mskFecEstatus.Enabled = false;
        this.mskFecEstatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.mskFecEstatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
        this.mskFecEstatus.Location = new System.Drawing.Point(264, 48);
        this.mskFecEstatus.Mask = "00/00/0000";
        this.mskFecEstatus.Name = "mskFecEstatus";
        this.mskFecEstatus.ResetOnSpace = false;
        this.mskFecEstatus.Size = new System.Drawing.Size(81, 20);
        this.mskFecEstatus.TabIndex = 3;
        this.mskFecEstatus.TabStop = false;
        this.mskFecEstatus.Tag = "";
        // 
        // _Label2_2
        // 
        this._Label2_2.BackColor = System.Drawing.SystemColors.Control;
        this._Label2_2.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label2_2.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label2_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label2_2.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label2_2.Location = new System.Drawing.Point(8, 48);
        this._Label2_2.Name = "_Label2_2";
        this._Label2_2.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label2_2.Size = new System.Drawing.Size(105, 17);
        this._Label2_2.TabIndex = 20;
        this._Label2_2.Tag = "";
        this._Label2_2.Text = "Fecha de Estatus:";
        // 
        // _Label1_1
        // 
        this._Label1_1.BackColor = System.Drawing.SystemColors.Control;
        this._Label1_1.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label1_1.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label1_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label1_1.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label1_1.Location = new System.Drawing.Point(8, 16);
        this._Label1_1.Name = "_Label1_1";
        this._Label1_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label1_1.Size = new System.Drawing.Size(81, 17);
        this._Label1_1.TabIndex = 19;
        this._Label1_1.Tag = "";
        this._Label1_1.Text = "Estatus:";
        // 
        // Frame3
        // 
        this.Frame3.BackColor = System.Drawing.SystemColors.Control;
        this.Frame3.Controls.Add(this.cmdProcesar);
        this.Frame3.Controls.Add(this.cmdSalir);
        this.Frame3.Controls.Add(this.cmdLimpiar);
        this.Frame3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Frame3.ForeColor = System.Drawing.SystemColors.ControlText;
        this.Frame3.Location = new System.Drawing.Point(0, 281);
        this.Frame3.Name = "Frame3";
        this.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.Frame3.Size = new System.Drawing.Size(361, 73);
        this.Frame3.TabIndex = 16;
        this.Frame3.TabStop = false;
        this.Frame3.Tag = "";
        this.Frame3.DoubleClick += new System.EventHandler(this.Frame3_DoubleClick);
        // 
        // fraInfoArchivo
        // 
        this.fraInfoArchivo.BackColor = System.Drawing.SystemColors.Control;
        this.fraInfoArchivo.Controls.Add(this.txtFolFinal);
        this.fraInfoArchivo.Controls.Add(this.txtFolInicial);
        this.fraInfoArchivo.Controls.Add(this.txtArchivo);
        this.fraInfoArchivo.Controls.Add(this.mskFecProceso);
        this.fraInfoArchivo.Controls.Add(this._Label2_1);
        this.fraInfoArchivo.Controls.Add(this._Label2_4);
        this.fraInfoArchivo.Controls.Add(this._Label2_3);
        this.fraInfoArchivo.Controls.Add(this._Label2_0);
        this.fraInfoArchivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.fraInfoArchivo.ForeColor = System.Drawing.Color.Blue;
        this.fraInfoArchivo.Location = new System.Drawing.Point(0, 127);
        this.fraInfoArchivo.Name = "fraInfoArchivo";
        this.fraInfoArchivo.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.fraInfoArchivo.Size = new System.Drawing.Size(361, 153);
        this.fraInfoArchivo.TabIndex = 12;
        this.fraInfoArchivo.TabStop = false;
        this.fraInfoArchivo.Tag = "";
        this.fraInfoArchivo.Text = "Información de la Remesa ";
        // 
        // txtFolFinal
        // 
        this.txtFolFinal.AcceptsReturn = true;
        this.txtFolFinal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
        this.txtFolFinal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.txtFolFinal.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtFolFinal.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtFolFinal.ForeColor = System.Drawing.Color.Maroon;
        this.txtFolFinal.Location = new System.Drawing.Point(126, 93);
        this.txtFolFinal.MaxLength = 35;
        this.txtFolFinal.Name = "txtFolFinal";
        this.txtFolFinal.ReadOnly = true;
        this.txtFolFinal.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtFolFinal.Size = new System.Drawing.Size(217, 20);
        this.txtFolFinal.TabIndex = 6;
        this.txtFolFinal.TabStop = false;
        this.txtFolFinal.Tag = "";
        this.txtFolFinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        // 
        // txtFolInicial
        // 
        this.txtFolInicial.AcceptsReturn = true;
        this.txtFolInicial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
        this.txtFolInicial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.txtFolInicial.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtFolInicial.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtFolInicial.ForeColor = System.Drawing.Color.Maroon;
        this.txtFolInicial.Location = new System.Drawing.Point(126, 60);
        this.txtFolInicial.MaxLength = 35;
        this.txtFolInicial.Name = "txtFolInicial";
        this.txtFolInicial.ReadOnly = true;
        this.txtFolInicial.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtFolInicial.Size = new System.Drawing.Size(217, 20);
        this.txtFolInicial.TabIndex = 5;
        this.txtFolInicial.TabStop = false;
        this.txtFolInicial.Tag = "";
        this.txtFolInicial.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        // 
        // txtArchivo
        // 
        this.txtArchivo.AcceptsReturn = true;
        this.txtArchivo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
        this.txtArchivo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.txtArchivo.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtArchivo.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtArchivo.ForeColor = System.Drawing.Color.Maroon;
        this.txtArchivo.Location = new System.Drawing.Point(126, 33);
        this.txtArchivo.MaxLength = 35;
        this.txtArchivo.Name = "txtArchivo";
        this.txtArchivo.ReadOnly = true;
        this.txtArchivo.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtArchivo.Size = new System.Drawing.Size(217, 20);
        this.txtArchivo.TabIndex = 4;
        this.txtArchivo.TabStop = false;
        this.txtArchivo.Tag = "";
        // 
        // mskFecProceso
        // 
        this.mskFecProceso.AllowPromptAsInput = false;
        this.mskFecProceso.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
        this.mskFecProceso.Enabled = false;
        this.mskFecProceso.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.mskFecProceso.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
        this.mskFecProceso.Location = new System.Drawing.Point(126, 120);
        this.mskFecProceso.Mask = "00/00/0000";
        this.mskFecProceso.Name = "mskFecProceso";
        this.mskFecProceso.ResetOnSpace = false;
        this.mskFecProceso.Size = new System.Drawing.Size(81, 20);
        this.mskFecProceso.TabIndex = 7;
        this.mskFecProceso.TabStop = false;
        this.mskFecProceso.Tag = "";
        // 
        // _Label2_1
        // 
        this._Label2_1.BackColor = System.Drawing.SystemColors.Control;
        this._Label2_1.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label2_1.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label2_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label2_1.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label2_1.Location = new System.Drawing.Point(8, 120);
        this._Label2_1.Name = "_Label2_1";
        this._Label2_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label2_1.Size = new System.Drawing.Size(129, 17);
        this._Label2_1.TabIndex = 17;
        this._Label2_1.Tag = "";
        this._Label2_1.Text = "Fecha de Proceso:";
        // 
        // _Label2_4
        // 
        this._Label2_4.BackColor = System.Drawing.SystemColors.Control;
        this._Label2_4.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label2_4.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label2_4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label2_4.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label2_4.Location = new System.Drawing.Point(8, 88);
        this._Label2_4.Name = "_Label2_4";
        this._Label2_4.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label2_4.Size = new System.Drawing.Size(73, 17);
        this._Label2_4.TabIndex = 15;
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
        this._Label2_3.Location = new System.Drawing.Point(8, 56);
        this._Label2_3.Name = "_Label2_3";
        this._Label2_3.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label2_3.Size = new System.Drawing.Size(73, 17);
        this._Label2_3.TabIndex = 14;
        this._Label2_3.Tag = "";
        this._Label2_3.Text = "Folio Inicial:";
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
        this._Label2_0.TabIndex = 13;
        this._Label2_0.Tag = "";
        this._Label2_0.Text = "Archivo:";
        // 
        // Frame1
        // 
        this.Frame1.BackColor = System.Drawing.SystemColors.Control;
        this.Frame1.Controls.Add(this.txtClaveRemesa);
        this.Frame1.Controls.Add(this._Label1_0);
        this.Frame1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Frame1.ForeColor = System.Drawing.SystemColors.ControlText;
        this.Frame1.Location = new System.Drawing.Point(0, 2);
        this.Frame1.Name = "Frame1";
        this.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.Frame1.Size = new System.Drawing.Size(361, 49);
        this.Frame1.TabIndex = 0;
        this.Frame1.TabStop = false;
        this.Frame1.Tag = "";
        // 
        // txtClaveRemesa
        // 
        this.txtClaveRemesa.AcceptsReturn = true;
        this.txtClaveRemesa.BackColor = System.Drawing.SystemColors.Window;
        this.txtClaveRemesa.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtClaveRemesa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtClaveRemesa.ForeColor = System.Drawing.SystemColors.WindowText;
        this.txtClaveRemesa.Location = new System.Drawing.Point(162, 16);
        this.txtClaveRemesa.MaxLength = 18;
        this.txtClaveRemesa.Name = "txtClaveRemesa";
        this.txtClaveRemesa.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtClaveRemesa.Size = new System.Drawing.Size(185, 20);
        this.txtClaveRemesa.TabIndex = 1;
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
        this._Label1_0.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label1_0.Location = new System.Drawing.Point(16, 16);
        this._Label1_0.Name = "_Label1_0";
        this._Label1_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label1_0.Size = new System.Drawing.Size(145, 17);
        this._Label1_0.TabIndex = 11;
        this._Label1_0.Tag = "";
        this._Label1_0.Text = "Clave de la Remesa:";
        // 
        // frmConsRemesa
        // 
        this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
        this.AutoScroll = true;
        this.BackColor = System.Drawing.SystemColors.Control;
        this.ClientSize = new System.Drawing.Size(369, 365);
        this.ControlBox = false;
        this.Controls.Add(this.Frame2);
        this.Controls.Add(this.Frame3);
        this.Controls.Add(this.fraInfoArchivo);
        this.Controls.Add(this.MainMenu1);
        this.Controls.Add(this.Frame1);
        this.Cursor = System.Windows.Forms.Cursors.Default;
        this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.KeyPreview = true;
        this.Location = new System.Drawing.Point(137, 184);
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.formHelper1.SetMoveable(this, false);
        this.Name = "frmConsRemesa";
        this.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
        this.Tag = "";
        this.Text = "Consulta de Estatus de la Remesa";
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmConsRemesa_FormClosing);
        this.MainMenu1.ResumeLayout(false);
        this.MainMenu1.PerformLayout();
        this.Frame2.ResumeLayout(false);
        this.Frame2.PerformLayout();
        this.Frame3.ResumeLayout(false);
        this.fraInfoArchivo.ResumeLayout(false);
        this.fraInfoArchivo.PerformLayout();
        this.Frame1.ResumeLayout(false);
        this.Frame1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.commandButtonHelper1)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

	}
	void  InitializeLabel1()
	{
			this.Label1[1] = _Label1_1;
			this.Label1[0] = _Label1_0;
	}
	void  InitializeLabel2()
	{
			this.Label2[2] = _Label2_2;
			this.Label2[1] = _Label2_1;
			this.Label2[4] = _Label2_4;
			this.Label2[3] = _Label2_3;
			this.Label2[0] = _Label2_0;
	}
#endregion 

        private System.Windows.Forms.ToolStripMenuItem dummy1;
        private System.Windows.Forms.ToolStripMenuItem dummy2;
        private Artinsoft.VB6.Gui.FormHelper formHelper1;
}
}