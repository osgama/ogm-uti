using Artinsoft.VB6.Utils;
using Microsoft.VisualBasic;
using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.Compatibility.VB6;
using VB6 = Microsoft.VisualBasic.Compatibility.VB6.Support;

namespace Masivos
{
    class mdlTranMasivo
    {

        //*******************************************************************************
        //* Identificación: mdlTranMasivo                                               *
        //* Autor:          Luis E. Aguilar; Abel Polo                                  *
        //* Modificaciones: Israel Garces; Alvaro Salinas                               *
        //* Instalación:    PRAXIS                                                      *
        //* Fecha:          1/09/2003                                                   *
        //* Versión:        1.0                                                         *
        //*******************************************************************************

        //*******************************************************************************
        //* Estructura de Arreglos que se utiliza en la Forma frmConsTodaRemesa
        //*******************************************************************************
        public struct udtConsRemesa
        {
            public string strFolioPreImpr;
            public string strNomSolicita;
            public string strCveEstatus;
            public string strEstatus;
            public string strCveProceso;
            public string strProceso;
            public string strCveSigProceso;
            public string strSigProceso;
            public string strCveCausaRecha;
            public string strCausaRecha;
            public static udtConsRemesa CreateInstance()
            {
                udtConsRemesa result = new udtConsRemesa();
                result.strFolioPreImpr = String.Empty;
                result.strNomSolicita = String.Empty;
                result.strCveEstatus = String.Empty;
                result.strEstatus = String.Empty;
                result.strCveProceso = String.Empty;
                result.strProceso = String.Empty;
                result.strCveSigProceso = String.Empty;
                result.strSigProceso = String.Empty;
                result.strCveCausaRecha = String.Empty;
                result.strCausaRecha = String.Empty;
                return result;
            }
        }
        static public udtConsRemesa[] estConsRemesa;// = ArraysHelper.InitializeArray<udtConsRemesa[]>(new int[]{});

        //*******************************************************************************
        //* Estructura para la Funcion funArmaHeader5562
        //*******************************************************************************
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct udtHeader5562
        {
            public FixedLengthString strHDCveTran;
            public FixedLengthString strHDFiller01;
            public FixedLengthString strHDSubTran;
            public FixedLengthString strHDFolPreimpreso;
            public FixedLengthString strHDFolInterno;
            public FixedLengthString strHDSistOrigen;
            public FixedLengthString strHDTramite;
            public FixedLengthString strHDEntOrig;
            public FixedLengthString strHDGpoEntOrig;
            public FixedLengthString strHDCveEntOrig;
            public FixedLengthString strHDEstatus; //MMS 11/05 Incremento en la longitud del campo (2 a 3)
            public FixedLengthString strHDCveResp;
            public FixedLengthString strHDDescResp;
            public FixedLengthString strHDNominaOper;
            public FixedLengthString strHDCvePaqEval;
            public FixedLengthString strHDCveProceso; //MMS 11/05 Incremento en la longitud del campo (2 a 3)
            public FixedLengthString strHDFlagInfo;
            public FixedLengthString strHDCveRechazo;
            public FixedLengthString strHDPantalla;
            public FixedLengthString strHDNumMapa; //MMS 11/05 Incremento en la longitud del campo (2 a 3)
            public FixedLengthString strHDProcIni; //MMS 11/05 Incremento en la longitud del campo (2 a 3)
            public FixedLengthString strHDFiller02; //MMS 11/05 Reducción del filler

            public static udtHeader5562 CreateInstance()
            {
                udtHeader5562 result = new udtHeader5562();
                result.strHDCveTran = new FixedLengthString(4);
                result.strHDFiller01 = new FixedLengthString(1);
                result.strHDSubTran = new FixedLengthString(2);
                result.strHDFolPreimpreso = new FixedLengthString(16);
                result.strHDFolInterno = new FixedLengthString(8);
                result.strHDSistOrigen = new FixedLengthString(4);
                result.strHDTramite = new FixedLengthString(2);
                result.strHDEntOrig = new FixedLengthString(2);
                result.strHDGpoEntOrig = new FixedLengthString(4);
                result.strHDCveEntOrig = new FixedLengthString(4);
                result.strHDEstatus = new FixedLengthString(3); //MMS 11/05 Incremento en la longitud del campo (2 a 3)
                result.strHDCveResp = new FixedLengthString(2);
                result.strHDDescResp = new FixedLengthString(50);
                result.strHDNominaOper = new FixedLengthString(10);
                result.strHDCvePaqEval = new FixedLengthString(4);
                result.strHDCveProceso = new FixedLengthString(3); //MMS 11/05 Incremento en la longitud del campo (2 a 3)
                result.strHDFlagInfo = new FixedLengthString(1);
                result.strHDCveRechazo = new FixedLengthString(4);
                result.strHDPantalla = new FixedLengthString(8);
                result.strHDNumMapa = new FixedLengthString(3); //MMS 11/05 Incremento en la longitud del campo (2 a 3)
                result.strHDProcIni = new FixedLengthString(3); //MMS 11/05 Incremento en la longitud del campo (2 a 3)
                result.strHDFiller02 = new FixedLengthString(38); //MMS 11/05 Reducción del filler
                return result;
            }
        }
        static public udtHeader5562 estHeader5562 = udtHeader5562.CreateInstance();
        //*******************************************************************************
        //* Estructura para la Funcion funArmaPrioridad
        //*******************************************************************************
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct udtPrioridad
        {
            public FixedLengthString strPRNumRemesa;
            public FixedLengthString strPRPrioridad;

            public static udtPrioridad CreateInstance()
            {
                udtPrioridad result = new udtPrioridad();
                result.strPRNumRemesa = new FixedLengthString(10);
                result.strPRPrioridad = new FixedLengthString(2);
                return result;
            }
        }
        static public udtPrioridad estPrioridad = udtPrioridad.CreateInstance();

        //*******************************************************************************
        //* Estructura para la Funcion funArmaGeneraRemesa
        //*******************************************************************************
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct udtArmaGeneraRemesa
        {
            public FixedLengthString strGRSolicitud;
            public FixedLengthString strGRFecIngCredi;
            public FixedLengthString strGRFecAcepCredi;
            public FixedLengthString strGRCodPromoci;
            public FixedLengthString strGRFolPreImpIni;
            public FixedLengthString strGRFolPreImpFin;

            public static udtArmaGeneraRemesa CreateInstance()
            {
                udtArmaGeneraRemesa result = new udtArmaGeneraRemesa();
                result.strGRSolicitud = new FixedLengthString(5);
                result.strGRFecIngCredi = new FixedLengthString(8);
                result.strGRFecAcepCredi = new FixedLengthString(8);
                result.strGRCodPromoci = new FixedLengthString(4);
                result.strGRFolPreImpIni = new FixedLengthString(16);
                result.strGRFolPreImpFin = new FixedLengthString(16);
                return result;
            }
        }
        static public udtArmaGeneraRemesa estArmaGeneraRemesa = udtArmaGeneraRemesa.CreateInstance();

        //*******************************************************************************
        //* Estructura para la Guardar los Valores del Formulario de Proc. Masivo IGA/PRAXIS
        //*******************************************************************************
        public struct udtMasivo
        {
            public string strFolioPreimpreso;
            public string strNombreSolicitante;
            public string strCausaDeclinacion;
            public static udtMasivo CreateInstance()
            {
                udtMasivo result = new udtMasivo();
                result.strFolioPreimpreso = String.Empty;
                result.strNombreSolicitante = String.Empty;
                result.strCausaDeclinacion = String.Empty;
                return result;
            }
        }
        static public udtMasivo[] estMasivo = ArraysHelper.InitializeArray<udtMasivo[]>(new int[] { 1 });

        //*******************************************************************************
        //* Respuestas de Consultas
        //*******************************************************************************
        static public string gvRecive5562_01 = String.Empty;
        static public string gvRecive5562_02 = String.Empty;
        static public string gvRecive5562_03 = String.Empty;
        static public string gvRecive5562_04 = String.Empty;
        static public string gvRecive5562_05 = String.Empty;
        static public string gvRecive5562_06 = String.Empty;

        //*******************************************************************************
        // Declara las Variables Para Guardar Masivos
        //*******************************************************************************
        static public string gvstrUltimoFolioRemesa = String.Empty;
        static public string gvstrPrimerFolioRemesa = String.Empty;
        static public string gvstrRemesa = String.Empty;
        static public string gvstrNumSolicitudes = String.Empty;
        static public string gstrMsgVal = String.Empty; // Acumula los mensajes de la validacion de la remesa
        static public string gvstrFechaProceso = String.Empty;
        static public string gstrFormActivo = String.Empty;
        public const string gcstrEstatusRemesa = "02";

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct udtPromHeader
        { //HEADER DE ENVIO PARA DIALOGOS AL ANALISIS
            public FixedLengthString strTipoReg;
            public FixedLengthString strTipoSolicitud;
            public FixedLengthString strFechaArchivo;
            public FixedLengthString strFolioInicial;
            public FixedLengthString strFolioFinal;
            public FixedLengthString strNumDiskette;

            public static udtPromHeader CreateInstance()
            {
                udtPromHeader result = new udtPromHeader();
                result.strTipoReg = new FixedLengthString(2);
                result.strTipoSolicitud = new FixedLengthString(4);
                result.strFechaArchivo = new FixedLengthString(6);
                result.strFolioInicial = new FixedLengthString(8);
                result.strFolioFinal = new FixedLengthString(8);
                result.strNumDiskette = new FixedLengthString(2);
                return result;
            }
        }
        static public udtPromHeader estPromHeader = udtPromHeader.CreateInstance();

        public enum enmEstatusRemesa
        {
            stAsignada = 1,
            stGenerada = 2,
            stDisponible = 3,
            stRegistrada = 4,
            stError = 0
        }

        public const string gcstrProduccion = "TARCRE";
        public const string gcstrPruebas = "SPRMY01";
        public const string gcstrDesarrollo = "CEDETB";
        static public string gstrIndicadorAmbiente = String.Empty;

