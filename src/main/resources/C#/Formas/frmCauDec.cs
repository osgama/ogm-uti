using Artinsoft.VB6.Utils;
using Microsoft.VisualBasic;
using System;
using System.Windows.Forms;
using VB6 = Microsoft.VisualBasic.Compatibility.VB6.Support;

namespace Masivos
{
    internal partial class frmCausasDec
        : System.Windows.Forms.Form
    {

        //*******************************************************************************
        //* Identificación: frmCausasDec                                                *
        //* Autor:          Luis E. Aguilar                                             *
        //* Instalación:    PRAXIS                                                      *
        //* Fecha:          17/09/2003                                                  *
        //* Versión:        1.0                                                         *
        //*******************************************************************************

        //*******************************************************************************
        //* Finalidad:  Subrutina que Realiza las validaciones cuando se da Click
        //*                  y se Selecciona la Clave de la Declinacion
        //*******************************************************************************
        private void fgridCausasDec_ClickEvent(Object eventSender, EventArgs eventArgs)
        {
            mdlGlobales.gblnSelecciono = true;
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina que Realiza las validaciones cuando se da Doble Click
        //*                  y se Selecciona la Clave de la Declinacion
        //*******************************************************************************
        private void fgridCausasDec_DblClick(Object eventSender, EventArgs eventArgs)
        {
            mdlGlobales.gblnSelecciono = true;
            subEligeCausaDeclina();
        }

        //*******************************************************************************
        //* Finalidad: Subrutina que Valida cuando se de un Enter o Cuando se de un Escape
        //*                  en la Pantalla
        //*******************************************************************************
        private void fgridCausasDec_KeyDownEvent(Object eventSender, AxMSFlexGridLib.DMSFlexGridEvents_KeyDownEvent eventArgs)
        {
            if (eventArgs.keyCode == ((short)Keys.Return))
            {
                fgridCausasDec_DblClick(fgridCausasDec, new EventArgs());
            }
            if (eventArgs.keyCode == ((short)Keys.Escape))
            {
                //AIS-1896 FSABORIO
                if (!frmCausasDec.DefInstance.SuspendFormClosing)
                    frmCausasDec.DefInstance.Close();
            }
        }

        //*******************************************************************************
        //* Finalidad: Subrutina para validar cuando la forma pierde el control del sistema
        //*******************************************************************************
        //UPGRADE_WARNING: (2065) Form event frmCausasDec.Deactivate has a new behavior.
        private void frmCausasDec_Deactivate(Object eventSender, EventArgs eventArgs)
        {
            mdlGlobales.gblnSelecciono = false;
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina que Realiza las Validaciones Iniciales de la Pantalla
        //*******************************************************************************
        //UPGRADE_WARNING: (1041) Form_Load event was upgraded to Form_Load method and has a new behavior.
        private void Form_Load()
        {
            mdlGlobales.subOcultaBotones();
            //Ocultar los menús
            mdlGlobales.subOcultaMenu(false);
            mdlGlobales.CentrarMDIHija(MDIMasivos.DefInstance, frmCausasDec.DefInstance);
            subConfGridCD();
            if (mdlGlobales.gbolCDecision)
            {
                if (mdlGlobales.gbolRecon)
                {
                    frmCausasDec.DefInstance.Text = "Claves de Reconsideración";
                    string tempRefParam = "78";
                    subCargaReconExcep(ref tempRefParam);
                }
                else if (mdlGlobales.gbolExcep)
                {
                    frmCausasDec.DefInstance.Text = "Claves de Excepciones";
                    string tempRefParam2 = "79";
                    subCargaReconExcep(ref tempRefParam2);
                }
            }
            else
            {
                string tempRefParam3 = "25";
                subCargaReconExcep(ref tempRefParam3);
            }
            mdlGlobales.gblnSelecciono = false;
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina que Carga el DBG con Claves y Exepciones
        //*******************************************************************************
        private void subCargaReconExcep(ref  string strCatalogo)
        {
            int intCont = 0;
            int kta = 1;
            bool blnChecaEntrada = false;
            string tempRefParam = "D";
            mdlComunica.OleCatalogos.AbreCatalogo(ref strCatalogo, ref tempRefParam);
            while (!mdlComunica.OleCatalogos.EOF_Renamed())
            {

                fgridCausasDec.Row = kta;
                fgridCausasDec.Col = 0;
                fgridCausasDec.Text = mdlComunica.OleCatalogos.getLlave1.Substring(mdlComunica.OleCatalogos.getLlave1.Length - Math.Min(mdlComunica.OleCatalogos.getLlave1.Length, 4));
                fgridCausasDec.Col = 1;
                fgridCausasDec.Text = mdlComunica.OleCatalogos.getDescripcion.Trim();
                //Agregamos otro renglon
                fgridCausasDec.Rows++;
                kta++;
                mdlComunica.OleCatalogos.MoveNext();
            }
            mdlComunica.OleCatalogos.CierraCatalogo();
            fgridCausasDec.Rows = kta;
            fgridCausasDec.Row = 1;
            fgridCausasDec.Col = 1;
        }

        //*******************************************************************************
        //* Finalidad: Subrutina que Inicializa el DBG de Causas de Declinacion
        //*******************************************************************************
        private void subConfGridCD()
        {
            // Inicializa al Grid de Causas de Declinación
            fgridCausasDec.set_ColWidth(0, 600); //Clave de Causa de Declinación
            fgridCausasDec.set_ColWidth(1, 4500); //No. de Folio Interno
            //Pone encabezados al Grid de Causas de Declinación
            fgridCausasDec.Col = 0;
            fgridCausasDec.Row = 0;
            fgridCausasDec.Text = "Clave";
            fgridCausasDec.Col = 1;
            fgridCausasDec.Text = "Descripción";
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina que Realiza las Validaciones Cuando se Cierra esta Pantalla
        //*******************************************************************************
        bool closingAllowed = true;
        //AIS-1896 FSABORIO
        public bool SuspendFormClosing = false;
        private void frmCausasDec_FormClosing(Object eventSender, FormClosingEventArgs eventArgs)
        {
            //AIS-1896 FSABORIO
            SuspendFormClosing = true;

            if (!MDIMasivos.DefInstance.closing && frmCausasDec.DefInstance.closingAllowed)
            {
                frmCausasDec.DefInstance.closingAllowed = false;

                int Cancel = (eventArgs.Cancel) ? 1 : 0;
                int UnloadMode = (int)eventArgs.CloseReason;
                if (UnloadMode != 1)
                {
                    subEligeCausaDeclina();
                }
                eventArgs.Cancel = Cancel != 0;
            }
            //AIS-1896 FSABORIO
            SuspendFormClosing = !eventArgs.Cancel;
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina que Realiza las validaciones para Conocer las Causas
        //*                  de Declinacion
        //*******************************************************************************
        private void subEligeCausaDeclina()
        {
            string strDatos = String.Empty;
            string strTitle = String.Empty;
            string strMsg = String.Empty;
            string strDgDef = String.Empty;
            string strResponse = String.Empty;

            mdlGlobales.gstrClaveDeclina = "";
            mdlGlobales.gstrCausaDec.Value = "0000";
            fgridCausasDec.Col = 0;
            string strCve = fgridCausasDec.Text;
            fgridCausasDec.Col = 1;
            string strTxt = fgridCausasDec.Text;
            //ASG 20040107 condición para corroborar que en efecto se seleccionó una causa
            //AIS-CONVERSION laralar
            if (StringsHelper.DoubleValue(strCve.Trim()) == 0 || strTxt.Trim() == "0" || !mdlGlobales.gblnSelecciono)
            {
                //ASG 20040107 Mostrar la pantalla que mandó a llamar el proceso
                if (mdlTranMasivo.gstrFormActivo == "PredFolio")
                {
                    frmPredFolio.DefInstance.Show();
                }
                else
                {
                    frmRegRemesas.DefInstance.Show();
                }
                //AIS-1896 FSABORIO
                if (!frmCausasDec.DefInstance.SuspendFormClosing)
                    frmCausasDec.DefInstance.Close();
            }
            else
            {
                //ASG 20040107 Limpiar el indicador de selección de causas de declinación
                mdlGlobales.gblnSelecciono = false;
                strTitle = "CAUSAS DE DECLINACION";
                if (mdlTranMasivo.gstrFormActivo == "PredFolio")
                {
                    strMsg = "EL FOLIO SERA DECLINADO CON LA CAUSA: " + strCve.Trim() + "-" + strTxt.Trim();
                }
                else
                {
                    strMsg = "LA REMESA SERA DECLINADA CON LA CAUSA: " + strCve.Trim() + "-" + strTxt.Trim();
                }
                strDgDef = (((int)MsgBoxStyle.YesNo) + ((int)MsgBoxStyle.Question) + ((int)MsgBoxStyle.DefaultButton2)).ToString();
                //UPGRADE_WARNING: (6021) Casting 'string' to Enum may cause different behaviour.
                //UPGRADE_WARNING: (1046) MsgBox Parameter 'context' is not supported, and was removed.
                //UPGRADE_WARNING: (1046) MsgBox Parameter 'helpfile' is not supported, and was removed.
                strResponse = ((int)Interaction.MsgBox(strMsg, (MsgBoxStyle)Int32.Parse(strDgDef), strTitle)).ToString();
                if (strResponse == ((int)System.Windows.Forms.DialogResult.Yes).ToString())
                {
                    this.Cursor = Cursors.WaitCursor;
                    if (mdlTranMasivo.gstrFormActivo == "PredFolio")
                    {
                        mdlGlobales.gstrClaveDeclina = strCve.Trim();
                        mdlGlobales.gstrCausaDec.Value = strTxt.Trim();
                        frmPredFolio.DefInstance.Visible = true; //MMS-BCM Jun2006 estaba comentada esta linea y por lo tanto al declinar, los botones de la barra se quedaban deshabilitados
                        frmPredFolio.DefInstance.subDeclinaFolio();
                        frmPredFolio.DefInstance.subLimpiaDatos();
                        this.Cursor = Cursors.Default;
                        this.Hide();
                        return;
                    }
                    else
                    {
                        //Varible que Guarda la Clave de la Declinacion Seleccionada
                        mdlGlobales.gstrClaveDeclina = strCve.Trim();
                        mdlGlobales.gstrCausaDec.Value = strCve.Trim();
                        this.Hide();
                        mdlGlobales.gblnRemesaRegistrada = true;
                        mdlGlobales.gblnEjecutaRegistro = true;
                        frmRegRemesas.DefInstance.cmdDeclinar.Enabled = false;
                        //AIS-1896 FSABORIO
                        if (!frmRegRemesas.DefInstance.SuspendFormClosing)
                            frmRegRemesas.DefInstance.Close();
                        frmProcMasivo.DefInstance.Show();
                    }
                }
                else
                {
                    if (mdlTranMasivo.gstrFormActivo == "PredFolio")
                    {
                        mdlGlobales.gstrClaveDeclina = "";
                        mdlGlobales.gstrCausaDec.Value = "0000";
                        mdlCatalogos.gstrCatProceso = "";
                        frmPredFolio.DefInstance.Visible = true;
                        this.Cursor = Cursors.Default;
                        this.Hide();
                        return;
                    }
                    else
                    {
                        mdlGlobales.gstrClaveDeclina = "";
                        mdlGlobales.gstrCausaDec.Value = "0000";
                        mdlCatalogos.gstrCatProceso = "";
                        frmRegRemesas.DefInstance.Show();
                        frmRegRemesas.DefInstance.cmdDeclinar.Focus();
                        this.Cursor = Cursors.Default;
                        //AIS-1896 FSABORIO
                        if (!frmCausasDec.DefInstance.SuspendFormClosing)
                            frmCausasDec.DefInstance.Close();
                    }
                }
            }
        }

        private void frmCausasDec_Closed(Object eventSender, EventArgs eventArgs)
        {
            mdlGlobales.subOcultaMenu(true);
            MemoryHelper.ReleaseMemory();
        }

        private void frmCausasDec_Load(object sender, EventArgs e)
        {
            frmCausasDec.DefInstance.closingAllowed = true;
        }
    }
}