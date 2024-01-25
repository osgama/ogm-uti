using Artinsoft.VB6.Utils; 
using Microsoft.VisualBasic; 
using Microsoft.VisualBasic.Compatibility.VB6; 
using System; 
using System.Drawing; 
using System.Globalization; 
using System.Runtime.InteropServices; 
using System.Text; 
using System.Windows.Forms; 
using VB6 = Microsoft.VisualBasic.Compatibility.VB6.Support; 

namespace Masivos
{
	class mdlPromotora
	{
        //strRegistro = ArraysHelper.InitializeArray<FixedLengthString[]>(new int[]{20}, new object[]{5});
		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
		public struct udtPromTrailer
		{ //TRAILER DE PROMOTORA
            public FixedLengthString strTipoReg;
            public FixedLengthString[] strRegistro;
            public FixedLengthString strEspacios;
            public FixedLengthString strCantSolic;

            public static udtPromTrailer CreateInstance()
            {
                udtPromTrailer result = new udtPromTrailer();
                result.strTipoReg = new FixedLengthString(2);
                result.strEspacios = new FixedLengthString(10);
                result.strCantSolic = new FixedLengthString(5);
                result.strRegistro = ArraysHelper.InitializeArray<FixedLengthString[]>(new int[]{20}, new object[]{5});
                return result;
            }
        }
		static public udtPromTrailer estPromTrailer = udtPromTrailer.CreateInstance();
		static public bool gblnContinuaProceso = false;
		static public bool gbolAvisoDatos = false;
		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
		private struct ArrayPromo
		{
            public FixedLengthString strArrayPromo;

            public static ArrayPromo CreateInstance()
            {
                ArrayPromo result = new ArrayPromo();
                result.strArrayPromo = new FixedLengthString(60);
                return result;
            }
        }
		
		//Arma la cadena de bloque
		static public string funArmaCadena( string strBloq,  int IntIndice)
		{
			StringBuilder result = new StringBuilder();
			result.Append(String.Empty);
			int IntI = 0;
			result = new StringBuilder("");
			
			if (strBloq == "FOLIOS")
			{
				result = new StringBuilder(mdlTranCaptura.estProm01.strFolio.Value); //Regresar el folio
			} else if (strBloq == mdlTranCaptura.gcstrDSSolicitante) { 
				result = new StringBuilder(mdlTranCaptura.strDatSolicitud.strDSEtiqueta.Value + mdlTranCaptura.strDatSolicitud.strDSFecSol.Value + mdlTranCaptura.strDatSolicitud.strDSEdoPromot.Value + 
				         mdlTranCaptura.strDatSolicitud.strDSSucPromot.Value + mdlTranCaptura.strDatSolicitud.strDSCanalPromot.Value + mdlTranCaptura.strDatSolicitud.strDSEmpPromot.Value + 
				         mdlTranCaptura.strDatSolicitud.strDSProdPromot.Value + mdlTranCaptura.strDatSolicitud.strDSRFCAgente.Value + mdlTranCaptura.strDatSolicitud.strDSNominaAgente.Value + mdlTranCaptura.strDatSolicitud.strDSSucursalSolic.Value);
			} else if (strBloq == mdlTranCaptura.gcstrDPSolicitante) { 
				if (mdlTranCaptura.strDatPersonales[IntIndice].strDPEtiqueta.Value != mdlTranCaptura.gcstrDPSolicitante)
				{
					return result.ToString();
				}
				//MMS 11/05 Se agregan los campos País de Origen, Tipo Seguridad Social y Número de Hijos menores de 18 años
                //AEFS se incluye Pais de Nacimiento y FIEL
                //INI*** Se incluye al final de la linea Ent.Fed. Nac - IRP – Proy. 66008-06
                //result = new StringBuilder(mdlTranCaptura.strDatPersonales[IntIndice].strDPEtiqueta.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPClaveParticip.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPTipoRelacion.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPIndicParticip.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPApPaterno.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPApMaterno.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPNombres.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPCalleNum.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPColPob.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPCodPos.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPDelMuni.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPCveEstado.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPLADA.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPTelef.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPExtension.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPCodAreaCEL.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPTelefonoCEL.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPCodAreaFAX.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPTelefonoFAX.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPCodAreaOtro.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPOtroTelef.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPCveNac.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPPais.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPEstCiv.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPFecNac.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPRFC.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPCURP.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPSeguroSocial.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPTmpRes.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPCveSexo.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPCveEsc.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPTpoViv.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPNumDep.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPTpoPers.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPFirma.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPFirmaBuro.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPEmail.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPFecAltaCte.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPParentesco.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPEsCliente.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPLugar.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPIndPartIng.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPPorcAcciones.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPNumClieBnx.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPIngFijosMens.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPOtrosIng.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPEgresos.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPTipoSegSoc.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPHijosMenores.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPPaisNacimiento.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPFIEL.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPEntFedNac.Value);
                //FIN*** Se incluye al final de la linea Ent.Fed. Nac - IRP – Proy. 66008-06
                //MODIF MAP ART.115 2016 SE AGREGAN CAMPOS EN BDP
                result = new StringBuilder(mdlTranCaptura.strDatPersonales[IntIndice].strDPEtiqueta.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPClaveParticip.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPTipoRelacion.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPIndicParticip.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPApPaterno.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPApMaterno.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPNombres.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPCalleNum.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPColPob.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPCodPos.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPDelMuni.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPCveEstado.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPLADA.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPTelef.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPExtension.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPCodAreaCEL.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPTelefonoCEL.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPCodAreaFAX.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPTelefonoFAX.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPCodAreaOtro.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPOtroTelef.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPCveNac.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPPais.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPEstCiv.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPFecNac.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPRFC.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPCURP.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPSeguroSocial.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPTmpRes.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPCveSexo.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPCveEsc.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPTpoViv.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPNumDep.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPTpoPers.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPFirma.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPFirmaBuro.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPEmail.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPFecAltaCte.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPParentesco.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPEsCliente.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPLugar.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPIndPartIng.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPPorcAcciones.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPNumClieBnx.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPIngFijosMens.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPOtrosIng.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPEgresos.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPTipoSegSoc.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPHijosMenores.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPPaisNacimiento.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPFIEL.Value + mdlTranCaptura.strDatPersonales[IntIndice].strDPEntFedNac.Value +
                    // mdlTranCaptura.strDatPersonales[IntIndice].strDPEntFedNac.Value
                    mdlTranCaptura.strDatPersonales[IntIndice].strIdentFis1.Value.PadLeft(20, '0') + mdlTranCaptura.strDatPersonales[IntIndice].strPaisAsig1.Value.PadLeft(4, '0') +
                    mdlTranCaptura.strDatPersonales[IntIndice].strIdentFis2.Value.PadLeft(20, '0') + mdlTranCaptura.strDatPersonales[IntIndice].strPaisAsig2.Value.PadLeft(4, '0') +
                    mdlTranCaptura.strDatPersonales[IntIndice].strFechaCons.Value.PadLeft(8, '0') + mdlTranCaptura.strDatPersonales[IntIndice].strGiroEmpre.Value.PadLeft(6, '0') + mdlTranCaptura.strDatPersonales[IntIndice].strTrabExtra.Value.PadRight(2, ' '));

			
            } else if (strBloq == mdlTranCaptura.gcstrDESolicitante) { 
				if (mdlTranCaptura.strDatEmpleo[IntIndice].strDEEtiqueta.Value != mdlTranCaptura.gcstrDESolicitante)
				{
					return result.ToString();
				}
				result = new StringBuilder(mdlTranCaptura.strDatEmpleo[IntIndice].strDEEtiqueta.Value + mdlTranCaptura.strDatEmpleo[IntIndice].strDEClaveParticip.Value + mdlTranCaptura.strDatEmpleo[IntIndice].strDETipoRelacion.Value + mdlTranCaptura.strDatEmpleo[IntIndice].strDEIndicParticip.Value + mdlTranCaptura.strDatEmpleo[IntIndice].strDECveOcuProf.Value + mdlTranCaptura.strDatEmpleo[IntIndice].strDENomEmp.Value + mdlTranCaptura.strDatEmpleo[IntIndice].strDECalleNum.Value + mdlTranCaptura.strDatEmpleo[IntIndice].strDEColPob.Value + mdlTranCaptura.strDatEmpleo[IntIndice].strDECodPos.Value + mdlTranCaptura.strDatEmpleo[IntIndice].strDEDelMuni.Value + mdlTranCaptura.strDatEmpleo[IntIndice].strDECveEstado.Value + mdlTranCaptura.strDatEmpleo[IntIndice].strDELADA.Value + mdlTranCaptura.strDatEmpleo[IntIndice].strDETelef.Value + mdlTranCaptura.strDatEmpleo[IntIndice].strDEExten.Value + mdlTranCaptura.strDatEmpleo[IntIndice].strDECodAreaFAX.Value + mdlTranCaptura.strDatEmpleo[IntIndice].strDETelefonoFAX.Value + mdlTranCaptura.strDatEmpleo[IntIndice].strDEDepto.Value + mdlTranCaptura.strDatEmpleo[IntIndice].strDEGiroEmp.Value + mdlTranCaptura.strDatEmpleo[IntIndice].strDECveSec.Value + mdlTranCaptura.strDatEmpleo[IntIndice].strDECvePuesto.Value + mdlTranCaptura.strDatEmpleo[IntIndice].strDEProfOficio.Value + mdlTranCaptura.strDatEmpleo[IntIndice].strDEAMAntig.Value + mdlTranCaptura.strDatEmpleo[IntIndice].strDEFecIngreso.Value);
			} else if (strBloq == mdlTranCaptura.gcstrRCSolicitante) { 
				if (mdlTranCaptura.strHdrRCredBancOtros[IntIndice].strRCEtiqueta.Value.Trim() != mdlTranCaptura.gcstrRCSolicitante)
				{
					return result.ToString();
				}
				result = new StringBuilder(mdlTranCaptura.strHdrRCredBancOtros[IntIndice].strRCEtiqueta.Value + mdlTranCaptura.strHdrRCredBancOtros[IntIndice].strRCClaveParticip.Value + mdlTranCaptura.strHdrRCredBancOtros[IntIndice].strRCTipoRelacion.Value + mdlTranCaptura.strHdrRCredBancOtros[IntIndice].strRCIndicParticip.Value + mdlTranCaptura.strHdrRCredBancOtros[IntIndice].strRCCantRef.Value);
                //AIS-xxx gramirez
                int tempForVar = Convert.ToInt32(Conversion.Val(mdlTranCaptura.strHdrRCredBancOtros[IntIndice].strRCCantRef.ToString()));				
				for (IntI = 1; IntI <= tempForVar; IntI++)
				{ //Devolver las ocurrencias
					result.Append(mdlTranCaptura.strRCredBancOtros[IntIndice, IntI].strRCCveProceso.Value + mdlTranCaptura.strRCredBancOtros[IntIndice, IntI].strRCInstitOtorga.Value + mdlTranCaptura.strRCredBancOtros[IntIndice, IntI].strRCTipoCuenta.Value + mdlTranCaptura.strRCredBancOtros[IntIndice, IntI].strRCNumCuenta.Value + mdlTranCaptura.strRCredBancOtros[IntIndice, IntI].strRCSaldo.Value + mdlTranCaptura.strRCredBancOtros[IntIndice, IntI].strRCPagoMensual.Value + mdlTranCaptura.strRCredBancOtros[IntIndice, IntI].strRCFechaApertura.Value);
				}
			} else if (strBloq == mdlTranCaptura.gcstrRPSolicitante) { 
				if (mdlTranCaptura.strRefPersonales[IntIndice].strRPEtiqueta.Value != mdlTranCaptura.gcstrRPSolicitante)
				{
					return result.ToString();
				}
				result = new StringBuilder(mdlTranCaptura.strRefPersonales[IntIndice].strRPEtiqueta.Value + mdlTranCaptura.strRefPersonales[IntIndice].strRPClaveParticip.Value + mdlTranCaptura.strRefPersonales[IntIndice].strRPTipoRelacion.Value + mdlTranCaptura.strRefPersonales[IntIndice].strRPIndicParticip.Value + mdlTranCaptura.strRefPersonales[IntIndice].strRPPaterno.Value + mdlTranCaptura.strRefPersonales[IntIndice].strRPMaterno.Value + mdlTranCaptura.strRefPersonales[IntIndice].strRPNombres.Value + mdlTranCaptura.strRefPersonales[IntIndice].strRPCveParent.Value + mdlTranCaptura.strRefPersonales[IntIndice].strRPCalleNum.Value + 
				         mdlTranCaptura.strRefPersonales[IntIndice].strRPColPob.Value + mdlTranCaptura.strRefPersonales[IntIndice].strRPCodPos.Value + mdlTranCaptura.strRefPersonales[IntIndice].strRPDelMuni.Value + mdlTranCaptura.strRefPersonales[IntIndice].strRPCveEstado.Value + mdlTranCaptura.strRefPersonales[IntIndice].strRPLADA.Value + mdlTranCaptura.strRefPersonales[IntIndice].strRPTelef.Value + 
				         mdlTranCaptura.strRefPersonales[IntIndice].strRPFecNac.Value + mdlTranCaptura.strRefPersonales[IntIndice].strRPCveSexo.Value + mdlTranCaptura.strRefPersonales[IntIndice].strRPFirma.Value);
			} else if (strBloq == mdlTranCaptura.gcstrCOSolicitante) {  //Armar el header
				if (mdlTranCaptura.strHdrComprobantes.strCOEtiqueta.Value != mdlTranCaptura.gcstrCOSolicitante)
				{
					return result.ToString();
				}
				result = new StringBuilder(mdlTranCaptura.strHdrComprobantes.strCOEtiqueta.Value + mdlTranCaptura.strHdrComprobantes.strCOClaveParticip.Value + mdlTranCaptura.strHdrComprobantes.strCOTipoRelacion.Value + mdlTranCaptura.strHdrComprobantes.strCOIndicParticip.Value + mdlTranCaptura.strHdrComprobantes.strCONumComprob.Value);
				//Armar las ocurrencias
				int tempForVar2 = Convert.ToInt32(Conversion.Val(mdlTranCaptura.strHdrComprobantes.strCONumComprob.Value));
				for (IntI = 1; IntI <= tempForVar2; IntI++)
				{
					result.Append(mdlTranCaptura.strComprobantes[IntI].strCOTipoComprob.Value + mdlTranCaptura.strComprobantes[IntI].strCOTipoDocto.Value + mdlTranCaptura.strComprobantes[IntI].strCONumDocto.Value);
				}
			} else if (strBloq == mdlTranCaptura.gcstrPRSolicitante) {  //Armar el header
				if (mdlTranCaptura.strHdrPropiedades.strPREtiqueta.Value != mdlTranCaptura.gcstrPRSolicitante)
				{
					return result.ToString();
				}
				result = new StringBuilder(mdlTranCaptura.strHdrPropiedades.strPREtiqueta.Value + mdlTranCaptura.strHdrPropiedades.strPRClaveParticip.Value + mdlTranCaptura.strHdrPropiedades.strPRTipoRelacion.Value + mdlTranCaptura.strHdrPropiedades.strPRIndicParticip.Value + mdlTranCaptura.strHdrPropiedades.strPRNumPropied.Value);
				int tempForVar3 = Convert.ToInt32(Conversion.Val(mdlTranCaptura.strHdrPropiedades.strPRNumPropied.Value));
				for (IntI = 1; IntI <= tempForVar3; IntI++)
				{
					result.Append(mdlTranCaptura.strPropiedades[IntI].strPRTipoBien.Value + mdlTranCaptura.strPropiedades[IntI].strPRSitPagoBien.Value + mdlTranCaptura.strPropiedades[IntI].strPRFechaCompra.Value + mdlTranCaptura.strPropiedades[IntI].strPRAntiguedad.Value + mdlTranCaptura.strPropiedades[IntI].strPRCveMarca.Value + mdlTranCaptura.strPropiedades[IntI].strPRDescripcion.Value);
				}
			} else if (strBloq == mdlTranCaptura.gcstrCSSolicitante) { 
				if (mdlTranCaptura.strCSolicitado.strCSEtiqueta.Value != mdlTranCaptura.gcstrCSSolicitante)
				{
					return result.ToString();
				}
				result = new StringBuilder(mdlTranCaptura.strCSolicitado.strCSEtiqueta.Value + mdlTranCaptura.strCSolicitado.strCSFamiliaProd.Value + mdlTranCaptura.strCSolicitado.strCSTipoSolicitud.Value + mdlTranCaptura.strCSolicitado.strCSImportancia.Value + mdlTranCaptura.strCSolicitado.strCSDestino.Value + mdlTranCaptura.strCSolicitado.strCSMontoSolic.Value + mdlTranCaptura.strCSolicitado.strCSLineaCredito.Value + mdlTranCaptura.strCSolicitado.strCSTipoBien.Value + mdlTranCaptura.strCSolicitado.strCSPlazo.Value + mdlTranCaptura.strCSolicitado.strCStTasa.Value + mdlTranCaptura.strCSolicitado.strCSOrigenGtia.Value);
			} else if (strBloq == mdlTranCaptura.gcstrIASolicitante) { 
				if (mdlTranCaptura.strIndAdicionales.strIAEtiqueta.Value != mdlTranCaptura.gcstrIASolicitante)
				{
					return result.ToString();
				}
				result = new StringBuilder(mdlTranCaptura.strIndAdicionales.strIAEtiqueta.Value + mdlTranCaptura.strIndAdicionales.strIAFamiliaProd.Value + mdlTranCaptura.strIndAdicionales.strIATipoSolicitud.Value + mdlTranCaptura.strIndAdicionales.strIAOcurrencias.Value);
				for (IntI = 0; IntI <= (Convert.ToInt32(Conversion.Val(mdlTranCaptura.strIndAdicionales.strIAOcurrencias.Value) - 1)); IntI++)
				{
					result.Append(mdlTranCaptura.strIndAdicionales.strIAIndicador[IntI].strCveIndicador.Value + mdlTranCaptura.strIndAdicionales.strIAIndicador[IntI].strValorIndicador.Value);
				}
			}
			return result.ToString();
		}
		
