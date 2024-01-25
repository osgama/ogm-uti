using Microsoft.VisualBasic;
using System;
using System.Windows.Forms;
using VB6 = Microsoft.VisualBasic.Compatibility.VB6.Support;

namespace Masivos
{
    internal partial class frmConsRemesa
        : System.Windows.Forms.Form
    {

        //*******************************************************************************
        //* Identificación: frmConsRemesa                                               *
        //* Autor:          Luis E. Aguilar                                             *
        //* Instalación:    PRAXIS                                                      *
        //* Fecha:          5/09/2003                                                   *
        //* Versión:        1.0                                                         *
        //*******************************************************************************

        //*******************************************************************************
        //* Finalidad:  Subrutina para Limpiar Datos Cuando se Seleccione el Comando Limpiar
        //*******************************************************************************
        private void cmdLimpiar_Click(Object eventSender, EventArgs eventArgs)
        {
            subLimpiaDatos();
            txtClaveRemesa.Text = "";
            txtClaveRemesa.Focus();
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Procesar y Asignar la Prioridad a la Remesa
        //*******************************************************************************
        private void cmdProcesar_Click(Object eventSender, EventArgs eventArgs)
        {
            subAsignaPrioridad();
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Activar el MDI cuando se salga de esta Pantalla
        //*******************************************************************************
        private void cmdSalir_Click(Object eventSender, EventArgs eventArgs)
        {
            mdlGlobales.subMuestraMenu();
            mdlGlobales.gblnBandCancela = true;
            mdlGlobales.gblnChecaCancela = true;
            //AIS-1896 FSABORIO
            if (!this.SuspendFormClosing)
                this.Close();
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Inicializar los Valores de esta Pantalla
        //*******************************************************************************
        //UPGRADE_WARNING: (1041) Form_Load event was upgraded to Form_Load method and has a new behavior.
        private void Form_Load()
        {
            mdlGlobales.subOcultaBotones();
            mdlGlobales.gstrFolInterno.Value = mdlGlobales.funZeroes(8);
            mdlGlobales.CentrarMDIHija(MDIMasivos.DefInstance, frmConsRemesa.DefInstance);
            string tempRefParam = "";
            mdlGlobales.subDespMsg(ref tempRefParam);
            cmdProcesar.Visible = false;
            mnuProcesar.Available = false;
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Limpiar los Datos Capturables de esta Pantalla
        //*******************************************************************************
        public void subLimpiaDatos()
        {
            txtArchivo.Text = "";
            txtEstatus.Text = "";
            txtFolFinal.Text = "";
            txtFolInicial.Text = "";
            mskFecEstatus.Text = "__/__/____";
            mskFecProceso.Text = "__/__/____";
            cmdProcesar.Visible = false;
            mnuProcesar.Available = false;
        }

        private void Frame3_DoubleClick(Object eventSender, EventArgs eventArgs)
        {
            cmdProcesar.Visible = true;
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Activar el Menu de Limpiar
        //*******************************************************************************
        public void mnuLimpiar_Click(Object eventSender, EventArgs eventArgs)
        {
            cmdLimpiar_Click(cmdLimpiar, new EventArgs());
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Activar el Menú de Procesar
        //*******************************************************************************
        public void mnuProcesar_Click(Object eventSender, EventArgs eventArgs)
        {
            cmdProcesar_Click(cmdProcesar, new EventArgs());
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Activar el Menu de Salir
        //*******************************************************************************
        public void mnuSalir_Click(Object eventSender, EventArgs eventArgs)
        {
            cmdSalir_Click(cmdSalir, new EventArgs());
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Obtener la Clave de la Remesa Capturada
        //*******************************************************************************
        private void txtClaveRemesa_Enter(Object eventSender, EventArgs eventArgs)
        {
            mdlGlobales.subSelTexto(txtClaveRemesa);
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Validar que la Clave de la Remesa cumpla con los
        //*             Requisitos
        //*******************************************************************************
        private void txtClaveRemesa_KeyPress(Object eventSender, KeyPressEventArgs eventArgs)
        {
            int KeyAscii = (int)eventArgs.KeyChar;
            if (KeyAscii == 13)
            {
                if (funValidaObligatorios())
                {
                    subLimpiaDatos();
                    txtClaveRemesa.Text = mdlGlobales.funPoneCeros(txtClaveRemesa.Text, 18);
                    if (txtClaveRemesa.Text == "000000000000000000")
                    {
                        //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                        string tempRefParam = "Error: LA CLAVE DE REMESA NO DEBE SER 0's PARA LA CONSULTA.";
                        MsgBoxStyle tempRefParam2 = (MsgBoxStyle)(((int)MsgBoxStyle.Information) + ((int)MsgBoxStyle.OkOnly));
                        mdlGlobales.subDespErrores(ref tempRefParam, ref tempRefParam2);
                        txtClaveRemesa.Text = " ";
                        txtClaveRemesa.Focus();
                        if (KeyAscii == 0)
                        {
                            eventArgs.Handled = true;
                        }
                        return;
                    }
                    mdlGlobales.gstrTramite.Value = Strings.Mid(txtClaveRemesa.Text, 1, 2);
                    mdlGlobales.gstrEntOrig.Value = mdlGlobales.funPoneCeros(Strings.Mid(txtClaveRemesa.Text, 3, 2), 4);
                    mdlGlobales.gstrCveEntOrig.Value = Strings.Mid(txtClaveRemesa.Text, 5, 4);
                    mdlGlobales.gstrNumRemesa.Value = Strings.Mid(txtClaveRemesa.Text, 9, 10);
                    subConsultaEstatus();
                }
            }
            else
            {
                subLimpiaDatos();
            }
            //Funcion que Valida que Solo Sean Digitos
            string tempRefParam3 = mdlComunica.DIGITOS;
            int tempRefParam4 = -1;
            mdlGlobales.subFiltraCaptura(ref KeyAscii, ref tempRefParam3, ref tempRefParam4);
            if (KeyAscii == 0)
            {
                eventArgs.Handled = true;
            }
            eventArgs.KeyChar = Convert.ToChar(KeyAscii);
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Validar que la Clave de Remesa no sea 0's o este vacia
        //*******************************************************************************
        private bool funValidaObligatorios()
        {
            bool result = false;
            result = true;
            if (txtClaveRemesa.Text == "")
            {
                //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                string tempRefParam = "ERROR: DEBE CAPTURAR UNA CLAVE DE REMESA PARA LA CONSULTA.";
                MsgBoxStyle tempRefParam2 = (MsgBoxStyle)(((int)MsgBoxStyle.Information) + ((int)MsgBoxStyle.OkOnly));
                mdlGlobales.subDespErrores(ref tempRefParam, ref tempRefParam2);
                txtClaveRemesa.Focus();
                return false;
            }
            if (txtClaveRemesa.Text == "000000000000000000")
            {
                //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                string tempRefParam3 = "ERROR: LA CLAVE DE REMESA NO DEBE SER 0's PARA LA CONSULTA.";
                MsgBoxStyle tempRefParam4 = (MsgBoxStyle)(((int)MsgBoxStyle.Information) + ((int)MsgBoxStyle.OkOnly));
                mdlGlobales.subDespErrores(ref tempRefParam3, ref tempRefParam4);
                txtClaveRemesa.Focus();
                return false;
            }
            return result;
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Consultar el Estatus de la Remesa
        //*******************************************************************************
        private void subConsultaEstatus()
        {
            this.Cursor = Cursors.WaitCursor;
            if (mdlTranMasivo.funEnviaRecibe5562("5562", "04", mdlGlobales.gstrNumRemesa.Value))
            {
                //MMS 11/05 Se recorre la posición del campo Clave Respuesta (50 a 51)
                if ((Strings.Mid(mdlTranMasivo.gvRecive5562_04, 51, 2) == "00") && (Strings.Mid(mdlTranMasivo.gvRecive5562_04, 177, 16) == "0000000000000000"))
                {
                    mdlGlobales.subDespMensajes("LA REMESA NO EXISTE, FAVOR DE REINTENTAR ...");
                    txtClaveRemesa.Focus();
                    //MMS 11/05 Se recorre la posición del campo Clave Respuesta (50 a 51)
                }
                else if ((Strings.Mid(mdlTranMasivo.gvRecive5562_04, 51, 2) == "00") && (Strings.Mid(mdlTranMasivo.gvRecive5562_04, 177, 16) != "0000000000000000"))
                {
                    subPresentaInfo5562_04();
                }
            }
            else
            {
                mdlGlobales.subDespMensajes(" ");
            }
            this.Cursor = Cursors.Default;
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Desplegar la Informacion en los Campos Correspondientes
        //*******************************************************************************
        private void subPresentaInfo5562_04()
        {
            string strEstatus = Strings.Mid(mdlTranMasivo.gvRecive5562_04, 193, 2);
            switch (strEstatus)
            {
                case "01":
                    txtEstatus.Text = "01 - ASIGNADA";
                    break;
                case "02":
                    txtEstatus.Text = "02 - GENERADA";
                    break;
                case "03":
                    txtEstatus.Text = "03 - DISPONIBLE";
                    cmdProcesar.Visible = true;
                    mnuProcesar.Available = true;
                    break;
                default:
                    txtEstatus.Text = "04 - REGISTRADA";
                    break;
            }
            // Se Realiza esta validacion Para desplegar en Pantalla la Fecha DD/MM/AAAA
            mskFecEstatus.Text = Strings.Mid(Strings.Mid(mdlTranMasivo.gvRecive5562_04, 195, 8).Trim(), 7, 2) + "/" + Strings.Mid(Strings.Mid(mdlTranMasivo.gvRecive5562_04, 195, 8).Trim(), 5, 2) + "/" + Strings.Mid(Strings.Mid(mdlTranMasivo.gvRecive5562_04, 195, 8).Trim(), 1, 4);
            txtArchivo.Text = Strings.Mid(mdlTranMasivo.gvRecive5562_04, 203, 8);
            txtFolInicial.Text = Strings.Mid(mdlTranMasivo.gvRecive5562_04, 211, 16);
            txtFolFinal.Text = Strings.Mid(mdlTranMasivo.gvRecive5562_04, 227, 16);
            string strNumRemesa = Strings.Mid(mdlTranMasivo.gvRecive5562_04, 177, 16);
            //Despliegue de la Fecha de Proceso Saliente del Numero de Remesa
            mskFecProceso.Text = Strings.Mid(Strings.Mid(strNumRemesa.Trim(), 7, 8).Trim(), 7, 2) + "/" + Strings.Mid(Strings.Mid(strNumRemesa.Trim(), 7, 8).Trim(), 5, 2) + "/" + Strings.Mid(Strings.Mid(strNumRemesa.Trim(), 7, 8).Trim(), 1, 4);
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Asignar la Prioridad a la Remesa
        //*******************************************************************************
        private void subAsignaPrioridad()
        {
            this.Cursor = Cursors.WaitCursor;
            string strDatos = mdlTranMasivo.funArmaPrioridad(frmConsRemesa.DefInstance);
            if (mdlTranMasivo.funEnviaRecibe5562("5562", "06", strDatos))
            {
                //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                string tempRefParam = "TRANSACCION EXITOSA: PRIORIDAD ALTA ";
                MsgBoxStyle tempRefParam2 = (MsgBoxStyle)(((int)MsgBoxStyle.Information) + ((int)MsgBoxStyle.OkOnly));
                string tempRefParam3 = "TRANSACCION EJECUTADA";
                mdlGlobales.subDespErrores(ref tempRefParam, ref tempRefParam2, ref tempRefParam3);
                subLimpiaDatos();
                cmdProcesar.Visible = false;
                mnuProcesar.Available = false;
                txtClaveRemesa.Focus();
            }
            else
            {
                //MMS 11/05 Se recorre la posición del campo Descripción Resultado (52 a 53)
                //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                string tempRefParam4 = Strings.Mid(mdlTranMasivo.gvRecive5562_06, 53, 50);
                MsgBoxStyle tempRefParam5 = (MsgBoxStyle)(((int)MsgBoxStyle.Exclamation) + ((int)MsgBoxStyle.OkOnly));
                mdlGlobales.subDespErrores(ref tempRefParam4, ref tempRefParam5);
                cmdProcesar.Focus();
                //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                string tempRefParam6 = "";
                MsgBoxStyle tempRefParam7 = (MsgBoxStyle)(((int)MsgBoxStyle.Information) + ((int)MsgBoxStyle.OkOnly));
                mdlGlobales.subDespErrores(ref tempRefParam6, ref tempRefParam7);
            }
            this.Cursor = Cursors.Default;
        }

        //AIS-1896 FSABORIO
        public bool SuspendFormClosing = false;
        private void frmConsRemesa_FormClosing(object sender, FormClosingEventArgs eventArgs)
        {
            //AIS-1896 FSABORIO
            SuspendFormClosing = true;
        }
    }
}