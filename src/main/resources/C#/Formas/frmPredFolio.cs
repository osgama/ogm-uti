using Artinsoft.VB6.Utils;
using Microsoft.VisualBasic;
using System;
using System.Windows.Forms;
using VB6 = Microsoft.VisualBasic.Compatibility.VB6.Support;

namespace Masivos
{
    internal partial class frmPredFolio
        : System.Windows.Forms.Form
    {

        //*******************************************************************************
        //* Identificación: PredFolio                                                   *
        //* Autor:          Abel Polo   *****                                           *
        //* Instalación:    PRAXIS                                                      *
        //* Fecha:          15/09/2003                                                  *
        //* Versión:        1.0                                                         *
        //*******************************************************************************

        private void cboTipoTram_KeyPress(Object eventSender, KeyPressEventArgs eventArgs)
        {
            int KeyAscii = (int)eventArgs.KeyChar;
            //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
            Keys tempRefParam = (Keys)KeyAscii;
            mdlGlobales.subAcepta_Return(ref tempRefParam);
            //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
            Keys tempRefParam2 = (Keys)KeyAscii;
            mdlCatalogos.subBusquedaRapida(ref cboTipoTram, ref tempRefParam2);
            if (KeyAscii == 0)
            {
                eventArgs.Handled = true;
            }
            eventArgs.KeyChar = Convert.ToChar(KeyAscii);
        }

        //*****************************************************************************************************
        //* Finalidad:  Subrutina para Declinar la Remesa
        //****************************************************************************************************
        private void cmdDeclinar_Click(Object eventSender, EventArgs eventArgs)
        {
            if (txtNombre.Text.Trim().Length > 0 && Conversion.Val(txtNumFolio.Text) > 0)
            {
                frmPredFolio.DefInstance.Visible = false;
                mdlTranMasivo.gstrFormActivo = "PredFolio";
                frmCausasDec.DefInstance.Show();
            }
            else
            {
                cmdDeclinar.Focus();
            }
        }

        //*****************************************************************************************************
        //* Finalidad:  Subrutina para Limpiar los Datos en la Forma PredFolio
        //****************************************************************************************************
        private void cmdLimpiar_Click(Object eventSender, EventArgs eventArgs)
        {
            string strDatos = String.Empty;
            if (txtNombre.Text == "" && txtNumFolio.Text == "")
            {
                txtFolioPreimpreso.Text = "";
                cboTipoTram.SelectedIndex = -1;
                cboTipoTram.Focus();
            }
            else
            {
                if (funDesbloqueaFolio())
                {
                    subLimpiaDatosFolio();
                    txtFolioPreimpreso.Text = "";
                    cboTipoTram.Focus();
                }
            }
        }

        //*****************************************************************************************************
        //* Finalidad:  Subrutina para salir de la forma PredFolio y regresar a MDIMasivos
        //****************************************************************************************************
        private void cmdSalir_Click(Object eventSender, EventArgs eventArgs)
        {
            if (funDesbloqueaFolio())
            {
                //AIS-1896 FSABORIO
                if (!this.SuspendFormClosing)
                    this.Close();
            }
        }

        private bool funDesbloqueaFolio()
        {
            bool result = false;
            string strDatos = String.Empty;
            result = true;
            if (txtNombre.Text == "" && txtNumFolio.Text == "")
            {
                return result;
            }
            else
            {
                mdlGlobales.gstrInicial.Value = mdlGlobales.gstrProceso.Value; //Se obtienen de la transacción 5560 18
                mdlGlobales.gstrEstatusInicial.Value = mdlGlobales.gstrEstatus.Value;
                strDatos = mdlGlobales.gstrInicial.Value + mdlGlobales.gstrEstatusInicial.Value;
                if (mdlTranAnalisis.funEnviaRecibe5561("5561", "23", strDatos))
                {
                    subLimpiaDatosFolio();
                    return result;
                }
                else
                {
                    //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                    string tempRefParam = "FAVOR DE REINTENTAR, EL ESTATUS INICIAL DE LA SOLICITUS (T-23) NO FUE CAMBIADO";
                    MsgBoxStyle tempRefParam2 = (MsgBoxStyle)(((int)MsgBoxStyle.Exclamation) + ((int)MsgBoxStyle.OkOnly));
                    mdlGlobales.subDespErrores(ref tempRefParam, ref tempRefParam2);
                    cmdSalir.Focus();
                }
            }
            return false;
        }

        //*****************************************************************************************************
        //* Finalidad:  Subrutina para Inicializar los Valores de la Forma frmPredFolio
        //****************************************************************************************************
        //UPGRADE_WARNING: (1041) Form_Load event was upgraded to Form_Load method and has a new behavior.
        private void Form_Load()
        {
            if (mdlTranMasivo.gstrFormActivo != "CausasDec")
            {
                mdlGlobales.subOcultaBotones();
                mdlGlobales.CentrarMDIHija(MDIMasivos.DefInstance, frmPredFolio.DefInstance);
                txtFolioPreimpreso.Text = "";
                cmdDeclinar.Enabled = false;
                mnuDeclinar.Enabled = false;
                mdlComunica.OleCatalogos.setAlineacionClave = Catalogos.clsCatalogos.enmAlineaciones.Izquierda;
                mdlComunica.OleCatalogos.setLongClave = 2;
                object tempRefParam = cboTipoTram.Text;
                string tempRefParam2 = "11";
                string tempRefParam3 = String.Empty;
                string tempRefParam4 = String.Empty;
                string tempRefParam5 = String.Empty;
                string tempRefParam6 = String.Empty;
                string tempRefParam7 = "D";
                mdlComunica.OleCatalogos.LlenaCombo(ref cboTipoTram, ref tempRefParam2, ref tempRefParam3, ref tempRefParam4, ref tempRefParam5, ref tempRefParam6, ref tempRefParam7);
                if (Conversion.Val(mdlGlobales.gstrTramite.Value) != 0)
                {
                    cboTipoTram.SelectedIndex = mdlComunica.OleCatalogos.ObtenIdxCombo(cboTipoTram, mdlGlobales.gstrTramite.Value);
                }
            }
        }

        //*****************************************************************************************************
        //* Finalidad:  Subrutina para cargar datos en el combo Tipo Tramite cuando se de Click
        //****************************************************************************************************
        private void cboTipoTram_SelectedIndexChanged(Object eventSender, EventArgs eventArgs)
        {
            if (cboTipoTram.SelectedIndex > -1)
            {
                mdlGlobales.gstrTramite.Value = Strings.Mid(cboTipoTram.Text, 1, 2);
                mdlGlobales.gstrDescTramite.Value = Strings.Mid(cboTipoTram.Text, 4, 30).Trim();
            }
            else
            {
                mdlGlobales.gstrTramite.Value = "00";
            }
        }

        //*****************************************************************************************************
        //* Finalidad:  Subrutina para Inicializae los Vaolores del Combo Tipo Tramite Cuando se da Enter
        //****************************************************************************************************
        private void cboTipoTram_KeyDown(Object eventSender, KeyEventArgs eventArgs)
        {
            int KeyCode = (int)eventArgs.KeyCode;
            int Shift = (int)eventArgs.KeyData / 0x10000;
            if (KeyCode == ((int)Keys.Escape))
            {
                cboTipoTram.SelectedIndex = -1;
            }
        }

        //*****************************************************************************************************
        //* Finalidad:  Subrutina que habilita los botones MDIMasivos
        //****************************************************************************************************
        private void frmPredFolio_Closed(Object eventSender, EventArgs eventArgs)
        {
            mdlGlobales.subMuestraMenu();
            mdlGlobales.gblnBandCancela = true;
            mdlGlobales.gblnChecaCancela = true;
        }

        //*****************************************************************************************************
        //* Finalidad:  Subrutina para Activar el Menu de Declinar
        //****************************************************************************************************
        public void mnuDeclinar_Click(Object eventSender, EventArgs eventArgs)
        {
            cmdDeclinar_Click(cmdDeclinar, new EventArgs());
        }

        //*****************************************************************************************************
        //* Finalidad:  Subrutina para Activar el Menu de Limpiar
        //****************************************************************************************************
        public void mnuLimpiar_Click(Object eventSender, EventArgs eventArgs)
        {
            cmdLimpiar_Click(cmdLimpiar, new EventArgs());
        }

        //*****************************************************************************************************
        //* Finalidad:  Subrutina para Activar el Menu de Salir
        //****************************************************************************************************
        public void mnuSalir_Click(Object eventSender, EventArgs eventArgs)
        {
            cmdSalir_Click(cmdSalir, new EventArgs());
        }

        //*****************************************************************************************************
        //* Finalidad:  Subrutina Para Seleccionar todo el Texto Cuando Obtenga el Foco txtFolioPreimpreso
        //****************************************************************************************************
        public void txtFolioPreimpreso_Enter(Object eventSender, EventArgs eventArgs)
        {
            mdlGlobales.subSelTexto(txtFolioPreimpreso);
        }

        //*****************************************************************************************************
        //* Finalidad:  Subrutina que valida los caracteres que se capturan
        //****************************************************************************************************
        public void txtFolioPreimpreso_KeyPress(Object eventSender, KeyPressEventArgs eventArgs)
        {
            int KeyAscii = (int)eventArgs.KeyChar;
            if (KeyAscii == 13)
            {
                if (!funDesbloqueaFolio())
                { //No permitir revisar otra solicitud mientras no se desbloquee la anterior
                    if (KeyAscii == 0)
                    {
                        eventArgs.Handled = true;
                    }
                    return;
                }
                if (funValidaObligatorios())
                {
                    txtFolioPreimpreso.Text = mdlGlobales.funPoneCeros(txtFolioPreimpreso.Text, 16);
                    if (txtFolioPreimpreso.Text == "0000000000000000")
                    {
                        //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                        string tempRefParam = "ERROR: NUMERO DE FOLIO NO DEBE SER CEROS.";
                        MsgBoxStyle tempRefParam2 = (MsgBoxStyle)(((int)MsgBoxStyle.Information) + ((int)MsgBoxStyle.OkOnly));
                        mdlGlobales.subDespErrores(ref tempRefParam, ref tempRefParam2);
                        txtFolioPreimpreso.Text = " ";
                        txtFolioPreimpreso.Focus();
                        if (KeyAscii == 0)
                        {
                            eventArgs.Handled = true;
                        }
                        return;
                    }
                    if (cboTipoTram.Text.Trim().Length == 0)
                    {
                        //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                        string tempRefParam3 = "ERROR: DEBE ELEGIR UN NUMERO DE TRAMITE PARA LA CONSULTA.";
                        MsgBoxStyle tempRefParam4 = (MsgBoxStyle)(((int)MsgBoxStyle.Information) + ((int)MsgBoxStyle.OkOnly));
                        mdlGlobales.subDespErrores(ref tempRefParam3, ref tempRefParam4);
                        cboTipoTram.Focus();
                    }
                    else
                    {
                        mdlGlobales.gstrFolPreimpreso.Value = txtFolioPreimpreso.Text;
                        mdlGlobales.gstrTramite.Value = Strings.Mid(cboTipoTram.Text, 1, 2);
                        subConsultaFolio();
                    }
                }
            }
            else
            {
                if (!funDesbloqueaFolio())
                {
                    KeyAscii = 0;
                }
                else
                {
                    subLimpiaDatos();
                }
            }
            string tempRefParam5 = mdlComunica.DIGITOS;
            int tempRefParam6 = -1;
            mdlGlobales.subFiltraCaptura(ref KeyAscii, ref tempRefParam5, ref tempRefParam6);
            if (KeyAscii == 0)
            {
                eventArgs.Handled = true;
            }
            eventArgs.KeyChar = Convert.ToChar(KeyAscii);
        }

        //*****************************************************************************************************
        //* Finalidad:  Funcion para Validar que los Datos Obligatorios se Capturen
        //****************************************************************************************************
        private bool funValidaObligatorios()
        {
            bool result = false;
            result = true;
            if (txtFolioPreimpreso.Text == "")
            {
                //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                string tempRefParam = "ERROR: DEBE CAPTURAR UN NUMERO DE FOLIO.";
                MsgBoxStyle tempRefParam2 = (MsgBoxStyle)(((int)MsgBoxStyle.Information) + ((int)MsgBoxStyle.OkOnly));
                mdlGlobales.subDespErrores(ref tempRefParam, ref tempRefParam2);
                txtFolioPreimpreso.Focus();
                return false;
            }
            if (txtFolioPreimpreso.Text == "0000000000000000")
            {
                //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                string tempRefParam3 = "ERROR: NUMERO DE FOLIO NO DEBE SER CEROS.";
                MsgBoxStyle tempRefParam4 = (MsgBoxStyle)(((int)MsgBoxStyle.Information) + ((int)MsgBoxStyle.OkOnly));
                mdlGlobales.subDespErrores(ref tempRefParam3, ref tempRefParam4);
                txtFolioPreimpreso.Focus();
                return false;
            }
            return result;
        }

        //*****************************************************************************************************
        //* Finalidad:  Subrutina para Consultar si el Folio Existe
        //****************************************************************************************************
        private void subConsultaFolio()
        {
            this.Cursor = Cursors.WaitCursor;
            //MMS 11/05 Parámetro Proceso de '19' a '019'
            if (mdlTranConsulta.funEnviaRecibe5420s("5420", "C ", "019"))
            {
                mdlGlobales.gstrFecInicial.Value = DateTime.Today.ToString("yyyyMMdd");
                mdlGlobales.gstrHoraInicial.Value = DateTime.Now.AddDays(-DateTime.Now.Date.ToOADate()).ToString("HHmmss");
                mdlGlobales.gstrFlagInfo.Value = "1";
                mdlGlobales.gstrIndicaCambio.Value = "0";
                mdlGlobales.gstrPantalla.Value = "00";
                if (mdlTranAnalisis.funEnviaRecibe5560("5560", "18", "01"))
                {
                    subPresentaFolio();
                    cmdDeclinar.Enabled = true;
                    mnuDeclinar.Enabled = true;
                }
                else
                {
                    //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                    string tempRefParam = "ERROR: FAVOR DE REINTENTAR.";
                    MsgBoxStyle tempRefParam2 = (MsgBoxStyle)(((int)MsgBoxStyle.Exclamation) + ((int)MsgBoxStyle.OkOnly));
                    mdlGlobales.subDespErrores(ref tempRefParam, ref tempRefParam2);
                    cmdDeclinar.Enabled = false;
                    mnuDeclinar.Enabled = false;
                    subLimpiaDatosFolio();
                }
            }
            mdlGlobales.subDespMensajes("");
            this.Cursor = Cursors.Default;
        }

        //*****************************************************************************************************
        //* Finalidad:  Subrutina para cargar Datos en la Firma PredFolio
        //****************************************************************************************************
        private void subPresentaFolio()
        {
            int intControlSexo = 0;
            txtNumFolio.Text = Strings.Mid(mdlTranConsulta.gvRecive5420C, 8, 16);
            txtFolioInt.Text = Strings.Mid(mdlTranConsulta.gvRecive5420C, 171, 8);
            txtNumCliente.Text = Strings.Mid(mdlTranConsulta.gvRecive5420C, 688, 16);
            txtNombre.Text = Strings.Mid(mdlTranConsulta.gvRecive5420C, 181, 50).Trim() + " " + Strings.Mid(mdlTranConsulta.gvRecive5420C, 231, 60).Trim() + " " + Strings.Mid(mdlTranConsulta.gvRecive5420C, 291, 60).Trim();
            mskFechaNaci.Text = StringsHelper.Format(Strings.Mid(mdlTranConsulta.gvRecive5420C, 458, 2) + Strings.Mid(mdlTranConsulta.gvRecive5420C, 456, 2) + Strings.Mid(mdlTranConsulta.gvRecive5420C, 452, 4), "&&/&&/&&&&");
            txtRFCPyme.Text = Strings.Mid(mdlTranConsulta.gvRecive5420C, 439, 13);
            string tempRefParam = "40";
            string tempRefParam2 = Strings.Mid(mdlTranConsulta.gvRecive5420C, 460, 1);
            string tempRefParam3 = null;
            string tempRefParam4 = null;
            string tempRefParam5 = null;
            Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam6 = Catalogos.clsCatalogos.enmCamposCatalogos.Atributos;
            string tempRefParam7 = "D";
            if (mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam, tempRefParam2, tempRefParam3, tempRefParam4, ref tempRefParam5, ref tempRefParam6, ref tempRefParam7))
            {
                intControlSexo = Convert.ToInt32(Conversion.Val(mdlComunica.OleCatalogos.getLlave1));
            }
            if (intControlSexo == 1)
            { //1:Femenino 2:Masculino
                optSexo[0].Checked = true; //Femenino
            }
            else
            {
                optSexo[1].Checked = true; //Masculino
            }
            txtCalleyNo.Text = Strings.Mid(mdlTranConsulta.gvRecive5420C, 557, 35);
            txtColonia.Text = Strings.Mid(mdlTranConsulta.gvRecive5420C, 592, 35);
            txtCd.Text = Strings.Mid(mdlTranConsulta.gvRecive5420C, 652, 26);
            txtEntidad.Text = Strings.Mid(mdlTranConsulta.gvRecive5420C, 632, 20);
            txtCP.Text = Strings.Mid(mdlTranConsulta.gvRecive5420C, 627, 5);
            mskTelefono.Text = StringsHelper.Format(Strings.Mid(mdlTranConsulta.gvRecive5420C, 678, 10), "(@@@)@@@,@@@@");
            txtProducto.Text = Strings.Mid(mdlTranConsulta.gvRecive5420C, 745, 4) + "-" + Strings.Mid(mdlTranConsulta.gvRecive5420C, 515, 42);
            txtProceso.Text = Strings.Mid(mdlTranConsulta.gvRecive5420C, 381, 50);
            //MMS 11/05 Incremento en la longitud del campo Estatus (2 a 3)
            txtEstatus.Text = Strings.Mid(mdlTranConsulta.gvRecive5420C, 42, 3) + "-" + Strings.Mid(mdlTranConsulta.gvRecive5420C, 351, 30);
            txtCausa.Text = Strings.Mid(mdlTranConsulta.gvRecive5420C, 461, 4) + "-" + Strings.Mid(mdlTranConsulta.gvRecive5420C, 465, 50);
            mskFecEstatus.Text = StringsHelper.Format(Strings.Mid(mdlTranConsulta.gvRecive5420C, 437, 2) + Strings.Mid(mdlTranConsulta.gvRecive5420C, 435, 2) + Strings.Mid(mdlTranConsulta.gvRecive5420C, 431, 4), "&&/&&/&&&&");
            cmdDeclinar.Enabled = true;
            mnuDeclinar.Enabled = true;
        }