        //*******************************************************************************
        //* Finalidad:  Funcion para Armar el Header Principal que se utiliza en la Funcion
        //*                      funEnviaRecibe5562
        //* Entradas:   Clave de Transaccion; Clave de Subtransaccion y los Datos que varian
        //*                      dependiendo de la Funcion que llame a funEnviaRecibe5562
        //*******************************************************************************
        static public string funArmaHeader5562(string strCveTran, string strCveSubTran)
        {
            estHeader5562.strHDCveTran.Value = strCveTran;
            estHeader5562.strHDFiller01.Value = new String(' ', 1);
            estHeader5562.strHDSubTran.Value = strCveSubTran;
            estHeader5562.strHDFolPreimpreso.Value = mdlGlobales.funPoneCeros((Conversion.Val(mdlGlobales.gstrFolPreimpreso.Value) == 0) ? "" : mdlGlobales.gstrFolPreimpreso.Value, 16); //funZeroes(16)
            estHeader5562.strHDFolInterno.Value = mdlGlobales.funZeroes(8); //funPoneCeros(IIf(Val(gstrFolInterno) = 0, "", gstrFolInterno), 8) 'ASG Para la transacción 05
            estHeader5562.strHDSistOrigen.Value = "S753";
            estHeader5562.strHDTramite.Value = mdlGlobales.gstrTramite.Value;
            estHeader5562.strHDEntOrig.Value = mdlGlobales.funPoneCeros(mdlGlobales.gstrEntOrig.Value, 4).Substring(mdlGlobales.funPoneCeros(mdlGlobales.gstrEntOrig.Value, 4).Length - Math.Min(mdlGlobales.funPoneCeros(mdlGlobales.gstrEntOrig.Value, 4).Length, 2));
            //ASG CAMBIO DE GRUPO DE ENTIDAD ORIGEN
            estHeader5562.strHDGpoEntOrig.Value = "0001"; //El grupo siempre es 1
            estHeader5562.strHDCveEntOrig.Value = mdlGlobales.funPoneCeros(mdlGlobales.gstrCveEntOrig.Value, 4);
            if (strCveSubTran == "02")
            {
                estHeader5562.strHDEstatus.Value = "001"; //MMS 11/05 Incremento en la longitud del campo (2 a 3)
            }
            else
            {
                estHeader5562.strHDEstatus.Value = mdlGlobales.funZeroes(3); //MMS 11/05 Incremento en la longitud del campo (2 a 3)
            }
            estHeader5562.strHDCveResp.Value = mdlGlobales.funZeroes(2);
            estHeader5562.strHDDescResp.Value = mdlGlobales.funZeroes(50);
            estHeader5562.strHDNominaOper.Value = mdlGlobales.funPoneCeros(mdlGlobales.gstrNomina.Value, 10);
            estHeader5562.strHDCvePaqEval.Value = mdlGlobales.funZeroes(4);
            estHeader5562.strHDCveProceso.Value = mdlGlobales.funZeroes(3); //MMS 11/05 Incremento en la longitud del campo (2 a 3)
            estHeader5562.strHDFlagInfo.Value = mdlGlobales.funZeroes(1);
            estHeader5562.strHDCveRechazo.Value = mdlGlobales.funZeroes(4);
            estHeader5562.strHDPantalla.Value = mdlGlobales.funZeroes(8);
            estHeader5562.strHDNumMapa.Value = mdlGlobales.funZeroes(3); //MMS 11/05 Incremento en la longitud del campo (2 a 3)
            estHeader5562.strHDProcIni.Value = mdlGlobales.funZeroes(3); //MMS 11/05 Incremento en la longitud del campo (2 a 3)
            estHeader5562.strHDFiller02.Value = mdlGlobales.funZeroes(39); //MMS 11/05 Reducción del filler

            return estHeader5562.strHDCveTran.Value +
            estHeader5562.strHDFiller01.Value +
            estHeader5562.strHDSubTran.Value +
            estHeader5562.strHDFolPreimpreso.Value +
            estHeader5562.strHDFolInterno.Value +
            estHeader5562.strHDSistOrigen.Value +
            estHeader5562.strHDTramite.Value +
            estHeader5562.strHDEntOrig.Value +
            estHeader5562.strHDGpoEntOrig.Value +
            estHeader5562.strHDCveEntOrig.Value +
            estHeader5562.strHDEstatus.Value +
            estHeader5562.strHDCveResp.Value +
            estHeader5562.strHDDescResp.Value +
            estHeader5562.strHDNominaOper.Value +
            estHeader5562.strHDCvePaqEval.Value +
            estHeader5562.strHDCveProceso.Value +
            estHeader5562.strHDFlagInfo.Value +
            estHeader5562.strHDCveRechazo.Value +
            estHeader5562.strHDPantalla.Value +
            estHeader5562.strHDNumMapa.Value +
            estHeader5562.strHDProcIni.Value +
            estHeader5562.strHDFiller02.Value;
        }

        //*******************************************************************************
        //* Finalidad:  Funcion para Armar la Prioridad de la Remesa que se utiliza en la
        //*                      forma frmConsRemesa
        //* Entradas:   La Forma frmConsRemesa
        //*******************************************************************************
        static public string funArmaPrioridad(frmConsRemesa frmForma)
        {
            estPrioridad.strPRNumRemesa.Value = mdlGlobales.gstrNumRemesa.Value;
            estPrioridad.strPRPrioridad.Value = "01";

            return estPrioridad.strPRNumRemesa.Value + estPrioridad.strPRPrioridad.Value;
        }

        //*******************************************************************************
        //* Finalidad:  Funcion para Armar la Remesa que se utiliza en diferentes Formas
        //* Entradas:   Depende, pero las formas que lo Utilizan son:
        //*                frmRegRemesas , frmTipoDeclinacion, frmAutoriza, frmCausasDec
        //*******************************************************************************
        static public string funArmaGeneraRemesa()
        {
            estArmaGeneraRemesa.strGRSolicitud.Value = frmProcMasivo.DefInstance.txtNumSolicitudes.Text;
            estArmaGeneraRemesa.strGRFecIngCredi.Value = Strings.Mid(mdlGlobales.gstrFechaIngreso.Value, 5, 4) + Strings.Mid(mdlGlobales.gstrFechaIngreso.Value, 3, 2) + Strings.Mid(mdlGlobales.gstrFechaIngreso.Value, 1, 2);
            estArmaGeneraRemesa.strGRFecAcepCredi.Value = Strings.Mid(mdlGlobales.gstrFechaAceptacion.Value, 5, 4) + Strings.Mid(mdlGlobales.gstrFechaAceptacion.Value, 3, 2) + Strings.Mid(mdlGlobales.gstrFechaAceptacion.Value, 1, 2);
            estArmaGeneraRemesa.strGRCodPromoci.Value = mdlGlobales.funPoneCeros(mdlGlobales.gstrPromocion.Value, 4);
            estArmaGeneraRemesa.strGRFolPreImpIni.Value = mdlGlobales.gstrFolPreImpIni.Value;
            estArmaGeneraRemesa.strGRFolPreImpFin.Value = mdlGlobales.gstrFolPreImpFin.Value;

            return estArmaGeneraRemesa.strGRSolicitud.Value +
            estArmaGeneraRemesa.strGRFecIngCredi.Value +
            estArmaGeneraRemesa.strGRFecAcepCredi.Value +
            estArmaGeneraRemesa.strGRCodPromoci.Value +
            estArmaGeneraRemesa.strGRFolPreImpIni.Value +
            estArmaGeneraRemesa.strGRFolPreImpFin.Value;
        }

        //*******************************************************************************
        //* Propósito:  Envío - Recepción de diálogos 5562
        //* Salida:     True  --> Si todo Ok
        //*             False --> Si hubo error
        //*******************************************************************************
        static public bool funEnviaRecibe5562(string strCveTran, string strCveSubTran, string strDatos)
        {
            bool result = false;
            try
            {
                int intIntentos = 0;
                string strCadPaso = String.Empty;

                Cursor.Current = Cursors.WaitCursor;
                switch (strCveSubTran)
                {
                    case "01":
                        //AIS-1899 FSABORIO
                        MDIMasivos.DefInstance.pnlEstado.Items[0].Text = "VALIDANDO NUMERO DE REMESA...";
                        break;
                    case "02":
                        //AIS-1899 FSABORIO
                        MDIMasivos.DefInstance.pnlEstado.Items[0].Text = "GENERANDO NUMERO DE REMESA...";
                        break;
                    case "03":
                        //AIS-1899 FSABORIO
                        MDIMasivos.DefInstance.pnlEstado.Items[0].Text = "CAMBIANDO EL ESTATUS DE LA REMESA ...";
                        break;
                    case "04":
                        //AIS-1899 FSABORIO
                        MDIMasivos.DefInstance.pnlEstado.Items[0].Text = "CONSULTANDO ESTATUS DE REMESA...";
                        break;
                    case "05":
                        //AIS-1899 FSABORIO
                        MDIMasivos.DefInstance.pnlEstado.Items[0].Text = "CONSULTANDO TODA LA REMESA...";
                        break;
                    case "06":
                        //AIS-1899 FSABORIO
                        MDIMasivos.DefInstance.pnlEstado.Items[0].Text = "ASIGNANDO PRIORIDAD A LA REMESA...";
                        break;
                }
                mdlComunica.gvMensaje = "";
                mdlComunica.gvMensaje = funArmaHeader5562(strCveTran, strCveSubTran);
                mdlComunica.gvMensaje = mdlComunica.gvMensaje + strDatos;
                mdlComunica.gvRecive = "";
                intIntentos = 1;

                while (Strings.Mid(mdlComunica.gvRecive, 1, 4) != estHeader5562.strHDCveTran.Value && (mdlComunica.gvRecive.IndexOf("SEG;") + 1) < 1 && intIntentos < 5)
                {

                    mdlGlobales.subRegBitacora("E");
                    mdlGlobales.subDespMensajes("RESPUESTA DE TANDEM INTENTO: " + Conversion.Str(intIntentos));
                    mdlComunica.gvRecive = mdlComunica.funCON(mdlComunica.gvMensaje);
                    if (mdlComunica.gvRecive.IndexOf("Repita Transaccion") >= 0)
                    {
                        mdlComunica.gvRecive = mdlComunica.funCON(mdlComunica.gvMensaje);
                    }
                    intIntentos++;
                }
                Cursor.Current = Cursors.Default;
                if (Strings.Mid(mdlComunica.gvRecive, 1, 4) == estHeader5562.strHDCveTran.Value && Strings.Mid(mdlComunica.gvRecive, 6, 2) == strCveSubTran)
                {
                    if (Strings.Mid(mdlComunica.gvRecive, 51, 2) == mdlTranAnalisis.gcRespOk && Strings.Mid(mdlComunica.gvRecive, 24, 8) == estHeader5562.strHDFolInterno.Value)
                    { //MMS 11/05 Incremento en la longitud del campo (2 a 3)
                        switch (strCveSubTran)
                        {
                            case "01":
                                gvRecive5562_01 = mdlComunica.gvRecive;
                                break;
                            case "02":
                                gvRecive5562_02 = mdlComunica.gvRecive;
                                break;
                            case "03":
                                gvRecive5562_03 = mdlComunica.gvRecive;
                                break;
                            case "04":
                                gvRecive5562_04 = mdlComunica.gvRecive;
                                break;
                            case "05":
                                gvRecive5562_05 = mdlComunica.gvRecive;
                                break;
                            case "06":
                                gvRecive5562_06 = mdlComunica.gvRecive;
                                break;
                        }
                        result = true;
                        mdlGlobales.subDespMensajes("");
                        return result;
                    }
                    else if (Strings.Mid(mdlComunica.gvRecive, 51, 2) == mdlTranAnalisis.gcRespOk && strCveSubTran == "05")
                    {  //ASG 20040204 Validar la clave de la subtransacción, ya que para la "05" no se recibe el no. de folio  'MMS 11/05 Incremento en la longitud del campo (2 a 3)
                        gvRecive5562_05 = mdlComunica.gvRecive;
                        result = true;
                        mdlGlobales.subDespMensajes("");
                        return result;
                    }
                    else
                    {
                        Interaction.Beep();
                        if (mdlComunica.gvRecive.Trim().Length == 0)
                        {
                            //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                            string tempRefParam7 = "RESPUESTA ERRONEA DE TANDEM O SE ACABÓ EL TIEMPO DE ESPERA. POR FAVOR REINTENTE.";
                            MsgBoxStyle tempRefParam8 = (MsgBoxStyle)(((int)MsgBoxStyle.Exclamation) + ((int)MsgBoxStyle.OkOnly));
                            mdlGlobales.subDespErrores(ref tempRefParam7, ref tempRefParam8);
                            result = false;
                        }
                        else if (mdlComunica.gvRecive.IndexOf("FOLIO YA ACTUALIZADO") >= 0)
                        {
                            //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                            string tempRefParam9 = Strings.Mid(mdlComunica.gvRecive, 53, 50);
                            MsgBoxStyle tempRefParam10 = (MsgBoxStyle)(((int)MsgBoxStyle.Exclamation) + ((int)MsgBoxStyle.OkOnly));
                            mdlGlobales.subDespErrores(ref tempRefParam9, ref tempRefParam10); //Descriptivo de respuesta   'MMS 11/05 Incremento en la longitud del campo (2 a 3)
                            result = true;
                        }
                        else
                        {
                            //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                            string tempRefParam11 = Strings.Mid(mdlComunica.gvRecive, 53, 50);
                            MsgBoxStyle tempRefParam12 = (MsgBoxStyle)(((int)MsgBoxStyle.Exclamation) + ((int)MsgBoxStyle.OkOnly));
                            mdlGlobales.subDespErrores(ref tempRefParam11, ref tempRefParam12); //Descriptivo de respuesta   'MMS 11/05 Incremento en la longitud del campo (2 a 3)
                            result = false;
                        }
                    }
                }
                else
                {
                    Interaction.Beep();
                    //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                    string tempRefParam13 = Strings.Mid(mdlComunica.gvRecive, 1, 99);
                    MsgBoxStyle tempRefParam14 = (MsgBoxStyle)(((int)MsgBoxStyle.Exclamation) + ((int)MsgBoxStyle.OkOnly));
                    mdlGlobales.subDespErrores(ref tempRefParam13, ref tempRefParam14);
                    result = false;
                }
                mdlGlobales.subDespMensajes("");
                return result;
            }
            catch (Exception excep)
            {

                Cursor.Current = Cursors.Default;
                //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                string tempRefParam15 = "ERROR AL INTENTAR EJECUTAR LA TRANSACCIÓN CON TANDEM; " + Information.Err().Number.ToString() + ": " + excep.Message;
                MsgBoxStyle tempRefParam16 = (MsgBoxStyle)(((int)MsgBoxStyle.Critical) + ((int)MsgBoxStyle.OkOnly));
                mdlGlobales.subDespErrores(ref tempRefParam15, ref tempRefParam16);
                return result;
            }
        }
        static public string funValidaFecha(string strDateIn)
        {
            //*******************************************************************************
            //* Finalidad:  Funcion para dar Formato a la fecha de entrada
            //* IGA 1/10/2003
            //* Entradas:  Formato corto de fecha(AAMMDD) "021023"
            //*
            //* Salida:    Formato largo de fecha(AAAAMMDD) "20021023"
            //*******************************************************************************

            double dbNumericTemp = 0;
            if (Double.TryParse(strDateIn, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp) && strDateIn.Trim().Length > 0 && Conversion.Val(strDateIn) != 0)
            {
                if (StringsHelper.IntValue(strDateIn.Substring(0, Math.Min(strDateIn.Length, 2))) < 50)
                {
                    return "20" + strDateIn;
                }
                else
                {
                    return "19" + strDateIn;
                }
            }
            else
            {
                return mdlGlobales.funZeroes(8);
            }
        }

