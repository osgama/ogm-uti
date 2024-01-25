using Artinsoft.VB6.Utils;
using Microsoft.VisualBasic;
using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using VB6 = Microsoft.VisualBasic.Compatibility.VB6.Support;

namespace Masivos
{
    internal partial class frmVentasCruzadas
        : System.Windows.Forms.Form
    {

        //*******************************************************************************
        //* Identificación: frmProcMasivo.frm                                           *
        //* Autor:          Israel Javier Garcés Morales                                *
        //* Instalación:    BANAMEX, S.A.                                               *
        //* Fecha:          09/09/2003                                                  *
        //* Versión:        1.0                                                         *
        //*******************************************************************************
        //* Objetivo: Seleccion del Archivo de Promotora a  procesar, de acuerdo al tra-*
        //*           mite seleccionado. Se realizan las validaciones minimas para la   *
        //*           aceptacion del archivo (validacion de header y trailer),naturaleza*
        //*           delos datos, información contra catalogos,  etc.                  *
        //*           Si el archivo es aceptado (al menos se cumple con el % de solici- *
        //*           tudes correctas -parametro por catalogo) y la remesa no existe, se*
        //*           procede a generarla. Si la remesa ya existe se presenta la informa*
        //*           cion correspondiente en pantalla.                                 *
        //*           Si existen remesas rechazadas se genera un archivo de errores     *
        //*******************************************************************************

        private void cmdAbrirArchivo_Click(Object eventSender, EventArgs eventArgs)
        {
            subConfigIniVentasCruzadas();
            dlgArchivoAbrir.CancelError = false;
            dlgArchivoAbrir.FileName = "";
            dlgArchivoAbrir.Flags = (int)MSComDlg.FileOpenConstants.cdlOFNHideReadOnly;
            dlgArchivoAbrir.Filter = "VtasCruzadas (A????997.txt)|A????997.txt";
            dlgArchivoAbrir.FilterIndex = 1;
            DespMensajes("SELECCIONANDO ARCHIVO DE VENTAS CRUZADAS...");

            dlgArchivoAbrir.ShowOpen();
            cmdEnviar.Enabled = false;

            DialogResult str_Renamed = System.Windows.Forms.DialogResult.None;
            DespMensajes("                                          ");
            if (dlgArchivoAbrir.FileName != "")
            {
                if (funAbreArchivoVC(dlgArchivoAbrir.FileName))
                {
                    subConfigVentasCruzadas();
                    cmdValidar.Focus();
                }
                else
                {
                    str_Renamed = MessageBox.Show("¡ PROBLEMAS AL INTENTAR CARGAR EL ARCHIVO " + txtArchivo.Text + " !", "Error de Archivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                subConfigIniVentasCruzadas();
            }
            DespMensajes("                                               ");


        }
        private bool funAbreArchivoVC(string strNomArchVC)
        {
            bool result = false;
            int intFileVCRead = 0;
            string strCadena = String.Empty;
            string strTipReg = String.Empty;
            string strFechArch = String.Empty;
            int intTotTipReg00 = 0;
            int intTotTipReg01 = 0;
            int intTotTipReg02 = 0;
            int intTotTipReg03 = 0;
            int intTotTipReg04 = 0;
            int intTotTipReg05 = 0;
            int intTotTipReg99 = 0;
            int intTotOtrosReg = 0;
            double intTotReg = 0;

            try
            {



                intTotTipReg00 = 0;
                intTotTipReg01 = 0;
                intTotTipReg02 = 0;
                intTotTipReg03 = 0;
                intTotTipReg04 = 0;
                intTotTipReg05 = 0;
                intTotTipReg99 = 0;
                intTotOtrosReg = 0;
                intTotReg = 0;


                mdlGlobales.gstrRegHeaderVC = "";
                mdlGlobales.gstrRegTrailerVC = "";

                intFileVCRead = FileSystem.FreeFile();
                FileSystem.FileOpen(intFileVCRead, strNomArchVC, OpenMode.Input, OpenAccess.Default, OpenShare.Default, -1);
                DespMensajes("ABRIENDO ARCHIVO ...");
                this.Cursor = Cursors.WaitCursor;
                result = true;
                while (!FileSystem.EOF(intFileVCRead))
                {

                    strCadena = FileSystem.LineInput(intFileVCRead);
                    strTipReg = Strings.Mid(strCadena, 1, 2);
                    intTotReg++;
                    switch (strTipReg)
                    {
                        case "00":
                            strFechArch = Strings.Mid(strCadena, 3, 8);
                            intTotTipReg00++;
                            txtFecha.Text = strFechArch;
                            intTotReg--;

                            break;
                        case "01":
                            intTotTipReg01++;

                            break;
                        case "02":
                            intTotTipReg02++;

                            break;
                        case "03":
                            intTotTipReg03++;

                            break;
                        case "04":
                            intTotTipReg04++;

                            break;
                        case "05":
                            intTotTipReg05++;

                            break;
                        case "99":
                            intTotTipReg99++;
                            txtNumRegistros.Text = Strings.Mid(strCadena, 3, 9);
                            intTotReg--;

                            break;
                        default:
                            intTotOtrosReg++;

                            break;
                    }
                }

                txtHeader.Text = mdlGlobales.Cadenas_Concatena_Blancos_O_Ceros(intTotTipReg00.ToString(), 0, 0, 2);
                txtRegTipo1.Text = mdlGlobales.Cadenas_Concatena_Blancos_O_Ceros(intTotTipReg01.ToString(), 0, 0, 9);
                txtRegTipo2.Text = mdlGlobales.Cadenas_Concatena_Blancos_O_Ceros(intTotTipReg02.ToString(), 0, 0, 9);
                txtRegTipo3.Text = mdlGlobales.Cadenas_Concatena_Blancos_O_Ceros(intTotTipReg03.ToString(), 0, 0, 9);
                txtRegTipo4.Text = mdlGlobales.Cadenas_Concatena_Blancos_O_Ceros(intTotTipReg04.ToString(), 0, 0, 9);
                txtRegTipo5.Text = mdlGlobales.Cadenas_Concatena_Blancos_O_Ceros(intTotTipReg05.ToString(), 0, 0, 9);
                txtTrailer.Text = mdlGlobales.Cadenas_Concatena_Blancos_O_Ceros(intTotTipReg99.ToString(), 0, 0, 2);
                txtNumRegistros.Text = mdlGlobales.Cadenas_Concatena_Blancos_O_Ceros(intTotReg.ToString(), 0, 0, 9);

                txtArchProce.Text = dlgArchivoAbrir.FileName;
                txtArchivo.Text = Strings.Mid(txtArchProce.Text, Strings.Len(txtArchProce.Text) - 11, 12);
                this.Cursor = Cursors.Default;
                DespMensajes("                      ");
                FileSystem.FileClose(intFileVCRead);
            }
            catch
            {
            }


            if (Information.Err().Number == 53)
            {
                string tempRefParam = "NO EXISTE EL ARCHIVO";
                MsgBoxStyle tempRefParam2 = MsgBoxStyle.Critical;
                mdlGlobales.subDespErrores(ref tempRefParam, ref tempRefParam2);
                result = false;
            }
            if (Information.Err().Number == 0 && intTotReg == 0)
            {
                string tempRefParam3 = "EL ARCHIVO ESTA VACÍO";
                MsgBoxStyle tempRefParam4 = MsgBoxStyle.Critical;
                mdlGlobales.subDespErrores(ref tempRefParam3, ref tempRefParam4);
                result = false;
                FileSystem.FileClose(intFileVCRead);
            }

            return result;
        }
        private void subConfigVentasCruzadas()
        {
            fraInfoArchivo.Enabled = true;
            if (Strings.Len(dlgArchivoAbrir.FileName) != 0)
            {
                cmdValidar.Enabled = true;
                for (int i = 0; i <= 9; i++)
                {
                    Label2[i].Enabled = true;
                }

                txtArchivo.Enabled = true;
                txtFecha.Enabled = true;
                txtHeader.Enabled = true;
                txtTrailer.Enabled = true;
                txtRegTipo1.Enabled = true;
                txtRegTipo2.Enabled = true;
                txtRegTipo3.Enabled = true;
                txtRegTipo4.Enabled = true;
                txtRegTipo5.Enabled = true;
                txtNumRegistros.Enabled = true;
            }
        }

        public void subConfigIniVentasCruzadas()
        {
            fraInfoArchivo.Enabled = false;
            cmdValidar.Enabled = false;
            cmdEnviar.Enabled = false;
            for (int i = 0; i <= 9; i++)
            {
                Label2[i].Enabled = false;
            }
            txtArchProce.Text = "";
            txtArchivo.Enabled = false;
            txtArchivo.Text = "";
            txtFecha.Enabled = false;
            txtFecha.Text = "";
            txtHeader.Enabled = false;
            txtHeader.Text = "";
            txtTrailer.Enabled = false;
            txtTrailer.Text = "";
            txtRegTipo1.Enabled = false;
            txtRegTipo1.Text = "";
            txtRegTipo2.Enabled = false;
            txtRegTipo2.Text = "";
            txtRegTipo3.Enabled = false;
            txtRegTipo3.Text = "";
            txtRegTipo4.Enabled = false;
            txtRegTipo4.Text = "";
            txtRegTipo5.Enabled = false;
            txtRegTipo5.Text = "";
            txtNumRegistros.Enabled = false;
            txtNumRegistros.Text = "";
            fraValArchivo.Enabled = false;
            labNumRegError.Enabled = false;
            txtNumRegError.Text = "";
            labConArhError.Enabled = false;
        }


        private void cmdEnviar_Click(Object eventSender, EventArgs eventArgs)
        {
            string strRutaArchivo = String.Empty;
            Scripting.FileSystemObject filArchivo = new Scripting.FileSystemObject();
            bool bolEncontroArchivo = false;
            DialogResult ss = MessageBox.Show("     ¿ESTÁ SEGURO DE ENVIAR EL ARCHIVO?    ", "Confirmación de envío", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ss == System.Windows.Forms.DialogResult.Yes)
            {
                bolEncontroArchivo = false;
                if (funValidaArchivoTandem(Strings.Mid(txtArchivo.Text, 1, 8)))
                {
                    ss = MessageBox.Show("     EL ARCHIVO YA FUE ENVIADO CON ANTERIORIDAD, DESEA ENVIARLO DE NUEV0?   ", "ARCHIVO ENVIADO CON ANTERIORIDAD", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    bolEncontroArchivo = true;
                }
                if (ss == System.Windows.Forms.DialogResult.Yes || !bolEncontroArchivo)
                {
                    //AIS-1893 FSABORIO
                    mdlGlobales.gstrRutaTemp = mdlMain.ApplicationPath;
                    strRutaArchivo = mdlGlobales.gstrRutaTemp + "\\" + "75301e01.s753." + txtArchivo.Text;
                    filArchivo.CopyFile(txtArchProce.Text, strRutaArchivo, true);
                    this.Cursor = Cursors.WaitCursor;
                    MDIMasivos.DefInstance.Enabled = false;
                    frmVentasCruzadas.DefInstance.Enabled = false;

                    //(JB-SAS 22/nov/2006) ANTES, POR FTP DIRECTO
                    //Call subEnviaArchivoVCFTP(strRutaArchivo)

                    //(JB-SAS 22/nov/2006) NUEVO, UTILIZANDO ICEP
                    if (!mdlICEPIntelar.blnICEP_ProcesaEnvioVCRUZADA(ref strRutaArchivo))
                    {
                        //(JB-SAS 23/nov/2006) NUEVO, PROCEDIMIENTO EN CASO DE CONTINGENCIA
                        mdlFTPIntelar.subEnviaArchivoVCFTP(ref strRutaArchivo);
                    }

                    frmVentasCruzadas.DefInstance.Enabled = true;
                    MDIMasivos.DefInstance.Enabled = true;
                    this.Cursor = Cursors.Default;
                    subConfigIniVentasCruzadas();
                }
            }

        }
        private bool funValidaArchivoTandem(string strArchivo)
        {

            bool result = false;
            string tempRefParam = mdlGlobales.gcStrMasivosIni;
            string tempRefParam2 = "RUTA_TANDEM";
            string tempRefParam3 = "RUTA";
            string strRutaTandem = mdlGlobales.funGetParam(ref tempRefParam, ref tempRefParam2, ref tempRefParam3);

            if (mdlTranAnalisis.funEnviaRecibe5560("5560", "41", strRutaTandem + strArchivo))
            {
                mdlGlobales.subDespMensajes("");
                if (Strings.Mid(mdlTranAnalisis.gvRecive5560_41, 110, 1) == "1")
                { //MMS Se recorre la posición de 108 a 110
                    result = true;
                }
            }

            return result;
        }

        private void cmdValidar_Click(Object eventSender, EventArgs eventArgs)
        {

            string strRutaErrores = String.Empty;

            DespMensajes("VALIDANDO ARCHIVO DE VENTAS CRUZADAS...");
            this.Cursor = Cursors.WaitCursor;
            frmVentasCruzadas.DefInstance.Enabled = false;
            if (funValidaArchivoVC())
            {

                DespMensajes("            ");
                cmdValidar.Enabled = false;
                this.Cursor = Cursors.Default;
                DespMensajes(" ARCHIVO CORRECTO ");
                string tempRefParam = "EL ARCHIVO SE VALIDÓ Y ES CORRECTO" + "\r" + "\r" + "   AHORA PUEDE SER ENVIADO";
                MsgBoxStyle tempRefParam2 = MsgBoxStyle.Exclamation;
                string tempRefParam3 = "ARCHIVO VALIDADO";
                mdlGlobales.subDespErrores(ref tempRefParam, ref tempRefParam2, ref tempRefParam3);
                frmVentasCruzadas.DefInstance.Enabled = true;
                cmdEnviar.Enabled = true;
                cmdEnviar.Focus();
                DespMensajes("            ");
            }
            else
            {
                string tempRefParam4 = mdlGlobales.gcStrMasivosIni;
                string tempRefParam5 = "RUTA_ERROR";
                string tempRefParam6 = "RUTA";
                strRutaErrores = mdlGlobales.funGetParam(ref tempRefParam4, ref tempRefParam5, ref tempRefParam6);
                DespMensajes(" ARCHIVO INCORRECTO ");
                string tempRefParam7 = "EL ARCHIVO NO SE PODRÁ ENVIAR, YA QUE CONTIENE ERRORES" + "\r" + "REVISAR ARCHIVO " + strRutaErrores + "/ERRORES_" + txtArchivo.Text;
                MsgBoxStyle tempRefParam8 = MsgBoxStyle.Critical;
                string tempRefParam9 = "ARCHIVO CON ERRORES";
                mdlGlobales.subDespErrores(ref tempRefParam7, ref tempRefParam8, ref tempRefParam9);
                frmVentasCruzadas.DefInstance.Enabled = true;
                this.Cursor = Cursors.Default;
                fraValArchivo.Enabled = true;
                labNumRegError.Enabled = true;
                txtNumRegError.Enabled = true;
                txtNumRegError.ReadOnly = true;
                labConArhError.Enabled = true;
                txtNumRegError.Text = mdlGlobales.Cadenas_Concatena_Blancos_O_Ceros(mdlGlobales.gintTotRegistrosError.ToString(), 0, 0, 9);
            }
            prg_proceso.Value = 0;
        }

        //UPGRADE_WARNING: (1041) Form_Load event was upgraded to Form_Load method and has a new behavior.
        private void Form_Load()
        {

            mdlGlobales.CentrarMDIHija(MDIMasivos.DefInstance, frmVentasCruzadas.DefInstance);
            cmdAbrirArchivo.Enabled = true;
            cmdSalir.Enabled = true;
            cmdEnviar.Enabled = false;
            cmdValidar.Enabled = false;
            fraInfoArchivo.Enabled = false;
            DespMensajes("                                           ");

        }

        private void cmdSalir_Click(Object eventSender, EventArgs eventArgs)
        {
            DialogResult ss = MessageBox.Show("     ¿ESTA SEGURO DE QUERER SALIR DE LA PANTALLA DE VENTAS CRUZADAS?    ", "Salir de Ventas Cruzadas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ss == System.Windows.Forms.DialogResult.Yes)
            {
                //AIS-1896 FSABORIO
                if (!this.SuspendFormClosing)
                    this.Close();
            }
        }

        private void frmVentasCruzadas_Closed(Object eventSender, EventArgs eventArgs)
        {
            DespMensajes("                                     ");
            MemoryHelper.ReleaseMemory();
        }

        public object DespMensajes(string strMensaje)
        {
            //AIS-1899 FSABORIO
            MDIMasivos.DefInstance.pnlEstado.Items[0].Text = strMensaje;
            return null;
        }

        public void SelTexto(Control cMsk)
        {
            //UPGRADE_TODO: (1067) Member SelStart is not defined in type VB.Control.
            ReflectionHelper.SetMember(cMsk, "SelStart", 0);
            //UPGRADE_TODO: (1067) Member SelLength is not defined in type VB.Control.
            //UPGRADE_TODO: (1067) Member MaxLength is not defined in type VB.Control.
            ReflectionHelper.SetMember(cMsk, "SelLength", ReflectionHelper.GetMember(cMsk, "MaxLength"));
        }

        private bool funValidaArchivoVC()
        {

            bool result = false;
            string strCadena = String.Empty;
            string strTipReg = String.Empty;

            mdlGlobales.gbolError = false;
            mdlGlobales.gintTotRegistrosError = 0;
            mdlGlobales.gbolProductoError = false;
            mdlGlobales.gbolEntFedError = false;
            mdlGlobales.gbolPlazoError = false;
            mdlGlobales.gstrTramiteAnt = "";
            mdlGlobales.gstrFamProdAnt = "";
            mdlGlobales.gstrTipSolAnt = "";
            mdlGlobales.gstrEntFedAnt = "";
            mdlGlobales.gstrPlazoAnt = "";

            subValidaHeaderTrailer();
            prg_proceso.Value = 0;
            int varNumRegistros = 0;
            int intFileVCRead = FileSystem.FreeFile();
            string strNomArchVC = dlgArchivoAbrir.FileName;
            FileSystem.FileOpen(intFileVCRead, strNomArchVC, OpenMode.Input, OpenAccess.Default, OpenShare.Default, -1);
            while (!FileSystem.EOF(intFileVCRead))
            {

                strCadena = FileSystem.LineInput(intFileVCRead);
                strTipReg = Strings.Mid(strCadena, 1, 2);
                varNumRegistros++;
                prg_proceso.Value = (float)((varNumRegistros * 100) / (Conversion.Val(txtNumRegistros.Text) + 2));
                switch (strTipReg)
                {
                    case "00":
                        subValidaHeader(strCadena);

                        break;
                    case "01":
                        subValidaRegTipo01(strCadena);

                        break;
                    case "02":
                        subValidaRegTipo02(strCadena);

                        break;
                    case "03":
                        subValidaRegTipo03(strCadena);

                        break;
                    case "04":
                        subValidaRegTipo04(strCadena);

                        break;
                    case "05":
                        subValidaRegTipo05(strCadena);

                        break;
                    case "99":
                        subValidaTrailer(strCadena);
                        break;
                    default:
                        subRegistraErrores("REGISTRO: " + strTipReg + ".- ERROR R" + strTipReg + ": TIPO DE REGISTRO NO VALIDO - ''");

                        break;
                }

            }
            FileSystem.FileClose(intFileVCRead);

            if (mdlGlobales.gintFileWrite != 0)
            {
                FileSystem.FileClose(mdlGlobales.gintFileWrite);
                mdlGlobales.gintFileWrite = 0;
            }

            if (!mdlGlobales.gbolError)
            {
                result = true;
            }

            return result;
        }

        private void subValidaHeaderTrailer()
        {

            if (Conversion.Val(txtHeader.Text) == 0)
            {

                subRegistraErrores("REGISTRO: 00 .- ERROR R00: NO EXISTE HEADER - ''");

            }

            if (Conversion.Val(txtTrailer.Text) == 0)
            {

                subRegistraErrores("REGISTRO: 99 .- ERROR R99: NO EXISTE TRAILER - ''");

            }


        }

        private void subValidaHeader(string strCadenaIn)
        {


            bool bolExisteError = false;
            string strRegError = "REGISTRO: " + Strings.Mid(strCadenaIn, 1, 2) + " " + Strings.Mid(strCadenaIn, 3, 8) + ".- ";

            string strCad = Strings.Mid(strCadenaIn, 3, 8);

            double dbNumericTemp = 0;
            if (!Double.TryParse(strCad, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp))
            {
                strRegError = strRegError + "ERROR R00: FECHA NO ES NUMÉRICO - ''; ";
                bolExisteError = true;
            }

            if (Conversion.Val(Strings.Mid(strCad, 1, 4)) < 1900 || Conversion.Val(Strings.Mid(strCad, 1, 4)) > 2030 || Conversion.Val(Strings.Mid(strCad, 5, 2)) < 1 || Conversion.Val(Strings.Mid(strCad, 5, 2)) > 12 || Conversion.Val(Strings.Mid(strCad, 7, 2)) < 1 || Conversion.Val(Strings.Mid(strCad, 7, 2)) > 31)
            {
                strRegError = strRegError + "ERROR R00: FECHA FUERA DE FORMATO - ''; ";
                bolExisteError = true;
            }


            if (Strings.Mid(txtArchivo.Text, 2, 4) != Strings.Mid(strCad, 5, 4))
            {
                strRegError = strRegError + "ERROR R00: FECHA DE HEADER Y NOMBRE DE ARCHIVO DIFERENTES - ''; ";
                bolExisteError = true;
            }

            //LONGITUD DE REGISTRO
            if (strCadenaIn.Length != mdlGlobales.gintLongRegVC)
            {
                strRegError = strRegError + "ERROR R00: REGISTRO FUERA DE LONGITUD - ''; ";
                bolExisteError = true;
            }

            if (bolExisteError)
            {
                //funcion escribe registro de error
                subRegistraErrores(strRegError);

            }

        }

        private void subValidaRegTipo01(string strCadenaIn)
        {


            bool bolExisteError = false;
            string strRegError = "REGISTRO: " + Strings.Mid(strCadenaIn, 1, 2) + " " + Strings.Mid(strCadenaIn, 3, 16) + ".- ";

            //Folio
            string strCad = Strings.Mid(strCadenaIn, 3, 16);
            double dbNumericTemp = 0;
            if (!Double.TryParse(strCad, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp))
            {
                strRegError = strRegError + "ERROR R01: FOLIO NO ES NUMÉRICO - ''; ";

                bolExisteError = true;
            }

            if (Conversion.Val(strCad) == 0 && strCad.Length != 0)
            {
                strRegError = strRegError + "ERROR R01: FOLIO CON VALOR 0 - ''; ";
                bolExisteError = true;
            }


            //Tramine
            strCad = Strings.Mid(strCadenaIn, 19, 2);
            string strTramite = strCad;
            double dbNumericTemp2 = 0;
            if (!Double.TryParse(strCad, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp2))
            {
                strRegError = strRegError + "ERROR R01: TRÁMITE NO ES NUMÉRICO - ''; ";
                bolExisteError = true;
            }

            //Familia Producto
            strCad = Strings.Mid(strCadenaIn, 21, 2);
            string strFamiliaProducto = strCad;
            double dbNumericTemp3 = 0;
            if (!Double.TryParse(strCad, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp3))
            {
                strRegError = strRegError + "ERROR R01: FAMILIA-PRODUCTO NO ES NUMÉRICO - ''; ";
                bolExisteError = true;
            }

            //Tipo Solicitud
            strCad = Strings.Mid(strCadenaIn, 23, 2);
            string strTipoSolicitud = strCad;
            double dbNumericTemp4 = 0;
            if (!Double.TryParse(strCad, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp4))
            {
                strRegError = strRegError + "ERROR R01: TIPO-SOLICITUD NO ES NUMÉRICO - ''; ";
                bolExisteError = true;
            }

            //trámite-familia-solicitud en catálogo.
            strCad = Strings.Mid(strCadenaIn, 19, 2) + Strings.Mid(strCadenaIn, 21, 2) + Strings.Mid(strCadenaIn, 23, 2);
            if (strCad != (mdlGlobales.gstrTramiteAnt + mdlGlobales.gstrFamProdAnt + mdlGlobales.gstrTipSolAnt))
            {
                strCad = mdlGlobales.funPoneCeros(strCad, 8);
                string tempRefParam = "94";
                string tempRefParam2 = null;
                string tempRefParam3 = null;
                string tempRefParam4 = null;
                Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam5 = Catalogos.clsCatalogos.enmCamposCatalogos.Default;
                string tempRefParam6 = "E";
                if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam, strCad, tempRefParam2, tempRefParam3, ref tempRefParam4, ref tempRefParam5, ref tempRefParam6))
                {
                    strRegError = strRegError + "ERROR R01: TRAMITE-FAMILIA-SOLICITUD NO EXISTE EN CATÁLOGO - ''; ";
                    bolExisteError = true;
                    mdlGlobales.gbolProductoError = true;
                }
                else
                {
                    mdlGlobales.gbolProductoError = false;
                }
                mdlGlobales.gstrTramiteAnt = Strings.Mid(strCadenaIn, 19, 2);
                mdlGlobales.gstrFamProdAnt = Strings.Mid(strCadenaIn, 21, 2);
                mdlGlobales.gstrTipSolAnt = Strings.Mid(strCadenaIn, 23, 2);
            }
            else
            {
                if (mdlGlobales.gbolProductoError)
                {
                    strRegError = strRegError + "ERROR R01: TRAMITE-FAMILIA-SOLICITUD NO EXISTE EN CATÁLOGO - ''; ";
                    bolExisteError = true;
                }
            }

            //Nombres
            strCad = Strings.Mid(strCadenaIn, 25, 30);
            if (strCad == "" || strCad == new String(' ', 30))
            {
                strRegError = strRegError + "ERROR R01: NOMBRES EN ESPACIOS O VACIO - ''; ";
                bolExisteError = true;
            }

            //Apellido Paterno
            strCad = Strings.Mid(strCadenaIn, 55, 30);
            if (strCad == "" || strCad == new String(' ', 30))
            {
                strRegError = strRegError + "ERROR R01: APELLIDO PATERNO EN ESPACIOS O VACIO - ''; ";
                bolExisteError = true;
            }

            //Apellido Materno
            strCad = Strings.Mid(strCadenaIn, 85, 30);
            if (strCad == "" || strCad == new String(' ', 30))
            {
                strRegError = strRegError + "ERROR R01: APELLIDO MATERNO EN ESPACIOS O VACIO - ''; ";
                bolExisteError = true;
            }

            //Calle y Número
            strCad = Strings.Mid(strCadenaIn, 128, 36);
            if (strCad == "" || strCad == new String(' ', 36))
            {
                strRegError = strRegError + "ERROR R01: CALLE Y NÚMERO EN ESPACIOS O VACIO - ''; ";
                bolExisteError = true;
            }

            //Colonia
            strCad = Strings.Mid(strCadenaIn, 164, 25);
            if (strCad == "" || strCad == new String(' ', 25))
            {
                strRegError = strRegError + "ERROR R01: COLONIA EN ESPACIOS O VACIO - ''; ";
                bolExisteError = true;
            }

            //Población
            strCad = Strings.Mid(strCadenaIn, 189, 25);
            if (strCad == "" || strCad == new String(' ', 25))
            {
                strRegError = strRegError + "ERROR R01: POBLACION EN ESPACIOS O VACIO - ''; ";
                bolExisteError = true;
            }

            //Entidad Federativa
            strCad = Strings.Mid(strCadenaIn, 214, 2);
            double dbNumericTemp5 = 0;
            if (!Double.TryParse(strCad, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp5))
            {
                strRegError = strRegError + "ERROR R01: ENTIDAD FEDERATIVA EN ESPACIOS O VACIO - ''; ";
                bolExisteError = true;
            }

            //Entidad Federativa en catálogo
            if (strCad != mdlGlobales.gstrEntFedAnt)
            {
                strCad = mdlGlobales.funPoneCeros(strCad, 8);
                string tempRefParam7 = "19";
                string tempRefParam8 = null;
                string tempRefParam9 = null;
                string tempRefParam10 = null;
                Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam11 = Catalogos.clsCatalogos.enmCamposCatalogos.Default;
                string tempRefParam12 = "E";
                if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam7, strCad, tempRefParam8, tempRefParam9, ref tempRefParam10, ref tempRefParam11, ref tempRefParam12))
                {
                    strRegError = strRegError + "ERROR R01: ENTIDAD FEDERATIVA NO EXISTE EN CATÁLOGO - ''; ";
                    bolExisteError = true;
                    mdlGlobales.gbolEntFedError = true;
                }
                else
                {
                    mdlGlobales.gbolEntFedError = false;
                }
                mdlGlobales.gstrEntFedAnt = Strings.Mid(strCadenaIn, 214, 2);
            }
            else
            {
                if (mdlGlobales.gbolEntFedError)
                {
                    strRegError = strRegError + "ERROR R01: ENTIDAD FEDERATIVA NO EXISTE EN CATÁLOGO - ''; ";
                    bolExisteError = true;
                }
            }

            //CP
            strCad = Strings.Mid(strCadenaIn, 216, 5);
            double dbNumericTemp6 = 0;
            if (!Double.TryParse(strCad, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp6))
            {
                strRegError = strRegError + "ERROR R01: CP NO ES NUMÉRICO - ''; ";
                bolExisteError = true;
            }

            if (Conversion.Val(strCad) == 0 && strCad.Length != 0)
            {
                strRegError = strRegError + "ERROR R01: CP CON VALOR 0 - ''; ";
                bolExisteError = true;
            }

            //Ingresos
            strCad = Strings.Mid(strCadenaIn, 247, 12);
            double dbNumericTemp7 = 0;
            if (!Double.TryParse(strCad, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp7))
            {
                strRegError = strRegError + "ERROR R01: INGRESOS MENSUALES NO ES NUMÉRICO - ''; ";
                bolExisteError = true;
            }

            //Crédito Autorizado
            strCad = Strings.Mid(strCadenaIn, 259, 12);
            double dbNumericTemp8 = 0;
            if (!Double.TryParse(strCad, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp8))
            {
                strRegError = strRegError + "ERROR R01: CRÉDITO AUTORIZADO NO ES NUMÉRICO - ''; ";
                bolExisteError = true;
            }

            //Tasa o perfil del Crédito
            strCad = Strings.Mid(strCadenaIn, 271, 2);
            double dbNumericTemp9 = 0;
            if (!Double.TryParse(strCad, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp9))
            {
                strRegError = strRegError + "ERROR R01: TASA O PERFIL NO ES NUMÉRICO - ''; ";
                bolExisteError = true;
            }

            //Plazo del Crédito
            strCad = Strings.Mid(strCadenaIn, 273, 2);
            double dbNumericTemp10 = 0;
            if (!Double.TryParse(strCad, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp10))
            {
                strRegError = strRegError + "ERROR R01: PLAZO DEL CRÉDITO NO ES NUMÉRICO - ''; ";
                bolExisteError = true;
            }

            //Verificar en catálogo
            strTramite = mdlGlobales.funPoneCeros(strTramite, 8);
            strFamiliaProducto = mdlGlobales.funPoneCeros(strFamiliaProducto, 8);
            strTipoSolicitud = mdlGlobales.funPoneCeros(strTipoSolicitud, 8);
            strCad = "000001" + strCad;
            if (strCad != mdlGlobales.gstrPlazoAnt)
            {
                string tempRefParam13 = "60";
                string tempRefParam14 = null;
                Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam15 = Catalogos.clsCatalogos.enmCamposCatalogos.Default;
                string tempRefParam16 = "E";
                if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam13, strTramite, strFamiliaProducto, strTipoSolicitud, ref tempRefParam14, ref tempRefParam15, ref tempRefParam16))
                {
                    strRegError = strRegError + "ERROR R01: PLAZO DEL CREDITO NO EXISTE EN CATÁLOGO - ''; ";
                    bolExisteError = true;
                    mdlGlobales.gbolPlazoError = true;
                }
                else
                {
                    mdlGlobales.gbolPlazoError = false;
                }
                mdlGlobales.gstrPlazoAnt = strCad;
            }
            else
            {
                if (mdlGlobales.gbolPlazoError)
                {
                    strRegError = strRegError + "ERROR R01: PLAZO DEL CREDITO NO EXISTE EN CATÁLOGO - ''; ";
                    bolExisteError = true;
                }
            }

