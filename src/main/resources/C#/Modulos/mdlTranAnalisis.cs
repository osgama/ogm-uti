using Microsoft.VisualBasic; 
using System; 
using System.Globalization; 
using System.Runtime.InteropServices; 
using System.Windows.Forms;
using Microsoft.VisualBasic.Compatibility.VB6;
using VB6 = Microsoft.VisualBasic.Compatibility.VB6.Support; 

namespace Masivos
{
	class mdlTranAnalisis
	{
	
		//*******************************************************************************
		//* Objetivo: Módulo de transacciones de actualización al host (ANALISIS)     *
		//*******************************************************************************
		
		//CLAVES Y SUBCLAVES DE TRANSACCIONES
		public const string gctTranAct5561 = "5561"; //TRANSACCIONES DE ACTUALIZACION
		
		//ESTRUCTURAS
		//HEADER DE ENVIO PARA DIALOGOS AL ANALISIS
		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
		public struct udtHeader
		{
            public FixedLengthString strHDCveTran;
            public FixedLengthString strHDFiller01;
            public FixedLengthString strHDSubTran;
            public FixedLengthString strHDFolPreimpreso;
            public FixedLengthString strHDFolInterno;
            public FixedLengthString strHDSistOrigen;
            public FixedLengthString strHDTramite;
            public FixedLengthString strHDFamilia;
            public FixedLengthString strHDTipoSol;
            public FixedLengthString strHDEstatus; //MMS 11/05 Incremento en la longitud del campo (2 a 3)
            public FixedLengthString strHDCveResp;
            public FixedLengthString strHDDescResp;
            public FixedLengthString strHDNominaOper;
            public FixedLengthString strHDCveProceso; //MMS 11/05 Incremento en la longitud del campo (2 a 3)
            public FixedLengthString strHDFlagInfo;
            public FixedLengthString strHDIndicaCambio;
            public FixedLengthString strHDPantalla; //Proceso inicial
            public FixedLengthString strHDFiller02; //MMS 11/05 Reducción del filler

            public static udtHeader CreateInstance()
            {
                udtHeader result = new udtHeader();
                result.strHDCveTran = new FixedLengthString(4);
                result.strHDFiller01 = new FixedLengthString(1);
                result.strHDSubTran = new FixedLengthString(2);
                result.strHDFolPreimpreso = new FixedLengthString(16);
                result.strHDFolInterno = new FixedLengthString(8);
                result.strHDSistOrigen = new FixedLengthString(4);
                result.strHDTramite = new FixedLengthString(2);
                result.strHDFamilia = new FixedLengthString(2);
                result.strHDTipoSol = new FixedLengthString(2);
                result.strHDEstatus = new FixedLengthString(3); //MMS 11/05 Incremento en la longitud del campo (2 a 3)
                result.strHDCveResp = new FixedLengthString(2);
                result.strHDDescResp = new FixedLengthString(50);
                result.strHDNominaOper = new FixedLengthString(10);
                result.strHDCveProceso = new FixedLengthString(3); //MMS 11/05 Incremento en la longitud del campo (2 a 3)
                result.strHDFlagInfo = new FixedLengthString(1);
                result.strHDIndicaCambio = new FixedLengthString(1);
                result.strHDPantalla = new FixedLengthString(2); //Proceso inicial
                result.strHDFiller02 = new FixedLengthString(63); //MMS 11/05 Reducción del filler
                return result;
            }
        }
		static public udtHeader estHeader = udtHeader.CreateInstance();
		
		//HEADER DE ENVIO PARA DIALOGOS A CONSULTAS
		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
		public struct udtHeaderCons
		{
            public FixedLengthString strHDCveTran;
            public FixedLengthString strHDFiller01;
            public FixedLengthString strHDSubTran;
            public FixedLengthString strHDFolPreimpreso;
            public FixedLengthString strHDFolInterno;
            public FixedLengthString strHDSistOrigen;
            public FixedLengthString strHDTramite;
            public FixedLengthString strHDFamilia;
            public FixedLengthString strHDTipoSol;
            public FixedLengthString strHDEstatus; //MMS 11/05 Incremento en la longitud del campo (2 a 3)
            public FixedLengthString strHDCveResp;
            public FixedLengthString strHDDescResp;
            public FixedLengthString strHDNominaOper;
            public FixedLengthString strHDCveProceso; //MMS 11/05 Incremento en la longitud del campo (2 a 3)
            public FixedLengthString strHDFlagInfo;
            public FixedLengthString strHDIndicaCambio;
            public FixedLengthString strHDPantalla;
            public FixedLengthString strHDCveTipoPart; //MMS 11/05 Se agrega el campo Clave Tipo de Participante
            public FixedLengthString strHDCveTipoRel; //MMS 11/05 Se agrega el campo Clave Tipo de Relación
            public FixedLengthString strHDIndPart; //MMS 11/05 Se agrega el campo Indicativo de Participante
            public FixedLengthString strHDFiller02; //MMS 11/05 Incremento en la longitud del campo (2 a 3)

