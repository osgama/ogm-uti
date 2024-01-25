using Artinsoft.VB6.Utils;
using Microsoft.VisualBasic;
using System;
using System.Globalization;
using System.Windows.Forms;
using VB6 = Microsoft.VisualBasic.Compatibility.VB6.Support;
using System.IO;

namespace Masivos
{
    internal partial class frmProcMasivo
        : System.Windows.Forms.Form
    {

        //*******************************************************************************
        //* Identificación: frmProcMasivo.frm                                           *
        //* Autor:          L.I.A. Israel Javier Garcés Morales                                *
        //* Instalación:    BANAMEX, S.A.                                               *
        //* Fecha:          09/09/2003                                                  *
        //* Versión:        1.0                                                         *
        //*******************************************************************************
        //* Objetivo: Seleccion del Archivo de Promotora a  procesar, de acuerdo al tra-*
        //*           mite seleccionado. Se realizan las validaciones minimas para la   *
        //*           aceptacion del archivo (validacion de header y trailer),naturaleza*
        //*           de los datos, información contra catalogos,  etc.                 *
        //*           Si el archivo es aceptado (al menos se cumple con el % de solici- *
        //*           tudes correctas -parametro por catalogo) y la remesa no existe, se*
        //*           procede a generarla. Si la remesa ya existe se presenta la informa*
        //*           cion correspondiente en pantalla.                                 *
        //*           Si existen remesas rechazadas se genera un archivo de errores     *
        //*******************************************************************************

        //*****************************************************************************************************
        //* Finalidad:  Subrutina para procesar las acciones cuando se presenta la selección de un tipo de trámite
        //*****************************************************************************************************
        private void cboTipoTram_SelectedIndexChanged(Object eventSender, EventArgs eventArgs)
        {
            subLimpiarDatos();
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
        //* Finalidad:  Subrutina para atrapar la tecla ESC desde el control cboTipoTram
        //*****************************************************************************************************
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
        //* Finalidad:  Subrutina para procesar la tecla ENTER, y hacerla funcionar como TAB
        //*****************************************************************************************************
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
        //* Finalidad:  Subrutina para limpiar los controles de la pantalla
        //*****************************************************************************************************
        public void subLimpiarDatos()
        {
            txtArchivo.Text = "";
            txtEntidadOrigen.Text = "";
            txtFamiliaProducto.Text = "";
            txtFolFinal.Text = "";
            txtFolInicial.Text = "";
            txtNumSolicitudes.Text = "0";
            txtSolAceptadas.Text = "0";
            txtSolRechazadas.Text = "0";
            txtRemesa.Text = "";
            txtTipoEntidad.Text = "";
            txtTipoSolicitud.Text = "";
            txtTipoTram.Text = "";
            lblGrabado.Visible = true;
            lblGrabado.Text = "";
            mdlGlobales.subDespMensajes("");
        }

        //*****************************************************************************************************
        //* Finalidad:  Subrutina para preparar la pantalla al ser cargada
        //*****************************************************************************************************
        //UPGRADE_WARNING: (1041) Form_Load event was upgraded to Form_Load method and has a new behavior.
        private void Form_Load()
        {
            mdlGlobales.CentrarMDIHija(MDIMasivos.DefInstance, frmProcMasivo.DefInstance);
            mdlGlobales.subOcultaBotones();
            string tempRefParam = "";
            mdlGlobales.subDespMsg(ref tempRefParam);
            subLimpiarDatos();
            mdlComunica.OleCatalogos.setAlineacionClave = Catalogos.clsCatalogos.enmAlineaciones.Izquierda;
            mdlComunica.OleCatalogos.setLongClave = 2;
            object tempRefParam2 = cboTipoTram.Text;
            string tempRefParam3 = "11";
            string tempRefParam4 = String.Empty;
            string tempRefParam5 = String.Empty;
            string tempRefParam6 = String.Empty;
            string tempRefParam7 = String.Empty;
            string tempRefParam8 = "D";
            mdlComunica.OleCatalogos.LlenaCombo(ref cboTipoTram, ref tempRefParam3, ref tempRefParam4, ref tempRefParam5, ref tempRefParam6, ref tempRefParam7, ref tempRefParam8);
            if (Conversion.Val(mdlGlobales.gstrTramite.Value) != 0)
            {
                cboTipoTram.SelectedIndex = mdlComunica.OleCatalogos.ObtenIdxCombo(cboTipoTram, mdlGlobales.gstrTramite.Value);
            }
            this.ActiveControl = this.cboTipoTram;
        }

        //*****************************************************************************************************
        //* Finalidad:  Subrutina para procesar el archivo de promotora después de haber generado la remesa correspondiente
        //*****************************************************************************************************
        private void cmdLeeArchivo_Enter(Object eventSender, EventArgs eventArgs)
        {
            mdlPromotora.subProcesaArchivoPromotora();
        }

        //*****************************************************************************************************
        //* Finalidad:  Subrutina para procesar y validar el archivo leído
        //*****************************************************************************************************
        private void cmdLeeArchivo_Click(Object eventSender, EventArgs eventArgs)
        {
            string strReg = String.Empty;
            string strFolio = String.Empty;
            int intFileRead = 0;
            int intTotalReg01 = 0;
            int intTotalReg03 = 0;
            int intTotalReg05 = 0;
            int intTotalReg10 = 0;
            int intTotalReg11 = 0;
            int intTotalReg12 = 0;
            int intTotalReg14 = 0;
            int intTotalReg15 = 0;
            int intTotalReg16 = 0;
            int intTotalReg17 = 0;
            string strCvePromocion = String.Empty;
            string strTipoComision = String.Empty;
            string strUltimoFolio = String.Empty;
            //int intCont = 0;
            string strCadena = String.Empty;
            string strDatos = String.Empty;
            string strCveResp = String.Empty;
            string strRemesa = String.Empty;
            //int intContador = 0;
            string strNombre = String.Empty;
            string strPaterno = String.Empty;
            string strMaterno = String.Empty;
            bool blnFolioIniOk = false;
            bool blnFolioFinOk = false;
            //bool blnProductoOk = false;
            //int intEncontre = 0;
            int intNumRegistro = 0;

            try
            {
                subLimpiarDatos();
                if (cboTipoTram.Text.Trim().Length == 0)
                {
                    //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                    string tempRefParam = "SELECCIONE 'TIPO DE TRAMITE'";
                    MsgBoxStyle tempRefParam2 = (MsgBoxStyle)(((int)MsgBoxStyle.Information) + ((int)MsgBoxStyle.OkOnly));
                    mdlGlobales.subDespErrores(ref tempRefParam, ref tempRefParam2);
                    cboTipoTram.Focus();
                    return;
                }

                mdlGlobales.gblnRemesaRegistrada = false;
                mdlGlobales.gblnEjecutaRegistro = false;

                dlgArchivoAbrir.CancelError = true;
                dlgArchivoAbrir.Flags = (int)MSComDlg.FileOpenConstants.cdlOFNHideReadOnly;
                dlgArchivoAbrir.Filter = "Todos los Archivos (*.*)|*.*|Archivos de Texto (*.txt)|*.txt";
                dlgArchivoAbrir.Filter = "Archivos de Texto (*.txt)|*.txt";
                dlgArchivoAbrir.FilterIndex = 2;
                dlgArchivoAbrir.ShowOpen();

                 //MODIF MAP 2010/OCT/06
                //string stFileRemesa= dlgArchivoAbrir.FileName.ToString();
                //string stFileRemesaTemp = stFileRemesa.Substring(stFileRemesa.IndexOf("\\") + 2);
                //string stFileNewWay = @System.Environment.GetEnvironmentVariable("HOMEDRIVE") +
                //                        System.Environment.GetEnvironmentVariable("HOMEPATH") +
                //                        "\\Masivos\\" + stFileRemesaTemp;
                //File.Copy(stFileRemesa, stFileNewWay);
                //dlgArchivoAbrir.FileName = stFileNewWay;

                if (Strings.Len(dlgArchivoAbrir.FileName) == 0)
                {
                    //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                    string tempRefParam3 = "No seleccionó ningún archivo";
                    MsgBoxStyle tempRefParam4 = (MsgBoxStyle)(((int)MsgBoxStyle.Information) + ((int)MsgBoxStyle.OkOnly));
                    mdlGlobales.subDespErrores(ref tempRefParam3, ref tempRefParam4);
                    cmdLeeArchivo.Focus();
                    return;
                }
                txtArchivo.Text = dlgArchivoAbrir.FileName;

                mdlGlobales.subDespMensajes("VALIDANDO ARCHIVO DE REMESA...");
                this.Cursor = Cursors.WaitCursor;

                intFileRead = FileSystem.FreeFile();
                FileSystem.FileOpen(intFileRead, txtArchivo.Text, OpenMode.Input, OpenAccess.Default, OpenShare.Default, -1);

                intTotalReg01 = 0;
                blnFolioIniOk = false;
                blnFolioFinOk = false;
                //blnProductoOk = false;

                while (!FileSystem.EOF(intFileRead))
                {

                    strCadena = FileSystem.LineInput(intFileRead);
                    strReg = Strings.Mid(strCadena, 1, 2);
                    strFolio = Strings.Mid(strCadena, 3, 8);
                    if (strFolio.Trim().Length == 8 && strReg == "01")
                    {
                        strUltimoFolio = strFolio;
                        intTotalReg03 = 0;
                        intTotalReg05 = 0;
                        intTotalReg10 = 0;
                        intTotalReg11 = 0;
                        intTotalReg12 = 0;
                        intTotalReg14 = 0;
                        intTotalReg15 = 0;
                        intTotalReg16 = 0;
                        intTotalReg17 = 0;
                    }

                    double dbNumericTemp = 0;
                    if (Double.TryParse(strReg, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp))
                    {
                        intNumRegistro = Int32.Parse(strReg);
                        //MODIF MAP ART.115 2016
                        //if (intNumRegistro >= 20 && intNumRegistro != 99)
                        if (intNumRegistro >= 22 && intNumRegistro != 99)
                        {
                            //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                            string tempRefParam5 = "ERROR REGISTRO " + strReg + " NO VÁLIDO";
                            MsgBoxStyle tempRefParam6 = (MsgBoxStyle)(((int)MsgBoxStyle.Exclamation) + ((int)MsgBoxStyle.OkOnly));
                            mdlGlobales.subDespErrores(ref tempRefParam5, ref tempRefParam6);
                            this.Cursor = Cursors.Default;
                            return;
                        }
                    }
                    else
                    {
                        //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                        string tempRefParam7 = "ERROR, REGISTRO NO NUMÉRICO";
                        MsgBoxStyle tempRefParam8 = (MsgBoxStyle)(((int)MsgBoxStyle.Exclamation) + ((int)MsgBoxStyle.OkOnly));
                        mdlGlobales.subDespErrores(ref tempRefParam7, ref tempRefParam8);
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    switch (strReg)
                    {
                        case "00":  //Header del Archivo 
                            mdlTranMasivo.subArmaReg(strCadena);
                            txtFolInicial.Text = mdlGlobales.funPoneCeros(mdlTranMasivo.estPromHeader.strFolioInicial.Value, 16);
                            txtFolFinal.Text = mdlGlobales.funPoneCeros(mdlTranMasivo.estPromHeader.strFolioFinal.Value, 16);
                            txtTipoSolicitud.Text = mdlTranMasivo.estPromHeader.strTipoSolicitud.Value;

                            mdlGlobales.gstrFolPreImpIni.Value = txtFolInicial.Text;
                            mdlGlobales.gstrFolPreImpFin.Value = txtFolFinal.Text;

                            if (!mdlCatalogos.funValidaCatalogoProductos())
                            {
                                subLimpiarDatos();
                                cmdLeeArchivo.Focus();
                                // MOPDIF MAP 2011/10/03 SI MARCA ERROR DE CATALOGO DE PRODUCTO SE DEJA EL ARCHIVO ABIERTO, EN TEORIA SE DEBE CERRAR AQUI
                                FileSystem.FileClose(intFileRead);
                                this.Cursor = Cursors.Default;
                                return;
                            }
                            txtTipoSolicitud.Text = mdlCatalogos.gstrTipSol + " " + mdlCatalogos.gstrDescTipoSol;
                            prg_proceso.Max = (float)(Double.Parse(txtFolFinal.Text) - Double.Parse(txtFolInicial.Text) + 1);
                            prg_proceso.Value = 0;

                            break;
                        case "99":  //valida trailer 
                            mdlTranMasivo.subArmaReg(strCadena);
                            txtNumSolicitudes.Text = mdlPromotora.estPromTrailer.strCantSolic.Value;
                            blnFolioFinOk = Int32.Parse(txtNumSolicitudes.Text) == intTotalReg01 && txtFolFinal.Text == mdlGlobales.funPoneCeros(strUltimoFolio, 16);

                            break;
                        case "01":  // Valida Folios 
                            strNombre = Strings.Mid(strCadena, 57, 50).TrimEnd() + " "; //AEFS Cambia long de 30 a 50
                            strPaterno = Strings.Mid(strCadena, 107, 60).TrimEnd() + " "; //AEFS Cambia pos de 87 a 107 y long de 30 a 60
                            strMaterno = Strings.Mid(strCadena, 167, 60).TrimEnd(); //AEFS Cambia pos de 117 a 167 y long de 30 a 60
                            mdlGlobales.gstrFechaSolicitud.Value = Strings.Mid(strCadena, 43, 6).TrimEnd();
                            if (Conversion.Val(Strings.Mid(mdlGlobales.gstrFechaSolicitud.Value, 1, 2)) > 50)
                            {
                                mdlGlobales.gstrFechaSolicitud.Value = "19" + mdlGlobales.gstrFechaSolicitud.Value;
                            }
                            else
                            {
                                mdlGlobales.gstrFechaSolicitud.Value = "20" + mdlGlobales.gstrFechaSolicitud.Value;
                            }
                            //UPGRADE_WARNING: (1042) Array estMasivo may need to have individual elements initialized. 
                            mdlTranMasivo.estMasivo = ArraysHelper.RedimPreserve<mdlTranMasivo.udtMasivo[]>(mdlTranMasivo.estMasivo, new int[] { intTotalReg01 + 2 });
                            mdlTranMasivo.estMasivo[intTotalReg01].strFolioPreimpreso = Strings.Mid(strCadena, 3, 8);
                            mdlTranMasivo.estMasivo[intTotalReg01].strNombreSolicitante = strNombre + strPaterno + strMaterno;
                            mdlTranMasivo.estMasivo[intTotalReg01].strCausaDeclinacion = "";
                            intTotalReg01++;
                            if (intTotalReg01 == 1 && txtFolInicial.Text == mdlGlobales.funPoneCeros(strFolio, 16))
                            {
                                blnFolioIniOk = true;
                            }
                            break;
                        case "03":  // Valida No. de Registro x Solicitud 
                            intTotalReg03++;
                            if ((strUltimoFolio == strFolio) && (intTotalReg03 > 1))
                            {
                                string tempRefParam9 = "SE RECHAZA LA REMESA -> FOLIO " + strFolio + " REGISTRO 03 (EMP. ACT.): NO. DE REGISTROS DIFERENTES DE UNO";
                                MsgBoxStyle tempRefParam10 = MsgBoxStyle.Exclamation;
                                string tempRefParam11 = "";
                                mdlGlobales.subDespErrores(ref tempRefParam9, ref tempRefParam10, ref tempRefParam11);
                                subLimpiarDatos();
                                cmdLeeArchivo.Focus();
                                this.Cursor = Cursors.Default;
                                return;
                            }
                            break;
                        case "05":  // Valida No. de Registro x Solicitud 
                            intTotalReg05++;
                            if ((strUltimoFolio == strFolio) && (intTotalReg05 > 1))
                            {
                                string tempRefParam12 = "SE RECHAZA LA REMESA -> FOLIO " + strFolio + " REGISTRO 05 (DAT. CON.): NO. DE REGISTROS DIFERENTES DE UNO";
                                MsgBoxStyle tempRefParam13 = MsgBoxStyle.Exclamation;
                                string tempRefParam14 = "";
                                mdlGlobales.subDespErrores(ref tempRefParam12, ref tempRefParam13, ref tempRefParam14);
                                subLimpiarDatos();
                                cmdLeeArchivo.Focus();
                                this.Cursor = Cursors.Default;
                                return;
                            }
                            break;
                        case "10":  // Valida No. de Registro x Solicitud 
                            intTotalReg10++;
                            if ((strUltimoFolio == strFolio) && (intTotalReg10 > 1))
                            {
                                string tempRefParam15 = "SE RECHAZA LA REMESA -> FOLIO " + strFolio + " REGISTRO 10 (PROP.): NO. DE REGISTROS DIFERENTES DE UNO";
                                MsgBoxStyle tempRefParam16 = MsgBoxStyle.Exclamation;
                                string tempRefParam17 = "";
                                mdlGlobales.subDespErrores(ref tempRefParam15, ref tempRefParam16, ref tempRefParam17);
                                subLimpiarDatos();
                                cmdLeeArchivo.Focus();
                                this.Cursor = Cursors.Default;
                                return;
                            }
                            break;
                        case "11":  // Valida No. de Registro x Solicitud 
                            intTotalReg11++;
                            if ((strUltimoFolio == strFolio) && (intTotalReg11 > 1))
                            {
                                string tempRefParam18 = "SE RECHAZA LA REMESA -> FOLIO " + strFolio + " REGISTRO 11 (ING. EGR.): NO. DE REGISTROS DIFERENTES DE UNO";
                                MsgBoxStyle tempRefParam19 = MsgBoxStyle.Exclamation;
                                string tempRefParam20 = "";
                                mdlGlobales.subDespErrores(ref tempRefParam18, ref tempRefParam19, ref tempRefParam20);
                                subLimpiarDatos();
                                cmdLeeArchivo.Focus();
                                this.Cursor = Cursors.Default;
                                return;
                            }
                            break;
                        case "12":  // Valida No. de Registro x Solicitud 
                            intTotalReg12++;
                            if ((strUltimoFolio == strFolio) && (intTotalReg12 > 1))
                            {
                                string tempRefParam21 = "SE RECHAZA LA REMESA -> FOLIO " + strFolio + " REGISTRO 12 (TAR. ADIC.): NO. DE REGISTROS DIFERENTES DE UNO";
                                MsgBoxStyle tempRefParam22 = MsgBoxStyle.Exclamation;
                                string tempRefParam23 = "";
                                mdlGlobales.subDespErrores(ref tempRefParam21, ref tempRefParam22, ref tempRefParam23);
                                subLimpiarDatos();
                                cmdLeeArchivo.Focus();
                                this.Cursor = Cursors.Default;
                                return;
                            }
                            break;
                        case "14":  // Valida No. de Registro x Solicitud 
                            intTotalReg14++;
                            if ((strUltimoFolio == strFolio) && (intTotalReg14 > 1))
                            {
                                string tempRefParam24 = "SE RECHAZA LA REMESA -> FOLIO " + strFolio + " REGISTRO 14 (OBL. SOL.): NO. DE REGISTROS DIFERENTES DE UNO";
                                MsgBoxStyle tempRefParam25 = MsgBoxStyle.Exclamation;
                                string tempRefParam26 = "";
                                mdlGlobales.subDespErrores(ref tempRefParam24, ref tempRefParam25, ref tempRefParam26);
                                subLimpiarDatos();
                                cmdLeeArchivo.Focus();
                                this.Cursor = Cursors.Default;
                                return;
                            }
                            break;
                        case "15":  // Valida No. de Registro x Solicitud 
                            intTotalReg15++;
                            if ((strUltimoFolio == strFolio) && (intTotalReg15 > 1))
                            {
                                string tempRefParam27 = "SE RECHAZA LA REMESA -> FOLIO " + strFolio + " REGISTRO 15 (COMP.): NO. DE REGISTROS DIFERENTES DE UNO";
                                MsgBoxStyle tempRefParam28 = MsgBoxStyle.Exclamation;
                                string tempRefParam29 = "";
                                mdlGlobales.subDespErrores(ref tempRefParam27, ref tempRefParam28, ref tempRefParam29);
                                subLimpiarDatos();
                                cmdLeeArchivo.Focus();
                                this.Cursor = Cursors.Default;
                                return;
                            }
                            break;
                        case "16":  // Valida No. de Registro x Solicitud 
                            intTotalReg16++;
                            if ((strUltimoFolio == strFolio) && (intTotalReg16 > 1))
                            {
                                string tempRefParam30 = "SE RECHAZA LA REMESA -> FOLIO " + strFolio + " REGISTRO 16 (DAT. ADIC. SOL.): NO. DE REGISTROS DIFERENTES DE UNO";
                                MsgBoxStyle tempRefParam31 = MsgBoxStyle.Exclamation;
                                string tempRefParam32 = "";
                                mdlGlobales.subDespErrores(ref tempRefParam30, ref tempRefParam31, ref tempRefParam32);
                                subLimpiarDatos();
                                cmdLeeArchivo.Focus();
                                this.Cursor = Cursors.Default;
                                return;
                            }
                            break;
                        case "17":  // Valida No. de Registro x Solicitud 
                            intTotalReg17++;
                            if ((strUltimoFolio == strFolio) && (intTotalReg17 > 1))
                            {
                                string tempRefParam33 = "SE RECHAZA LA REMESA -> FOLIO " + strFolio + " REGISTRO 17 (DAT. ADIC. OS): NO. DE REGISTROS DIFERENTES DE UNO";
                                MsgBoxStyle tempRefParam34 = MsgBoxStyle.Exclamation;
                                string tempRefParam35 = "";
                                mdlGlobales.subDespErrores(ref tempRefParam33, ref tempRefParam34, ref tempRefParam35);
                                subLimpiarDatos();
                                cmdLeeArchivo.Focus();
                                this.Cursor = Cursors.Default;
                                return;
                            }
                            break;
                    }
                }
                FileSystem.FileClose(intFileRead);

                this.Cursor = Cursors.Default;
                prg_proceso.Value = 0;

                if (Int32.Parse(txtNumSolicitudes.Text) != intTotalReg01)
                {
                    string tempRefParam36 = "EL NUMERO DE REGISTROS EN EL ARCHIVO NO COINCIDE CON EL TOTAL DE SOLICITUDES";
                    MsgBoxStyle tempRefParam37 = MsgBoxStyle.Exclamation;
                    string tempRefParam38 = "ERROR EN ARCHIVO";
                    mdlGlobales.subDespErrores(ref tempRefParam36, ref tempRefParam37, ref tempRefParam38);
                    cmdLeeArchivo.Focus();
                    subLimpiarDatos();
                    return;
                }

                if (!blnFolioIniOk)
                {
                    string tempRefParam39 = "FOLIO INICIAL DEL HEADER NO COINCIDE CON EL PRIMER FOLIO DE LOS REGISTROS EN EL ARCHIVO";
                    MsgBoxStyle tempRefParam40 = MsgBoxStyle.Exclamation;
                    string tempRefParam41 = "";
                    mdlGlobales.subDespErrores(ref tempRefParam39, ref tempRefParam40, ref tempRefParam41);
                    subLimpiarDatos();
                    cmdLeeArchivo.Focus();
                    return;
                }

                if (!blnFolioFinOk)
                {
                    string tempRefParam42 = "FOLIO FINAL DEL HEADER NO COINCIDE CON ULTIMO FOLIO DE LOS REGISTROS EN EL ARCHIVO";
                    MsgBoxStyle tempRefParam43 = MsgBoxStyle.Exclamation;
                    string tempRefParam44 = "";
                    mdlGlobales.subDespErrores(ref tempRefParam42, ref tempRefParam43, ref tempRefParam44);
                    subLimpiarDatos();
                    cmdLeeArchivo.Focus();
                    return;
                }
                mdlGlobales.gbolEncontrePromoDflt = false;
                txtSolAceptadas.Text = "0";
                txtSolRechazadas.Text = "0";
                txtTipoTram.Text = Strings.Mid(cboTipoTram.Text, 4, Strings.Len(cboTipoTram.Text));
                mdlGlobales.gstrEntOrig.Value = txtTipoEntidad.Text;
                mdlGlobales.gstrTipoEntOrig.Value = mdlGlobales.gstrEntOrig.Value;
                mdlGlobales.gstrCveEntOrig.Value = txtEntidadOrigen.Text;
                mdlGlobales.gstrGpoEntOrig.Value = "0001"; //ASG CAMBIO DE GRUPO DE ENTIDAD ORIGEN

                string tempRefParam45 = "94";
                string tempRefParam46 = "E";
                mdlComunica.OleCatalogos.AbreCatalogo(ref tempRefParam45, ref tempRefParam46, mdlGlobales.gstrTramite.Value + mdlCatalogos.gstrCatFamilia + mdlCatalogos.gstrTipSol);
                if (Strings.Mid(mdlComunica.OleCatalogos.getAtributos, 22, 1) == "1")
                {
                    mdlGlobales.gbolAplicaPromo = true;
                    mdlGlobales.gbolAplicaComision = Strings.Mid(mdlComunica.OleCatalogos.getAtributos, 23, 1) != "0";
                }
                else
                {
                    mdlGlobales.gbolAplicaPromo = false;
                }
                mdlComunica.OleCatalogos.CierraCatalogo();
                if (mdlGlobales.gbolAplicaPromo)
                {
                    mdlPromotora.subObtienePromoDflt();
                    if (!mdlGlobales.gbolEncontrePromoDflt)
                    {
                        if (MessageBox.Show("NO EXISTE UNA PROMOCION POR DEFAULT VIGENTE PARA ESTE TRAMITE-FAMILIA-TIPO SOLICITUD, DESEA CONTINUAR CON EL PROCESO DEL ARCHIVO? ", "CLAVE PROMOCION DE DEFAULT", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.No)
                        {
                            return;
                        }
                    }
                }

                mdlGlobales.subDespMensajes("VALIDANDO LA EXISTENCIA DE LA REMESA...");

                strDatos = mdlGlobales.gstrFolPreImpIni.Value + mdlGlobales.gstrFolPreImpFin.Value;
                if (mdlTranMasivo.funEnviaRecibe5562("5562", "01", strDatos))
                {
                    strCveResp = Strings.Mid(mdlTranMasivo.gvRecive5562_01, 51, 2); //MMS Se recorre posición de 50 a 51
                    strRemesa = Strings.Mid(mdlTranMasivo.gvRecive5562_01, 177, 16);
                    if (strCveResp == "00" && strRemesa != "0000000000000000")
                    {
                        //Obtener estos datos del la variable gvRecibe5561_01
                        txtRemesa.Text = this.cboTipoTram.Text.Substring(0, Math.Min(this.cboTipoTram.Text.Length, 2)) + Strings.Mid(mdlTranMasivo.gvRecive5562_01, 177, 16);
                        txtArchivo.Text = Strings.Mid(mdlTranMasivo.gvRecive5562_01, 193, 8); //Vaciar por especificación
                        if (Conversion.Val(Strings.Mid(mdlTranMasivo.gvRecive5562_01, 201, 5)) > 0)
                        {
                            txtNumSolicitudes.Text = Strings.Mid(mdlTranMasivo.gvRecive5562_01, 201, 5);
                        }
                        txtSolAceptadas.Text = Strings.Mid(mdlTranMasivo.gvRecive5562_01, 206, 5);
                        txtSolRechazadas.Text = (Double.Parse(txtNumSolicitudes.Text) - Double.Parse(txtSolAceptadas.Text)).ToString();
                        txtTipoEntidad.Text = Strings.Mid(mdlTranMasivo.gvRecive5562_01, 177, 2);
                        txtEntidadOrigen.Text = Strings.Mid(mdlTranMasivo.gvRecive5562_01, 179, 4);
                        string tempRefParam47 = "66";
                        string tempRefParam48 = Strings.Mid(mdlTranMasivo.gvRecive5562_01, 177, 2);
                        string tempRefParam49 = null;
                        string tempRefParam50 = null;
                        string tempRefParam51 = null;
                        Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam52 = Catalogos.clsCatalogos.enmCamposCatalogos.Default;
                        string tempRefParam53 = "E";
                        if (mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam47, tempRefParam48, tempRefParam49, tempRefParam50, ref tempRefParam51, ref tempRefParam52, ref tempRefParam53))
                        {
                            txtTipoEntidad.Text = txtTipoEntidad.Text + " " + mdlComunica.OleCatalogos.getDescripcion;
                        }
                        string tempRefParam54 = "66";
                        string tempRefParam55 = Strings.Mid(mdlTranMasivo.gvRecive5562_01, 177, 2);
                        string tempRefParam56 = null;
                        string tempRefParam57 = null;
                        string tempRefParam58 = null;
                        Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam59 = Catalogos.clsCatalogos.enmCamposCatalogos.Default;
                        string tempRefParam60 = "E";
                        if (mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam54, tempRefParam55, tempRefParam56, tempRefParam57, ref tempRefParam58, ref tempRefParam59, ref tempRefParam60))
                        {
                            txtEntidadOrigen.Text = txtEntidadOrigen.Text + " " + mdlComunica.OleCatalogos.getDescripcion;
                        }
                        mdlGlobales.gstrEntOrig.Value = txtTipoEntidad.Text.Substring(0, Math.Min(txtTipoEntidad.Text.Length, 2));
                        mdlGlobales.gstrCveEntOrig.Value = txtEntidadOrigen.Text.Substring(0, Math.Min(txtEntidadOrigen.Text.Length, 4));
                        //ASG CAMBIO DE GRUPO DE ENTIDAD ORIGEN
                        mdlGlobales.gstrGpoEntOrig.Value = "0001";
                        if (mdlTranMasivo.funConsultaEstatus(txtRemesa.Text.Substring(txtRemesa.Text.Length - Math.Min(txtRemesa.Text.Length, 10))) == mdlTranMasivo.enmEstatusRemesa.stAsignada)
                        {
                            if (MessageBox.Show("LA REMESA YA FUE ASIGNADA PERO NO HA SIDO ENVIADA, ¿DESEA ENVIARLA AHORA?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                            {
                                //AIS-Bug 8428 FSABORIO
                                Application.DoEvents();
                                mdlGlobales.gstrTipoEntOrig.Value = this.txtTipoEntidad.Text.Substring(0, Math.Min(this.txtTipoEntidad.Text.Length, 2));
                                if (!mdlTranMasivo.funLeePaquete(mdlGlobales.funPoneCeros(this.cboTipoTram.Text.Substring(0, Math.Min(this.cboTipoTram.Text.Length, 2)), 4), this.txtFamiliaProducto.Text.Substring(0, Math.Min(this.txtFamiliaProducto.Text.Length, 2)), this.txtTipoSolicitud.Text.Substring(0, Math.Min(this.txtTipoSolicitud.Text.Length, 2))))
                                {
                                    //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                                    string tempRefParam61 = "NO EXISTE LA CLAVE PAQUETE PARA ESTE TRAMITE-FAMILIA-TIPO SOLICITUD, NO SE PUEDE GENERAR LA REMESA CORRESPONDIENTE AL ARCHIVO";
                                    MsgBoxStyle tempRefParam62 = (MsgBoxStyle)(((int)MsgBoxStyle.Critical) + ((int)MsgBoxStyle.OkOnly));
                                    string tempRefParam63 = "CLAVE PAQUETE";
                                    mdlGlobales.subDespErrores(ref tempRefParam61, ref tempRefParam62, ref tempRefParam63);
                                    return;
                                }
                                string tempRefParam64 = "94";
                                string tempRefParam65 = "E";
                                mdlComunica.OleCatalogos.AbreCatalogo(ref tempRefParam64, ref tempRefParam65, mdlGlobales.gstrTramite.Value + mdlCatalogos.gstrCatFamilia + mdlCatalogos.gstrTipSol);
                                if (Strings.Mid(mdlComunica.OleCatalogos.getAtributos, 22, 1) == "1")
                                {
                                    mdlGlobales.gbolAplicaPromo = true;
                                    mdlGlobales.gbolAplicaComision = Strings.Mid(mdlComunica.OleCatalogos.getAtributos, 23, 1) == "1";
                                }
                                else
                                {
                                    mdlGlobales.gbolAplicaPromo = false;
                                }
                                mdlComunica.OleCatalogos.CierraCatalogo();
                                if (mdlGlobales.gbolAplicaPromo)
                                {
                                    mdlPromotora.subObtienePromoDflt();
                                    if (!mdlGlobales.gbolEncontrePromoDflt)
                                    {
                                        if (MessageBox.Show("NO EXISTE UNA PROMOCION POR DEFAULT VIGENTE PARA ESTE TRAMITE-FAMILIA-TIPO SOLICITUD, DESEA CONTINUAR CON EL PROCESO DEL ARCHIVO? ", "CLAVE PROMOCION DE DEFAULT", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.No)
                                        {
                                            return;
                                        }
                                    }
                                }
                                mdlGlobales.gblnProcesaArchivo = true;
                                mdlPromotora.subProcesaArchivoPromotora();
                                mdlGlobales.gblnProcesaArchivo = false;
                            }
                            else
                            {
                                subLimpiarDatos();
                            }
                        }
                        else
                        {
                            //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                            string tempRefParam66 = "LA REMESA YA FUE GENERADA";
                            MsgBoxStyle tempRefParam67 = (MsgBoxStyle)(((int)MsgBoxStyle.Information) + ((int)MsgBoxStyle.OkOnly));
                            string tempRefParam68 = "MASIVOS";
                            mdlGlobales.subDespErrores(ref tempRefParam66, ref tempRefParam67, ref tempRefParam68);
                            subLimpiarDatos();
                        }
                    }
                    else if (strCveResp == "00" && strRemesa == "0000000000000000")
                    {
                        if (MessageBox.Show("NO SE HA GENERADO LA REMESA CORRESPONDIENTE AL ARCHIVO. ¿DESEA GENERARLA?", "MASIVOS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                        {
                            subLimpiarDatos();
                            cmdLeeArchivo_Enter(cmdLeeArchivo, new EventArgs());
                            return;
                        }
                        else
                        {
                            frmRegRemesas.DefInstance.Show();
                            this.Hide();
                        }
                    }
                }
                else
                {
                    subLimpiarDatos();
                    cmdLeeArchivo.Focus();
                }
                mdlGlobales.subDespMensajes("");
            }
            catch (Exception excep)
            {

                if (Information.Err().Number == 32755)
                {
                    return;
                }
                else if (Information.Err().Number == 13)
                {  //TYPE MISTMATCH
                    //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                    string tempRefParam69 = "SE DETECTARON DATOS NO NUMÉRICOS";
                    MsgBoxStyle tempRefParam70 = (MsgBoxStyle)(((int)MsgBoxStyle.Exclamation) + ((int)MsgBoxStyle.OkOnly));
                    mdlGlobales.subDespErrores(ref tempRefParam69, ref tempRefParam70);
                }
                else
                {
                    if (txtArchivo.Text.ToUpper().IndexOf(".TXT") >= 0)
                    {
                        string tempRefParam71 = "ERROR: " + Information.Err().Number.ToString().Trim() + " " + excep.Message.ToUpper();
                        MsgBoxStyle tempRefParam72 = MsgBoxStyle.Exclamation;
                        string tempRefParam73 = "ERROR DE LECTURA";
                        mdlGlobales.subDespErrores(ref tempRefParam71, ref tempRefParam72, ref tempRefParam73);
                    }
                    if (Information.Err().Number > 0)
                    {
                        string tempRefParam74 = "NO SE HA GENERADO LA REMESA CORRESPONDIENTE AL ARCHIVO";
                        MsgBoxStyle tempRefParam75 = MsgBoxStyle.Information;
                        mdlGlobales.subDespErrores(ref tempRefParam74, ref tempRefParam75);
                        subLimpiarDatos();
                        mdlGlobales.InhibeControles(frmProcMasivo.DefInstance, true, "PROCESO");
                        cmdLeeArchivo.Focus();
                        return;
                    }
                }
            }
        }

        //*****************************************************************************************************
        //* Finalidad:  Subrutina para procesar las acciones al hacer click sobre el botón Salir
        //*****************************************************************************************************
        private void cmdSalir_Click(Object eventSender, EventArgs eventArgs)
        {
            //AIS-1896 FSABORIO
            if (!this.SuspendFormClosing)
                this.Close();
        }

        //*****************************************************************************************************
        //* Finalidad:  Subrutina para procesar las acciones cuando se descarga el formulario
        //*****************************************************************************************************
        private void frmProcMasivo_Closed(Object eventSender, EventArgs eventArgs)
        {
            mdlGlobales.subMuestraMenu();
            lblGrabado.Visible = false;
            lblGrabado.Text = "";
            MemoryHelper.ReleaseMemory();
        }

        public void mnuLeeArchivo_Click(Object eventSender, EventArgs eventArgs)
        {
            cmdLeeArchivo_Click(cmdLeeArchivo, new EventArgs());
        }

        public void mnuSalir_Click(Object eventSender, EventArgs eventArgs)
        {
            cmdSalir_Click(cmdSalir, new EventArgs());
        }

        //AIS-1896 FSABORIO
        public bool SuspendFormClosing = false;
        private void frmProcMasivo_FormClosing(object sender, FormClosingEventArgs eventArgs)
        {
            //AIS-1896 FSABORIO
            SuspendFormClosing = true;
        }
    }
}
