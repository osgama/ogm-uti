using Artinsoft.VB6.Utils;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility.VB6;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using VB6 = Microsoft.VisualBasic.Compatibility.VB6.Support;

namespace Masivos
{
    public class mdlComunica
    {


        //Valores de teclas y caracteres usados a lo largo del programa.
        public const byte gbFS = 28;
        public const byte gbESC = 27;
        public const byte gbHOME = 30;
        public const byte gbCR = 10;
        public const byte gbLF = 13;
        public const byte gbEOT = 3;
        public const byte gbNULO = 0;
        public const byte gbTAB = 9;
        //Validación de digitos numéricos
        public const string DIGITOS = "1234567890";
        //Validación de solo letras
        public const string DIGINVAL = "1234567890/";
        public const string LETRAS = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ# ";
        public const byte gbCTRLC = 3; // Hot Key para realizar copiado en Grid.
        public const byte gbCTRLV = 22; // Hot key para pegar en Grid.
        public const byte gbCTRLX = 24;
        public const byte gbCTRLZ = 26;
        //Funciones de interface con la librería que compacta y descompacta Usr y Pwd, algoritmo 3DES
        [DllImport("des32.dll")]
        public extern static short Inicia_Encripcion(string Cadena, short version);
        [DllImport("des32.dll")]
        public extern static short E3Des(string Cad_en_claro, string Cad_encrip, short version);
        [DllImport("des32.dll")]
        public extern static short D3Des(string Cad_encrip, string Cad_en_claro);
        //Funciones para calclular el digito verificador en DLL
        [DllImport("DllDigver.dll")]
        public extern static short Digver_AdiTC(string strCuenta);
        [DllImport("DllDigver.dll")]
        public extern static short Digver_AdiChe(string strCuenta);
        [DllImport("DllDigver.dll")]
        public extern static short Digver_AdiCheCiti(string strCuenta);
        //Variables de comdriver 16 'LAM:/PRAXIS
        static public COMDRV32.TcpServer objProxy = null;
        static public string gvMensaje = String.Empty;
        static public string gvRecive = String.Empty;
        static private Catalogos.clsCatalogos _OleCatalogos = null;
        static public Catalogos.clsCatalogos OleCatalogos
        {
            get
            {
                if (_OleCatalogos == null)
                {
                    _OleCatalogos = new Catalogos.clsCatalogos();
                }
                return _OleCatalogos;
            }
            set
            {
                _OleCatalogos = value;
            }
        }

        static private Catalogos.clsCatalogos _OleCatalogosPromo = null;
        static public Catalogos.clsCatalogos OleCatalogosPromo
        {
            get
            {
                if (_OleCatalogosPromo == null)
                {
                    _OleCatalogosPromo = new Catalogos.clsCatalogos();
                }
                return _OleCatalogosPromo;
            }
            set
            {
                _OleCatalogosPromo = value;
            }
        }

        static private string strCadPaso = String.Empty;
        static public string strQueTramite = String.Empty;
        static public string strCadena = String.Empty, strCadError = String.Empty;
        static public FixedLengthString strCveCausa = new FixedLengthString(4);

        //*******************************************************************************
        //* Finalidad:  Subrutina para depurar el mensaje de respuesta de TANDEM
        //*******************************************************************************
        static public string funDepuraMensaje(string vRecive)
        {
            for (int intVIndice = 1; intVIndice <= vRecive.Length; intVIndice++)
            {
                if (Strings.Mid(vRecive, intVIndice, 1) == Strings.Chr(gbFS).ToString() || Strings.Mid(vRecive, intVIndice, 1) == Strings.Chr(gbEOT).ToString() || Strings.Mid(vRecive, intVIndice, 1) == Strings.Chr(gbLF).ToString() || Strings.Mid(vRecive, intVIndice, 1) == Strings.Chr(gbTAB).ToString() || Strings.Mid(vRecive, intVIndice, 1) == Strings.Chr(gbNULO).ToString() || Strings.Mid(vRecive, intVIndice, 1) == Strings.Chr(gbESC).ToString() || Strings.Mid(vRecive, intVIndice, 1) == Strings.Chr(gbHOME).ToString() || Strings.Mid(vRecive, intVIndice, 1) == Strings.Chr(gbCR).ToString())
                {
                    //Se tuvo que cambiar para evitar los primeros caracteres y substituir los intermedio por espacios, en lugar de quitarlos.
                    if (intVIndice > 5)
                    {
                        vRecive = Strings.Mid(vRecive, 1, intVIndice - 1) + " " + Strings.Mid(vRecive, intVIndice + 1);
                    }
                    else
                    {
                        vRecive = Strings.Mid(vRecive, 1, intVIndice - 1) + Strings.Mid(vRecive, intVIndice + 1);
                        intVIndice--;
                    }
                }
            }
            if (vRecive.IndexOf("SEG:") >= 0)
            {
                mdlGlobales.gblnEstaSeg = true;
            }
            else if (vRecive.IndexOf("SEG;") >= 0)
            {
                mdlGlobales.gblnEstaSeg = false;
            }
            gvRecive = vRecive;
            return vRecive;
       }