            public static udtHeaderCons CreateInstance()
            {
                udtHeaderCons result = new udtHeaderCons();
                result.strHDCveTran = new FixedLengthString(4);
                result.strHDFiller01 = new FixedLengthString(1);
                result.strHDSubTran = new FixedLengthString(2);
                result.strHDFolPreimpreso = new FixedLengthString(16);
                result.strHDFolInterno = new FixedLengthString(8);
                result.strHDSistOrigen = new FixedLengthString(4);
                result.strHDTramite = new FixedLengthString(2);
                result.strHDFamilia = new FixedLengthString(2);
                result.strHDTipoSol = new FixedLengthString(2);
                result.strHDEstatus = new FixedLengthString(3); //MMS 11/05 Incremento en la longitud del campo (2 a 3)
                result.strHDCveResp = new FixedLengthString(2);
                result.strHDDescResp = new FixedLengthString(50);
                result.strHDNominaOper = new FixedLengthString(10);
                result.strHDCveProceso = new FixedLengthString(3); //MMS 11/05 Incremento en la longitud del campo (2 a 3)
                result.strHDFlagInfo = new FixedLengthString(1);
                result.strHDIndicaCambio = new FixedLengthString(1);
                result.strHDPantalla = new FixedLengthString(2);
                result.strHDCveTipoPart = new FixedLengthString(2); //MMS 11/05 Se agrega el campo Clave Tipo de Participante
                result.strHDCveTipoRel = new FixedLengthString(2); //MMS 11/05 Se agrega el campo Clave Tipo de Relación
                result.strHDIndPart = new FixedLengthString(2); //MMS 11/05 Se agrega el campo Indicativo de Participante
                result.strHDFiller02 = new FixedLengthString(57); //MMS 11/05 Incremento en la longitud del campo (2 a 3)
                return result;
            }
        }
		static public udtHeaderCons estHeaderCons = udtHeaderCons.CreateInstance();
		
		//DATOS DE ACTUALIZACION DE ESTATUS
		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
		public struct udtActEstatus
		{
            public FixedLengthString strAEFecInicial;
            public FixedLengthString strAEHoraInicial;
            public FixedLengthString strAEFecFinal;
            public FixedLengthString strAEHoraFinal;
            public FixedLengthString strAECausaDec;
            public FixedLengthString strAEIndComentario;
            public FixedLengthString strAEComentarios;
            public FixedLengthString strAEMapa;
            public FixedLengthString strAEProceso;

            public static udtActEstatus CreateInstance()
            {
                udtActEstatus result = new udtActEstatus();
                result.strAEFecInicial = new FixedLengthString(8);
                result.strAEHoraInicial = new FixedLengthString(6);
                result.strAEFecFinal = new FixedLengthString(8);
                result.strAEHoraFinal = new FixedLengthString(6);
                result.strAECausaDec = new FixedLengthString(4);
                result.strAEIndComentario = new FixedLengthString(2);
                result.strAEComentarios = new FixedLengthString(150);
                result.strAEMapa = new FixedLengthString(3);
                result.strAEProceso = new FixedLengthString(3);
                return result;
            }
        }
		static public udtActEstatus estActEstatus = udtActEstatus.CreateInstance();
		
		public const string gcRespOk = "00";
		//RESPUESTAS DE CONSULTAS
		static public string gvRecive5560_36 = String.Empty; //Verificación de Límite inferior o superior de Créditos solicitados
		static public string gvRecive5560_18 = String.Empty; //Consulta siguiente folio
		static public string gvRecive5560_41 = String.Empty; //Verifica existencia de archivo VC, BOTÓNVC MEB 11-ENE-2005
		static public string gvRecive5560_42 = String.Empty; //Regresa el ambiente en el que se corre la aplicación (Producción, Pruebas o Desarrollo)
		//RESPUESTAS DE ACTUALIZACIONES
		static public string gvRecive5561_08 = String.Empty; //Actualización de Estatus
		static public string gvRecive5561_23 = String.Empty; //Recuperación Proceso Estatus
		static public string gvRecive5561_99 = String.Empty; //Actualización de Audit Trails
		
