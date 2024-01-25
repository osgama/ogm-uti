using Artinsoft.VB6.Utils;
using Microsoft.VisualBasic;
using System;
using System.IO;
using System.Windows.Forms;
using VB6 = Microsoft.VisualBasic.Compatibility.VB6.Support;

namespace Masivos
{
    internal partial class frmConsTodaRemesa
        : System.Windows.Forms.Form
    {

        //*******************************************************************************
        //* Identificación: frmConsTodaRemesa                                           *
        //* Autor:          Luis E. Aguilar                                             *
        //* Instalación:    PRAXIS                                                      *
        //* Fecha:          9/09/2003                                                   *
        //* Versión:        1.0                                                         *
        //*******************************************************************************
        private XArrayDBObject.XArrayDB _grdDatosRemesa = null;
        XArrayDBObject.XArrayDB grdDatosRemesa
        {
            get
            {
                if (_grdDatosRemesa == null)
                {
                    _grdDatosRemesa = new XArrayDBObject.XArrayDB();
                }
                return _grdDatosRemesa;
            }
            set
            {
                _grdDatosRemesa = value;
            }
        }

        int gfIntProceso = 0;
        int gfIntContFolios = 0;

        //*******************************************************************************
        //* Finalidad:  Subrutina para Inicializar los controles de la forma
        //*******************************************************************************
        private void Form_Initialize_Renamed()
        {
            grdDatosRemesa.ReDim(0, 0, 0, 5);
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Inicializar los Valores de la Pantalla
        //*******************************************************************************
        //UPGRADE_WARNING: (1041) Form_Load event was upgraded to Form_Load method and has a new behavior.
        private void Form_Load()
        {
            grdDatosRemesa.ReDim(0, 0, 0, 5);
            mdlGlobales.subOcultaBotones();
            mdlGlobales.gstrFolInterno.Value = mdlGlobales.funZeroes(8);
            mdlGlobales.CentrarMDIHija(MDIMasivos.DefInstance, frmConsTodaRemesa.DefInstance);
            string tempRefParam = "";
            mdlGlobales.subDespMsg(ref tempRefParam);
            cmdGenArch.Enabled = false;
            mnuGenArchivo.Enabled = false;
        }

        private void frmConsTodaRemesa_Closed(Object eventSender, EventArgs eventArgs)
        {
            //Limpiar el arreglo antes de salir
            subLimpiaDatos();
            MemoryHelper.ReleaseMemory();
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Validar la Clave de la Remesa
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
                        string tempRefParam = "ERROR: LA CLAVE DE REMESA DEBE SER UN VALOR DIFERENTE A CERO PARA LA CONSULTA.";
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
                    subConsultaNumRegistros();
                    this.lblTotal.Text = "Total de folios en la remesa: " + (gfIntContFolios + 1).ToString();
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
        //* Finalidad:  Subrutina para Validar que la Clave de la Remesa no sea 0's o este Vacia
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
        //* Finalidad:  Subrutina para Validar el Menu de Generar Archivo
        //*******************************************************************************
        public void mnuGenArchivo_Click(Object eventSender, EventArgs eventArgs)
        {
            cmdGenArch_Click(cmdGenArch, new EventArgs());
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Validar el Menu de Limpiar
        //*******************************************************************************
        public void mnuLimpiar_Click(Object eventSender, EventArgs eventArgs)
        {
            cmdLimpiar_Click(cmdLimpiar, new EventArgs());
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Validar el Menu de Salir
        //*******************************************************************************
        public void mnuSalir_Click(Object eventSender, EventArgs eventArgs)
        {
            cmdSalir_Click(cmdSalir, new EventArgs());
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Seleccionar la Clave de la Remesa
        //*******************************************************************************
        private void txtClaveRemesa_Enter(Object eventSender, EventArgs eventArgs)
        {
            mdlGlobales.subSelTexto(txtClaveRemesa);
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Conocer Cuantos Registros se Van a Escribir en el DBG
        //*******************************************************************************
        private void subConsultaNumRegistros()
        {
            this.Cursor = Cursors.WaitCursor;
            gfIntContFolios = -1;
            mdlGlobales.gstrFolInterno.Value = mdlGlobales.funZeroes(8);
            mdlTranMasivo.enmEstatusRemesa IntEstatus = mdlTranMasivo.funConsultaEstatus(txtClaveRemesa.Text.Substring(txtClaveRemesa.Text.Length - Math.Min(txtClaveRemesa.Text.Length, 10)));
            if (IntEstatus != mdlTranMasivo.enmEstatusRemesa.stRegistrada && IntEstatus != mdlTranMasivo.enmEstatusRemesa.stError)
            {
                //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                string tempRefParam = mdlTranMasivo.funDescripcionEstatusRemesa(IntEstatus) + ", NO SE PUEDEN MOSTRAR LOS FOLIOS";
                MsgBoxStyle tempRefParam2 = (MsgBoxStyle)(((int)MsgBoxStyle.Exclamation) + ((int)MsgBoxStyle.OkOnly));
                mdlGlobales.subDespErrores(ref tempRefParam, ref tempRefParam2);
                return;
            }
            else if (IntEstatus == mdlTranMasivo.enmEstatusRemesa.stError)
            {
                //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                string tempRefParam3 = "NO EXISTE LA REMESA SOLICITADA";
                MsgBoxStyle tempRefParam4 = (MsgBoxStyle)(((int)MsgBoxStyle.Exclamation) + ((int)MsgBoxStyle.OkOnly));
                mdlGlobales.subDespErrores(ref tempRefParam3, ref tempRefParam4);
                return;
            }
            if (mdlTranMasivo.funEnviaRecibe5562("5562", "05", mdlGlobales.gstrNumRemesa.Value))
            {
                //MMS 11/05 Se recorre la posición del campo Clave Respuesta (50 a 51)
                if (Strings.Mid(mdlTranMasivo.gvRecive5562_05, 51, 2) != "00")
                {
                    //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                    string tempRefParam5 = "Error.";
                    MsgBoxStyle tempRefParam6 = (MsgBoxStyle)(((int)MsgBoxStyle.Exclamation) + ((int)MsgBoxStyle.OkOnly));
                    mdlGlobales.subDespErrores(ref tempRefParam5, ref tempRefParam6);
                    txtClaveRemesa.Focus();
                }
                else
                {
                    //MMS 11/05 Se recorre la posición del campo Flag Información (118 a 120)

                    while ((Strings.Mid(mdlTranMasivo.gvRecive5562_05, 120, 1) == "1"))
                    { //Flag de información
                        subReciveInfRemesa();
                        mdlGlobales.gstrFolPreimpreso.Value = Strings.Mid(mdlTranMasivo.gvRecive5562_05, 8, 16);
                        if (!mdlTranMasivo.funEnviaRecibe5562("5562", "05", mdlGlobales.gstrNumRemesa.Value))
                        {
                            break;
                        }
                    };
                    //MMS 11/05 Se recorre la posición del campo Flag Información (118 a 120)
                    if (Strings.Mid(mdlTranMasivo.gvRecive5562_05, 120, 1) == "0")
                    {
                        subReciveInfRemesa();
                    }
                    subPresentaRemesa();
                    cmdGenArch.Enabled = true;
                    mnuGenArchivo.Enabled = true;
                }
            }
            else
            {
                mdlGlobales.subDespMensajes(" ");
            }
            this.Cursor = Cursors.Default;
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Desplegar la Informacion en DBgrid
        //*******************************************************************************
        private void subPresentaRemesa()
        {
            grdDatosRemesa.ReDim(0, gfIntContFolios, 0, 5);
            for (int gfIntProceso = 0; gfIntProceso <= mdlTranMasivo.estConsRemesa.GetUpperBound(0); gfIntProceso++)
            {
                grdDatosRemesa.Set(gfIntProceso, 0, mdlTranMasivo.estConsRemesa[gfIntProceso].strFolioPreImpr); //Folio Impreso
                grdDatosRemesa.Set(gfIntProceso, 1, mdlTranMasivo.estConsRemesa[gfIntProceso].strNomSolicita); //Nombre del solicitante
                grdDatosRemesa.Set(gfIntProceso, 2, mdlTranMasivo.estConsRemesa[gfIntProceso].strEstatus); //Estatus
                grdDatosRemesa.Set(gfIntProceso, 3, mdlTranMasivo.estConsRemesa[gfIntProceso].strProceso); //Proceso
                grdDatosRemesa.Set(gfIntProceso, 4, mdlTranMasivo.estConsRemesa[gfIntProceso].strSigProceso); //Siguiente Proceso
                grdDatosRemesa.Set(gfIntProceso, 5, mdlTranMasivo.estConsRemesa[gfIntProceso].strCausaRecha); //Causa del Rechazo
            }
            tdbInfRemesa.Array = grdDatosRemesa;
            tdbInfRemesa.ReBind();
            tdbInfRemesa.Refresh();
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Generar el Archivo
        //*******************************************************************************
        private void cmdGenArch_Click(Object eventSender, EventArgs eventArgs)
        {
            int intNumFichero = 0;
            string strGenArchivo = String.Empty;
            string strNoRemesa = String.Empty;
            string strPath = String.Empty;
            string strVerifica = String.Empty;
            try
            {
                string tempRefParam = "GENERANDO ARCHIVO, POR FAVOR ESPERE ...";
                mdlGlobales.subDespMsg(ref tempRefParam);
                this.Cursor = Cursors.WaitCursor;
                //AIS-1434 rvillalta
                strVerifica = Convert.ToString(grdDatosRemesa[gfIntProceso, 0]) + Convert.ToString(grdDatosRemesa[gfIntProceso, 1]) + Convert.ToString(grdDatosRemesa[gfIntProceso, 2]) + Convert.ToString(grdDatosRemesa[gfIntProceso, 3]) + Convert.ToString(grdDatosRemesa[gfIntProceso, 4]) + Convert.ToString(grdDatosRemesa[gfIntProceso, 5]);
                if (strVerifica.Trim() != "")
                {
                    strNoRemesa = txtClaveRemesa.Text;
                    //AIS-1893 FSABORIO

                    //MIG WXP INI JGC 20090825
                    string strMASRuta3 = mdlRegistry.RegistryMasivos("MASRuta3");
                    //MIG WXP FIN JGC 20090825
                    //strPath = mdlMain.ApplicationPath + "\\Archivos\\";
                    strPath = mdlMain.ApplicationPath + strMASRuta3;

                    //Validar que exista el directorio
                    if (FileSystem.Dir(strPath, FileAttribute.Normal) == "")
                    {
                        //El directorio no existe, por tanto hay que crearlo
                        Directory.CreateDirectory(strPath);
                    }
                    intNumFichero = FileSystem.FreeFile();

                    //MIG WXP INI JGC 20090825
                    string strMASRuta4 = mdlRegistry.RegistryMasivos("MASRuta4");
                    //MIG WXP FIN JGC 20090825
                    //FileSystem.FileOpen(intNumFichero, strPath + "\\rep_" + strNoRemesa + ".TXT", OpenMode.Output, OpenAccess.Default, OpenShare.Default, -1);
                    FileSystem.FileOpen(intNumFichero, strPath + strMASRuta4 + strNoRemesa + ".TXT", OpenMode.Output, OpenAccess.Default, OpenShare.Default, -1);

                    for (int intContArch = 0; intContArch <= gfIntContFolios; intContArch++)
                    {//AIS-1434 rvillalta
                        strGenArchivo = Convert.ToString(grdDatosRemesa[intContArch, 0]) + "," + Convert.ToString(grdDatosRemesa[intContArch, 1]) + "," + mdlTranMasivo.estConsRemesa[intContArch].strCveEstatus + "," + Convert.ToString(grdDatosRemesa[intContArch, 2]) + "," + mdlTranMasivo.estConsRemesa[intContArch].strCveProceso + "," + Convert.ToString(grdDatosRemesa[intContArch, 3]) + "," + mdlTranMasivo.estConsRemesa[intContArch].strCveSigProceso + "," + Convert.ToString(grdDatosRemesa[intContArch, 4]) + "," + mdlTranMasivo.estConsRemesa[intContArch].strCveCausaRecha + "," + Convert.ToString(grdDatosRemesa[intContArch, 5]);
                        FileSystem.PrintLine(intNumFichero, strGenArchivo);
                    }
                    FileSystem.FileClose(intNumFichero);
                    //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                    //MIG WXP INI JGC 20090825
                    //string tempRefParam2 = "SE HA GENERADO EL ARCHIVO '" + strPath + "\\rep_" + strNoRemesa + ".TXT' CON LAS SOLICITUDES DE LA REMESA";
                    string tempRefParam2 = "SE HA GENERADO EL ARCHIVO '" + strPath + strMASRuta4 + strNoRemesa + ".TXT' CON LAS SOLICITUDES DE LA REMESA";
                    //MIG WXP FIN JGC 20090825
                    MsgBoxStyle tempRefParam3 = (MsgBoxStyle)(((int)MsgBoxStyle.Information) + ((int)MsgBoxStyle.OkOnly));
                    string tempRefParam4 = "ARCHIVO GENERADO";
                    mdlGlobales.subDespErrores(ref tempRefParam2, ref tempRefParam3, ref tempRefParam4);
                }
                else
                {
                    //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                    string tempRefParam5 = "ERROR: NO HAY INFORMACION PARA GENERAR EL ARCHIVO.";
                    MsgBoxStyle tempRefParam6 = (MsgBoxStyle)(((int)MsgBoxStyle.Exclamation) + ((int)MsgBoxStyle.OkOnly));
                    mdlGlobales.subDespErrores(ref tempRefParam5, ref tempRefParam6);
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception excep)
            {

                this.Cursor = Cursors.Default;
                //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                string tempRefParam7 = "ERROR AL INTENTAR GENERAR EL ARCHIVO; " + Information.Err().Number.ToString() + ": " + excep.Message;
                MsgBoxStyle tempRefParam8 = (MsgBoxStyle)(((int)MsgBoxStyle.Critical) + ((int)MsgBoxStyle.OkOnly));
                mdlGlobales.subDespErrores(ref tempRefParam7, ref tempRefParam8);
            }
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Limpiar los Datos Cuando se Utilize este Comando
        //*******************************************************************************
        private void cmdLimpiar_Click(Object eventSender, EventArgs eventArgs)
        {
            subLimpiaDatos();
            txtClaveRemesa.Text = "";
            txtClaveRemesa.Focus();
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Limpiar los Datos
        //*******************************************************************************
        private void subLimpiaDatos()
        {
            //Limpiar el arreglo dinámico
            //UPGRADE_WARNING: (1042) Array estConsRemesa may need to have individual elements initialized.
            mdlTranMasivo.estConsRemesa = ArraysHelper.InitializeArray<mdlTranMasivo.udtConsRemesa>(1);
            grdDatosRemesa.Set(0, 0, " ");
            grdDatosRemesa.Set(0, 1, " ");
            grdDatosRemesa.Set(0, 2, " ");
            grdDatosRemesa.Set(0, 3, " ");
            grdDatosRemesa.Set(0, 4, " ");
            grdDatosRemesa.Set(0, 5, " ");
            gfIntContFolios = -1;
            grdDatosRemesa.ReDim(0, 0, 0, 5);
            tdbInfRemesa.Array = grdDatosRemesa;
            tdbInfRemesa.ReBind();
            mdlGlobales.gstrFolPreimpreso.Value = mdlGlobales.funZeroes(16);
            cmdGenArch.Enabled = false;
            mnuGenArchivo.Enabled = false;
            gfIntContFolios = 0;
            lblTotal.Text = "Total de folios en la remesa: 0";
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Salir de esta Pantalla, Habilitando el MDI
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
        //* Finalidad:  Subrutina para Guardar en una Estructura de Arreglos la Informacion
        //*******************************************************************************
        private void subReciveInfRemesa()
        {
            this.Cursor = Cursors.WaitCursor;
            mdlGlobales.subDespMensajes("POR FAVOR ESPERE ...");
            int IntIndiceInicial = 177; //Indice donde comienza el primero folio
            int IntCuentaOcurrencias = 1;

            while ((Conversion.Val(Strings.Mid(mdlTranMasivo.gvRecive5562_05, IntIndiceInicial, 16)) != 0) && (IntCuentaOcurrencias <= 33))
            {
                gfIntContFolios++;
                //UPGRADE_WARNING: (1042) Array estConsRemesa may need to have individual elements initialized.
                mdlTranMasivo.estConsRemesa = ArraysHelper.RedimPreserve<mdlTranMasivo.udtConsRemesa[]>(mdlTranMasivo.estConsRemesa, new int[] { gfIntContFolios + 1 });
                //mdlTranMasivo.udtConsRemesa withVar = mdlTranMasivo.estConsRemesa[gfIntContFolios];
                mdlTranMasivo.estConsRemesa[gfIntContFolios].strFolioPreImpr = Strings.Mid(mdlTranMasivo.gvRecive5562_05, IntIndiceInicial, 16); //Folio Pre Impreso
                IntIndiceInicial += 16;
                mdlTranMasivo.estConsRemesa[gfIntContFolios].strNomSolicita = Strings.Mid(mdlTranMasivo.gvRecive5562_05, IntIndiceInicial, 26); //Nombre del solicitante
                IntIndiceInicial += 26;
                mdlTranMasivo.estConsRemesa[gfIntContFolios].strCveEstatus = Strings.Mid(mdlTranMasivo.gvRecive5562_05, IntIndiceInicial, 3); //Estatus  MMS 11/05 Incremento en la longitud del campo Estatus (2 a 3)
                string tempRefParam = "6";
                string tempRefParam2 = null;
                string tempRefParam3 = null;
                string tempRefParam4 = null;
                Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam5 = Catalogos.clsCatalogos.enmCamposCatalogos.Default;
                string tempRefParam6 = "D";
                if (mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam, mdlTranMasivo.estConsRemesa[gfIntContFolios].strCveEstatus, tempRefParam2, tempRefParam3, ref tempRefParam4, ref tempRefParam5, ref tempRefParam6))
                {
                    mdlTranMasivo.estConsRemesa[gfIntContFolios].strEstatus = Strings.Mid(mdlComunica.OleCatalogos.getDescripcion, 5, Strings.Len(mdlComunica.OleCatalogos.getDescripcion)).Trim();
                }
                IntIndiceInicial += 3; //MMS 11/05 Incremento en la longitud del campo Clave Estatus (2 a 3)
                mdlTranMasivo.estConsRemesa[gfIntContFolios].strCveProceso = Strings.Mid(mdlTranMasivo.gvRecive5562_05, IntIndiceInicial, 3); //Proceso  MMS 11/05 Incremento en la longitud del campo Proceso (2 a 3)
                string tempRefParam7 = "10";
                string tempRefParam8 = null;
                string tempRefParam9 = null;
                string tempRefParam10 = null;
                Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam11 = Catalogos.clsCatalogos.enmCamposCatalogos.Default;
                string tempRefParam12 = "D";
                if (mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam7, mdlTranMasivo.estConsRemesa[gfIntContFolios].strCveProceso, tempRefParam8, tempRefParam9, ref tempRefParam10, ref tempRefParam11, ref tempRefParam12))
                {
                    mdlTranMasivo.estConsRemesa[gfIntContFolios].strProceso = mdlComunica.OleCatalogos.getDescripcion;
                }
                else
                {
                    mdlTranMasivo.estConsRemesa[gfIntContFolios].strProceso = "";
                }
                IntIndiceInicial += 3; //MMS 11/05 Incremento en la longitud del campo Clave Proceso (2 a 3)
                mdlTranMasivo.estConsRemesa[gfIntContFolios].strCveSigProceso = Strings.Mid(mdlTranMasivo.gvRecive5562_05, IntIndiceInicial, 3); //Siguiente Proceso  MMS 11/05 Incremento en la longitud del campo Sig Proceso (2 a 3)
                string tempRefParam13 = "10";
                string tempRefParam14 = null;
                string tempRefParam15 = null;
                string tempRefParam16 = null;
                Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam17 = Catalogos.clsCatalogos.enmCamposCatalogos.Default;
                string tempRefParam18 = "D";
                if (mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam13, mdlTranMasivo.estConsRemesa[gfIntContFolios].strCveSigProceso, tempRefParam14, tempRefParam15, ref tempRefParam16, ref tempRefParam17, ref tempRefParam18))
                {
                    mdlTranMasivo.estConsRemesa[gfIntContFolios].strSigProceso = mdlComunica.OleCatalogos.getDescripcion;
                }
                else
                {
                    if (mdlTranMasivo.estConsRemesa[gfIntContFolios].strCveSigProceso == "***")
                    { //MMS 11/05 Incremento en la longitud del campo (2 a 3)
                        mdlTranMasivo.estConsRemesa[gfIntContFolios].strSigProceso = mdlTranMasivo.estConsRemesa[gfIntContFolios].strCveSigProceso;
                    }
                    else
                    {
                        mdlTranMasivo.estConsRemesa[gfIntContFolios].strSigProceso = "";
                    }
                }
                IntIndiceInicial += 3; //MMS 11/05 Incremento en la longitud del campo Siguiente Proceso (2 a 3)
                mdlTranMasivo.estConsRemesa[gfIntContFolios].strCveCausaRecha = Strings.Mid(mdlTranMasivo.gvRecive5562_05, IntIndiceInicial, 4); //Causa del Rechazo
                string tempRefParam19 = "25";
                string tempRefParam20 = Strings.Mid(mdlTranMasivo.gvRecive5562_05, IntIndiceInicial, 4);
                string tempRefParam21 = null;
                string tempRefParam22 = null;
                string tempRefParam23 = null;
                Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam24 = Catalogos.clsCatalogos.enmCamposCatalogos.Default;
                string tempRefParam25 = "D";
                if (mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam19, tempRefParam20, tempRefParam21, tempRefParam22, ref tempRefParam23, ref tempRefParam24, ref tempRefParam25))
                {
                    mdlTranMasivo.estConsRemesa[gfIntContFolios].strCausaRecha = mdlComunica.OleCatalogos.getDescripcion;
                }
                else
                {
                    mdlTranMasivo.estConsRemesa[gfIntContFolios].strCausaRecha = "";
                }
                IntIndiceInicial += 4; //Ya avanzó los 55 elementos de la longitud de la ocurrencia
                IntCuentaOcurrencias++;
            };
            mdlGlobales.subDespMensajes("");
            this.Cursor = Cursors.Default;
        }

        //AIS-1896 FSABORIO
        public bool SuspendFormClosing = false;
        private void frmConsTodaRemesa_FormClosing(object sender, FormClosingEventArgs eventArgs)
        {
            //AIS-1896 FSABORIO
            SuspendFormClosing = true;
        }
    }
}