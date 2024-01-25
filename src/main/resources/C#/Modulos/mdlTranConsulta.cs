using Microsoft.VisualBasic; 
using System; 
using System.Runtime.InteropServices; 
using System.Windows.Forms;
using Microsoft.VisualBasic.Compatibility.VB6;
using VB6 = Microsoft.VisualBasic.Compatibility.VB6.Support; 

namespace Masivos
{
	class mdlTranConsulta
	{
	
		//*******************************************************************************
		//* Identificación: Modulo TranConsulta                                                   *
		//* Autor:          Abel Polo   *****                                           *
		//* Instalación:    PRAXIS                                                      *
		//* Fecha:          15/09/2003                                                  *
		//* Versión:        1.0                                                         *
		//*******************************************************************************
		
		//ESTRUCTURAS
		//HEADER DE ENVIO PARA DIALOGOS 5420s
		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
		public struct udtHeader5420
		{
            public FixedLengthString strHDECveTran;
            public FixedLengthString strHDEFiller01;
            public FixedLengthString strHDEIndicaTran;
            public FixedLengthString strHDEFolioPreImp;
            public FixedLengthString strHDEFolInterno;
            public FixedLengthString strHDESistema;
            public FixedLengthString strHDETipoTramite;
            public FixedLengthString strHDEFamiliaProd;
            public FixedLengthString strHDETipoSolicitud;
            public FixedLengthString strHDEstatus; //MMS 11/05 Incremento en la longitud del campo (2 a 3)
            public FixedLengthString strHDEResultado;
            public FixedLengthString strHDEDescResultado;
            public FixedLengthString strHDENumCte;
            public FixedLengthString strHDECveProc; //MMS 11/05 Incremento en la longitud del campo (2 a 3)
            public FixedLengthString strHDEFlag;
            public FixedLengthString strHDEIndControl;
            public FixedLengthString strHDEPantalla;
            public FixedLengthString strHDETipoPart;
            public FixedLengthString strHDETipoRelacion;
            public FixedLengthString strHDEIndPart;
            public FixedLengthString strHDECveTipoReg;
            public FixedLengthString strConsecutivo;
            public FixedLengthString strConsecutivoRef;
            public FixedLengthString strCveReporte;
            public FixedLengthString strFecIni;
            public FixedLengthString strFecFin;
            public FixedLengthString strEtiquetaSeg;
            public FixedLengthString strNumCuenta;
            public FixedLengthString strFiller02; //MMS 11/05 Incremento en la longitud del campo (2 a 3)

            public static udtHeader5420 CreateInstance()
            {
                udtHeader5420 result = new udtHeader5420();
                result.strHDECveTran = new FixedLengthString(4);
                result.strHDEFiller01 = new FixedLengthString(1);
                result.strHDEIndicaTran = new FixedLengthString(2);
                result.strHDEFolioPreImp = new FixedLengthString(16);
                result.strHDEFolInterno = new FixedLengthString(8);
                result.strHDESistema = new FixedLengthString(4);
                result.strHDETipoTramite = new FixedLengthString(2);
                result.strHDEFamiliaProd = new FixedLengthString(2);
                result.strHDETipoSolicitud = new FixedLengthString(2);
                result.strHDEstatus = new FixedLengthString(3); //MMS 11/05 Incremento en la longitud del campo (2 a 3)
                result.strHDEResultado = new FixedLengthString(2);
                result.strHDEDescResultado = new FixedLengthString(50);
                result.strHDENumCte = new FixedLengthString(12);
                result.strHDECveProc = new FixedLengthString(3); //MMS 11/05 Incremento en la longitud del campo (2 a 3)
                result.strHDEFlag = new FixedLengthString(1);
                result.strHDEIndControl = new FixedLengthString(1);
                result.strHDEPantalla = new FixedLengthString(2);
                result.strHDETipoPart = new FixedLengthString(2);
                result.strHDETipoRelacion = new FixedLengthString(2);
                result.strHDEIndPart = new FixedLengthString(2);
                result.strHDECveTipoReg = new FixedLengthString(2);
                result.strConsecutivo = new FixedLengthString(2);
                result.strConsecutivoRef = new FixedLengthString(2);
                result.strCveReporte = new FixedLengthString(2);
                result.strFecIni = new FixedLengthString(8);
                result.strFecFin = new FixedLengthString(8);
                result.strEtiquetaSeg = new FixedLengthString(4);
                result.strNumCuenta = new FixedLengthString(16);
                result.strFiller02 = new FixedLengthString(5); //MMS 11/05 Incremento en la longitud del campo (2 a 3)
                return result;
            }
        }
		static public udtHeader5420 estHeader5420 = udtHeader5420.CreateInstance();
		static public string gvRecive5420C = String.Empty;
		public const string strTipoBusqueda = "00";
		public const string gcstrCveProceso = "019"; //MMS 11/05 Incremento en la longitud del campo (2 a 3)
		
