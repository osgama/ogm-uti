using Artinsoft.VB6.Gui;
using Artinsoft.VB6.Utils;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility.VB6;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using VB6 = Microsoft.VisualBasic.Compatibility.VB6.Support;

namespace Masivos
{
    class mdlGlobales
    {

        //*******************************************************************************
        //* Identificación: mdlGlobales.bas                                             *
        //* Autor:          Odeth S. Montaño López                                      *
        //* Instalación:    BANAMEX, S.A.                                               *
        //* Fecha:          07/01/2003                                                  *
        //* Versión:        1.0                                                         *
        //*******************************************************************************
        //* Objetivo: Módulo de rutinas globales del sistema                            *
        //* - subAbreBitacora
        //*******************************************************************************
        //* Modificación: 1                                                             *
        //* Descripción: Depuración para exclusivo Módulo de Masivos.                   *                                            *
        //* Fecha:                                                                      *
        //* Versión:                                                                    *
        //* Modificó:  STG                                                              *
        //*******************************************************************************

        //VARIABLES PARA ACCESO AL SISTEMA
        static public FixedLengthString gstrNominaAces = new FixedLengthString(8);
        static public FixedLengthString gstrNomina = new FixedLengthString(10);
        static public FixedLengthString gstrPwd = new FixedLengthString(8);
        static public bool gblnEstaSeg = false;
        static public bool gblnRefirma = false;
        static public bool gblnProcesaArchivo = false;
        static public bool gblnDeclinaFolios = false;
        static public bool gblnSelecciono = false; //Variable para el control de la selección de causas
        static public bool gblnEnvioTansac = false;
        static public bool gblnAutorizando = false; //Variable que indica que la autorización se está llevando a cabo
        static public bool gblnFinaliza = false;
        static public string gstrClaveDeclina = String.Empty;
        static public bool gblnBandAcess = false;
        static public bool gblnBandCancela = false;
        static public bool gblnChecaAcess = false;
        static public bool gblnChecaCancela = false;
        static private edvddv.Cedvddv _ENCRIP = null;
        static public edvddv.Cedvddv ENCRIP
        {
            get
            {
                if (_ENCRIP == null)
                {
                    _ENCRIP = new edvddv.Cedvddv();
                }
                return _ENCRIP;
            }
            set
            {
                _ENCRIP = value;
            }
        }
        //LAM/PRAXIS   Encriptación
        static public string gstrDatosEncripta = String.Empty; //LAM/PRAXIS   Encriptación
        static public string gstrDatosEncripta1 = String.Empty; //LAM/PRAXIS   Encriptación
        static public string gstrDatosEncripta2 = String.Empty; //LAM/PRAXIS   Encriptación
        static public string gstrDatosEncripta3 = String.Empty; //LAM/PRAXIS   Encriptación

        //VARIABLES GLOBALES DE INFO. DEL FOLIO
        static public FixedLengthString gstrPromocion = new FixedLengthString(4);
        static public FixedLengthString gstrFolPreimpreso = new FixedLengthString(16);
        static public FixedLengthString gstrFolInterno = new FixedLengthString(8);
        static public FixedLengthString gstrTramite = new FixedLengthString(2);
        static public FixedLengthString gstrTipoEntOrig = new FixedLengthString(2);
        static public FixedLengthString gstrEntOrig = new FixedLengthString(4);
        static public FixedLengthString gstrGpoEntOrig = new FixedLengthString(4);
        static public FixedLengthString gstrCveEntOrig = new FixedLengthString(4);
        static public FixedLengthString gstrNumRemesa = new FixedLengthString(10);
        static public FixedLengthString gstrFamilia = new FixedLengthString(2);
        static public FixedLengthString gstrTipoSol = new FixedLengthString(2);
        static public FixedLengthString gstrProceso = new FixedLengthString(3); //MMS 11/05 Incremento en la longitud del campo (2 a 3)
        static public FixedLengthString gstrEstatus = new FixedLengthString(3); //MMS 11/05 Incremento en la longitud del campo (2 a 3)
        static public FixedLengthString gstrInicial = new FixedLengthString(3); //MMS 11/05 Incremento en la longitud del campo (2 a 3)
        static public FixedLengthString gstrEstatusInicial = new FixedLengthString(3); //MMS 11/05 Incremento en la longitud del campo (2 a 3)
        static public FixedLengthString gstrDescTramite = new FixedLengthString(33);
        static public FixedLengthString gstrDescTipoEntOrig = new FixedLengthString(33);
        static public FixedLengthString gstrDescPromocion = new FixedLengthString(33);
        static public FixedLengthString gstrDescEntOrig = new FixedLengthString(33);
        static public FixedLengthString gstrFolPreImpIni = new FixedLengthString(16);
        static public FixedLengthString gstrFolPreImpFin = new FixedLengthString(16);
        static public FixedLengthString gstrIndicaCambio = new FixedLengthString(1);
        static public FixedLengthString gstrPantalla = new FixedLengthString(2);
        static public FixedLengthString gstrFlagInfo = new FixedLengthString(1); //En actualizac. 1=Verifica concurrencia
        static public FixedLengthString gstrFecInicial = new FixedLengthString(8);
        static public FixedLengthString gstrHoraInicial = new FixedLengthString(6);
        static public FixedLengthString gstrFecFinal = new FixedLengthString(8);
        static public FixedLengthString gstrHoraFinal = new FixedLengthString(6);
        static public FixedLengthString gstrCausaDec = new FixedLengthString(4);
        static public FixedLengthString gstrComentarios = new FixedLengthString(150);
        static public FixedLengthString gstrCvePaquete = new FixedLengthString(4); //Variable para guardar la clave de paquete generado
        static public string gstrRutaTemp = String.Empty; //Variable para guardar la ruta de la carpeta de temporales
        static public FixedLengthString gstrSigProceso = new FixedLengthString(3); //Variable para almacenar el siguiente proceso   'MMS 11/05  Incremento en la longitud del campo (2 a 3)
        static public string gstrNombreUsuario = String.Empty;
        static public bool gbolEstaIniciado = false;

