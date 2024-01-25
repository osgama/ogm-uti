/************************************************************************
 * Clase creada para hacer la peticion de Errores en la Validación      *
 * de Remesas                                                           *
 * **********************************************************************
 * Clase que contiene la funcion que recibe los ultimos parametros      *
 * obtenidos y hace la peticion por los datos de errores en validacion  *
 * **********************************************************************
 * INFOWARE                                                             *
 * **********************************************************************
 * Autor: Miguel Angel Garnica Angulo (Infoware, León)                  *
 *                                                                      *
 * **********************************************************************
 * Historico de cambios:                                                *
 *                                                                      *
 * Fecha de Creación:   22/10/09                                        *
 *                      Clase creada para mostrar errores en validacion.*
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
    class clsErroresValidacion
    {
        //Funcion que hace la peticion de errores en la remesa que fue seleccionada
        public string Errores(string stUltimoFolio, string stUltimoConsErrFolio)
        {
            string stTrans = "5562";
            string stEmpty = " ";
            string stSubTrans = "23";
            string stSistema = "S753";
            string stCaptura = frmValidaRemesas.arrEmpCapt[frmValidaRemesas.iCell];
            string stPromotora = frmValidaRemesas.arrEmpPromo[frmValidaRemesas.iCell];
            string stFechCapt = frmValidaRemesas.arrFecCapt[frmValidaRemesas.iCell];
            string stTipoTramite = frmValidaRemesas.arrTipoTram[frmValidaRemesas.iCell];
            string stFamilia = frmValidaRemesas.arrFamProd[frmValidaRemesas.iCell];
            string stConsecutivo = frmValidaRemesas.arrConsCapt[frmValidaRemesas.iCell];
            string stResTrans = "00";
            string stDescResTrans = stEmpty.PadLeft(50);
            string stEjecu = frmValidaRemesas.stNumEjec;

            string stUFolio = stUltimoFolio;
            string stUConsErrFolio = stUltimoConsErrFolio;
            string strEnvio = null;
            string strRespuesta = null;

            stEjecu = stEjecu.Trim();
            stEjecu = valida_Tam(stEjecu, 10);

            strEnvio = stTrans + stEmpty + stSubTrans + stSistema + stCaptura + stPromotora + stFechCapt + stTipoTramite + stFamilia +
                       //stConsecutivo + stEmpty.PadLeft(18) + stResTrans + stDescResTrans + stEjecu + stEmpty + stUFolio +
                       stConsecutivo + stEmpty.PadLeft(19) + stResTrans + stDescResTrans + stEjecu + stEmpty + stUFolio +
                       //stUConsErrFolio + stEmpty.PadLeft(46);
                       stUConsErrFolio + stEmpty.PadLeft(45);

            mdlComunica.gvMensaje = strEnvio;
            mdlGlobales.subRegBitacora("E");
            strRespuesta = Masivos.mdlComunica.funCON(strEnvio);

            if (strRespuesta.IndexOf("SEG") > -1)
            {
                mdlComunica.gvMensaje = strEnvio;
                mdlGlobales.subRegBitacora("E");
                strRespuesta = mdlComunica.funCON(strEnvio);
            }            

            //Condicion que verifica si la cadena de respuesta no esta vacia
            if (strRespuesta == null || strRespuesta == "")
            {
                MessageBox.Show("(" + " " + ") " + "Error en la comunicación", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            //Condicion que verifica si la transaccion de respuesta es la correcta
            if (strRespuesta.Substring(0, 4) != stTrans)
            {
                MessageBox.Show("Transacción no valida", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            //Condicion que verifica si la respuesta no contiene errores, sino los muestra en pantalla
            if (strRespuesta.Substring(49, 2) != "00")
            {
                MessageBox.Show("Error: (" + strRespuesta.Substring(51, 50) + ")", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            else
            {
                return strRespuesta;
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