        //*****************************************************************************************************
        //* Finalidad:  Subrutina para Limpiar Datos
        //****************************************************************************************************
        private void subLimpiaDatosFolio()
        {
            //Limpia datos de la pantalla
            mdlComunica.OleCatalogos.setAlineacionClave = Catalogos.clsCatalogos.enmAlineaciones.Izquierda;
            mdlComunica.OleCatalogos.setLongClave = 2;
            object tempRefParam = cboTipoTram.Text;
            string tempRefParam2 = "11";
            string tempRefParam3 = String.Empty;
            string tempRefParam4 = String.Empty;
            string tempRefParam5 = String.Empty;
            string tempRefParam6 = String.Empty;
            string tempRefParam7 = "D";
            mdlComunica.OleCatalogos.LlenaCombo(ref cboTipoTram, ref tempRefParam2, ref tempRefParam3, ref tempRefParam4, ref tempRefParam5, ref tempRefParam6, ref tempRefParam7);
            if (Conversion.Val(mdlGlobales.gstrTramite.Value) != 0)
            {
                cboTipoTram.SelectedIndex = mdlComunica.OleCatalogos.ObtenIdxCombo(cboTipoTram, mdlGlobales.gstrTramite.Value);
            }
            subLimpiaDatos();
        }

        public void subLimpiaDatos()
        {
            mdlGlobales.subDespMensajes("");
            txtNumFolio.Text = "";
            txtFolioInt.Text = "";
            txtNumCliente.Text = "";
            txtNombre.Text = "";
            mskFechaNaci.Mask = "";
            mskFechaNaci.Text = StringsHelper.Format("", "&&/&&/&&&&");
            mskFechaNaci.Mask = "00/00/0000";
            txtRFCPyme.Text = "";
            txtCalleyNo.Text = "";
            txtColonia.Text = "";
            txtCd.Text = "";
            txtEntidad.Text = "";
            txtCP.Text = "";
            mskTelefono.Mask = "";
            mskTelefono.Text = StringsHelper.Format("", "(@@@)@@@,@@@@");
            mskTelefono.Mask = "(000)000,0000";
            txtProducto.Text = "";
            txtProceso.Text = "";
            txtEstatus.Text = "";
            txtCausa.Text = "";
            mskFecEstatus.Mask = "";
            mskFecEstatus.Text = StringsHelper.Format("", "&&/&&/&&&&");
            mskFecEstatus.Mask = "00/00/0000";
            cmdDeclinar.Enabled = false;
            mnuDeclinar.Enabled = false;
        }

