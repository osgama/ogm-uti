using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Masivos
{
    public partial class frmInspeccionRemesas : Form
    {
        frmInspeccionFolio Folios = new frmInspeccionFolio();
        frmInspeccionCambios Cambios = new frmInspeccionCambios();
        List<string> listRemesas = new List<string>();

        string st202_0 = "Listos para Inspeccionar";
        string st203_0 = "Listo para Enviar a ARIES";
        //string st203_5 = "Inspeccionado con límite de errores rebasado";
        string st203_5 = "Approval Rate No Cumplido";
                       
        
        string stNomina = mdlGlobales.gstrNomina.Value.ToString();
        
        public frmInspeccionRemesas()
        {
            InitializeComponent();
            btnEnviaAries.Enabled = false;
            btnCambios.Enabled = false;
            btnInspeccion.Enabled = false;
        }

        private void actualizarCampos()
        {
            dataGridRemesas.Rows.Clear();
            if (CargaRemesas())
            {

            }

        }

        private void frmInspeccionRemesas_Load(object sender, EventArgs e)
        {
            stNomina = stNomina.Trim();
            stNomina = llenaCeros(stNomina, 10);
            //dataGridRemesas.DataSource = null;

            actualizarCampos();
            //dataGridRemesas.Rows.Clear();

            //if (CargaRemesas())
            //{
            //    //btnEnviaAries.Enabled = true;
            //    //btnInspeccion.Enabled = true;
            //    //dataGridRemesas.SelectedRows = 0;
            //}

        }

        private string llenaCeros(string stString, int iTam)
        {
            while (stString.Length < iTam)
                stString = "0" + stString;

            return stString;
        }
        private bool CargaRemesas()
        {
            //wInspeccion inspeccion = new wInspeccion();
            clsWRemesas inspeccion = new clsWRemesas();
                         
            string stRemesa = null;
            string stProceso = null;
            string stStatus = null;
            string stProcesoStatus = null;
            string stDescripcion = " ";

            string stMas = null;
            string stHeader = null;
            string stMensaje = null;
            string stMensajeFull = null;

            int iTam = 26;            

            //dataGridRemesas.DataSource = null;
            dataGridRemesas.Rows.Clear();
            listRemesas.Clear();

            do
            {

                stMensaje = inspeccion.ConsultaRemesas5562_24(stNomina, stHeader);
                if (stMensaje == null || stMensaje == "")
                    return false;

                //stMas = stMensaje.Substring(111, 1);
                stMas = stMensaje.Substring(112, 1);
                if (stMas == "1")
                {
                    stHeader = stMensaje.Substring(0, 176);
                }
                //GUARDAR MENSAJE QUITANDOLE *** DEL FINAL
                //stMensajeFull += stMensaje.Substring(176);
                int iPosicionAsterisco = stMensaje.IndexOf("***");
                if (iPosicionAsterisco != -1)
                {
                    //stMensajeFull += stMensaje.Substring(176, iPosicionAsterisco);
                    stMensajeFull = stMensaje.Substring(176, iPosicionAsterisco - 176);
                    stMensajeFull = stMensajeFull.Trim();
                }
                else
                {
                    //stMensajeFull += stMensaje.Substring(176);
                    stMensajeFull = stMensaje.Substring(176);
                    stMensajeFull = stMensajeFull.Trim();
                }

                listRemesas.Add(stMensajeFull + "***");

            } while (stMas == "1");

            //stMensajeFull += "***";

            //int iRegistros = stMensajeFull.Length / iTam;
            int iRegistros = 66;
            int iFinLinea = 0;

            for (int iLineaLista = 0; iLineaLista < listRemesas.Count; iLineaLista++)
            {
                stMensajeFull = listRemesas[iLineaLista];
                

                for (int icont = 0; icont < iRegistros && iFinLinea !=1; icont++)
                {
                    if (stMensajeFull.Length <= iTam * iRegistros)
                    {
                        if (stMensajeFull.Substring(icont * iTam + 0, 3) != "***" && stMensajeFull.Substring(icont * iTam + 0, 3) != "   ")
                        {
                            stRemesa = stMensajeFull.Substring(icont * iTam + 0, 20);
                            stProceso = stMensajeFull.Substring(icont * iTam + 20, 3);
                            stStatus = stMensajeFull.Substring(icont * iTam + 23, 3);
                            stProcesoStatus = int.Parse(stProceso).ToString() + "," + int.Parse(stStatus).ToString();

                            switch (stProcesoStatus) //"201,203"
                            {
                                case "202,0": stDescripcion = st202_0; break;
                                case "203,0": stDescripcion = st203_0; break;
                                case "203,5": stDescripcion = st203_5; break;
                            }
                            dataGridRemesas.Rows.Add(stRemesa, stProcesoStatus, stDescripcion);
                        }
                        else
                            iFinLinea = 1;
                    }
                }
            }

            return true;
        }

        private void btnInspeccion_Click(object sender, EventArgs e)
        {
            
            string stInspRemesa = dataGridRemesas.Rows[dataGridRemesas.CurrentRow.Index].Cells[0].Value.ToString();
            string stProcesoStatus = dataGridRemesas.Rows[dataGridRemesas.CurrentRow.Index].Cells[1].Value.ToString();

            try
            {
                Folios.getSet(stInspRemesa, stProcesoStatus, ref dataGridRemesas, dataGridRemesas.CurrentRow.Index, ref btnInspeccion, ref btnCambios, stNomina, ref btnEnviaAries);
                //Folios.getSet(stInspRemesa, stProcesoStatus, stNomina, ref dataGridRemesas);
                Folios.MdiParent = Masivos.MDIMasivos.DefInstance;
                Folios.Show();
                //Folios.ShowDialog();  
                CargaRemesas();
            }
            catch
            {
                Folios = new frmInspeccionFolio();
                Folios.getSet(stInspRemesa, stProcesoStatus, ref dataGridRemesas, dataGridRemesas.CurrentRow.Index, ref btnInspeccion, ref btnCambios, stNomina, ref btnEnviaAries);
                //Folios.getSet(stInspRemesa, stProcesoStatus, stNomina, ref dataGridRemesas);
                Folios.MdiParent = Masivos.MDIMasivos.DefInstance;
                Folios.Show();
                //Folios.ShowDialog();
                CargaRemesas();
            }            
        }

        private void dataGridRemesas_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGridRemesas.CurrentRow != null)
            {
                // Estado de Boton Inspeccion
                //if (dataGridRemesas.Rows[dataGridRemesas.CurrentRow.Index].Cells[2].Value.ToString() == st202_0)
                if (dataGridRemesas.Rows[dataGridRemesas.CurrentRow.Index].Cells[2].Value.ToString() == st202_0 ||
                    dataGridRemesas.Rows[dataGridRemesas.CurrentRow.Index].Cells[2].Value.ToString() == st203_5)
                {
                    btnInspeccion.Enabled = true;
                }
                else
                    btnInspeccion.Enabled = false;

                // Estado de Boton Ver Cambios
                if (dataGridRemesas.Rows[dataGridRemesas.CurrentRow.Index].Cells[2].Value.ToString() == st203_0 ||
                    dataGridRemesas.Rows[dataGridRemesas.CurrentRow.Index].Cells[2].Value.ToString() == st203_5)
                {
                    btnCambios.Enabled = true;
                }
                else
                    btnCambios.Enabled = false;

                // Estado de Boton Enviar Aries
                if (dataGridRemesas.Rows[dataGridRemesas.CurrentRow.Index].Cells[2].Value.ToString() == st203_0)
                    btnEnviaAries.Enabled = true;
                else
                    btnEnviaAries.Enabled = false;
            }
        }

        
        private void btnCambios_Click(object sender, EventArgs e)
        {
            string stLineaRemesa = null;

            stLineaRemesa = dataGridRemesas.Rows[dataGridRemesas.CurrentRow.Index].Cells[0].Value.ToString() +
                dataGridRemesas.Rows[dataGridRemesas.CurrentRow.Index].Cells[1].Value.ToString();

            try
            {
                Cambios.getSet(stLineaRemesa, stNomina);
                //Cambios.Show();
                Cambios.ShowDialog();
            }
            catch
            {
                Cambios = new frmInspeccionCambios();
                Cambios.getSet(stLineaRemesa, stNomina);
                //Cambios.Show();
                Cambios.ShowDialog();
            }
        }

        private void btnEnviaAries_Click(object sender, EventArgs e)
        {            
            clsWRemesas inspeccion = new clsWRemesas();
            string stRemesaAries = dataGridRemesas.Rows[dataGridRemesas.CurrentRow.Index].Cells[0].Value.ToString();
            if (inspeccion.EnviaSolicitudAries5562_65(stRemesaAries, stNomina))
            {
                MessageBox.Show("Remesa Enviada a Aries Satisfactoriamente", "C753 ARIES - Inspeccion de Remesas", MessageBoxButtons.OK, MessageBoxIcon.Information);                
                if (CargaRemesas())
                {
                    //btnEnviaAries.Enabled = true;
                    //btnInspeccion.Enabled = true;
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            actualizarCampos();
        }
        

    }
}