        static public FixedLengthString gstrFechaSolicitud = new FixedLengthString(8);
        static public string gstrCvePromoDflt = String.Empty;
        static public bool gbolPromoEncontrada = false;
        static public bool gbolPromoValida = false;
        static public bool gbolEncontrePromoDflt = false;
        static public bool gbolAplicaComision = false;
        static public bool gbolAplicaPromo = false;
        //Variables para guardar los datos de frmRegRemesas
        static public FixedLengthString gstrFechaAceptacion = new FixedLengthString(8);
        static public FixedLengthString gstrFechaProceso = new FixedLengthString(8);
        static public FixedLengthString gstrFechaIngreso = new FixedLengthString(8);
        //Variable para ejecutar la transacción 5562 02
        static public bool gblnEjecutaRegistro = false;
        //Variable para registro de remesas
        static public bool gblnRemesaRegistrada = false;
        //Variable para determinar si un usuario es nómina nivel 3
        static public bool gblnUsuarioAutorizado = false;
        //Constante para determinar si un archivo trae patrón erróneo
        public const string gstrPatron = "}y";

        //AUTORIZACIONES
        static public FixedLengthString gstrNominaAutoriza = new FixedLengthString(10);
        static public FixedLengthString gstrPrefBanco = new FixedLengthString(16);
        static public FixedLengthString gstrProducto = new FixedLengthString(4);
        static public FixedLengthString gstrTipoValorac = new FixedLengthString(1);
        static public double gdblLinCredIni = 0;
        static public double gdblLinCredCap = 0;

        //ASG: LISTA DE CARACTERES ACEPTADOS PARA ARMAR LOS BLOQUES
        public const string gcstrCaracteresValidos = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890-,&@# ";
        static public string gstrCaracteresInvalidos = String.Empty;

        static public string gstrRegHeaderVC = String.Empty;
        static public string gstrRegTrailerVC = String.Empty;
        public const int gintLongRegVC = 319; //Longitud de registro de archivo Ventas Cruzadas
        static public int gintFileWrite = 0; //Control de archivo de errores
        static public bool gbolError = false; //Indicadir de error encontrado
        static public int gintTotRegistrosError = 0; //Total de Registros con algún error
        static public string gstrTramiteAnt = String.Empty; //Tramite del registro anterior
        static public string gstrFamProdAnt = String.Empty; //Familia Producto del registro anterior
        static public string gstrTipSolAnt = String.Empty; //Tipo Solicitud del registro anterior
        static public string gstrEntFedAnt = String.Empty; //Entidad Federativa del registro anterior
        static public string gstrPlazoAnt = String.Empty; //Entidad Federativa del registro anterior
        static public bool gbolProductoError = false;
        static public bool gbolEntFedError = false;
        static public bool gbolPlazoError = false;

        //CAMBIO DE DECISION
        static public bool gbolCDecision = false;
        static public bool gbolRecon = false;
        static public bool gbolExcep = false;

        [DllImport("User32.dll")]
        extern public static short FindWindow(string lpClassName, string lpWindowName);

        [DllImport("Kernel32.dll")]
        extern public static double GetModuleUsage(short hModule);


        //FUNCION PARA LEER ARCHIVOS DE PARAMETROS (*.INI) 'FSW/JEM
        [DllImport("Kernel32.dll")]
        extern public static int GetPrivateProfileString(string lpApplicationName, string lpKeyName, string lpDefault, string lpReturnedString, int nSize, string lpFileName);

        [DllImport("Kernel32.dll")]
        extern public static int WritePrivateProfileString(string lpApplicationName, string lpKeyName, string lpString, string lpFileName);

        //AIS-1893 FSABORIO

        //MIG WXP INI JGC 20090825
        //public static readonly string gcStrMasivosIni = mdlMain.ApplicationPath + "\\masivos.ini"; //Variable para almacenar el nombre del archivo INI
        //public static readonly string gcStrMasivosIni = mdlMain.ApplicationPath + "\\masivos.ini"; //Variable para almacenar el nombre del archivo INI
        //MIG WXP FIN JGC 20090825

