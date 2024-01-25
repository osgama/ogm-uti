using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Masivos
{
    public partial class frmInspeccionCambios : Form
    {
        string stRemesa = null;
        string stNominaRef;

        List<string> listCambios = new List<string>();
        
        
        public frmInspeccionCambios()
        {
            InitializeComponent();
        }

        private void frmInspeccionCambios_Load(object sender, EventArgs e)
        {
            //if (!ConsultaCambios())
            //    MessageBox.Show("Error");
            //dataGridCambios.Columns.Clear();
            dataGridCambios.DataSource = null;
            dataGridCambios.Rows.Clear();
            ConsultaCambios();
        }

        private bool ConsultaCambios()
        {
            //wInspeccion wInsp = new wInspeccion();
            clsWRemesas wInsp = new clsWRemesas();
            string stMensaje = null;
            string stHeader = null;
            string stMensajeFull = null;
            string stMas = "0";
            //int iTam = 236;

            //PAGINEO
            do
            {
                //stMensajeFull = null;
                stMensaje = wInsp.ConsultaCambiosRemesa5562_27(stRemesa, stNominaRef, stHeader);
                if (stMensaje == null)
                    return false;
                //stMas = stMensaje.Substring(111, 1);
                stMas = stMensaje.Substring(112, 1);
                if (stMas == "1")
                {
                    stHeader = stMensaje.Substring(0, 176);
                }
                //stMensajeFull = stMensaje.Substring(176);
                //GUARDAR MENSAJE QUITANDOLE *** DEL FINAL
                int iPosicionAsterisco = stMensaje.IndexOf("***");
                if (iPosicionAsterisco != -1)
                    stMensajeFull = stMensaje.Substring(176, iPosicionAsterisco - 176);
                else
                    stMensajeFull = stMensaje.Substring(176);
                LlenaGridCambios(stMensajeFull);

            } while (stMas == "1");
           
            return true;
        }

        private void LlenaGridCambios(string stMensajeFull)
        {
            int iTamReg = 236, iCampos, iRegCampos, iTamRegCampos = 44;
            string stPreimpreso, stCampo, stDescripcion;
            string stMensajeTemp;

            stMensajeFull += "***";
            //stPreimpreso = stMensajeTemp.Substring(0, 16);
            //stMensajeTemp = stMensajeFull.Substring(16);
            //int iRegistros = stMensajeFull.Length / iTamReg;
            /*
            for (int iCont = 0; iCont < iRegistros; iCont++)
            {
                stPreimpreso = stMensajeFull.Substring(iCont * iTamReg + 0, 16);
                stMensajeCampos = stMensajeFull.Substring(iCont * iTamReg + 16);
                for(int iCampos = 0; iCampos < 
                if (stMensajeFull.Substring(iCont * iTamReg + 0, 3) != "***" && stMensajeFull.Substring(iCont * iTamReg + 0, 3) != "   ")
                {
                    
                    stCampo = stMensajeFull.Substring(iCont * iTamReg + 16, 4);
                    stDescripcion = stMensajeFull.Substring(iCont * iTamReg + 20, 40);
                    if (stCampo.Trim() != "" && stCampo != "0000")
                        dataGridCambios.Rows.Add(stPreimpreso, stCampo, stDescripcion);
                }
            }
            */
            int iRegistros = stMensajeFull.Length / iTamReg;
            for (int icont = 0; icont < iRegistros; icont++)
            {
                stMensajeTemp = stMensajeFull.Substring(icont * iTamReg + 0, iTamReg);
                stPreimpreso = stMensajeTemp.Substring(0, 16);
                stMensajeTemp = stMensajeTemp.Substring(16) + "***";
                iRegCampos = stMensajeTemp.Length / iTamRegCampos;

                for (iCampos = 0; iCampos < iRegCampos; iCampos++)
                {
                    if (stMensajeTemp.Substring(iCampos * iTamRegCampos + 0, 3) != "***" && stMensajeTemp.Substring(iCampos * iTamRegCampos + 0, 3) != "   ")
                    {
                        stCampo = stMensajeTemp.Substring(iCampos * iTamRegCampos + 0, 4);
                        stDescripcion = stMensajeTemp.Substring(iCampos * iTamRegCampos + 4, 40);
                        if (stCampo.Trim() != "" && stCampo != "0000")
                            dataGridCambios.Rows.Add(stPreimpreso, stCampo, stDescripcion);

                    }

                }
            }

        }

        public void getSet(string stRenglon, string stNomina)
        {
            stRemesa = stRenglon.Substring(0, 20);
            tbRemesa.Text = stRemesa;
            tbProceso.Text = stRenglon.Substring(20);
            stNominaRef = stNomina;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        protected override void OnClosing(CancelEventArgs e)
        {            
            dataGridCambios.Rows.Clear();
        }
    }

}