		//APG/PRAXIS   'No hay hoja de especificacion de la estructura
		//*****************************************************************************************************
		//* Finalidad:  Rutina para llenado de estructura del Header de Consultas       -5560-
		//*                                                             Actualizaciones -5561-
		//* Entradas: strCveTran     Clave De La Transaccion
		//*           strCveSubTran  Clave De La SubTransaccion
		//* Salida:   Cadena de envio
		//* Versión: 1.0
		//****************************************************************************************************
		static public string funArmaHeaderAnalisis( string strCveTran,  string strCveSubTran)
		{
			//Llena la estructura de Encabezado para transacciones al ANALISIS
			estHeader.strHDCveTran.Value = strCveTran;
			estHeader.strHDFiller01.Value = " ";
			estHeader.strHDSubTran.Value = strCveSubTran;
			double dbNumericTemp = 0;
			if (! Double.TryParse(mdlGlobales.gstrFolPreimpreso.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp))
			{ //MMS 05/01/06 Validación para llenar de ceros cuando la variable esta vacia
				mdlGlobales.gstrFolPreimpreso.Value = Convert.ToString(0);
			}
			estHeader.strHDFolPreimpreso.Value = mdlGlobales.funPoneCeros(mdlGlobales.gstrFolPreimpreso.Value, 16); //MMS 05/01/06 Se manda el número de folio como cadena
			estHeader.strHDFolInterno.Value = mdlGlobales.funPoneCeros(Conversion.Val(mdlGlobales.gstrFolInterno.Value).ToString(), 8);
			estHeader.strHDSistOrigen.Value = mdlTranCaptura.gcStrSistOrigen.Value;
			estHeader.strHDTramite.Value = mdlGlobales.funPoneCeros(Conversion.Val(mdlGlobales.gstrTramite.Value).ToString(), 2);
			estHeader.strHDFamilia.Value = mdlGlobales.funPoneCeros(Conversion.Val(mdlGlobales.gstrFamilia.Value).ToString(), 2);
			estHeader.strHDTipoSol.Value = mdlGlobales.funPoneCeros(Conversion.Val(mdlGlobales.gstrTipoSol.Value).ToString(), 2);
			estHeader.strHDEstatus.Value = mdlGlobales.funPoneCeros(Conversion.Val(mdlGlobales.gstrEstatus.Value).ToString(), 3); //MMS 11/05 Incremento en la longitud del campo Estatus de 2 a 3
			estHeader.strHDCveResp.Value = "00";
			estHeader.strHDDescResp.Value = new String(' ', 50);
			estHeader.strHDNominaOper.Value = mdlGlobales.funPoneCeros(Conversion.Val(mdlGlobales.gstrNomina.Value).ToString(), 10);
			estHeader.strHDCveProceso.Value = mdlGlobales.funPoneCeros(Conversion.Val(mdlGlobales.gstrProceso.Value).ToString(), 3); //MMS 11/05 Incremento en la longitud del campo Clave de Proceso de 2 a 3
			estHeader.strHDFlagInfo.Value = mdlGlobales.funPoneCeros(Conversion.Val(mdlGlobales.gstrFlagInfo.Value).ToString(), 1);
			estHeader.strHDIndicaCambio.Value = mdlGlobales.funPoneCeros(Conversion.Val(mdlGlobales.gstrIndicaCambio.Value).ToString(), 1);
			estHeader.strHDPantalla.Value = mdlGlobales.gstrPantalla.Value;
			estHeader.strHDFiller02.Value = new String(' ', 63); //MMS 11/05 Reducción del filler
			
			return estHeader.strHDCveTran.Value + estHeader.strHDFiller01.Value + estHeader.strHDSubTran.Value + estHeader.strHDFolPreimpreso.Value + 
			estHeader.strHDFolInterno.Value + estHeader.strHDSistOrigen.Value + estHeader.strHDTramite.Value + estHeader.strHDFamilia.Value + 
			estHeader.strHDTipoSol.Value + estHeader.strHDEstatus.Value + estHeader.strHDCveResp.Value + estHeader.strHDDescResp.Value + 
			estHeader.strHDNominaOper.Value + estHeader.strHDCveProceso.Value + estHeader.strHDFlagInfo.Value + estHeader.strHDIndicaCambio.Value + 
			estHeader.strHDPantalla.Value + estHeader.strHDFiller02.Value;
		}
		