		//*****************************************************************************************************
		//*Finalidad:  Rutina para llenado de estructura del Header de consultas de solicitudes de ARIES
		//*             (Transacciones 5420s)
		//*Entradas:   strCveTran        Clave De La Transaccion
		//*            strCveSubTran     Clave De La SubTransaccion
		//*Salida:     Cadena de envio
		//*Versión: 2.0
		//****************************************************************************************************
		//Llena la estructura de Encabezado de Solicitudes ARIES
		static public string funArmaHeader5420( string strCveTran,  string strCveSubTran)
		{
			estHeader5420.strHDECveTran.Value = strCveTran;
			estHeader5420.strHDEFiller01.Value = " ";
			estHeader5420.strHDEIndicaTran.Value = strCveSubTran;
			estHeader5420.strHDEFolioPreImp.Value = mdlGlobales.funPoneCeros(frmPredFolio.DefInstance.txtFolioPreimpreso.Text, 16);
			estHeader5420.strHDEFolInterno.Value = mdlGlobales.funZeroes(8);
			estHeader5420.strHDESistema.Value = "S753";
			estHeader5420.strHDETipoTramite.Value = frmPredFolio.DefInstance.cboTipoTram.Text.Substring(0, Math.Min(frmPredFolio.DefInstance.cboTipoTram.Text.Length, 2));
			estHeader5420.strHDEFamiliaProd.Value = mdlGlobales.funZeroes(2);
			estHeader5420.strHDETipoSolicitud.Value = mdlGlobales.funZeroes(2);
			estHeader5420.strHDEstatus.Value = mdlGlobales.funZeroes(3); //MMS 11/05 Incremento en la longitud del campo (2 a 3)
			estHeader5420.strHDEResultado.Value = mdlGlobales.funZeroes(2);
			estHeader5420.strHDEDescResultado.Value = new String(' ', 50);
			estHeader5420.strHDENumCte.Value = mdlGlobales.funZeroes(12);
			estHeader5420.strHDECveProc.Value = gcstrCveProceso; //("19")
			estHeader5420.strHDEFlag.Value = "1"; //Para que regrese el estatus del expediente en la sección de resultados de la transacción
			estHeader5420.strHDEIndControl.Value = mdlGlobales.funZeroes(1);
			estHeader5420.strHDEPantalla.Value = mdlGlobales.funZeroes(2);
			estHeader5420.strHDETipoPart.Value = mdlGlobales.funZeroes(2);
			estHeader5420.strHDETipoRelacion.Value = mdlGlobales.funZeroes(2);
			estHeader5420.strHDEIndPart.Value = mdlGlobales.funZeroes(2);
			estHeader5420.strHDECveTipoReg.Value = mdlGlobales.funZeroes(2);
			estHeader5420.strConsecutivo.Value = mdlGlobales.funZeroes(2);
			estHeader5420.strConsecutivoRef.Value = mdlGlobales.funZeroes(2);
			estHeader5420.strCveReporte.Value = mdlGlobales.funZeroes(2);
			estHeader5420.strFecIni.Value = mdlGlobales.funZeroes(8);
			estHeader5420.strFecFin.Value = mdlGlobales.funZeroes(8);
			estHeader5420.strEtiquetaSeg.Value = new String(' ', 4);
			estHeader5420.strNumCuenta.Value = mdlGlobales.funZeroes(16);
			estHeader5420.strFiller02.Value = new String(' ', 5); //MMS 11/05 Incremento en la longitud del campo (2 a 3)
			//Genera gvMensaje5420
			return estHeader5420.strHDECveTran.Value + estHeader5420.strHDEFiller01.Value + 
			estHeader5420.strHDEIndicaTran.Value + estHeader5420.strHDEFolioPreImp.Value + estHeader5420.strHDEFolInterno.Value + 
			estHeader5420.strHDESistema.Value + estHeader5420.strHDETipoTramite.Value + estHeader5420.strHDEFamiliaProd.Value + 
			estHeader5420.strHDETipoSolicitud.Value + estHeader5420.strHDEstatus.Value + estHeader5420.strHDEResultado.Value + 
			estHeader5420.strHDEDescResultado.Value + estHeader5420.strHDENumCte.Value + estHeader5420.strHDECveProc.Value + 
			estHeader5420.strHDEFlag.Value + estHeader5420.strHDEIndControl.Value + estHeader5420.strHDEPantalla.Value + 
			estHeader5420.strHDETipoPart.Value + estHeader5420.strHDETipoRelacion.Value + estHeader5420.strHDEIndPart.Value + 
			estHeader5420.strHDECveTipoReg.Value + estHeader5420.strConsecutivo.Value + estHeader5420.strConsecutivoRef.Value + 
			estHeader5420.strCveReporte.Value + estHeader5420.strFecIni.Value + estHeader5420.strFecFin.Value + estHeader5420.strEtiquetaSeg.Value + 
			estHeader5420.strNumCuenta.Value + estHeader5420.strFiller02.Value;
		}
		
