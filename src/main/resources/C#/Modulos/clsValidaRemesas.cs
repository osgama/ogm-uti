/************************************************************************
 * Clase creada para Validación de Remesas                              *
 * **********************************************************************
 * Clase que contiene la funcion que recibe los ultimos parametros      *
 * obtenidos y hace la peticion por los datos de validacion             *
 * **********************************************************************
 * INFOWARE                                                             *
 * **********************************************************************
 * Autor: Miguel Angel Garnica Angulo (Infoware, León)                  *
 *                                                                      *
 * **********************************************************************
 * Historico de cambios:                                                *
 *                                                                      *
 * Fecha de Creación:   20/10/09                                        *
 *                      Clase creada para validación de remesas.        *
 * Modificaciones:                                                      *
 *                                                                      *
 * **********************************************************************
 
 */


using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace Masivos
{
    class clsValidaRemesas
    {
        //Funcion que hace la peticion de remesas existentes para validacion
        public string Cadena(string stUltimaCapt, string stUltimaPromo, string stUltimaFechCapt, string stUltimoTipo, string stUltimaFam, string stUltimoConsecutivo)
        {
            string stTrans = "5562";
            string stEmpty = " ";
            string stSubTrans = "21";
            string stSistema = "S753";
            string stCaptura = "  ";
            string stPromotora = "    ";
            string stFechCapt = "        ";
            string stTipoTramite = "  ";
            string stFamilia = "  ";
            string stConsecutivo = "  ";
            string stResTrans = "00";
            string stDescResTrans = stEmpty.PadLeft(50);
            string stEjecu = frmValidaRemesas.stNumEjec;

            string stUCapt = stUltimaCapt;
            string stUPromo = stUltimaPromo;
            string stUFechCapt = stUltimaFechCapt;
            string stUTipo = stUltimoTipo;
            string stUFam = stUltimaFam;
            string stUConsecutivo = stUltimoConsecutivo;
            string strRespuesta = null;

            stEjecu = stEjecu.Trim();
            stEjecu = valida_Tam(stEjecu, 10);

            string strEnvio = stTrans + stEmpty + stSubTrans + stSistema + stCaptura + stPromotora + stFechCapt + stTipoTramite +
                              stFamilia + stConsecutivo + stEmpty.PadLeft(18) + stResTrans + stDescResTrans + stEjecu + stEmpty +
                              stUCapt + stUPromo + stUFechCapt + stUTipo + stUFam + stUConsecutivo + stEmpty.PadLeft(44);

            mdlComunica.gvMensaje = strEnvio;
            mdlGlobales.subRegBitacora("E");
            strRespuesta = mdlComunica.funCON(strEnvio);

            if (strRespuesta.IndexOf("SEG") > -1)
            {
                mdlComunica.gvMensaje = strEnvio;
                mdlGlobales.subRegBitacora("E");
                strRespuesta = mdlComunica.funCON(strEnvio);
            }
            
            //Condicion que verifica que la cadena de respuesta no este vacia
            if (strRespuesta == null || strRespuesta=="")
            {
                MessageBox.Show("(" + " " + ") " + "Error en la comunicación", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            //Condicion que verifica que la transaccion sea la correcta
            if (strRespuesta.Substring(0, 4) != stTrans)
            {
                MessageBox.Show("Transacción no valida", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            //Condicion que verifica que la respuesta no contenga errores, sino los muestra en pantalla.
            if (strRespuesta.Substring(49, 2) != "00")
            {
                MessageBox.Show("Error: " + "(" + strRespuesta.Substring(51, 50) + ")", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            else
            {
                return strRespuesta;                
            }
        }
        //Funcion que envia errores si los hubo en la remesa, sino envia la remesa que fue validada correctamente.
        public string Validacion(int iFlag, int iError, int iTotalErrores, string stDatos)
        {
            string stTrans = "5562";
            string stEmpty = " ";
            string stSubTrans = "61";
            string stSistema = "S753";
            string stCaptura = "  ";
            string stPromotora = "    ";
            string stFechCapt = "        ";
            string stTipoTramite = "  ";
            string stFamilia = "  ";
            string stConsecutivo = "  ";
            string stResTrans = "00";
            string stDescResTrans = stEmpty.PadLeft(50);
            string stEjecu = frmValidaRemesas.stNumEjec;
            string stRespuesta = null;

            stEjecu = stEjecu.Trim();
            stEjecu = valida_Tam(stEjecu, 10);

            string stEnvio = stTrans + stEmpty + stSubTrans + stSistema + stCaptura + stPromotora + stFechCapt + stTipoTramite +
                stFamilia + stConsecutivo + stEmpty.PadLeft(18) + stResTrans + stDescResTrans + stEjecu + iFlag.ToString() + iError.ToString() +
                stEmpty.PadLeft(63) + iTotalErrores.ToString().PadLeft(4, '0') + stEmpty.PadLeft(4) + stDatos;

            mdlComunica.gvMensaje = stEnvio;
            mdlGlobales.subRegBitacora("E");
            stRespuesta = Masivos.mdlComunica.funCON(stEnvio);

            if (stRespuesta.IndexOf("SEG") > -1)
            {
                mdlComunica.gvMensaje = stEnvio;
                mdlGlobales.subRegBitacora("E");
                stRespuesta = mdlComunica.funCON(stEnvio);
            } 

            //Condicion que verifica si la cadena de respuesta esta vacio
            if (stRespuesta == null || stRespuesta == "")
            {
                MessageBox.Show("(" + " " + ") " + "Error en la comunicación", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            //Condicion que valida si la cadena de respuesta contiene errores, si es asi los muestra en pantalla
            if (stRespuesta.Substring(49, 2) != "00")
            {
                MessageBox.Show("Error: " + "(" + stRespuesta.Substring(51, 50) + ")", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            else
            {
                return stRespuesta;
            }
        }
        public string Preimpreso(string stFolios)
        {
            string stTrans = "5562";
            string stEmpty = " ";
            string stSubTrans = "22";
            string stSistema = "S753";
            string stCaptura = "  ";
            string stPromotora = "    ";
            string stFechCapt = "        ";
            string stTipoTramite = "  ";
            string stFamilia = "  ";
            string stConsecutivo = "  ";
            string stResTrans = "00";
            string stDescResTrans = stEmpty.PadLeft(50);
            string stEjecu = frmValidaRemesas.stNumEjec;
            int iFlag = 0;
            string stRespuesta = null;
            

            string stEnvio = stTrans + stEmpty + stSubTrans + stSistema + stCaptura + stPromotora + stFechCapt + stTipoTramite +
                //stFamilia + stConsecutivo + stEmpty.PadLeft(18) + stResTrans + stDescResTrans + stEjecu + iFlag.ToString() +
                stFamilia + stConsecutivo + stEmpty.PadLeft(19) + stResTrans + stDescResTrans + stEjecu + iFlag.ToString() +
                //stEmpty.PadLeft(64) + stFolios;
                stEmpty.PadLeft(63) + stFolios;

            mdlComunica.gvMensaje = stEnvio;
            mdlGlobales.subRegBitacora("E");
            stRespuesta = Masivos.mdlComunica.funCON(stEnvio);

            if (stRespuesta.IndexOf("SEG") > -1)
            {
                mdlComunica.gvMensaje = stEnvio;
                mdlGlobales.subRegBitacora("E");
                stRespuesta = mdlComunica.funCON(stEnvio);
            }

            //Condicion que verifica si la cadena de respuesta esta vacio
            if (stRespuesta == null || stRespuesta == "")
            {
                MessageBox.Show("Error en la comunicación", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            //Condicion que valida si la cadena de respuesta contiene errores, si es asi los muestra en pantalla
            //if (stRespuesta.Substring(49, 2) != "00")
            if (stRespuesta.Substring(50, 2) != "00")
            {
                //MessageBox.Show("Error: " + "(" + stRespuesta.Substring(51, 50) + ")", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Error: " + "(" + stRespuesta.Substring(52, 50) + ")", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            else
            {
                return stRespuesta;
            }
        }

        private string valida_Tam(string stString, int iTam)
        {
            while (stString.Length < iTam)
                stString = "0" + stString;

            return stString;
        }
    }
}
