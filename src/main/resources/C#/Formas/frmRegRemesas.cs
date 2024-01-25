using Microsoft.VisualBasic;
using System;
using System.Windows.Forms;
using VB6 = Microsoft.VisualBasic.Compatibility.VB6.Support;

namespace Masivos
{
    internal partial class frmRegRemesas
        : System.Windows.Forms.Form
    {

        //*******************************************************************************
        //* Identificación: frmRegRemesas                                               *
        //* Autor:          Luis E. Aguilar                                             *
        //* Instalación:    PRAXIS                                                      *
        //* Fecha:          16/09/2003                                                  *
        //* Versión:        1.0                                                         *
        //*******************************************************************************

        //*******************************************************************************
        //* Finalidad:  Subrutina para procesar el control cboEntidadOrigen cuando recibe el foco
        //*******************************************************************************
        private void cboEntidadOrigen_Enter(Object eventSender, EventArgs eventArgs)
        {
            subLlenaEntidadOrigen();
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para procesar el control cboEntidadOrigen cuando pierde el foco
        //*******************************************************************************
        private void cboEntidadOrigen_Leave(Object eventSender, EventArgs eventArgs)
        {
            frmProcMasivo.DefInstance.txtEntidadOrigen.Text = cboEntidadOrigen.Text.Trim();
        }

        private void cboFamiliaProducto_KeyPress(Object eventSender, KeyPressEventArgs eventArgs)
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

        //*******************************************************************************
        //* Finalidad:  Subrutina para LLenar el Combo de Tipo de Entidad Origen cuando
        //*                       se de Click en el Combo
        //*******************************************************************************
        private void cboTipoEntOrig_SelectedIndexChanged(Object eventSender, EventArgs eventArgs)
        {
            subLlenaEntidadOrigen();
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para LLenar el Combo de Tipo de Entidad Origen cuando
        //*                       se Seleccione el Combo
        //*******************************************************************************
        private void cboTipoEntOrig_KeyDown(Object eventSender, KeyEventArgs eventArgs)
        {
            int KeyCode = (int)eventArgs.KeyCode;
            int Shift = (int)eventArgs.KeyData / 0x10000;
            if (KeyCode == ((int)Keys.Escape))
            {
                cboTipoEntOrig.SelectedIndex = -1;
            }
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para LLenar el Combo de Tipo de Entidad Origen cuando
        //*                       se Seleccione el Combo con Enter
        //*******************************************************************************
        private void cboTipoEntOrig_KeyPress(Object eventSender, KeyPressEventArgs eventArgs)
        {
            int KeyAscii = (int)eventArgs.KeyChar;
            if (KeyAscii == 13)
            {
                subLlenaEntidadOrigen();
                //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                Keys tempRefParam = (Keys)KeyAscii;
                mdlGlobales.subAcepta_Return(ref tempRefParam);
            }
            if (KeyAscii == 0)
            {
                eventArgs.Handled = true;
            }
            eventArgs.KeyChar = Convert.ToChar(KeyAscii);
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para LLenar el Combo de Promocion cuando se de Click en
        //*                      el Combo
        //*******************************************************************************
        private void cboPromocion_SelectedIndexChanged(Object eventSender, EventArgs eventArgs)
        {
            if (cboPromocion.SelectedIndex > -1)
            {
                mdlGlobales.gstrPromocion.Value = mdlGlobales.funPoneCeros(Strings.Mid(cboPromocion.Text, 1, 2), 4);
                mdlGlobales.gstrDescPromocion.Value = Strings.Mid(cboPromocion.Text, 4, 30).Trim();
            }
            else
            {
                mdlGlobales.gstrPromocion.Value = "00";
            }
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para LLenar el Combo de Promocion cuando se Seleccione
        //*                     el Combo
        //*******************************************************************************
        private void cboPromocion_KeyDown(Object eventSender, KeyEventArgs eventArgs)
        {
            int KeyCode = (int)eventArgs.KeyCode;
            int Shift = (int)eventArgs.KeyData / 0x10000;
            if (KeyCode == ((int)Keys.Escape))
            {
                cboPromocion.SelectedIndex = -1;
            }
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para LLenar el Combo de Promocion cuando se Seleccione
        //*                     el Combo con Enter
        //*******************************************************************************
        private void cboPromocion_KeyPress(Object eventSender, KeyPressEventArgs eventArgs)
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

        //*******************************************************************************
        //* Finalidad:  Subrutina para LLenar el Combo de Entidad Origen cuando
        //*                       se de Click en el Combo
        //*******************************************************************************
        private void cboEntidadOrigen_SelectedIndexChanged(Object eventSender, EventArgs eventArgs)
        {
            if (cboEntidadOrigen.SelectedIndex > -1)
            {
                //ASG 20040106 llenar la variable global
                mdlGlobales.gstrCveEntOrig.Value = mdlGlobales.funPoneCeros(cboEntidadOrigen.Text.Substring(0, Math.Min(cboEntidadOrigen.Text.Length, 4)), 4);
                mdlGlobales.gstrDescEntOrig.Value = Strings.Mid(cboEntidadOrigen.Text, 6, 30).Trim();
                mdlGlobales.gstrGpoEntOrig.Value = "0001";
            }
            else
            {
                //ASG 20030106 Llenar la variable global
                mdlGlobales.gstrCveEntOrig.Value = "0000";
                mdlGlobales.gstrGpoEntOrig.Value = "0000";
            }
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para LLenar el Combo de Entidad Origen cuando
        //*                       se Seleccione el Combo
        //*******************************************************************************
        private void cboEntidadOrigen_KeyDown(Object eventSender, KeyEventArgs eventArgs)
        {
            int KeyCode = (int)eventArgs.KeyCode;
            int Shift = (int)eventArgs.KeyData / 0x10000;
            if (KeyCode == ((int)Keys.Escape))
            {
                cboEntidadOrigen.SelectedIndex = -1;
            }
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para LLenar el Combo de Entidad Origen cuando
        //*                       se Seleccione el Combo con Enter
        //*******************************************************************************
        private void cboEntidadOrigen_KeyPress(Object eventSender, KeyPressEventArgs eventArgs)
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

        //*******************************************************************************
        //* Finalidad:  Subrutina para pasar el tipo de entidad origen seleccionado a la pantalla de ProcMasivo
        //*******************************************************************************
        private void cboTipoEntOrig_Leave(Object eventSender, EventArgs eventArgs)
        {
            frmProcMasivo.DefInstance.txtTipoEntidad.Text = cboTipoEntOrig.Text.Trim();
        }

        private void cboTipoSolicitud_KeyPress(Object eventSender, KeyPressEventArgs eventArgs)
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

        private void cboTipoTram_KeyPress(Object eventSender, KeyPressEventArgs eventArgs)
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

        //*******************************************************************************
        //* Finalidad:  Subrutina para Generar la Remesa cuando se Oprima el botón Aceptar
        //*******************************************************************************
        public void cmdAceptar_Click(Object eventSender, EventArgs eventArgs)
        {
            string strDatos = String.Empty;
            if (!funValidaObligatorios())
            {
                return;
            }
            mdlGlobales.gblnEjecutaRegistro = true;
            mdlGlobales.gblnRemesaRegistrada = true;
            frmProcMasivo.DefInstance.Show();
            //AIS-1896 FSABORIO
            if (!this.SuspendFormClosing)
                this.Close();
            //    Call funGeneraRemesa(strDatos)
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Declinar la Remesa cuando se oprima este botón
        //*******************************************************************************
        private void cmdDeclinar_Click(Object eventSender, EventArgs eventArgs)
        {
            string strDatos = String.Empty;
            if (!funValidaObligatorios())
            {
                return;
            }
            frmTipoDeclinacion.DefInstance.Show();
            this.Hide();
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Limpiar los Valores cuando se oprima este botón
        //*******************************************************************************
        private void cmdLimpiar_Click(Object eventSender, EventArgs eventArgs)
        {
            subLimpiaPantalla();
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Validar la Salida de esta Pantalla
        //*******************************************************************************
        private void cmdSalir_Click(Object eventSender, EventArgs eventArgs)
        {
            if (MessageBox.Show("¿DESEA SALIR DE ESTE PROCESO?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                subLimpiaPantalla();
                //AIS-1896 FSABORIO
                if (!frmRegRemesas.DefInstance.SuspendFormClosing)
                    frmRegRemesas.DefInstance.Close();
                frmProcMasivo.DefInstance.subLimpiarDatos();
                frmProcMasivo.DefInstance.Show();
                frmProcMasivo.DefInstance.cmdLeeArchivo.Focus();
            }
            else
            {
                cmdSalir.Focus();
            }
        }

        private void frmRegRemesas_KeyPress(Object eventSender, KeyPressEventArgs eventArgs)
        {
            int KeyAscii = (int)eventArgs.KeyChar;
            if (this.ActiveControl != null)
            {
                if (this.ActiveControl is ComboBox)
                {
                    //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                    ComboBox tempRefParam = (ComboBox)this.ActiveControl;
                    Keys tempRefParam2 = (Keys)KeyAscii;
                    mdlCatalogos.subBusquedaRapida(ref tempRefParam, ref tempRefParam2);
                }
            }
            if (KeyAscii == 0)
            {
                eventArgs.Handled = true;
            }
            eventArgs.KeyChar = Convert.ToChar(KeyAscii);
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Inicializar los Valores de la Pantalla
        //*******************************************************************************
        //UPGRADE_WARNING: (1041) Form_Load event was upgraded to Form_Load method and has a new behavior.
        private void Form_Load()
        {
            string strDatos = String.Empty;

            mdlGlobales.subDespMensajes("LEYENDO PARÁMETROS");
            mdlGlobales.CentrarMDIHija(MDIMasivos.DefInstance, frmRegRemesas.DefInstance);

            mdlGlobales.subOcultaBotones();
            mdlGlobales.gstrFolInterno.Value = mdlGlobales.funZeroes(8);
            string tempRefParam = "";
            mdlGlobales.subDespMsg(ref tempRefParam);


            cboTipoTram.Items.Clear();
            cboTipoTram.Items.Add(frmProcMasivo.DefInstance.cboTipoTram.Text);
            cboTipoTram.SelectedIndex = 0;
            cboTipoTram.Enabled = false;

            cboTipoEntOrig.Items.Clear();
            mdlComunica.OleCatalogos.setAlineacionClave = Catalogos.clsCatalogos.enmAlineaciones.Izquierda;
            mdlComunica.OleCatalogos.setLongClave = 2;
            object tempRefParam2 = cboTipoEntOrig.Text;
            string tempRefParam3 = "66";
            string tempRefParam4 = String.Empty;
            string tempRefParam5 = String.Empty;
            string tempRefParam6 = String.Empty;
            string tempRefParam7 = String.Empty;
            string tempRefParam8 = "E";
            mdlComunica.OleCatalogos.LlenaCombo(ref cboTipoEntOrig, ref tempRefParam3, ref tempRefParam4, ref tempRefParam5, ref tempRefParam6, ref tempRefParam7, ref tempRefParam8);
            cboTipoEntOrig.SelectedIndex = -1;

            mdlGlobales.gstrTipoEntOrig.Value = Strings.Mid(cboTipoEntOrig.Text, 1, 2).Trim();
            mdlGlobales.gstrDescTipoEntOrig.Value = Strings.Mid(cboTipoEntOrig.Text, 4, 30).Trim();
            mdlComunica.OleCatalogos.setLongClave = 4;
            object tempRefParam9 = cboEntidadOrigen.Text;
            string tempRefParam10 = "13";
            string tempRefParam11 = mdlGlobales.gstrTipoEntOrig.Value;
            string tempRefParam12 = "1";
            string tempRefParam13 = String.Empty;
            string tempRefParam14 = String.Empty;
            string tempRefParam15 = "E";
            mdlComunica.OleCatalogos.LlenaCombo(ref cboEntidadOrigen, ref tempRefParam10, ref tempRefParam11, ref tempRefParam12, ref tempRefParam13, ref tempRefParam14, ref tempRefParam15);
            mdlGlobales.gstrTipoEntOrig.Value = tempRefParam11;
            cboEntidadOrigen.SelectedIndex = -1;
            mdlComunica.OleCatalogos.setLongClave = 2;

            cboPromocion.Items.Clear();
            object tempRefParam16 = cboPromocion.Text;
            string tempRefParam17 = "64";
            string tempRefParam18 = String.Empty;
            string tempRefParam19 = String.Empty;
            string tempRefParam20 = String.Empty;
            string tempRefParam21 = String.Empty;
            string tempRefParam22 = "D";
            mdlComunica.OleCatalogos.LlenaCombo(ref cboPromocion, ref tempRefParam17, ref tempRefParam18, ref tempRefParam19, ref tempRefParam20, ref tempRefParam21, ref tempRefParam22);
            if (cboPromocion.Items.Count > 0)
            {
                cboPromocion.SelectedIndex = 0;
            }

            cboFamiliaProducto.Text = frmProcMasivo.DefInstance.txtFamiliaProducto.Text;
            cboFamiliaProducto.Enabled = false;
            cboTipoSolicitud.Text = frmProcMasivo.DefInstance.txtTipoSolicitud.Text;
            cboTipoSolicitud.Enabled = false;

            mskFecProceso.Text = DateTime.Now.ToString("dd/MM/yyyy");
            mskFecProceso.Enabled = false;
            txtTotalSolEnviadas.Text = Int32.Parse(frmProcMasivo.DefInstance.txtNumSolicitudes.Text).ToString();
            txtTotalSolEnviadas.Enabled = false;
            txtCveRemesa.Enabled = false;

            mdlGlobales.subDespMensajes("");
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Validar el Menu Aceptar
        //*******************************************************************************
        public void mnuAceptar_Click(Object eventSender, EventArgs eventArgs)
        {
            cmdAceptar_Click(cmdAceptar, new EventArgs());
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Validar el Menu Declinar
        //*******************************************************************************
        public void mnuDeclinar_Click(Object eventSender, EventArgs eventArgs)
        {
            cmdDeclinar_Click(cmdDeclinar, new EventArgs());
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Validar el Menu Limpiar
        //*******************************************************************************
        public void mnuLimpiar_Click(Object eventSender, EventArgs eventArgs)
        {
            cmdLimpiar_Click(cmdLimpiar, new EventArgs());
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Validar que los Datos sean Correctos y Llama a la
        //*                  Pantalla de Autorizacion Para Generar la Remesa
        //*******************************************************************************
        public void mnuRegLinea_Click(Object eventSender, EventArgs eventArgs)
        {
            string strDatos = String.Empty;
            if (!funValidaObligatorios())
            {
                return;
            }
            mdlTranAnalisis.subValidaAutorizacion();
            if (mdlGlobales.gblnUsuarioAutorizado)
            {
                //AIS-1896 FSABORIO
                if (!frmRegRemesas.DefInstance.SuspendFormClosing)
                    frmRegRemesas.DefInstance.Close();
                frmProcMasivo.DefInstance.Show();
                mdlGlobales.gblnEjecutaRegistro = true;
                mdlGlobales.gblnRemesaRegistrada = true;
                mdlGlobales.gblnEnvioTansac = true;
            }
            mdlGlobales.subDespMensajes("");
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Validar el Menu de Salir
        //*******************************************************************************
        public void mnuSalir_Click(Object eventSender, EventArgs eventArgs)
        {
            cmdSalir_Click(cmdSalir, new EventArgs());
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Colocar los Valores de este Combo (CboEntidadOrigen)
        //*******************************************************************************
        public void subLlenaEntidadOrigen()
        {
            if (cboTipoEntOrig.SelectedIndex > -1)
            {
                mdlGlobales.gstrTipoEntOrig.Value = Strings.Mid(cboTipoEntOrig.Text, 1, 2).Trim();
                mdlGlobales.gstrDescTipoEntOrig.Value = Strings.Mid(cboTipoEntOrig.Text, 4, 30).Trim();
            }
            else
            {
                mdlGlobales.gstrTipoEntOrig.Value = "00";
            }
            mdlGlobales.gstrEntOrig.Value = mdlGlobales.gstrTipoEntOrig.Value;
            if (Conversion.Val(mdlGlobales.gstrTipoEntOrig.Value) != 0)
            {
                mdlComunica.OleCatalogos.setLongClave = 4;
                mdlComunica.OleCatalogos.setAlineacionClave = Catalogos.clsCatalogos.enmAlineaciones.Izquierda;
                cboEntidadOrigen.Items.Clear();
                object tempRefParam = cboEntidadOrigen.Text;
                string tempRefParam2 = "13";
                string tempRefParam3 = mdlGlobales.gstrTipoEntOrig.Value;
                string tempRefParam4 = "1";
                string tempRefParam5 = String.Empty;
                string tempRefParam6 = String.Empty;
                string tempRefParam7 = "E";
                mdlComunica.OleCatalogos.LlenaCombo(ref cboEntidadOrigen, ref tempRefParam2, ref tempRefParam3, ref tempRefParam4, ref tempRefParam5, ref tempRefParam6, ref tempRefParam7);
                mdlGlobales.gstrTipoEntOrig.Value = tempRefParam3;
                cboEntidadOrigen.SelectedIndex = -1;
                mdlComunica.OleCatalogos.setLongClave = 2;
            }
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Limpiar la Pantalla
        //*******************************************************************************
        private void subLimpiaPantalla()
        {
            this.cboEntidadOrigen.SelectedIndex = -1;
            cboTipoEntOrig.SelectedIndex = -1;
            cboPromocion.SelectedIndex = -1;
            mdlCatalogos.gstrCatProceso = "";
            mskFecIngresoCred.Mask = "";
            mskFecIngresoCred.Text = "";
            //AIS-1916 FSABORIO
            mskFecIngresoCred.Mask = "00/00/0000";
            mskFecAceptaCred.Mask = "";
            mskFecAceptaCred.Text = "";
            //AIS-1916 FSABORIO
            mskFecAceptaCred.Mask = "00/00/0000";
            cboEntidadOrigen.SelectedIndex = -1;
            cboTipoEntOrig.Focus();
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Validar que los Datos de la Pantalla se Capturen y
        //*                      Cumplan con las especificaciones
        //*******************************************************************************
        private bool funValidaObligatorios()
        {
            bool result = false;
            object vntFecha = null;
            if (cboTipoEntOrig.Text.Trim().Length == 0)
            {
                string tempRefParam = "EL 'TIPO DE EBTIDAD-ORIGEN' DEBE SER CAPTURADO";
                MsgBoxStyle tempRefParam2 = MsgBoxStyle.Exclamation;
                string tempRefParam3 = "ERROR DE CAPTURA";
                mdlGlobales.subDespErrores(ref tempRefParam, ref tempRefParam2, ref tempRefParam3);
                cboTipoEntOrig.Focus();
                return result;
            }
            if (cboEntidadOrigen.Text.Trim().Length == 0)
            {
                string tempRefParam4 = "EL DATO 'ENTIDAD-ORIGEN' DEBE SER CAPTURADO";
                MsgBoxStyle tempRefParam5 = MsgBoxStyle.Exclamation;
                string tempRefParam6 = "ERROR DE CAPTURA";
                mdlGlobales.subDespErrores(ref tempRefParam4, ref tempRefParam5, ref tempRefParam6);
                cboEntidadOrigen.Focus();
                return result;
            }
            if (cboPromocion.Text.Trim().Length == 0)
            {
                string tempRefParam7 = "EL DATO 'PROMOCION' DEBE SER CAPTURADO";
                MsgBoxStyle tempRefParam8 = MsgBoxStyle.Exclamation;
                string tempRefParam9 = "ERROR DE CAPTURA";
                mdlGlobales.subDespErrores(ref tempRefParam7, ref tempRefParam8, ref tempRefParam9);
                cboPromocion.Focus();
                return result;
            }
            if (mskFecIngresoCred.Text == "__/__/____")
            {
                string tempRefParam10 = "LA 'FECHA DE INGRESO DEL CREDITO' DEBE SER CAPTURADA";
                MsgBoxStyle tempRefParam11 = MsgBoxStyle.Exclamation;
                string tempRefParam12 = "ERROR DE CAPTURA";
                mdlGlobales.subDespErrores(ref tempRefParam10, ref tempRefParam11, ref tempRefParam12);
                mskFecIngresoCred.Focus();
                return result;
            }
            if (!funValidaFechaIngreso())
            {
                return result;
            }
            if (mskFecAceptaCred.Text == "__/__/____")
            {
                string tempRefParam13 = "LA 'FECHA DE ACEPTACIÓN DE CRÉDITO' DEBE SER CAPTURADA";
                MsgBoxStyle tempRefParam14 = MsgBoxStyle.Exclamation;
                string tempRefParam15 = "ERROR DE CAPTURA";
                mdlGlobales.subDespErrores(ref tempRefParam13, ref tempRefParam14, ref tempRefParam15);
                mskFecAceptaCred.Focus();
                return result;
            }
            if (!funValidaFechaAceptacion())
            {
                return result;
            }
            return true;
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Validar el Enter en el mskFecAceptaCred
        //*******************************************************************************
        private void mskFecAceptaCred_KeyPress(Object eventSender, KeyPressEventArgs eventArgs)
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

        public bool funValidaFechaAceptacion()
        {
            bool result = false;
            if (mskFecIngresoCred.Text == "__/__/____")
            {
                //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                string tempRefParam = "DEBE CAPTURAR LA 'FECHA DE INGRESO DEL CRÉDITO' ANTES DE CAPTURAR LA 'FECHA DE ACEPTACIÓN'";
                MsgBoxStyle tempRefParam2 = (MsgBoxStyle)(((int)MsgBoxStyle.Exclamation) + ((int)MsgBoxStyle.OkOnly));
                string tempRefParam3 = "VALIDACIÓN DE CONTEXTO";
                mdlGlobales.subDespErrores(ref tempRefParam, ref tempRefParam2, ref tempRefParam3);
                mskFecIngresoCred.Focus();
                return result;
            }
            if (!Information.IsDate(mskFecAceptaCred.Text))
            {
                string tempRefParam4 = "LA 'FECHA DE ACEPTACIÓN' DEBE SER DE TIPO 'DATE'";
                MsgBoxStyle tempRefParam5 = MsgBoxStyle.Exclamation;
                string tempRefParam6 = "ERROR DE CONTEXTO";
                mdlGlobales.subDespErrores(ref tempRefParam4, ref tempRefParam5, ref tempRefParam6);
                mskFecAceptaCred.Focus();
                return result;
            }
            System.DateTime FechaAceptacion = DateTime.Parse(mskFecAceptaCred.Text);
            System.DateTime FechaProceso = DateTime.Parse(mskFecProceso.Text);
            System.DateTime FechaIngreso = DateTime.Parse(mskFecIngresoCred.Text);
            if (FechaAceptacion > FechaProceso)
            {
                string tempRefParam7 = "LA 'FECHA DE ACEPTACIÓN' NO PUEDE SER MAYOR A LA 'FECHA DE PROCESO'";
                MsgBoxStyle tempRefParam8 = MsgBoxStyle.Exclamation;
                string tempRefParam9 = "ERROR DE CAPTURA";
                mdlGlobales.subDespErrores(ref tempRefParam7, ref tempRefParam8, ref tempRefParam9);
                mskFecAceptaCred.Focus();
                return result;
            }
            if (FechaAceptacion < FechaIngreso)
            {
                //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                string tempRefParam10 = "LA 'FECHA DE ACEPTACIÓN' NO PUEDE SER MENOR A LA 'FECHA DE INGRESO'";
                MsgBoxStyle tempRefParam11 = (MsgBoxStyle)(((int)MsgBoxStyle.Exclamation) + ((int)MsgBoxStyle.OkOnly));
                string tempRefParam12 = "ERROR DE CAPTURA";
                mdlGlobales.subDespErrores(ref tempRefParam10, ref tempRefParam11, ref tempRefParam12);
                mskFecAceptaCred.Focus();
                return result;
            }
            return true;
        }

        private void mskFecAceptaCred_Leave(Object eventSender, EventArgs eventArgs)
        {
            //UPGRADE_WARNING: (1041) ClipText has a new behavior.
            if (Artinsoft.VB6.Gui.MaskedTextBoxHelper.GetClipText(mskFecAceptaCred).Trim() == "")
            {
                mskFecAceptaCred.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
            if (!funValidaFechaAceptacion())
            {
                return;
            }
            //AIS-1255 FSABORIO
            mdlGlobales.gstrFechaAceptacion.Value = Artinsoft.VB6.Gui.MaskedTextBoxHelper.GetClipText(mskFecAceptaCred);
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Validar el Enter en el mskFecIngresoCred
        //*******************************************************************************
        private void mskFecIngresoCred_KeyPress(Object eventSender, KeyPressEventArgs eventArgs)
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

        private void mskFecIngresoCred_Leave(Object eventSender, EventArgs eventArgs)
        {
            //UPGRADE_WARNING: (1041) ClipText has a new behavior.
            if (Artinsoft.VB6.Gui.MaskedTextBoxHelper.GetClipText(mskFecIngresoCred).Trim() == "")
            {
                mskFecIngresoCred.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
            if (!funValidaFechaIngreso())
            {
                return;
            }
            //AIS-1255 FSABORIO
            mdlGlobales.gstrFechaIngreso.Value = Artinsoft.VB6.Gui.MaskedTextBoxHelper.GetClipText(mskFecIngresoCred);
        }

        private bool funValidaFechaIngreso()
        {
            bool result = false;
            if (!Information.IsDate(mskFecIngresoCred.Text))
            {
                string tempRefParam = "LA 'FECHA DE INGRESO DEL CRÉDITO' DEBE SER DE TIPO 'DATE'";
                MsgBoxStyle tempRefParam2 = MsgBoxStyle.Exclamation;
                string tempRefParam3 = "ERROR DE CONTEXTO";
                mdlGlobales.subDespErrores(ref tempRefParam, ref tempRefParam2, ref tempRefParam3);
                mskFecIngresoCred.Focus();
                return result;
            }
            System.DateTime FechaIngreso = DateTime.Parse(mskFecIngresoCred.Text);
            System.DateTime FechaProceso = DateTime.Parse(mskFecProceso.Text);
            if (FechaIngreso > FechaProceso)
            {
                string tempRefParam4 = "LA 'FECHA DE INGRESO DEL CRÉDITO' NO PUEDE SER MAYOR A LA 'FECHA DE PROCESO'";
                MsgBoxStyle tempRefParam5 = MsgBoxStyle.Exclamation;
                string tempRefParam6 = "ERROR DE CAPTURA";
                mdlGlobales.subDespErrores(ref tempRefParam4, ref tempRefParam5, ref tempRefParam6);
                mskFecIngresoCred.Focus();
                return result;
            }
            if ((FechaProceso - FechaIngreso).TotalDays > 15)
            {
                string tempRefParam7 = "LA 'FECHA DE INGRESO DEL CRÉDITO' NO PUEDE TENER MÁS DE 15 DÍAS NATURALES " + "\r" +
                                       "DE DIFERENCIA CON LA 'FECHA DE PROCESO'";
                MsgBoxStyle tempRefParam8 = MsgBoxStyle.Exclamation;
                string tempRefParam9 = "ERROR DE CONTEXTO";
                mdlGlobales.subDespErrores(ref tempRefParam7, ref tempRefParam8, ref tempRefParam9);
                mskFecIngresoCred.Focus();
                return result;
            }
            return true;
        }

        //AIS-1896 FSABORIO
        public bool SuspendFormClosing = false;
        private void frmRegRemesas_FormClosing(object sender, FormClosingEventArgs eventArgs)
        {
            //AIS-1896 FSABORIO
            SuspendFormClosing = true;
        }
    }
}