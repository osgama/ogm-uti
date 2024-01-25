using System;
using System.Windows.Forms;
using VB6 = Microsoft.VisualBasic.Compatibility.VB6.Support;

namespace Masivos
{
    internal partial class frmTipoDeclinacion
        : System.Windows.Forms.Form
    {

        //*******************************************************************************
        //* Identificación: frmTipoDeclinacion                                          *
        //* Autor:          Luis E. Aguilar                                             *
        //* Instalación:    PRAXIS                                                      *
        //* Fecha:          19/09/2003                                                  *
        //* Versión:        1.0                                                         *
        //*******************************************************************************

        //*******************************************************************************
        //* Finalidad:  Subrutina que Realiza las validaciones cuando se da Click en Aceptar
        //*                  y se Selecciona el Tipo de Declinacion
        //*******************************************************************************
        private void cmdAceptar_Click(Object eventSender, EventArgs eventArgs)
        {
            mdlCatalogos.gstrCatProceso = "D";
            if (optDeclinaRemesa.Checked)
            {
                mdlCatalogos.gstrCatProceso = "DR";
                mdlTranMasivo.gstrFormActivo = "ProcMasivo";
                frmCausasDec.DefInstance.Show();
                //AIS-1896 FSABORIO
                if (!this.SuspendFormClosing)
                    this.Close();
            }
            else if (optDeclinaFolios.Checked)
            {
                mdlCatalogos.gstrCatProceso = "DF";
                frmDecFolios.DefInstance.Show();
                //AIS-1896 FSABORIO
                if (!this.SuspendFormClosing)
                    this.Close();
            }
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina que Realiza las validaciones cuando se da Enter en Aceptar
        //*                  y se Selecciona el Tipo de Declinacion
        //*******************************************************************************
        private void cmdAceptar_KeyPress(Object eventSender, KeyPressEventArgs eventArgs)
        {
            int KeyAscii = (int)eventArgs.KeyChar;
            if (KeyAscii == 13)
            {
                mdlCatalogos.gstrCatProceso = "D";
                if (optDeclinaRemesa.Checked)
                {
                    mdlCatalogos.gstrCatProceso = "DR";
                    mdlTranMasivo.gstrFormActivo = "ProcMasivo";
                    frmCausasDec.DefInstance.Show();
                    //AIS-1896 FSABORIO
                    if (!this.SuspendFormClosing)
                        this.Close();
                }
                else if (optDeclinaFolios.Checked)
                {
                    mdlCatalogos.gstrCatProceso = "DF";
                    frmDecFolios.DefInstance.Show();
                    //AIS-1896 FSABORIO
                    if (!this.SuspendFormClosing)
                        this.Close();
                }
            }
            if (KeyAscii == 0)
            {
                eventArgs.Handled = true;
            }
            eventArgs.KeyChar = Convert.ToChar(KeyAscii);
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina que Realiza las Validaciones cuando se da Click en Cancelar
        //*******************************************************************************
        private void cmdCancelar_Click(Object eventSender, EventArgs eventArgs)
        {
            mdlCatalogos.gstrCatProceso = "";
            //AIS-1896 FSABORIO
            if (!this.SuspendFormClosing)
                this.Close();
            frmRegRemesas.DefInstance.Show();
            frmRegRemesas.DefInstance.cmdDeclinar.Focus();
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina que Realiza las Validaciones cuando se da Enter en Cancelar
        //*******************************************************************************
        private void cmdCancelar_KeyPress(Object eventSender, KeyPressEventArgs eventArgs)
        {
            int KeyAscii = (int)eventArgs.KeyChar;
            if (KeyAscii == 13)
            {
                //AIS-1896 FSABORIO
                if (!frmTipoDeclinacion.DefInstance.SuspendFormClosing)
                    frmTipoDeclinacion.DefInstance.Close();
                frmRegRemesas.DefInstance.Show();
                frmRegRemesas.DefInstance.cmdDeclinar.Focus();
            }
            if (KeyAscii == 0)
            {
                eventArgs.Handled = true;
            }
            eventArgs.KeyChar = Convert.ToChar(KeyAscii);
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Validar los Valores Inicales de esta Pantalla
        //*******************************************************************************
        //UPGRADE_WARNING: (1041) Form_Load event was upgraded to Form_Load method and has a new behavior.
        private void Form_Load()
        {

            string strDatos = String.Empty;
            mdlGlobales.subOcultaBotones();
            mdlGlobales.gstrFolInterno.Value = mdlGlobales.funZeroes(8);
            mdlGlobales.CentrarMDIHija(MDIMasivos.DefInstance, frmTipoDeclinacion.DefInstance);

            mdlCatalogos.gstrCatProceso = "";
            string tempRefParam = "";
            mdlGlobales.subDespMsg(ref tempRefParam);
            this.Cursor = Cursors.Default;
        }

        public void mnuAceptar_Click(Object eventSender, EventArgs eventArgs)
        {
            cmdAceptar_Click(cmdAceptar, new EventArgs());
        }

        public void mnuCancela_Click(Object eventSender, EventArgs eventArgs)
        {
            cmdCancelar_Click(cmdCancelar, new EventArgs());
        }

        public void mnuDeclinaFolios_Click(Object eventSender, EventArgs eventArgs)
        {
            subProcesaOpcion();
        }

        public void mnuDeclinaToda_Click(Object eventSender, EventArgs eventArgs)
        {
            subProcesaOpcion();
        }

        private void subProcesaOpcion()
        {
            mnuDeclinaToda.Checked = !mnuDeclinaToda.Checked;
            mnuDeclinaFolios.Checked = !mnuDeclinaFolios.Checked;
            if (mnuDeclinaToda.Checked)
            {
                optDeclinaRemesa.Checked = true;
            }
            else
            {
                optDeclinaFolios.Checked = true;
            }
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Validar el Enter en el optDeclinaRemesa
        //*******************************************************************************
        private void optDeclinaRemesa_KeyPress(Object eventSender, KeyPressEventArgs eventArgs)
        {
            int KeyAscii = (int)eventArgs.KeyChar;
            //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
            Keys tempRefParam = (Keys)KeyAscii;
            mdlGlobales.subAcepta_Return(ref tempRefParam);
            if (KeyAscii == 0)
            {
                eventArgs.Handled = true;
            }
            eventArgs.KeyChar = Convert.ToChar(KeyAscii);
        }

        //AIS-1896 FSABORIO
        public bool SuspendFormClosing = false;
        private void frmTipoDeclinacion_FormClosing(object sender, FormClosingEventArgs eventArgs)
        {
            //AIS-1896 FSABORIO
            SuspendFormClosing = true;
        }
    }
}