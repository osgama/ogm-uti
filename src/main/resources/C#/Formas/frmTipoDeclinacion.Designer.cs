using System; 
using VB6 = Microsoft.VisualBasic.Compatibility.VB6.Support; 

namespace Masivos
{
	partial class frmTipoDeclinacion
	{
	
#region "Upgrade Support "
        public static frmTipoDeclinacion m_vb6FormDefInstance;
		private static bool m_InitializingDefInstance;
		public static frmTipoDeclinacion DefInstance
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
		public frmTipoDeclinacion():base(){
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
			//This form is an MDI child.
			//This code simulates the VB6 
			// functionality of automatically
			// loading and showing an MDI
			// child's parent.
			this.MdiParent = Masivos.MDIMasivos.DefInstance;
			Masivos.MDIMasivos.DefInstance.Show();
		}
	public static frmTipoDeclinacion CreateInstance()
	{
			frmTipoDeclinacion theInstance = new frmTipoDeclinacion();
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
	public  System.Windows.Forms.ToolStripMenuItem mnuDeclinaToda;
	public  System.Windows.Forms.ToolStripMenuItem mnuDeclinaFolios;
	public  System.Windows.Forms.ToolStripSeparator mnuSep1;
	public  System.Windows.Forms.ToolStripMenuItem mnuAceptar;
	public  System.Windows.Forms.ToolStripSeparator mnuSep2;
	public  System.Windows.Forms.ToolStripMenuItem mnuCancela;
	public  System.Windows.Forms.ToolStripMenuItem mnuTipoDeclina;
	public  System.Windows.Forms.MenuStrip MainMenu1;
	public  System.Windows.Forms.Button cmdCancelar;
	public  System.Windows.Forms.Button cmdAceptar;
	public  System.Windows.Forms.RadioButton optDeclinaFolios;
	public  System.Windows.Forms.RadioButton optDeclinaRemesa;
	public  System.Windows.Forms.GroupBox Frame1;
	private Artinsoft.VB6.Gui.CommandButtonHelper commandButtonHelper1;
	//NOTE: The following procedure is required by the Windows Form Designer
	//It can be modified using the Windows Form Designer.
	//Do not modify it using the code editor.
	[System.Diagnostics.DebuggerStepThrough]
	 private void  InitializeComponent()
	{
        this.components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTipoDeclinacion));
        this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
        this.cmdCancelar = new System.Windows.Forms.Button();
        this.cmdAceptar = new System.Windows.Forms.Button();
        this.MainMenu1 = new System.Windows.Forms.MenuStrip();
        this.mnuTipoDeclina = new System.Windows.Forms.ToolStripMenuItem();
        this.mnuDeclinaToda = new System.Windows.Forms.ToolStripMenuItem();
        this.mnuDeclinaFolios = new System.Windows.Forms.ToolStripMenuItem();
        this.mnuSep1 = new System.Windows.Forms.ToolStripSeparator();
        this.mnuAceptar = new System.Windows.Forms.ToolStripMenuItem();
        this.mnuSep2 = new System.Windows.Forms.ToolStripSeparator();
        this.mnuCancela = new System.Windows.Forms.ToolStripMenuItem();
        this.procesamientoMasivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.predictaminaciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.firmaAlS041ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.Frame1 = new System.Windows.Forms.GroupBox();
        this.optDeclinaFolios = new System.Windows.Forms.RadioButton();
        this.optDeclinaRemesa = new System.Windows.Forms.RadioButton();
        this.commandButtonHelper1 = new Artinsoft.VB6.Gui.CommandButtonHelper(this.components);
        this.formHelper1 = new Artinsoft.VB6.Gui.FormHelper(this.components);
        this.MainMenu1.SuspendLayout();
        this.Frame1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.commandButtonHelper1)).BeginInit();
        this.SuspendLayout();
        // 
        // cmdCancelar
        // 
        this.cmdCancelar.BackColor = System.Drawing.SystemColors.Control;
        this.commandButtonHelper1.SetCorrectEventsBehavior(this.cmdCancelar, true);
        this.cmdCancelar.Cursor = System.Windows.Forms.Cursors.Default;
        this.cmdCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.commandButtonHelper1.SetDisabledPicture(this.cmdCancelar, null);
        this.commandButtonHelper1.SetDownPicture(this.cmdCancelar, null);
        this.cmdCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.cmdCancelar.ForeColor = System.Drawing.SystemColors.ControlText;
        this.cmdCancelar.Image = ((System.Drawing.Image)(resources.GetObject("cmdCancelar.Image")));
        this.cmdCancelar.Location = new System.Drawing.Point(276, 60);
        this.commandButtonHelper1.SetMaskColor(this.cmdCancelar, System.Drawing.Color.Silver);
        this.cmdCancelar.Name = "cmdCancelar";
        this.cmdCancelar.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.cmdCancelar.Size = new System.Drawing.Size(81, 33);
        this.commandButtonHelper1.SetStyle(this.cmdCancelar, 1);
        this.cmdCancelar.TabIndex = 4;
        this.cmdCancelar.Tag = "";
        this.cmdCancelar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
        this.cmdCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
        this.ToolTip1.SetToolTip(this.cmdCancelar, "Cancelar");
        this.cmdCancelar.UseVisualStyleBackColor = false;
        this.cmdCancelar.Click += new System.EventHandler(this.cmdCancelar_Click);
        this.cmdCancelar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmdCancelar_KeyPress);
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
        this.cmdAceptar.Location = new System.Drawing.Point(276, 18);
        this.commandButtonHelper1.SetMaskColor(this.cmdAceptar, System.Drawing.Color.Silver);
        this.cmdAceptar.Name = "cmdAceptar";
        this.cmdAceptar.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.cmdAceptar.Size = new System.Drawing.Size(81, 33);
        this.commandButtonHelper1.SetStyle(this.cmdAceptar, 1);
        this.cmdAceptar.TabIndex = 3;
        this.cmdAceptar.Tag = "";
        this.cmdAceptar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
        this.cmdAceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
        this.ToolTip1.SetToolTip(this.cmdAceptar, "Aceptar");
        this.cmdAceptar.UseVisualStyleBackColor = false;
        this.cmdAceptar.Click += new System.EventHandler(this.cmdAceptar_Click);
        this.cmdAceptar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmdAceptar_KeyPress);
        // 
        // MainMenu1
        // 
        this.MainMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTipoDeclina,
            this.procesamientoMasivoToolStripMenuItem,
            this.predictaminaciónToolStripMenuItem,
            this.firmaAlS041ToolStripMenuItem});
        this.MainMenu1.Location = new System.Drawing.Point(0, 0);
        this.MainMenu1.Name = "MainMenu1";
        this.MainMenu1.Size = new System.Drawing.Size(492, 24);
        this.MainMenu1.TabIndex = 5;
        this.MainMenu1.Visible = false;
        // 
        // mnuTipoDeclina
        // 
        this.mnuTipoDeclina.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDeclinaToda,
            this.mnuDeclinaFolios,
            this.mnuSep1,
            this.mnuAceptar,
            this.mnuSep2,
            this.mnuCancela});
        this.mnuTipoDeclina.Name = "mnuTipoDeclina";
        this.mnuTipoDeclina.Size = new System.Drawing.Size(110, 20);
        this.mnuTipoDeclina.Tag = "";
        this.mnuTipoDeclina.Text = "Tipo de Declinación";
        // 
        // mnuDeclinaToda
        // 
        this.mnuDeclinaToda.Checked = true;
        this.mnuDeclinaToda.CheckState = System.Windows.Forms.CheckState.Checked;
        this.mnuDeclinaToda.Name = "mnuDeclinaToda";
        this.mnuDeclinaToda.Size = new System.Drawing.Size(227, 22);
        this.mnuDeclinaToda.Tag = "";
        this.mnuDeclinaToda.Text = "Declinación de toda la remesa";
        this.mnuDeclinaToda.Click += new System.EventHandler(this.mnuDeclinaToda_Click);
        // 
        // mnuDeclinaFolios
        // 
        this.mnuDeclinaFolios.Name = "mnuDeclinaFolios";
        this.mnuDeclinaFolios.Size = new System.Drawing.Size(227, 22);
        this.mnuDeclinaFolios.Tag = "";
        this.mnuDeclinaFolios.Text = "Declinación por Folios";
        this.mnuDeclinaFolios.Click += new System.EventHandler(this.mnuDeclinaFolios_Click);
        // 
        // mnuSep1
        // 
        this.mnuSep1.Name = "mnuSep1";
        this.mnuSep1.Size = new System.Drawing.Size(224, 6);
        this.mnuSep1.Tag = "";
        // 
        // mnuAceptar
        // 
        this.mnuAceptar.Name = "mnuAceptar";
        this.mnuAceptar.Size = new System.Drawing.Size(227, 22);
        this.mnuAceptar.Tag = "";
        this.mnuAceptar.Text = "Aceptar";
        this.mnuAceptar.Click += new System.EventHandler(this.mnuAceptar_Click);
        // 
        // mnuSep2
        // 
        this.mnuSep2.Name = "mnuSep2";
        this.mnuSep2.Size = new System.Drawing.Size(224, 6);
        this.mnuSep2.Tag = "";
        // 
        // mnuCancela
        // 
        this.mnuCancela.Name = "mnuCancela";
        this.mnuCancela.Size = new System.Drawing.Size(227, 22);
        this.mnuCancela.Tag = "";
        this.mnuCancela.Text = "Cancelar";
        this.mnuCancela.Click += new System.EventHandler(this.mnuCancela_Click);
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
        // Frame1
        // 
        this.Frame1.BackColor = System.Drawing.SystemColors.Control;
        this.Frame1.Controls.Add(this.optDeclinaFolios);
        this.Frame1.Controls.Add(this.optDeclinaRemesa);
        this.Frame1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Frame1.ForeColor = System.Drawing.SystemColors.ControlText;
        this.Frame1.Location = new System.Drawing.Point(12, 12);
        this.Frame1.Name = "Frame1";
        this.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.Frame1.Size = new System.Drawing.Size(257, 90);
        this.Frame1.TabIndex = 2;
        this.Frame1.TabStop = false;
        this.Frame1.Tag = "";
        this.Frame1.Text = "Seleccionar el tipo de declinación:";
        // 
        // optDeclinaFolios
        // 
        this.optDeclinaFolios.BackColor = System.Drawing.SystemColors.Control;
        this.optDeclinaFolios.Cursor = System.Windows.Forms.Cursors.Default;
        this.optDeclinaFolios.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.optDeclinaFolios.ForeColor = System.Drawing.SystemColors.ControlText;
        this.optDeclinaFolios.Location = new System.Drawing.Point(16, 48);
        this.optDeclinaFolios.Name = "optDeclinaFolios";
        this.optDeclinaFolios.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.optDeclinaFolios.Size = new System.Drawing.Size(161, 33);
        this.optDeclinaFolios.TabIndex = 1;
        this.optDeclinaFolios.TabStop = true;
        this.optDeclinaFolios.Tag = "";
        this.optDeclinaFolios.Text = "Declinación por Folios";
        this.optDeclinaFolios.UseVisualStyleBackColor = false;
        // 
        // optDeclinaRemesa
        // 
        this.optDeclinaRemesa.BackColor = System.Drawing.SystemColors.Control;
        this.optDeclinaRemesa.Checked = true;
        this.optDeclinaRemesa.Cursor = System.Windows.Forms.Cursors.Default;
        this.optDeclinaRemesa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.optDeclinaRemesa.ForeColor = System.Drawing.SystemColors.ControlText;
        this.optDeclinaRemesa.Location = new System.Drawing.Point(16, 24);
        this.optDeclinaRemesa.Name = "optDeclinaRemesa";
        this.optDeclinaRemesa.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.optDeclinaRemesa.Size = new System.Drawing.Size(193, 25);
        this.optDeclinaRemesa.TabIndex = 0;
        this.optDeclinaRemesa.TabStop = true;
        this.optDeclinaRemesa.Tag = "";
        this.optDeclinaRemesa.Text = "Declinación de Toda la Remesa";
        this.optDeclinaRemesa.UseVisualStyleBackColor = false;
        this.optDeclinaRemesa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.optDeclinaRemesa_KeyPress);
        // 
        // frmTipoDeclinacion
        // 
        this.AcceptButton = this.cmdAceptar;
        this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
        this.AutoScroll = true;
        this.BackColor = System.Drawing.SystemColors.Control;
        this.CancelButton = this.cmdCancelar;
        this.ClientSize = new System.Drawing.Size(369, 114);
        this.ControlBox = false;
        this.Controls.Add(this.cmdCancelar);
        this.Controls.Add(this.cmdAceptar);
        this.Controls.Add(this.Frame1);
        this.Controls.Add(this.MainMenu1);
        this.Cursor = System.Windows.Forms.Cursors.Default;
        this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.Location = new System.Drawing.Point(3, 41);
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.formHelper1.SetMoveable(this, false);
        this.Name = "frmTipoDeclinacion";
        this.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
        this.Tag = "";
        this.Text = "Tipo de Declinación";
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTipoDeclinacion_FormClosing);
        this.MainMenu1.ResumeLayout(false);
        this.MainMenu1.PerformLayout();
        this.Frame1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.commandButtonHelper1)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

	}
#endregion 

        private System.Windows.Forms.ToolStripMenuItem procesamientoMasivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem predictaminaciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem firmaAlS041ToolStripMenuItem;
        private Artinsoft.VB6.Gui.FormHelper formHelper1;
}
}