        //*******************************************************************************
        //* Finalidad:  Subrutina principal
        //*******************************************************************************
        static public void Main_Renamed()
        {
            Process[] Procesos = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
            if (Procesos.Length > 1 && Process.GetCurrentProcess().StartTime != Procesos[0].StartTime)
            { //Checa si ya se está ejecutando el programa,
                Environment.Exit(0); //para no permitir mas de una sola instancia del mismo.
            }
            else
            {
                mdlCatalogos.gBolHayCatalogos = false;
                mdlGlobales.gLngArchivoBitacora = FileSystem.FreeFile();
                mdlGlobales.ENCRIP.KEY = "[AÌ&ÿ58B5-Ì&Ì >5?@Ì&-:Ìó;:Ì&F-81?ÌÌ&EÌí>@AÌ&>;Ìø1EÌ&B-Ìû85Ì$B1>-Z]";
                mdlGlobales.ENCRIP.Corrimiento = 10;
                mdlGlobales.subAbreBitacora();
                mdlGlobales.subObtenCaracteresInvalidos(); //Rutina para filtrar los caracteres inválidos del archivo de promotora
                objProxy = new COMDRV32.TcpServer();
                objProxy.Connect();
                mdlGlobales.gblnFinaliza = true;
                mdlGlobales.gblnEstaSeg = false;
                //        Set MDIMasivos.OleAcceso.Conexion = objProxy
                //        gblnEstaSeg = MDIMasivos.OleAcceso.FirmarS041
                mdlGlobales.gblnEstaSeg = true;
                MDIMasivos.DefInstance.Show();
                if (!mdlGlobales.gblnEstaSeg)
                {
                    objProxy.Disconnect();
                    Environment.Exit(0);
                }
                mdlGlobales.gstrNomina.Value = MDIMasivos.DefInstance.OleAcceso.Nomina;
            }
        }

        //*******************************************************************************
        //* Finalidad:  Función para enviar y recibir las transacciones a TANDEM
        //*******************************************************************************
        static public string funCON(string strMensaje, bool NoDepuraRespuesta)
        {

            int intTM = 0;
            string s = String.Empty;
            bool bContinue = false;
            bool tmpContinue = false;
            try
            {
                strCadPaso = "D" + strMensaje + Strings.Chr(3).ToString();
                //AIS-Bug 8428 FSABORIO
                objProxy.SendRequest(StringsHelper.StrConv4(strCadPaso, StringsHelper.VbStrConvEnum.vbFromUnicode, 0), (short)strCadPaso.Length);
                intTM = 0;
                s = "";
                bContinue = true;

                while (bContinue)
                {

                    do
                    {
                        try
                        {
                            //AIS-1706 laralar: Char3 added
                            //AIS-Bug 8428 FSABORIO
                            s = s + StringsHelper.StrConv4(objProxy.ReceiveResponse(90000), StringsHelper.VbStrConvEnum.vbUnicode, 0) + Strings.Chr(3).ToString();
                           
                            tmpContinue = false;
                        }
                        catch
                        {
                            manejaError(ref tmpContinue);
                        }
                    } while (tmpContinue);

                    if (s.Length > 13)
                    {
                        if (s.IndexOf("H10013CMDD") >= 0)
                        { //Evitamos el mensaje para el ADMWIN
                            s = Strings.Mid(s, (s.IndexOf("H10013CMDD") + 1) + 13);
                        }
                        //Checa si al reposicionarnos hay cadena válida
                        if (Strings.Mid(s, 1, 2) == "H1" && Strings.Mid(s, 7, 4) == "CMDD")
                        {
                            if (intTM == 0)
                            {
                                intTM = StringsHelper.IntValue(Strings.Mid(s, 3, 4)); //Toma la longitud de la cadena
                            }
                            if (s.Length >= intTM)
                            {
                                //Para sincronizar el mensaje (se asegura de que sea el que pidió).
                                if (Strings.Mid(s, 11, 4) == "5420" || Strings.Mid(s, 11, 4) == "5560" || Strings.Mid(s, 11, 4) == "5561" || Strings.Mid(s, 11, 4) == "5562" || Strings.Mid(s, 11, 4) == "5595")
                                {
                                    if (Strings.Mid(strMensaje, 1, 4) == Strings.Mid(s, 11, 4))
                                    {
                                        bContinue = false;
                                    }
                                }
                                else
                                {
                                    bContinue = false;
                                }
                            }
                        }
                    }
                }
                if (s.IndexOf("SEG;") >= 0 || s.IndexOf("SEG:") >= 0)
                {
                    gvRecive = Strings.Mid(s, 11);
                }
                else
                {
                    gvRecive = Strings.Mid(funDepuraMensaje(s), 11);
                }
                mdlGlobales.subRegBitacora("R");
                return gvRecive;
            }
            catch
            {
            }
            return String.Empty;
        }
        private static void manejaError(ref bool tmpContinue)
        {
            objProxy = null;
            objProxy = new COMDRV32.TcpServer();
            objProxy.Connect();
            objProxy.SendRequest(StringsHelper.StrConv4(strCadPaso, StringsHelper.VbStrConvEnum.vbFromUnicode, 0), (short)strCadPaso.Length);
            tmpContinue = true;
        }
        static public string funCON(string strMensaje)
        {
            return funCON(strMensaje, false);
        }
    }
}