        static public int gLngArchivoBitacora = 0; //ASG: Definición del handle del arhivo de bitácora

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct udfAControlador
        { //ESTRUCTURA DE CONTROL
            public FixedLengthString strACEtiqueta;
            public string strFolioPreimp; //Cada controlador tendrá el folio correspondiente al header
            public int IntACLongitud;
            public FixedLengthString strACEstatus; //1 Enviado, 0 por Enviar
            public string strACDatos;
            public string strFlagInform;
            public static udfAControlador CreateInstance()
            {
                udfAControlador result = new udfAControlador();
                result.strFolioPreimp = String.Empty;
                result.strACDatos = String.Empty;
                result.strFlagInform = String.Empty;
                result.strACEtiqueta = new FixedLengthString(4);
                result.strACEstatus = new FixedLengthString(1); //1 Enviado, 0 por Enviar
                return result;
            }
        }
        static public udfAControlador[] strAControlador;// = ArraysHelper.InitializeArray<udfAControlador[]>(new int[]{});

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct udfTranEncol
        { //TRANSACCION DE ENCOLAMIENTO
            public FixedLengthString strTECveTran;
            public FixedLengthString strTEFiller01;
            public FixedLengthString strTECveSubtran;
            public FixedLengthString strTEFillerTran;
            public FixedLengthString strTEServer;
            public FixedLengthString strTEIdTrans;
            public FixedLengthString strTEFolio;
            public FixedLengthString strTEMapa; //MMS 11/05 Incremento en la longitud del campo (2 a 3)
            public FixedLengthString strTEEstado; //Es el Proceso  MMS 11/05 Incremento en la longitud del campo (2 a 3)
            public FixedLengthString strTEEstatus; //MMS 11/05  Incremento en la longitud del campo (2 a 3)

            public static udfTranEncol CreateInstance()
            {
                udfTranEncol result = new udfTranEncol();
                result.strTECveTran = new FixedLengthString(4);
                result.strTEFiller01 = new FixedLengthString(1);
                result.strTECveSubtran = new FixedLengthString(2);
                result.strTEFillerTran = new FixedLengthString(163);
                result.strTEServer = new FixedLengthString(15);
                result.strTEIdTrans = new FixedLengthString(4);
                result.strTEFolio = new FixedLengthString(8);
                result.strTEMapa = new FixedLengthString(3); //MMS 11/05 Incremento en la longitud del campo (2 a 3)
                result.strTEEstado = new FixedLengthString(3); //Es el Proceso  MMS 11/05 Incremento en la longitud del campo (2 a 3)
                result.strTEEstatus = new FixedLengthString(3); //MMS 11/05  Incremento en la longitud del campo (2 a 3)
                return result;
            }
        }
        static public udfTranEncol strTranEncol = udfTranEncol.CreateInstance();
        static public string strPathError = String.Empty; //Ruta del archivo de errores del encolamiento

        static public void subObtenCaracteresInvalidos()
        {
            StringBuilder strCadena = new StringBuilder();
            for (int IntI = 32; IntI <= 255; IntI++)
            {
                if ((gcstrCaracteresValidos.IndexOf(Strings.Chr(IntI).ToString().ToUpper()) + 1) == 0)
                {
                    if (Strings.Chr(IntI).ToString() != "")
                    {
                        strCadena.Append(Strings.Chr(IntI).ToString());
                    }
                }
            }
            gstrCaracteresInvalidos = strCadena.ToString();
        }

        //*******************************************************************************
        //* Subrutina que Abre la Bitacora del Front de Aries
        //*******************************************************************************
        static public void subAbreBitacora()
        {
            //Abre archivo de bitácora de cambios
            string strPath = String.Empty;
            try
            {

                //MIG WXP INI JGC 20090825
                string strMASRutaBItacora = mdlRegistry.RegistryMasivos("MASRutaBItacora");
                //MIG WXP FIN JGC 20090825
                //AIS-1893 FSABORIO
                String path = mdlMain.ApplicationPath;
                String pathLog = mdlMain.ApplicationPath + strMASRutaBItacora;
                strPath = path + strMASRutaBItacora + "\\BM" + DateTime.Today.ToString("yyyyMMdd") + ".TXT";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                if (!Directory.Exists(pathLog))
                    Directory.CreateDirectory(pathLog);

                FileSystem.FileOpen(gLngArchivoBitacora, strPath, OpenMode.Append, OpenAccess.Default, OpenShare.Default, -1);
                gstrDatosEncripta = ENCRIP.EDV("--- INICIO DE BITACORA DEL FRONT DE ARIES MASIVOS ---> ");
                gstrDatosEncripta1 = ENCRIP.EDV(DateTime.Now.ToString("HH:mm:ss") + " -- " + MDIMasivos.DefInstance.Wskprincipal.LocalIP);

                FileSystem.PrintLine(gLngArchivoBitacora, gstrDatosEncripta + gstrDatosEncripta1);
                FileSystem.PrintLine(gLngArchivoBitacora, ENCRIP.EDV("--- VERSIÓN DE MÓDULO ---> " + MDIMasivos.DefInstance.funEscribeVersion()));
            }
            catch (Exception excep)
            {


                if (Information.Err().Number == 53)
                { //archivo no existe
                    gLngArchivoBitacora = FileSystem.FreeFile();
                    FileSystem.FileOpen(gLngArchivoBitacora, strPath, OpenMode.Output, OpenAccess.Default, OpenShare.Default, -1);
                }
                else if (Information.Err().Number != 55)
                {
                    //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                    string tempRefParam = "Error, " + Information.Err().Number.ToString() + ": " + excep.Message;
                    MsgBoxStyle tempRefParam2 = (MsgBoxStyle)(((int)MsgBoxStyle.Critical) + ((int)MsgBoxStyle.OkOnly));
                    subDespErrores(ref tempRefParam, ref tempRefParam2);
                }
            }
        }

        //*******************************************************************************
        //* Subrutina que Registra en el Archivo de Bitacora del Front de Aries los Cambios
        //*******************************************************************************
        static public void subRegBitacora(string strTipo)
        {
            //Registra en el archivo de bitácora de cambios, los cambios efectuados
            //strCadena --> Mensaje enviado, recibido del host
            //strTipo   --> Valores E=Envio al host, R=Recepción del host
            string strCadena = String.Empty, strMensaje = String.Empty;
            if (strTipo == "E")
            {
                strCadena = mdlComunica.gvMensaje;
                strMensaje = mdlComunica.funDepuraMensaje(strCadena);
                strCadena = "ENVIO:  " + strMensaje;
            }
            else
            {
                strCadena = mdlComunica.gvRecive;
                strMensaje = mdlComunica.funDepuraMensaje(strCadena);
                strCadena = "RECIBE: " + strMensaje;
            }
            gstrDatosEncripta = ENCRIP.EDV(DateTime.Now.AddDays(-DateTime.Now.Date.ToOADate()).ToString("HH:mm:ss"));
            gstrDatosEncripta1 = ENCRIP.EDV(" ");
            gstrDatosEncripta2 = ENCRIP.EDV(strCadena);
            FileSystem.PrintLine(gLngArchivoBitacora, gstrDatosEncripta + gstrDatosEncripta1 + gstrDatosEncripta2);
        }

