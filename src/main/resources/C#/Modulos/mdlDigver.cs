using Artinsoft.VB6.Utils; 
using Microsoft.VisualBasic; 
using System; 
using VB6 = Microsoft.VisualBasic.Compatibility.VB6.Support; 

namespace Masivos
{
	class mdlDigver
	{
	
		static int[, ] multijp = null;
		
		static public void  cargamulti()
		{
			multijp = new int[5, 4];
			
			multijp[0, 0] = 0;
			multijp[0, 1] = 5000;
			multijp[0, 2] = 500;
			
			multijp[1, 0] = 5001;
			multijp[1, 1] = 25000;
			multijp[1, 2] = 1000;
			
			multijp[2, 0] = 25001;
			multijp[2, 1] = 50000;
			multijp[2, 2] = 2500;
			
			multijp[3, 0] = 50001;
			multijp[3, 1] = 99999999;
			multijp[3, 2] = 5000;
			
		}
		
		//Funcion modificada para usar las funciones de la DLL
		static public int DigVer_Valida_DigVer_cta( int ICta,  string Cta)
		{
			// Esta funcion valida el digito verificador del numero de cuenta Cta, de
			// acuerdo con el tipo de esta, el cual es indicado en ICta. ICta=1 para
			// tarjeta de credito, ICta=2 para cuenta de cheques, ICta=3 para cuenta de
			// ahorros, ICta=4 para valores, ICta=5 para numero de negocio afiliado a
			// BANAMEX o sucursal BANAMEX. Regresa True si el digito verificador es
			// correcto y False en caso contrario.
			
			string aux = String.Empty;
			
			try
			{
					
					aux = Strings.Mid(Cta, 1, Cta.Length - 1);
					aux = aux + "0";
					
					switch(ICta)
					{
						case 1 : 
							mdlComunica.Digver_AdiTC(aux); 
							break;
						case 2 : 
							mdlComunica.Digver_AdiChe(aux); 
							break;
						case 3 : 
							mdlComunica.Digver_AdiCheCiti(aux); 
							break;
						case 5 : 
							Digver_Adig_Neg_Suc(aux); 
							break;
					}
					
					if (Cta == aux)
					{
						return -1;
					} else
					{
						return 0;
					}
				}
			catch 
			{
			}
			
			
			mdlGlobales.subDespMensajes("EN DIGVER_VALIDA_DIGVER_CTA: " + Microsoft.VisualBasic.Conversion.ErrorToString() + ". " + "NEXUS (Digver).");
			return 0;
		}
		
		static public string Digver_Adig_Neg_Suc( string NegSuc)
		{
			// Esta funcion recibe en NegSuc el numero de negocio o suscursal Banamex,
			// sin digito verificador, es decir, el digito de la extrema derecha, lo
			// calcula y regresa el numero recibido en el parametro NegSuc incluyendo
			// el digito verificador.
			
			int j = 0;
			int k = 0;
			int d = 0;
			int s = 0;
			int Sum = 0;
			string aux = String.Empty;
			
			try
			{
					
					k = 0;
					Sum = 0;
					for (int i = 1; i <= NegSuc.Length; i++)
					{
						k++;
						d = StringsHelper.IntValue(Strings.Mid(NegSuc, NegSuc.Length + 1 - i, 1));
						if ((k % 2) == 0)
						{
							j = 1;
						} else
						{
							j = 2;
						}
						d *= j;
						aux = Conversion.Str(d);
						aux = StringsHelper.MidAssignment(aux, 1, "0");
						d = 0;
						for (int l = 1; l <= 2; l++)
						{
							s = StringsHelper.IntValue(Strings.Mid(aux, l, 1));
							d += s;
						}
						Sum += d;
					}
					s = Sum % 10;
					if (s == 0)
					{
						d = 0;
					} else
					{
						d = 10 - s;
					}
					aux = Conversion.Str(d);
					aux = Strings.Mid(aux, 2, aux.Length - 1);
					
					return NegSuc + aux;
				}
			catch 
			{
				
				
				//UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
				string tempRefParam = "EN DIGVER_ADIG_NEG_DUC: " + Microsoft.VisualBasic.Conversion.ErrorToString() + ". ";
				MsgBoxStyle tempRefParam2 = (MsgBoxStyle) (((int) MsgBoxStyle.Exclamation) + ((int) MsgBoxStyle.OkOnly));
				mdlGlobales.subDespErrores(ref tempRefParam, ref tempRefParam2);
			}
			return String.Empty;
		}
	}
}