using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Masivos
{
    class clsCapFolFisico
    {
        public string ValidacionFisica(int iError, int iNumFol)
        {
            string stTrans = "5562";
            string stEmpty = " ";
            string stSubTrans = "62";
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
            string stRespuesta = null;

            stEjecu = stEjecu.Trim();
            stEjecu = valida_Tam(stEjecu, 10);

            string stEnvio = stTrans + stEmpty + stSubTrans + stSistema + stCaptura + stPromotora + stFechCapt + stTipoTramite +
                //stFamilia + stConsecutivo + stEmpty.PadLeft(18) + stResTrans + stDescResTrans + stEjecu + stEmpty +
                stFamilia + stConsecutivo + stEmpty.PadLeft(19) + stResTrans + stDescResTrans + stEjecu + stEmpty +
                //iError.ToString() + stEmpty.PadLeft(63) + iNumFol.ToString().PadLeft(8, '0');
                iError.ToString() + stEmpty.PadLeft(62) + iNumFol.ToString().PadLeft(8, '0');

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
        public string ConsultaFolios()
        {
            string stTrans = "5562";
            string stEmpty = " ";
            string stSubTrans = "20";
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
            string stRespuesta = null;

            stEjecu = stEjecu.Trim();
            stEjecu = valida_Tam(stEjecu, 10);

            string stEnvio = stTrans + stEmpty + stSubTrans + stSistema + stCaptura + stPromotora + stFechCapt + stTipoTramite +
                //stFamilia + stConsecutivo + stEmpty.PadLeft(18) + stResTrans + stDescResTrans + stEjecu + stEmpty + stEmpty.PadLeft(64);
                stFamilia + stConsecutivo + stEmpty.PadLeft(19) + stResTrans + stDescResTrans + stEjecu + stEmpty + stEmpty.PadLeft(63);

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