        static public string funValidaFechaNacimiento(string strDateIn)
        {
            //*******************************************************************************
            //* Finalidad:  Funcion para dar Formato a la fecha de entrada
            //* IGA 1/10/2003
            //* Entradas:  Formato corto de fecha(AAMMDD) "021023"
            //*
            //* Salida:    Formato largo de fecha(AAAAMMDD) "20021023"
            //*******************************************************************************

            double dbNumericTemp = 0;
            if (Double.TryParse(strDateIn, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp) && strDateIn.Trim().Length > 0 && Conversion.Val(strDateIn) != 0)
            {
                return "19" + strDateIn;
            }
            else
            {
                return mdlGlobales.funZeroes(8);
            }
        }


        //Limpia los arreglos para guardar los registros del archivo leído
        static public void subLimpiaVariablesDeBloque()
        {
            int intRefs = 0;
            int intReg08 = 0;
            int intReg09 = 0;
            estPromHeader.strTipoReg.Value = mdlGlobales.funZeroes(2);
            estPromHeader.strTipoSolicitud.Value = mdlGlobales.funZeroes(4);
            estPromHeader.strFechaArchivo.Value = new String(' ', 6);
            estPromHeader.strFolioInicial.Value = mdlGlobales.funZeroes(8);
            estPromHeader.strFolioFinal.Value = mdlGlobales.funZeroes(8);
            estPromHeader.strNumDiskette.Value = mdlGlobales.funZeroes(2);
            mdlTranCaptura.estProm01.strTipoReg.Value = mdlGlobales.funZeroes(2);
            mdlTranCaptura.estProm01.strFolio.Value = mdlGlobales.funZeroes(8);
            mdlTranCaptura.estProm01.strRFCPromotor.Value = new String(' ', 10);
            mdlTranCaptura.estProm01.strFirmaSol.Value = new String(' ', 1);
            mdlTranCaptura.estProm01.strLugar.Value = new String(' ', 20);
            mdlTranCaptura.estProm01.strLimiteCredito.Value = mdlGlobales.funZeroes(8);
            mdlTranCaptura.estProm01.strNombre.Value = new String(' ', 50); //AEFS Cambia de 30 a 50
            mdlTranCaptura.estProm01.strPaterno.Value = new String(' ', 60); //AEFS Cambia de 30 a 60
            mdlTranCaptura.estProm01.strMaterno.Value = new String(' ', 60); //AEFS Cambia de 30 a 60
            mdlTranCaptura.estProm01.strDomicilio.Value = new String(' ', 35);
            mdlTranCaptura.estProm01.strColonia.Value = new String(' ', 35);
            mdlTranCaptura.estProm01.strCodigoPostal.Value = mdlGlobales.funZeroes(5);
            mdlTranCaptura.estProm01.strMunicipio.Value = new String(' ', 26);
            mdlTranCaptura.estProm01.strEstado.Value = new String(' ', 4);
            mdlTranCaptura.estProm01.strFecSolicitud.Value = new String(' ', 6);
            mdlTranCaptura.estProm01.strLada.Value = mdlGlobales.funZeroes(3);
            mdlTranCaptura.estProm01.strTelefono.Value = mdlGlobales.funZeroes(7);
            mdlTranCaptura.estProm01.strAnosResidir.Value = mdlGlobales.funZeroes(4);
            mdlTranCaptura.estProm01.strNumDependientes.Value = mdlGlobales.funZeroes(2);
            mdlTranCaptura.estProm01.strFecNacimiento.Value = mdlGlobales.funZeroes(6);
            mdlTranCaptura.estProm01.strSexo.Value = new String(' ', 1);
            mdlTranCaptura.estProm01.strEstadoCivil.Value = new String(' ', 1);
            mdlTranCaptura.estProm01.strEscolaridad.Value = mdlGlobales.funZeroes(2);
            mdlTranCaptura.estProm01.strTipoVivienda.Value = new String(' ', 1);
            mdlTranCaptura.estProm01.strRFC.Value = new String(' ', 13);
            mdlTranCaptura.estProm01.strPNacimiento.Value = mdlGlobales.funZeroes(4); //AEFS Dato Nuevo
            mdlTranCaptura.estProm01.strPNacionalidad.Value = mdlGlobales.funZeroes(4); //AEFS Dato Nuevo
            mdlTranCaptura.estProm01.strFIEL.Value = new String(' ', 20); //AEFS Dato Nuevo
            //*** INI - IRP – Proy. 66008-06
            mdlTranCaptura.estProm01.strEntFedNac.Value= new String(' ', 2);
            //*** FIN - IRP – Proy. 66008-06
            //MODIF MAP ART.115 2016
            mdlTranCaptura.estProm01.strPaisAsig1.Value = mdlGlobales.funZeroes(4);
            mdlTranCaptura.estProm01.strIdentFis2.Value = mdlGlobales.funZeroes(20);
            mdlTranCaptura.estProm01.strPaisAsig2.Value = mdlGlobales.funZeroes(4);
            mdlTranCaptura.estProm01.strFechaCons.Value = mdlGlobales.funZeroes(8);
            mdlTranCaptura.estProm01.strGiroEmpre.Value = mdlGlobales.funZeroes(6);
            mdlTranCaptura.estProm01.strTrabExtra.Value = new String(' ', 2);
            mdlTranCaptura.estProm01.strTipoPerso.Value = mdlGlobales.funZeroes(2);


            mdlTranCaptura.estProm03.strTipoReg.Value = mdlGlobales.funZeroes(2);
            mdlTranCaptura.estProm03.strFolio.Value = mdlGlobales.funZeroes(8);
            mdlTranCaptura.estProm03.strAntiguedad.Value = mdlGlobales.funZeroes(4);
            mdlTranCaptura.estProm03.strCodigoPostal.Value = mdlGlobales.funZeroes(5);
            mdlTranCaptura.estProm03.strEstado.Value = new String(' ', 4);
            mdlTranCaptura.estProm03.strExtension.Value = mdlGlobales.funZeroes(4);
            mdlTranCaptura.estProm03.strLada.Value = mdlGlobales.funZeroes(3);
            mdlTranCaptura.estProm03.strOcupacion.Value = mdlGlobales.funZeroes(4);//AEFS Cambia de 3 a 4
            mdlTranCaptura.estProm03.strTelefono.Value = mdlGlobales.funZeroes(7);
            mdlTranCaptura.estProm03.strDomicilio.Value = new String(' ', 35);
            mdlTranCaptura.estProm03.strNomEmpresa.Value = new String(' ', 40);
            mdlTranCaptura.estProm03.strColPobFracc.Value = new String(' ', 35);
            mdlTranCaptura.estProm03.strMunicipio.Value = new String(' ', 26);
            mdlTranCaptura.estProm03.strDeptoArea.Value = new String(' ', 20);
            mdlTranCaptura.estProm05.strTipoReg.Value = mdlGlobales.funZeroes(2);
            mdlTranCaptura.estProm05.strFolio.Value = mdlGlobales.funZeroes(8);
            mdlTranCaptura.estProm05.strPaterno.Value = new String(' ', 60); //AEFS Cambia de 30 a 60
            mdlTranCaptura.estProm05.strMaterno.Value = new String(' ', 60); //AEFS Cambia de 30 a 60
            mdlTranCaptura.estProm05.strNombre.Value = new String(' ', 50); //AEFS Cambia de 4 a 50
            mdlTranCaptura.estProm05.strEstado.Value = new String(' ', 4);
            mdlTranCaptura.estProm05.strCodigoPostal.Value = mdlGlobales.funZeroes(5);
            mdlTranCaptura.estProm05.strLada.Value = mdlGlobales.funZeroes(3);
            mdlTranCaptura.estProm05.strExtension.Value = mdlGlobales.funZeroes(4);
            mdlTranCaptura.estProm05.strTelefono.Value = mdlGlobales.funZeroes(7);
            mdlTranCaptura.estProm05.strNombreEmp.Value = new String(' ', 40);
            mdlTranCaptura.estProm05.strDomicilio.Value = new String(' ', 35);
            mdlTranCaptura.estProm05.strColPobFracc.Value = new String(' ', 35);
            mdlTranCaptura.estProm05.strMunicipio.Value = new String(' ', 26);
            for (intRefs = 0; intRefs <= 1; intRefs++)
            {
                mdlTranCaptura.estProm06[intRefs].strTipoReg.Value = mdlGlobales.funZeroes(2);
                mdlTranCaptura.estProm06[intRefs].strFolio.Value = mdlGlobales.funZeroes(8);
                mdlTranCaptura.estProm06[intRefs].strNombre.Value = new String(' ', 50); //AEFS Cambia de 30 a 50
                mdlTranCaptura.estProm06[intRefs].strPaterno.Value = new String(' ', 60); //AEFS Cambia de 30 a 60
                mdlTranCaptura.estProm06[intRefs].strMaterno.Value = new String(' ', 60); //AEFS Cambia de 30 a 60
                mdlTranCaptura.estProm06[intRefs].strEstado.Value = new String(' ', 4);
                mdlTranCaptura.estProm06[intRefs].strCodigoPostal.Value = mdlGlobales.funZeroes(5);
                mdlTranCaptura.estProm06[intRefs].strLada.Value = mdlGlobales.funZeroes(3);
                mdlTranCaptura.estProm06[intRefs].strTelefono.Value = mdlGlobales.funZeroes(7);
                mdlTranCaptura.estProm06[intRefs].strParentesco.Value = new String(' ', 1);
                mdlTranCaptura.estProm06[intRefs].strDomicilio.Value = new String(' ', 35);
                mdlTranCaptura.estProm06[intRefs].strColPobFracc.Value = new String(' ', 35);
                mdlTranCaptura.estProm06[intRefs].strMunicipio.Value = new String(' ', 26);
            }
            mdlTranCaptura.estProm07.strTipoReg.Value = mdlGlobales.funZeroes(2);
            mdlTranCaptura.estProm07.strFolio.Value = mdlGlobales.funZeroes(8);
            mdlTranCaptura.estProm07.strTipoServicio.Value = mdlGlobales.funZeroes(2);
            mdlTranCaptura.estProm07.strNumCuenta.Value = mdlGlobales.funZeroes(16);
            mdlTranCaptura.estProm07.strSucursal.Value = mdlGlobales.funZeroes(4);
            for (intReg08 = 0; intReg08 <= 1; intReg08++)
            {
                mdlTranCaptura.estProm08[intReg08].strTipoReg.Value = mdlGlobales.funZeroes(2);
                mdlTranCaptura.estProm08[intReg08].strFolio.Value = mdlGlobales.funZeroes(8);
                mdlTranCaptura.estProm08[intReg08].strEmisor.Value = mdlGlobales.funZeroes(3);
                mdlTranCaptura.estProm08[intReg08].strTipoServicio.Value = mdlGlobales.funZeroes(2);
                mdlTranCaptura.estProm08[intReg08].strNumCuenta.Value = mdlGlobales.funZeroes(16);
            }
            for (intReg09 = 0; intReg09 <= 1; intReg09++)
            {
                mdlTranCaptura.estProm09[intReg09].strTipoReg.Value = mdlGlobales.funZeroes(2);
                mdlTranCaptura.estProm09[intReg09].strFolio.Value = mdlGlobales.funZeroes(8);
                mdlTranCaptura.estProm09[intReg09].strEmisor.Value = mdlGlobales.funZeroes(3);
                mdlTranCaptura.estProm09[intReg09].strNumCuenta.Value = mdlGlobales.funZeroes(16);
            }
            mdlTranCaptura.estProm10.strTipoReg.Value = mdlGlobales.funZeroes(2);
            mdlTranCaptura.estProm10.strFolio.Value = mdlGlobales.funZeroes(8);
            mdlTranCaptura.estProm10.strTipoPropiedad.Value = new String(' ', 1);
            mdlTranCaptura.estProm10.strAño1.Value = mdlGlobales.funZeroes(2);
            mdlTranCaptura.estProm10.strAño2.Value = mdlGlobales.funZeroes(2);
            mdlTranCaptura.estProm10.strMarca1.Value = new String(' ', 20);
            mdlTranCaptura.estProm10.strMarca2.Value = new String(' ', 20);
            mdlTranCaptura.estProm11.strTipoReg.Value = mdlGlobales.funZeroes(2);
            mdlTranCaptura.estProm11.strFolio.Value = mdlGlobales.funZeroes(8);
            mdlTranCaptura.estProm11.strIngfijos.Value = mdlGlobales.funZeroes(8);
            mdlTranCaptura.estProm11.strIngComisiones.Value = mdlGlobales.funZeroes(8);
            mdlTranCaptura.estProm11.strIngConyugue.Value = mdlGlobales.funZeroes(8);
            mdlTranCaptura.estProm11.strIngInversiones.Value = mdlGlobales.funZeroes(8);
            mdlTranCaptura.estProm11.strEgrOtros.Value = mdlGlobales.funZeroes(8);
            mdlTranCaptura.estProm11.strIngOtros.Value = mdlGlobales.funZeroes(8);
            mdlTranCaptura.estProm11.strEgrGastoFam.Value = mdlGlobales.funZeroes(8);
            mdlTranCaptura.estProm11.strEgrPagoAdeudo.Value = mdlGlobales.funZeroes(8);
            mdlTranCaptura.estProm11.strIngHonorarios.Value = mdlGlobales.funZeroes(8);
            mdlTranCaptura.estProm12.strTipoReg.Value = mdlGlobales.funZeroes(2);
            mdlTranCaptura.estProm12.strFolio.Value = mdlGlobales.funZeroes(8);
            mdlTranCaptura.estProm12.strFecNacimiento.Value = mdlGlobales.funZeroes(6);
            mdlTranCaptura.estProm12.strNombre.Value = new String(' ', 50); //AEFS Cambia de 30 a 50
            mdlTranCaptura.estProm12.strParentesco.Value = new String(' ', 1);
            mdlTranCaptura.estProm12.strPaterno.Value = new String(' ', 60); //AEFS Cambia de 30 a 60
            mdlTranCaptura.estProm12.strMaterno.Value = new String(' ', 60); //AEFS Cambia de 30 a 60
            mdlTranCaptura.estProm12.strSexo.Value = new String(' ', 1);
            mdlTranCaptura.estProm12.strFirmaAdic.Value = new String(' ', 1);
            mdlTranCaptura.estProm13.strTipoReg.Value = mdlGlobales.funZeroes(2);
            mdlTranCaptura.estProm13.strFolio.Value = mdlGlobales.funZeroes(8);
            mdlTranCaptura.estProm13.strSucursal.Value = new String(' ', 4);
            mdlTranCaptura.estProm13.strNomEjecutivo.Value = new String(' ', 8);
            mdlTranCaptura.estProm14.strTipoReg.Value = mdlGlobales.funZeroes(2);
            mdlTranCaptura.estProm14.strFolio.Value = mdlGlobales.funZeroes(8);
            mdlTranCaptura.estProm14.strCodigoPostal.Value = mdlGlobales.funZeroes(5);
            mdlTranCaptura.estProm14.strColonia.Value = new String(' ', 35);
            mdlTranCaptura.estProm14.strDomicilio.Value = new String(' ', 35);
            mdlTranCaptura.estProm14.strEstado.Value = new String(' ', 4);
            mdlTranCaptura.estProm14.strFirma.Value = new String(' ', 1);
            mdlTranCaptura.estProm14.strIngMensuales.Value = mdlGlobales.funZeroes(8);
            mdlTranCaptura.estProm14.strMaterno.Value = new String(' ', 60); //AEFS Cambia de 30 a 60
            mdlTranCaptura.estProm14.strMunicipio.Value = new String(' ', 26);
            mdlTranCaptura.estProm14.strNombre.Value = new String(' ', 50); //AEFS Cambia de 30 a 50
            mdlTranCaptura.estProm14.strPaterno.Value = new String(' ', 60); //AEFS Cambia de 30 a 60
            mdlTranCaptura.estProm14.strLada.Value = mdlGlobales.funZeroes(3);
            mdlTranCaptura.estProm14.strTelefono.Value = mdlGlobales.funZeroes(7);
            mdlTranCaptura.estProm14.strExtension.Value = mdlGlobales.funZeroes(4);
            mdlTranCaptura.estProm14.strOcupacion.Value = mdlGlobales.funZeroes(3);
            mdlTranCaptura.estProm14.strAntiguedad.Value = mdlGlobales.funZeroes(4);
            mdlTranCaptura.estProm14.strNomEmpresa.Value = new String(' ', 40);
            mdlTranCaptura.estProm15.strTipoReg.Value = mdlGlobales.funZeroes(2);
            mdlTranCaptura.estProm15.strFolio.Value = mdlGlobales.funZeroes(8);
            mdlTranCaptura.estProm15.strCompDomicilio.Value = new String(' ', 1);
            mdlTranCaptura.estProm15.strCompIdentificacion.Value = new String(' ', 1);
            mdlTranCaptura.estProm15.strCompIngresos.Value = new String(' ', 1);
            mdlTranCaptura.estProm15.strDescIdentificacion.Value = new String(' ', 20);
            mdlTranCaptura.estProm15.strIdentCatalogo.Value = new String(' ', 2);
            mdlTranCaptura.estProm16.strTipoReg.Value = mdlGlobales.funZeroes(2);
            mdlTranCaptura.estProm16.strFolio.Value = mdlGlobales.funZeroes(8);
            mdlTranCaptura.estProm16.strFecNacimiento.Value = mdlGlobales.funZeroes(8);
            mdlTranCaptura.estProm16.strFtesOtrosIng.Value = new String(' ', 1);
            mdlTranCaptura.estProm16.strNacionalidad.Value = new String(' ', 2);
            mdlTranCaptura.estProm16.strNumCliente.Value = mdlGlobales.funZeroes(12);
            mdlTranCaptura.estProm16.strSector.Value = new String(' ', 2);
            mdlTranCaptura.estProm16.strProfOfic.Value = new String(' ', 30);
            mdlTranCaptura.estProm17.strTipoReg.Value = mdlGlobales.funZeroes(2);
            mdlTranCaptura.estProm17.strFolio.Value = mdlGlobales.funZeroes(8);
            mdlTranCaptura.estProm17.strRFC.Value = new String(' ', 13);
            mdlTranCaptura.estProm17.strEmisorTarjeta1.Value = mdlGlobales.funZeroes(2);
            mdlTranCaptura.estProm17.strEmisorTarjeta2.Value = mdlGlobales.funZeroes(2);
            mdlTranCaptura.estProm17.strFecNacimiento.Value = mdlGlobales.funZeroes(8);
            mdlTranCaptura.estProm17.strNumCuenta.Value = mdlGlobales.funZeroes(6);
            mdlTranCaptura.estProm17.strNumTarjeta1.Value = mdlGlobales.funZeroes(16);
            mdlTranCaptura.estProm17.strNumTarjeta2.Value = mdlGlobales.funZeroes(16);
            mdlTranCaptura.estProm17.strOtrosIngresos.Value = mdlGlobales.funZeroes(8);
            mdlTranCaptura.estProm17.strReferencia.Value = new String(' ', 1);
            mdlTranCaptura.estProm17.strSucursal.Value = mdlGlobales.funZeroes(4);
            mdlTranCaptura.estProm18.strTipoReg.Value = mdlGlobales.funZeroes(2);
            mdlTranCaptura.estProm18.strFolio.Value = mdlGlobales.funZeroes(8);
            mdlTranCaptura.estProm18.strCanalVta.Value = new String(' ', 2);
            mdlTranCaptura.estProm18.strEstado.Value = new String(' ', 2);
            //AIS-Bug 9453 FSABORIO
            //Modif JGC 174/07/2008 PARA QUE SE GENERE EL NUMERO DE EMPRESA CON 4 DIGITOS
            //.strMedio = Space(3)
            mdlTranCaptura.estProm18.strMedio.Value = new String(' ', 4);
            mdlTranCaptura.estProm18.strProducto.Value = new String(' ', 4);
            mdlTranCaptura.estProm18.strRFCPromotor.Value = new String(' ', 10);
            mdlTranCaptura.estProm18.strSucursal.Value = new String(' ', 4);
            mdlTranCaptura.estProm19.strTipoReg.Value = mdlGlobales.funZeroes(2);
            mdlTranCaptura.estProm19.strFolio.Value = mdlGlobales.funZeroes(8);
            mdlTranCaptura.estProm19.strNumCuenta.Value = mdlGlobales.funZeroes(16);
            mdlTranCaptura.estProm19.strPlazo.Value = mdlGlobales.funZeroes(2);
            mdlTranCaptura.estProm19.strCURP.Value = new String(' ', 18); //AEFS Se Incluye
            mdlTranCaptura.estProm19.strCorreoE.Value = new String(' ', 78); //AEFS Cambia de 40 a 78
            mdlTranCaptura.estProm19.strCvePromocion.Value = new String(' ', 12); // VAR 04 Abril 2005 proyecto 20410 promociones Modificado de 8 a 12 para 2o alcance
            mdlTranCaptura.estProm19.strTipoComision.Value = new String(' ', 2);
            //MODIF MAP ART.115 2016
            //mdlTranCaptura.estProm21.strTipoRegis.Value = mdlGlobales.funZeroes(2);           
            //mdlTranCaptura.estProm21.strFolioPrei.Value = mdlGlobales.funZeroes(8);
            //mdlTranCaptura.estProm21.strIdentFis1.Value = mdlGlobales.funZeroes(20);
            //mdlTranCaptura.estProm21.strPaisAsig1.Value = mdlGlobales.funZeroes(4);
            //mdlTranCaptura.estProm21.strIdentFis2.Value = mdlGlobales.funZeroes(20);
            //mdlTranCaptura.estProm21.strPaisAsig2.Value = mdlGlobales.funZeroes(4);
            //mdlTranCaptura.estProm21.strFechaCons.Value = mdlGlobales.funZeroes(8);
            //mdlTranCaptura.estProm21.strGiroEmpre.Value = mdlGlobales.funZeroes(6);
            //mdlTranCaptura.estProm21.strTrabExtra.Value = new String(' ', 2);
            

            mdlPromotora.estPromTrailer.strTipoReg.Value = mdlGlobales.funZeroes(2);
            mdlPromotora.estPromTrailer.strRegistro[0].Value = mdlGlobales.funZeroes(5);
            mdlPromotora.estPromTrailer.strRegistro[1].Value = mdlGlobales.funZeroes(5);
            mdlPromotora.estPromTrailer.strRegistro[2].Value = mdlGlobales.funZeroes(5);
            mdlPromotora.estPromTrailer.strRegistro[3].Value = mdlGlobales.funZeroes(5);
            mdlPromotora.estPromTrailer.strRegistro[4].Value = mdlGlobales.funZeroes(5);
            mdlPromotora.estPromTrailer.strRegistro[5].Value = mdlGlobales.funZeroes(5);
            mdlPromotora.estPromTrailer.strRegistro[6].Value = mdlGlobales.funZeroes(5);
            mdlPromotora.estPromTrailer.strRegistro[7].Value = mdlGlobales.funZeroes(5);
            mdlPromotora.estPromTrailer.strRegistro[8].Value = mdlGlobales.funZeroes(5);
            mdlPromotora.estPromTrailer.strRegistro[9].Value = mdlGlobales.funZeroes(5);
            mdlPromotora.estPromTrailer.strRegistro[10].Value = mdlGlobales.funZeroes(5);
            mdlPromotora.estPromTrailer.strRegistro[11].Value = mdlGlobales.funZeroes(5);
            mdlPromotora.estPromTrailer.strRegistro[12].Value = mdlGlobales.funZeroes(5);
            mdlPromotora.estPromTrailer.strRegistro[13].Value = mdlGlobales.funZeroes(5);
            mdlPromotora.estPromTrailer.strRegistro[14].Value = mdlGlobales.funZeroes(5);
            mdlPromotora.estPromTrailer.strRegistro[15].Value = mdlGlobales.funZeroes(5);
            mdlPromotora.estPromTrailer.strRegistro[16].Value = mdlGlobales.funZeroes(5);
            mdlPromotora.estPromTrailer.strRegistro[17].Value = mdlGlobales.funZeroes(5);
            mdlPromotora.estPromTrailer.strRegistro[18].Value = mdlGlobales.funZeroes(5);
            mdlPromotora.estPromTrailer.strRegistro[19].Value = mdlGlobales.funZeroes(5);
            mdlPromotora.estPromTrailer.strEspacios.Value = mdlGlobales.funZeroes(10);
            mdlPromotora.estPromTrailer.strCantSolic.Value = mdlGlobales.funZeroes(5);
        }