        //*****************************************************************************************************
        //* Finalidad:  Subrutina para relizar la transaccion del Folio a Declinar
        //*             Es llamada desde el formulario "Causas de Declinación" (frmCausasDec)
        //****************************************************************************************************
        public void subDeclinaFolio()
        {
            string strDatos = String.Empty;
            if (txtNombre.Text.Trim().Length > 0 && txtNumFolio.Text.Trim().Length > 0)
            {
                mdlGlobales.gstrEstatus.Value = "005"; //Enviar el status 05 (Rechazo o declinación) 'MMS 11/05 Se le agrega un 0 al valor debido al incremento en la longitud del campo
                mdlGlobales.gstrProceso.Value = mdlGlobales.gstrSigProceso.Value; //Declinar asignando el siguiente proceso al proceso actúal
                mdlGlobales.gstrFecFinal.Value = DateTime.Today.ToString("yyyyMMdd");
                mdlGlobales.gstrHoraFinal.Value = DateTime.Now.AddDays(-DateTime.Now.Date.ToOADate()).ToString("HHmmss");
                mdlGlobales.gstrComentarios.Value = new String(' ', 150);
                mdlGlobales.gstrCausaDec.Value = mdlGlobales.funPoneCeros(mdlGlobales.gstrClaveDeclina, 4);
                strDatos = mdlTranAnalisis.funArmaActEstatus(frmPredFolio.DefInstance);
                if (mdlTranAnalisis.funEnviaRecibe5561("5561", "08", strDatos))
                {
                    mdlGlobales.subDespMensajes("TRANSACCION EXITOSA");
                    //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                    string tempRefParam = "TRANSACCION EXITOSA";
                    MsgBoxStyle tempRefParam2 = (MsgBoxStyle)(((int)MsgBoxStyle.Information) + ((int)MsgBoxStyle.OkOnly));
                    mdlGlobales.subDespErrores(ref tempRefParam, ref tempRefParam2);
                }
                else
                {
                    //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                    string tempRefParam3 = "ERROR: FAVOR DE REINTENTAR";
                    MsgBoxStyle tempRefParam4 = (MsgBoxStyle)(((int)MsgBoxStyle.Exclamation) + ((int)MsgBoxStyle.OkOnly));
                    mdlGlobales.subDespErrores(ref tempRefParam3, ref tempRefParam4);
                }
            }
        }

        //AIS-1896 FSABORIO
        public bool SuspendFormClosing = false;
        private void frmPredFolio_FormClosing(object sender, FormClosingEventArgs eventArgs)
        {
            //AIS-1896 FSABORIO
            SuspendFormClosing = true;
        }
    }
}