using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Masivos
{
    public partial class frmConsultaRemesas : Form
    {
        ComboBox cbPuente = new ComboBox(); 
        ComboBox cbDescAtrib = new ComboBox();

        public frmConsultaRemesas()
        {
            InitializeComponent();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            tbRespuestas.Clear();
        }

        private void frmConsultaRemesas_Load(object sender, EventArgs e)
        {
            lstConsultas.Items.Clear();
            llenaCombo();
            llenaLista();
            lstOpciones.SelectedIndex = 0;
            lstConsultas.SelectedIndex = 0;
        }

        private void llenaLista()
        {
            lstConsultas.Items.Clear();
            for (int iCont = 0; iCont < cbPuente.Items.Count; iCont++)
            {
                lstConsultas.Items.Add(cbPuente.Items[iCont].ToString().ToUpper());
            }

        }

        private void llenaCombo()
        {
            string stCatalogo = "270";
            string stAtributos;
            int iContador = 0;
            string tempRefParam1, tempRefParam2, tempRefParam3, tempRefParam4, tempRefParam5, tempRefParam6;


            cbPuente.Items.Clear();
            tempRefParam1 = stCatalogo; // OPCIONES DE CONSULTA
            tempRefParam2 = "";
            tempRefParam3 = "";
            tempRefParam4 = "";
            tempRefParam5 = "";
            tempRefParam6 = "E";

            //mdlGlobales.gCatalogos.setLongClave = 2;
            mdlComunica.OleCatalogos.setLongClave = 2;
            Catalogos.clsCatalogos.enmAlineaciones tempRefParamCatenm = Catalogos.clsCatalogos.enmAlineaciones.Izquierda;
            //mdlGlobales.gCatalogos.setAlineacionClave = tempRefParamCatenm;
            mdlComunica.OleCatalogos.setAlineacionClave = tempRefParamCatenm;
            mdlComunica.OleCatalogos.LlenaCombo(ref cbPuente, ref tempRefParam1, ref tempRefParam2, ref tempRefParam3, ref tempRefParam4, ref tempRefParam5, ref tempRefParam6);

            mdlComunica.OleCatalogos.AbreCatalogo(ref stCatalogo);
            mdlComunica.OleCatalogos.MoveFirst();
            while (!mdlComunica.OleCatalogos.EOF_Renamed())
            {
                stAtributos = mdlComunica.OleCatalogos.getAtributos.Trim();
                cbDescAtrib.Items.Add(stAtributos);
                iContador++;
                mdlComunica.OleCatalogos.MoveNext();
            }
            mdlComunica.OleCatalogos.CierraCatalogo();

            //for (int iCont = 0; iCont < 7; iCont++)
            //{
            //    cbPuente.Items.Add("CONSULTA " + (iCont + 1).ToString());
            //}
            //cbPuente.Items.Add("REMESA");
            
            //cbDescAtrib.Items.Add("5");
            //cbDescAtrib.Items.Add("V");
            //cbDescAtrib.Items.Add("A");
            //cbDescAtrib.Items.Add("F");
            //cbDescAtrib.Items.Add("D");
            //cbDescAtrib.Items.Add("B");
            //cbDescAtrib.Items.Add("C");
            //cbDescAtrib.Items.Add("R");
            //cbDescAtrib.Items.Add("N");
            //mdlGlobales.gCatalogos.setLongClave = 0;
            mdlComunica.OleCatalogos.setLongClave = 0;
        }

        private void lstOpciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            string stOpcion= "";
            if(lstConsultas.SelectedIndex != -1)
                stOpcion = lstConsultas.SelectedItem.ToString();

            if (lstOpciones.SelectedIndex == 0)
            {
                lstConsultas.Visible = true;
                lbArchivo.Visible = false;
                tbArchivo.Visible = false;

                if (stOpcion.IndexOf("REMESA") > -1)
                {
                    lbRemesa.Visible = true;
                    tbRemesa.Visible = true;
                    lbFechaRemesa.Visible = true;
                    dtpFechaRemesa.Visible = true;
                }
            }
            else
            {
                lstConsultas.Visible = false;
                lbArchivo.Visible = true;
                tbArchivo.Visible = true;

                lbRemesa.Visible = false;
                tbRemesa.Visible = false;
                lbFechaRemesa.Visible = false;
                dtpFechaRemesa.Visible = false;
            }
        }

        private void lstConsultas_SelectedIndexChanged(object sender, EventArgs e)
        {
            string stOpcion = lstConsultas.SelectedItem.ToString();

            if (stOpcion.IndexOf("REMESA") > -1)
            {
                lbRemesa.Visible = true;
                tbRemesa.Visible = true;
                lbFechaRemesa.Visible = true;
                dtpFechaRemesa.Visible = true;

                tbRemesa.Text = "";                
                dtpFechaRemesa.Value = DateTime.Now;
                
            }
            else
            {
                lbRemesa.Visible = false;
                tbRemesa.Visible = false;
                lbFechaRemesa.Visible = false;
                dtpFechaRemesa.Visible = false;
            }
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string stOpcion;
            string stRespuesta = null;
            //string stRespuestaFull = null;
            string stRemesa = "0000000000000000000000";
            string stFecha = DateTime.Now.ToString("yyyyMMdd");
            string stArchivo = "        ";

            clsWRemesas consulta = new clsWRemesas();

            switch (lstOpciones.SelectedIndex)
            {
                case 0:
                    stOpcion = cbDescAtrib.Items[lstConsultas.SelectedIndex].ToString();
                    if (stOpcion == "R")
                    {
                        stRemesa = tbRemesa.Text;
                        stRemesa = stRemesa.PadLeft(22, '0');
                        stFecha = dtpFechaRemesa.Value.ToString("yyyyMMdd");
                    }
                    else
                    {
                        stRemesa = "0000000000000000000000";
                        stFecha = DateTime.Now.ToString("yyyyMMdd");
                        stArchivo = "        ";
                    }
                    stRespuesta = consulta.EjecutarConsulta5562_28(stOpcion, stRemesa, stArchivo, stFecha);
                    break;
                case 1:
                    if (tbArchivo.Text.Trim() == "")
                    {
                        MessageBox.Show("Se debe introducir nombre de archivo a retransmitir", "C753 ARIES - CONSULTA REMESA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        stOpcion = "N";
                        stArchivo = tbArchivo.Text;
                        stArchivo = stArchivo.PadRight(8);
                        stRemesa = "0000000000000000000000";
                        stFecha = DateTime.Now.ToString("yyyyMMdd");
                        stRespuesta = consulta.EjecutarConsulta5562_28(stOpcion, stRemesa, stArchivo, stFecha);
                    }
                    break;

            }
            if (stRespuesta != null)
            {
                //stRespuestaFull += stRespuesta.Substring(50, 52).Trim() + "\n" +
                //    "Nombre Archivo Tarjeta: " + stRespuesta.Substring(176, 8) + "\n" +
                //    "Nombre Archivo Retail: " + stRespuesta.Substring(184, 8) + "\n";

                stRespuesta = stRespuesta.Substring(50, 52) + " - " + stRespuesta.Substring(176, 16);

                //Convert.ToChar(13) + Convert.ToChar(10)
                tbRespuestas.Text = lstConsultas.SelectedItem.ToString() + " - " + stRespuesta.Substring(4, 48) + Convert.ToChar(13) + Convert.ToChar(10) +
                    "Nombre Archivo Tarjeta: " + stRespuesta.Substring(55, 8) + Convert.ToChar(13) + Convert.ToChar(10) +
                    "Nombre Archivo Retail: " + stRespuesta.Substring(63, 8) + Convert.ToChar(13) + Convert.ToChar(10) + Convert.ToChar(13) + Convert.ToChar(10) +
                    tbRespuestas.Text;
            }

            Cursor.Current = Cursors.Default;
        }

        private void tbRemesa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Delete || e.KeyChar == (char)Keys.Left ||
                                e.KeyChar == (char)Keys.Right || IsNumber(e.KeyChar.ToString()))
            {
                if (e.KeyChar == '.' || e.KeyChar == '%' || e.KeyChar == '&' || e.KeyChar == '-' ||
                    e.KeyChar == '*' || e.KeyChar == Convert.ToChar("'"))
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        public bool IsNumber(string inputvalue)
        {
            Regex isnumber = new Regex(@"^?[0-9]$");
            return isnumber.IsMatch(inputvalue);
        }

        private void dtpFechaRemesa_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFechaRemesa.Value.Date > DateTime.Now.Date)
                dtpFechaRemesa.Value = DateTime.Now;
            
            DateTimePicker dtpRangoFechaValido = new DateTimePicker();
                        
            dtpRangoFechaValido.Value = DateTime.Now.AddDays(-2);

            if (dtpFechaRemesa.Value.Date < dtpRangoFechaValido.Value.Date)
            {
                MessageBox.Show("FECHA NO CUMPLE MAXIMO DE 3 DIAS APARTIR DE FECHA ACTUAL", "C753 ARIES - CONSULTA REMESA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpFechaRemesa.Value = DateTime.Now;
            }
            
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
    }
}