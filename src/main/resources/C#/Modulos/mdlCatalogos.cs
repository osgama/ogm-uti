using Artinsoft.VB6.Utils; 
using Microsoft.VisualBasic; 
using Microsoft.VisualBasic.Compatibility.VB6; 
using System; 
using System.Globalization; 
using System.Text; 
using System.Windows.Forms; 
using VB6 = Microsoft.VisualBasic.Compatibility.VB6.Support; 

namespace Masivos
{
	class mdlCatalogos
	{
	
		//'*******************************************************************************
		//'* Identificación: mdlCatalogos.bas                                            *
		//'* Autor:          Odeth S. Montaño López                                      *
		//'* Instalación:    BANAMEX, S.A.                                               *
		//'* Fecha:          08/01/2003                                                  *
		//'* Versión:        1.0                                                         *
		//'*******************************************************************************
		//'* Objetivo: Módulo para manejo de Catálogos del sistema (bajar de información *
		//'*           de tandem, generación de archivo texto, y llenado de combos)      *
		//'*                                                                             *
		//'*******************************************************************************
		//'* Modificación: < No. de Requerimiento >                                      *
		//'* Descripción:                                                                *
		//'* Fecha:                                                                      *
		//'* Versión:                                                                    *
		//'* Modificó:                                                                   *
		//'*******************************************************************************
		//
		static public string gstrCatFamilia = String.Empty;
		static public string gstrCatTramite = String.Empty;
		static public string gstrDescTipoSol = String.Empty;
		static public string gstrTipSol = String.Empty;
		static public bool gBolHayCatalogos = false;
		static public string gstrCatProceso = String.Empty;
		static public FixedLengthString gstrTipoOp = new FixedLengthString(3);
		static public bool gblnModal = false;
		
		public struct udtBusquedaCombo
		{
			public ComboBox cboCombo;
			public string strLlaveAlfa;
			public string strLlaveNum;
			public int intUltimoIndice;
			public static udtBusquedaCombo CreateInstance()
			{
					udtBusquedaCombo result = new udtBusquedaCombo();
					result.strLlaveAlfa = String.Empty;
					result.strLlaveNum = String.Empty;
					return result;
			}
		}
		static public udtBusquedaCombo estCombos = mdlCatalogos.udtBusquedaCombo.CreateInstance();
		