        //Rutina para realizar el llenado de las estructuras de promotora en base a la cadena leída del archivo de promotoras
        static public void subArmaReg(string strCadena)
        {
            int IntRegistroRef = 0;
            byte intRegistro08 = 0;
            int intRegistro09 = 0;
            //OML 18/JUNIO/2004 MODIFICADO PARA MAS DE UN REG.8 Y MÁS DE UN REG.9
            string strTipoReg = Strings.Mid(strCadena, 1, 2);
            switch (strTipoReg)
            {
                case "00":
                    estPromHeader.strTipoReg.Value = Strings.Mid(strCadena, 1, 2);
                    estPromHeader.strTipoSolicitud.Value = Strings.Mid(strCadena, 3, 4);
                    estPromHeader.strFechaArchivo.Value = Strings.Mid(strCadena, 7, 6);
                    estPromHeader.strFolioInicial.Value = Strings.Mid(strCadena, 13, 8);
                    estPromHeader.strFolioFinal.Value = Strings.Mid(strCadena, 21, 8);
                    estPromHeader.strNumDiskette.Value = Strings.Mid(strCadena, 29, 2);
                    break;
                case "01":
                    mdlTranCaptura.estProm01.strTipoReg.Value = Strings.Mid(strCadena, 1, 2);
                    mdlTranCaptura.estProm01.strFolio.Value = Strings.Mid(strCadena, 3, 8);
                    mdlTranCaptura.estProm01.strRFCPromotor.Value = Strings.Mid(strCadena, 11, 10);
                    mdlTranCaptura.estProm01.strFirmaSol.Value = Strings.Mid(strCadena, 21, 1);
                    mdlTranCaptura.estProm01.strLugar.Value = Strings.Mid(strCadena, 22, 20);
                    mdlTranCaptura.estProm01.strLimiteCredito.Value = Strings.Mid(strCadena, 49, 8);
                    mdlTranCaptura.estProm01.strNombre.Value = Strings.Mid(strCadena, 57, 50); //AEFS Cambia de 30 a 50
                    mdlTranCaptura.estProm01.strPaterno.Value = Strings.Mid(strCadena, 107, 60); //AEFS Cambia pos 87 a 107 y long de 30 a 60
                    mdlTranCaptura.estProm01.strMaterno.Value = Strings.Mid(strCadena, 167, 60); //AEFS Cambia pos 117 a 167 y long de 30 a 60
                    mdlTranCaptura.estProm01.strDomicilio.Value = Strings.Mid(strCadena, 227, 35); //AEFS Cambia pos de 147 a 227
                    mdlTranCaptura.estProm01.strColonia.Value = Strings.Mid(strCadena, 262, 35); //AEFS Cambia pos de 182 a 262
                    mdlTranCaptura.estProm01.strCodigoPostal.Value = Strings.Mid(strCadena, 327, 5); //AEFS Cambia pos de 247 a 327
                    mdlTranCaptura.estProm01.strMunicipio.Value = Strings.Mid(strCadena, 297, 26); //AEFS Cambia pos de 217 a 297
                    mdlTranCaptura.estProm01.strEstado.Value = Strings.Mid(strCadena, 323, 4); //AEFS Cambia pos de 243 a 323
                    mdlTranCaptura.estProm01.strFecSolicitud.Value = Strings.Mid(strCadena, 43, 6);
                    mdlTranCaptura.estProm01.strLada.Value = Strings.Mid(strCadena, 332, 3); //AEFS Cambia pos de 252 a 332
                    mdlTranCaptura.estProm01.strTelefono.Value = Strings.Mid(strCadena, 335, 7); //AEFS Cambia pos de 255 a 335
                    mdlTranCaptura.estProm01.strAnosResidir.Value = Strings.Mid(strCadena, 354, 4); //AEFS Cambia pos de 274 a 354
                    mdlTranCaptura.estProm01.strNumDependientes.Value = Strings.Mid(strCadena, 351, 2); //AEFS Cambia pos de 271 a 351
                    mdlTranCaptura.estProm01.strFecNacimiento.Value = Strings.Mid(strCadena, 344, 6); //AEFS Cambia pos de 264 a 344
                    mdlTranCaptura.estProm01.strSexo.Value = Strings.Mid(strCadena, 371, 1); //AEFS Cambia pos de 291 a 371
                    mdlTranCaptura.estProm01.strEstadoCivil.Value = Strings.Mid(strCadena, 350, 1); //AEFS Cambia pos de 270 a 350
                    mdlTranCaptura.estProm01.strEscolaridad.Value = Strings.Mid(strCadena, 374, 2); //AEFS Cambia pos de 294 a 374
                    mdlTranCaptura.estProm01.strTipoVivienda.Value = Strings.Mid(strCadena, 353, 1); //AEFS Cambia pos de 273 a 353
                    mdlTranCaptura.estProm01.strRFC.Value = Strings.Mid(strCadena, 358, 13); //AEFS Cambia pos de 278 a 358
                    mdlTranCaptura.estProm01.strPNacimiento.Value = Strings.Mid(strCadena, 404, 4); //AEFS Dato Nuevo
                    mdlTranCaptura.estProm01.strPNacionalidad.Value = Strings.Mid(strCadena, 408, 4); //AEFS Dato Nuevo
                    mdlTranCaptura.estProm01.strFIEL.Value = Strings.Mid(strCadena, 412, 20); //AEFS Dato Nuevo
                    mdlTranCaptura.estProm01.strEntFedNac.Value = Strings.Mid(strCadena, 432, 2); //IRP – Proy. 66008-06                   
                    break;
                case "03":
                    mdlTranCaptura.estProm03.strTipoReg.Value = Strings.Mid(strCadena, 1, 2);
                    mdlTranCaptura.estProm03.strFolio.Value = Strings.Mid(strCadena, 3, 8);
                    mdlTranCaptura.estProm03.strAntiguedad.Value = Strings.Mid(strCadena, 12, 4);
                    mdlTranCaptura.estProm03.strCodigoPostal.Value = Strings.Mid(strCadena, 211, 5);//AEFS Cambia de 229 a 211
                    mdlTranCaptura.estProm03.strEstado.Value = Strings.Mid(strCadena, 207, 4); //AEFS Cambia de 225 a 207
                    mdlTranCaptura.estProm03.strExtension.Value = Strings.Mid(strCadena, 226, 4); //AEFS Cambia de 244 a 226
                    mdlTranCaptura.estProm03.strLada.Value = Strings.Mid(strCadena, 216, 3); //AEFS Cambia de 234 a 216
                    mdlTranCaptura.estProm03.strOcupacion.Value = Strings.Mid(strCadena, 87, 4); //AEFS Cambia de 106 a 87, long 3 a 4
                    mdlTranCaptura.estProm03.strTelefono.Value = Strings.Mid(strCadena, 219, 7); //AEFS Cambia de 237 a 219
                    mdlTranCaptura.estProm03.strDomicilio.Value = Strings.Mid(strCadena, 111, 35); //AEFS Cambia de 129 a 111
                    mdlTranCaptura.estProm03.strNomEmpresa.Value = Strings.Mid(strCadena, 16, 40);
                    mdlTranCaptura.estProm03.strColPobFracc.Value = Strings.Mid(strCadena, 146, 35); //AEFS Cambia de 164 a 146
                    mdlTranCaptura.estProm03.strMunicipio.Value = Strings.Mid(strCadena, 181, 26); //AEFS Cambia de 199 a 181
                    mdlTranCaptura.estProm03.strDeptoArea.Value = Strings.Mid(strCadena, 91, 20); //AEFS Cambia de 109 a 91 
                    break;
                case "04":
                    break;
                case "05":
                    mdlTranCaptura.estProm05.strTipoReg.Value = Strings.Mid(strCadena, 1, 2);
                    mdlTranCaptura.estProm05.strFolio.Value = Strings.Mid(strCadena, 3, 8);
                    mdlTranCaptura.estProm05.strPaterno.Value = Strings.Mid(strCadena, 41, 30); 
                    mdlTranCaptura.estProm05.strMaterno.Value = Strings.Mid(strCadena, 71, 30);
                    mdlTranCaptura.estProm05.strNombre.Value = Strings.Mid(strCadena, 11, 30);
                    mdlTranCaptura.estProm05.strEstado.Value = Strings.Mid(strCadena, 237, 4);
                    mdlTranCaptura.estProm05.strCodigoPostal.Value = Strings.Mid(strCadena, 241, 5);
                    mdlTranCaptura.estProm05.strLada.Value = Strings.Mid(strCadena, 246, 3);
                    mdlTranCaptura.estProm05.strExtension.Value = Strings.Mid(strCadena, 256, 4);
                    mdlTranCaptura.estProm05.strTelefono.Value = Strings.Mid(strCadena, 249, 7);
                    mdlTranCaptura.estProm05.strNombreEmp.Value = Strings.Mid(strCadena, 101, 40);
                    mdlTranCaptura.estProm05.strDomicilio.Value = Strings.Mid(strCadena, 141, 35);
                    mdlTranCaptura.estProm05.strColPobFracc.Value = Strings.Mid(strCadena, 176, 35);
                    mdlTranCaptura.estProm05.strMunicipio.Value = Strings.Mid(strCadena, 211, 26);
                    break;
                case "06":
                    if (Conversion.Val(mdlTranCaptura.estProm06[0].strTipoReg.Value) == 0)
                    { //Validar que el registro este lleno
                        IntRegistroRef = 0;
                    }
                    else
                    {
                        IntRegistroRef = 1;
                    }
                    mdlTranCaptura.estProm06[IntRegistroRef].strTipoReg.Value = Strings.Mid(strCadena, 1, 2);
                    mdlTranCaptura.estProm06[IntRegistroRef].strFolio.Value = Strings.Mid(strCadena, 3, 8);
                    mdlTranCaptura.estProm06[IntRegistroRef].strNombre.Value = Strings.Mid(strCadena, 11, 50); //AEFS cambia long de 30 a 50
                    mdlTranCaptura.estProm06[IntRegistroRef].strPaterno.Value = Strings.Mid(strCadena, 61, 50); //AEFS cambia pos de 41 a 61, long de 30 a 60
                    mdlTranCaptura.estProm06[IntRegistroRef].strMaterno.Value = Strings.Mid(strCadena, 121, 50); //AEFS cambia pos de 71 a 121,long de 30 a 60
                    mdlTranCaptura.estProm06[IntRegistroRef].strEstado.Value = Strings.Mid(strCadena, 277, 4); //AEFS cambia pos de 197 a 277
                    mdlTranCaptura.estProm06[IntRegistroRef].strCodigoPostal.Value = Strings.Mid(strCadena, 281, 5); //AEFS cambia pos de 201 a 281
                    mdlTranCaptura.estProm06[IntRegistroRef].strLada.Value = Strings.Mid(strCadena, 286, 3); //AEFS cambia pos de 206 a 286
                    mdlTranCaptura.estProm06[IntRegistroRef].strTelefono.Value = Strings.Mid(strCadena, 289, 7); //AEFS cambia pos de 209 a 289
                    mdlTranCaptura.estProm06[IntRegistroRef].strParentesco.Value = Strings.Mid(strCadena, 296, 1); //AEFS cambia pos de 216 a 296
                    mdlTranCaptura.estProm06[IntRegistroRef].strDomicilio.Value = Strings.Mid(strCadena, 181, 35); //AEFS cambia pos de 101 a 181
                    mdlTranCaptura.estProm06[IntRegistroRef].strColPobFracc.Value = Strings.Mid(strCadena, 216, 35); //AEFS cambia pos de 136 a 216
                    mdlTranCaptura.estProm06[IntRegistroRef].strMunicipio.Value = Strings.Mid(strCadena, 251, 26); //AEFS cambia pos de 171 a 251
                    break;
                case "07":
                    mdlTranCaptura.estProm07.strTipoReg.Value = Strings.Mid(strCadena, 1, 2);
                    mdlTranCaptura.estProm07.strFolio.Value = Strings.Mid(strCadena, 3, 8);
                    mdlTranCaptura.estProm07.strTipoServicio.Value = Strings.Mid(strCadena, 11, 2);
                    mdlTranCaptura.estProm07.strNumCuenta.Value = Strings.Mid(strCadena, 13, 16);
                    mdlTranCaptura.estProm07.strSucursal.Value = Strings.Mid(strCadena, 29, 4);
                    break;
                case "08":
                    if (Conversion.Val(mdlTranCaptura.estProm08[0].strTipoReg.Value) == 0)
                    { //Validar que el registro este lleno
                        intRegistro08 = 0;
                    }
                    else
                    {
                        intRegistro08 = 1;
                    }
                    mdlTranCaptura.estProm08[intRegistro08].strTipoReg.Value = Strings.Mid(strCadena, 1, 2);
                    mdlTranCaptura.estProm08[intRegistro08].strFolio.Value = Strings.Mid(strCadena, 3, 8);
                    mdlTranCaptura.estProm08[intRegistro08].strEmisor.Value = Strings.Mid(strCadena, 11, 3);
                    mdlTranCaptura.estProm08[intRegistro08].strTipoServicio.Value = Strings.Mid(strCadena, 68, 2);
                    mdlTranCaptura.estProm08[intRegistro08].strNumCuenta.Value = Strings.Mid(strCadena, 70, 16);
                    break;
                case "09":
                    if (Conversion.Val(mdlTranCaptura.estProm09[0].strTipoReg.Value) == 0)
                    { //Validar que el registro este lleno
                        intRegistro09 = 0;
                    }
                    else
                    {
                        intRegistro09 = 1;
                    }
                    mdlTranCaptura.estProm09[intRegistro09].strTipoReg.Value = Strings.Mid(strCadena, 1, 2);
                    mdlTranCaptura.estProm09[intRegistro09].strFolio.Value = Strings.Mid(strCadena, 3, 8);
                    mdlTranCaptura.estProm09[intRegistro09].strEmisor.Value = Strings.Mid(strCadena, 11, 3);
                    mdlTranCaptura.estProm09[intRegistro09].strNumCuenta.Value = Strings.Mid(strCadena, 14, 16);
                    break;
                case "10":
                    mdlTranCaptura.estProm10.strTipoReg.Value = Strings.Mid(strCadena, 1, 2);
                    mdlTranCaptura.estProm10.strFolio.Value = Strings.Mid(strCadena, 3, 8);
                    mdlTranCaptura.estProm10.strTipoPropiedad.Value = Strings.Mid(strCadena, 11, 1);
                    mdlTranCaptura.estProm10.strAño1.Value = Strings.Mid(strCadena, 48, 2);
                    mdlTranCaptura.estProm10.strAño2.Value = Strings.Mid(strCadena, 78, 2);
                    mdlTranCaptura.estProm10.strMarca1.Value = Strings.Mid(strCadena, 28, 20);
                    mdlTranCaptura.estProm10.strMarca2.Value = Strings.Mid(strCadena, 58, 20);
                    break;
                case "11":
                    mdlTranCaptura.estProm11.strTipoReg.Value = Strings.Mid(strCadena, 1, 2);
                    mdlTranCaptura.estProm11.strFolio.Value = Strings.Mid(strCadena, 3, 8);
                    mdlTranCaptura.estProm11.strIngfijos.Value = Strings.Mid(strCadena, 11, 8);
                    mdlTranCaptura.estProm11.strIngComisiones.Value = Strings.Mid(strCadena, 19, 8);
                    mdlTranCaptura.estProm11.strIngConyugue.Value = Strings.Mid(strCadena, 27, 8);
                    mdlTranCaptura.estProm11.strIngInversiones.Value = Strings.Mid(strCadena, 35, 8);
                    mdlTranCaptura.estProm11.strEgrOtros.Value = Strings.Mid(strCadena, 75, 8);
                    mdlTranCaptura.estProm11.strIngOtros.Value = Strings.Mid(strCadena, 51, 8);
                    mdlTranCaptura.estProm11.strEgrGastoFam.Value = Strings.Mid(strCadena, 59, 8);
                    mdlTranCaptura.estProm11.strEgrPagoAdeudo.Value = Strings.Mid(strCadena, 67, 8);
                    mdlTranCaptura.estProm11.strIngHonorarios.Value = Strings.Mid(strCadena, 43, 8);
                    break;
                case "12":
                    mdlTranCaptura.estProm12.strTipoReg.Value = Strings.Mid(strCadena, 1, 2);
                    mdlTranCaptura.estProm12.strFolio.Value = Strings.Mid(strCadena, 3, 8);
                    mdlTranCaptura.estProm12.strFecNacimiento.Value = Strings.Mid(strCadena, 182, 6); //AEFS Cambia pos de 102 a 182
                    mdlTranCaptura.estProm12.strNombre.Value = Strings.Mid(strCadena, 11, 50); //AEFS Cambia long de 30 a 50
                    mdlTranCaptura.estProm12.strParentesco.Value = Strings.Mid(strCadena, 181, 1); //AEFS Cambia pos de 101 a 181
                    mdlTranCaptura.estProm12.strPaterno.Value = Strings.Mid(strCadena, 61, 60); //AEFS Cambia pos de 41 a 61, long de 30 a 60
                    mdlTranCaptura.estProm12.strMaterno.Value = Strings.Mid(strCadena, 121, 60); //AEFS Cambia pos de 71 a 121, long de 30 a 60
                    mdlTranCaptura.estProm12.strSexo.Value = Strings.Mid(strCadena, 188, 1); //AEFS Cambia pos de 108 a 188
                    mdlTranCaptura.estProm12.strFirmaAdic.Value = Strings.Mid(strCadena, 189, 1); //AEFS Cambia pos de 109 a 189
                    break;
                case "13":
                    mdlTranCaptura.estProm13.strTipoReg.Value = Strings.Mid(strCadena, 1, 2);
                    mdlTranCaptura.estProm13.strFolio.Value = Strings.Mid(strCadena, 3, 8);
                    mdlTranCaptura.estProm13.strSucursal.Value = Strings.Mid(strCadena, 11, 4);
                    mdlTranCaptura.estProm13.strNomEjecutivo.Value = Strings.Mid(strCadena, 41, 8);
                    break;
                case "14":
                    mdlTranCaptura.estProm14.strTipoReg.Value = Strings.Mid(strCadena, 1, 2);
                    mdlTranCaptura.estProm14.strFolio.Value = Strings.Mid(strCadena, 3, 8);
                    mdlTranCaptura.estProm14.strCodigoPostal.Value = Strings.Mid(strCadena, 201, 5);
                    mdlTranCaptura.estProm14.strColonia.Value = Strings.Mid(strCadena, 136, 35);
                    mdlTranCaptura.estProm14.strDomicilio.Value = Strings.Mid(strCadena, 101, 35);
                    mdlTranCaptura.estProm14.strEstado.Value = Strings.Mid(strCadena, 197, 4);
                    mdlTranCaptura.estProm14.strFirma.Value = Strings.Mid(strCadena, 456, 1);
                    mdlTranCaptura.estProm14.strIngMensuales.Value = Strings.Mid(strCadena, 448, 8);
                    mdlTranCaptura.estProm14.strMaterno.Value = Strings.Mid(strCadena, 71, 30);
                    mdlTranCaptura.estProm14.strMunicipio.Value = Strings.Mid(strCadena, 171, 26);
                    mdlTranCaptura.estProm14.strNombre.Value = Strings.Mid(strCadena, 11, 30);
                    mdlTranCaptura.estProm14.strPaterno.Value = Strings.Mid(strCadena, 41, 30);

                    mdlTranCaptura.estProm14.strTelefono.Value = Strings.Mid(strCadena, 209, 7);
                    mdlTranCaptura.estProm14.strExtension.Value = Strings.Mid(strCadena, 371, 4);
                    mdlTranCaptura.estProm14.strOcupacion.Value = Strings.Mid(strCadena, 425, 3);
                    mdlTranCaptura.estProm14.strAntiguedad.Value = Strings.Mid(strCadena, 428, 4);
                    mdlTranCaptura.estProm14.strNomEmpresa.Value = Strings.Mid(strCadena, 216, 40);
                    break;
                case "15":
                    mdlTranCaptura.estProm15.strTipoReg.Value = Strings.Mid(strCadena, 1, 2);
                    mdlTranCaptura.estProm15.strFolio.Value = Strings.Mid(strCadena, 3, 8);
                    mdlTranCaptura.estProm15.strCompDomicilio.Value = Strings.Mid(strCadena, 13, 1);
                    mdlTranCaptura.estProm15.strCompIdentificacion.Value = Strings.Mid(strCadena, 11, 1);
                    mdlTranCaptura.estProm15.strCompIngresos.Value = Strings.Mid(strCadena, 12, 1);
                    mdlTranCaptura.estProm15.strIdentCatalogo.Value = Strings.Mid(strCadena, 17, 2);
                    mdlTranCaptura.estProm15.strDescIdentificacion.Value = Strings.Mid(strCadena, 19, 20);
                    break;
                case "16":
                    mdlTranCaptura.estProm16.strTipoReg.Value = Strings.Mid(strCadena, 1, 2);
                    mdlTranCaptura.estProm16.strFolio.Value = Strings.Mid(strCadena, 3, 8);
                    mdlTranCaptura.estProm16.strFecNacimiento.Value = Strings.Mid(strCadena, 166, 8);
                    mdlTranCaptura.estProm16.strFtesOtrosIng.Value = Strings.Mid(strCadena, 37, 1);
                    mdlTranCaptura.estProm16.strNacionalidad.Value = Strings.Mid(strCadena, 23, 2);
                    mdlTranCaptura.estProm16.strNumCliente.Value = Strings.Mid(strCadena, 11, 12);
                    mdlTranCaptura.estProm16.strSector.Value = Strings.Mid(strCadena, 35, 2);
                    mdlTranCaptura.estProm16.strProfOfic.Value = Strings.Mid(strCadena, 38, 30);
                    break;
                case "17":
                    mdlTranCaptura.estProm17.strTipoReg.Value = Strings.Mid(strCadena, 1, 2);
                    mdlTranCaptura.estProm17.strFolio.Value = Strings.Mid(strCadena, 3, 8);
                    mdlTranCaptura.estProm17.strRFC.Value = Strings.Mid(strCadena, 27, 13);
                    mdlTranCaptura.estProm17.strEmisorTarjeta1.Value = Strings.Mid(strCadena, 65, 2);
                    mdlTranCaptura.estProm17.strEmisorTarjeta2.Value = Strings.Mid(strCadena, 83, 2);
                    mdlTranCaptura.estProm17.strFecNacimiento.Value = Strings.Mid(strCadena, 11, 8);
                    mdlTranCaptura.estProm17.strNumCuenta.Value = Strings.Mid(strCadena, 47, 16);
                    mdlTranCaptura.estProm17.strNumTarjeta1.Value = Strings.Mid(strCadena, 67, 16);
                    mdlTranCaptura.estProm17.strNumTarjeta2.Value = Strings.Mid(strCadena, 85, 16);
                    mdlTranCaptura.estProm17.strOtrosIngresos.Value = Strings.Mid(strCadena, 19, 8);
                    mdlTranCaptura.estProm17.strReferencia.Value = Strings.Mid(strCadena, 40, 1);
                    mdlTranCaptura.estProm17.strSucursal.Value = Strings.Mid(strCadena, 43, 4);
                    break;
                case "18":
                    mdlTranCaptura.estProm18.strTipoReg.Value = Strings.Mid(strCadena, 1, 2);
                    mdlTranCaptura.estProm18.strFolio.Value = Strings.Mid(strCadena, 3, 8);
                    mdlTranCaptura.estProm18.strCanalVta.Value = Strings.Mid(strCadena, 17, 2);
                    mdlTranCaptura.estProm18.strEstado.Value = Strings.Mid(strCadena, 11, 2);
                    //AIS-Bug 9453 FSABORIO
                    //Modif JGC 21/07/2008 PARA QUE SE GENERE EL NUMERO DE EMPRESA CON 4 DIGITOS
                    //.strMedio = Mid(strCadena, 19, 3)
                    //.strProducto = Mid(strCadena, 22, 4)
                    //.strRFCPromotor = Mid(strCadena, 26, 10)
                    mdlTranCaptura.estProm18.strMedio.Value = Strings.Mid(strCadena, 19, 4);
                    mdlTranCaptura.estProm18.strProducto.Value = Strings.Mid(strCadena, 23, 4);
                    mdlTranCaptura.estProm18.strRFCPromotor.Value = Strings.Mid(strCadena, 27, 10);
                    //Fin modif JGC 21/07/2008
                    mdlTranCaptura.estProm18.strSucursal.Value = Strings.Mid(strCadena, 13, 4);
                    break;
                case "19":
                    mdlTranCaptura.estProm19.strTipoReg.Value = Strings.Mid(strCadena, 1, 2);
                    mdlTranCaptura.estProm19.strFolio.Value = Strings.Mid(strCadena, 3, 8);
                    mdlTranCaptura.estProm19.strNumCuenta.Value = Strings.Mid(strCadena, 13, 16);
                    mdlTranCaptura.estProm19.strPlazo.Value = mdlGlobales.funPoneCeros(Strings.Mid(strCadena, 11, 2), 4);
                    mdlTranCaptura.estProm19.strCorreoE.Value = Strings.Mid(strCadena, 99, 78); //AEFS Cambia long de 40 a 78, pos 42 a 99
                    mdlTranCaptura.estProm19.strCURP.Value = Strings.Mid(strCadena, 29, 18);  //ASG Cargar el CURP del archivo de promotoras // AEFS cambia long de 13 a 18
                    mdlTranCaptura.estProm19.strCvePromocion.Value = Strings.Mid(strCadena, 87, 12); //AEFS Cambia pos 82 a 87
                    mdlTranCaptura.estProm19.strTipoComision.Value = Strings.Mid(strCadena, 190, 2); //AEFS Cambia pos 94 a 190 -- Este campo no esta en el layout
                    break;
                case "21":                    
                    mdlTranCaptura.estProm01.strIdentFis1.Value = Strings.Mid(strCadena, 11, 20);
                    mdlTranCaptura.estProm01.strPaisAsig1.Value = Strings.Mid(strCadena, 31, 4);
                    mdlTranCaptura.estProm01.strIdentFis2.Value = Strings.Mid(strCadena, 35, 20);
                    mdlTranCaptura.estProm01.strPaisAsig2.Value = Strings.Mid(strCadena, 55, 4);
                    mdlTranCaptura.estProm01.strFechaCons.Value = Strings.Mid(strCadena, 59, 8);
                    mdlTranCaptura.estProm01.strGiroEmpre.Value = Strings.Mid(strCadena, 67, 6);
                    mdlTranCaptura.estProm01.strTrabExtra.Value = Strings.Mid(strCadena, 73, 2);
                    mdlTranCaptura.estProm01.strTipoPerso.Value = Strings.Mid(strCadena, 75, 2);      
                    break;
                case "99":
                    mdlPromotora.estPromTrailer.strTipoReg.Value = Strings.Mid(strTipoReg, 1, 2);
                    mdlPromotora.estPromTrailer.strRegistro[0].Value = new String(' ', 5);
                    mdlPromotora.estPromTrailer.strRegistro[1].Value = Strings.Mid(strCadena, 3, 5);
                    mdlPromotora.estPromTrailer.strRegistro[2].Value = Strings.Mid(strCadena, 8, 5);
                    mdlPromotora.estPromTrailer.strRegistro[3].Value = Strings.Mid(strCadena, 13, 5);
                    mdlPromotora.estPromTrailer.strRegistro[4].Value = Strings.Mid(strCadena, 18, 5);
                    mdlPromotora.estPromTrailer.strRegistro[5].Value = Strings.Mid(strCadena, 23, 5);
                    mdlPromotora.estPromTrailer.strRegistro[6].Value = Strings.Mid(strCadena, 28, 5);
                    mdlPromotora.estPromTrailer.strRegistro[7].Value = Strings.Mid(strCadena, 33, 5);
                    mdlPromotora.estPromTrailer.strRegistro[8].Value = Strings.Mid(strCadena, 38, 5);
                    mdlPromotora.estPromTrailer.strRegistro[9].Value = Strings.Mid(strCadena, 43, 5);
                    mdlPromotora.estPromTrailer.strRegistro[10].Value = Strings.Mid(strCadena, 48, 5);
                    mdlPromotora.estPromTrailer.strRegistro[11].Value = Strings.Mid(strCadena, 53, 5);
                    mdlPromotora.estPromTrailer.strRegistro[12].Value = Strings.Mid(strCadena, 58, 5);
                    mdlPromotora.estPromTrailer.strRegistro[13].Value = Strings.Mid(strCadena, 63, 5);
                    mdlPromotora.estPromTrailer.strRegistro[14].Value = Strings.Mid(strCadena, 68, 5);
                    mdlPromotora.estPromTrailer.strRegistro[15].Value = Strings.Mid(strCadena, 73, 5);
                    mdlPromotora.estPromTrailer.strRegistro[16].Value = Strings.Mid(strCadena, 78, 5);
                    mdlPromotora.estPromTrailer.strRegistro[17].Value = Strings.Mid(strCadena, 83, 5);
                    mdlPromotora.estPromTrailer.strRegistro[18].Value = Strings.Mid(strCadena, 88, 5);
                    mdlPromotora.estPromTrailer.strRegistro[19].Value = Strings.Mid(strCadena, 93, 5);
                    mdlPromotora.estPromTrailer.strEspacios.Value = Strings.Mid(strCadena, 98, 10);
                    mdlPromotora.estPromTrailer.strCantSolic.Value = Strings.Mid(strCadena, 108, 5);
                    break;
            }
        }

