using System; 
using VB6 = Microsoft.VisualBasic.Compatibility.VB6.Support; 

namespace Masivos
{
	partial class frmRegRemesas
	{
	
#region "Upgrade Support "
        public static frmRegRemesas m_vb6FormDefInstance;
		private static bool m_InitializingDefInstance;
		public static frmRegRemesas DefInstance
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
		public frmRegRemesas():base(){
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
			//This form is an MDI child.
			//This code simulates the VB6 
			// functionality of automatically
			// loading and showing an MDI
			// child's parent.
			this.MdiParent = Masivos.MDIMasivos.DefInstance;
			Masivos.MDIMasivos.DefInstance.Show();
		}
	public static frmRegRemesas CreateInstance()
	{
			frmRegRemesas theInstance = new frmRegRemesas();
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
	public  System.Windows.Forms.ToolStripMenuItem mnuAceptar;
	public  System.Windows.Forms.ToolStripMenuItem mnuDeclinar;
	public  System.Windows.Forms.ToolStripMenuItem mnuLimpiar;
	public  System.Windows.Forms.ToolStripSeparator mnuGuion1;
	public  System.Windows.Forms.ToolStripMenuItem mnuRegLinea;
	public  System.Windows.Forms.ToolStripSeparator mnuGuion2;
	public  System.Windows.Forms.ToolStripMenuItem mnuSalir;
	public  System.Windows.Forms.ToolStripMenuItem mnuRegRemesas;
	public  System.Windows.Forms.MenuStrip MainMenu1;
	public  System.Windows.Forms.Button cmdLimpiar;
	public  System.Windows.Forms.Button cmdSalir;
	public  System.Windows.Forms.Button cmdAceptar;
	public  System.Windows.Forms.Button cmdDeclinar;
	public  System.Windows.Forms.GroupBox Frame2;
	public  System.Windows.Forms.TextBox txtTotalSolEnviadas;
	public  System.Windows.Forms.ComboBox cboPromocion;
	public  System.Windows.Forms.ComboBox cboTipoSolicitud;
	public  System.Windows.Forms.ComboBox cboFamiliaProducto;
	public  System.Windows.Forms.ComboBox cboEntidadOrigen;
	public  System.Windows.Forms.ComboBox cboTipoEntOrig;
	public  System.Windows.Forms.ComboBox cboTipoTram;
	public  System.Windows.Forms.TextBox txtCveRemesa;
	public  System.Windows.Forms.MaskedTextBox mskFecProceso;
	public  System.Windows.Forms.MaskedTextBox mskFecIngresoCred;
	public  System.Windows.Forms.MaskedTextBox mskFecAceptaCred;
	private  System.Windows.Forms.Label _Label1_13;
	private  System.Windows.Forms.Label _Label1_12;
	private  System.Windows.Forms.Label _Label1_11;
	private  System.Windows.Forms.Label _Label1_10;
	private  System.Windows.Forms.Label _Label1_9;
	private  System.Windows.Forms.Label _Label1_8;
	private  System.Windows.Forms.Label _Label1_7;
	private  System.Windows.Forms.Label _Label1_6;
	private  System.Windows.Forms.Label _Label1_5;
	private  System.Windows.Forms.Label _Label1_4;
	private  System.Windows.Forms.Label _Label1_3;
	private  System.Windows.Forms.Label _Label1_2;
	private  System.Windows.Forms.Label _Label1_1;
	private  System.Windows.Forms.Label _Label1_0;
	public  System.Windows.Forms.GroupBox Frame1;
	public System.Windows.Forms.Label[] Label1 = new System.Windows.Forms.Label[14];
	private Artinsoft.VB6.Gui.ListControlHelper listBoxComboBoxHelper1;
	private Artinsoft.VB6.Gui.CommandButtonHelper commandButtonHelper1;
	//NOTE: The following procedure is required by the Windows Form Designer
	//It can be modified using the Windows Form Designer.
	//Do not modify it using the code editor.
	[System.Diagnostics.DebuggerStepThrough]
	 private void  InitializeComponent()
	{
        this.components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegRemesas));
        this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
        this.cmdLimpiar = new System.Windows.Forms.Button();
        this.cmdSalir = new System.Windows.Forms.Button();
        this.cmdAceptar = new System.Windows.Forms.Button();
        this.cmdDeclinar = new System.Windows.Forms.Button();
        this.MainMenu1 = new System.Windows.Forms.MenuStrip();
        this.mnuRegRemesas = new System.Windows.Forms.ToolStripMenuItem();
        this.mnuAceptar = new System.Windows.Forms.ToolStripMenuItem();
        this.mnuDeclinar = new System.Windows.Forms.ToolStripMenuItem();
        this.mnuLimpiar = new System.Windows.Forms.ToolStripMenuItem();
        this.mnuGuion1 = new System.Windows.Forms.ToolStripSeparator();
        this.mnuRegLinea = new System.Windows.Forms.ToolStripMenuItem();
        this.mnuGuion2 = new System.Windows.Forms.ToolStripSeparator();
        this.mnuSalir = new System.Windows.Forms.ToolStripMenuItem();
        this.procesamientoMasivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.predictaminaciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.firmaAlS041ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.Frame2 = new System.Windows.Forms.GroupBox();
        this.Frame1 = new System.Windows.Forms.GroupBox();
        this.txtTotalSolEnviadas = new System.Windows.Forms.TextBox();
        this.cboPromocion = new System.Windows.Forms.ComboBox();
        this.cboTipoSolicitud = new System.Windows.Forms.ComboBox();
        this.cboFamiliaProducto = new System.Windows.Forms.ComboBox();
        this.cboEntidadOrigen = new System.Windows.Forms.ComboBox();
        this.cboTipoEntOrig = new System.Windows.Forms.ComboBox();
        this.cboTipoTram = new System.Windows.Forms.ComboBox();
        this.txtCveRemesa = new System.Windows.Forms.TextBox();
        this.mskFecProceso = new System.Windows.Forms.MaskedTextBox();
        this.mskFecIngresoCred = new System.Windows.Forms.MaskedTextBox();
        this.mskFecAceptaCred = new System.Windows.Forms.MaskedTextBox();
        this._Label1_13 = new System.Windows.Forms.Label();
        this._Label1_12 = new System.Windows.Forms.Label();
        this._Label1_11 = new System.Windows.Forms.Label();
        this._Label1_10 = new System.Windows.Forms.Label();
        this._Label1_9 = new System.Windows.Forms.Label();
        this._Label1_8 = new System.Windows.Forms.Label();
        this._Label1_7 = new System.Windows.Forms.Label();
        this._Label1_6 = new System.Windows.Forms.Label();
        this._Label1_5 = new System.Windows.Forms.Label();
        this._Label1_4 = new System.Windows.Forms.Label();
        this._Label1_3 = new System.Windows.Forms.Label();
        this._Label1_2 = new System.Windows.Forms.Label();
        this._Label1_1 = new System.Windows.Forms.Label();
        this._Label1_0 = new System.Windows.Forms.Label();
        this.listBoxComboBoxHelper1 = new Artinsoft.VB6.Gui.ListControlHelper(this.components);
        this.commandButtonHelper1 = new Artinsoft.VB6.Gui.CommandButtonHelper(this.components);
        this.formHelper1 = new Artinsoft.VB6.Gui.FormHelper(this.components);
        this.MainMenu1.SuspendLayout();
        this.Frame2.SuspendLayout();
        this.Frame1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.listBoxComboBoxHelper1)).BeginInit();
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
        this.cmdLimpiar.Location = new System.Drawing.Point(216, 18);
        this.commandButtonHelper1.SetMaskColor(this.cmdLimpiar, System.Drawing.Color.Silver);
        this.cmdLimpiar.Name = "cmdLimpiar";
        this.cmdLimpiar.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.cmdLimpiar.Size = new System.Drawing.Size(59, 49);
        this.commandButtonHelper1.SetStyle(this.cmdLimpiar, 1);
        this.cmdLimpiar.TabIndex = 13;
        this.cmdLimpiar.Tag = "";
        this.cmdLimpiar.Text = "Limpiar";
        this.cmdLimpiar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
        this.cmdLimpiar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
        this.ToolTip1.SetToolTip(this.cmdLimpiar, "Limpiar datos capturables");
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
        this.cmdSalir.Location = new System.Drawing.Point(288, 19);
        this.commandButtonHelper1.SetMaskColor(this.cmdSalir, System.Drawing.Color.Silver);
        this.cmdSalir.Name = "cmdSalir";
        this.cmdSalir.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.cmdSalir.Size = new System.Drawing.Size(59, 49);
        this.commandButtonHelper1.SetStyle(this.cmdSalir, 1);
        this.cmdSalir.TabIndex = 14;
        this.cmdSalir.Tag = "";
        this.cmdSalir.Text = "Salir";
        this.cmdSalir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
        this.cmdSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
        this.ToolTip1.SetToolTip(this.cmdSalir, "Salir de la pantalla");
        this.cmdSalir.UseVisualStyleBackColor = false;
        this.cmdSalir.Click += new System.EventHandler(this.cmdSalir_Click);
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
        this.cmdAceptar.Location = new System.Drawing.Point(144, 18);
        this.commandButtonHelper1.SetMaskColor(this.cmdAceptar, System.Drawing.Color.Silver);
        this.cmdAceptar.Name = "cmdAceptar";
        this.cmdAceptar.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.cmdAceptar.Size = new System.Drawing.Size(59, 49);
        this.commandButtonHelper1.SetStyle(this.cmdAceptar, 1);
        this.cmdAceptar.TabIndex = 12;
        this.cmdAceptar.Tag = "";
        this.cmdAceptar.Text = "Aceptar";
        this.cmdAceptar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
        this.cmdAceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
        this.ToolTip1.SetToolTip(this.cmdAceptar, "Procesamiento de la remesa");
        this.cmdAceptar.UseVisualStyleBackColor = false;
        this.cmdAceptar.Click += new System.EventHandler(this.cmdAceptar_Click);
        // 
        // cmdDeclinar
        // 
        this.cmdDeclinar.BackColor = System.Drawing.SystemColors.Control;
        this.commandButtonHelper1.SetCorrectEventsBehavior(this.cmdDeclinar, true);
        this.cmdDeclinar.Cursor = System.Windows.Forms.Cursors.Default;
        this.commandButtonHelper1.SetDisabledPicture(this.cmdDeclinar, null);
        this.commandButtonHelper1.SetDownPicture(this.cmdDeclinar, null);
        this.cmdDeclinar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.cmdDeclinar.ForeColor = System.Drawing.SystemColors.ControlText;
        this.cmdDeclinar.Image = ((System.Drawing.Image)(resources.GetObject("cmdDeclinar.Image")));
        this.cmdDeclinar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
        this.cmdDeclinar.Location = new System.Drawing.Point(72, 18);
        this.commandButtonHelper1.SetMaskColor(this.cmdDeclinar, System.Drawing.Color.Silver);
        this.cmdDeclinar.Name = "cmdDeclinar";
        this.cmdDeclinar.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.cmdDeclinar.Size = new System.Drawing.Size(59, 49);
        this.commandButtonHelper1.SetStyle(this.cmdDeclinar, 1);
        this.cmdDeclinar.TabIndex = 11;
        this.cmdDeclinar.Tag = "";
        this.cmdDeclinar.Text = "Declinar";
        this.cmdDeclinar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
        this.cmdDeclinar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
        this.ToolTip1.SetToolTip(this.cmdDeclinar, "Declinación de la remesa");
        this.cmdDeclinar.UseVisualStyleBackColor = false;
        this.cmdDeclinar.Click += new System.EventHandler(this.cmdDeclinar_Click);
        // 
        // MainMenu1
        // 
        this.MainMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRegRemesas,
            this.procesamientoMasivoToolStripMenuItem,
            this.predictaminaciónToolStripMenuItem,
            this.firmaAlS041ToolStripMenuItem});
        this.MainMenu1.Location = new System.Drawing.Point(0, 0);
        this.MainMenu1.Name = "MainMenu1";
        this.MainMenu1.Size = new System.Drawing.Size(484, 24);
        this.MainMenu1.TabIndex = 31;
        this.MainMenu1.Visible = false;
        // 
        // mnuRegRemesas
        // 
        this.mnuRegRemesas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAceptar,
            this.mnuDeclinar,
            this.mnuLimpiar,
            this.mnuGuion1,
            this.mnuRegLinea,
            this.mnuGuion2,
            this.mnuSalir});
        this.mnuRegRemesas.Name = "mnuRegRemesas";
        this.mnuRegRemesas.Size = new System.Drawing.Size(120, 20);
        this.mnuRegRemesas.Tag = "";
        this.mnuRegRemesas.Text = "Registro de Remesas";
        // 
        // mnuAceptar
        // 
        this.mnuAceptar.Name = "mnuAceptar";
        this.mnuAceptar.Size = new System.Drawing.Size(172, 22);
        this.mnuAceptar.Tag = "";
        this.mnuAceptar.Text = "&Aceptar";
        this.mnuAceptar.Click += new System.EventHandler(this.mnuAceptar_Click);
        // 
        // mnuDeclinar
        // 
        this.mnuDeclinar.Name = "mnuDeclinar";
        this.mnuDeclinar.Size = new System.Drawing.Size(172, 22);
        this.mnuDeclinar.Tag = "";
        this.mnuDeclinar.Text = "&Declinar";
        this.mnuDeclinar.Click += new System.EventHandler(this.mnuDeclinar_Click);
        // 
        // mnuLimpiar
        // 
        this.mnuLimpiar.Name = "mnuLimpiar";
        this.mnuLimpiar.Size = new System.Drawing.Size(172, 22);
        this.mnuLimpiar.Tag = "";
        this.mnuLimpiar.Text = "&Limpiar Pantalla";
        this.mnuLimpiar.Click += new System.EventHandler(this.mnuLimpiar_Click);
        // 
        // mnuGuion1
        // 
        this.mnuGuion1.Name = "mnuGuion1";
        this.mnuGuion1.Size = new System.Drawing.Size(169, 6);
        this.mnuGuion1.Tag = "";
        // 
        // mnuRegLinea
        // 
        this.mnuRegLinea.Name = "mnuRegLinea";
        this.mnuRegLinea.Size = new System.Drawing.Size(172, 22);
        this.mnuRegLinea.Tag = "";
        this.mnuRegLinea.Text = "&Registrar en Línea";
        this.mnuRegLinea.Click += new System.EventHandler(this.mnuRegLinea_Click);
        // 
        // mnuGuion2
        // 
        this.mnuGuion2.Name = "mnuGuion2";
        this.mnuGuion2.Size = new System.Drawing.Size(169, 6);
        this.mnuGuion2.Tag = "";
        // 
        // mnuSalir
        // 
        this.mnuSalir.Name = "mnuSalir";
        this.mnuSalir.Size = new System.Drawing.Size(172, 22);
        this.mnuSalir.Tag = "";
        this.mnuSalir.Text = "&Salir";
        this.mnuSalir.Click += new System.EventHandler(this.mnuSalir_Click);
        // 
        // procesamientoMasivoToolStripMenuItem
        // 
        this.procesamientoMasivoToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Remove;
        this.procesamientoMasivoToolStripMenuItem.Name = "procesamientoMasivoToolStripMenuItem";
        this.procesamientoMasivoToolStripMenuItem.Size = new System.Drawing.Size(125, 20);
        this.procesamientoMasivoToolStripMenuItem.Text = "&Procesamiento Masivo";
        // 
        // predictaminaciónToolStripMenuItem
        // 
        this.predictaminaciónToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Remove;
        this.predictaminaciónToolStripMenuItem.Name = "predictaminaciónToolStripMenuItem";
        this.predictaminaciónToolStripMenuItem.Size = new System.Drawing.Size(99, 20);
        this.predictaminaciónToolStripMenuItem.Text = "Predictaminación";
        // 
        // firmaAlS041ToolStripMenuItem
        // 
        this.firmaAlS041ToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Remove;
        this.firmaAlS041ToolStripMenuItem.Name = "firmaAlS041ToolStripMenuItem";
        this.firmaAlS041ToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
        this.firmaAlS041ToolStripMenuItem.Text = "Firma al S041";
        // 
        // Frame2
        // 
        this.Frame2.BackColor = System.Drawing.SystemColors.Control;
        this.Frame2.Controls.Add(this.cmdLimpiar);
        this.Frame2.Controls.Add(this.cmdSalir);
        this.Frame2.Controls.Add(this.cmdAceptar);
        this.Frame2.Controls.Add(this.cmdDeclinar);
        this.Frame2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Frame2.ForeColor = System.Drawing.SystemColors.ControlText;
        this.Frame2.Location = new System.Drawing.Point(6, 388);
        this.Frame2.Name = "Frame2";
        this.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.Frame2.Size = new System.Drawing.Size(467, 73);
        this.Frame2.TabIndex = 30;
        this.Frame2.TabStop = false;
        this.Frame2.Tag = "";
        // 
        // Frame1
        // 
        this.Frame1.BackColor = System.Drawing.SystemColors.Control;
        this.Frame1.Controls.Add(this.txtTotalSolEnviadas);
        this.Frame1.Controls.Add(this.cboPromocion);
        this.Frame1.Controls.Add(this.cboTipoSolicitud);
        this.Frame1.Controls.Add(this.cboFamiliaProducto);
        this.Frame1.Controls.Add(this.cboEntidadOrigen);
        this.Frame1.Controls.Add(this.cboTipoEntOrig);
        this.Frame1.Controls.Add(this.cboTipoTram);
        this.Frame1.Controls.Add(this.txtCveRemesa);
        this.Frame1.Controls.Add(this.mskFecProceso);
        this.Frame1.Controls.Add(this.mskFecIngresoCred);
        this.Frame1.Controls.Add(this.mskFecAceptaCred);
        this.Frame1.Controls.Add(this._Label1_13);
        this.Frame1.Controls.Add(this._Label1_12);
        this.Frame1.Controls.Add(this._Label1_11);
        this.Frame1.Controls.Add(this._Label1_10);
        this.Frame1.Controls.Add(this._Label1_9);
        this.Frame1.Controls.Add(this._Label1_8);
        this.Frame1.Controls.Add(this._Label1_7);
        this.Frame1.Controls.Add(this._Label1_6);
        this.Frame1.Controls.Add(this._Label1_5);
        this.Frame1.Controls.Add(this._Label1_4);
        this.Frame1.Controls.Add(this._Label1_3);
        this.Frame1.Controls.Add(this._Label1_2);
        this.Frame1.Controls.Add(this._Label1_1);
        this.Frame1.Controls.Add(this._Label1_0);
        this.Frame1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Frame1.ForeColor = System.Drawing.Color.Blue;
        this.Frame1.Location = new System.Drawing.Point(5, 12);
        this.Frame1.Name = "Frame1";
        this.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.Frame1.Size = new System.Drawing.Size(467, 377);
        this.Frame1.TabIndex = 15;
        this.Frame1.TabStop = false;
        this.Frame1.Tag = "";
        this.Frame1.Text = "Datos Generales ";
        // 
        // txtTotalSolEnviadas
        // 
        this.txtTotalSolEnviadas.AcceptsReturn = true;
        this.txtTotalSolEnviadas.BackColor = System.Drawing.SystemColors.Window;
        this.txtTotalSolEnviadas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.txtTotalSolEnviadas.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtTotalSolEnviadas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtTotalSolEnviadas.ForeColor = System.Drawing.SystemColors.WindowText;
        this.txtTotalSolEnviadas.Location = new System.Drawing.Point(192, 330);
        this.txtTotalSolEnviadas.MaxLength = 5;
        this.txtTotalSolEnviadas.Name = "txtTotalSolEnviadas";
        this.txtTotalSolEnviadas.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtTotalSolEnviadas.Size = new System.Drawing.Size(65, 20);
        this.txtTotalSolEnviadas.TabIndex = 10;
        this.txtTotalSolEnviadas.Tag = "";
        this.txtTotalSolEnviadas.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        // 
        // cboPromocion
        // 
        this.cboPromocion.BackColor = System.Drawing.SystemColors.Window;
        this.cboPromocion.Cursor = System.Windows.Forms.Cursors.Default;
        this.cboPromocion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cboPromocion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.cboPromocion.ForeColor = System.Drawing.Color.Blue;
        this.listBoxComboBoxHelper1.SetItemData(this.cboPromocion, new int[0]);
        this.cboPromocion.Location = new System.Drawing.Point(192, 216);
        this.cboPromocion.Name = "cboPromocion";
        this.cboPromocion.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.cboPromocion.Size = new System.Drawing.Size(256, 21);
        this.cboPromocion.TabIndex = 6;
        this.cboPromocion.Tag = "";
        this.cboPromocion.SelectedIndexChanged += new System.EventHandler(this.cboPromocion_SelectedIndexChanged);
        this.cboPromocion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboPromocion_KeyPress);
        this.cboPromocion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboPromocion_KeyDown);
        // 
        // cboTipoSolicitud
        // 
        this.cboTipoSolicitud.BackColor = System.Drawing.SystemColors.Window;
        this.cboTipoSolicitud.Cursor = System.Windows.Forms.Cursors.Default;
        this.cboTipoSolicitud.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.cboTipoSolicitud.ForeColor = System.Drawing.Color.Blue;
        this.listBoxComboBoxHelper1.SetItemData(this.cboTipoSolicitud, new int[0]);
        this.cboTipoSolicitud.Location = new System.Drawing.Point(192, 184);
        this.cboTipoSolicitud.Name = "cboTipoSolicitud";
        this.cboTipoSolicitud.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.cboTipoSolicitud.Size = new System.Drawing.Size(256, 21);
        this.cboTipoSolicitud.TabIndex = 5;
        this.cboTipoSolicitud.Tag = "";
        this.cboTipoSolicitud.Text = "cboTipoSolicitud";
        this.cboTipoSolicitud.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboTipoSolicitud_KeyPress);
        // 
        // cboFamiliaProducto
        // 
        this.cboFamiliaProducto.BackColor = System.Drawing.SystemColors.Window;
        this.cboFamiliaProducto.Cursor = System.Windows.Forms.Cursors.Default;
        this.cboFamiliaProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.cboFamiliaProducto.ForeColor = System.Drawing.Color.Blue;
        this.listBoxComboBoxHelper1.SetItemData(this.cboFamiliaProducto, new int[0]);
        this.cboFamiliaProducto.Location = new System.Drawing.Point(192, 150);
        this.cboFamiliaProducto.Name = "cboFamiliaProducto";
        this.cboFamiliaProducto.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.cboFamiliaProducto.Size = new System.Drawing.Size(256, 21);
        this.cboFamiliaProducto.TabIndex = 4;
        this.cboFamiliaProducto.Tag = "";
        this.cboFamiliaProducto.Text = "cboFamiliaProducto";
        this.cboFamiliaProducto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboFamiliaProducto_KeyPress);
        // 
        // cboEntidadOrigen
        // 
        this.cboEntidadOrigen.BackColor = System.Drawing.SystemColors.Window;
        this.cboEntidadOrigen.Cursor = System.Windows.Forms.Cursors.Default;
        this.cboEntidadOrigen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cboEntidadOrigen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.cboEntidadOrigen.ForeColor = System.Drawing.Color.Blue;
        this.listBoxComboBoxHelper1.SetItemData(this.cboEntidadOrigen, new int[0]);
        this.cboEntidadOrigen.Location = new System.Drawing.Point(192, 120);
        this.cboEntidadOrigen.Name = "cboEntidadOrigen";
        this.cboEntidadOrigen.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.cboEntidadOrigen.Size = new System.Drawing.Size(256, 21);
        this.cboEntidadOrigen.TabIndex = 3;
        this.cboEntidadOrigen.Tag = "";
        this.cboEntidadOrigen.Leave += new System.EventHandler(this.cboEntidadOrigen_Leave);
        this.cboEntidadOrigen.Enter += new System.EventHandler(this.cboEntidadOrigen_Enter);
        this.cboEntidadOrigen.SelectedIndexChanged += new System.EventHandler(this.cboEntidadOrigen_SelectedIndexChanged);
        this.cboEntidadOrigen.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboEntidadOrigen_KeyPress);
        this.cboEntidadOrigen.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboEntidadOrigen_KeyDown);
        // 
        // cboTipoEntOrig
        // 
        this.cboTipoEntOrig.BackColor = System.Drawing.SystemColors.Window;
        this.cboTipoEntOrig.Cursor = System.Windows.Forms.Cursors.Default;
        this.cboTipoEntOrig.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cboTipoEntOrig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.cboTipoEntOrig.ForeColor = System.Drawing.Color.Blue;
        this.listBoxComboBoxHelper1.SetItemData(this.cboTipoEntOrig, new int[0]);
        this.cboTipoEntOrig.Location = new System.Drawing.Point(192, 90);
        this.cboTipoEntOrig.Name = "cboTipoEntOrig";
        this.cboTipoEntOrig.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.cboTipoEntOrig.Size = new System.Drawing.Size(256, 21);
        this.cboTipoEntOrig.TabIndex = 2;
        this.cboTipoEntOrig.Tag = "";
        this.cboTipoEntOrig.Leave += new System.EventHandler(this.cboTipoEntOrig_Leave);
        this.cboTipoEntOrig.SelectedIndexChanged += new System.EventHandler(this.cboTipoEntOrig_SelectedIndexChanged);
        this.cboTipoEntOrig.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboTipoEntOrig_KeyPress);
        this.cboTipoEntOrig.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboTipoEntOrig_KeyDown);
        // 
        // cboTipoTram
        // 
        this.cboTipoTram.BackColor = System.Drawing.SystemColors.Window;
        this.cboTipoTram.Cursor = System.Windows.Forms.Cursors.Default;
        this.cboTipoTram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cboTipoTram.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.cboTipoTram.ForeColor = System.Drawing.Color.Blue;
        this.listBoxComboBoxHelper1.SetItemData(this.cboTipoTram, new int[] {
            0});
        this.cboTipoTram.Items.AddRange(new object[] {
            "01 CUENTAS NUEVAS"});
        this.cboTipoTram.Location = new System.Drawing.Point(192, 56);
        this.cboTipoTram.Name = "cboTipoTram";
        this.cboTipoTram.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.cboTipoTram.Size = new System.Drawing.Size(256, 21);
        this.cboTipoTram.TabIndex = 1;
        this.cboTipoTram.Tag = "";
        this.cboTipoTram.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboTipoTram_KeyPress);
        // 
        // txtCveRemesa
        // 
        this.txtCveRemesa.AcceptsReturn = true;
        this.txtCveRemesa.BackColor = System.Drawing.SystemColors.Window;
        this.txtCveRemesa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.txtCveRemesa.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtCveRemesa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtCveRemesa.ForeColor = System.Drawing.SystemColors.WindowText;
        this.txtCveRemesa.Location = new System.Drawing.Point(192, 27);
        this.txtCveRemesa.MaxLength = 18;
        this.txtCveRemesa.Name = "txtCveRemesa";
        this.txtCveRemesa.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtCveRemesa.Size = new System.Drawing.Size(256, 20);
        this.txtCveRemesa.TabIndex = 0;
        this.txtCveRemesa.Tag = "";
        this.txtCveRemesa.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        // 
        // mskFecProceso
        // 
        this.mskFecProceso.AllowPromptAsInput = false;
        this.mskFecProceso.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.mskFecProceso.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
        this.mskFecProceso.Location = new System.Drawing.Point(192, 248);
        this.mskFecProceso.Mask = "00/00/0000";
        this.mskFecProceso.Name = "mskFecProceso";
        this.mskFecProceso.ResetOnSpace = false;
        this.mskFecProceso.Size = new System.Drawing.Size(81, 20);
        this.mskFecProceso.TabIndex = 7;
        this.mskFecProceso.Tag = "";
        // 
        // mskFecIngresoCred
        // 
        this.mskFecIngresoCred.AllowPromptAsInput = false;
        this.mskFecIngresoCred.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.mskFecIngresoCred.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
        this.mskFecIngresoCred.Location = new System.Drawing.Point(192, 275);
        this.mskFecIngresoCred.Mask = "00/00/0000";
        this.mskFecIngresoCred.Name = "mskFecIngresoCred";
        this.mskFecIngresoCred.ResetOnSpace = false;
        this.mskFecIngresoCred.Size = new System.Drawing.Size(81, 20);
        this.mskFecIngresoCred.TabIndex = 8;
        this.mskFecIngresoCred.Tag = "";
        this.mskFecIngresoCred.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mskFecIngresoCred_KeyPress);
        this.mskFecIngresoCred.Leave += new System.EventHandler(this.mskFecIngresoCred_Leave);
        // 
        // mskFecAceptaCred
        // 
        this.mskFecAceptaCred.AllowPromptAsInput = false;
        this.mskFecAceptaCred.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.mskFecAceptaCred.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
        this.mskFecAceptaCred.Location = new System.Drawing.Point(192, 302);
        this.mskFecAceptaCred.Mask = "00/00/0000";
        this.mskFecAceptaCred.Name = "mskFecAceptaCred";
        this.mskFecAceptaCred.ResetOnSpace = false;
        this.mskFecAceptaCred.Size = new System.Drawing.Size(81, 20);
        this.mskFecAceptaCred.TabIndex = 9;
        this.mskFecAceptaCred.Tag = "";
        this.mskFecAceptaCred.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mskFecAceptaCred_KeyPress);
        this.mskFecAceptaCred.Leave += new System.EventHandler(this.mskFecAceptaCred_Leave);
        // 
        // _Label1_13
        // 
        this._Label1_13.BackColor = System.Drawing.SystemColors.Control;
        this._Label1_13.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label1_13.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label1_13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label1_13.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label1_13.Location = new System.Drawing.Point(280, 304);
        this._Label1_13.Name = "_Label1_13";
        this._Label1_13.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label1_13.Size = new System.Drawing.Size(81, 17);
        this._Label1_13.TabIndex = 29;
        this._Label1_13.Tag = "";
        this._Label1_13.Text = "dd/mm/aaaa";
        // 
        // _Label1_12
        // 
        this._Label1_12.BackColor = System.Drawing.SystemColors.Control;
        this._Label1_12.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label1_12.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label1_12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label1_12.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label1_12.Location = new System.Drawing.Point(280, 280);
        this._Label1_12.Name = "_Label1_12";
        this._Label1_12.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label1_12.Size = new System.Drawing.Size(81, 17);
        this._Label1_12.TabIndex = 28;
        this._Label1_12.Tag = "";
        this._Label1_12.Text = "dd/mm/aaaa";
        // 
        // _Label1_11
        // 
        this._Label1_11.BackColor = System.Drawing.SystemColors.Control;
        this._Label1_11.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label1_11.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label1_11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label1_11.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label1_11.Location = new System.Drawing.Point(280, 256);
        this._Label1_11.Name = "_Label1_11";
        this._Label1_11.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label1_11.Size = new System.Drawing.Size(81, 17);
        this._Label1_11.TabIndex = 27;
        this._Label1_11.Tag = "";
        this._Label1_11.Text = "dd/mm/aaaa";
        // 
        // _Label1_10
        // 
        this._Label1_10.BackColor = System.Drawing.SystemColors.Control;
        this._Label1_10.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label1_10.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label1_10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label1_10.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label1_10.Location = new System.Drawing.Point(16, 337);
        this._Label1_10.Name = "_Label1_10";
        this._Label1_10.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label1_10.Size = new System.Drawing.Size(177, 17);
        this._Label1_10.TabIndex = 26;
        this._Label1_10.Tag = "";
        this._Label1_10.Text = "Total de Solicitudes Enviadas:";
        // 
        // _Label1_9
        // 
        this._Label1_9.BackColor = System.Drawing.SystemColors.Control;
        this._Label1_9.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label1_9.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label1_9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label1_9.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label1_9.Location = new System.Drawing.Point(16, 310);
        this._Label1_9.Name = "_Label1_9";
        this._Label1_9.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label1_9.Size = new System.Drawing.Size(177, 17);
        this._Label1_9.TabIndex = 25;
        this._Label1_9.Tag = "";
        this._Label1_9.Text = "Fecha Aceptación en Crédito:";
        // 
        // _Label1_8
        // 
        this._Label1_8.BackColor = System.Drawing.SystemColors.Control;
        this._Label1_8.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label1_8.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label1_8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label1_8.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label1_8.Location = new System.Drawing.Point(16, 283);
        this._Label1_8.Name = "_Label1_8";
        this._Label1_8.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label1_8.Size = new System.Drawing.Size(169, 17);
        this._Label1_8.TabIndex = 24;
        this._Label1_8.Tag = "";
        this._Label1_8.Text = "Fecha de Ingreso a Crédito:";
        // 
        // _Label1_7
        // 
        this._Label1_7.BackColor = System.Drawing.SystemColors.Control;
        this._Label1_7.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label1_7.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label1_7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label1_7.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label1_7.Location = new System.Drawing.Point(16, 255);
        this._Label1_7.Name = "_Label1_7";
        this._Label1_7.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label1_7.Size = new System.Drawing.Size(113, 17);
        this._Label1_7.TabIndex = 23;
        this._Label1_7.Tag = "";
        this._Label1_7.Text = "Fecha de Proceso:";
        // 
        // _Label1_6
        // 
        this._Label1_6.BackColor = System.Drawing.SystemColors.Control;
        this._Label1_6.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label1_6.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label1_6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label1_6.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label1_6.Location = new System.Drawing.Point(16, 224);
        this._Label1_6.Name = "_Label1_6";
        this._Label1_6.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label1_6.Size = new System.Drawing.Size(73, 17);
        this._Label1_6.TabIndex = 22;
        this._Label1_6.Tag = "";
        this._Label1_6.Text = "Promoción:";
        // 
        // _Label1_5
        // 
        this._Label1_5.BackColor = System.Drawing.SystemColors.Control;
        this._Label1_5.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label1_5.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label1_5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label1_5.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label1_5.Location = new System.Drawing.Point(16, 192);
        this._Label1_5.Name = "_Label1_5";
        this._Label1_5.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label1_5.Size = new System.Drawing.Size(105, 17);
        this._Label1_5.TabIndex = 21;
        this._Label1_5.Tag = "";
        this._Label1_5.Text = "Tipo de Solcitud:";
        // 
        // _Label1_4
        // 
        this._Label1_4.BackColor = System.Drawing.SystemColors.Control;
        this._Label1_4.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label1_4.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label1_4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label1_4.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label1_4.Location = new System.Drawing.Point(16, 160);
        this._Label1_4.Name = "_Label1_4";
        this._Label1_4.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label1_4.Size = new System.Drawing.Size(121, 17);
        this._Label1_4.TabIndex = 20;
        this._Label1_4.Tag = "";
        this._Label1_4.Text = "Familia de Producto:";
        // 
        // _Label1_3
        // 
        this._Label1_3.BackColor = System.Drawing.SystemColors.Control;
        this._Label1_3.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label1_3.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label1_3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label1_3.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label1_3.Location = new System.Drawing.Point(16, 128);
        this._Label1_3.Name = "_Label1_3";
        this._Label1_3.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label1_3.Size = new System.Drawing.Size(113, 17);
        this._Label1_3.TabIndex = 19;
        this._Label1_3.Tag = "";
        this._Label1_3.Text = "Entidad Origen:";
        // 
        // _Label1_2
        // 
        this._Label1_2.BackColor = System.Drawing.SystemColors.Control;
        this._Label1_2.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label1_2.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label1_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label1_2.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label1_2.Location = new System.Drawing.Point(16, 96);
        this._Label1_2.Name = "_Label1_2";
        this._Label1_2.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label1_2.Size = new System.Drawing.Size(145, 17);
        this._Label1_2.TabIndex = 18;
        this._Label1_2.Tag = "";
        this._Label1_2.Text = "Tipo de Entidad Origen:";
        // 
        // _Label1_1
        // 
        this._Label1_1.BackColor = System.Drawing.SystemColors.Control;
        this._Label1_1.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label1_1.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label1_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label1_1.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label1_1.Location = new System.Drawing.Point(16, 64);
        this._Label1_1.Name = "_Label1_1";
        this._Label1_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label1_1.Size = new System.Drawing.Size(113, 17);
        this._Label1_1.TabIndex = 17;
        this._Label1_1.Tag = "";
        this._Label1_1.Text = "Tipo de Trámite:";
        // 
        // _Label1_0
        // 
        this._Label1_0.BackColor = System.Drawing.SystemColors.Control;
        this._Label1_0.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label1_0.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label1_0.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label1_0.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label1_0.Location = new System.Drawing.Point(16, 32);
        this._Label1_0.Name = "_Label1_0";
        this._Label1_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label1_0.Size = new System.Drawing.Size(113, 17);
        this._Label1_0.TabIndex = 16;
        this._Label1_0.Tag = "";
        this._Label1_0.Text = "Clave de Remesa:";
        // 
        // frmRegRemesas
        // 
        this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
        this.AutoScroll = true;
        this.BackColor = System.Drawing.SystemColors.Control;
        this.ClientSize = new System.Drawing.Size(484, 484);
        this.ControlBox = false;
        this.Controls.Add(this.Frame2);
        this.Controls.Add(this.Frame1);
        this.Controls.Add(this.MainMenu1);
        this.Cursor = System.Windows.Forms.Cursors.Default;
        this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.KeyPreview = true;
        this.Location = new System.Drawing.Point(92, 92);
        this.MainMenuStrip = this.MainMenu1;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.formHelper1.SetMoveable(this, false);
        this.Name = "frmRegRemesas";
        this.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
        this.Tag = "";
        this.Text = "Registro de Remesas";
        this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmRegRemesas_KeyPress);
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmRegRemesas_FormClosing);
        this.MainMenu1.ResumeLayout(false);
        this.MainMenu1.PerformLayout();
        this.Frame2.ResumeLayout(false);
        this.Frame1.ResumeLayout(false);
        this.Frame1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.listBoxComboBoxHelper1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.commandButtonHelper1)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

	}
	void  InitializeLabel1()
	{
			this.Label1[13] = _Label1_13;
			this.Label1[12] = _Label1_12;
			this.Label1[11] = _Label1_11;
			this.Label1[10] = _Label1_10;
			this.Label1[9] = _Label1_9;
			this.Label1[8] = _Label1_8;
			this.Label1[7] = _Label1_7;
			this.Label1[6] = _Label1_6;
			this.Label1[5] = _Label1_5;
			this.Label1[4] = _Label1_4;
			this.Label1[3] = _Label1_3;
			this.Label1[2] = _Label1_2;
			this.Label1[1] = _Label1_1;
			this.Label1[0] = _Label1_0;
	}
#endregion 

        private System.Windows.Forms.ToolStripMenuItem procesamientoMasivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem predictaminaciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem firmaAlS041ToolStripMenuItem;
        private Artinsoft.VB6.Gui.FormHelper formHelper1;
}
}