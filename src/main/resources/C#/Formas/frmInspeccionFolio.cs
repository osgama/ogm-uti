using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Masivos
{
    public partial class frmInspeccionFolio : Form
    {
        
        string stRemesaRef;
        string stProcesoStatusRef;
        string stNominaRef;
        DataGridView dataGridRemesaRef = new DataGridView();
        int iLineaRef;
        Button btnInspeccionRef = new Button();
        Button btnVerCambiosRef = new Button();
        Button btnEnviaAriesRef = new Button();
        List<string> listFolios = new List<string>();
        string stEnInspeccion = "En Inspección";
        string stInspeccionadoSinMuestra = "Inspeccionado Sin Terminar Muestra";            

        //int iNumFolios = 0;
        
        public frmInspeccionFolio()
        {
            InitializeComponent();
        }

        
        public void getSet(string stRemesa, string stProcesoStatus, ref DataGridView dataGridRemesa, int iLINEA,
            ref Button btnInspeccion, ref Button btnVerCambios, string stNomina, ref Button btnEnviaAries)
        {
            stRemesaRef = stRemesa;
            stProcesoStatusRef = stProcesoStatus;
            dataGridRemesaRef = dataGridRemesa;
            iLineaRef = iLINEA;
            btnInspeccionRef = btnInspeccion;
            btnVerCambiosRef = btnVerCambios;
            stNominaRef = stNomina;
            btnEnviaAriesRef = btnEnviaAries;
        }


        private void frmInspeccionFolio_Load(object sender, EventArgs e)
        {
            btnTerminarInspeccion.Enabled = false;

            tbRemesa.Text = stRemesaRef;
            tbProcesoStatus.Text = stProcesoStatusRef;

            dataGridFolios.Rows.Clear();
            inspeccionFolio_Load_2();
                        
            if (dataGridFolios.RowCount == 0)
            {
                btnInspeccionFolio.Enabled = false;
                btnTerminarInspeccion.Enabled = false;                
            }
        }

        private void inspeccionFolio_Load_2()
        {
            CargaFoliosRemesas();

            int iNumFolios = 0;
            for (int iRows = 0; iRows < dataGridFolios.RowCount; iRows++)
            {                
                if (dataGridFolios.Rows[iRows].Cells[2].Value.ToString() == stEnInspeccion)
                    iNumFolios++;
            }
            if (iNumFolios == 0 && dataGridFolios.RowCount > 0)
                btnTerminarInspeccion.Enabled = true;
        }

        private bool CargaFoliosRemesas()
        {            
            clsWRemesas wInsp = new clsWRemesas();

            string stMensaje = null;
            string stMas = null;
            string stMensajeFull = null; 
            
            int icont = new int();
            int iTam = 57;
            int iFinLinea = 0;
            string stConsInspeccion = null;
            
            string stFolio = null;
            string stNombre = null;            
            string stIndInspeccion = null;
            string stHeader = null;

            dataGridFolios.Rows.Clear();
            listFolios.Clear();

            do
            {
                stMensaje = wInsp.ConsultaFolioRemesas5562_25(stNominaRef, stRemesaRef, stHeader);
                if (stMensaje == null)
                {
                    btnInspeccionFolio.Enabled = false;
                    return false;
                }
                //stMas = stMensaje.Substring(111, 1);
                stMas = stMensaje.Substring(112, 1);
                if (stMas == "1")
                    stHeader = stMensaje.Substring(0, 176);

                stConsInspeccion = stMensaje.Substring(176, 4);
                //GUARDAR MENSAJE QUITANDOLE *** DEL FINAL
                //stMensajeFull += stMensaje.Substring(180);
                int iPosicionAsterisco = stMensaje.IndexOf("***");
                if (iPosicionAsterisco != -1)                
                    stMensajeFull = stMensaje.Substring(180, iPosicionAsterisco - 180);
                                    
                else                
                    stMensajeFull = stMensaje.Substring(180);

                stMensajeFull = stMensajeFull.Trim();
                listFolios.Add(stMensajeFull + "***");
            } while (stMas == "1");

            //stMensajeFull += "***";
            tbConsInspeccion.Text = stConsInspeccion;

            //int iRegistros = stMensajeFull.Length / iTam;
            int iRegistros = 30;

            for (int iLineaLista = 0; iLineaLista < listFolios.Count; iLineaLista++)
            {
                stMensajeFull = listFolios[iLineaLista];

                for (icont = 0; icont < iRegistros && iFinLinea != 1; icont++)
                {
                    if (stMensajeFull.Substring(icont * iTam + 0, 3) != "***" && stMensajeFull.Substring(icont * iTam + 0, 3) != "   ")
                    {
                        stFolio = stMensajeFull.Substring(icont * iTam + 0, 16);
                        stNombre = stMensajeFull.Substring(icont * iTam + 16, 40);
                        stIndInspeccion = stMensajeFull.Substring(icont * iTam + 56, 1);

                        stNombre = stNombre.Trim();
                        switch (stIndInspeccion)
                        {
                            //case "0": stIndInspeccion = "En Inspección"; break;
                            //case "1": stIndInspeccion = "Inspeccionado Sin Terminar Muestra"; break;
                            case "0": stIndInspeccion = stEnInspeccion; break;
                            case "1": stIndInspeccion = stInspeccionadoSinMuestra; break;
                        }
                        dataGridFolios.Rows.Add(stFolio, stNombre, stIndInspeccion);
                    }
                    else
                        iFinLinea = 1;
                }
            }
            return true;            
        }

        private void btnInspeccionFolio_Click(object sender, EventArgs e)
        {
            frmInspeccionCampos Campos = new frmInspeccionCampos();
            
            string stPreimpreso = dataGridFolios.Rows[dataGridFolios.CurrentRow.Index].Cells[0].Value.ToString();            
            try
            {
                Campos.getSet(stRemesaRef, stPreimpreso, stProcesoStatusRef, ref dataGridFolios, dataGridFolios.CurrentRow.Index, ref btnInspeccionFolio, stNominaRef);
                Campos.MdiParent = Masivos.MDIMasivos.DefInstance;
                Campos.Show();
                //Campos.ShowDialog();
            }
            catch
            {
                Campos = new frmInspeccionCampos();
                Campos.getSet(stRemesaRef, stPreimpreso, stProcesoStatusRef, ref dataGridFolios, dataGridFolios.CurrentRow.Index, ref btnInspeccionFolio, stNominaRef);              
                //Campos.ShowDialog();
                Campos.MdiParent = Masivos.MDIMasivos.DefInstance;
                Campos.Show();
            }

            inspeccionFolio_Load_2();           
        }

        private void dataGridFolios_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGridFolios.CurrentRow != null)
            {
                //if (dataGridFolios.Rows[dataGridFolios.CurrentRow.Index].Cells[2].Value.ToString() == "Inspeccionado Sin Terminar Muestra")
                if (dataGridFolios.Rows[dataGridFolios.CurrentRow.Index].Cells[2].Value.ToString() == stInspeccionadoSinMuestra)
                    btnInspeccionFolio.Enabled = false;
                else
                    btnInspeccionFolio.Enabled = true;
            }
        }

        private void btnTerminarInspeccion_Click(object sender, EventArgs e)
        {
            TerminarInspeccion();
            this.Close();
        }

        private void TerminarInspeccion()
        {
            clsWRemesas wInsp = new clsWRemesas();

            string stResultado = wInsp.RegistroFinInspeccion5562_64(stRemesaRef, stNominaRef);

            if (stResultado != null)
            {
                MessageBox.Show(stResultado, "S753 ARIES - Inspección de Folios", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                //MODIF MAP 10/08/2010
                //dataGridRemesaRef.Rows.Clear();

                // MODIF MAP 10/08/2010
                for (int iCont = 0; iCont < dataGridRemesaRef.RowCount; iCont++)
                {
                    if (dataGridRemesaRef.Rows[iCont].Cells[0].Value.ToString() == stRemesaRef)
                    {
                        //dataGridRemesaRef.Rows[iCont].Cells[1].Value = "203,0";
                        //dataGridRemesaRef.Rows[iCont].Cells[2].Value = "Listo para Enviar a ARIES";
                        ////ORIGINAL
                        //btnInspeccionRef.Enabled = false;
                        //btnVerCambiosRef.Enabled = true;
                        //btnEnviaAriesRef.Enabled = true;                        
                        //iCont = dataGridRemesaRef.RowCount;

                        // MODIF MAP 01/10/2010 INICIO
                        if (iCont != 0 && dataGridRemesaRef.RowCount > 1 && dataGridRemesaRef.Rows[iCont].Cells[1].Value != "203,0")
                        {
                            dataGridRemesaRef.Rows[iCont].Cells[1].Value = "203,0";
                            dataGridRemesaRef.Rows[iCont].Cells[2].Value = "Listo para Enviar a ARIES";
                        }
                        else
                        {
                            dataGridRemesaRef.Rows[iCont].Cells[1].Value = "203,0";
                            dataGridRemesaRef.Rows[iCont].Cells[2].Value = "Listo para Enviar a ARIES";
                            btnInspeccionRef.Enabled = false;
                            btnVerCambiosRef.Enabled = true;
                            btnEnviaAriesRef.Enabled = true;
                        }
                        iCont = dataGridRemesaRef.RowCount;
                        // MODIF MAP 01/10/2010 FIN
                        
                    }
                }
                // MODIF MAP 06/08/2010
                //if (dataGridRemesaRef.Rows[iLineaRef].Cells[0].Value == stRemesaRef)
                //{
                //    dataGridRemesaRef.Rows[iLineaRef].Cells[1].Value = "203,0";
                //    dataGridRemesaRef.Rows[iLineaRef].Cells[2].Value = "Listo para Enviar a ARIES";
                //    btnInspeccionRef.Enabled = false;
                //    btnVerCambiosRef.Enabled = true;
                //    btnEnviaAriesRef.Enabled = true;
                //}
            }
            //this.Close();
        }

        private void dataGridFolios_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {            
            int iNumFolios = 0;
            for (int iRows = 0; iRows < dataGridFolios.RowCount; iRows++)
            {
                if (dataGridFolios.Rows[iRows].Cells[2].Value.ToString() == stEnInspeccion)
                    iNumFolios++;
            }
            if (iNumFolios == 0 && dataGridFolios.RowCount > 0)
                btnTerminarInspeccion.Enabled = true;
        }
    }
}