		static public string funArmaHeaderConsulta( string strCveTran,  string strCveSubTran)
		{
			//Llena la estructura de Encabezado para transacciones al ANALISIS
			estHeaderCons.strHDCveTran.Value = strCveTran;
			estHeaderCons.strHDFiller01.Value = " ";
			estHeaderCons.strHDSubTran.Value = strCveSubTran;
			double dbNumericTemp = 0;
			if (! Double.TryParse(mdlGlobales.gstrFolPreimpreso.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp))
			{ //MMS 05/01/06 Validación para llenar de ceros cuando la variable esta vacia
				mdlGlobales.gstrFolPreimpreso.Value = Convert.ToString(0);
			}
			estHeaderCons.strHDFolPreimpreso.Value = mdlGlobales.funPoneCeros(mdlGlobales.gstrFolPreimpreso.Value, 16); //MMS 05/01/06 Se manda el número de folio como cadena
			estHeaderCons.strHDFolInterno.Value = mdlGlobales.funPoneCeros(Conversion.Val(mdlGlobales.gstrFolInterno.Value).ToString(), 8);
			estHeaderCons.strHDSistOrigen.Value = mdlTranCaptura.gcStrSistOrigen.Value;
			estHeaderCons.strHDTramite.Value = mdlGlobales.funPoneCeros(Conversion.Val(mdlGlobales.gstrTramite.Value).ToString(), 2);
			estHeaderCons.strHDFamilia.Value = mdlGlobales.funPoneCeros(Conversion.Val(mdlGlobales.gstrFamilia.Value).ToString(), 2);
			estHeaderCons.strHDTipoSol.Value = mdlGlobales.funPoneCeros(Conversion.Val(mdlGlobales.gstrTipoSol.Value).ToString(), 2);
			estHeaderCons.strHDEstatus.Value = "000"; //MMS 11/05 Incremento en la longitud del campo Estatus de 2 a 3
			estHeaderCons.strHDCveResp.Value = "00";
			estHeaderCons.strHDDescResp.Value = new String(' ', 50);
			estHeaderCons.strHDNominaOper.Value = mdlGlobales.funPoneCeros(Conversion.Val(mdlGlobales.gstrNomina.Value).ToString(), 10);
			estHeaderCons.strHDCveProceso.Value = "066"; //MMS 11/05 Incremento en la longitud del campo Clave de Proceso de 2 a 3 (66 a 066)
			estHeaderCons.strHDFlagInfo.Value = mdlGlobales.funPoneCeros(Conversion.Val(mdlGlobales.gstrFlagInfo.Value).ToString(), 1);
			estHeaderCons.strHDIndicaCambio.Value = mdlGlobales.funPoneCeros(Conversion.Val(mdlGlobales.gstrIndicaCambio.Value).ToString(), 1);
			estHeaderCons.strHDPantalla.Value = mdlGlobales.gstrPantalla.Value;
			estHeaderCons.strHDCveTipoPart.Value = "00"; //MMS 11/05 Se agrega campo Clave Tipo de Participante
			estHeaderCons.strHDCveTipoRel.Value = "00"; //MMS 11/05 Se agrega campo Clave Tipo de Relación
			estHeaderCons.strHDIndPart.Value = "00"; //MMS 11/05 Se agrega campo Clave Indicativo de Participante
			estHeaderCons.strHDFiller02.Value = new String(' ', 57); //MMS 11/05 Reducción del filler
			return estHeaderCons.strHDCveTran.Value + estHeaderCons.strHDFiller01.Value + estHeaderCons.strHDSubTran.Value + estHeaderCons.strHDFolPreimpreso.Value + 
			estHeaderCons.strHDFolInterno.Value + estHeaderCons.strHDSistOrigen.Value + estHeaderCons.strHDTramite.Value + estHeaderCons.strHDFamilia.Value + 
			estHeaderCons.strHDTipoSol.Value + estHeaderCons.strHDEstatus.Value + estHeaderCons.strHDCveResp.Value + estHeaderCons.strHDDescResp.Value + 
			estHeaderCons.strHDNominaOper.Value + estHeaderCons.strHDCveProceso.Value + estHeaderCons.strHDFlagInfo.Value + estHeaderCons.strHDIndicaCambio.Value + 
			estHeaderCons.strHDPantalla.Value + estHeaderCons.strHDCveTipoPart.Value + estHeaderCons.strHDCveTipoRel.Value + estHeaderCons.strHDIndPart.Value + 
			estHeaderCons.strHDFiller02.Value;
		}
		
		//*******************************************************************************
		//* Identificación: funArmaActEstatus                                                   *
		//* Autor:          Abel Polo   *****                                           *
		//* Instalación:    PRAXIS                                                      *
		//* Fecha:          15/09/2003                                                  *
		//* Versión:        1.0                                                         *
		//*******************************************************************************
		//*****************************************************************************************************
		//* Finalidad:  Rutina para llenado de estructura de datos para tran. Actualización de Estatus
		//* Entrada:    Forma PredFolio
		//* Salida:     Cadena de envio
		//* Versión: 1.0
		//****************************************************************************************************
		static public string funArmaActEstatus( frmPredFolio frmForma)
		{
			estActEstatus.strAEFecInicial.Value = mdlGlobales.gstrFecInicial.Value;
			estActEstatus.strAEHoraInicial.Value = mdlGlobales.gstrHoraInicial.Value;
			estActEstatus.strAEFecFinal.Value = mdlGlobales.gstrFecFinal.Value;
			estActEstatus.strAEHoraFinal.Value = mdlGlobales.gstrHoraFinal.Value;
			estActEstatus.strAECausaDec.Value = mdlGlobales.gstrCausaDec.Value;
			estActEstatus.strAEIndComentario.Value = "00";
			estActEstatus.strAEComentarios.Value = new String(' ', 150);
			estActEstatus.strAEMapa.Value = mdlGlobales.funZeroes(3);
			estActEstatus.strAEProceso.Value = mdlGlobales.funZeroes(3);
			return estActEstatus.strAEFecInicial.Value + estActEstatus.strAEHoraInicial.Value + 
			estActEstatus.strAEFecFinal.Value + estActEstatus.strAEHoraFinal.Value + estActEstatus.strAECausaDec.Value + 
			estActEstatus.strAEIndComentario.Value + estActEstatus.strAEComentarios.Value + estActEstatus.strAEMapa.Value + 
			estActEstatus.strAEProceso.Value;
		}
		