        //Función para generar la remesa
        static public bool funGeneraRemesa(ref  string strCveRemesa)
        {
            //Validar que exista la clave de paquete para este tramite-famile-producto
            if (!funLeePaquete(mdlGlobales.gstrTramite.Value, mdlCatalogos.gstrCatFamilia, mdlCatalogos.gstrTipSol))
            {
                //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                string tempRefParam = "NO EXISTE LA CLAVE PAQUETE PARA ESTE TRAMITE-FAMILIA-TIPO SOLICITUD, NO SE PUEDE GENERAR LA REMESA CORRESPONDIENTE AL ARCHIVO";
                MsgBoxStyle tempRefParam2 = (MsgBoxStyle)(((int)MsgBoxStyle.Critical) + ((int)MsgBoxStyle.OkOnly));
                string tempRefParam3 = "CLAVE PAQUETE";
                mdlGlobales.subDespErrores(ref tempRefParam, ref tempRefParam2, ref tempRefParam3);
                return false;
            }
            string strDatos = funArmaGeneraRemesa();
            if (funEnviaRecibe5562("5562", "02", strDatos))
            {
                strCveRemesa = Strings.Mid(mdlGlobales.gstrTramite.Value.Trim(), 1, 2) +
                               Strings.Mid(gvRecive5562_02, 177, 16);
                mdlGlobales.subDespMensajes("NÚMERO DE REMESA: " + strCveRemesa);
                frmProcMasivo.DefInstance.Refresh();
                frmProcMasivo.DefInstance.txtEntidadOrigen.Text = mdlGlobales.gstrCveEntOrig.Value + " " + mdlGlobales.gstrDescEntOrig.Value.Trim();
                frmProcMasivo.DefInstance.txtArchivo.Text = Strings.Mid(gvRecive5562_02, 193, 8);
                frmProcMasivo.DefInstance.txtTipoEntidad.Text = mdlGlobales.gstrTipoEntOrig.Value + " " + mdlGlobales.gstrDescTipoEntOrig.Value.Trim();
                frmProcMasivo.DefInstance.txtRemesa.Text = strCveRemesa;
                mdlGlobales.gblnRemesaRegistrada = true;
                return true;
            }
            else
            {
                //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                string tempRefParam4 = "Error: NO SE GENERÓ LA REMESA.";
                MsgBoxStyle tempRefParam5 = (MsgBoxStyle)(((int)MsgBoxStyle.Exclamation) + ((int)MsgBoxStyle.OkOnly));
                mdlGlobales.subDespErrores(ref tempRefParam4, ref tempRefParam5);
                mdlGlobales.gblnRemesaRegistrada = false;
                return false;
            }
        }