        //****************************************************************************
        //* PROCEDIMIENTO.......: CentrarMDIHija                                     *
        //*--------------------------------------------------------------------------*
        //* AUTOR(es)...........: Ing. Arturo Pardo Bello                            *
        //* MODIFICADO POR .....:                                                    *
        //* FECHA DE ELABORACION: 25/06/94                                           *
        //*--------------------------------------------------------------------------*
        //* DESCRIPCION.........: Este procedimiento permite centrar una ventana MDI *
        //* hija dentro de la forma MDI padre.                                       *
        //*                                                                          *
        //* SINTAXIS........: Call CentrarMDIHija(pFrmPadre, pFrmHija)               *
        //*                                                                          *
        //* PARAMETROS......: pFrmPadre  Nombre de la Forma MDI Padre                *
        //*                   pFrmHija   Nombre de la Forma MDI Hija                 *
        //*                                                                          *
        //* RETORNO.........: Ninguno                                                *
        //****************************************************************************
        static public void CentrarMDIHija(MDIMasivos pFrmPadre, Form pFrmHija)
        {

            int intLiTop = Convert.ToInt32((((float)VB6.PixelsToTwipsY(pFrmPadre.ClientRectangle.Height)) - ((float)VB6.PixelsToTwipsY(pFrmHija.Height))) / 2);
            int intLiLeft = Convert.ToInt32((((float)VB6.PixelsToTwipsX(pFrmPadre.ClientRectangle.Width)) - ((float)VB6.PixelsToTwipsX(pFrmHija.Width))) / 2);

            pFrmHija.SetBounds(Convert.ToInt32((float)VB6.TwipsToPixelsX(intLiLeft)), Convert.ToInt32((float)VB6.TwipsToPixelsY(intLiTop)), 0, 0, BoundsSpecified.X | BoundsSpecified.Y);
        }

        //____________________________________________________________________________
        // Título:   subAcepta_Return
        // Descripción: Acepta la tecla return y pasa al siguiente campo
        // Módulo:   Aplic.bas
        // Versión:  1.0
        // Fecha:    Octubre 2002
        // Autor:    Gloria Campos
        //____________________________________________________________________________
        static public void subAcepta_Return(ref  Keys KeyAscii)
        {
            if (KeyAscii == Keys.Return)
            {
                VB6.SendKeys("{TAB}", false);
                //UPGRADE_WARNING: (6021) Casting 'byte' to Enum may cause different behaviour.
                KeyAscii = (Keys)0;
            }
        }