		//*******************************************************************************
		//* Identificación: funEnviaRecibe5560                                                   *
		//* Autor:          Abel Polo   *****                                           *
		//* Instalación:    PRAXIS                                                      *
		//* Fecha:          15/09/2003                                                  *
		//* Versión:        1.0                                                         *
		//*******************************************************************************
		//*****************************************************************************************************
		//* Finalidad:  Envío-Recepción de diálogos de Consulta 5560 - 30 Petición de Ingresos
		//*                                                          -
		//* Entradas:   strCveTran       Clave Transaccion
		//*             strCveSubTran    Clave SubTransaccion
		//*             strDatos         Datos
		//* Salida:     True  --> Si todo Ok
		//*             False --> Si hubo error
		//* Versión: 1.0
		//****************************************************************************************************
		static public bool funEnviaRecibe5560( string strCveTran,  string strCveSubTran,  string strDatos)
		{
			bool result = false;
			int intIntentos = 0;
			string strCadPaso = String.Empty;
			
			switch(strCveSubTran)
			{
				case "36" : 
					mdlGlobales.subDespMensajes("VALIDANDO NUMERO DE NOMINA ..."); 
					break;
				case "18" : 
					mdlGlobales.subDespMensajes("VERIFICANDO DISPONIBILIDAD DE LA SOLICITUD ..."); 
					break;
				case "41" : 
					mdlGlobales.subDespMensajes("VERIFICANDO EXISTENCIA DE ARCHIVO EN ARIES ..."); 
					break;
			}
			mdlComunica.gvMensaje = "";
			mdlComunica.gvMensaje = funArmaHeaderConsulta(strCveTran, strCveSubTran);
			mdlComunica.gvMensaje = mdlComunica.gvMensaje + strDatos;
			mdlComunica.gvRecive = "";
			while (Strings.Mid(mdlComunica.gvRecive, 1, 4) != estHeaderCons.strHDCveTran.Value && (mdlComunica.gvRecive.IndexOf("SEG;") + 1) < 1 && intIntentos < 4)
				{
				
					mdlGlobales.subRegBitacora("E");
					mdlComunica.gvRecive = mdlComunica.funCON(mdlComunica.gvMensaje);
					intIntentos++;
				}
			if (Strings.Mid(mdlComunica.gvRecive, 1, 4) == estHeaderCons.strHDCveTran.Value && Strings.Mid(mdlComunica.gvRecive, 6, 2) == strCveSubTran)
			{
				//Validar aquí que la respuesta sea OK (0)
				//MMS 11/05 Se recorre la posición del campo Resultado de la transacción (44 a 45) debido al aumento del campo Estatus de 2 a 3 posiciones ('00' a '000')
				if (Strings.Mid(mdlComunica.gvRecive, 45, 2) == gcRespOk && Strings.Mid(mdlComunica.gvRecive, 42, 3) == "000" && (Strings.Mid(mdlComunica.gvRecive, 24, 8) == estHeaderCons.strHDFolInterno.Value || Strings.Mid(mdlComunica.gvRecive, 8, 16) == estHeaderCons.strHDFolPreimpreso.Value))
				{
					switch(strCveSubTran)
					{
						case "36" : 
							gvRecive5560_36 = mdlComunica.gvRecive; 
							break;
						case "18" : 
							gvRecive5560_18 = mdlComunica.gvRecive; 
							break;
						case "41" : 
							gvRecive5560_41 = mdlComunica.gvRecive; 
							break;
						case "42" : 
							gvRecive5560_42 = mdlComunica.gvRecive; 
							break;
					}
					return true;
				} else if (Strings.Mid(mdlComunica.gvRecive, 24, 8) == estHeaderCons.strHDFolInterno.Value && Strings.Mid(mdlComunica.gvRecive, 45, 2) == gcRespOk) { 
					switch(Strings.Mid(mdlComunica.gvRecive, 42, 3))
					{ //MMS 11/05 Incremento en la longitud del campo (2 a 3)
						case "001" : 
							//UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour. 
							string tempRefParam = "SOLICITUD NO EXISTE EN BD."; 
							MsgBoxStyle tempRefParam2 = (MsgBoxStyle) (((int) MsgBoxStyle.Information) + ((int) MsgBoxStyle.OkOnly)); 
							mdlGlobales.subDespErrores(ref tempRefParam, ref tempRefParam2); 
							break;
						case "002" : 
							//UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour. 
							string tempRefParam3 = "SOLICITUD ESTA SIENDO UTILIZADA POR: " + Strings.Mid(mdlComunica.gvRecive, 201, 8); 
							MsgBoxStyle tempRefParam4 = (MsgBoxStyle) (((int) MsgBoxStyle.Information) + ((int) MsgBoxStyle.OkOnly)); 
							mdlGlobales.subDespErrores(ref tempRefParam3, ref tempRefParam4); 
							break;
						case "003" : 
							//UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour. 
							string tempRefParam5 = "SOLICITUD NO CONCLUYO PROCESO"; 
							MsgBoxStyle tempRefParam6 = (MsgBoxStyle) (((int) MsgBoxStyle.Information) + ((int) MsgBoxStyle.OkOnly)); 
							mdlGlobales.subDespErrores(ref tempRefParam5, ref tempRefParam6); 
							break;
						case "004" : 
							mdlComunica.strCveCausa.Value = Strings.Mid(mdlComunica.gvRecive, 380, 4); 
							//UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour. 
							string tempRefParam7 = "SOLICITUD DECLINADA. " + "\r\n" + mdlComunica.strCveCausa.Value + " - " + Strings.Mid("385", 50); 
							MsgBoxStyle tempRefParam8 = (MsgBoxStyle) (((int) MsgBoxStyle.Information) + ((int) MsgBoxStyle.OkOnly)); 
							mdlGlobales.subDespErrores(ref tempRefParam7, ref tempRefParam8); 
							break;
						case "005" : 
							//UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour. 
							string tempRefParam9 = "SOLICITUD NO EXISTE EN I36"; 
							MsgBoxStyle tempRefParam10 = (MsgBoxStyle) (((int) MsgBoxStyle.Information) + ((int) MsgBoxStyle.OkOnly)); 
							mdlGlobales.subDespErrores(ref tempRefParam9, ref tempRefParam10); 
							break;
						case "006" : 
							//UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour. 
							string tempRefParam11 = "SOLICITUD EN PROCESO 57"; 
							MsgBoxStyle tempRefParam12 = (MsgBoxStyle) (((int) MsgBoxStyle.Information) + ((int) MsgBoxStyle.OkOnly)); 
							mdlGlobales.subDespErrores(ref tempRefParam11, ref tempRefParam12); 
							break;
						case "098" : case "099" : 
							//UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour. 
							string tempRefParam13 = "ERROR EN BD"; 
							MsgBoxStyle tempRefParam14 = (MsgBoxStyle) (((int) MsgBoxStyle.Exclamation) + ((int) MsgBoxStyle.OkOnly)); 
							mdlGlobales.subDespErrores(ref tempRefParam13, ref tempRefParam14); 
							break;
						default:
							//UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour. 
							string tempRefParam15 = Strings.Mid(mdlComunica.gvRecive, 45, 52); 
							MsgBoxStyle tempRefParam16 = (MsgBoxStyle) (((int) MsgBoxStyle.Exclamation) + ((int) MsgBoxStyle.OkOnly)); 
							mdlGlobales.subDespErrores(ref tempRefParam15, ref tempRefParam16);  //Descriptivo de respuesta  MMS 11/05 Incremento en la longitud del campo (2 a 3) 
							break;
					}
					return false;
				} else
				{
					Interaction.Beep();
					if (mdlComunica.gvRecive.Trim().Length == 0)
					{
						//UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
						string tempRefParam17 = "Respuesta erronea de Tandem o se acabó el tiempo de espera. Por favor reintente.";
						MsgBoxStyle tempRefParam18 = (MsgBoxStyle) (((int) MsgBoxStyle.Exclamation) + ((int) MsgBoxStyle.OkOnly));
						mdlGlobales.subDespErrores(ref tempRefParam17, ref tempRefParam18);
					} else
					{
						//UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
						string tempRefParam19 = Strings.Mid(mdlComunica.gvRecive, 47, 50).Trim();
						MsgBoxStyle tempRefParam20 = (MsgBoxStyle) (((int) MsgBoxStyle.Exclamation) + ((int) MsgBoxStyle.OkOnly));
						mdlGlobales.subDespErrores(ref tempRefParam19, ref tempRefParam20); //Descriptivo de respuesta  MMS 11/05 Incremento en la longitud del campo (2 a 3)
					}
					result = false;
				}
			} else
			{
				Interaction.Beep();
				//UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
				string tempRefParam21 = Strings.Mid(mdlComunica.gvRecive, 1, 80);
				MsgBoxStyle tempRefParam22 = (MsgBoxStyle) (((int) MsgBoxStyle.Exclamation) + ((int) MsgBoxStyle.OkOnly));
				mdlGlobales.subDespErrores(ref tempRefParam21, ref tempRefParam22);
				result = false;
			}
			return result;
		}
		