		//Limpia la cadenas de bloques
		static public void  subLimpiaCadena()
		{
			int IntI = 0;
			//Limpiar la cadena de datos de la solicitud (gcstrDSSolicitante)
			mdlTranCaptura.strDatSolicitud.strDSEtiqueta.Value = "";
			mdlTranCaptura.strDatSolicitud.strDSFecSol.Value = "";
			mdlTranCaptura.strDatSolicitud.strDSEdoPromot.Value = "";
			mdlTranCaptura.strDatSolicitud.strDSSucPromot.Value = "";
			mdlTranCaptura.strDatSolicitud.strDSCanalPromot.Value = "";
			mdlTranCaptura.strDatSolicitud.strDSEmpPromot.Value = "";
			mdlTranCaptura.strDatSolicitud.strDSProdPromot.Value = "";
			mdlTranCaptura.strDatSolicitud.strDSRFCAgente.Value = "";
			mdlTranCaptura.strDatSolicitud.strDSNominaAgente.Value = "";
			mdlTranCaptura.strDatSolicitud.strDSSucursalSolic.Value = "";
			//Limpiar la cadena de datos personales (tanto del solicitante como de los demás participantes) (gcstrDPSolicitante)			
			for (IntI = 0; IntI <= mdlTranCaptura.strDatPersonales.GetUpperBound(0); IntI++)
			{
				mdlTranCaptura.strDatPersonales[IntI].strDPEtiqueta.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPClaveParticip.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPTipoRelacion.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPIndicParticip.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPApPaterno.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPApMaterno.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPNombres.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPCalleNum.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPColPob.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPCodPos.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPDelMuni.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPCveEstado.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPLADA.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPTelef.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPExtension.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPCodAreaCEL.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPTelefonoCEL.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPCodAreaFAX.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPTelefonoFAX.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPCodAreaOtro.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPOtroTelef.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPCveNac.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPPais.Value = ""; //MMS 11/05 Se agrega el campo País de Origen
				mdlTranCaptura.strDatPersonales[IntI].strDPEstCiv.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPFecNac.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPRFC.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPCURP.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPSeguroSocial.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPTmpRes.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPCveSexo.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPCveEsc.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPTpoViv.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPNumDep.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPTpoPers.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPFirma.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPFirmaBuro.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPEmail.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPFecAltaCte.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPParentesco.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPEsCliente.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPLugar.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPIndPartIng.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPPorcAcciones.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPNumClieBnx.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPIngFijosMens.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPOtrosIng.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPEgresos.Value = "";
				mdlTranCaptura.strDatPersonales[IntI].strDPTipoSegSoc.Value = ""; //MMS 11/05 Se agrega el campo Tipo Seguridad Social
				mdlTranCaptura.strDatPersonales[IntI].strDPHijosMenores.Value = ""; //MMS 11/05 Se agrega el campo Hijos Menores de 18 años
                mdlTranCaptura.strDatPersonales[IntI].strDPPaisNacimiento.Value = ""; //AEFS Campo Nuevo
                mdlTranCaptura.strDatPersonales[IntI].strDPFIEL.Value = ""; //AEFS Campo Nuevo
                //*** INI - IRP – Proy. 66008-06//Ent.Fed.Nac
                mdlTranCaptura.strDatPersonales[IntI].strDPEntFedNac.Value = ""; //AEFS Campo Nuevo
                //*** FIN - IRP – Proy. 66008-06//Ent.Fed.Nac
            }
			//Limpiar los bloques de datos del empleo (gcstrDESolicitante)			
			for (IntI = 0; IntI <= mdlTranCaptura.strDatEmpleo.GetUpperBound(0); IntI++)
			{
				mdlTranCaptura.strDatEmpleo[IntI].strDEEtiqueta.Value = "";
				mdlTranCaptura.strDatEmpleo[IntI].strDEClaveParticip.Value = "";
				mdlTranCaptura.strDatEmpleo[IntI].strDETipoRelacion.Value = "";
				mdlTranCaptura.strDatEmpleo[IntI].strDEIndicParticip.Value = "";
				mdlTranCaptura.strDatEmpleo[IntI].strDECveOcuProf.Value = "";
				mdlTranCaptura.strDatEmpleo[IntI].strDENomEmp.Value = "";
				mdlTranCaptura.strDatEmpleo[IntI].strDECalleNum.Value = "";
				mdlTranCaptura.strDatEmpleo[IntI].strDEColPob.Value = "";
				mdlTranCaptura.strDatEmpleo[IntI].strDECodPos.Value = "";
				mdlTranCaptura.strDatEmpleo[IntI].strDEDelMuni.Value = "";
				mdlTranCaptura.strDatEmpleo[IntI].strDECveEstado.Value = "";
				mdlTranCaptura.strDatEmpleo[IntI].strDELADA.Value = "";
				mdlTranCaptura.strDatEmpleo[IntI].strDETelef.Value = "";
				mdlTranCaptura.strDatEmpleo[IntI].strDEExten.Value = "";
				mdlTranCaptura.strDatEmpleo[IntI].strDECodAreaFAX.Value = "";
				mdlTranCaptura.strDatEmpleo[IntI].strDETelefonoFAX.Value = "";
				mdlTranCaptura.strDatEmpleo[IntI].strDEDepto.Value = "";
				mdlTranCaptura.strDatEmpleo[IntI].strDEGiroEmp.Value = "";
				mdlTranCaptura.strDatEmpleo[IntI].strDECveSec.Value = "";
				mdlTranCaptura.strDatEmpleo[IntI].strDECvePuesto.Value = "";
				mdlTranCaptura.strDatEmpleo[IntI].strDEProfOficio.Value = "";
				mdlTranCaptura.strDatEmpleo[IntI].strDEAMAntig.Value = "";
				mdlTranCaptura.strDatEmpleo[IntI].strDEFecIngreso.Value = "";
			}
			//Limpiar los bloques de referencias crediticias del solicitante (gcstrRCSolicitante)			
			for (IntI = 0; IntI <= mdlTranCaptura.strHdrRCredBancOtros.GetUpperBound(0); IntI++)
			{
				mdlTranCaptura.strHdrRCredBancOtros[IntI].strRCEtiqueta.Value = "";
				mdlTranCaptura.strHdrRCredBancOtros[IntI].strRCClaveParticip.Value = "";
				mdlTranCaptura.strHdrRCredBancOtros[IntI].strRCTipoRelacion.Value = "";
				mdlTranCaptura.strHdrRCredBancOtros[IntI].strRCIndicParticip.Value = "";
				mdlTranCaptura.strHdrRCredBancOtros[IntI].strRCCantRef.Value = "";
			}
			//Limpiar el detalle
            mdlTranCaptura.strRCredBancOtros = ArraysHelper.InitializeArray<mdlTranCaptura.udfRefCredBancOtros[,]>(new int[]{1, 1});
			//Limpiar los bloques de referencias personales (gcstrRPSolicitante)			
			for (IntI = 0; IntI <= mdlTranCaptura.strRefPersonales.GetUpperBound(0); IntI++)
			{
				mdlTranCaptura.strRefPersonales[IntI].strRPEtiqueta.Value = "";
				mdlTranCaptura.strRefPersonales[IntI].strRPClaveParticip.Value = "";
				mdlTranCaptura.strRefPersonales[IntI].strRPTipoRelacion.Value = "";
				mdlTranCaptura.strRefPersonales[IntI].strRPIndicParticip.Value = "";
				mdlTranCaptura.strRefPersonales[IntI].strRPPaterno.Value = "";
				mdlTranCaptura.strRefPersonales[IntI].strRPMaterno.Value = "";
				mdlTranCaptura.strRefPersonales[IntI].strRPNombres.Value = "";
				mdlTranCaptura.strRefPersonales[IntI].strRPCveParent.Value = "";
				mdlTranCaptura.strRefPersonales[IntI].strRPCalleNum.Value = "";
				mdlTranCaptura.strRefPersonales[IntI].strRPColPob.Value = "";
				mdlTranCaptura.strRefPersonales[IntI].strRPCodPos.Value = "";
				mdlTranCaptura.strRefPersonales[IntI].strRPDelMuni.Value = "";
				mdlTranCaptura.strRefPersonales[IntI].strRPCveEstado.Value = "";
				mdlTranCaptura.strRefPersonales[IntI].strRPLADA.Value = "";
				mdlTranCaptura.strRefPersonales[IntI].strRPTelef.Value = "";
				mdlTranCaptura.strRefPersonales[IntI].strRPFecNac.Value = "";
				mdlTranCaptura.strRefPersonales[IntI].strRPCveSexo.Value = "";
				mdlTranCaptura.strRefPersonales[IntI].strRPFirma.Value = "";
			}
			//Limpiar las referencias de comprobantes (gcstrCOSolicitante) //Armar el header
			mdlTranCaptura.strHdrComprobantes.strCOEtiqueta.Value = "";
			mdlTranCaptura.strHdrComprobantes.strCOClaveParticip.Value = "";
			mdlTranCaptura.strHdrComprobantes.strCOTipoRelacion.Value = "";
			mdlTranCaptura.strHdrComprobantes.strCOIndicParticip.Value = "";
			mdlTranCaptura.strHdrComprobantes.strCONumComprob.Value = "";
			mdlTranCaptura.strComprobantes = ArraysHelper.InitializeArray<mdlTranCaptura.udfComprobantes>(1); //Inicializa el detalle
			//Limpiar el bloque de propiedades (gcstrPRSolicitante) //Armar el header
			mdlTranCaptura.strHdrPropiedades.strPREtiqueta.Value = "";
			mdlTranCaptura.strHdrPropiedades.strPRClaveParticip.Value = "";
			mdlTranCaptura.strHdrPropiedades.strPRTipoRelacion.Value = "";
			mdlTranCaptura.strHdrPropiedades.strPRIndicParticip.Value = "";
			mdlTranCaptura.strHdrPropiedades.strPRNumPropied.Value = "";
			mdlTranCaptura.strPropiedades = ArraysHelper.InitializeArray<mdlTranCaptura.udfPropiedades>(1);
			//Limpiar el bloque de los datos del crédito solicitado (gcstrCSSolicitante)
			mdlTranCaptura.strCSolicitado.strCSEtiqueta.Value = "";
			mdlTranCaptura.strCSolicitado.strCSFamiliaProd.Value = "";
			mdlTranCaptura.strCSolicitado.strCSTipoSolicitud.Value = "";
			mdlTranCaptura.strCSolicitado.strCSImportancia.Value = "";
			mdlTranCaptura.strCSolicitado.strCSDestino.Value = "";
			mdlTranCaptura.strCSolicitado.strCSMontoSolic.Value = "";
			mdlTranCaptura.strCSolicitado.strCSLineaCredito.Value = "";
			mdlTranCaptura.strCSolicitado.strCSTipoBien.Value = "";
			mdlTranCaptura.strCSolicitado.strCSPlazo.Value = "";
			mdlTranCaptura.strCSolicitado.strCStTasa.Value = "";
			mdlTranCaptura.strCSolicitado.strCSOrigenGtia.Value = ""; //MMS Se agrega el campo Origen de la garantia
		}
		
