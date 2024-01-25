using System; 
using VB6 = Microsoft.VisualBasic.Compatibility.VB6.Support; 

namespace Masivos
{
	partial class frmProceso
	{
	
#region "Upgrade Support "
        public static frmProceso m_vb6FormDefInstance;
		private static bool m_InitializingDefInstance;
		public static frmProceso DefInstance
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
		public frmProceso():base(){
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
		}
	public static frmProceso CreateInstance()
	{
			frmProceso theInstance = new frmProceso();
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
	public  AxMSComCtl2.AxAnimation aniTransfer;
	public  System.Windows.Forms.GroupBox fraTransfer;
	//NOTE: The following procedure is required by the Windows Form Designer
	//It can be modified using the Windows Form Designer.
	//Do not modify it using the code editor.
	[System.Diagnostics.DebuggerStepThrough]
	 private void  InitializeComponent()
	{
        this.components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProceso));
        this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
        this.aniTransfer = new AxMSComCtl2.AxAnimation();
        this.fraTransfer = new System.Windows.Forms.GroupBox();
        this.formHelper1 = new Artinsoft.VB6.Gui.FormHelper(this.components);
        ((System.ComponentModel.ISupportInitialize)(this.aniTransfer)).BeginInit();
        this.SuspendLayout();
        // 
        // aniTransfer
        // 
        this.aniTransfer.Location = new System.Drawing.Point(24, 32);
        this.aniTransfer.Name = "aniTransfer";
        this.aniTransfer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("aniTransfer.OcxState")));
        this.aniTransfer.Size = new System.Drawing.Size(297, 49);
        this.aniTransfer.TabIndex = 0;
        this.aniTransfer.Tag = "";
        // 
        // fraTransfer
        // 
        this.fraTransfer.BackColor = System.Drawing.SystemColors.Control;
        this.fraTransfer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.fraTransfer.ForeColor = System.Drawing.SystemColors.ControlText;
        this.fraTransfer.Location = new System.Drawing.Point(8, 8);
        this.fraTransfer.Name = "fraTransfer";
        this.fraTransfer.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.fraTransfer.Size = new System.Drawing.Size(329, 89);
        this.fraTransfer.TabIndex = 1;
        this.fraTransfer.TabStop = false;
        this.fraTransfer.Tag = "";
        // 
        // frmProceso
        // 
        this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
        this.AutoScroll = true;
        this.BackColor = System.Drawing.SystemColors.Control;
        this.ClientSize = new System.Drawing.Size(348, 112);
        this.ControlBox = false;
        this.Controls.Add(this.aniTransfer);
        this.Controls.Add(this.fraTransfer);
        this.Cursor = System.Windows.Forms.Cursors.Default;
        this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.Location = new System.Drawing.Point(234, 240);
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.formHelper1.SetMoveable(this, false);
        this.Name = "frmProceso";
        this.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
        this.Tag = "";
        this.Text = "Transacción en proceso........";
        this.Closed += new System.EventHandler(this.frmProceso_Closed);
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmProceso_FormClosing);
        ((System.ComponentModel.ISupportInitialize)(this.aniTransfer)).EndInit();
        this.ResumeLayout(false);

	}
#endregion 

        private Artinsoft.VB6.Gui.FormHelper formHelper1;
}
}