        //Función para cambiar el estatus de la remesa de 01 (Asignada) a 02 (Generada)
        static public bool funCambiaEstatusRemesa(string strRemesa, string strNombreArchivo, string strEstatus)
        {
            string strDatos = mdlGlobales.funPoneCeros(strRemesa, 10) + strNombreArchivo + strEstatus;
            if (funEnviaRecibe5562("5562", "03", strDatos))
            {
                string tempRefParam = Strings.Mid(gvRecive5562_03, 53, 50);
                mdlGlobales.subDespMsg(ref tempRefParam); //MMS 11/05 Incremento en la longitud del campo (2 a 3)
                return true;
            }
            else
            {
                string tempRefParam2 = Strings.Mid(gvRecive5562_03, 53, 50);
                mdlGlobales.subDespMsg(ref tempRefParam2); //MMS 11/05 Incremento en la longitud del campo (2 a 3)
                return false;
            }
        }

        //Función que devuelve el estatus actúal de la remesa
        static public enmEstatusRemesa funConsultaEstatus(string strCveRemesa)
        {
            if (funEnviaRecibe5562("5562", "04", strCveRemesa))
            {
                //UPGRADE_WARNING: (6021) Casting 'string' to Enum may cause different behaviour.
                return (enmEstatusRemesa)StringsHelper.IntValue(Strings.Mid(gvRecive5562_04, 193, 2));
            }
            else
            {
                return enmEstatusRemesa.stError;
            }
        }