		//*******************************************************************************
		//* Identificación: funEnviaRecibe5561                                                   *
		//* Autor:          Abel Polo   *****                                           *
		//* Instalación:    PRAXIS                                                      *
		//* Fecha:          15/09/2003                                                  *
		//* Versión:        1.0                                                         *
		//*******************************************************************************
		//*****************************************************************************************************
		//* Finalidad:  Envío-Recepción de diálogos 5561 - 23
		//* Entradas:   strCveTran       Clave Transaccion
		//*             strCveSubTran    Clave SubTransaccion
		//*             strDatos         Datos
		//* Salida:     True  --> Si todo Ok
		//*             False --> Si hubo error
		//* Versión: 1.0
		//****************************************************************************************************
		static public bool funEnviaRecibe5561( string strCveTran,  string strCveSubTran,  string strDatos)
		{
			bool result = false;
			string strCadPaso = String.Empty;
			
			switch(strCveSubTran)
			{
				case "08" :
                    //AIS-1899 FSABORIO
					MDIMasivos.DefInstance.pnlEstado.Items[0].Text = "PREDICTAMINACION DEL FOLIO"; 
					break;
				case "23" :
                    //AIS-1899 FSABORIO
					MDIMasivos.DefInstance.pnlEstado.Items[0].Text = "RECUPERANDO PROCESO-ESTATUS"; 
					break;
			}
			mdlComunica.gvMensaje = "";
			mdlComunica.gvMensaje = funArmaHeaderAnalisis(strCveTran, strCveSubTran);
			mdlComunica.gvMensaje = mdlComunica.gvMensaje + strDatos;
			mdlComunica.gvRecive = "";
			int intIntentos = 1;
			while (Strings.Mid(mdlComunica.gvRecive, 1, 4) != estHeader.strHDCveTran.Value && (mdlComunica.gvRecive.IndexOf("SEG;") + 1) < 1 && intIntentos < 2)
				{
				
					mdlGlobales.subRegBitacora("E");
					mdlComunica.gvRecive = mdlComunica.funCON(mdlComunica.gvMensaje);
					if (mdlComunica.gvRecive.IndexOf("Repita Transaccion") >= 0)
					{
						mdlComunica.gvRecive = mdlComunica.funCON(mdlComunica.gvMensaje);
					}
					intIntentos++;
				}
			
			if (Strings.Mid(mdlComunica.gvRecive, 1, 4) == estHeader.strHDCveTran.Value && Strings.Mid(mdlComunica.gvRecive, 6, 2) == strCveSubTran)
			{
				//Validar aquí que la respuesta sea OK (0)
				if (Strings.Mid(mdlComunica.gvRecive, 45, 2) == gcRespOk && Strings.Mid(mdlComunica.gvRecive, 24, 8) == estHeader.strHDFolInterno.Value)
				{ //MMS 11/05 Incremento en la longitud del campo (2 a 3)
					switch(strCveSubTran)
					{
						case "08" : 
							gvRecive5561_08 = mdlComunica.gvRecive; 
							break;
						case "23" : 
							gvRecive5561_23 = mdlComunica.gvRecive; 
							break;
					}
					return true;
				} else
				{
					Interaction.Beep();
					if (mdlComunica.gvRecive.Trim().Length == 0)
					{
						//UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
						string tempRefParam3 = "Respuesta erronea de Tandem o se acabó el tiempo de espera. Por favor reintente.";
						MsgBoxStyle tempRefParam4 = (MsgBoxStyle) (((int) MsgBoxStyle.Exclamation) + ((int) MsgBoxStyle.OkOnly));
						mdlGlobales.subDespErrores(ref tempRefParam3, ref tempRefParam4);
						result = false;
					} else if (mdlComunica.gvRecive.IndexOf("FOLIO YA ACTUALIZADO") >= 0) { 
						//UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
						string tempRefParam5 = Strings.Mid(mdlComunica.gvRecive, 45, 52);
						MsgBoxStyle tempRefParam6 = (MsgBoxStyle) (((int) MsgBoxStyle.Exclamation) + ((int) MsgBoxStyle.OkOnly));
						mdlGlobales.subDespErrores(ref tempRefParam5, ref tempRefParam6); //Descriptivo de respuesta  MMS 11/05 Incremento en la longitud del campo (2 a 3)
						result = true;
					} else
					{
						//UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
						string tempRefParam7 = Strings.Mid(mdlComunica.gvRecive, 45, 52);
						MsgBoxStyle tempRefParam8 = (MsgBoxStyle) (((int) MsgBoxStyle.Exclamation) + ((int) MsgBoxStyle.OkOnly));
						mdlGlobales.subDespErrores(ref tempRefParam7, ref tempRefParam8); //Descriptivo de respuesta  MMS 11/05 Incremento en la longitud del campo (2 a 3)
						result = false;
					}
				}
			} else
			{
				Interaction.Beep();
				//UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
				string tempRefParam9 = Strings.Mid(mdlComunica.gvRecive, 1, 80);
				MsgBoxStyle tempRefParam10 = (MsgBoxStyle) (((int) MsgBoxStyle.Exclamation) + ((int) MsgBoxStyle.OkOnly));
				mdlGlobales.subDespErrores(ref tempRefParam9, ref tempRefParam10);
				result = false;
			}
			return result;
		}
		
