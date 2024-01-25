using System; 
using VB6 = Microsoft.VisualBasic.Compatibility.VB6.Support; 

namespace Masivos
{
	partial class frmPredFolio
	{
	
#region "Upgrade Support "
        public static frmPredFolio m_vb6FormDefInstance;
		private static bool m_InitializingDefInstance;
		public static frmPredFolio DefInstance
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
		public frmPredFolio():base(){
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
			InitializeoptSexo();
			//This form is an MDI child.
			//This code simulates the VB6 
			// functionality of automatically
			// loading and showing an MDI
			// child's parent.
			this.MdiParent = Masivos.MDIMasivos.DefInstance;
			Masivos.MDIMasivos.DefInstance.Show();
		}
	public static frmPredFolio CreateInstance()
	{
			frmPredFolio theInstance = new frmPredFolio();
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
	public  System.Windows.Forms.ToolStripMenuItem mnuDeclinar;
	public  System.Windows.Forms.ToolStripMenuItem mnuLimpiar;
	public  System.Windows.Forms.ToolStripSeparator mnuSep1;
	public  System.Windows.Forms.ToolStripMenuItem mnuSalir;
	public  System.Windows.Forms.ToolStripMenuItem mnuPredFolio;
	public  System.Windows.Forms.MenuStrip MainMenu1;
	public  System.Windows.Forms.TextBox txtEntidad;
	public  System.Windows.Forms.Button cmdDeclinar;
	public  System.Windows.Forms.Button cmdSalir;
	public  System.Windows.Forms.Button cmdLimpiar;
	public  System.Windows.Forms.GroupBox Frame2;
	public  System.Windows.Forms.TextBox txtProducto;
	public  System.Windows.Forms.TextBox txtProceso;
	public  System.Windows.Forms.TextBox txtEstatus;
	public  System.Windows.Forms.TextBox txtCausa;
	public  System.Windows.Forms.MaskedTextBox mskFecEstatus;
	private  System.Windows.Forms.Label _Label1_12;
	private  System.Windows.Forms.Label _Label1_13;
	private  System.Windows.Forms.Label _Label1_16;
	private  System.Windows.Forms.Label _Label1_17;
	private  System.Windows.Forms.Label _Label1_39;
	public  System.Windows.Forms.GroupBox Frame5;
	public  System.Windows.Forms.TextBox txtCP;
	public  System.Windows.Forms.TextBox txtColonia;
	public  System.Windows.Forms.TextBox txtCalleyNo;
	public  System.Windows.Forms.TextBox txtCd;
	public  System.Windows.Forms.TextBox txtRFCPyme;
	public  System.Windows.Forms.TextBox txtNumCliente;
	public  System.Windows.Forms.TextBox txtNumFolio;
	public  System.Windows.Forms.TextBox txtNombre;
	private  System.Windows.Forms.RadioButton _optSexo_1;
	private  System.Windows.Forms.RadioButton _optSexo_0;
	public  System.Windows.Forms.GroupBox Frame3;
	public  System.Windows.Forms.TextBox txtFolioInt;
	public  System.Windows.Forms.MaskedTextBox mskFechaNaci;
	public  System.Windows.Forms.MaskedTextBox mskTelefono;
	private  System.Windows.Forms.Label _Label1_23;
	private  System.Windows.Forms.Label _Label1_22;
	private  System.Windows.Forms.Label _Label1_5;
	private  System.Windows.Forms.Label _Label1_21;
	private  System.Windows.Forms.Label _Label1_20;
	private  System.Windows.Forms.Label _Label1_19;
	private  System.Windows.Forms.Label _Label1_18;
	private  System.Windows.Forms.Label _Label1_4;
	private  System.Windows.Forms.Label _Label1_2;
	private  System.Windows.Forms.Label _Label1_1;
	private  System.Windows.Forms.Label _Label1_6;
	private  System.Windows.Forms.Label _Label1_3;
	public  System.Windows.Forms.GroupBox fraDatosSol;
	public  System.Windows.Forms.TextBox txtFolioPreimpreso;
	public  System.Windows.Forms.ComboBox cboTipoTram;
	private  System.Windows.Forms.Label _Label1_0;
	public  System.Windows.Forms.Label Label2;
	public  System.Windows.Forms.GroupBox Frame1;
	public System.Windows.Forms.Label[] Label1 = new System.Windows.Forms.Label[40];
	public System.Windows.Forms.RadioButton[] optSexo = new System.Windows.Forms.RadioButton[2];
	private Artinsoft.VB6.Gui.CommandButtonHelper commandButtonHelper1;
	//NOTE: The following procedure is required by the Windows Form Designer
	//It can be modified using the Windows Form Designer.
	//Do not modify it using the code editor.
	[System.Diagnostics.DebuggerStepThrough]
	 private void  InitializeComponent()
	{
        this.components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPredFolio));
        this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
        this.cmdDeclinar = new System.Windows.Forms.Button();
        this.cmdSalir = new System.Windows.Forms.Button();
        this.cmdLimpiar = new System.Windows.Forms.Button();
        this.MainMenu1 = new System.Windows.Forms.MenuStrip();
        this.mnuPredFolio = new System.Windows.Forms.ToolStripMenuItem();
        this.mnuDeclinar = new System.Windows.Forms.ToolStripMenuItem();
        this.mnuLimpiar = new System.Windows.Forms.ToolStripMenuItem();
        this.mnuSep1 = new System.Windows.Forms.ToolStripSeparator();
        this.mnuSalir = new System.Windows.Forms.ToolStripMenuItem();
        this.dummy1 = new System.Windows.Forms.ToolStripMenuItem();
        this.dummy2 = new System.Windows.Forms.ToolStripMenuItem();
        this.txtEntidad = new System.Windows.Forms.TextBox();
        this.Frame2 = new System.Windows.Forms.GroupBox();
        this.Frame5 = new System.Windows.Forms.GroupBox();
        this.txtProducto = new System.Windows.Forms.TextBox();
        this.txtProceso = new System.Windows.Forms.TextBox();
        this.txtEstatus = new System.Windows.Forms.TextBox();
        this.txtCausa = new System.Windows.Forms.TextBox();
        this.mskFecEstatus = new System.Windows.Forms.MaskedTextBox();
        this._Label1_12 = new System.Windows.Forms.Label();
        this._Label1_13 = new System.Windows.Forms.Label();
        this._Label1_16 = new System.Windows.Forms.Label();
        this._Label1_17 = new System.Windows.Forms.Label();
        this._Label1_39 = new System.Windows.Forms.Label();
        this.fraDatosSol = new System.Windows.Forms.GroupBox();
        this.txtCP = new System.Windows.Forms.TextBox();
        this.txtColonia = new System.Windows.Forms.TextBox();
        this.txtCalleyNo = new System.Windows.Forms.TextBox();
        this.txtCd = new System.Windows.Forms.TextBox();
        this.txtRFCPyme = new System.Windows.Forms.TextBox();
        this.txtNumCliente = new System.Windows.Forms.TextBox();
        this.txtNumFolio = new System.Windows.Forms.TextBox();
        this.txtNombre = new System.Windows.Forms.TextBox();
        this.Frame3 = new System.Windows.Forms.GroupBox();
        this._optSexo_1 = new System.Windows.Forms.RadioButton();
        this._optSexo_0 = new System.Windows.Forms.RadioButton();
        this.txtFolioInt = new System.Windows.Forms.TextBox();
        this.mskFechaNaci = new System.Windows.Forms.MaskedTextBox();
        this.mskTelefono = new System.Windows.Forms.MaskedTextBox();
        this._Label1_23 = new System.Windows.Forms.Label();
        this._Label1_22 = new System.Windows.Forms.Label();
        this._Label1_5 = new System.Windows.Forms.Label();
        this._Label1_21 = new System.Windows.Forms.Label();
        this._Label1_20 = new System.Windows.Forms.Label();
        this._Label1_19 = new System.Windows.Forms.Label();
        this._Label1_18 = new System.Windows.Forms.Label();
        this._Label1_4 = new System.Windows.Forms.Label();
        this._Label1_2 = new System.Windows.Forms.Label();
        this._Label1_1 = new System.Windows.Forms.Label();
        this._Label1_6 = new System.Windows.Forms.Label();
        this._Label1_3 = new System.Windows.Forms.Label();
        this.Frame1 = new System.Windows.Forms.GroupBox();
        this.txtFolioPreimpreso = new System.Windows.Forms.TextBox();
        this.cboTipoTram = new System.Windows.Forms.ComboBox();
        this._Label1_0 = new System.Windows.Forms.Label();
        this.Label2 = new System.Windows.Forms.Label();
        this.commandButtonHelper1 = new Artinsoft.VB6.Gui.CommandButtonHelper(this.components);
        this.formHelper1 = new Artinsoft.VB6.Gui.FormHelper(this.components);
        this.MainMenu1.SuspendLayout();
        this.Frame2.SuspendLayout();
        this.Frame5.SuspendLayout();
        this.fraDatosSol.SuspendLayout();
        this.Frame3.SuspendLayout();
        this.Frame1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.commandButtonHelper1)).BeginInit();
        this.SuspendLayout();
        // 
        // cmdDeclinar
        // 
        this.cmdDeclinar.BackColor = System.Drawing.SystemColors.Control;
        this.commandButtonHelper1.SetCorrectEventsBehavior(this.cmdDeclinar, true);
        this.cmdDeclinar.Cursor = System.Windows.Forms.Cursors.Default;
        this.commandButtonHelper1.SetDisabledPicture(this.cmdDeclinar, null);
        this.commandButtonHelper1.SetDownPicture(this.cmdDeclinar, null);
        this.cmdDeclinar.Enabled = false;
        this.cmdDeclinar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.cmdDeclinar.ForeColor = System.Drawing.SystemColors.ControlText;
        this.cmdDeclinar.Image = ((System.Drawing.Image)(resources.GetObject("cmdDeclinar.Image")));
        this.cmdDeclinar.Location = new System.Drawing.Point(216, 16);
        this.commandButtonHelper1.SetMaskColor(this.cmdDeclinar, System.Drawing.Color.Silver);
        this.cmdDeclinar.Name = "cmdDeclinar";
        this.cmdDeclinar.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.cmdDeclinar.Size = new System.Drawing.Size(59, 49);
        this.commandButtonHelper1.SetStyle(this.cmdDeclinar, 1);
        this.cmdDeclinar.TabIndex = 46;
        this.cmdDeclinar.TabStop = false;
        this.cmdDeclinar.Tag = "";
        this.cmdDeclinar.Text = "Declinar";
        this.cmdDeclinar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
        this.cmdDeclinar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
        this.ToolTip1.SetToolTip(this.cmdDeclinar, "Declinar esta solicitud");
        this.cmdDeclinar.UseVisualStyleBackColor = false;
        this.cmdDeclinar.Click += new System.EventHandler(this.cmdDeclinar_Click);
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
        this.cmdSalir.Location = new System.Drawing.Point(344, 16);
        this.commandButtonHelper1.SetMaskColor(this.cmdSalir, System.Drawing.Color.Silver);
        this.cmdSalir.Name = "cmdSalir";
        this.cmdSalir.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.cmdSalir.Size = new System.Drawing.Size(59, 49);
        this.commandButtonHelper1.SetStyle(this.cmdSalir, 1);
        this.cmdSalir.TabIndex = 45;
        this.cmdSalir.Tag = "";
        this.cmdSalir.Text = "Salir";
        this.cmdSalir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
        this.cmdSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
        this.ToolTip1.SetToolTip(this.cmdSalir, "Salir de la pantalla");
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
        this.cmdLimpiar.Location = new System.Drawing.Point(280, 16);
        this.commandButtonHelper1.SetMaskColor(this.cmdLimpiar, System.Drawing.Color.Silver);
        this.cmdLimpiar.Name = "cmdLimpiar";
        this.cmdLimpiar.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.cmdLimpiar.Size = new System.Drawing.Size(59, 49);
        this.commandButtonHelper1.SetStyle(this.cmdLimpiar, 1);
        this.cmdLimpiar.TabIndex = 44;
        this.cmdLimpiar.Tag = "";
        this.cmdLimpiar.Text = "Limpiar";
        this.cmdLimpiar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
        this.cmdLimpiar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
        this.ToolTip1.SetToolTip(this.cmdLimpiar, "Limpiar los datos de la pantalla");
        this.cmdLimpiar.UseVisualStyleBackColor = false;
        this.cmdLimpiar.Click += new System.EventHandler(this.cmdLimpiar_Click);
        // 
        // MainMenu1
        // 
        this.MainMenu1.BackColor = System.Drawing.SystemColors.ButtonFace;
        this.MainMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuPredFolio,
            this.dummy1,
            this.dummy2});
        this.MainMenu1.Location = new System.Drawing.Point(0, 0);
        this.MainMenu1.Name = "MainMenu1";
        this.MainMenu1.Size = new System.Drawing.Size(637, 24);
        this.MainMenu1.TabIndex = 48;
        this.MainMenu1.Visible = false;
        // 
        // mnuPredFolio
        // 
        this.mnuPredFolio.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDeclinar,
            this.mnuLimpiar,
            this.mnuSep1,
            this.mnuSalir});
        this.mnuPredFolio.MergeAction = System.Windows.Forms.MergeAction.Replace;
        this.mnuPredFolio.MergeIndex = 0;
        this.mnuPredFolio.Name = "mnuPredFolio";
        this.mnuPredFolio.Size = new System.Drawing.Size(99, 20);
        this.mnuPredFolio.Tag = "";
        this.mnuPredFolio.Text = "&Predictaminacion";
        // 
        // mnuDeclinar
        // 
        this.mnuDeclinar.Name = "mnuDeclinar";
        this.mnuDeclinar.Size = new System.Drawing.Size(123, 22);
        this.mnuDeclinar.Tag = "";
        this.mnuDeclinar.Text = "&Declinar";
        this.mnuDeclinar.Click += new System.EventHandler(this.mnuDeclinar_Click);
        // 
        // mnuLimpiar
        // 
        this.mnuLimpiar.Name = "mnuLimpiar";
        this.mnuLimpiar.Size = new System.Drawing.Size(123, 22);
        this.mnuLimpiar.Tag = "";
        this.mnuLimpiar.Text = "&Limpiar";
        this.mnuLimpiar.Click += new System.EventHandler(this.mnuLimpiar_Click);
        // 
        // mnuSep1
        // 
        this.mnuSep1.Name = "mnuSep1";
        this.mnuSep1.Size = new System.Drawing.Size(120, 6);
        this.mnuSep1.Tag = "";
        // 
        // mnuSalir
        // 
        this.mnuSalir.Name = "mnuSalir";
        this.mnuSalir.Size = new System.Drawing.Size(123, 22);
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
        // txtEntidad
        // 
        this.txtEntidad.AcceptsReturn = true;
        this.txtEntidad.BackColor = System.Drawing.SystemColors.Window;
        this.txtEntidad.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtEntidad.Enabled = false;
        this.txtEntidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtEntidad.ForeColor = System.Drawing.Color.Blue;
        this.txtEntidad.Location = new System.Drawing.Point(96, 197);
        this.txtEntidad.MaxLength = 26;
        this.txtEntidad.Name = "txtEntidad";
        this.txtEntidad.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtEntidad.Size = new System.Drawing.Size(225, 20);
        this.txtEntidad.TabIndex = 47;
        this.txtEntidad.TabStop = false;
        this.txtEntidad.Tag = "CCIUDAD";
        // 
        // Frame2
        // 
        this.Frame2.BackColor = System.Drawing.SystemColors.Control;
        this.Frame2.Controls.Add(this.cmdDeclinar);
        this.Frame2.Controls.Add(this.cmdSalir);
        this.Frame2.Controls.Add(this.cmdLimpiar);
        this.Frame2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Frame2.ForeColor = System.Drawing.SystemColors.ControlText;
        this.Frame2.Location = new System.Drawing.Point(0, 349);
        this.Frame2.Name = "Frame2";
        this.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.Frame2.Size = new System.Drawing.Size(633, 73);
        this.Frame2.TabIndex = 43;
        this.Frame2.TabStop = false;
        this.Frame2.Tag = "";
        // 
        // Frame5
        // 
        this.Frame5.BackColor = System.Drawing.SystemColors.Control;
        this.Frame5.Controls.Add(this.txtProducto);
        this.Frame5.Controls.Add(this.txtProceso);
        this.Frame5.Controls.Add(this.txtEstatus);
        this.Frame5.Controls.Add(this.txtCausa);
        this.Frame5.Controls.Add(this.mskFecEstatus);
        this.Frame5.Controls.Add(this._Label1_12);
        this.Frame5.Controls.Add(this._Label1_13);
        this.Frame5.Controls.Add(this._Label1_16);
        this.Frame5.Controls.Add(this._Label1_17);
        this.Frame5.Controls.Add(this._Label1_39);
        this.Frame5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Frame5.ForeColor = System.Drawing.Color.Blue;
        this.Frame5.Location = new System.Drawing.Point(0, 257);
        this.Frame5.Name = "Frame5";
        this.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.Frame5.Size = new System.Drawing.Size(633, 95);
        this.Frame5.TabIndex = 19;
        this.Frame5.TabStop = false;
        this.Frame5.Tag = "";
        this.Frame5.Text = "Situación de la Solicitud ";
        // 
        // txtProducto
        // 
        this.txtProducto.AcceptsReturn = true;
        this.txtProducto.BackColor = System.Drawing.SystemColors.Window;
        this.txtProducto.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtProducto.Enabled = false;
        this.txtProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtProducto.ForeColor = System.Drawing.SystemColors.WindowText;
        this.txtProducto.Location = new System.Drawing.Point(72, 16);
        this.txtProducto.MaxLength = 0;
        this.txtProducto.Name = "txtProducto";
        this.txtProducto.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtProducto.Size = new System.Drawing.Size(233, 20);
        this.txtProducto.TabIndex = 23;
        this.txtProducto.TabStop = false;
        this.txtProducto.Tag = "";
        // 
        // txtProceso
        // 
        this.txtProceso.AcceptsReturn = true;
        this.txtProceso.BackColor = System.Drawing.SystemColors.Window;
        this.txtProceso.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtProceso.Enabled = false;
        this.txtProceso.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtProceso.ForeColor = System.Drawing.SystemColors.WindowText;
        this.txtProceso.Location = new System.Drawing.Point(72, 40);
        this.txtProceso.MaxLength = 0;
        this.txtProceso.Name = "txtProceso";
        this.txtProceso.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtProceso.Size = new System.Drawing.Size(233, 20);
        this.txtProceso.TabIndex = 22;
        this.txtProceso.TabStop = false;
        this.txtProceso.Tag = "";
        // 
        // txtEstatus
        // 
        this.txtEstatus.AcceptsReturn = true;
        this.txtEstatus.BackColor = System.Drawing.SystemColors.Window;
        this.txtEstatus.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtEstatus.Enabled = false;
        this.txtEstatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtEstatus.ForeColor = System.Drawing.SystemColors.WindowText;
        this.txtEstatus.Location = new System.Drawing.Point(72, 64);
        this.txtEstatus.MaxLength = 0;
        this.txtEstatus.Name = "txtEstatus";
        this.txtEstatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtEstatus.Size = new System.Drawing.Size(233, 20);
        this.txtEstatus.TabIndex = 21;
        this.txtEstatus.TabStop = false;
        this.txtEstatus.Tag = "";
        // 
        // txtCausa
        // 
        this.txtCausa.AcceptsReturn = true;
        this.txtCausa.BackColor = System.Drawing.SystemColors.Window;
        this.txtCausa.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtCausa.Enabled = false;
        this.txtCausa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtCausa.ForeColor = System.Drawing.SystemColors.WindowText;
        this.txtCausa.Location = new System.Drawing.Point(392, 40);
        this.txtCausa.MaxLength = 0;
        this.txtCausa.Name = "txtCausa";
        this.txtCausa.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtCausa.Size = new System.Drawing.Size(233, 20);
        this.txtCausa.TabIndex = 20;
        this.txtCausa.TabStop = false;
        this.txtCausa.Tag = "";
        // 
        // mskFecEstatus
        // 
        this.mskFecEstatus.AllowPromptAsInput = false;
        this.mskFecEstatus.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
        this.mskFecEstatus.Enabled = false;
        this.mskFecEstatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.mskFecEstatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
        this.mskFecEstatus.Location = new System.Drawing.Point(393, 63);
        this.mskFecEstatus.Mask = "00/00/0000";
        this.mskFecEstatus.Name = "mskFecEstatus";
        this.mskFecEstatus.ResetOnSpace = false;
        this.mskFecEstatus.Size = new System.Drawing.Size(81, 20);
        this.mskFecEstatus.TabIndex = 24;
        this.mskFecEstatus.TabStop = false;
        this.mskFecEstatus.Tag = "CFEC.NAC.ADIC.";
        // 
        // _Label1_12
        // 
        this._Label1_12.BackColor = System.Drawing.SystemColors.Control;
        this._Label1_12.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label1_12.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label1_12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label1_12.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label1_12.Location = new System.Drawing.Point(8, 16);
        this._Label1_12.Name = "_Label1_12";
        this._Label1_12.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label1_12.Size = new System.Drawing.Size(57, 17);
        this._Label1_12.TabIndex = 29;
        this._Label1_12.Tag = "";
        this._Label1_12.Text = "Producto:";
        // 
        // _Label1_13
        // 
        this._Label1_13.BackColor = System.Drawing.SystemColors.Control;
        this._Label1_13.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label1_13.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label1_13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label1_13.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label1_13.Location = new System.Drawing.Point(8, 40);
        this._Label1_13.Name = "_Label1_13";
        this._Label1_13.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label1_13.Size = new System.Drawing.Size(57, 17);
        this._Label1_13.TabIndex = 28;
        this._Label1_13.Tag = "";
        this._Label1_13.Text = "Proceso:";
        // 
        // _Label1_16
        // 
        this._Label1_16.BackColor = System.Drawing.SystemColors.Control;
        this._Label1_16.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label1_16.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label1_16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label1_16.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label1_16.Location = new System.Drawing.Point(8, 64);
        this._Label1_16.Name = "_Label1_16";
        this._Label1_16.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label1_16.Size = new System.Drawing.Size(41, 17);
        this._Label1_16.TabIndex = 27;
        this._Label1_16.Tag = "";
        this._Label1_16.Text = "Estatus:";
        // 
        // _Label1_17
        // 
        this._Label1_17.BackColor = System.Drawing.SystemColors.Control;
        this._Label1_17.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label1_17.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label1_17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label1_17.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label1_17.Location = new System.Drawing.Point(312, 42);
        this._Label1_17.Name = "_Label1_17";
        this._Label1_17.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label1_17.Size = new System.Drawing.Size(41, 17);
        this._Label1_17.TabIndex = 26;
        this._Label1_17.Tag = "";
        this._Label1_17.Text = "Causa:";
        // 
        // _Label1_39
        // 
        this._Label1_39.BackColor = System.Drawing.SystemColors.Control;
        this._Label1_39.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label1_39.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label1_39.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label1_39.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label1_39.Location = new System.Drawing.Point(312, 64);
        this._Label1_39.Name = "_Label1_39";
        this._Label1_39.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label1_39.Size = new System.Drawing.Size(73, 17);
        this._Label1_39.TabIndex = 25;
        this._Label1_39.Tag = "";
        this._Label1_39.Text = "Fecha Estatus:";
        // 
        // fraDatosSol
        // 
        this.fraDatosSol.BackColor = System.Drawing.SystemColors.Control;
        this.fraDatosSol.Controls.Add(this.txtCP);
        this.fraDatosSol.Controls.Add(this.txtColonia);
        this.fraDatosSol.Controls.Add(this.txtCalleyNo);
        this.fraDatosSol.Controls.Add(this.txtCd);
        this.fraDatosSol.Controls.Add(this.txtRFCPyme);
        this.fraDatosSol.Controls.Add(this.txtNumCliente);
        this.fraDatosSol.Controls.Add(this.txtNumFolio);
        this.fraDatosSol.Controls.Add(this.txtNombre);
        this.fraDatosSol.Controls.Add(this.Frame3);
        this.fraDatosSol.Controls.Add(this.txtFolioInt);
        this.fraDatosSol.Controls.Add(this.mskFechaNaci);
        this.fraDatosSol.Controls.Add(this.mskTelefono);
        this.fraDatosSol.Controls.Add(this._Label1_23);
        this.fraDatosSol.Controls.Add(this._Label1_22);
        this.fraDatosSol.Controls.Add(this._Label1_5);
        this.fraDatosSol.Controls.Add(this._Label1_21);
        this.fraDatosSol.Controls.Add(this._Label1_20);
        this.fraDatosSol.Controls.Add(this._Label1_19);
        this.fraDatosSol.Controls.Add(this._Label1_18);
        this.fraDatosSol.Controls.Add(this._Label1_4);
        this.fraDatosSol.Controls.Add(this._Label1_2);
        this.fraDatosSol.Controls.Add(this._Label1_1);
        this.fraDatosSol.Controls.Add(this._Label1_6);
        this.fraDatosSol.Controls.Add(this._Label1_3);
        this.fraDatosSol.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.fraDatosSol.ForeColor = System.Drawing.Color.Blue;
        this.fraDatosSol.Location = new System.Drawing.Point(0, 61);
        this.fraDatosSol.Name = "fraDatosSol";
        this.fraDatosSol.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.fraDatosSol.Size = new System.Drawing.Size(633, 193);
        this.fraDatosSol.TabIndex = 5;
        this.fraDatosSol.TabStop = false;
        this.fraDatosSol.Tag = "";
        this.fraDatosSol.Text = "Datos de Solicitud";
        // 
        // txtCP
        // 
        this.txtCP.AcceptsReturn = true;
        this.txtCP.BackColor = System.Drawing.SystemColors.Window;
        this.txtCP.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtCP.Enabled = false;
        this.txtCP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtCP.ForeColor = System.Drawing.Color.Blue;
        this.txtCP.Location = new System.Drawing.Point(96, 160);
        this.txtCP.MaxLength = 5;
        this.txtCP.Name = "txtCP";
        this.txtCP.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtCP.Size = new System.Drawing.Size(41, 20);
        this.txtCP.TabIndex = 39;
        this.txtCP.TabStop = false;
        this.txtCP.Tag = "CC.P.";
        this.txtCP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        // 
        // txtColonia
        // 
        this.txtColonia.AcceptsReturn = true;
        this.txtColonia.BackColor = System.Drawing.SystemColors.Window;
        this.txtColonia.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtColonia.Enabled = false;
        this.txtColonia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtColonia.ForeColor = System.Drawing.Color.Blue;
        this.txtColonia.Location = new System.Drawing.Point(96, 88);
        this.txtColonia.MaxLength = 35;
        this.txtColonia.Name = "txtColonia";
        this.txtColonia.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtColonia.Size = new System.Drawing.Size(329, 20);
        this.txtColonia.TabIndex = 32;
        this.txtColonia.TabStop = false;
        this.txtColonia.Tag = "CCOLONIA";
        // 
        // txtCalleyNo
        // 
        this.txtCalleyNo.AcceptsReturn = true;
        this.txtCalleyNo.BackColor = System.Drawing.SystemColors.Window;
        this.txtCalleyNo.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtCalleyNo.Enabled = false;
        this.txtCalleyNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtCalleyNo.ForeColor = System.Drawing.Color.Blue;
        this.txtCalleyNo.Location = new System.Drawing.Point(96, 64);
        this.txtCalleyNo.MaxLength = 35;
        this.txtCalleyNo.Name = "txtCalleyNo";
        this.txtCalleyNo.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtCalleyNo.Size = new System.Drawing.Size(329, 20);
        this.txtCalleyNo.TabIndex = 31;
        this.txtCalleyNo.TabStop = false;
        this.txtCalleyNo.Tag = "CDOMICILIO";
        // 
        // txtCd
        // 
        this.txtCd.AcceptsReturn = true;
        this.txtCd.BackColor = System.Drawing.SystemColors.Window;
        this.txtCd.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtCd.Enabled = false;
        this.txtCd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtCd.ForeColor = System.Drawing.Color.Blue;
        this.txtCd.Location = new System.Drawing.Point(96, 112);
        this.txtCd.MaxLength = 26;
        this.txtCd.Name = "txtCd";
        this.txtCd.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtCd.Size = new System.Drawing.Size(225, 20);
        this.txtCd.TabIndex = 30;
        this.txtCd.TabStop = false;
        this.txtCd.Tag = "CCIUDAD";
        // 
        // txtRFCPyme
        // 
        this.txtRFCPyme.AcceptsReturn = true;
        this.txtRFCPyme.BackColor = System.Drawing.SystemColors.Window;
        this.txtRFCPyme.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtRFCPyme.Enabled = false;
        this.txtRFCPyme.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtRFCPyme.ForeColor = System.Drawing.SystemColors.WindowText;
        this.txtRFCPyme.Location = new System.Drawing.Point(528, 64);
        this.txtRFCPyme.MaxLength = 0;
        this.txtRFCPyme.Name = "txtRFCPyme";
        this.txtRFCPyme.ReadOnly = true;
        this.txtRFCPyme.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtRFCPyme.Size = new System.Drawing.Size(97, 20);
        this.txtRFCPyme.TabIndex = 13;
        this.txtRFCPyme.TabStop = false;
        this.txtRFCPyme.Tag = "";
        // 
        // txtNumCliente
        // 
        this.txtNumCliente.AcceptsReturn = true;
        this.txtNumCliente.BackColor = System.Drawing.SystemColors.Window;
        this.txtNumCliente.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtNumCliente.Enabled = false;
        this.txtNumCliente.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtNumCliente.ForeColor = System.Drawing.SystemColors.MenuText;
        this.txtNumCliente.Location = new System.Drawing.Point(528, 16);
        this.txtNumCliente.MaxLength = 12;
        this.txtNumCliente.Name = "txtNumCliente";
        this.txtNumCliente.ReadOnly = true;
        this.txtNumCliente.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtNumCliente.Size = new System.Drawing.Size(81, 20);
        this.txtNumCliente.TabIndex = 12;
        this.txtNumCliente.TabStop = false;
        this.txtNumCliente.Tag = "";
        this.txtNumCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        // 
        // txtNumFolio
        // 
        this.txtNumFolio.AcceptsReturn = true;
        this.txtNumFolio.BackColor = System.Drawing.SystemColors.Window;
        this.txtNumFolio.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtNumFolio.Enabled = false;
        this.txtNumFolio.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtNumFolio.ForeColor = System.Drawing.SystemColors.MenuText;
        this.txtNumFolio.Location = new System.Drawing.Point(96, 16);
        this.txtNumFolio.MaxLength = 16;
        this.txtNumFolio.Name = "txtNumFolio";
        this.txtNumFolio.ReadOnly = true;
        this.txtNumFolio.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtNumFolio.Size = new System.Drawing.Size(114, 20);
        this.txtNumFolio.TabIndex = 11;
        this.txtNumFolio.TabStop = false;
        this.txtNumFolio.Tag = "";
        this.txtNumFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        // 
        // txtNombre
        // 
        this.txtNombre.AcceptsReturn = true;
        this.txtNombre.BackColor = System.Drawing.SystemColors.Window;
        this.txtNombre.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtNombre.Enabled = false;
        this.txtNombre.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtNombre.ForeColor = System.Drawing.Color.Blue;
        this.txtNombre.Location = new System.Drawing.Point(96, 40);
        this.txtNombre.MaxLength = 26;
        this.txtNombre.Name = "txtNombre";
        this.txtNombre.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtNombre.Size = new System.Drawing.Size(329, 20);
        this.txtNombre.TabIndex = 10;
        this.txtNombre.TabStop = false;
        this.txtNombre.Tag = "CNOMBRE";
        // 
        // Frame3
        // 
        this.Frame3.BackColor = System.Drawing.SystemColors.Control;
        this.Frame3.Controls.Add(this._optSexo_1);
        this.Frame3.Controls.Add(this._optSexo_0);
        this.Frame3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Frame3.ForeColor = System.Drawing.SystemColors.ControlText;
        this.Frame3.Location = new System.Drawing.Point(440, 88);
        this.Frame3.Name = "Frame3";
        this.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.Frame3.Size = new System.Drawing.Size(181, 54);
        this.Frame3.TabIndex = 7;
        this.Frame3.TabStop = false;
        this.Frame3.Tag = "";
        this.Frame3.Text = "Sexo: ";
        // 
        // _optSexo_1
        // 
        this._optSexo_1.BackColor = System.Drawing.SystemColors.Control;
        this._optSexo_1.Cursor = System.Windows.Forms.Cursors.Default;
        this._optSexo_1.Enabled = false;
        this._optSexo_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._optSexo_1.ForeColor = System.Drawing.SystemColors.ControlText;
        this._optSexo_1.Location = new System.Drawing.Point(88, 24);
        this._optSexo_1.Name = "_optSexo_1";
        this._optSexo_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._optSexo_1.Size = new System.Drawing.Size(73, 17);
        this._optSexo_1.TabIndex = 9;
        this._optSexo_1.Tag = "";
        this._optSexo_1.Text = "Masculino";
        this._optSexo_1.UseVisualStyleBackColor = false;
        // 
        // _optSexo_0
        // 
        this._optSexo_0.BackColor = System.Drawing.SystemColors.Control;
        this._optSexo_0.Cursor = System.Windows.Forms.Cursors.Default;
        this._optSexo_0.Enabled = false;
        this._optSexo_0.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._optSexo_0.ForeColor = System.Drawing.SystemColors.ControlText;
        this._optSexo_0.Location = new System.Drawing.Point(16, 24);
        this._optSexo_0.Name = "_optSexo_0";
        this._optSexo_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._optSexo_0.Size = new System.Drawing.Size(73, 17);
        this._optSexo_0.TabIndex = 8;
        this._optSexo_0.Tag = "";
        this._optSexo_0.Text = "Femenino";
        this._optSexo_0.UseVisualStyleBackColor = false;
        // 
        // txtFolioInt
        // 
        this.txtFolioInt.AcceptsReturn = true;
        this.txtFolioInt.BackColor = System.Drawing.SystemColors.Window;
        this.txtFolioInt.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtFolioInt.Enabled = false;
        this.txtFolioInt.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtFolioInt.ForeColor = System.Drawing.SystemColors.MenuText;
        this.txtFolioInt.Location = new System.Drawing.Point(320, 16);
        this.txtFolioInt.MaxLength = 16;
        this.txtFolioInt.Name = "txtFolioInt";
        this.txtFolioInt.ReadOnly = true;
        this.txtFolioInt.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtFolioInt.Size = new System.Drawing.Size(105, 20);
        this.txtFolioInt.TabIndex = 6;
        this.txtFolioInt.TabStop = false;
        this.txtFolioInt.Tag = "";
        this.txtFolioInt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        // 
        // mskFechaNaci
        // 
        this.mskFechaNaci.AllowPromptAsInput = false;
        this.mskFechaNaci.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
        this.mskFechaNaci.Enabled = false;
        this.mskFechaNaci.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.mskFechaNaci.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
        this.mskFechaNaci.Location = new System.Drawing.Point(528, 40);
        this.mskFechaNaci.Mask = "00/00/0000";
        this.mskFechaNaci.Name = "mskFechaNaci";
        this.mskFechaNaci.ResetOnSpace = false;
        this.mskFechaNaci.Size = new System.Drawing.Size(81, 20);
        this.mskFechaNaci.TabIndex = 37;
        this.mskFechaNaci.TabStop = false;
        this.mskFechaNaci.Tag = "CFEC.NAC.ADIC.";
        // 
        // mskTelefono
        // 
        this.mskTelefono.AllowPromptAsInput = false;
        this.mskTelefono.Enabled = false;
        this.mskTelefono.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.mskTelefono.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
        this.mskTelefono.Location = new System.Drawing.Point(216, 160);
        this.mskTelefono.Mask = "(000)000,0000";
        this.mskTelefono.Name = "mskTelefono";
        this.mskTelefono.ResetOnSpace = false;
        this.mskTelefono.Size = new System.Drawing.Size(105, 20);
        this.mskTelefono.TabIndex = 40;
        this.mskTelefono.TabStop = false;
        this.mskTelefono.Tag = "CTELEF.";
        // 
        // _Label1_23
        // 
        this._Label1_23.AutoSize = true;
        this._Label1_23.BackColor = System.Drawing.SystemColors.Control;
        this._Label1_23.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label1_23.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label1_23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label1_23.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label1_23.Location = new System.Drawing.Point(160, 160);
        this._Label1_23.Name = "_Label1_23";
        this._Label1_23.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label1_23.Size = new System.Drawing.Size(52, 13);
        this._Label1_23.TabIndex = 42;
        this._Label1_23.Tag = "";
        this._Label1_23.Text = "Teléfono:";
        // 
        // _Label1_22
        // 
        this._Label1_22.AutoSize = true;
        this._Label1_22.BackColor = System.Drawing.SystemColors.Control;
        this._Label1_22.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label1_22.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label1_22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label1_22.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label1_22.Location = new System.Drawing.Point(8, 160);
        this._Label1_22.Name = "_Label1_22";
        this._Label1_22.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label1_22.Size = new System.Drawing.Size(64, 13);
        this._Label1_22.TabIndex = 41;
        this._Label1_22.Tag = "";
        this._Label1_22.Text = "Cod. Postal:";
        // 
        // _Label1_5
        // 
        this._Label1_5.AutoSize = true;
        this._Label1_5.BackColor = System.Drawing.SystemColors.Control;
        this._Label1_5.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label1_5.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label1_5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label1_5.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label1_5.Location = new System.Drawing.Point(440, 43);
        this._Label1_5.Name = "_Label1_5";
        this._Label1_5.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label1_5.Size = new System.Drawing.Size(84, 13);
        this._Label1_5.TabIndex = 38;
        this._Label1_5.Tag = "";
        this._Label1_5.Text = "Fec.Nacimiento:";
        // 
        // _Label1_21
        // 
        this._Label1_21.AutoSize = true;
        this._Label1_21.BackColor = System.Drawing.SystemColors.Control;
        this._Label1_21.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label1_21.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label1_21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label1_21.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label1_21.Location = new System.Drawing.Point(8, 138);
        this._Label1_21.Name = "_Label1_21";
        this._Label1_21.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label1_21.Size = new System.Drawing.Size(61, 13);
        this._Label1_21.TabIndex = 36;
        this._Label1_21.Tag = "";
        this._Label1_21.Text = "Entid. Fed.:";
        // 
        // _Label1_20
        // 
        this._Label1_20.AutoSize = true;
        this._Label1_20.BackColor = System.Drawing.SystemColors.Control;
        this._Label1_20.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label1_20.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label1_20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label1_20.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label1_20.Location = new System.Drawing.Point(8, 114);
        this._Label1_20.Name = "_Label1_20";
        this._Label1_20.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label1_20.Size = new System.Drawing.Size(55, 13);
        this._Label1_20.TabIndex = 35;
        this._Label1_20.Tag = "";
        this._Label1_20.Text = "Cd./Mpo.:";
        // 
        // _Label1_19
        // 
        this._Label1_19.AutoSize = true;
        this._Label1_19.BackColor = System.Drawing.SystemColors.Control;
        this._Label1_19.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label1_19.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label1_19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label1_19.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label1_19.Location = new System.Drawing.Point(8, 91);
        this._Label1_19.Name = "_Label1_19";
        this._Label1_19.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label1_19.Size = new System.Drawing.Size(69, 13);
        this._Label1_19.TabIndex = 34;
        this._Label1_19.Tag = "";
        this._Label1_19.Text = "Col./Poblcn.:";
        // 
        // _Label1_18
        // 
        this._Label1_18.AutoSize = true;
        this._Label1_18.BackColor = System.Drawing.SystemColors.Control;
        this._Label1_18.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label1_18.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label1_18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label1_18.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label1_18.Location = new System.Drawing.Point(8, 67);
        this._Label1_18.Name = "_Label1_18";
        this._Label1_18.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label1_18.Size = new System.Drawing.Size(61, 13);
        this._Label1_18.TabIndex = 33;
        this._Label1_18.Tag = "";
        this._Label1_18.Text = "Calle y No.:";
        // 
        // _Label1_4
        // 
        this._Label1_4.AutoSize = true;
        this._Label1_4.BackColor = System.Drawing.SystemColors.Control;
        this._Label1_4.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label1_4.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label1_4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label1_4.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label1_4.Location = new System.Drawing.Point(440, 67);
        this._Label1_4.Name = "_Label1_4";
        this._Label1_4.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label1_4.Size = new System.Drawing.Size(40, 13);
        this._Label1_4.TabIndex = 18;
        this._Label1_4.Tag = "";
        this._Label1_4.Text = "R.F.C.:";
        // 
        // _Label1_2
        // 
        this._Label1_2.AutoSize = true;
        this._Label1_2.BackColor = System.Drawing.SystemColors.Control;
        this._Label1_2.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label1_2.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label1_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label1_2.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label1_2.Location = new System.Drawing.Point(440, 18);
        this._Label1_2.Name = "_Label1_2";
        this._Label1_2.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label1_2.Size = new System.Drawing.Size(85, 13);
        this._Label1_2.TabIndex = 17;
        this._Label1_2.Tag = "";
        this._Label1_2.Text = "Num. de Cliente:";
        // 
        // _Label1_1
        // 
        this._Label1_1.AutoSize = true;
        this._Label1_1.BackColor = System.Drawing.SystemColors.Control;
        this._Label1_1.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label1_1.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label1_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label1_1.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label1_1.Location = new System.Drawing.Point(8, 43);
        this._Label1_1.Name = "_Label1_1";
        this._Label1_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label1_1.Size = new System.Drawing.Size(47, 13);
        this._Label1_1.TabIndex = 16;
        this._Label1_1.Tag = "";
        this._Label1_1.Text = "Nombre:";
        // 
        // _Label1_6
        // 
        this._Label1_6.AutoSize = true;
        this._Label1_6.BackColor = System.Drawing.SystemColors.Control;
        this._Label1_6.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label1_6.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label1_6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label1_6.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label1_6.Location = new System.Drawing.Point(8, 22);
        this._Label1_6.Name = "_Label1_6";
        this._Label1_6.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label1_6.Size = new System.Drawing.Size(87, 13);
        this._Label1_6.TabIndex = 15;
        this._Label1_6.Tag = "";
        this._Label1_6.Text = "Folio Preimpreso:";
        // 
        // _Label1_3
        // 
        this._Label1_3.AutoSize = true;
        this._Label1_3.BackColor = System.Drawing.SystemColors.Control;
        this._Label1_3.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label1_3.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label1_3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label1_3.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label1_3.Location = new System.Drawing.Point(232, 16);
        this._Label1_3.Name = "_Label1_3";
        this._Label1_3.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label1_3.Size = new System.Drawing.Size(68, 13);
        this._Label1_3.TabIndex = 14;
        this._Label1_3.Tag = "";
        this._Label1_3.Text = "Folio Interno:";
        // 
        // Frame1
        // 
        this.Frame1.BackColor = System.Drawing.SystemColors.Control;
        this.Frame1.Controls.Add(this.txtFolioPreimpreso);
        this.Frame1.Controls.Add(this.cboTipoTram);
        this.Frame1.Controls.Add(this._Label1_0);
        this.Frame1.Controls.Add(this.Label2);
        this.Frame1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Frame1.ForeColor = System.Drawing.SystemColors.ControlText;
        this.Frame1.Location = new System.Drawing.Point(0, 1);
        this.Frame1.Name = "Frame1";
        this.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.Frame1.Size = new System.Drawing.Size(633, 56);
        this.Frame1.TabIndex = 0;
        this.Frame1.TabStop = false;
        this.Frame1.Tag = "";
        // 
        // txtFolioPreimpreso
        // 
        this.txtFolioPreimpreso.AcceptsReturn = true;
        this.txtFolioPreimpreso.BackColor = System.Drawing.SystemColors.Window;
        this.txtFolioPreimpreso.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtFolioPreimpreso.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtFolioPreimpreso.ForeColor = System.Drawing.Color.Blue;
        this.txtFolioPreimpreso.Location = new System.Drawing.Point(464, 24);
        this.txtFolioPreimpreso.MaxLength = 16;
        this.txtFolioPreimpreso.Name = "txtFolioPreimpreso";
        this.txtFolioPreimpreso.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.txtFolioPreimpreso.Size = new System.Drawing.Size(153, 20);
        this.txtFolioPreimpreso.TabIndex = 2;
        this.txtFolioPreimpreso.Tag = "";
        this.txtFolioPreimpreso.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        this.txtFolioPreimpreso.Enter += new System.EventHandler(this.txtFolioPreimpreso_Enter);
        this.txtFolioPreimpreso.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFolioPreimpreso_KeyPress);
        // 
        // cboTipoTram
        // 
        this.cboTipoTram.BackColor = System.Drawing.SystemColors.Window;
        this.cboTipoTram.Cursor = System.Windows.Forms.Cursors.Default;
        this.cboTipoTram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cboTipoTram.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.cboTipoTram.ForeColor = System.Drawing.Color.Blue;
        this.cboTipoTram.Location = new System.Drawing.Point(104, 24);
        this.cboTipoTram.Name = "cboTipoTram";
        this.cboTipoTram.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.cboTipoTram.Size = new System.Drawing.Size(233, 21);
        this.cboTipoTram.TabIndex = 1;
        this.cboTipoTram.Tag = "";
        this.cboTipoTram.SelectedIndexChanged += new System.EventHandler(this.cboTipoTram_SelectedIndexChanged);
        this.cboTipoTram.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboTipoTram_KeyPress);
        this.cboTipoTram.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboTipoTram_KeyDown);
        // 
        // _Label1_0
        // 
        this._Label1_0.BackColor = System.Drawing.SystemColors.Control;
        this._Label1_0.Cursor = System.Windows.Forms.Cursors.Default;
        this._Label1_0.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this._Label1_0.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._Label1_0.ForeColor = System.Drawing.SystemColors.ControlText;
        this._Label1_0.Location = new System.Drawing.Point(8, 24);
        this._Label1_0.Name = "_Label1_0";
        this._Label1_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this._Label1_0.Size = new System.Drawing.Size(97, 17);
        this._Label1_0.TabIndex = 4;
        this._Label1_0.Tag = "";
        this._Label1_0.Text = "Tipo de Trámite:";
        // 
        // Label2
        // 
        this.Label2.BackColor = System.Drawing.SystemColors.Control;
        this.Label2.Cursor = System.Windows.Forms.Cursors.Default;
        this.Label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this.Label2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Label2.ForeColor = System.Drawing.SystemColors.ControlText;
        this.Label2.Location = new System.Drawing.Point(352, 24);
        this.Label2.Name = "Label2";
        this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.Label2.Size = new System.Drawing.Size(105, 17);
        this.Label2.TabIndex = 3;
        this.Label2.Tag = "";
        this.Label2.Text = "Folio Preimpreso:";
        // 
        // frmPredFolio
        // 
        this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
        this.AutoScroll = true;
        this.BackColor = System.Drawing.SystemColors.Control;
        this.ClientSize = new System.Drawing.Size(642, 433);
        this.ControlBox = false;
        this.Controls.Add(this.txtEntidad);
        this.Controls.Add(this.Frame2);
        this.Controls.Add(this.Frame5);
        this.Controls.Add(this.fraDatosSol);
        this.Controls.Add(this.MainMenu1);
        this.Controls.Add(this.Frame1);
        this.Cursor = System.Windows.Forms.Cursors.Default;
        this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.Location = new System.Drawing.Point(73, 119);
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.formHelper1.SetMoveable(this, false);
        this.Name = "frmPredFolio";
        this.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
        this.Tag = "";
        this.Text = "Predictaminación del Folio";
        this.Closed += new System.EventHandler(this.frmPredFolio_Closed);
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPredFolio_FormClosing);
        this.MainMenu1.ResumeLayout(false);
        this.MainMenu1.PerformLayout();
        this.Frame2.ResumeLayout(false);
        this.Frame5.ResumeLayout(false);
        this.Frame5.PerformLayout();
        this.fraDatosSol.ResumeLayout(false);
        this.fraDatosSol.PerformLayout();
        this.Frame3.ResumeLayout(false);
        this.Frame1.ResumeLayout(false);
        this.Frame1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.commandButtonHelper1)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

	}
	void  InitializeLabel1()
	{
			this.Label1[12] = _Label1_12;
			this.Label1[13] = _Label1_13;
			this.Label1[16] = _Label1_16;
			this.Label1[17] = _Label1_17;
			this.Label1[39] = _Label1_39;
			this.Label1[23] = _Label1_23;
			this.Label1[22] = _Label1_22;
			this.Label1[5] = _Label1_5;
			this.Label1[21] = _Label1_21;
			this.Label1[20] = _Label1_20;
			this.Label1[19] = _Label1_19;
			this.Label1[18] = _Label1_18;
			this.Label1[4] = _Label1_4;
			this.Label1[2] = _Label1_2;
			this.Label1[1] = _Label1_1;
			this.Label1[6] = _Label1_6;
			this.Label1[3] = _Label1_3;
			this.Label1[0] = _Label1_0;
	}
	void  InitializeoptSexo()
	{
			this.optSexo[1] = _optSexo_1;
			this.optSexo[0] = _optSexo_0;
	}
#endregion 

        private System.Windows.Forms.ToolStripMenuItem dummy1;
        private System.Windows.Forms.ToolStripMenuItem dummy2;
        private Artinsoft.VB6.Gui.FormHelper formHelper1;
}
}