            //Frecuencia
            strCad = Strings.Mid(strCadenaIn, 275, 2);
            double dbNumericTemp11 = 0;
            if (!Double.TryParse(strCad, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp11))
            {
                strRegError = strRegError + "ERROR R01: FRECUENCIA NO ES NUMÉRICO - ''; ";
                bolExisteError = true;
            }

            //Número de Cliente
            strCad = Strings.Mid(strCadenaIn, 277, 12);
            double dbNumericTemp12 = 0;
            if (!Double.TryParse(strCad, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp12))
            {
                strRegError = strRegError + "ERROR R01: NÚMERO DE CLIENTE NO ES NUMÉRICO - ''; ";
                bolExisteError = true;
            }

            if (Conversion.Val(strCad) == 0 && strCad.Length != 0)
            {
                strRegError = strRegError + "ERROR R01: NÚMERO DE CLIENTE CON VALOR 0 - ''; ";
                bolExisteError = true;
            }

            //LONGITUD DE REGISTRO
            if (strCadenaIn.Length != mdlGlobales.gintLongRegVC)
            {
                strRegError = strRegError + "ERROR R01: REGISTRO FUERA DE LONGITUD - ''; ";
                bolExisteError = true;
            }

            if (bolExisteError)
            {
                subRegistraErrores(strRegError);
            }


        }

        private void subValidaRegTipo02(string strCadenaIn)
        {


            bool bolExisteError = false;
            string strRegError = "REGISTRO: " + Strings.Mid(strCadenaIn, 1, 2) + " " + Strings.Mid(strCadenaIn, 3, 16) + ".- ";

            string strCad = Strings.Mid(strCadenaIn, 3, 16);
            double dbNumericTemp = 0;
            if (!Double.TryParse(strCad, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp))
            {
                strRegError = strRegError + "ERROR R02: FOLIO NO ES NUMÉRICO - ''; ";
                bolExisteError = true;
            }

            if (Conversion.Val(strCad) == 0)
            {
                strRegError = strRegError + "ERROR R02: FOLIO CON VALOR 0 - ''; ";
                bolExisteError = true;
            }

            if (strCadenaIn.Length != mdlGlobales.gintLongRegVC)
            {
                strRegError = strRegError + "ERROR R02: REGISTRO FUERA DE LONGITUD - ''; ";
                bolExisteError = true;
            }

            if (bolExisteError)
            {
                subRegistraErrores(strRegError);
            }

        }


        private void subValidaRegTipo03(string strCadenaIn)
        {


            bool bolExisteError = false;
            string strRegError = "REGISTRO: " + Strings.Mid(strCadenaIn, 1, 2) + " " + Strings.Mid(strCadenaIn, 3, 4) + ".- ";
            string strCad = Strings.Mid(strCadenaIn, 3, 4);

            double dbNumericTemp = 0;
            if (!Double.TryParse(strCad, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp))
            {
                strRegError = strRegError + "ERROR R03: CLAVE DE PROMOCIÓN NO ES NUMÉRICO - ''; ";
                bolExisteError = true;
            }

            //Clave de Promoción en catálogo
            strCad = mdlGlobales.funPoneCeros(strCad, 8);
            string tempRefParam = "64";
            string tempRefParam2 = null;
            string tempRefParam3 = null;
            string tempRefParam4 = null;
            Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam5 = Catalogos.clsCatalogos.enmCamposCatalogos.Default;
            string tempRefParam6 = "D";
            if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam, strCad, tempRefParam2, tempRefParam3, ref tempRefParam4, ref tempRefParam5, ref tempRefParam6))
            {
                strRegError = strRegError + "ERROR R01: CLAVE DE PROMOCIÓN NO EXISTE EN CATÁLOGO - ''; ";
                bolExisteError = true;
            }

            //LONGITUD DE REGISTRO
            if (strCadenaIn.Length != mdlGlobales.gintLongRegVC)
            {
                strRegError = strRegError + "ERROR R03: REGISTRO FUERA DE LONGITUD - ''; ";
                bolExisteError = true;
            }

            if (bolExisteError)
            {
                subRegistraErrores(strRegError);
            }

        }
        private void subValidaRegTipo04(string strCadenaIn)
        {


            bool bolExisteError = false;
            string strRegError = "REGISTRO: " + Strings.Mid(strCadenaIn, 1, 2) + " " + Strings.Mid(strCadenaIn, 3, 16) + ".- ";

            string strCad = Strings.Mid(strCadenaIn, 3, 8);
            double dbNumericTemp = 0;
            if (!Double.TryParse(strCad, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp))
            {
                strRegError = strRegError + "ERROR R04: FECHA DE INICIO NO ES NUMÉRICO - ''; ";
                bolExisteError = true;
            }

            if (Conversion.Val(Strings.Mid(strCad, 1, 4)) < 1900 || Conversion.Val(Strings.Mid(strCad, 1, 4)) > 2030 || Conversion.Val(Strings.Mid(strCad, 5, 2)) < 1 || Conversion.Val(Strings.Mid(strCad, 5, 2)) > 12 || Conversion.Val(Strings.Mid(strCad, 7, 2)) < 1 || Conversion.Val(Strings.Mid(strCad, 7, 2)) > 31)
            {
                strRegError = strRegError + "ERROR R04: FECHA DE INICIO FUERA DE FORMATO - ''; ";
                bolExisteError = true;
            }

            strCad = Strings.Mid(strCadenaIn, 11, 8);
            double dbNumericTemp2 = 0;
            if (!Double.TryParse(strCad, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp2))
            {
                strRegError = strRegError + "ERROR R04: FECHA DE FIN NO ES NUMÉRICO - ''; ";
                bolExisteError = true;
            }

            if (Conversion.Val(Strings.Mid(strCad, 1, 4)) < 1900 || Conversion.Val(Strings.Mid(strCad, 1, 4)) > 2030 || Conversion.Val(Strings.Mid(strCad, 5, 2)) < 1 || Conversion.Val(Strings.Mid(strCad, 5, 2)) > 12 || Conversion.Val(Strings.Mid(strCad, 7, 2)) < 1 || Conversion.Val(Strings.Mid(strCad, 7, 2)) > 31)
            {
                strRegError = strRegError + "ERROR R04: FECHA DE FIN FUERA DE FORMATO - ''; ";
                bolExisteError = true;
            }

            //LONGITUD DE REGISTRO
            if (strCadenaIn.Length != mdlGlobales.gintLongRegVC)
            {
                strRegError = strRegError + "ERROR R04: REGISTRO FUERA DE LONGITUD - ''; ";
                bolExisteError = true;
            }

            if (bolExisteError)
            {
                subRegistraErrores(strRegError);
            }

        }

        private void subValidaRegTipo05(string strCadenaIn)
        {


            bool bolExisteError = false;
            string strRegError = "REGISTRO: " + Strings.Mid(strCadenaIn, 1, 2) + " " + Strings.Mid(strCadenaIn, 3, 32) + ".- ";

            string strCad = Strings.Mid(strCadenaIn, 3, 16);
            double dbNumericTemp = 0;
            if (!Double.TryParse(strCad, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp))
            {
                strRegError = strRegError + "ERROR R05: FOLIO INICIAL NO ES NUMÉRICO - ''; ";
                bolExisteError = true;
            }

            if (Conversion.Val(strCad) == 0)
            {
                strRegError = strRegError + "ERROR R05: FOLIO INICIAL CON VALOR 0 - ''; ";
                bolExisteError = true;
            }

            strCad = Strings.Mid(strCadenaIn, 19, 16);
            double dbNumericTemp2 = 0;
            if (!Double.TryParse(strCad, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp2))
            {
                strRegError = strRegError + "ERROR R05: FOLIO FINAL NO ES NUMÉRICO - ''; ";
                bolExisteError = true;
            }

            if (Conversion.Val(strCad) == 0)
            {
                strRegError = strRegError + "ERROR R05: FOLIO FINAL CON VALOR 0 - ''; ";
                bolExisteError = true;
            }

            //LONGITUD DE REGISTRO
            if (strCadenaIn.Length != mdlGlobales.gintLongRegVC)
            {
                strRegError = strRegError + "ERROR R05: REGISTRO FUERA DE LONGITUD - ''; ";
                bolExisteError = true;
            }

            if (bolExisteError)
            {
                subRegistraErrores(strRegError);
            }

        }

        private void subValidaTrailer(string strCadenaIn)
        {


            bool bolExisteError = false;
            string strRegError = "REGISTRO: " + Strings.Mid(strCadenaIn, 1, 2) + " " + Strings.Mid(strCadenaIn, 3, 9) + ".- ";

            string strCad = Strings.Mid(strCadenaIn, 3, 9);
            double dbNumericTemp = 0;
            if (!Double.TryParse(strCad, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp))
            {
                strRegError = strRegError + "ERROR R99: NÚMERO DE REGISTROS NO ES NUMÉRICO - ''; ";
                bolExisteError = true;
            }

            if (Conversion.Val(txtNumRegistros.Text) != Conversion.Val(strCad))
            {
                strRegError = strRegError + "ERROR R99: NÚMERO DE REGISTROS NO ES IGUAL AL TOTAL DE REGISTROS LEIDOS - ''; ";
                bolExisteError = true;
            }

            //LONGITUD DE REGISTRO
            if (strCadenaIn.Length != mdlGlobales.gintLongRegVC)
            {
                strRegError = strRegError + "ERROR R99: REGISTRO FUERA DE LONGITUD - ''; ";
                bolExisteError = true;
            }

            if (bolExisteError)
            {
                subRegistraErrores(strRegError);
            }

        }

        public void subRegistraErrores(string strCadRegErrores)
        {

            string strPathError = String.Empty;
            string strFileMasivos = String.Empty;

            try
            {

                if (mdlGlobales.gintFileWrite != 0)
                {
                    FileSystem.PrintLine(mdlGlobales.gintFileWrite, strCadRegErrores);
                    mdlGlobales.gintTotRegistrosError++;
                    mdlGlobales.gbolError = true;
                }
                else
                {
                    if (FileSystem.Dir(strFileMasivos, FileAttribute.Normal) != "")
                    {
                        string tempRefParam = mdlGlobales.gcStrMasivosIni;
                        string tempRefParam2 = "RUTA_ERROR";
                        string tempRefParam3 = "RUTA";
                        strPathError = mdlGlobales.funGetParam(ref tempRefParam, ref tempRefParam2, ref tempRefParam3);

                    }
                    mdlGlobales.gintFileWrite = FileSystem.FreeFile();
                    FileSystem.FileOpen(mdlGlobales.gintFileWrite, strPathError + "\\ERRORES_" + txtArchivo.Text.Trim(), OpenMode.Output, OpenAccess.Default, OpenShare.Default, -1);
                    FileSystem.PrintLine(mdlGlobales.gintFileWrite, strCadRegErrores);
                    mdlGlobales.gintTotRegistrosError++;
                    mdlGlobales.gbolError = true;
                }
            }
            catch
            {
            }




            if (Information.Err().Number == 55)
            { //directorio y archivo no existe
                mdlGlobales.gintFileWrite = FileSystem.FreeFile();
                FileSystem.FileOpen(mdlGlobales.gintFileWrite, strPathError + "AVISOS_" + txtArchivo.Text.Trim() + ".TXT", OpenMode.Output, OpenAccess.Default, OpenShare.Default, -1);
                FileSystem.PrintLine(mdlGlobales.gintFileWrite, strCadRegErrores);
                mdlGlobales.gintTotRegistrosError++;
                mdlGlobales.gbolError = true;
            }

        }

        //AIS-1896 FSABORIO
        public bool SuspendFormClosing = false;
        private void frmVentasCruzadas_FormClosing(object sender, FormClosingEventArgs eventArgs)
        {
            //AIS-1896 FSABORIO
            SuspendFormClosing = true;
        }
    }
}