		//*****************************************************************************************************
		//* Finalidad:  Función para validar que existan los datos en el catálogo
		//*****************************************************************************************************
		static public bool funValidaCatalogoProductos()
		{
			bool result = false;
			int intCont = 0;
			
			bool blnExisteProducto = false;
			bool blnExisteFamProd = false;
			bool blnExisteTipoSol = false;
			
			//Validar la familia del producto
			string tempRefParam3 = "99";
			string tempRefParam4 = frmProcMasivo.DefInstance.cboTipoTram.Text.Substring(0, Math.Min(frmProcMasivo.DefInstance.cboTipoTram.Text.Length, 2));
			string tempRefParam5 = Strings.Mid(frmProcMasivo.DefInstance.txtTipoSolicitud.Text, 1, 2);
			string tempRefParam6 = null;
			string tempRefParam7 = null;
			Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam8 = Catalogos.clsCatalogos.enmCamposCatalogos.Default;
			string tempRefParam9 = "E";
			if (mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam3, tempRefParam4, tempRefParam5, tempRefParam6, ref tempRefParam7, ref tempRefParam8, ref tempRefParam9))
			{
				result = true;
				gstrCatFamilia = mdlComunica.OleCatalogos.getLlave2.Substring(mdlComunica.OleCatalogos.getLlave2.Length - Math.Min(mdlComunica.OleCatalogos.getLlave2.Length, 2));
				frmProcMasivo.DefInstance.txtFamiliaProducto.Text = gstrCatFamilia + " " + mdlComunica.OleCatalogos.getDescripcion;
			} else
			{
				//UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
				string tempRefParam = "LA FAMILIA PRODUCTO QUE MARCA EL DISKETE NO EXISTE";
				MsgBoxStyle tempRefParam2 = (MsgBoxStyle) (((int) MsgBoxStyle.Exclamation) + ((int) MsgBoxStyle.OkOnly));
				mdlGlobales.subDespErrores(ref tempRefParam, ref tempRefParam2);
                // CERRAR EL ARCHVIO UTILIZADO

				return result;
			}
			//Validar el tipo de solicitud
			string tempRefParam12 = "94";
			string tempRefParam13 = mdlGlobales.funPoneCeros(Strings.Mid(frmProcMasivo.DefInstance.cboTipoTram.Text, 1, 2), 4) + Strings.Mid(frmProcMasivo.DefInstance.txtTipoSolicitud.Text, 1, 4);
			string tempRefParam14 = null;
			string tempRefParam15 = null;
			string tempRefParam16 = null;
			Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam17 = Catalogos.clsCatalogos.enmCamposCatalogos.Default;
			string tempRefParam18 = "E";
			if (mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam12, tempRefParam13, tempRefParam14, tempRefParam15, ref tempRefParam16, ref tempRefParam17, ref tempRefParam18))
			{
				result = true;
				gstrTipSol = Strings.Mid(frmProcMasivo.DefInstance.txtTipoSolicitud.Text, 3, 2);
				gstrDescTipoSol = funObtenDescripcion(mdlComunica.OleCatalogos.getDescripcion).Trim();
			} else
			{
				//UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
				string tempRefParam10 = "EL TIPO DE SOLICITUD QUE MARCA EL DISKETE NO EXISTE";
				MsgBoxStyle tempRefParam11 = (MsgBoxStyle) (((int) MsgBoxStyle.Exclamation) + ((int) MsgBoxStyle.OkOnly));
				mdlGlobales.subDespErrores(ref tempRefParam10, ref tempRefParam11);
				return result;
			}
			return result;
		}
		
		static private string funObtenDescripcion( string strDescripcion)
		{
			int IntI = 0;
			for (IntI = 1; IntI <= strDescripcion.Length; IntI++)
			{
				double dbNumericTemp = 0;
				if (! Double.TryParse(Strings.Mid(strDescripcion, IntI, 1), NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp) && Strings.Mid(strDescripcion, IntI, 1) != " ")
				{
					break;
				}
			}
			return Strings.Mid(strDescripcion, IntI, strDescripcion.Length);
		}
		
		static public void  subBusquedaRapida(ref  ComboBox cboCombo, ref  Keys KeyAscii)
		{
			int IntI = 0;
			string strCadena = String.Empty;
			bool blnEncontrado = false;
			bool blnSeInicializa = false;
			if (cboCombo.Items.Count == 0)
			{
				return;
			} //El combo está vacío
			//UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
			KeyAscii = (Keys) Strings.Asc(Strings.Chr((int) KeyAscii).ToString().ToUpper()[0]);
			if (estCombos.cboCombo == null)
			{
				subInicializaEstructura(cboCombo);
			} else if (estCombos.cboCombo != cboCombo) { 
				subInicializaEstructura(cboCombo);
			}
			if (KeyAscii == Keys.Escape)
			{
				subInicializaEstructura(cboCombo); //Al dar la tecla ESC se inicializa la búsqueda
			}
			//Validar la llave de búsqueda, armando una concatenación de llaves
			double dbNumericTemp = 0;
			if (Double.TryParse(Strings.Chr((int) KeyAscii).ToString(), NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp))
			{
				estCombos.strLlaveAlfa = "";
				if (funObtenSeccionNumerica(VB6.GetItemString(cboCombo, 0)).Length >= estCombos.strLlaveNum.Trim().Length)
				{
					estCombos.strLlaveNum = estCombos.strLlaveNum + Strings.Chr((int) KeyAscii).ToString();
				} else
				{
					estCombos.strLlaveNum = Strings.Chr((int) KeyAscii).ToString();
				}
				estCombos.intUltimoIndice = -1;
			} else if (((int) KeyAscii) >= 65 && ((int) KeyAscii) <= 90) {  //Sólo acepta caracteres de la A a la Z
				if (! (estCombos.strLlaveAlfa.Length == 1 && estCombos.strLlaveAlfa == Strings.Chr((int) KeyAscii).ToString()))
				{
					estCombos.strLlaveAlfa = estCombos.strLlaveAlfa + Strings.Chr((int) KeyAscii).ToString();
					estCombos.intUltimoIndice = -1;
				}
				estCombos.strLlaveNum = "";
			} else
			{
				return;
			}
			//Una vez armada la llave de búsqueda se comienza a realizar el recorrido
			blnEncontrado = false;
			for (IntI = estCombos.intUltimoIndice + 1; IntI <= cboCombo.Items.Count - 1; IntI++)
			{
				strCadena = VB6.GetItemString(cboCombo, IntI);
				if (estCombos.strLlaveAlfa.Trim() != "")
				{ //Búsqueda por llave alfanumérica
					if (funObtenSeccionAlfaNumerica(strCadena).Substring(0, Math.Min(funObtenSeccionAlfaNumerica(strCadena).Length, estCombos.strLlaveAlfa.Length)) == estCombos.strLlaveAlfa)
					{
						estCombos.intUltimoIndice = IntI;
						blnEncontrado = true;
						break;
					}
				} else
				{
					//Búsqueda por llave numérica
					if (Conversion.Val(funObtenSeccionNumerica(strCadena)) == Conversion.Val(estCombos.strLlaveNum))
					{
						estCombos.intUltimoIndice = IntI;
						blnEncontrado = true;
						break;
					}
				}
			}
			if (blnEncontrado)
			{
				cboCombo.SelectedIndex = IntI;
			} else
			{
				Interaction.Beep();
				subInicializaEstructura(cboCombo);
			}
			//UPGRADE_WARNING: (6021) Casting 'byte' to Enum may cause different behaviour.
			KeyAscii = (Keys) 0;
		}
		
		//Diferencia con la función anterior es que la búsqueda por la llave numérica se
		//hace sin la función VAL, es decir al oprimir un 1, la cadena correspondiente a 0100 coincidirá
		static public void  subBusquedaRapidaCadena(ref  ComboBox cboCombo, ref  Keys KeyAscii)
		{
			int IntI = 0;
			string strCadena = String.Empty;
			bool blnEncontrado = false;
			bool blnSeInicializa = false;
			if (cboCombo.Items.Count == 0)
			{
				return;
			} //El combo está vacío
			//UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
			KeyAscii = (Keys) Strings.Asc(Strings.Chr((int) KeyAscii).ToString().ToUpper()[0]);
			if (estCombos.cboCombo == null)
			{
				subInicializaEstructura(cboCombo);
			} else if (estCombos.cboCombo != cboCombo) { 
				subInicializaEstructura(cboCombo);
			}
			if (KeyAscii == Keys.Escape)
			{
				subInicializaEstructura(cboCombo); //Al dar la tecla ESC se inicializa la búsqueda
			}
			//Validar la llave de búsqueda, armando una concatenación de llaves
			double dbNumericTemp = 0;
			if (Double.TryParse(Strings.Chr((int) KeyAscii).ToString(), NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp))
			{
				estCombos.strLlaveAlfa = "";
				if (funObtenSeccionNumerica(VB6.GetItemString(cboCombo, 0)).Length >= estCombos.strLlaveNum.Trim().Length)
				{
					estCombos.strLlaveNum = estCombos.strLlaveNum + Strings.Chr((int) KeyAscii).ToString();
				} else
				{
					estCombos.strLlaveNum = Strings.Chr((int) KeyAscii).ToString();
				}
				estCombos.intUltimoIndice = -1;
			} else if (((int) KeyAscii) >= 65 && ((int) KeyAscii) <= 90) {  //Sólo acepta caracteres de la A a la Z
				if (! (estCombos.strLlaveAlfa.Length == 1 && estCombos.strLlaveAlfa == Strings.Chr((int) KeyAscii).ToString()))
				{
					estCombos.strLlaveAlfa = estCombos.strLlaveAlfa + Strings.Chr((int) KeyAscii).ToString();
					estCombos.intUltimoIndice = -1;
				}
				estCombos.strLlaveNum = "";
			} else
			{
				return;
			}
			//Una vez armada la llave de búsqueda se comienza a realizar el recorrido
			blnEncontrado = false;
			for (IntI = estCombos.intUltimoIndice + 1; IntI <= cboCombo.Items.Count - 1; IntI++)
			{
				strCadena = VB6.GetItemString(cboCombo, IntI);
				if (estCombos.strLlaveAlfa.Trim() != "")
				{ //Búsqueda por llave alfanumérica
					if (funObtenSeccionAlfaNumerica(strCadena).Substring(0, Math.Min(funObtenSeccionAlfaNumerica(strCadena).Length, estCombos.strLlaveAlfa.Length)) == estCombos.strLlaveAlfa)
					{
						estCombos.intUltimoIndice = IntI;
						blnEncontrado = true;
						break;
					}
				} else
				{
					//Búsqueda por llave numérica
					if (StringsHelper.DoubleValue(Conversion.Val(funObtenSeccionNumerica(strCadena)).ToString().Substring(0, Math.Min(Conversion.Val(funObtenSeccionNumerica(strCadena)).ToString().Length, estCombos.strLlaveNum.Length))) == Conversion.Val(estCombos.strLlaveNum))
					{
						estCombos.intUltimoIndice = IntI;
						blnEncontrado = true;
						break;
					}
				}
			}
			if (blnEncontrado)
			{
				cboCombo.SelectedIndex = IntI;
			} else
			{
				Interaction.Beep();
				subInicializaEstructura(cboCombo);
			}
			//UPGRADE_WARNING: (6021) Casting 'byte' to Enum may cause different behaviour.
			KeyAscii = (Keys) 0;
		}
		
		static private void  subInicializaEstructura( ComboBox cboCombo)
		{
			estCombos.cboCombo = cboCombo;
			estCombos.intUltimoIndice = -1;
			estCombos.strLlaveAlfa = "";
			estCombos.strLlaveNum = "";
		}
		
		static private string funObtenSeccionNumerica( string strTexto)
		{
			StringBuilder strCadArmada = new StringBuilder();
			for (int IntI = 1; IntI <= strTexto.Length; IntI++)
			{
				double dbNumericTemp = 0;
				if (Double.TryParse(Strings.Mid(strTexto, IntI, 1), NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp))
				{
					strCadArmada.Append(Strings.Mid(strTexto, IntI, 1));
				} else
				{
					break;
				}
			}
			return strCadArmada.ToString();
		}
		
		static private string funObtenSeccionAlfaNumerica( string strTexto)
		{
			int IntI = 0;
			//Obtener el índice de la sección numérica y comenzar a partir de allí
			for (IntI = 1; IntI <= strTexto.Length; IntI++)
			{
				double dbNumericTemp = 0;
				if (! Double.TryParse(Strings.Mid(strTexto, IntI, 1), NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp))
				{
					break;
				}
			}
			return Strings.Mid(strTexto, IntI + ((IntI > 1) ? 1: 0), strTexto.Length);
		}
	}
}