		//*******************************************************************************
		//* Finalidad:  Funcion para Armar el Header en la transacción 5420 C
		//* Entradas:   La Forma frmConsRemesa
		//*******************************************************************************
		static public string funArmaHeader542099( string strFolio,  string strMapa,  string strEstado)
		{
			mdlGlobales.strTranEncol.strTECveTran.Value= "5420";
			mdlGlobales.strTranEncol.strTEFiller01.Value= " ";
			mdlGlobales.strTranEncol.strTECveSubtran.Value= "99";
			mdlGlobales.strTranEncol.strTEFillerTran.Value= mdlGlobales.funZeroes(163);
			mdlGlobales.strTranEncol.strTEServer.Value= "S753-CATCHER   ";
			mdlGlobales.strTranEncol.strTEIdTrans.Value= "000A";
			mdlGlobales.strTranEncol.strTEFolio.Value= strFolio;
			mdlGlobales.strTranEncol.strTEMapa.Value= mdlGlobales.funPoneCeros(strMapa, 3); //MMS 11/05 Incremento en la longitud del campo (2 a 3)
			mdlGlobales.strTranEncol.strTEEstado.Value= strEstado; //Proceso inicial
			mdlGlobales.strTranEncol.strTEEstatus.Value= "000"; //MMS 11/05 Incremento en la longitud del campo (2 a 3)
		    return mdlGlobales.strTranEncol.strTECveTran.Value + mdlGlobales.strTranEncol.strTEFiller01.Value +
		           mdlGlobales.strTranEncol.strTECveSubtran.Value +
		           mdlGlobales.strTranEncol.strTEFillerTran.Value + mdlGlobales.strTranEncol.strTEServer.Value +
		           mdlGlobales.strTranEncol.strTEIdTrans.Value + mdlGlobales.strTranEncol.strTEFolio.Value +
		           mdlGlobales.strTranEncol.strTEMapa.Value + mdlGlobales.strTranEncol.strTEEstado.Value +
		           mdlGlobales.strTranEncol.strTEEstatus.Value;
		}
		