        //Función que regresa una cadena rellena de ceros de longitud wRepite
        static public string funZeroes(int wRepite)
        {
            StringBuilder result = new StringBuilder();
            result.Append(String.Empty);
            for (int intVIndice = 1; intVIndice <= wRepite; intVIndice++)
            {
                result.Append("0");
            }
            return result.ToString();
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para rellenar la cadena con espacios hasta la longitud dada
        //*******************************************************************************
        static public string funPonEspacios(ref  string strRecive, int intLonCamp)
        {
            strRecive = strRecive.Trim();
            int intLonReg = strRecive.Length;
            intLonReg = intLonCamp - intLonReg;
            if (intLonReg > 0)
            {
                for (int intCuenta = 0; intCuenta <= intLonReg - 1; intCuenta++)
                {
                    strRecive = strRecive + " ";
                }
            }
            return strRecive;
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para desplegar mensajes de error
        //*******************************************************************************
        static public void subDespErrores(ref  string strError, ref  MsgBoxStyle vbdtTipoDesp, ref  string strCaption)
        {
            if (!mdlCatalogos.gblnModal)
            {
                if (!gblnEstaSeg)
                {
                    strError = strError + "\n" + "\r" + "\n" + "\r" + "REQUIERE FIRMARSE A SEGURIDAD CORPORATIVA";
                }
                if (strCaption.Trim() == "")
                {
                    strCaption = "S753 ARIES - CCyBE";
                }
                //UPGRADE_WARNING: (1046) MsgBox Parameter 'context' is not supported, and was removed.
                //UPGRADE_WARNING: (1046) MsgBox Parameter 'helpfile' is not supported, and was removed.
                Interaction.MsgBox(strError, vbdtTipoDesp, strCaption);
                if (!gblnEstaSeg && mdlCatalogos.gBolHayCatalogos)
                {
                    gblnEstaSeg = MDIMasivos.DefInstance.OleAcceso.FirmarS041();
                }
            }
            else
            {
                subDespMsg(ref strError);
            }
        }

        static public void subDespErrores(ref  string strError, ref  MsgBoxStyle vbdtTipoDesp)
        {
            string tempRefParam = "";
            subDespErrores(ref strError, ref vbdtTipoDesp, ref tempRefParam);
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para desplegar mensajes informativos
        //*******************************************************************************
        static public void subDespMsg(ref  string strMsg)
        {
            if (!gblnEstaSeg)
            {
                if (mdlCatalogos.gBolHayCatalogos)
                {
                    gblnEstaSeg = MDIMasivos.DefInstance.OleAcceso.FirmarS041();
                }
            }
            subDespMensajes(strMsg);
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para desplegar mensajes en la barra de estado
        //*******************************************************************************
        static public void subDespMensajes(string strMensaje)
        {
            //AIS-1899 FSABORIO
            MDIMasivos.DefInstance.pnlEstado.Items[0].Text = strMensaje;
        }


        //*******************************************************************************
        //* Finalidad:  Subrutina para rellenar la cadena con ceros hasta la longitud dada
        //*******************************************************************************
        static public string funPoneCeros(string vRecive, int vLonCamp)
        {
            vRecive = vRecive.Trim();
            int intLonRec = vRecive.Length;
            intLonRec = vLonCamp - intLonRec;
            if (intLonRec > 0)
            {
                for (int intCuenta = 0; intCuenta <= intLonRec - 1; intCuenta++)
                {
                    vRecive = "0" + vRecive;
                }
            }
            return vRecive;
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para seleccionar el contenido de los TextBox
        //*******************************************************************************
        static public void subSelTexto(TextBox cMsk)
        {
            cMsk.SelectionStart = 0;
            cMsk.SelectionLength = cMsk.MaxLength;
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para validar captura de numeros unicamente
        //*******************************************************************************
        static public void subFiltraCaptura(ref  int KeyAscii, ref  string Filtro, ref  int Incluye)
        {
            if (KeyAscii > 0)
            {
                if (Incluye != 0)
                {
                    Incluye = (((Filtro + Strings.Chr(8).ToString() + Strings.Chr(mdlComunica.gbCTRLC).ToString() + Strings.Chr(mdlComunica.gbCTRLV).ToString() + Strings.Chr(mdlComunica.gbCTRLX).ToString() +
                              Strings.Chr(mdlComunica.gbCTRLZ).ToString()).IndexOf(Strings.Chr(KeyAscii).ToString()) + 1) == 0) ? -1 : 0;
                }
                else
                {
                    Incluye = (Filtro.IndexOf(Strings.Chr(KeyAscii).ToString()) >= 0) ? -1 : 0;
                }
                if (Incluye != 0)
                {
                    Interaction.Beep();
                    KeyAscii = 0;
                }
            }
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para mostrar los menús y habilitar los botones de la toolbar
        //*******************************************************************************
        static public void subMuestraMenu()
        {
            object tempRefParam = 1;
            MDIMasivos.DefInstance.tlbMasivos.Buttons.get_ControlDefault(ref tempRefParam).Enabled = true;
            object tempRefParam2 = 2;
            MDIMasivos.DefInstance.tlbMasivos.Buttons.get_ControlDefault(ref tempRefParam2).Enabled = true;
            object tempRefParam3 = 5;
            MDIMasivos.DefInstance.tlbMasivos.Buttons.get_ControlDefault(ref tempRefParam3).Enabled = true;
            object tempRefParam4 = 6;
            MDIMasivos.DefInstance.tlbMasivos.Buttons.get_ControlDefault(ref tempRefParam4).Enabled = true;
            object tempRefParam5 = 10;
            MDIMasivos.DefInstance.tlbMasivos.Buttons.get_ControlDefault(ref tempRefParam5).Enabled = true;
            object tempRefParam6 = 14;
            MDIMasivos.DefInstance.tlbMasivos.Buttons.get_ControlDefault(ref tempRefParam6).Enabled = true;

            // BOTONES LISTA NEGRAS
            object tempRefParam7 = 15;
            MDIMasivos.DefInstance.tlbMasivos.Buttons.get_ControlDefault(ref tempRefParam7).Enabled = true;
            object tempRefParam8 = 16;
            MDIMasivos.DefInstance.tlbMasivos.Buttons.get_ControlDefault(ref tempRefParam8).Enabled = true;
            object tempRefParam9 = 17;
            MDIMasivos.DefInstance.tlbMasivos.Buttons.get_ControlDefault(ref tempRefParam9).Enabled = true;

            subOcultaMenu(true);
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para deshabilitar los botones de la toolbar
        //*******************************************************************************
        static public void subOcultaBotones()
        {
            object tempRefParam = 1;
            MDIMasivos.DefInstance.tlbMasivos.Buttons.get_ControlDefault(ref tempRefParam).Enabled = false;
            object tempRefParam2 = 2;
            MDIMasivos.DefInstance.tlbMasivos.Buttons.get_ControlDefault(ref tempRefParam2).Enabled = false;
            object tempRefParam3 = 5;
            MDIMasivos.DefInstance.tlbMasivos.Buttons.get_ControlDefault(ref tempRefParam3).Enabled = false;
            object tempRefParam4 = 6;
            MDIMasivos.DefInstance.tlbMasivos.Buttons.get_ControlDefault(ref tempRefParam4).Enabled = false;
            object tempRefParam5 = 14;
            MDIMasivos.DefInstance.tlbMasivos.Buttons.get_ControlDefault(ref tempRefParam5).Enabled = false;

            // BOTONES LISTA NEGRAS
            object tempRefParam6 = 15;
            MDIMasivos.DefInstance.tlbMasivos.Buttons.get_ControlDefault(ref tempRefParam6).Enabled = false;
            object tempRefParam7 = 16;
            MDIMasivos.DefInstance.tlbMasivos.Buttons.get_ControlDefault(ref tempRefParam7).Enabled = false;
            object tempRefParam8 = 17;
            MDIMasivos.DefInstance.tlbMasivos.Buttons.get_ControlDefault(ref tempRefParam8).Enabled = false;
        }

        //*****************************************************************************************************
        //* Finalidad:  Subrutina para ocultar/mostrar los menues de la aplicación
        //*****************************************************************************************************
        static public void subOcultaMenu(bool blnValor)
        {
            MDIMasivos.DefInstance.mnuProcesamientoMasivo.Available = blnValor;
            MDIMasivos.DefInstance.mnuPredictaminacion.Available = blnValor;
            MDIMasivos.DefInstance.mnuFirmaS041.Available = blnValor;
        }

        //*****************************************************************************************************
        //* Finalidad:  Subrutina para habilitar/deshabilitar los controles cuyo Tag sea strTag de la pantalla frmForma
        //*****************************************************************************************************
        static public void InhibeControles(frmProcMasivo frmForma, bool blnEnabled, string strTag)
        {
            //UPGRADE_TODO: (1069) Error handling statement (On Error Resume Next) was converted to a complex pattern which might not be equivalent to the original.
            try
            {
                //UPGRADE_WARNING: (2065) Form property frmForma.Controls has a new behavior.
                foreach (Control ctrGeneral in ContainerHelper.Controls(frmForma))
                {
                    if (strTag.Trim() == "")
                    {
                        ControlHelper.SetEnabled(ctrGeneral, blnEnabled);
                    }
                    else
                    {
                        if (strTag.Trim() == ControlHelper.GetTag(ctrGeneral).ToString().Trim())
                        {
                            ControlHelper.SetEnabled(ctrGeneral, blnEnabled);
                        }
                    }
                    if (ctrGeneral is ToolStripMenuItem)
                    {
                        ctrGeneral.Enabled = blnEnabled;
                    }
                }
                MDIMasivos.DefInstance.mnuFirmaS041.Enabled = blnEnabled;
                object tempRefParam = 10;
                MDIMasivos.DefInstance.tlbMasivos.Buttons.get_ControlDefault(ref tempRefParam).Enabled = blnEnabled;
            }
            catch (Exception exc)
            {
                throw new Exception("Migration Exception: The following exception could be handled in a different way after the conversion: " + exc.Message);
            }
        }

        static public void InhibeControles(frmProcMasivo frmForma, bool blnEnabled)
        {
            InhibeControles(frmForma, blnEnabled, "");
        }

        static public void subValidaRuta(string strRuta)
        {
            //UPGRADE_TODO: (1069) Error handling statement (On Error Resume Next) was converted to a complex pattern which might not be equivalent to the original.
            try
            {
                if (FileSystem.Dir(strRuta + "\\", FileAttribute.Normal) == "")
                {
                    Directory.CreateDirectory(strRuta);
                }
            }
            catch (Exception exc)
            {
                throw new Exception("Migration Exception: The following exception could be handled in a different way after the conversion: " + exc.Message);
            }
        }

        //MIG WXP INI JGC 20090825
        static public bool funCreaMasivosIni()
        {
        //    bool result = false;
        //    int intResultado = 0;
        //    string strRuta = String.Empty;
        //    try
        //    {
        //        if (FileSystem.Dir(gcStrMasivosIni, FileAttribute.Normal) != "")
        //        { //Eliminarlo
        //            File.Delete(gcStrMasivosIni);
        //        }
        //        //Validar la creación de los subdirectorios que se almacenan en las rutas
        //        //AIS-1893 FSABORIO
        //        strRuta = mdlMain.ApplicationPath + "\\Errores";
        //        subValidaRuta(strRuta);
        //        intResultado = funSetParam(gcStrMasivosIni, "RUTA_ERROR", "RUTA", strRuta);
        //        //AIS-1893 FSABORIO
        //        strRuta = mdlMain.ApplicationPath + "\\Bitacora";
        //        subValidaRuta(strRuta);
        //        intResultado = funSetParam(gcStrMasivosIni, "ENLACE_FTP", "RUTA", strRuta);
        //        intResultado = funSetParam(gcStrMasivosIni, "ENLACE_FTP", "IP1", "10.221.166.2");
        //        intResultado = funSetParam(gcStrMasivosIni, "ENLACE_FTP", "PUERTO1", "10121");
        //        intResultado = funSetParam(gcStrMasivosIni, "ENLACE_FTP", "IP2", "10.221.94.90");
        //        intResultado = funSetParam(gcStrMasivosIni, "ENLACE_FTP", "PUERTO2", "10121");
        //        intResultado = funSetParam(gcStrMasivosIni, "ENLACE_FTP", "IP3", "10.221.30.225");
        //        intResultado = funSetParam(gcStrMasivosIni, "ENLACE_FTP", "PUERTO3", "10021");
        //        intResultado = funSetParam(gcStrMasivosIni, "ENLACE_FTP", "USUARIO", "¦ÏtHåì©");
        //        intResultado = funSetParam(gcStrMasivosIni, "ENLACE_FTP", "PWD", "¦ÏtHåì©");
        //        intResultado = funSetParam(gcStrMasivosIni, "ENLACE_FTP", "FTP", "FTP -s:");
        //        intResultado = funSetParam(gcStrMasivosIni, "ENLACE_FTP", "FILECMD", "CmdEnvioFtp.txt");
        //        intResultado = funSetParam(gcStrMasivosIni, "ENLACE_FTP", "FILEBATCH", "CmdEnvioFtp.bat");
        //        intResultado = funSetParam(gcStrMasivosIni, "ENLACE_FTP", "FILELOG", "CmdEnvioFtp.log");
        //        intResultado = funSetParam(gcStrMasivosIni, "ENLACE_FTPVC", "RUTA", strRuta);
        //        intResultado = funSetParam(gcStrMasivosIni, "ENLACE_FTPVC", "IP1", "10.221.166.2");
        //        intResultado = funSetParam(gcStrMasivosIni, "ENLACE_FTPVC", "PUERTO", "10121");
        //        intResultado = funSetParam(gcStrMasivosIni, "ENLACE_FTPVC", "IP2", "10.221.166.2");
        //        intResultado = funSetParam(gcStrMasivosIni, "ENLACE_FTPVC", "PUERTO", "10121");
        //        intResultado = funSetParam(gcStrMasivosIni, "ENLACE_FTPVC", "IP3", "10.221.166.2");
        //        intResultado = funSetParam(gcStrMasivosIni, "ENLACE_FTPVC", "PUERTO", "10121");
        //        intResultado = funSetParam(gcStrMasivosIni, "ENLACE_FTPVC", "USUARIO", "¦ÏtHåì©");
        //        intResultado = funSetParam(gcStrMasivosIni, "ENLACE_FTPVC", "PWD", "¦ÏtHåì©");
        //        intResultado = funSetParam(gcStrMasivosIni, "ENLACE_FTPVC", "FTP", "FTP -s:");
        //        intResultado = funSetParam(gcStrMasivosIni, "ENLACE_FTPVC", "FILECMD", "CmdEnvioFtp.txt");
        //        intResultado = funSetParam(gcStrMasivosIni, "ENLACE_FTPVC", "FILEBATCH", "CmdEnvioFtp.bat");
        //        intResultado = funSetParam(gcStrMasivosIni, "ENLACE_FTPVC", "FILELOG", "CmdEnvioFtp.log");
        //        intResultado = funSetParam(gcStrMasivosIni, "RUTA_TANDEM", "RUTA", "$DATA90 .S753FILE");

        //        //(JB-SAS DIC/2006) NUEVO, UTILIZANDO ICEP
        //        intResultado = funSetParam(gcStrMasivosIni, "INTELAR", "COMANDO_ICEP", "C:\\APPS\\C617\\402\\ICEP\\ICEPWIN.EXE");
        //        intResultado = funSetParam(gcStrMasivosIni, "INTELAR", "EQUIPO", "intelarintmx1");
        //        intResultado = funSetParam(gcStrMasivosIni, "INTELAR", "USUARIO", "gwin");
        //        intResultado = funSetParam(gcStrMasivosIni, "INTELAR", "BUZON", "s753s01e");
        //        intResultado = funSetParam(gcStrMasivosIni, "INTELAR", "MODO", "2");
        //        intResultado = funSetParam(gcStrMasivosIni, "INTELAR", "RUTA", "C:\\APPS\\C617\\402\\ICEP");

                return true;
        //    }
        //    catch (Exception excep)
        //    {

        //        result = false;
        //        //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
        //        string tempRefParam = "IMPOSIBLE REGENERAR EL ARCHIVO MASIVOS.INI, CAUSA: " + Information.Err().Number.ToString() + " - " + excep.Message;
        //        MsgBoxStyle tempRefParam2 = (MsgBoxStyle)(((int)MsgBoxStyle.Critical) + ((int)MsgBoxStyle.OkOnly));
        //        subDespErrores(ref tempRefParam, ref tempRefParam2);
        //        return result;
        //    }
        }
        //MIG WXP FIN JGC 20090825

        //MIG WXP INi JGC 20090825
        //static public string funGetParam(ref  string strArchivoIni, ref  string strSeccion, ref  string strVariable, bool blnCreaMasivos)
        //{
        //    string result = String.Empty;
        //    string strTexto = String.Empty, strMensaje = String.Empty;
        //    int IntNumChar = 0;
        //    try
        //    {
        //        result = "unknown";
        //        strTexto = new string((char)0, 255);
        //        IntNumChar = API.KERNEL.GetPrivateProfileString(strSeccion, strVariable, "", ref strTexto, 120, strArchivoIni);
        //        if (IntNumChar == 0 && !blnCreaMasivos)
        //        {
        //            //Si no se encontró parámetro, hay que crear de nuevo el archivo INI
        //            if (!funCreaMasivosIni())
        //            {
        //                //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
        //                string tempRefParam = "OCURRIÓ UN ERROR CON EL ARCHIVO MASIVOS.INI, NO SE PUDO ENCONTRAR EL CAMPO '" + strVariable + "' EN LA SECCIÓN '" + strSeccion + "'";
        //                MsgBoxStyle tempRefParam2 = (MsgBoxStyle)(((int)MsgBoxStyle.Critical) + ((int)MsgBoxStyle.OkOnly));
        //                subDespErrores(ref tempRefParam, ref tempRefParam2);
        //            }
        //            else
        //            {
        //                result = funGetParam(ref strArchivoIni, ref strSeccion, ref strVariable, true);
        //            }
        //        }
        //        else
        //        {
        //            result = strTexto.Substring(0, Math.Min(strTexto.Length, IntNumChar));
        //        }
        //        return result;
        //    }
        //    catch
        //    {

        //        //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
        //        string tempRefParam3 = "OCURRIÓ UN ERROR CON EL ARCHIVO MASIVOS.INI, NO SE PUDO ENCONTRAR EL CAMPO '" + strVariable + "' EN LA SECCIÓN '" + strSeccion + "'";
        //        MsgBoxStyle tempRefParam4 = (MsgBoxStyle)(((int)MsgBoxStyle.Critical) + ((int)MsgBoxStyle.OkOnly));
        //        subDespErrores(ref tempRefParam3, ref tempRefParam4);
        //        return result;
        //    }
        //}
        //MIG WXP FIN JGC 20090825

        //static public string funGetParam(ref  string strArchivoIni, ref  string strSeccion, ref  string strVariable)
        //{
        //    return funGetParam(ref strArchivoIni, ref strSeccion, ref strVariable, false);
        //}

        //MIG WXP INI JGC 20090825
        static public string funGetParam()
        {
            return "1";
        }
        //MIG WXP FIN JGC 20090825
        static public int funSetParam(string strArchivoIni, string strSeccion, string strVariable, string strValor)
        {
            try
            {
                return WritePrivateProfileString(strSeccion, strVariable, strValor, strArchivoIni);
            }
            catch (Exception excep)
            {

                //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                string tempRefParam = ("ERROR MIENTRAS INTENTABA AGREGAR UN CAMPO A MASIVOS.INI - " + Information.Err().Number.ToString() + ": " + excep.Message).ToUpper();
                MsgBoxStyle tempRefParam2 = (MsgBoxStyle)(((int)MsgBoxStyle.Critical) + ((int)MsgBoxStyle.OkOnly));
                subDespErrores(ref tempRefParam, ref tempRefParam2);
                return 0;
            }
            return 0;
        }

        //Función para cambiar el caracter definido por strCaracterIni a strCaracterFin
        static public string funCambiaCaracter(string strCadena, string strCaracterIni, string strCaracterFin)
        {
            for (int intContador = 1; intContador <= strCadena.Length; intContador++)
            {
                if (Strings.Mid(strCadena, intContador, 1) == strCaracterIni)
                {
                    strCadena = StringsHelper.MidAssignment(strCadena, intContador, strCaracterFin);
                }
            }
            return strCadena;
        }

        static public string Cadenas_Concatena_Blancos_O_Ceros(string Cad, int BlanCer, int IzqDer, int longi)
        {
            //Esta funcion regresa una cadena a la que se le concatenaron blancos o ceros a la izquierda o
            //a la derecha hasta que alcance determinada longitud.
            //Cad - Es la cadena a la que se desea concatenar blancos o ceros.
            //BlanCer - Indicador del caracter a concatenar. Si Blancer es 0 entonces concatena ceros, si es
            //          1 concatena blancos.
            //IzqDer - Indicador de donde se desea la concatenacion. Si IzqDer es 0 se concatena a la izquierda,
            //         si es 1 se concatena a la derecha.
            //Longi - Es la longitud que se desea alcanzar despues de la concatenacion.
            //
            StringBuilder aux = new StringBuilder();
            aux.Append(String.Empty);
            string CarCon = String.Empty;

            try
            {

                aux = new StringBuilder(Cad);
                if (aux.ToString().Length < longi)
                {
                    switch (BlanCer)
                    {
                        case 0:
                            CarCon = "0";
                            break;
                        case 1:
                            CarCon = " ";
                            break;
                    }
                    for (int i = 1; i <= longi - aux.ToString().Length; i++)
                    {
                        switch (IzqDer)
                        {
                            case 0:
                                aux = new StringBuilder(CarCon + aux.ToString());
                                break;
                            case 1:
                                aux.Append(CarCon);
                                break;
                        }
                    }
                }

                return aux.ToString();
            }
            catch
            {


                MessageBox.Show("EN CADENAS_CONCATENA_BLANCOS_O_CEROS: " + Microsoft.VisualBasic.Conversion.ErrorToString() + ". ", "NEXUS (Cadenas).", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return String.Empty;
            }
            return String.Empty;
        }


        static public bool funEstaEnviando(int x)
        {


            if (GetModuleUsage((short)x) > 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        //*** INI - IRP – Proy. 66008-06//irp
        //*****************************************************************************************************
        //* Propósito:  da formato a la cadena
        //* Entradas: cadena -> cadena a dar formato, pos -> longitud de cadena,
        //            chars -> caracter para rellenar
        //****************************************************************************************************
        static public string funFormatStr(string cadena, int pos, string chars)
        {

            StringBuilder result = new StringBuilder();
            result.Append(String.Empty);

            string strCad = cadena.Trim();
            int intposact = strCad.Length;
            int intposadd = pos - intposact;
            for (int intX = 1; intX <= intposadd; intX++)
            {
                result.Append(chars);
            }
            if (chars == "0")
            {
                return result.ToString() + strCad;
            }
            else
            {
                return strCad + result.ToString();
            }

            return result.ToString();
        }
        //*** FIN - IRP – Proy. 66008-06//irp

        //*** INI - IRP – Proy. 66008-06//irp
        static public void subGetCatDesc(ref string  strExiste, ref  string strCatalogo, ref  string strLlave)
        {
            subGetCatDesc(ref strExiste , ref strCatalogo, ref strLlave, false);
        }
        //*******************************************************************************
        //* Finalidad:   Rutina para obtener descripciones de los catalogos
        //* Entradas :   Nombre de la forma
        //********************************************************************************
        static public void subGetCatDesc(ref string  strDescripcion, ref  string strCatalogo, ref  string strLlave, bool blnEnAtrib)
        {
            double intTemp = -1;
            if (blnEnAtrib)
            {
                intTemp = 5; 
            }
            else
            {
                strLlave = funFormatStr(strLlave, 8, "0");
            }
           
            string tempRefParam = null;
            string tempRefParam2 = null;
            string tempRefParam3 = null;
            Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam4 = (Catalogos.clsCatalogos.enmCamposCatalogos)StringsHelper.IntValue(Conversion.Str(intTemp));
            string tempRefParam5 = "D";
            if (gCatalogos.BuscaCatalogo(ref strCatalogo, strLlave, tempRefParam, tempRefParam2, ref tempRefParam3, ref tempRefParam4, ref tempRefParam5))
            {
                strDescripcion = gCatalogos.getDescripcion.Trim();
            }
            else
            {
                strDescripcion = "";
            }

        }
        //*** FIN - IRP – Proy. 66008-06//irp
        //*** INI - IRP – Proy. 66008-06//irp
        static private Catalogos.clsCatalogos _gCatalogos = null;
        static public Catalogos.clsCatalogos gCatalogos
        {
            get
            {
                if (_gCatalogos == null)
                {
                    _gCatalogos = new Catalogos.clsCatalogos();
                    //AIS-1638 ogarcia
                    _gCatalogos.setupCatalogoConCodigos();
                }
                return _gCatalogos;
            }
            set
            {
                _gCatalogos = value;
            }
        }
        //*** FIN - IRP – Proy. 66008-06//irp



    }
}