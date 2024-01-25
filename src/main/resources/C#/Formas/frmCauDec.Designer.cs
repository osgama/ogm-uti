using System; 
using VB6 = Microsoft.VisualBasic.Compatibility.VB6.Support; 

namespace Masivos
{
	partial class frmCausasDec
	{
	
#region "Upgrade Support "
		public static frmCausasDec m_vb6FormDefInstance;
		private static bool m_InitializingDefInstance;
		public static frmCausasDec DefInstance
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
		public frmCausasDec():base(){
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
	public static frmCausasDec CreateInstance()
	{
			frmCausasDec theInstance = new frmCausasDec();
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
	public  AxMSFlexGridLib.AxMSFlexGrid fgridCausasDec;
	//NOTE: The following procedure is required by the Windows Form Designer
	//It can be modified using the Windows Form Designer.
	//Do not modify it using the code editor.
	[System.Diagnostics.DebuggerStepThrough]
	 private void  InitializeComponent()
	{
        this.components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCausasDec));
        this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
        this.fgridCausasDec = new AxMSFlexGridLib.AxMSFlexGrid();
        this.formHelper1 = new Artinsoft.VB6.Gui.FormHelper(this.components);
        ((System.ComponentModel.ISupportInitialize)(this.fgridCausasDec)).BeginInit();
        this.SuspendLayout();
        // 
        // fgridCausasDec
        // 
        this.fgridCausasDec.Location = new System.Drawing.Point(0, 0);
        this.fgridCausasDec.Name = "fgridCausasDec";
        this.fgridCausasDec.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("fgridCausasDec.OcxState")));
        this.fgridCausasDec.Size = new System.Drawing.Size(353, 276);
        this.fgridCausasDec.TabIndex = 0;
        this.fgridCausasDec.Tag = "";
        this.fgridCausasDec.KeyDownEvent += new AxMSFlexGridLib.DMSFlexGridEvents_KeyDownEventHandler(this.fgridCausasDec_KeyDownEvent);
        this.fgridCausasDec.DblClick += new System.EventHandler(this.fgridCausasDec_DblClick);
        this.fgridCausasDec.ClickEvent += new System.EventHandler(this.fgridCausasDec_ClickEvent);
        // 
        // frmCausasDec
        // 
        this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
        this.AutoScroll = true;
        this.BackColor = System.Drawing.SystemColors.Control;
        this.ClientSize = new System.Drawing.Size(354, 277);
        this.ControlBox = false;
        this.Controls.Add(this.fgridCausasDec);
        this.Cursor = System.Windows.Forms.Cursors.Default;
        this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.KeyPreview = true;
        this.Location = new System.Drawing.Point(144, 227);
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.formHelper1.SetMoveable(this, false);
        this.Name = "frmCausasDec";
        this.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
        this.Tag = "";
        this.Text = "Causas de Declinación";
        this.Deactivate += new System.EventHandler(this.frmCausasDec_Deactivate);
        this.Closed += new System.EventHandler(this.frmCausasDec_Closed);
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCausasDec_FormClosing);
        this.Load += new System.EventHandler(this.frmCausasDec_Load);
        ((System.ComponentModel.ISupportInitialize)(this.fgridCausasDec)).EndInit();
        this.ResumeLayout(false);

	}
#endregion 

        private Artinsoft.VB6.Gui.FormHelper formHelper1;
}
}