		//Función para validar los registros leídos del archivo Promotora
        static public bool funValidaRegistro()
        {
            int intContReg01 = 0;
            int intContReg02 = 0;
            int intContReg03 = 0;
            int intContReg04 = 0;
            int intContReg05 = 0;
            int intContReg06 = 0;
            int intContReg07 = 0;
            int intContReg08 = 0;
            int intContReg09 = 0;
            int intContReg10 = 0;
            int intContReg11 = 0;
            int intContReg12 = 0;
            int intContReg13 = 0;
            int intContReg14 = 0;
            int intContReg15 = 0;
            int intContReg16 = 0;
            int intContReg17 = 0;
            int intContReg18 = 0;
            int intContReg19 = 0;
            int intExisteDato = 0;
            StringBuilder strMensaje = new StringBuilder();
            strMensaje.Append(String.Empty);
            string strLinea = String.Empty;
            bool blnErrorDatos = false;

            try
            {
                blnErrorDatos = false;

                intContReg01 = 0;
                intContReg02 = 0;
                intContReg03 = 0;
                intContReg04 = 0;
                intContReg05 = 0;
                intContReg06 = 0;
                intContReg07 = 0;
                intContReg08 = 0;
                intContReg09 = 0;
                intContReg10 = 0;
                intContReg11 = 0;
                intContReg12 = 0;
                intContReg13 = 0;
                intContReg14 = 0;
                intContReg15 = 0;
                intContReg16 = 0;
                intContReg17 = 0;
                intContReg18 = 0;
                intContReg19 = 0;

                gblnContinuaProceso = true;
                gbolAvisoDatos = false;
                for (int intConta = 0; intConta <= mdlTranCaptura.gblaBloque.GetUpperBound(0); intConta++)
                {
                    strLinea = mdlTranCaptura.gblaBloque[intConta];
                    if (!funValidaPatron(ref strLinea))
                    {
                        mdlTranMasivo.gstrMsgVal = mdlTranMasivo.gstrMsgVal + "PATRON ERRÓNEO EN LA CADENA: '" + strLinea + "'; ";
                        gblnContinuaProceso = false;
                        blnErrorDatos = true;
                        return false;
                    }
                    mdlTranMasivo.subArmaReg(strLinea);
                    strMensaje = new StringBuilder("");
                    switch ((strLinea.Substring(0, Math.Min(strLinea.Length, 2))))
                    {
                        case "01":  //DATOS PERSONALES 
                            intContReg01++;

                            if (mdlTranCaptura.estProm01.strNombre.Value.Trim().Length == 0)
                            {
                                strMensaje.Append("ERROR R01: NO EXISTE NOMBRE DEL SOLICITANTE; ");
                            }
                            if (mdlTranCaptura.estProm01.strPaterno.Value.Trim().Length == 0)
                            {
                                strMensaje.Append("ERROR R01: NO EXISTE APELLIDO PATERNO DEL SOLICITANTE; ");
                            }
                            if (mdlTranCaptura.estProm01.strDomicilio.Value.Trim().Length == 0)
                            {
                                strMensaje.Append("ERROR R01: NO EXISTE CALLE Y NUMERO DEL SOLICTANTE; ");
                            }

                            if (strMensaje.ToString().Length > 0)
                            {
                                strMensaje.Append("NO SE PUEDE GENERAR LA SOLICITUD; ");
                                blnErrorDatos = true;
                                break;
                            }
                            if (mdlTranCaptura.estProm01.strCodigoPostal.Value.Trim().Length == 0)
                            {
                                strMensaje.Append("AVISO R01: NO EXISTE CP DEL SOLICITANTE; ");
                            }
                            if (mdlTranCaptura.estProm01.strColonia.Value.Trim().Length == 0)
                            {
                                strMensaje.Append("AVISO R01: NO EXISTE COLONIA DEL SOLICITANTE; ");
                            }
                            if (mdlTranCaptura.estProm01.strMunicipio.Value.Trim().Length == 0)
                            {
                                strMensaje.Append("AVISO R01: NO EXISTE CIUDAD O MUNICIPIO DEL SOLICTANTE; ");
                            }
                            if (mdlTranCaptura.estProm01.strEstado.Value.Trim().Length == 0)
                            {
                                strMensaje.Append("AVISO R01: NO EXISTE EL ESTADO DEL SOLICITANTE; ");
                            }
                            if (mdlTranCaptura.estProm01.strFecSolicitud.Value.Length < 6)
                            {
                                strMensaje.Append("AVISO R01: LA LONGITUD DE LA FECHA DE LA SOLICITUD DEBE SER DE 6 DÍGITOS - '" + mdlTranCaptura.estProm01.strFecSolicitud.Value.Trim() + "'; ");
                            }
                            double dbNumericTemp = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm01.strFecSolicitud.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp))
                            {
                                strMensaje.Append("AVISO R01: FECHA DE SOLICITUD NO ES NUMÉRICO - '" + mdlTranCaptura.estProm01.strFecSolicitud.Value.Trim() + "'; ");
                            }
                            double dbNumericTemp2 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm01.strCodigoPostal.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp2))
                            {
                                strMensaje.Append("AVISO R01: CP DEL SOLICITANTE NO ES NUMÉRICO - '" + mdlTranCaptura.estProm01.strCodigoPostal.Value.Trim() + "'; ");
                            }
                            double dbNumericTemp3 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm01.strLada.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp3))
                            {
                                strMensaje.Append("AVISO R01: CLAVE LADA DEL SOLICITANTE NO ES NUMÉRICA - '" + mdlTranCaptura.estProm01.strLada.Value.Trim() + "'; ");
                            }
                            double dbNumericTemp4 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm01.strTelefono.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp4))
                            {
                                strMensaje.Append("AVISO R01: TELÉFONO DEL SOLICITANTE NO ES NUMÉRICO - '" + mdlTranCaptura.estProm01.strTelefono.Value.Trim() + "'; ");
                            }
                            double dbNumericTemp5 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm01.strAnosResidir.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp5))
                            {
                                strMensaje.Append("AVISO R01: AÑOS DE RESIDENCIA DEL SOLICITANTE NO SON NUMÉRICOS - '" + mdlTranCaptura.estProm01.strAnosResidir.Value.Trim() + "'; ");
                            }
                            double dbNumericTemp6 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm01.strNumDependientes.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp6))
                            {
                                strMensaje.Append("AVISO R01: NUM. DE DEPENDIENTES DEL SOLICITANTE NO ES NUMÉRICO - '" + mdlTranCaptura.estProm01.strNumDependientes.Value.Trim() + "'; ");
                            }
                            if (mdlTranCaptura.estProm01.strFecNacimiento.Value.Length < 6)
                            {
                                strMensaje.Append("AVISO R01: LONGITUD DE LA FECHA DE NACIMIENTO DEL SOLICITANTE ES MENOR A 6 DIGITOS - '" + mdlTranCaptura.estProm01.strFecNacimiento.Value.Trim() + "'; ");
                            }
                            double dbNumericTemp7 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm01.strFecNacimiento.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp7))
                            {
                                strMensaje.Append("AVISO R01: FECHA DE NACIMIENTO DEL SOLICITANTE NO ES NUMÉRICA - '" + mdlTranCaptura.estProm01.strFecNacimiento.Value.Trim() + "'; ");
                            }
                            string tempRefParam = "40";
                            string tempRefParam2 = null;
                            string tempRefParam3 = null;
                            string tempRefParam4 = null;
                            Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam5 = Catalogos.clsCatalogos.enmCamposCatalogos.Atributos;
                            string tempRefParam6 = "D";
                            if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam, mdlTranCaptura.estProm01.strSexo.Value, tempRefParam2, tempRefParam3, ref tempRefParam4, ref tempRefParam5, ref tempRefParam6))
                            {
                                strMensaje.Append("AVISO R01: NO EXISTE 'GÉNERO' EN EL CATALOGO ARIES (CAT.40D); ");
                            }
                            string tempRefParam7 = "19";
                            string tempRefParam8 = null;
                            string tempRefParam9 = null;
                            string tempRefParam10 = null;
                            Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam11 = Catalogos.clsCatalogos.enmCamposCatalogos.Atributos;
                            string tempRefParam12 = "E";
                            if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam7, mdlTranCaptura.estProm01.strEstado.Value, tempRefParam8, tempRefParam9, ref tempRefParam10, ref tempRefParam11, ref tempRefParam12))
                            {
                                strMensaje.Append("AVISO R01: NO EXISTE 'ESTADO' EN EL CATALOGO ARIES (CAT.19E); ");
                            }
                            string tempRefParam13 = "28";
                            string tempRefParam14 = null;
                            string tempRefParam15 = null;
                            string tempRefParam16 = null;
                            Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam17 = Catalogos.clsCatalogos.enmCamposCatalogos.Atributos;
                            string tempRefParam18 = "D";
                            if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam13, mdlTranCaptura.estProm01.strTipoVivienda.Value, tempRefParam14, tempRefParam15, ref tempRefParam16, ref tempRefParam17, ref tempRefParam18))
                            {
                                strMensaje.Append("AVISO R01: NO EXISTE 'TIPO DE VIVIENDA' EN EL CATALOGO ARIES (CAT.28D); ");
                            }
                            string tempRefParam19 = "20";
                            string tempRefParam20 = null;
                            string tempRefParam21 = null;
                            string tempRefParam22 = null;
                            Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam23 = Catalogos.clsCatalogos.enmCamposCatalogos.Atributos;
                            string tempRefParam24 = "D";
                            if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam19, mdlTranCaptura.estProm01.strEstadoCivil.Value, tempRefParam20, tempRefParam21, ref tempRefParam22, ref tempRefParam23, ref tempRefParam24))
                            {
                                strMensaje.Append("AVISO R01: NO EXISTE 'ESTADO CIVIL' EN EL CATALOGO ARIES (CAT.20D); ");
                            }
                            string tempRefParam25 = "22";
                            string tempRefParam26 = null;
                            string tempRefParam27 = null;
                            string tempRefParam28 = null;
                            Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam29 = Catalogos.clsCatalogos.enmCamposCatalogos.Default;
                            string tempRefParam30 = "D";
                            if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam25, mdlTranCaptura.estProm01.strEscolaridad.Value, tempRefParam26, tempRefParam27, ref tempRefParam28, ref tempRefParam29, ref tempRefParam30))
                            {
                                strMensaje.Append("AVISO R01: NO EXISTE 'ESCOLARIDAD' EN EL CATALOGO ARIES (CAT.22D); ");
                            }
                            //*** INI - IRP – Proy. 66008-06//irp
                            bool blnPaisNacimiento;
                            bool blnEntidadNacimiento;
                            try
                            {
                                int intIdPN = Convert.ToInt32(mdlTranCaptura.estProm01.strPNacimiento.Value);
                                blnPaisNacimiento = true;
                            }
                            catch
                            {
                                blnPaisNacimiento = false;
                            }

                            try
                            {
                                int intIdEN = Convert.ToInt32(mdlTranCaptura.estProm01.strEntFedNac.Value);
                                blnEntidadNacimiento = true;
                            }
                            catch
                            {
                                blnEntidadNacimiento = false;
                            }

                            if (blnPaisNacimiento == true && blnEntidadNacimiento == true)
                            {
                                string strIdPaisNacimiento = "";
                                string tempRefParam157 = "238";
                                string strPaisNacimiento = "";
                                strIdPaisNacimiento = mdlTranCaptura.estProm01.strPNacimiento.Value;
                                int intExtranjero;
                                intExtranjero = Convert.ToInt32(strIdPaisNacimiento);
                                mdlGlobales.subGetCatDesc(ref  strPaisNacimiento, ref tempRefParam157, ref strIdPaisNacimiento);
                                if (strPaisNacimiento == "")
                                {
                                    strMensaje.Append("AVISO R01 DATO MANDATORIO: NO EXISTE 'PAÍS DE NACIMIENTO.' EN EL CATALOGO ARIES (CAT.238D); ");
                                    //*** INI - IRP – Proy. 66008-06//irp
                                    //-- Se agregan variables de validacion 04/03/2013
                                    //blnErrorDatos = true;         //MOD DCL - Req art 115 no se detiene el proceso por datos mandatorios - 2013/03/22
                                    //gblnContinuaProceso = false;  //MOD DCL - Req art 115 no se detiene el proceso por datos mandatorios - 2013/03/15
                                    //*** FIN - IRP – Proy. 66008-06//irp
                                    //-- Se agregan variables de validacion 04/03/2013


                                }
                                else if (intExtranjero == 1)
                                {
                                    string tempRefParam151 = "19";
                                    string tempRefParam152 = null;
                                    string tempRefParam153 = null;
                                    string tempRefParam154 = null;
                                    Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam155 = Catalogos.clsCatalogos.enmCamposCatalogos.Atributos;
                                    string tempRefParam156 = "E";
                                    if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam151, mdlTranCaptura.estProm01.strEntFedNac.Value, tempRefParam152, tempRefParam153, ref tempRefParam154, ref tempRefParam155, ref tempRefParam156))
                                    {

                                        strMensaje.Append("AVISO R01 DATO MANDATORIO: NO EXISTE 'ENT. FED. NAC.' EN EL CATALOGO ARIES (CAT.19E); ");
                                        //*** INI - IRP – Proy. 66008-06//irp
                                        //-- Se agregan variables de validacion 04/03/2013
                                        //blnErrorDatos = true;             //MOD DCL - Req art 115 no se detiene el proceso por datos mandatorios - 2013/03/22
                                        //gblnContinuaProceso = false;      //MOD DCL - Req art 115 no se detiene el proceso por datos mandatorios - 2013/03/15
                                        //*** FIN - IRP – Proy. 66008-06//irp
                                        //-- Se agregan variables de validacion 04/03/2013

                                    }
                                }
                                else
                                {
                                    if (mdlTranCaptura.estProm01.strEntFedNac.Value != "00")
                                    {
                                        strMensaje.Append("AVISO R01 DATO MANDATORIO: LA 'ENT. FED. NAC.' DEBE TENER FORMATO 00 YA QUE EL PAÍS DE NAC. NO ES MÉXICO; ");
                                        //*** INI - IRP – Proy. 66008-06//irp
                                        //-- Se agregan variables de validacion 04/03/2013
                                        //blnErrorDatos = true;         //MOD DCL - Req art 115 no se detiene el proceso por datos mandatorios - 2013/03/22
                                        //gblnContinuaProceso = false;  //MOD DCL - Req art 115 no se detiene el proceso por datos mandatorios - 2013/03/15                                            
                                        //*** FIN - IRP – Proy. 66008-06//irp
                                        //-- Se agregan variables de validacion 04/03/2013

                                    }

                                }
                                string tempRefParam163 = "238";
                                string strPaisNacionalidad = "";
                                string strIdPaisNacionalidad = mdlTranCaptura.estProm01.strPNacionalidad.Value;
                                mdlGlobales.subGetCatDesc(ref  strPaisNacionalidad, ref tempRefParam163, ref strIdPaisNacionalidad);

                                if (strPaisNacionalidad == "")
                                {
                                    strMensaje.Append("AVISO R01: NO EXISTE 'PAIS DE NACIONALIDAD.' EN EL CATALOGO ARIES (CAT.238D); ");
                                }

                            }
                            else
                            {

                                strMensaje.Append("AVISO R01 DATO MANDATORIO: 'ENT. FED. NAC.' O 'PAÍS DE NACIMIENTO NO NUMERICO' ");
                                //*** INI - IRP – Proy. 66008-06//irp
                                //-- Se agregan variables de validacion 04/03/2013
                                //blnErrorDatos = true;                 //MOD DCL - Req art 115 no se detiene el proceso por datos mandatorios - 2013/03/22
                                //gblnContinuaProceso = false;          //MOD DCL - Req art 115 no se detiene el proceso por datos mandatorios - 2013/03/22
                                //*** FIN - IRP – Proy. 66008-06//irp
                                //-- Se agregan variables de validacion 04/03/2013

                            }
                            //*** FIN - IRP – Proy. 66008-06//irp


                            break;
                        case "02":  //DATOS PERSONALES ADICIONALES 
                            intContReg02++;
                            break;
                        case "03":  //EMPLEO ACTUAL 
                            intContReg03++;

                            double dbNumericTemp8 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm03.strTelefono.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp8))
                            {
                                strMensaje.Append("AVISO R03: TELEFONO NO ES NUMÉRICO - '" + mdlTranCaptura.estProm03.strTelefono.Value.Trim() + "'; ");
                            }

                            double dbNumericTemp9 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm03.strCodigoPostal.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp9))
                            {
                                strMensaje.Append("AVISO R03: CP NO ES NUMÉRICO - '" + mdlTranCaptura.estProm03.strCodigoPostal.Value.Trim() + "'; ");
                            }
                            double dbNumericTemp10 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm03.strLada.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp10))
                            {
                                strMensaje.Append("AVISO R03: LADA NO ES NUMÉRICO - '" + mdlTranCaptura.estProm03.strLada.Value.Trim() + "'; ");
                            }
                            double dbNumericTemp11 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm03.strExtension.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp11))
                            {
                                strMensaje.Append("AVISO R03: EXTENSION NO ES NUMÉRICO - '" + mdlTranCaptura.estProm03.strExtension.Value.Trim() + "'; ");
                            }
                            double dbNumericTemp12 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm03.strAntiguedad.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp12))
                            {
                                strMensaje.Append("AVISO R03: ANTIGUEDAD NO ES NUMÉRICO - '" + mdlTranCaptura.estProm03.strAntiguedad.Value.Trim() + "'; ");
                            }
                            string tempRefParam31 = "23";
                            string tempRefParam32 = mdlTranCaptura.estProm03.strOcupacion.Value.Substring(mdlTranCaptura.estProm03.strOcupacion.Value.Length - Math.Min(mdlTranCaptura.estProm03.strOcupacion.Value.Length, 2));
                            string tempRefParam33 = null;
                            string tempRefParam34 = null;
                            string tempRefParam35 = null;
                            Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam36 = Catalogos.clsCatalogos.enmCamposCatalogos.Default;
                            string tempRefParam37 = "D";
                            if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam31, tempRefParam32, tempRefParam33, tempRefParam34, ref tempRefParam35, ref tempRefParam36, ref tempRefParam37))
                            {
                                strMensaje.Append("AVISO R03: NO EXISTE 'OCUPACIÓN' EN EL CATALOGO ARIES (CAT.23D); ");
                            }
                            string tempRefParam38 = "19";
                            string tempRefParam39 = null;
                            string tempRefParam40 = null;
                            string tempRefParam41 = null;
                            Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam42 = Catalogos.clsCatalogos.enmCamposCatalogos.Atributos;
                            string tempRefParam43 = "E";
                            if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam38, mdlTranCaptura.estProm03.strEstado.Value, tempRefParam39, tempRefParam40, ref tempRefParam41, ref tempRefParam42, ref tempRefParam43))
                            {
                                strMensaje.Append("AVISO R03: NO EXISTE 'ESTADO' EN EL CATÁLOGO ARIES (CAT.19E); ");
                            }
                            break;
                        case "04":  //EMPLEO ANTERIOR 
                            intContReg04++;
                            break;
                        case "05":  //DATOS DEL CONYUGE 
                            intContReg05++;

                            if (mdlTranCaptura.estProm05.strNombre.Value.Trim().Length == 0)
                            {
                                strMensaje.Append("ERROR R05: NO EXISTE EL NOMBRE DEL CÓNYUGE; ");
                            }
                            if (mdlTranCaptura.estProm05.strPaterno.Value.Trim().Length == 0)
                            {
                                strMensaje.Append("ERROR R05: NO EXISTE EL AP. PATERNO DEL CÓNYUGE; ");
                            }
                            if (strMensaje.ToString().Length > 0)
                            {
                                strMensaje.Append("NO EXISTEN DATOS OBLIGATORIOS PARA GENERAR EL BLOQUE 1.1.DATOS PERSONALES DEL CONYUGUE; ");
                            }

                            double dbNumericTemp13 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm05.strTelefono.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp13))
                            {
                                strMensaje.Append("ERROR R05: EL CAMPO TELEFONO NO ES NUMÉRICO - '" + mdlTranCaptura.estProm05.strTelefono.Value.Trim() + "'; ");
                            }
                            if (strMensaje.ToString().Length > 0)
                            {
                                strMensaje.Append("NO EXISTEN DATOS OBLIGATORIOS PARA GENERAR BLOQUE 2.2.DATOS DEL EMPLEO DEL CONYUGE; ");
                            }

                            double dbNumericTemp14 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm05.strCodigoPostal.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp14))
                            {
                                strMensaje.Append("AVISO R05: CP NO ES NUMÉRICO - '" + mdlTranCaptura.estProm05.strCodigoPostal.Value.Trim() + "'; ");
                            }
                            double dbNumericTemp15 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm05.strLada.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp15))
                            {
                                strMensaje.Append("AVISO R05: LADA NO ES NUMÉRICO - '" + mdlTranCaptura.estProm05.strLada.Value.Trim() + "'; ");
                            }
                            double dbNumericTemp16 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm05.strExtension.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp16))
                            {
                                strMensaje.Append("AVISO R05: EXTENSION NO ES NUMÉRICO - '" + mdlTranCaptura.estProm05.strExtension.Value.Trim() + "'; ");
                            }
                            string tempRefParam44 = "19";
                            string tempRefParam45 = null;
                            string tempRefParam46 = null;
                            string tempRefParam47 = null;
                            Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam48 = Catalogos.clsCatalogos.enmCamposCatalogos.Default;
                            string tempRefParam49 = "E";
                            if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam44, mdlTranCaptura.estProm05.strEstado.Value, tempRefParam45, tempRefParam46, ref tempRefParam47, ref tempRefParam48, ref tempRefParam49))
                            {
                                strMensaje.Append("AVISO R05: NO EXISTE 'ESTADO' EN EL CATALOGO ARIES (CAT.19E); ");
                            }
                            break;
                        case "06":  //REFERENCIAS PERSONALES/FAMILIARES 
                            intContReg06++;
                            if ((intContReg06 - 1) <= 1)
                            {
                                if (mdlTranCaptura.estProm06[intContReg06 - 1].strNombre.Value.Trim().Length == 0)
                                {
                                    strMensaje.Append("ERROR R06: NO EXISTE EL NOMBRE DE LA REFERENCIA; ");
                                }
                                if (mdlTranCaptura.estProm06[intContReg06 - 1].strPaterno.Value.Trim().Length == 0)
                                {
                                    strMensaje.Append("ERROR R06: NO EXISTE EL AP. PATERNO DE LA REFERENCIA; ");
                                }
                                string tempRefParam50 = "41";
                                string tempRefParam51 = null;
                                string tempRefParam52 = null;
                                string tempRefParam53 = null;
                                Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam54 = Catalogos.clsCatalogos.enmCamposCatalogos.Atributos;
                                string tempRefParam55 = "D";
                                if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam50, mdlTranCaptura.estProm06[intContReg06 - 1].strParentesco.Value, tempRefParam51, tempRefParam52, ref tempRefParam53, ref tempRefParam54, ref tempRefParam55))
                                {
                                    strMensaje.Append("ERROR R06: NO SE ENCONTRÓ PARENTESCO EN EL CATÁLOGO ARIES (CAT.41D); ");
                                }

                                if (strMensaje.ToString().Length > 0)
                                {
                                    strMensaje.Append("NO EXISTEN LOS DATOS OBLIGATORIOS PARA GENERAR BLOQUE 4-REFERENCIAS PERSONALES; ");
                                }

                                double dbNumericTemp17 = 0;
                                if (!Double.TryParse(mdlTranCaptura.estProm06[intContReg06 - 1].strCodigoPostal.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp17))
                                {
                                    strMensaje.Append("AVISO R06: CP NO ES NUMÉRICO - '" + mdlTranCaptura.estProm06[intContReg06 - 1].strCodigoPostal.Value.Trim() + "'; ");
                                }
                                double dbNumericTemp18 = 0;
                                if (!Double.TryParse(mdlTranCaptura.estProm06[intContReg06 - 1].strLada.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp18))
                                {
                                    strMensaje.Append("AVISO R06: CLAVE LADA NO ES NUMÉRICA - '" + mdlTranCaptura.estProm06[intContReg06 - 1].strLada.Value.Trim() + "'; ");
                                }
                                double dbNumericTemp19 = 0;
                                if (!Double.TryParse(mdlTranCaptura.estProm06[intContReg06 - 1].strTelefono.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp19))
                                {
                                    strMensaje.Append("AVISO R06: TELEFONO NO ES NUMÉRICO - '" + mdlTranCaptura.estProm06[intContReg06 - 1].strTelefono.Value.Trim() + "'; ");
                                }
                                string tempRefParam56 = "19";
                                string tempRefParam57 = null;
                                string tempRefParam58 = null;
                                string tempRefParam59 = null;
                                Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam60 = Catalogos.clsCatalogos.enmCamposCatalogos.Atributos;
                                string tempRefParam61 = "E";
                                if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam56, mdlTranCaptura.estProm06[intContReg06 - 1].strEstado.Value, tempRefParam57, tempRefParam58, ref tempRefParam59, ref tempRefParam60, ref tempRefParam61))
                                {
                                    strMensaje.Append("AVISO R06: NO EXISTE 'ESTADO' EN EL CÁTALOGO ARIES (CAT.19E); ");
                                }
                            }
                            break;
                        case "07":  //REFERENCIAS BANAMEX 
                            intContReg07++;

                            intExisteDato = 0;
                            double dbNumericTemp20 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm07.strSucursal.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp20) && mdlTranCaptura.estProm07.strSucursal.Value.Trim().Length == 0)
                            {
                                strMensaje.Append("ERROR R07: CAMPO SUCURSAL DEBE SER NUMÉRICA Y MAYOR A CERO - '" + mdlTranCaptura.estProm07.strSucursal.Value.Trim() + "'; ");
                                intExisteDato++;
                            }
                            double dbNumericTemp21 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm07.strNumCuenta.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp21) && mdlTranCaptura.estProm07.strNumCuenta.Value.Trim().Length == 0)
                            {
                                strMensaje.Append("ERROR R07: CAMPO NUM. CUENTA DEBE SER NUMÉRICO Y MAYOR A CERO - '" + mdlTranCaptura.estProm07.strNumCuenta.Value.Trim() + "'; ");
                                intExisteDato++;
                            }

                            if (intExisteDato == 2)
                            {
                                strMensaje.Append("NO EXISTEN LOS DATOS PARA GENERAR LOS BLOQUES 3.0.REFERENCIAS BANAMEX DEL SOLICITANTE; ");
                            }

                            string tempRefParam62 = "51";
                            string tempRefParam63 = null;
                            string tempRefParam64 = null;
                            string tempRefParam65 = null;
                            Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam66 = Catalogos.clsCatalogos.enmCamposCatalogos.Default;
                            string tempRefParam67 = "D";
                            if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam62, mdlTranCaptura.estProm07.strTipoServicio.Value, tempRefParam63, tempRefParam64, ref tempRefParam65, ref tempRefParam66, ref tempRefParam67))
                            {
                                strMensaje.Append("AVISO R07: NO EXISTE 'TIPO SERVICIO' EN EL CATALOGO ARIES (CAT.51D); ");
                            }
                            break;
                        case "08":  //REFENCIAS COMERCIALES/BANCARIAS/CASA DE BOLSA 
                            intContReg08++;
                            intExisteDato = 0;
                            //OML 18/JUNIO/2004 IF PARA MANEJO DE + DE UN REG.08 E INDICES 
                            if ((intContReg08 - 1) <= 1)
                            {
                                string tempRefParam68 = "43";
                                string tempRefParam69 = null;
                                string tempRefParam70 = null;
                                string tempRefParam71 = null;
                                Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam72 = Catalogos.clsCatalogos.enmCamposCatalogos.Default;
                                string tempRefParam73 = "D";
                                if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam68, mdlTranCaptura.estProm08[intContReg08 - 1].strEmisor.Value, tempRefParam69, tempRefParam70, ref tempRefParam71, ref tempRefParam72, ref tempRefParam73) || mdlTranCaptura.estProm08[intContReg08 - 1].strEmisor.Value.Trim().Length == 0)
                                {
                                    strMensaje.Append("ERROR R08: NO EXISTE EMISOR EN EL CATÁLOGO ARIES (CAT.43D); ");
                                    intExisteDato++;
                                }
                                double dbNumericTemp22 = 0;
                                if (!Double.TryParse(mdlTranCaptura.estProm08[intContReg08 - 1].strNumCuenta.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp22) || mdlTranCaptura.estProm08[intContReg08 - 1].strNumCuenta.Value.Trim().Length == 0)
                                {
                                    strMensaje.Append("ERROR R08: EL CAMPO NUM. CUENTA DEBE SER NUMÉRICO Y MAYOR QUE CERO - '" + mdlTranCaptura.estProm08[intContReg08 - 1].strNumCuenta.Value.Trim() + "'; ");
                                    intExisteDato++;
                                }

                                if (intExisteDato == 2)
                                {
                                    strMensaje.Append("NO EXISTEN DATOS PARA GENERAR EL BLOQUE 3.0 REFERENCIAS BANAMEX DEL SOLICITENTE; ");
                                }
                                string tempRefParam74 = "51";
                                string tempRefParam75 = null;
                                string tempRefParam76 = null;
                                string tempRefParam77 = null;
                                Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam78 = Catalogos.clsCatalogos.enmCamposCatalogos.Default;
                                string tempRefParam79 = "D";
                                if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam74, mdlTranCaptura.estProm08[intContReg08 - 1].strTipoServicio.Value, tempRefParam75, tempRefParam76, ref tempRefParam77, ref tempRefParam78, ref tempRefParam79))
                                {
                                    strMensaje.Append("AVISO R08: NO EXISTE 'TIPO DE SERVICIO' EN EL CATÁLOGO ARIES (CAT.51D); ");
                                }
                            }
                            break;
                        case "09":  //TARJETAS DE CREDITO 
                            intContReg09++;
                            intExisteDato = 0;
                            //OML 18/JUNIO/2004 IF PARA MANEJO DE + DE UN REG.08 E INDICES 
                            if ((intContReg09 - 1) <= 1)
                            {
                                string tempRefParam80 = "43";
                                string tempRefParam81 = null;
                                string tempRefParam82 = null;
                                string tempRefParam83 = null;
                                Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam84 = Catalogos.clsCatalogos.enmCamposCatalogos.Default;
                                string tempRefParam85 = "D";
                                if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam80, mdlTranCaptura.estProm09[intContReg09 - 1].strEmisor.Value, tempRefParam81, tempRefParam82, ref tempRefParam83, ref tempRefParam84, ref tempRefParam85) && mdlTranCaptura.estProm09[intContReg09 - 1].strNumCuenta.Value.Trim().Length == 0)
                                {
                                    strMensaje.Append("ERROR R09: NO EXISTE 'EMISOR' EN EL CATÁLOGO ARIES (CAT.43D)");
                                    intExisteDato++;
                                }
                                double dbNumericTemp23 = 0;
                                if (!Double.TryParse(mdlTranCaptura.estProm09[intContReg09 - 1].strNumCuenta.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp23) && mdlTranCaptura.estProm09[intContReg09 - 1].strNumCuenta.Value.Trim().Length == 0)
                                {
                                    strMensaje.Append("ERROR R09: NUM. CUENTA DEBE SER NUMÉRICO Y MAYOR A CERO; ");
                                    intExisteDato++;
                                }
                                if (intExisteDato == 2)
                                {
                                    strMensaje.Append("NO EXISTEN DATOS PARA GENERAR EL BLOQUE 3.0.TARJETAS DE CREDITO DEL SOLICITENTE / ");
                                }
                            }
                            break;
                        case "10":  //PROPIEDADES 
                            intContReg10++;

                            strMensaje = new StringBuilder("");
                            if (mdlTranCaptura.estProm10.strTipoPropiedad.Value.Trim().Length < 1)
                            {
                                strMensaje.Append("ERROR R10: NO EXISTE EL CAMPO PROPIEDAD; ");
                            }
                            if (strMensaje.ToString().Length > 0)
                            {
                                strMensaje.Append("NO EXISTEN DATOS PARA GENERAR EL BLOQUE 7.DATOS DE PROPIEDADES; ");
                            }
                            string tempRefParam86 = "63";
                            string tempRefParam87 = null;
                            string tempRefParam88 = null;
                            string tempRefParam89 = null;
                            Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam90 = Catalogos.clsCatalogos.enmCamposCatalogos.Default;
                            string tempRefParam91 = "D";
                            if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam86, mdlTranCaptura.estProm10.strTipoPropiedad.Value, tempRefParam87, tempRefParam88, ref tempRefParam89, ref tempRefParam90, ref tempRefParam91))
                            {
                                strMensaje.Append("AVISO R10: NO EXISTE 'PROPIEDAD' EN EL CATÁLOGO ARIES (CAT.63D); ");
                            }
                            string tempRefParam92 = "68";
                            string tempRefParam93 = null;
                            string tempRefParam94 = null;
                            string tempRefParam95 = null;
                            Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam96 = Catalogos.clsCatalogos.enmCamposCatalogos.Default;
                            string tempRefParam97 = "D";
                            if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam92, mdlTranCaptura.estProm10.strMarca1.Value, tempRefParam93, tempRefParam94, ref tempRefParam95, ref tempRefParam96, ref tempRefParam97))
                            {
                                strMensaje.Append("AVISO R10: NO EXISTE 'MARCA1' EN EL CATÁLOGO ARIES (CAT.68D); ");
                            }
                            break;
                        case "11":
                            intContReg11++;

                            double dbNumericTemp24 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm11.strIngfijos.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp24))
                            {
                                mdlTranCaptura.estProm11.strIngfijos.Value = "0";
                            }
                            double dbNumericTemp25 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm11.strIngComisiones.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp25))
                            {
                                mdlTranCaptura.estProm11.strIngComisiones.Value = "0";
                            }
                            double dbNumericTemp26 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm11.strIngConyugue.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp26))
                            {
                                mdlTranCaptura.estProm11.strIngConyugue.Value = "0";
                            }
                            double dbNumericTemp27 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm11.strIngInversiones.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp27))
                            {
                                mdlTranCaptura.estProm11.strIngInversiones.Value = "0";
                            }
                            double dbNumericTemp28 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm11.strIngHonorarios.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp28))
                            {
                                mdlTranCaptura.estProm11.strIngHonorarios.Value = "0";
                            }
                            double dbNumericTemp29 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm11.strIngOtros.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp29))
                            {
                                mdlTranCaptura.estProm11.strIngOtros.Value = "0";
                            }
                            double dbNumericTemp30 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm11.strEgrGastoFam.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp30))
                            {
                                mdlTranCaptura.estProm11.strEgrGastoFam.Value = "0";
                            }
                            double dbNumericTemp31 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm11.strEgrPagoAdeudo.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp31))
                            {
                                mdlTranCaptura.estProm11.strEgrPagoAdeudo.Value = "0";
                            }
                            double dbNumericTemp32 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm11.strEgrOtros.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp32))
                            {
                                mdlTranCaptura.estProm11.strEgrOtros.Value = "0";
                            }
                            break;
                        case "12":
                            intContReg12++;

                            strMensaje = new StringBuilder("");
                            if (mdlTranCaptura.estProm12.strNombre.Value.Trim().Length == 0)
                            {
                                strMensaje.Append("ERROR R12: NO EXISTE EL NOMBRE DEL ADICIONAL; ");
                            }
                            if (mdlTranCaptura.estProm12.strPaterno.Value.Trim().Length == 0)
                            {
                                strMensaje.Append("ERROR R12: NO EXISTE EL AP. PATERNO DEL ADICIONAL; ");
                            }
                            if (strMensaje.ToString().Length > 0)
                            {
                                strMensaje.Append("NO EXISTEN LOS DATOS OBLIGATORIOS PARA GENERAR BLOQUE 4.1.REFERENCIAS PERSONALES(TARJETAS ADCICIONALES); ");
                            }

                            string tempRefParam98 = "41";
                            string tempRefParam99 = null;
                            string tempRefParam100 = null;
                            string tempRefParam101 = null;
                            Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam102 = Catalogos.clsCatalogos.enmCamposCatalogos.Atributos;
                            string tempRefParam103 = "D";
                            if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam98, mdlTranCaptura.estProm12.strParentesco.Value, tempRefParam99, tempRefParam100, ref tempRefParam101, ref tempRefParam102, ref tempRefParam103))
                            {
                                strMensaje.Append("AVISO R12: NO EXISTE 'PARENTESCO' EN EL CATALOGO ARIES (CAT.41D); ");
                            }
                            double dbNumericTemp33 = 0;
                            if (mdlTranCaptura.estProm12.strFecNacimiento.Value.Length < 6 || !Double.TryParse(mdlTranCaptura.estProm12.strFecNacimiento.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp33))
                            {
                                strMensaje.Append("AVISO R12: FECHA DE NACIMIENTO DEL ADICIONAL DEBE SER NUMÉRICA E IGUAL A 6 DÍGITOS - '" + mdlTranCaptura.estProm12.strFecNacimiento.Value.Trim() + "'; ");
                            }
                            string tempRefParam104 = "40";
                            string tempRefParam105 = null;
                            string tempRefParam106 = null;
                            string tempRefParam107 = null;
                            Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam108 = Catalogos.clsCatalogos.enmCamposCatalogos.Atributos;
                            string tempRefParam109 = "D";
                            if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam104, mdlTranCaptura.estProm12.strSexo.Value, tempRefParam105, tempRefParam106, ref tempRefParam107, ref tempRefParam108, ref tempRefParam109))
                            {
                                strMensaje.Append("AVISO R12: NO EXISTE 'GÉNERO' EN EL CATALOGO ARIES (CAT.40D); ");
                            }

                            break;
                        case "13":  //PAGO INCENTIVOS 
                            intContReg13++;
                            if (Strings.Mid(mdlTranCaptura.estProm13.strFolio.Value, 3, 2) == "51")
                            {
                                mdlTranCaptura.estProm13.strSucursal.Value = mdlTranCaptura.estProm13.strSucursal.Value;
                            }
                            mdlTranCaptura.estProm13.strNomEjecutivo.Value = mdlTranCaptura.estProm13.strNomEjecutivo.Value;
                            break;
                        case "14":  //OBLIGADO SOLIDARIO 
                            intContReg14++;

                            if (mdlTranCaptura.estProm14.strNombre.Value.Trim().Length == 0)
                            {
                                strMensaje.Append("ERROR R14: NO EXISTE NOMBRE DEL OBLIGADO; ");
                            }
                            if (mdlTranCaptura.estProm14.strPaterno.Value.Trim().Length == 0)
                            {
                                strMensaje.Append("ERROR R14: NO EXISTE AP. PATERNO DEL OBLIGADO; ");
                            }
                            if (mdlTranCaptura.estProm14.strDomicilio.Value.Trim().Length == 0)
                            {
                                strMensaje.Append("ERROR R14: NO EXISTE DOMICILIO DEL OBLIGADO; ");
                            }
                            if (strMensaje.ToString().Length > 0)
                            {
                                strMensaje.Append("NO EXISTEN LOS DATOS OBLIGATORIOS PARA GENERAR LOS BLOQUES 1.2.DATOS PERSONALES DEL OBLIGADO Y 2.1.DATOS DEL EMPLEO DEL OBLIGADO; ");
                            }

                            if (mdlTranCaptura.estProm14.strCodigoPostal.Value.Trim().Length == 0)
                            {
                                strMensaje.Append("ERROR R14: NO EXISTE CP DEL OBLIGADO; ");
                            }
                            if (mdlTranCaptura.estProm14.strColonia.Value.Trim().Length == 0)
                            {
                                strMensaje.Append("ERROR R14: NO EXISTE COLONIA DEL OBLIGADO; ");
                            }
                            if (mdlTranCaptura.estProm14.strMunicipio.Value.Trim().Length == 0)
                            {
                                strMensaje.Append("ERROR R14: NO EXISTE CIUDAD O MUNICIPIO DEL OBLIGADO; ");
                            }
                            if (mdlTranCaptura.estProm14.strEstado.Value.Trim().Length == 0)
                            {
                                strMensaje.Append("ERROR R14: NO EXISTE CVE. DE ESTADO DEL OBLIGADO; ");
                            }
                            double dbNumericTemp34 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm14.strCodigoPostal.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp34))
                            {
                                strMensaje.Append("ERROR R14: EL CP NO ES NUMÉRICO; ");
                            }
                            string tempRefParam110 = "19";
                            string tempRefParam111 = null;
                            string tempRefParam112 = null;
                            string tempRefParam113 = null;
                            Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam114 = Catalogos.clsCatalogos.enmCamposCatalogos.Atributos;
                            string tempRefParam115 = "E";
                            if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam110, mdlTranCaptura.estProm14.strEstado.Value, tempRefParam111, tempRefParam112, ref tempRefParam113, ref tempRefParam114, ref tempRefParam115))
                            {
                                strMensaje.Append("ERROR R14: NO EXISTE EL CAMPO 'ESTADO' EN EL CATALOGO ARIES (CAT.19E); ");
                            }

                            double dbNumericTemp35 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm14.strTelefono.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp35))
                            {
                                strMensaje.Append("ERROR R14: TELEFONO DEL OBLIGADO NO ES NUMÉRICO - '" + mdlTranCaptura.estProm14.strTelefono.Value.Trim() + "'; ");
                            }
                            if (strMensaje.ToString().Length > 0)
                            {
                                strMensaje.Append("NO EXISTEN DATOS OBLIGATORIOS PARA GENERAR EL BLOQUE 2.1.DATOS DEL EMPLEO DEL OBLIGADO; ");
                                break;
                            }

                            double dbNumericTemp36 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm14.strLada.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp36))
                            {
                                strMensaje.Append("AVISO R14: LADA NO ES NUMÉRICA - '" + mdlTranCaptura.estProm14.strTelefono.Value.Trim() + "'; ");
                            }
                            double dbNumericTemp37 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm14.strExtension.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp37))
                            {
                                strMensaje.Append("AVISO R14: EXTENSION NO ES NUMÉRICA - '" + mdlTranCaptura.estProm14.strExtension.Value.Trim() + "'; ");
                            }
                            double dbNumericTemp38 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm14.strAntiguedad.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp38))
                            {
                                strMensaje.Append("AVISO R14: ANTIGÜEDAD NO ES NUMÉRICA - '" + mdlTranCaptura.estProm14.strAntiguedad.Value.Trim() + "'; ");
                            }
                            double dbNumericTemp39 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm14.strIngMensuales.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp39))
                            {
                                strMensaje.Append("AVISO R14: INGRESOS MENSUALES NO SON NUMÉRICOS - '" + mdlTranCaptura.estProm14.strIngMensuales.Value.Trim() + "'; ");
                            }
                            string tempRefParam116 = "23";
                            string tempRefParam117 = mdlTranCaptura.estProm14.strOcupacion.Value.Substring(mdlTranCaptura.estProm14.strOcupacion.Value.Length - Math.Min(mdlTranCaptura.estProm14.strOcupacion.Value.Length, 2));
                            string tempRefParam118 = null;
                            string tempRefParam119 = null;
                            string tempRefParam120 = null;
                            Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam121 = Catalogos.clsCatalogos.enmCamposCatalogos.Default;
                            string tempRefParam122 = "D";
                            if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam116, tempRefParam117, tempRefParam118, tempRefParam119, ref tempRefParam120, ref tempRefParam121, ref tempRefParam122))
                            {
                                strMensaje.Append("AVISO R14: NO EXISTE 'PARENTESCO' EN EL CATALOGO ARIES (CAT.23D); ");
                            }
                            break;
                        case "15":
                            intContReg15++;

                            intExisteDato = 0;
                            double dbNumericTemp40 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm15.strCompIdentificacion.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp40) && mdlTranCaptura.estProm15.strCompIdentificacion.Value.Trim().Length == 0 && mdlTranCaptura.estProm15.strCompIdentificacion.Value != "N" && mdlTranCaptura.estProm15.strCompIdentificacion.Value != "S")
                            {
                                strMensaje.Append("ERROR R15: NO EXISTE EL COMP. DE IDENTIFICACION; ");
                            }
                            double dbNumericTemp41 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm15.strCompDomicilio.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp41) && mdlTranCaptura.estProm15.strCompDomicilio.Value.Trim().Length == 0 && mdlTranCaptura.estProm15.strCompDomicilio.Value != "N" && mdlTranCaptura.estProm15.strCompDomicilio.Value != "S")
                            {
                                strMensaje.Append("ERROR R15: NO EXISTE COMP. DE DOMICILIO; ");
                            }
                            double dbNumericTemp42 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm15.strCompIdentificacion.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp42) && mdlTranCaptura.estProm15.strCompIngresos.Value.Trim().Length == 0 && mdlTranCaptura.estProm15.strCompIngresos.Value != "N" && mdlTranCaptura.estProm15.strCompIngresos.Value != "S")
                            {
                                strMensaje.Append("ERROR R15: NO EXISTE COMP. DE INGRESOS; ");
                            }
                            if (mdlTranCaptura.estProm15.strDescIdentificacion.Value.Trim().Length == 0)
                            {
                                strMensaje.Append("ERROR R15: NO EXISTE DESC. DE IDENTIFICACION; ");
                            }

                            if (strMensaje.ToString().Length > 0)
                            {
                                strMensaje.Append("NO EXISTEN LOS DATOS OBLIGATORIOS PARA GENERAR LOS BLOQUES 5.COMPROBANTES; ");
                            }

                            break;
                        case "16":
                            intContReg16++;

                            string tempRefParam123 = "39";
                            string tempRefParam124 = null;
                            string tempRefParam125 = null;
                            string tempRefParam126 = null;
                            Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam127 = Catalogos.clsCatalogos.enmCamposCatalogos.Default;
                            string tempRefParam128 = "D";
                            if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam123, mdlTranCaptura.estProm16.strNacionalidad.Value, tempRefParam124, tempRefParam125, ref tempRefParam126, ref tempRefParam127, ref tempRefParam128))
                            {
                                strMensaje.Append("AVISO R16: NO EXISTE 'NACIONALIDAD' EN EL CATALOGO ARIES (CAT.39D); ");
                            }
                            string tempRefParam129 = "52";
                            string tempRefParam130 = null;
                            string tempRefParam131 = null;
                            string tempRefParam132 = null;
                            Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam133 = Catalogos.clsCatalogos.enmCamposCatalogos.Default;
                            string tempRefParam134 = "D";
                            if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam129, mdlTranCaptura.estProm16.strSector.Value, tempRefParam130, tempRefParam131, ref tempRefParam132, ref tempRefParam133, ref tempRefParam134))
                            {
                                strMensaje.Append("AVISO R16: NO EXISTE 'SECTOR' EN EL CATALOGO ARIES (CAT.52D); ");
                            }
                            string tempRefParam135 = "62";
                            string tempRefParam136 = null;
                            string tempRefParam137 = null;
                            string tempRefParam138 = null;
                            Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam139 = Catalogos.clsCatalogos.enmCamposCatalogos.Default;
                            string tempRefParam140 = "D";
                            if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam135, mdlTranCaptura.estProm16.strFtesOtrosIng.Value, tempRefParam136, tempRefParam137, ref tempRefParam138, ref tempRefParam139, ref tempRefParam140))
                            {
                                strMensaje.Append("AVISO R16: NO EXISTE 'FUENTES DE OTROS INGRESOS' EN EL CATALOGO ARIES (CAT.62D); ");
                            }
                            double dbNumericTemp43 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm16.strFtesOtrosIng.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp43))
                            {
                                strMensaje.Append("AVISO R16: FTES. DE OTROS INGRESOS NO ES NUMÉRICO - '" + mdlTranCaptura.estProm16.strFtesOtrosIng.Value.Trim() + "'; ");
                            }
                            double dbNumericTemp44 = 0;
                            if (mdlTranCaptura.estProm16.strFecNacimiento.Value.Length < 6 || !Double.TryParse(mdlTranCaptura.estProm16.strFecNacimiento.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp44))
                            {
                                strMensaje.Append("AVISO R16: FECHA DE NACIMIENTO DEBE SER NUMÉRICO Y DE LONGITUD 6 - '" + mdlTranCaptura.estProm16.strFecNacimiento.Value.Trim() + "'; ");
                            }

                            break;
                        case "17":
                            intContReg17++;

                            intExisteDato = 0;
                            if (mdlTranCaptura.estProm17.strReferencia.Value == "S")
                            {
                                double dbNumericTemp45 = 0;
                                if (mdlTranCaptura.estProm17.strSucursal.Value.Trim().Length == 0 && !Double.TryParse(mdlTranCaptura.estProm17.strSucursal.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp45))
                                {
                                    strMensaje.Append("ERROR R17: SUCURSAL NO NUMÉRICA - '" + mdlTranCaptura.estProm17.strSucursal.Value.Trim() + "'; ");
                                    intExisteDato++;
                                }
                                double dbNumericTemp46 = 0;
                                if (mdlTranCaptura.estProm17.strNumCuenta.Value.Trim().Length == 0 && !Double.TryParse(mdlTranCaptura.estProm17.strNumCuenta.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp46))
                                {
                                    strMensaje.Append("ERROR R17: NUM. DE CUENTA NO NUMÉRICA - '" + mdlTranCaptura.estProm17.strNumCuenta.Value.Trim() + "'; ");
                                    intExisteDato++;
                                }
                                if (intExisteDato == 2)
                                {
                                    strMensaje.Append("NO EXISTEN LOS DATOS PARA GENERAR EL BLOQUE 3.1 REFERENCIAS CREDITICIAS, BANCARIAS Y OTROS CRÉDITOS. (OBLIGADO SOLIDARIO); ");
                                }

                                intExisteDato = 0;
                                double dbNumericTemp47 = 0;
                                if (mdlTranCaptura.estProm17.strEmisorTarjeta1.Value.Trim().Length == 0 && !Double.TryParse(mdlTranCaptura.estProm17.strEmisorTarjeta1.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp47))
                                {
                                    strMensaje.Append("ERROR R17: EMISOR TARJETA 1 NO NUMÉRICO - '" + mdlTranCaptura.estProm17.strEmisorTarjeta1.Value.Trim() + "'; ");
                                    intExisteDato++;
                                }
                                double dbNumericTemp48 = 0;
                                if (mdlTranCaptura.estProm17.strEmisorTarjeta2.Value.Trim().Length == 0 && !Double.TryParse(mdlTranCaptura.estProm17.strEmisorTarjeta2.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp48))
                                {
                                    strMensaje.Append("ERROR R17: EMISOR TARJETA 2 NO NUMÉRICO - '" + mdlTranCaptura.estProm17.strEmisorTarjeta2.Value.Trim() + "'; ");
                                    intExisteDato++;
                                }
                                if (intExisteDato == 2)
                                {
                                    strMensaje.Append("NO EXISTEN LOS DATOS PARA GENERAR EL BLOQUE 3.1 REFERENCIAS CREDITICIAS, BANCARIAS Y OTROS CRÉDITOS. (OBLIGADO SOLIDARIO); ");
                                }

                                double dbNumericTemp49 = 0;
                                if (mdlTranCaptura.estProm17.strNumTarjeta2.Value.Trim().Length == 0 && !Double.TryParse(mdlTranCaptura.estProm17.strNumTarjeta2.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp49))
                                {
                                    strMensaje.Append("AVISO R17: EMISOR TARJETA 2 NO NUMÉRICO - '" + mdlTranCaptura.estProm17.strNumTarjeta2.Value.Trim() + "'; ");
                                    intExisteDato++;
                                }
                                if (mdlTranCaptura.estProm17.strOtrosIngresos.Value.Trim().Length == 0)
                                {
                                    strMensaje.Append("AVISO R17: NO EXISTE OTROS INGRESOS; ");
                                }
                                double dbNumericTemp50 = 0;
                                if (!Double.TryParse(mdlTranCaptura.estProm17.strOtrosIngresos.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp50))
                                {
                                    mdlTranCaptura.estProm11.strIngInversiones.Value = "0";
                                    strMensaje.Append("AVISO R17: OTROS INGRESOS NO NUMÉRICO - '" + mdlTranCaptura.estProm17.strOtrosIngresos.Value.Trim() + "'; ");
                                }
                                if (mdlTranCaptura.estProm16.strFecNacimiento.Value.Length < 6)
                                {
                                    strMensaje.Append("AVISO R17: FORMATO INCORRECTO EN LA FECHA DE NACIMIENTO - '" + mdlTranCaptura.estProm16.strFecNacimiento.Value.Trim() + "'; ");
                                }
                                double dbNumericTemp51 = 0;
                                if (!Double.TryParse(mdlTranCaptura.estProm17.strFecNacimiento.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp51))
                                {
                                    mdlTranCaptura.estProm11.strIngInversiones.Value = "0";
                                    strMensaje.Append("AVISO R17: FECHA DE NACIMIENTO NO NUMÉRICO - '" + mdlTranCaptura.estProm17.strFecNacimiento.Value.Trim() + "'; ");
                                }
                            }

                            break;
                        case "18":
                            intContReg18++;

                            strMensaje = new StringBuilder("");
                            if (mdlTranCaptura.estProm18.strProducto.Value.Trim().Length == 0)
                            {
                                strMensaje.Append("ERROR R18: NO EXISTE EL CAMPO PRODUCTO; ");
                            }
                            double dbNumericTemp52 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm18.strProducto.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp52))
                            {
                                mdlTranCaptura.estProm11.strIngInversiones.Value = "0";
                                strMensaje.Append("ERROR R18: CAMPO PRODUCTO NO ES NUMÉRICO - '" + mdlTranCaptura.estProm18.strProducto.Value.Trim() + "'; ");
                            }
                            if (strMensaje.ToString().Length > 0)
                            {
                                strMensaje.Append("NO EXISTEN LOS DATOS PARA GENERAR LOS BLOQUES 8.CRÉDITO SOLICITADO; ");
                                blnErrorDatos = true;
                                break;
                            }

                            double dbNumericTemp53 = 0;
                            if (Double.TryParse(Strings.Mid(mdlTranCaptura.estProm18.strRFCPromotor.Value, 1, 4), NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp53))
                            {
                                strMensaje.Append("AVISO R18: PARTE NO NUMÉRICA DEL RFC DEL PROMOTOR ES INCORRECTA - '" + mdlTranCaptura.estProm18.strRFCPromotor.Value.Trim() + "'; ");
                            }
                            double dbNumericTemp54 = 0;
                            if (!Double.TryParse(Strings.Mid(mdlTranCaptura.estProm18.strRFCPromotor.Value, 5, 6), NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp54))
                            {
                                strMensaje.Append("AVISO R18: PARTE NUMÉRICA DEL RFC DEL PROMOTOR ES INCORRECTA - '" + mdlTranCaptura.estProm18.strRFCPromotor.Value.Trim() + "'; ");
                            }

                            double dbNumericTemp55 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm18.strEstado.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp55))
                            {
                                strMensaje.Append("AVISO R18: ESTADO NO ES NUMÉRICO - '" + mdlTranCaptura.estProm18.strEstado.Value.Trim() + "'; ");
                            }
                            double dbNumericTemp56 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm18.strSucursal.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp56))
                            {
                                strMensaje.Append("AVISO R18: SUCURSAL NO ES NUMÉRICO - '" + mdlTranCaptura.estProm18.strSucursal.Value.Trim() + "'; ");
                            }
                            double dbNumericTemp57 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm18.strCanalVta.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp57))
                            {
                                strMensaje.Append("AVISO R18: CANAL DE VENTA NO ES NUMÉRICO - '" + mdlTranCaptura.estProm18.strCanalVta.Value.Trim() + "'; ");
                            }
                            double dbNumericTemp58 = 0;
                            if (!Double.TryParse(mdlTranCaptura.estProm18.strMedio.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp58))
                            {
                                strMensaje.Append("AVISO R18: MEDIO NO ES NUMÉRICO - '" + mdlTranCaptura.estProm18.strMedio.Value.Trim() + "'; ");
                            }

                            break;
                        case "19":
                            //VALIDAR EL REGISTRO SOLO PARA TRÁMITE 15 
                            mdlGlobales.gbolPromoValida = true;
                            intContReg19++;
                            double dbNumericTemp59 = 0;
                            if (mdlTranCaptura.estProm19.strPlazo.Value.Trim().Length == 0 || !Double.TryParse(mdlTranCaptura.estProm19.strPlazo.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp59))
                            {
                                strMensaje.Append("ERROR R19: EL CAMPO PLAZO NO EXISTE O NO ES NUMÉRICO - '" + mdlTranCaptura.estProm19.strPlazo.Value.Trim() + "'; ");
                            }
                            double dbNumericTemp60 = 0;
                            if (mdlTranCaptura.estProm19.strNumCuenta.Value.Trim().Length == 0 || !Double.TryParse(mdlTranCaptura.estProm19.strNumCuenta.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp60))
                            {
                                strMensaje.Append("ERROR R19: EL CAMPO NUM. CUENTA NO EXISTE O NO ES NUMÉRICO - '" + mdlTranCaptura.estProm19.strNumCuenta.Value.Trim() + "'; ");
                            }
                            if (strMensaje.ToString().Length > 0)
                            {
                                if (frmProcMasivo.DefInstance.cboTipoTram.Text.StartsWith("15"))
                                { //SÓLO SI EL TRÁMITE ES 15 Y NO SE CUMPLIERON NINGUNA DE LAS OPCIONES ANTERIORES, ENTONCES SE RECHAZA LA SOLICITUD OML 11/JUNIO/2004
                                    blnErrorDatos = true;
                                    break;
                                }
                            }
                            //VAR 04 Abril 2005 Proyecto 20410 promociones se valida Clave de Promocion y Tipo de Comision 
                            if (!frmProcMasivo.DefInstance.cboTipoTram.Text.StartsWith("09"))
                            { //SÓLO SI EL TRÁMITE ES DIFERENTE DE 09 (AUTOS) SE VALIDA EL TIPO DE COMISION
                                double dbNumericTemp61 = 0;
                                if (!Double.TryParse(mdlTranCaptura.estProm19.strTipoComision.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp61))
                                {
                                    strMensaje.Append("AVISO R19: EL CAMPO TIPO DE COMISION NO ES NUMÉRICO - '" + mdlTranCaptura.estProm19.strTipoComision.Value.Trim() + "'; ");
                                }
                            }
                            string tempRefParam141 = "94";
                            string tempRefParam142 = "E";
                            mdlComunica.OleCatalogos.AbreCatalogo(ref tempRefParam141, ref tempRefParam142, mdlGlobales.gstrTramite.Value + mdlCatalogos.gstrCatFamilia + mdlCatalogos.gstrTipSol);
                            if (Strings.Mid(mdlComunica.OleCatalogos.getAtributos, 22, 1) == "1")
                            {
                                mdlGlobales.gbolAplicaPromo = true;
                                mdlGlobales.gbolAplicaComision = Strings.Mid(mdlComunica.OleCatalogos.getAtributos, 23, 1) != "0";
                            }
                            else
                            {
                                mdlGlobales.gbolAplicaPromo = false;
                            }
                            if (mdlGlobales.gbolAplicaPromo)
                            {
                                subValidaPromocion(mdlTranCaptura.estProm19.strCvePromocion.Value, mdlTranCaptura.estProm19.strTipoComision.Value, intConta);
                            }
                            if (!mdlGlobales.gbolPromoValida)
                            {
                                strMensaje.Append("ERROR R19: LA CLAVE DE PROMOCION -" + mdlTranCaptura.estProm19.strCvePromocion.Value.Trim() + "- No ES VÁLIDA Y NO SE ENCONTRÓ UNA CLAVE DE PROMOCION POR DEFAULT VIGENTE PARA ESTE PRODUCTO ; ");
                                blnErrorDatos = true;
                            }
                            break;
                        case "21": break;
                        default:
                            strMensaje = new StringBuilder("ERROR: NUMERO DE REGISTRO INVÁLIDO; ");
                            strMensaje = new StringBuilder(Strings.Mid(strLinea, 3, 8) + " " + Strings.Mid(strLinea, 1, 2) + " " + strMensaje.ToString());
                            break;
                    }

                    if (strMensaje.ToString().Length > 0)
                    {
                        mdlTranMasivo.gstrMsgVal = mdlTranMasivo.gstrMsgVal + " REGISTRO:" + Strings.Mid(strLinea, 1, 2) + " " + strMensaje.ToString() + " ";
                    }

                }
                strMensaje = new StringBuilder("");
                //OML SIEMPRE DEBE VALIDAR NUMERO DE REGISTROS 14/JUNIO/2004
                if (intContReg01 > 1)
                {
                    strMensaje.Append("REGISTRO 01 (DAT.PER): NO. DE REGISTROS DIFERENTES DE UNO; ");
                    blnErrorDatos = true;
                    gblnContinuaProceso = false;
                }
                if (intContReg03 > 1)
                {
                    strMensaje.Append("REGISTRO 03 (EMP. ACT.): NO. DE REGISTROS DIFERENTES DE UNO; ");
                    blnErrorDatos = true;
                    gblnContinuaProceso = false;
                }
                if (intContReg05 > 1)
                {
                    strMensaje.Append("REGISTRO 05 (DAT. CON.): NO. DE REGISTROS DIFERENTES DE UNO; ");
                    blnErrorDatos = true;
                    gblnContinuaProceso = false;
                }
                if (intContReg10 > 1)
                {
                    strMensaje.Append("REGISTRO 10 (PROP.): NO. DE REGISTROS DIFERENTES DE UNO; ");
                    blnErrorDatos = true;
                    gblnContinuaProceso = false;
                }
                if (intContReg11 > 1)
                {
                    strMensaje.Append("REGISTRO 11 (ING. EGR.): NO. DE REGISTROS DIFERENTES DE UNO; ");
                    blnErrorDatos = true;
                    gblnContinuaProceso = false;
                }
                if (intContReg12 > 1)
                {
                    strMensaje.Append("REGISTRO 12 (TAR. ADIC.): NO. DE REGISTROS DIFERENTES DE UNO; ");
                    blnErrorDatos = true;
                    gblnContinuaProceso = false;
                }
                if (intContReg14 > 1)
                {
                    strMensaje.Append("REGISTRO 14 (OBL. SOL.): NO. DE REGISTROS DIFERENTES DE UNO; ");
                    blnErrorDatos = true;
                    gblnContinuaProceso = false;
                }
                if (intContReg15 > 1)
                {
                    strMensaje.Append("REGISTRO 15 (COMP.): NO. DE REGISTROS DIFERENTES DE UNO; ");
                    blnErrorDatos = true;
                    gblnContinuaProceso = false;
                }
                if (intContReg16 > 1)
                {
                    strMensaje.Append("REGISTRO 16 (DAT. ADIC. SOL.): NO. DE REGISTROS DIFERENTES DE UNO; ");
                    blnErrorDatos = true;
                    gblnContinuaProceso = false;
                }
                if (intContReg17 > 1)
                {
                    strMensaje.Append("REGISTRO 17 (DAT. ADIC. OS): NO. DE REGISTROS DIFERENTES DE UNO; ");
                    blnErrorDatos = true;
                    gblnContinuaProceso = false;
                }

                mdlTranMasivo.gstrMsgVal = Strings.Mid(strLinea, 3, 8) + ".- " + mdlTranMasivo.gstrMsgVal + strMensaje.ToString();
                gbolAvisoDatos = mdlTranMasivo.gstrMsgVal.Trim().Length > 0;
                if (blnErrorDatos)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
            }

            mdlTranMasivo.gstrMsgVal = Strings.Mid(strLinea, 3, 8) + ".- ERROR AL VALIDAR FOLIO: " + Information.Err().Number.ToString() + " - " + Information.Err().Description;
            return false;
        }
		
		//*****************************************************************************************************
		//* Finalidad:  Subrutina para procesar y armar el archivo de salida a partir del archivo de promotora
		//*****************************************************************************************************
        static public void subProcesaArchivoPromotora()
        {
            int intContHeader = 0;
            int intContTrailer = 0;
            int intContLineas = 0;
            string strCadenaLeida = String.Empty;
            string strEncabezado = String.Empty;
            string strReg = String.Empty;
            string strFolio = String.Empty;
            string strBloqueActual = String.Empty;
            int intArchivoLectura = 0;
            int intFileWrite = 0;
            bool blnErrHeader = false;
            int intBloquesOk = 0;
            int intBloquesMal = 0;
            int intPorcAceptadas = 0;
            float SngPorcentajeAceptadas = 0;
            string strCveRemesa = String.Empty;
            string strArchivoError = String.Empty;
            try
            {

                //ASG 20040105 Condición de validación de remesa = 0 para evitar el procesamiento cuando aún no se ha generado la remesa.
                //Validar también que exista el archivo a procesar y que la remesa haya sido generada
                if ((frmProcMasivo.DefInstance.dlgArchivoAbrir.FileName.Trim().Length == 0 || FileSystem.Dir(frmProcMasivo.DefInstance.dlgArchivoAbrir.FileName, FileAttribute.Normal) == "" || !mdlGlobales.gblnRemesaRegistrada) && !mdlGlobales.gblnProcesaArchivo)
                {
                    return;
                }

                //Deshabilitar los controles de la forma
                mdlGlobales.InhibeControles(frmProcMasivo.DefInstance, false, "PROCESO");
                mdlGlobales.gblnRemesaRegistrada = false; //La remesa aún no se ha generado
                //if (FileSystem.Dir(mdlGlobales.gcStrMasivosIni, FileAttribute.Normal) == "")
                //{
                //if (! mdlGlobales.funCreaMasivosIni())
                //{
                //	string tempRefParam = "ARCHIVO DE PARÁMETROS: " + mdlGlobales.gcStrMasivosIni + ", INEXISTENTE";
                //	MsgBoxStyle tempRefParam2 = MsgBoxStyle.Exclamation;
                //	string tempRefParam3 = "ERROR";
                //	mdlGlobales.subDespErrores(ref tempRefParam, ref tempRefParam2, ref tempRefParam3);
                //	return;
                //}
                //}

                //string tempRefParam4 = mdlGlobales.gcStrMasivosIni;
                //string tempRefParam5 = "RUTA_ERROR";
                //string tempRefParam6 = "RUTA";

                //MIG WXP INI JGC 20090825
                //mdlGlobales.strPathError = mdlGlobales.funGetParam(ref tempRefParam4, ref tempRefParam5, ref tempRefParam6);
                //if (mdlGlobales.strPathError == "unknown")
                //{
                //if (mdlGlobales.funCreaMasivosIni())
                //{
                //	string tempRefParam7 = "RUTA_ERROR";
                //	string tempRefParam8 = "RUTA";
                //	string tempRefParam9 = mdlGlobales.gcStrMasivosIni;
                //	mdlGlobales.strPathError = mdlGlobales.funGetParam(ref tempRefParam7, ref tempRefParam8, ref tempRefParam9);
                //} else
                //{
                //	return;
                //}
                //}

                string strMASRutaAplicacion = mdlRegistry.RegistryMasivos("MASRutaAplicacion");
                mdlGlobales.strPathError = mdlRegistry.RegistryMasivos("MASRutaBItacora");
                mdlGlobales.strPathError = strMASRutaAplicacion + mdlGlobales.strPathError + "\\";
                //MIG WXP FIN JGC 20090825

                mdlGlobales.subDespMensajes("");
                mdlGlobales.subDespMensajes("NUMERO DE REMESA: " + frmProcMasivo.DefInstance.txtRemesa.Text);
                frmProcMasivo.DefInstance.prg_proceso.Value = 0;
                Cursor.Current = Cursors.WaitCursor;

                intArchivoLectura = FileSystem.FreeFile();
                FileSystem.FileOpen(intArchivoLectura, frmProcMasivo.DefInstance.dlgArchivoAbrir.FileName, OpenMode.Input, OpenAccess.Default, OpenShare.Default, -1);

                intContHeader = 0;
                intContTrailer = 0;
                intContLineas = 0;
                intBloquesOk = 0;
                intBloquesMal = 0;

                mdlTranCaptura.ClAceptadas = new Collection();
                blnErrHeader = false;

                mdlGlobales.subDespMensajes("VALIDANDO REMESA");
                while (!FileSystem.EOF(intArchivoLectura) && !blnErrHeader)
                {

                    strCadenaLeida = FileSystem.LineInput(intArchivoLectura);
                    strReg = Strings.Mid(strCadenaLeida, 1, 2);
                    strFolio = Strings.Mid(strCadenaLeida, 3, 8);

                    if (strReg == "00")
                    {
                        intContHeader++;
                        strBloqueActual = strFolio;
                        strBloqueActual = Double.Parse(frmProcMasivo.DefInstance.txtFolInicial.Text).ToString();
                        if (intContHeader > 1)
                        {
                            blnErrHeader = true;
                        }
                    }

                    if (strReg == "99")
                    {
                        intContTrailer++;
                    }

                    if (strReg != "00")
                    {
                        if (Conversion.Val(strBloqueActual) != Conversion.Val(strFolio) && (intContHeader == 1))
                        {
                            strBloqueActual = strFolio;
                            intContLineas = 0;
                            mdlTranMasivo.gstrMsgVal = "";
                            //Valida el bloque
                            if (funValidaRegistro())
                            {
                                intBloquesOk++;
                                if (gbolAvisoDatos)
                                {
                                    intFileWrite = FileSystem.FreeFile();
                                    strArchivoError = frmProcMasivo.DefInstance.dlgArchivoAbrir.FileTitle;
                                    if (strArchivoError.IndexOf('.') >= 0)
                                    {
                                        strArchivoError = Strings.Mid(strArchivoError, 1, strArchivoError.IndexOf('.'));
                                    }
                                    strArchivoError = "AVISOS_" + strArchivoError + ".TXT";
                                    FileSystem.FileOpen(intFileWrite, mdlGlobales.strPathError + strArchivoError, OpenMode.Append, OpenAccess.Default, OpenShare.Default, -1);
                                    FileSystem.PrintLine(intFileWrite, "Folio: " + mdlTranMasivo.gstrMsgVal);
                                    FileSystem.FileClose(intFileWrite);
                                }
                                mdlTranCaptura.ClAceptadas.Add(mdlTranCaptura.gblaBloque, null, null, null);
                                frmProcMasivo.DefInstance.txtSolAceptadas.Text = intBloquesOk.ToString();
                                frmProcMasivo.DefInstance.txtSolAceptadas.Refresh();

                            }
                            else
                            {
                                intFileWrite = FileSystem.FreeFile();
                                strArchivoError = frmProcMasivo.DefInstance.dlgArchivoAbrir.FileTitle;
                                if (strArchivoError.IndexOf('.') >= 0)
                                {
                                    strArchivoError = Strings.Mid(strArchivoError, 1, strArchivoError.IndexOf('.'));
                                }
                                strArchivoError = "ERR_" + strArchivoError + ".TXT";
                                FileSystem.FileOpen(intFileWrite, mdlGlobales.strPathError + strArchivoError, OpenMode.Append, OpenAccess.Default, OpenShare.Default, -1);
                                FileSystem.PrintLine(intFileWrite, "Folio: " + mdlTranMasivo.gstrMsgVal);
                                FileSystem.FileClose(intFileWrite);
                                intBloquesMal++;
                                frmProcMasivo.DefInstance.txtSolRechazadas.Text = intBloquesMal.ToString();
                                frmProcMasivo.DefInstance.txtSolRechazadas.Refresh();
                                if (!gblnContinuaProceso)
                                {
                                    //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                                    string tempRefParam10 = "SE DETECTARON ERRORES EN EL ARCHIVO DE PROMOTORA, REVISAR BITACORA DE ERRORES";
                                    MsgBoxStyle tempRefParam11 = (MsgBoxStyle)(((int)MsgBoxStyle.Exclamation) + ((int)MsgBoxStyle.OkOnly));
                                    string tempRefParam12 = "NO SE PUEDE CONTINUAR EL PROCESO";
                                    mdlGlobales.subDespErrores(ref tempRefParam10, ref tempRefParam11, ref tempRefParam12);
                                    frmProcMasivo.DefInstance.subLimpiarDatos();
                                    mdlGlobales.InhibeControles(frmProcMasivo.DefInstance, true, "PROCESO");
                                    frmProcMasivo.DefInstance.cmdLeeArchivo.Focus();
                                    Cursor.Current = Cursors.Default;
                                    return;
                                }
                            }
                            intContLineas = 0;
                        }
                        mdlTranCaptura.gblaBloque = ArraysHelper.RedimPreserve<string[]>(mdlTranCaptura.gblaBloque, new int[] { intContLineas + 1 });
                        mdlTranCaptura.gblaBloque[intContLineas] = strCadenaLeida;
                        intContLineas++;
                    }
                    if (frmProcMasivo.DefInstance.prg_proceso.Value >= intBloquesOk + intBloquesMal)
                    {
                        frmProcMasivo.DefInstance.prg_proceso.Value = intBloquesOk + intBloquesMal;
                    }
                }
                FileSystem.FileClose(intArchivoLectura);
                frmProcMasivo.DefInstance.prg_proceso.Value = 0;
                //AIS-1896 FSABORIO
                if (!frmProceso.DefInstance.SuspendFormClosing)
                    frmProceso.DefInstance.Close();
                Cursor.Current = Cursors.Default;

                if (intContHeader > 1 || blnErrHeader)
                {
                    //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                    string tempRefParam13 = "ERROR DE FORMATO EN EL HEADER";
                    MsgBoxStyle tempRefParam14 = (MsgBoxStyle)(((int)MsgBoxStyle.Critical) + ((int)MsgBoxStyle.OkOnly));
                    mdlGlobales.subDespErrores(ref tempRefParam13, ref tempRefParam14);
                    return;
                }
                if (intContTrailer > 1)
                {
                    //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                    string tempRefParam15 = "ERROR DE FORMATO EN EL TRAILER";
                    MsgBoxStyle tempRefParam16 = (MsgBoxStyle)(((int)MsgBoxStyle.Critical) + ((int)MsgBoxStyle.OkOnly));
                    mdlGlobales.subDespErrores(ref tempRefParam15, ref tempRefParam16);
                    return;
                }

                strEncabezado = new String(' ', 8);
                if (intContHeader > 1)
                {
                    strEncabezado = strEncabezado + "00HEADERHAY MÁS DE UN REGISTRO00/";
                }
                else
                {
                    if (intContHeader != 1)
                    {
                        strEncabezado = strEncabezado + "00HEADERINEXISTENTE EN ARCHIVO ELEGIDO/";
                    }
                }

                if (intContTrailer > 1)
                {
                    strEncabezado = strEncabezado + "99TRAILERHAY MÁS DE UN REGISTRO99/";
                }
                else
                {
                    if (intContTrailer != 1)
                    {
                        strEncabezado = strEncabezado + "99TRAILERINEXISTENTE EN ARCHIVO ELEGIDO/";
                    }
                }
                mdlGlobales.subDespMensajes("");
                intContLineas = Convert.ToInt32((Int32.Parse(frmProcMasivo.DefInstance.txtSolAceptadas.Text) * 100) / ((double)Int32.Parse(frmProcMasivo.DefInstance.txtNumSolicitudes.Text)));
                frmProcMasivo.DefInstance.txtSolRechazadas.Text = (Conversion.Val(frmProcMasivo.DefInstance.txtNumSolicitudes.Text) - Conversion.Val(frmProcMasivo.DefInstance.txtSolAceptadas.Text)).ToString();
                frmProcMasivo.DefInstance.lblGrabado.Visible = true;
                frmProcMasivo.DefInstance.lblGrabado.Text = "";
                //ASG Obtener de catálogo de parámetros (24) el no. mínimo de solicitudes aceptadas
                string tempRefParam19 = "24";
                string tempRefParam20 = "1";
                if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam19, ref tempRefParam20))
                {
                    //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                    string tempRefParam17 = "NO SE ENCONTRÓ EL PARÁMETRO EN EL CATÁLOGO, FAVOR DE AVISAR A SU ADMINISTRADOR";
                    MsgBoxStyle tempRefParam18 = (MsgBoxStyle)(((int)MsgBoxStyle.Critical) + ((int)MsgBoxStyle.OkOnly));
                    mdlGlobales.subDespErrores(ref tempRefParam17, ref tempRefParam18);
                    return;
                }
                SngPorcentajeAceptadas = (float)Conversion.Val(mdlComunica.OleCatalogos.getRangoIni.Substring(mdlComunica.OleCatalogos.getRangoIni.Length - Math.Min(mdlComunica.OleCatalogos.getRangoIni.Length, 1)) + Strings.Mid(mdlComunica.OleCatalogos.getRangoIni, 2, 9) + "." + mdlComunica.OleCatalogos.getRangoIni.Substring(mdlComunica.OleCatalogos.getRangoIni.Length - Math.Min(mdlComunica.OleCatalogos.getRangoIni.Length, 4)));
                if (intContLineas >= SngPorcentajeAceptadas)
                {
                    frmProcMasivo.DefInstance.lblGrabado.Text = "Solicitudes aceptadas: " + StringsHelper.Format(intContLineas.ToString().Trim(), "0.00") + "% obtenido; se envía remesa automáticamente";
                    frmProcMasivo.DefInstance.lblGrabado.ForeColor = Color.Blue;
                    if (mdlGlobales.gblnEjecutaRegistro)
                    {
                        if (mdlTranMasivo.funGeneraRemesa(ref strCveRemesa))
                        {
                            mdlTranCaptura.subEnviaBloques();
                        }
                    }
                    else
                    {
                        mdlTranCaptura.subEnviaBloques();
                    }
                    mdlGlobales.gblnEjecutaRegistro = false;
                }
                else
                {
                    frmProcMasivo.DefInstance.lblGrabado.Text = "Solicitudes aceptadas: " + StringsHelper.Format(Conversion.Str(intContLineas).Trim(), "0.00") + "% obtenido; se rechaza la remesa";
                    frmProcMasivo.DefInstance.lblGrabado.ForeColor = Color.FromArgb(192, 0, 0);
                }

                mdlGlobales.gblnRemesaRegistrada = false; //REINICIALIZAR LA VARIABLE DE REGISTRO DE REMESAS
                mdlGlobales.InhibeControles(frmProcMasivo.DefInstance, true, "PROCESO");
            }
            catch (Exception excep)
            {

                mdlGlobales.subDespMensajes("");
                //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                string tempRefParam21 = "ERROR EN EL PROCESAMIENTO DEL ARCHIVO DE PROMOTORA, " + Information.Err().Number.ToString() + ": " + excep.Message;
                MsgBoxStyle tempRefParam22 = (MsgBoxStyle)(((int)MsgBoxStyle.Critical) + ((int)MsgBoxStyle.OkOnly));
                mdlGlobales.subDespErrores(ref tempRefParam21, ref tempRefParam22);
                mdlGlobales.InhibeControles(frmProcMasivo.DefInstance, true, "PROCESO");
            }
        }
		
		static public bool funValidaPatron(ref  string strLinea)
		{
			if (strLinea.IndexOf(mdlGlobales.gstrPatron) >= 0)
			{
				return false;
			} else
			{
				return true;
			}
		}
		//VAR 23 Marzo 2005 proyecto 20410 promociones
		//*****************************************************************************************************
		//* Finalidad:  Subrutina para validar y obtener la clave de promoción de cada solicitud proyeto 20410 promociones.
		//*****************************************************************************************************
		static public void  subValidaPromocion( string strCvePromocion,  string strTipoComision,  int intPosicion)
		{
			int intFechaInicio = 0;
			string strFechaTermino = String.Empty;
			string strFecTerminoFinal = String.Empty;
			int intDiasVigenciaRecepcion = 0;
			int intFechaSolicitud = 0;
			int intNumDefault = 0;
			ArrayPromo[] arrPromo = null;// = ArraysHelper.InitializeArray<ArrayPromo[]>(new int[]{});
			
			bool bolPromoEncontrada = false;
			int intContadorPromo = 0;
			string tempRefParam = "172";
			string tempRefParam2 = "E";
			mdlComunica.OleCatalogosPromo.AbreCatalogo(ref tempRefParam, ref tempRefParam2);
			while (! mdlComunica.OleCatalogosPromo.EOF_Renamed())
				{
				
					if (Conversion.Val(mdlGlobales.gstrTramite.Value) == Conversion.Val(mdlComunica.OleCatalogosPromo.getLlave2) && Conversion.Val(mdlCatalogos.gstrCatFamilia) == Conversion.Val(mdlComunica.OleCatalogosPromo.getLlave3) && Conversion.Val(mdlCatalogos.gstrTipSol) == Conversion.Val(mdlComunica.OleCatalogosPromo.getLlave4))
					{
						//       Si pertenece al producto se incluye para el proceso.
						intContadorPromo++;
						arrPromo = ArraysHelper.RedimPreserve<ArrayPromo[]>(arrPromo, new int[]{intContadorPromo + 1});
						arrPromo[intContadorPromo].strArrayPromo.Value = mdlComunica.OleCatalogosPromo.getLlave1;
					}
					mdlComunica.OleCatalogosPromo.MoveNext();
				}
			mdlComunica.OleCatalogosPromo.CierraCatalogo();
			int intContador = 1;
			//   Busca la promoción que contenga la clave de proceso alfanumérica
			while (intContador <= intContadorPromo && ! bolPromoEncontrada)
				{
				
					string tempRefParam3 = "171";
					string tempRefParam4 = "E";
					mdlComunica.OleCatalogosPromo.AbreCatalogo(ref tempRefParam3, ref tempRefParam4, arrPromo[intContador].strArrayPromo.Value.Trim());
					if (! mdlComunica.OleCatalogosPromo.EOF_Renamed())
					{
						if (Strings.Mid(mdlComunica.OleCatalogosPromo.getAtributos, 3, 12) == strCvePromocion)
						{
							//               Si además es Promoción y es Seleccionable
							if (Strings.Mid(mdlComunica.OleCatalogosPromo.getAtributos, 1, 1) == "P" && Strings.Mid(mdlComunica.OleCatalogosPromo.getAtributos, 2, 1) == "S")
							{
								bolPromoEncontrada = true;
                                //mdlTranCaptura.gblaBloque[intPosicion] = Strings.Mid(mdlTranCaptura.gblaBloque[intPosicion], 1, 81) + mdlGlobales.funPoneCeros(mdlComunica.OleCatalogosPromo.getLlave1, 12) + Strings.Mid(mdlTranCaptura.gblaBloque[intPosicion], 94, 2); //ABH 10/12/2011 ART 115 81 por 86, 94 por 99, 2 por 91 
                                mdlTranCaptura.gblaBloque[intPosicion] = Strings.Mid(mdlTranCaptura.gblaBloque[intPosicion], 1, 86) + mdlGlobales.funPoneCeros(mdlComunica.OleCatalogosPromo.getLlave1, 12) + Strings.Mid(mdlTranCaptura.gblaBloque[intPosicion], 99, 91); //ABH 10/12/2011 ART 115 81 por 86, 94 por 99, 2 por 91 
							}
						}
					}
					mdlComunica.OleCatalogosPromo.CierraCatalogo();
					intContador++;
				}
			
			if (! bolPromoEncontrada)
			{
				if (mdlGlobales.gbolEncontrePromoDflt)
				{
					// Asignar la promocion de default
                    //mdlTranCaptura.gblaBloque[intPosicion] = Strings.Mid(mdlTranCaptura.gblaBloque[intPosicion], 1, 81) + mdlGlobales.gstrCvePromoDflt + Strings.Mid(mdlTranCaptura.gblaBloque[intPosicion], 94, 2); //ABH 10/12/2011 ART 115 81 por 86, 94 por 99, 2 por 91 
                    mdlTranCaptura.gblaBloque[intPosicion] = Strings.Mid(mdlTranCaptura.gblaBloque[intPosicion], 1, 86) + mdlGlobales.gstrCvePromoDflt + Strings.Mid(mdlTranCaptura.gblaBloque[intPosicion], 99, 91); //ABH 10/12/2011 ART 115 81 por 86, 94 por 99, 2 por 91 
				} else
				{
					mdlGlobales.gbolPromoValida = false;
				}
			}
			
			if (mdlGlobales.gbolAplicaComision)
			{
				//       Búsqueda directa del Tipo de Comisión
				string tempRefParam5 = "170";
				string tempRefParam6 = "E";
				mdlComunica.OleCatalogos.AbreCatalogo(ref tempRefParam5, ref tempRefParam6, mdlGlobales.funPoneCeros(strTipoComision, 8), "00000000", "00000000", "00000000");
				if (mdlComunica.OleCatalogos.getLlave1 != mdlGlobales.funPoneCeros(strTipoComision, 8))
				{
                    //mdlTranCaptura.gblaBloque[intPosicion] = Strings.Mid(mdlTranCaptura.gblaBloque[intPosicion], 1, 93) + "99";
                    mdlTranCaptura.gblaBloque[intPosicion] = Strings.Mid(mdlTranCaptura.gblaBloque[intPosicion], 1, 189) + "99"; //ABH 10/12/2011 ART 115 93 por 189
				}
				mdlComunica.OleCatalogos.CierraCatalogo();
			} else
			{
                //mdlTranCaptura.gblaBloque[intPosicion] = Strings.Mid(mdlTranCaptura.gblaBloque[intPosicion], 1, 93) + "  ";
                mdlTranCaptura.gblaBloque[intPosicion] = Strings.Mid(mdlTranCaptura.gblaBloque[intPosicion], 1, 189) + "  "; //ABH 10/12/2011 ART 115 93 por 189
			}
		}
		
		static public void  subObtienePromoDflt()
		{
			int intFechaInicio = 0;
			string strFechaTermino = String.Empty;
			int intDiasVigenciaRecepcion = 0;
			string strFecTerminoFinal = String.Empty;
			string strFechaProceso = String.Empty;
			ArrayPromo[] arrPromo = null;// = ArraysHelper.InitializeArray<ArrayPromo[]>(new int[]{});
			
			mdlGlobales.gbolEncontrePromoDflt = false;
			int intContadorPromo = 0;
			//       Todas las promociones del catálogo 172
			string tempRefParam = "172";
			string tempRefParam2 = "E";
			mdlComunica.OleCatalogosPromo.AbreCatalogo(ref tempRefParam, ref tempRefParam2);
			while (! mdlComunica.OleCatalogosPromo.EOF_Renamed())
				{
				
					if (Conversion.Val(mdlGlobales.gstrTramite.Value) == Conversion.Val(mdlComunica.OleCatalogosPromo.getLlave2) && Conversion.Val(mdlCatalogos.gstrCatFamilia) == Conversion.Val(mdlComunica.OleCatalogosPromo.getLlave3) && Conversion.Val(mdlCatalogos.gstrTipSol) == Conversion.Val(mdlComunica.OleCatalogosPromo.getLlave4))
					{
						intFechaInicio = Convert.ToInt32(Conversion.Val(Strings.Mid(mdlComunica.OleCatalogosPromo.getRangoIni, 2, 9)));
						if ((Conversion.Val(Strings.Mid(mdlComunica.OleCatalogosPromo.getAtributos, 7, 2)) > 0 && Conversion.Val(Strings.Mid(mdlComunica.OleCatalogosPromo.getAtributos, 7, 2)) <= 31) && (Conversion.Val(Strings.Mid(mdlComunica.OleCatalogosPromo.getAtributos, 5, 2)) > 0 && Conversion.Val(Strings.Mid(mdlComunica.OleCatalogosPromo.getAtributos, 5, 2)) <= 12))
						{
							strFechaTermino = Strings.Mid(mdlComunica.OleCatalogosPromo.getAtributos, 7, 2) + "/" + Strings.Mid(mdlComunica.OleCatalogosPromo.getAtributos, 5, 2) + "/" + Strings.Mid(mdlComunica.OleCatalogosPromo.getAtributos, 1, 4);
							strFechaTermino = DateTime.Parse(strFechaTermino).ToString("yyyy/MM/dd");
						} else
						{
							strFechaTermino = "1900/01/01";
						}
						intDiasVigenciaRecepcion = Convert.ToInt32(Conversion.Val(Strings.Mid(mdlComunica.OleCatalogosPromo.getAtributos, 9, 3)));
						strFecTerminoFinal = DateTime.Parse(strFechaTermino).AddDays(intDiasVigenciaRecepcion).ToString("yyyyMMdd");
						strFechaProceso = DateTime.Today.ToString("yyyyMMdd");
						//           Obtener todas las que esten vigentes
						if (intFechaInicio <= Conversion.Val(strFechaProceso) && Conversion.Val(strFechaProceso) <= Conversion.Val(strFecTerminoFinal))
						{
							intContadorPromo++;
							arrPromo = ArraysHelper.RedimPreserve<ArrayPromo[]>(arrPromo, new int[]{intContadorPromo + 1});
							arrPromo[intContadorPromo].strArrayPromo.Value = mdlComunica.OleCatalogosPromo.getLlave1;
						}
					}
					mdlComunica.OleCatalogosPromo.MoveNext();
				}
			mdlComunica.OleCatalogosPromo.CierraCatalogo();
			int intContador = 1;
			//       De las que están vigentes obtener la de default?
			while (intContador <= intContadorPromo && ! mdlGlobales.gbolEncontrePromoDflt)
				{
				
					string tempRefParam3 = "171";
					string tempRefParam4 = "E";
					mdlComunica.OleCatalogosPromo.AbreCatalogo(ref tempRefParam3, ref tempRefParam4, arrPromo[intContador].strArrayPromo.Value.Trim());
					if ((Strings.Mid(mdlComunica.OleCatalogosPromo.getAtributos, 2, 1)) == "D")
					{
						mdlGlobales.gstrCvePromoDflt = mdlGlobales.funPoneCeros(mdlComunica.OleCatalogosPromo.getLlave1, 12);
						mdlGlobales.gbolEncontrePromoDflt = true;
					}
					intContador++;
					mdlComunica.OleCatalogosPromo.CierraCatalogo();
				}
		}
	}
}