		//*******************************************************************************
		//* Finalidad:  Función que se encarga de validar autorización en línea
		//*******************************************************************************
		static public void  subValidaAutorizacion()
		{
			string strCadena = String.Empty;
			MDIMasivos.DefInstance.OleAcceso.Nomina = "";
			MDIMasivos.DefInstance.OleAcceso.NominaRequerida = false;
			bool blnAutorizado = MDIMasivos.DefInstance.OleAcceso.FirmarS041();
			if (! blnAutorizado)
			{
				return;
			}
			//Validar si el usuario cambió de nómina para volverlo a refirmar
			mdlGlobales.gstrNominaAutoriza.Value = mdlGlobales.funPoneCeros(MDIMasivos.DefInstance.OleAcceso.Nomina.Trim(), 10);
			//Procesar
			mdlGlobales.gblnEnvioTansac = false;
			mdlGlobales.gstrProceso.Value = "000"; //MMS 11/05 Incremento en la longitud del campo (2 a 3)
			mdlGlobales.gstrEstatus.Value = "000"; //MMS 11/05 Incremento en la longitud del campo (2 a 3)
			mdlGlobales.gstrIndicaCambio.Value = "0";
			mdlGlobales.gstrPantalla.Value = "00";
			string strDatos = mdlGlobales.gstrNominaAutoriza.Value.Substring(mdlGlobales.gstrNominaAutoriza.Value.Length - Math.Min(mdlGlobales.gstrNominaAutoriza.Value.Length, 8));
			if (funEnviaRecibe5560("5560", "36", strDatos))
			{
				mdlGlobales.subDespMensajes("");
				Cursor.Current = Cursors.WaitCursor;
				if (Strings.Mid(gvRecive5560_36, 45, 2) == "00")
				{ //MMS 11/05 Incremento en la longitud del campo (2 a 3)
					subValidaReFirma();
					mdlGlobales.gblnUsuarioAutorizado = true;
				} else
				{
					mdlGlobales.subDespMensajes("AUTORIZACIÓN NEGADA;TRANSACCIÓN NO EJECUTADA");
					subValidaReFirma();
					mdlGlobales.gblnUsuarioAutorizado = false;
				}
				Cursor.Current = Cursors.Default;
				mdlGlobales.subDespMensajes(" ");
			} else
			{
				subValidaReFirma();
				mdlGlobales.gblnUsuarioAutorizado = false;
				//UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
				string tempRefParam = "TRANSACCION NO EJECUTADA";
				MsgBoxStyle tempRefParam2 = (MsgBoxStyle) (((int) MsgBoxStyle.Exclamation) + ((int) MsgBoxStyle.OkOnly));
				mdlGlobales.subDespErrores(ref tempRefParam, ref tempRefParam2);
			}
		}
		