        //Función para interpretar los estatus de la remesa
        static public string funDescripcionEstatusRemesa(enmEstatusRemesa IntEstatus)
        {
            if (IntEstatus == enmEstatusRemesa.stError)
            {
                return "ERROR AL CONSULTAR EL ESTATUS DE LA REMESA";
            }
            else if (IntEstatus == enmEstatusRemesa.stAsignada)
            {
                return "REMESA ASIGNADA (01)";
            }
            else if (IntEstatus == enmEstatusRemesa.stDisponible)
            {
                return "REMESA DISPONIBLE (03)";
            }
            else if (IntEstatus == enmEstatusRemesa.stGenerada)
            {
                return "REMESA GENERADA (02)";
            }
            else if (IntEstatus == enmEstatusRemesa.stRegistrada)
            {
                return "REMESA REGISTRADA (04)";
            }
            else
            {
                return "ERROR AL CONSULTAR EL ESTATUS DE LA REMESA";
            }
        }

        static public bool funLeePaquete(string strTramite, string strFamiliaProd, string strTipoSol)
        {
            //Validar que exista la clave de paquete para este tramite-famile-producto
            bool result = false;
            string strDatos = mdlGlobales.funPoneCeros(strTramite, 4) + mdlGlobales.funPoneCeros(strFamiliaProd, 2) + mdlGlobales.funPoneCeros(strTipoSol, 2);
            string tempRefParam4 = "46";
            string tempRefParam5 = mdlGlobales.gstrTipoEntOrig.Value;
            string tempRefParam6 = "1";
            string tempRefParam7 = "0";
            Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam8 = Catalogos.clsCatalogos.enmCamposCatalogos.Default;
            string tempRefParam9 = "E";
            if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam4, tempRefParam5, tempRefParam6, tempRefParam7, ref strDatos, ref tempRefParam8, ref tempRefParam9))
            {
                mdlGlobales.gstrTipoEntOrig.Value = tempRefParam5;
                //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                string tempRefParam = "NO EXISTE LA CLAVE PAQUETE PARA ESTE TRAMITE-FAMILIA-TIPO SOLICITUD, NO SE PUEDE GENERAR LA REMESA CORRESPONDIENTE AL ARCHIVO";
                MsgBoxStyle tempRefParam2 = (MsgBoxStyle)(((int)MsgBoxStyle.Critical) + ((int)MsgBoxStyle.OkOnly));
                string tempRefParam3 = "CLAVE PAQUETE";
                mdlGlobales.subDespErrores(ref tempRefParam, ref tempRefParam2, ref tempRefParam3);
                return false;
            }
            else
            {
                mdlGlobales.gstrTipoEntOrig.Value = tempRefParam5;
                mdlGlobales.gstrCvePaquete.Value = mdlGlobales.funPoneCeros(mdlComunica.OleCatalogos.getAtributos.Substring(0, Math.Min(mdlComunica.OleCatalogos.getAtributos.Length, 3)), 4);
                result = true;
            }
            return result;
        }
    }
}