		//*****************************************************************************************************
		//* Finalidad:  Envío-Recepción de diálogo 5420 - Consulta de datos generales
		//*             --> Por Folio Preimpreso
		//*             --> Por Cliente
		//* Entradas:   strCveTran       Clave De La transaccion
		//*             strCveSubTran    Clave De La Subtransaccion
		//*             strProceso       Clave Del Proceso
		//* Salida:     True  --> Si todo Ok
		//*             False --> Si hubo error
		//* Versión: 1.0
		//****************************************************************************************************
		static public bool funEnviaRecibe5420s( string strCveTran,  string strCveSubTran,  string strProceso)
		{
			bool result = false;
			int intIntentos = 0;
			string strCadError = String.Empty;
            //AIS-1899 FSABORIO
			MDIMasivos.DefInstance.pnlEstado.Items[0].Text = "CONSULTANDO INFORMACION";
			mdlComunica.gvMensaje = funArmaHeader5420(strCveTran, strCveSubTran);
			Application.DoEvents();
			//Realiza envio y valida transacción y número de intentos
			mdlComunica.gvRecive = "";
			while (Strings.Mid(mdlComunica.gvRecive, 1, 4) != estHeader5420.strHDECveTran.Value && (mdlComunica.gvRecive.IndexOf("SEG;") + 1) < 1 && intIntentos < 3)
				{
				
					mdlGlobales.subRegBitacora("E");
					string tempRefParam2 = "POR FAVOR ESPERE RESPUESTA DE TANDEM - INTENTO No. : " + Conversion.Str(intIntentos);
					mdlGlobales.subDespMsg(ref tempRefParam2);
					mdlComunica.gvRecive = mdlComunica.funCON(mdlComunica.gvMensaje);
					intIntentos++;
				}
			if (Strings.Mid(mdlComunica.gvRecive, 1, 4) == estHeader5420.strHDECveTran.Value && Strings.Mid(mdlComunica.gvRecive, 6, 1) == strCveSubTran.Trim())
			{
				//Validar aquí que la respuesta sea OK (0)
				switch(strCveSubTran)
				{
					case "C " :  //FOLIO 
						gvRecive5420C = mdlComunica.gvRecive; 
						if ((Strings.Mid(mdlComunica.gvRecive, 45, 2) == "00" || Strings.Mid(mdlComunica.gvRecive, 45, 2) == "01") && Conversion.Val(Strings.Mid(mdlComunica.gvRecive, 171, 8).Trim()) > 0)
						{ //MMS 11/05 Se recorre posición del campo Resultado Transacción
							mdlGlobales.gstrFolInterno.Value = Strings.Mid(mdlComunica.gvRecive, 171, 8);
							mdlGlobales.gstrFamilia.Value = Strings.Mid(mdlComunica.gvRecive, 38, 2);
							mdlGlobales.gstrTipoSol.Value = Strings.Mid(mdlComunica.gvRecive, 40, 2);
							mdlGlobales.gstrEstatus.Value = Strings.Mid(mdlComunica.gvRecive, 781, 3); //MAP //MMS 11/05 Incremento en la longitud del campo Estatus (2 a 3)
							mdlGlobales.gstrProceso.Value = Strings.Mid(mdlComunica.gvRecive, 778, 3); //MAP //MMS 11/05 Incremento en la longitud del campo Proceso (2 a 3)
							mdlGlobales.gstrSigProceso.Value = Strings.Mid(mdlComunica.gvRecive, 849, 3); //MAP //MMS 11/05 Incremento en la longitud del campo Siguiente Proceso (2 a 3)
							string tempRefParam3 = "";
							mdlGlobales.subDespMsg(ref tempRefParam3);
							return true;
						} else if (Strings.Mid(mdlComunica.gvRecive, 45, 2) == "02") {  //MMS 11/05  Incremento en la longitud del campo (2 a 3)
							strCadError = "\n" + "\r" + "P.F. VERIFIQUE Y REINTENTE.";
							//UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
							string tempRefParam4 = "SOLICITUD DADA DE ALTA." + strCadError;
							MsgBoxStyle tempRefParam5 = (MsgBoxStyle) (((int) MsgBoxStyle.Information) + ((int) MsgBoxStyle.OkOnly));
							mdlGlobales.subDespErrores(ref tempRefParam4, ref tempRefParam5);
							string tempRefParam6 = "";
							mdlGlobales.subDespMsg(ref tempRefParam6);
							return false;
						} else if (Strings.Mid(mdlComunica.gvRecive, 45, 2) == "05") {  //MMS 11/05 Incremento en la longitud del campo (2 a 3)
							strCadError = "\n" + "\r" + "\n" + "\r" + "STATUS: " + Strings.Mid(mdlComunica.gvRecive, 351, 30) + "\n" + "\r" + 
							              "PROCESO APLICADO: " + Strings.Mid(mdlComunica.gvRecive, 381, 50) + 
                                          "\n" + "\r" + "CAUSA: " + Strings.Mid(mdlComunica.gvRecive, 461, 50);
							//UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
							string tempRefParam7 = "SOLICITUD DECLINADA." + "\n" + "\r" + "P.F. VERIFIQUE Y REINTENTE." + strCadError;
							MsgBoxStyle tempRefParam8 = (MsgBoxStyle) (((int) MsgBoxStyle.Information) + ((int) MsgBoxStyle.OkOnly));
							mdlGlobales.subDespErrores(ref tempRefParam7, ref tempRefParam8);
							string tempRefParam9 = "";
							mdlGlobales.subDespMsg(ref tempRefParam9);
							return false;
						} else if (Strings.Mid(mdlComunica.gvRecive, 45, 2) == "08") {  //MMS 11/05 Incremento en la longitud del campo (2 a 3)
							strCadError = "\n" + "\r" + "\n" + "\r" + "PROCESO SIGUIENTE:" + Strings.Mid(mdlComunica.gvRecive, 108, 2);
							//UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
							string tempRefParam10 = "SOLICITUD NO DISPONIBLE." + "\n" + "\r" + "P.F. VERIFIQUE Y REINTENTE." + strCadError;
							MsgBoxStyle tempRefParam11 = (MsgBoxStyle) (((int) MsgBoxStyle.Information) + ((int) MsgBoxStyle.OkOnly));
							mdlGlobales.subDespErrores(ref tempRefParam10, ref tempRefParam11);
							string tempRefParam12 = "";
							mdlGlobales.subDespMsg(ref tempRefParam12);
							return false;
						} else if (Strings.Mid(mdlComunica.gvRecive, 45, 2) == "09") {  //MMS 11/05 Incremento en la longitud del campo (2 a 3)
							strCadError = Strings.Mid(mdlComunica.gvRecive, 47, 50).Trim(); //MMS 11/05 Incremento en la longitud del campo (2 a 3)
							//UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
							string tempRefParam13 = strCadError + ". P.F. VERIFIQUE Y REINTENTE.";
							MsgBoxStyle tempRefParam14 = (MsgBoxStyle) (((int) MsgBoxStyle.Information) + ((int) MsgBoxStyle.OkOnly));
							mdlGlobales.subDespErrores(ref tempRefParam13, ref tempRefParam14);
							string tempRefParam15 = "";
							mdlGlobales.subDespMsg(ref tempRefParam15);
							return false;
						} else
						{
							strCadError = "\n" + "\r" + "\n" + "\r" + Strings.Mid(mdlComunica.gvRecive, 171, mdlComunica.gvRecive.Length - (mdlComunica.gvRecive.IndexOf("TR54") + 1)).Trim();
							//UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
							string tempRefParam16 = "ERROR EN LA CONSULTA DE LA SOLICITUD." + "\n" + "\r" + "P.F. VERIFIQUE Y REINTENTE." + strCadError;
							MsgBoxStyle tempRefParam17 = (MsgBoxStyle) (((int) MsgBoxStyle.Exclamation) + ((int) MsgBoxStyle.OkOnly));
							mdlGlobales.subDespErrores(ref tempRefParam16, ref tempRefParam17);
						} 
						break;
				}
			} else
			{
				Interaction.Beep();
				if (mdlComunica.gvRecive.Trim().Length == 0)
				{
					//UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
					string tempRefParam18 = "RESPUESTA ERRONEA DE TANDEM O SE ACABÓ EL TIEMPO DE ESPERA. POR FAVOR REINTENTE.";
					MsgBoxStyle tempRefParam19 = (MsgBoxStyle) (((int) MsgBoxStyle.Exclamation) + ((int) MsgBoxStyle.OkOnly));
					mdlGlobales.subDespErrores(ref tempRefParam18, ref tempRefParam19);
				} else
				{
					//UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
					string tempRefParam20 = Strings.Mid(mdlComunica.gvRecive, 47, 50);
					MsgBoxStyle tempRefParam21 = (MsgBoxStyle) (((int) MsgBoxStyle.Exclamation) + ((int) MsgBoxStyle.OkOnly));
					mdlGlobales.subDespErrores(ref tempRefParam20, ref tempRefParam21); //MMS 11/05 Incremento en la longitud del campo (2 a 3)
				}
				result = false;
			}
			return result;
		}
	}
}