		//Valida que no haya cambiado el usuario
		static public void  subValidaReFirma()
		{
			if (Conversion.Val(mdlGlobales.gstrNomina.Value) != Conversion.Val(mdlGlobales.gstrNominaAutoriza.Value))
			{
				//UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
				string tempRefParam = "SEGURIDAD: DEBE VOLVER A FIRMARSE PARA CONTINUAR CON EL PROCESO";
				MsgBoxStyle tempRefParam2 = (MsgBoxStyle) (((int) MsgBoxStyle.Information) + ((int) MsgBoxStyle.OkOnly));
				mdlGlobales.subDespErrores(ref tempRefParam, ref tempRefParam2);
				MDIMasivos.DefInstance.OleAcceso.DesFirmarS041();
				mdlGlobales.subDespMensajes("POR FAVOR ESPERE ...");
				mdlGlobales.gblnEstaSeg = false;
				
				while(mdlGlobales.gblnBandCancela || ! mdlGlobales.gblnEstaSeg)
				{
					Cursor.Current = Cursors.Default;
					mdlGlobales.gblnAutorizando = true;
					MDIMasivos.DefInstance.OleAcceso.Nomina = Conversion.Val(mdlGlobales.gstrNomina.Value).ToString();
					MDIMasivos.DefInstance.OleAcceso.NominaRequerida = true;
					MDIMasivos.DefInstance.OleAcceso.FirmarS041();
					mdlGlobales.gblnEstaSeg = MDIMasivos.DefInstance.OleAcceso.EstaSeg;
					if (mdlGlobales.gblnBandCancela || ! mdlGlobales.gblnEstaSeg)
					{
						//UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
						string tempRefParam3 = "REQUIERE FIRMARSE PARA CONTINUAR";
						MsgBoxStyle tempRefParam4 = (MsgBoxStyle) (((int) MsgBoxStyle.Exclamation) + ((int) MsgBoxStyle.OkOnly));
						mdlGlobales.subDespErrores(ref tempRefParam3, ref tempRefParam4);
					}
				};
			}
		}
	}
}