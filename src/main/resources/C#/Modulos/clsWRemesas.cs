using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Masivos
{
    public class clsWRemesas
    {
        /********************************************************************************************************************************** 
         * CONSULTA REMESAS EN ESTATUS DE INSPECCION - SERVICIO 5562-20  
         * 
         * DESCRIPCION: METODO QUE SOLICITA EL ALTA POR CAPTURA DE FOLIO FISICO
        /**********************************************************************************************************************************/
        public string ConsultaFolios5562_20()
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
                stFamilia + stConsecutivo + stEmpty.PadLeft(19) + stResTrans + stDescResTrans + stEjecu + stEmpty + stEmpty.PadLeft(63);


            //MODIF MAP 2011/06/09 QUE SE ESCRIBA EN BITACORA QUE PROCESO LLEVA
            mdlComunica.gvMensaje = "*** CONSULTA DE CANTIDAD DE FOLIOS DE LA REMESA ***";
            mdlGlobales.subRegBitacora("E");

            mdlComunica.gvMensaje = stEnvio;
            mdlGlobales.subRegBitacora("E");
            stRespuesta = Masivos.mdlComunica.funCON(stEnvio);

            if (stRespuesta.IndexOf("SEG; VUELV") == -1)
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
            if (stRespuesta.Substring(50, 2) != "00")
            {
                MessageBox.Show("Error: " + "(" + stRespuesta.Substring(52, 50) + ")", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            else
            {
                return stRespuesta;
            }
        }

        /********************************************************************************************************************************** 
         * CONSULTA REMESAS EXISTENTES PARA VALIDACION - SERVICIO 5562-21  
         * stUltimaCapt         = 
         * stUltimaPromo        = 
         * stUltimaFechCapt     = 
         * stUltimoTipo         = 
         * stUltimaFam          = 
         * stUltimoConsecutivo  =
         * 
         * DESCRIPCION: METODO QUE SOLICITA TODAS LAS REMESAS QUE ESTAN LISTAS PARA VALIDACION Y VER ERRORES QUE SE
         *              GENERARON EN EL ARRIBO DE REMESAS DEL ARCHIVO DE CAPTURA
        /**********************************************************************************************************************************/
        //public string Cadena(string stUltimaCapt, string stUltimaPromo, string stUltimaFechCapt, string stUltimoTipo, string stUltimaFam, string stUltimoConsecutivo)
        public string ValidacionRemesasExistentes5562_21(string stUltimaCapt, string stUltimaPromo, string stUltimaFechCapt, string stUltimoTipo, string stUltimaFam, string stUltimoConsecutivo)
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
                              stFamilia + stConsecutivo + stEmpty.PadLeft(19) + stResTrans + stDescResTrans + stEjecu + stEmpty +
                              stUCapt + stUPromo + stUFechCapt + stUTipo + stUFam + stUConsecutivo + stEmpty.PadLeft(43);

            //MODIF MAP 2011/06/09 QUE SE ESCRIBA EN BITACORA QUE PROCESO LLEVA
            mdlComunica.gvMensaje = "*** CONSULTA REMESAS EN STATUS DE VALIDACION ***";
            mdlGlobales.subRegBitacora("E");

            mdlComunica.gvMensaje = strEnvio;
            mdlGlobales.subRegBitacora("E");
            strRespuesta = mdlComunica.funCON(strEnvio);

            if (strRespuesta.IndexOf("SEG; VUELV") == -1)
                if (strRespuesta.IndexOf("SEG") > -1)
                {
                    mdlComunica.gvMensaje = strEnvio;
                    mdlGlobales.subRegBitacora("E");
                    strRespuesta = mdlComunica.funCON(strEnvio);
                }

            //Condicion que verifica que la cadena de respuesta no este vacia
            if (strRespuesta == null || strRespuesta == "" || strRespuesta.Length < 4 )
            {
                MessageBox.Show("(" + " " + ") " + "Error en la comunicación", "C753 ARIES - Validación de Remesas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            //Condicion que verifica que la transaccion sea la correcta
            if (strRespuesta.Substring(0, 4) != stTrans)
            {
                MessageBox.Show(strRespuesta, "C753 ARIES - Validación de Remesas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            //Condicion que verifica que la respuesta no contenga errores, sino los muestra en pantalla.
            if (strRespuesta.Substring(50, 2) != "00")
            {
                MessageBox.Show(strRespuesta.Substring(52, 50), "C753 ARIES - Validación de Remesas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
            else
            {
                return strRespuesta;
            }
        }

        /********************************************************************************************************************************** 
         * CONSULTA ERRORES EN REMESA SELECCIONADA - SERVICIO 5562-22           
         * stUltimoFolio         = 
         * stUltimoConsErrFolio  = 
         * 
         * DESCRIPCION: METODO QUE SOLICITA TODAS LAS REMESAS QUE ESTAN LISTAS PARA VALIDACION Y VER ERRORES QUE SE
        /**********************************************************************************************************************************/
        public string ConsultaPreimpresoAries5562_22(string stFolios)
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

            stEjecu = stEjecu.Trim();

            stEjecu = valida_Tam(stEjecu, 10);

            string stEnvio = stTrans + stEmpty + stSubTrans + stSistema + stCaptura + stPromotora + stFechCapt + stTipoTramite +
                stFamilia + stConsecutivo + stEmpty.PadLeft(19) + stResTrans + stDescResTrans + stEjecu + iFlag.ToString() +
                stEmpty.PadLeft(63) + stFolios;

            //MODIF MAP 2011/06/09 QUE SE ESCRIBA EN BITACORA QUE PROCESO LLEVA
            mdlComunica.gvMensaje = "*** CONSULTA DE FOLIO PREIMPRESO EN ARIES Y TABLA TEMPORAL ***";
            mdlGlobales.subRegBitacora("E");

            mdlComunica.gvMensaje = stEnvio;
            mdlGlobales.subRegBitacora("E");
            stRespuesta = Masivos.mdlComunica.funCON(stEnvio);

            if (stRespuesta.IndexOf("SEG; VUELV") == -1)
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
            if (stRespuesta.Substring(0, 4) != stTrans)
            {
                MessageBox.Show("Error: " + stRespuesta, "C753 ARIES - Validación de Remesas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            //Condicion que valida si la cadena de respuesta contiene errores, si es asi los muestra en pantalla            
            if (stRespuesta.Substring(50, 2) != "00")
            {                
                MessageBox.Show("Error: " + "(" + stRespuesta.Substring(52, 50) + ")", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            else
            {
                return stRespuesta;
            }
        }

        /********************************************************************************************************************************** 
         * CONSULTA ERRORES EN REMESA SELECCIONADA - SERVICIO 5562-23           
         * stUltimoFolio         = 
         * stUltimoConsErrFolio  = 
        /**********************************************************************************************************************************/
        /*public string ErroresRemesa5562_23v2(string stHeader)
        {
            string stClave_tran = "5562";
            string stSub_clave_tran = "23";
            string stSistema = "S753";
            string stRespuesta = null;
            string stEnvio;
            string sEmptyStr = " ";
            string stEspacios;

            if (stHeader == null)
            {
                stEspacios = sEmptyStr.PadLeft(39);
                stEnvio = stClave_tran + " " + stSub_clave_tran + stSistema + stEspacios + "00";
                stEspacios = sEmptyStr.PadLeft(50);
                stEnvio += stEspacios + stEjecutivo;
                stEspacios = sEmptyStr.PadLeft(64);
                stEnvio += stEspacios;
            }
            else
                stEnvio = stHeader;

            mdlComunica.gvMensaje = stEnvio;
            mdlGlobales.subRegBitacora("E");
            stRespuesta = mdlComunica.funCON(stEnvio);

            if (stRespuesta.IndexOf("SEG") > -1)
            {
                mdlComunica.gvMensaje = stEnvio;
                mdlGlobales.subRegBitacora("E");
                stRespuesta = mdlComunica.funCON(stEnvio);
            }

            if (stRespuesta == "" || stRespuesta == null) //Respuesta Vacia
            {
                MessageBox.Show("Error de comunicación", "S753 ARIES - Validación de Remesas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            if (stRespuesta.Substring(0, 4) != stClave_tran) //No corresponde clave de Transaccion
            {
                stRespuesta = stRespuesta.Trim();
                MessageBox.Show("ERROR: " + stRespuesta, "C753 ARIES - Validación de Remesas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            if (stRespuesta.Substring(50, 2) == "00") //Respuesta Exitosa
            {
                return (stRespuesta);
            }
            else
            {
                MessageBox.Show(stRespuesta.Substring(52, 50), "C753 ARIES - Validación de Remesas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

        }*/



        /********************************************************************************************************************************** 
         * CONSULTA ERRORES EN REMESA SELECCIONADA - SERVICIO 5562-23           
         * stUltimoFolio         = 
         * stUltimoConsErrFolio  = 
        /**********************************************************************************************************************************/
        public string ErroresRemesa5562_23(string stUltimoFolio, string stUltimoConsErrFolio, string stMasInfo)
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
                stConsecutivo + stEmpty.PadLeft(19) + stResTrans + stDescResTrans + stEjecu + stMasInfo + stUFolio +
                stUConsErrFolio + stEmpty.PadLeft(45);

            //MODIF MAP 2011/06/09 QUE SE ESCRIBA EN BITACORA QUE PROCESO LLEVA
            mdlComunica.gvMensaje = "*** CONSULTA REMESA EN STATUS VALIDACION Y CON ERROR ***";
            mdlGlobales.subRegBitacora("E");

            mdlComunica.gvMensaje = strEnvio;
            mdlGlobales.subRegBitacora("E");
            strRespuesta = Masivos.mdlComunica.funCON(strEnvio);

            if (strRespuesta.IndexOf("SEG; VUELV") == -1)
                if (strRespuesta.IndexOf("SEG") > -1)
                {
                    mdlComunica.gvMensaje = strEnvio;
                    mdlGlobales.subRegBitacora("E");
                    strRespuesta = mdlComunica.funCON(strEnvio);
                }

            //Condicion que verifica si la cadena de respuesta no esta vacia
            if (strRespuesta == null || strRespuesta == "" || strRespuesta.Length < 4 )
            {
                MessageBox.Show("(" + " " + ") " + "Error en la comunicación", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            //Condicion que verifica si la transaccion de respuesta es la correcta
            if (strRespuesta.Substring(0, 4) != stTrans)
            {
                MessageBox.Show(strRespuesta, "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            //Condicion que verifica si la respuesta no contiene errores, sino los muestra en pantalla
            if (strRespuesta.Substring(50, 2) != "00")
            {
                MessageBox.Show("Error: (" + strRespuesta.Substring(52, 50) + ")", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            else
            {
                return strRespuesta;
            }
        }


        /********************************************************************************************************************************** 
         * CONSULTA REMESAS EN ESTATUS DE INSPECCION - SERVICIO 5562-24  
         * stEjecutivo    = Nomina
         * stHeader       = Header Original Si Existe Pagineo, Null Si ya no hay mas informacion
        /**********************************************************************************************************************************/
        public string ConsultaRemesas5562_24(string stEjecutivo, string stHeader)
        {
            string stClave_tran = "5562";
            string stSub_clave_tran = "24";
            string stSistema = "S753";
            string stRespuesta = null;
            string stEnvio;
            string sEmptyStr = " ";
            string stEspacios;

            if (stHeader == null)
            {
                stEspacios = sEmptyStr.PadLeft(39);
                stEnvio = stClave_tran + " " + stSub_clave_tran + stSistema + stEspacios + "00";
                stEspacios = sEmptyStr.PadLeft(50);
                stEnvio += stEspacios + stEjecutivo;
                stEspacios = sEmptyStr.PadLeft(64);
                stEnvio += stEspacios;
            }
            else
                stEnvio = stHeader;

            //MODIF MAP 2011/06/09 QUE SE ESCRIBA EN BITACORA QUE PROCESO LLEVA
            mdlComunica.gvMensaje = "*** CONSULTA DE REMESAS PARA INSPECCION";
            mdlGlobales.subRegBitacora("E");

            mdlComunica.gvMensaje = stEnvio;
            mdlGlobales.subRegBitacora("E");
            stRespuesta = mdlComunica.funCON(stEnvio);

            if (stRespuesta.IndexOf("SEG; VUELV") == -1)
                if (stRespuesta.IndexOf("SEG") > -1)
                {
                    mdlComunica.gvMensaje = stEnvio;
                    mdlGlobales.subRegBitacora("E");
                    stRespuesta = mdlComunica.funCON(stEnvio);
                }

            if (stRespuesta == "" || stRespuesta == null) //Respuesta Vacia
            {
                MessageBox.Show("Error de comunicación", "S753 ARIES - Inspección de Remesas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            if (stRespuesta.Substring(0, 4) != stClave_tran) //No corresponde clave de Transaccion
            {
                stRespuesta = stRespuesta.Trim();
                MessageBox.Show(stRespuesta, "C753 ARIES - Inspección de Remesas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            
            if (stRespuesta.Substring(50, 2) == "00") //Respuesta Exitosa
            {
                return (stRespuesta);
            }
            else
            {
                if (stRespuesta.Substring(50, 2) == "02") // No Existen Remesas
                    MessageBox.Show(stRespuesta.Substring(52, 50), "S753 ARIES - Inspección de Remesas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else

                    MessageBox.Show(stRespuesta.Substring(52, 50), "S753 ARIES - Inspección de Remesas", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }
        }

        /********************************************************************************************************************************** 
         * CONSULTA FOLIOS A INSPECCIONAR - SERVICIO 5562-25
         * stRemesa       = Numero de Remesa         
         * stHeader       = Header Original Si Existe Pagineo, Null Si ya no hay mas informacion
        /**********************************************************************************************************************************/
        public string ConsultaFolioRemesas5562_25(string stEjecutivo, string stRemesa, string stHeader)
        {
            string stClave_tran = "5562";
            string stSub_clave_tran = "25";
            string stSistema = "S753";
            string stRespuesta = null;
            string stEnvio;
            string sEmptyStr = " ";
            string stEspacios;

            if (stHeader == null)
            {
                stEspacios = sEmptyStr.PadLeft(19);
                stEnvio = stClave_tran + " " + stSub_clave_tran + stSistema + stRemesa + stEspacios + "00";
                stEspacios = sEmptyStr.PadLeft(50);
                stEnvio += stEspacios + stEjecutivo;
                stEspacios = sEmptyStr.PadLeft(64);
                stEnvio += stEspacios;
            }
            else
                stEnvio = stHeader;

            //MODIF MAP 2011/06/09 QUE SE ESCRIBA EN BITACORA QUE PROCESO LLEVA
            mdlComunica.gvMensaje = "*** CONSULTA DE FOLIOS A INSPECCIONAR ***";
            mdlGlobales.subRegBitacora("E");
            mdlComunica.gvMensaje = stEnvio;
            mdlGlobales.subRegBitacora("E");
            stRespuesta = mdlComunica.funCON(stEnvio);

            if (stRespuesta.IndexOf("SEG; VUELV") == -1)
                if (stRespuesta.IndexOf("SEG") > -1)
                {
                    mdlComunica.gvMensaje = stEnvio;
                    mdlGlobales.subRegBitacora("E");
                    stRespuesta = mdlComunica.funCON(stEnvio);
                }

            if (stRespuesta == "" || stRespuesta == null)
            {
                MessageBox.Show("Error de comunicacion ", "C753 ARIES - Inspección de Folios", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            if (stRespuesta.Substring(0, 4) != stClave_tran)
            {
                stRespuesta = stRespuesta.Trim();
                MessageBox.Show("ERROR: " + stRespuesta, "C753 ARIES - Inspección de Folios", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            if (stRespuesta.Substring(50, 2) == "00")
            {
                return (stRespuesta);
            }
            else
            {
                MessageBox.Show(stRespuesta.Substring(52, 50), "C753 ARIES - Inspección de Folios", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /********************************************************************************************************************************** 
         * CONSULTA DATOS FOLIOS A INSPECCIONAR - SERVICIO 5562-26
         * stRemesa       = Numero de Remesa
         * stPreimpreso   = Folio Preimpreso
         * stHeader       = Header Original Si Existe Pagineo, Null Si ya no hay mas informacion
        /**********************************************************************************************************************************/
        public string ConsultaDatosInspeccionar5562_26(string stRemesa, string stEjecutivo, string stPreimpreso, string stHeader)
        {
            string stClave_tran = "5562";
            string stSub_clave_tran = "26";
            string stSistema = "S753";
            string stRespuesta = null;
            string stEnvio;
            string sEmptyStr = " ";
            string stEspacios;
            // HEADER
            if (stHeader == null)
            {
                stEspacios = sEmptyStr.PadLeft(19);
                stEnvio = stClave_tran + " " + stSub_clave_tran + stSistema + stRemesa + stEspacios + "00";
                stEspacios = sEmptyStr.PadLeft(50);
                stEnvio += stEspacios + stEjecutivo;
                stEspacios = sEmptyStr.PadLeft(64);
                stEnvio += stEspacios;
            }
            else
                stEnvio = stHeader;
            //DATOS
            stEnvio += stPreimpreso;

            //MODIF MAP 2011/06/09 QUE SE ESCRIBA EN BITACORA QUE PROCESO LLEVA
            mdlComunica.gvMensaje = "*** CONSULTA DE DATOS FOLIOS A INSPECCIONAR ***";
            mdlGlobales.subRegBitacora("E");

            mdlComunica.gvMensaje = stEnvio;
            mdlGlobales.subRegBitacora("E");
            stRespuesta = mdlComunica.funCON(stEnvio);

            if (stRespuesta.IndexOf("SEG; VUELV") == -1)
                if (stRespuesta.IndexOf("SEG") > -1)
                {
                    mdlComunica.gvMensaje = stEnvio;
                    mdlGlobales.subRegBitacora("E");
                    stRespuesta = mdlComunica.funCON(stEnvio);
                }


            if (stRespuesta != null && stRespuesta != "")
            {
                if (stRespuesta.Substring(0, 4) == stClave_tran)
                {
                    if (stRespuesta.Substring(50, 2) == "00")
                    {
                        return stRespuesta;
                    }
                    else
                        MessageBox.Show(stRespuesta.Substring(52, 50), "S753 ARIES - Inspección de Campo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    stRespuesta = stRespuesta.Trim();
                    MessageBox.Show("ERROR: " + stRespuesta, "C753 ARIES - IInspeccion de Campo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return null;
        }

        /********************************************************************************************************************************** 
         * CONSULTA REMESA EN ESTATUS DE IINSPECCION Y CON CAMBIOS - SERVICIO 5562-27
         * stRemesa       = Numero de Remesa
         *  stHeader       = Header Original Si Existe Pagineo, Null Si ya no hay mas informacion
        /**********************************************************************************************************************************/
        public string ConsultaCambiosRemesa5562_27(string stRemesa, string stEjecutivo, string stHeader)
        {
            string stClave_tran = "5562";
            string stSub_clave_tran = "27";
            string stSistema = "S753";
            string stRespuesta = null;
            string stEnvio;
            string sEmptyStr = " ";
            string stEspacios = null;
            //HEADER
            if (stHeader == null)
            {
                stEspacios = sEmptyStr.PadLeft(19);
                stEnvio = stClave_tran + " " + stSub_clave_tran + stSistema + stRemesa + stEspacios + "00";
                stEspacios = sEmptyStr.PadLeft(50);
                stEnvio += stEspacios + stEjecutivo + "0";
                stEspacios = sEmptyStr.PadLeft(63);
                stEnvio += stEspacios;
            }
            else
                stEnvio = stHeader;

            //MODIF MAP 2011/06/09 QUE SE ESCRIBA EN BITACORA QUE PROCESO LLEVA
            mdlComunica.gvMensaje = "*** CONSULTA REMESA EN STATUS DE INSPECCION Y CON CAMBIOS ***";
            mdlGlobales.subRegBitacora("E");

            mdlComunica.gvMensaje = stEnvio;
            mdlGlobales.subRegBitacora("E");
            stRespuesta = mdlComunica.funCON(stEnvio);

            if (stRespuesta.IndexOf("SEG; VUELV") == -1)
                if (stRespuesta.IndexOf("SEG") > -1)
                {
                    mdlComunica.gvMensaje = stEnvio;
                    mdlGlobales.subRegBitacora("E");
                    stRespuesta = mdlComunica.funCON(stEnvio);
                }

            if (stRespuesta != null && stRespuesta != "")
            {
                if (stRespuesta.Substring(0, 4) == stClave_tran)
                {
                    if (stRespuesta.Substring(50, 2) == "00")
                    {
                        return stRespuesta;
                    }
                    else
                    {
                        if (stRespuesta.Substring(50, 2) == "02")
                            MessageBox.Show(stRespuesta.Substring(52, 50), "S753 ARIES - Inspección de Campo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                        MessageBox.Show(stRespuesta.Substring(52, 50), "S753 ARIES - Inspección de Campo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    stRespuesta = stRespuesta.Trim();
                    MessageBox.Show("ERROR: " + stRespuesta, "C753 ARIES - IInspeccion de Campo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return null;
        }

        /********************************************************************************************************************************** 
         * CONSULTA REMESAS  - SERVICIO 5562-28
         * 
         *   
        /**********************************************************************************************************************************/
        public string EjecutarConsulta5562_28(string stOper, string stRemesa, string stArchivo, string stFecha)
        {
            string stClave_tran = "5562";
            string stSub_clave_tran = "28";
            string stSistema = "S753";
            string stRespuesta = null;
            string stEnvio;
            string sEmptyStr = " ";
            string stEspacios = null;
            //string stOper = " ";
            //string stArchivo = sEmptyStr.PadRight(8);
            //string stRemesa = sEmptyStr.PadRight(22);
            string stNomina = mdlGlobales.gstrNomina.Value.ToString();            

            stNomina = stNomina.Trim();
            stNomina = valida_Tam(stNomina, 10);

            //Header
            stEspacios = sEmptyStr.PadLeft(39);
            stEnvio = stClave_tran + " " + stSub_clave_tran + stSistema + stEspacios + "00";
            stEspacios = sEmptyStr.PadLeft(50);

            stNomina = stNomina.PadRight(10);
            stEnvio += stEspacios + stNomina + "0";
            stEspacios = sEmptyStr.PadLeft(63);
            stEnvio += stEspacios;

            //Body

            stEnvio += stOper + stRemesa + stArchivo + stFecha;
            
            //MODIF MAP 2011/06/09 QUE SE ESCRIBA EN BITACORA QUE PROCESO LLEVA
            mdlComunica.gvMensaje = "*** GENERACION DE ARCHIVO DE REMESAS ***";
            mdlGlobales.subRegBitacora("E");
            mdlComunica.gvMensaje = stEnvio;
            mdlGlobales.subRegBitacora("E");
            stRespuesta = mdlComunica.funCON(stEnvio);

            if (stRespuesta.IndexOf("SEG; VUELV") == -1)
                if (stRespuesta.IndexOf("SEG") > -1)
                {
                    mdlComunica.gvMensaje = stEnvio;
                    mdlGlobales.subRegBitacora("E");
                    stRespuesta = mdlComunica.funCON(stEnvio);
                }
            
            if (stRespuesta != null && stRespuesta != "")
            {
                if (stRespuesta.Substring(0, 4) == stClave_tran)
                {                    
                    if (stRespuesta.Substring(50, 2) == "00")
                        return stRespuesta;
                    else                        
                        MessageBox.Show(stRespuesta.Substring(50, 52), "C753 ARIES - Consulta Remesa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    stRespuesta = stRespuesta.Trim();
                    MessageBox.Show("ERROR: " + stRespuesta, "C753 ARIES - Consulta Remesa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Error de Comunicación Transacción", "C753 ARIES - Consulta Remesa", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //txtResultado.Text += stEnvio + Convert.ToChar(13) + Convert.ToChar(10);
            return null;
        

        }


        /***********************************************************************************************************/
        // FUNCION:                  GuardaRemesas5562_60()
        // PARAMETROS ENTRADA:       ref List <Linea> Lista
        // PARAMETROS SALIDA:        string 
        //					
        // RESULTADO:	              
        //               
        // CREACION:                  10/2009
        // DESCRIPCION:                Metodo  que guarda la informacion de las Remesas
        //                           
        //                             
        // DESARROLLADO POR:          AAHB (Infoware )
        // ULTIMA MODIFICACION:
        // DESCRIPCION:
        //
        /**********************************************************************************************************/
        public string GuardaRemesas5562_60(ref List<Linea> Lista, string stDatos, ref List<Linea> ListaOrg)
        {
            bool   bBandera = true;
            string stClave_tran = "5562";
            string stSub_clave_tran = "60";
            string stSistema = "S753";
            string stRespuesta = null;
            string stEnvio;
            string sEmptyStr = " ";            
            string stEjecu = mdlGlobales.gstrNomina.Value.ToString();            
            int iFlag = new int();
            int iError = new int();            
            string stClvCampo = "1234";
            string stDescCampo;
            
            iFlag = 0;

            string stHeader;
            string stTotErrore;

            stEjecu = stEjecu.Trim();
            stEjecu = stEjecu.PadLeft(10, '0');
            //stEjecu = valida_Tam(stEjecu, 10);

            stHeader = stClave_tran + sEmptyStr + stSub_clave_tran + stSistema;
            for (int iCont = 0; iCont < Lista.Count; iCont++)
            {
                if (Lista[iCont].Remesa.StartsWith("01"))
                {
                    if (Lista[iCont].Errores == 0)
                        iError = 0;
                    else
                        iError = 1;
                    stTotErrore = Lista[iCont].Errores.ToString();

                    string cad = stDatos.Substring(30, 10);
                    stEnvio = stHeader +                            //Transaccion-Filler-Subtransaccion-Sistema                        
                        Lista[iCont].Remesa.Substring(97, 2) +      //EMPRESA CAPTURA MODIFICADA
                        "00" +Lista[iCont].Remesa.Substring(95, 2) +//EMPRESA PROMOTORA MODIFICADA
                        Lista[iCont].Remesa.Substring(87, 8) +      //Fecha Entrega Captura
                        Lista[iCont].Remesa.Substring(99, 2) +      //Tipo Tramite
                        Lista[iCont].Remesa.Substring(103, 2) +     //Familia Producto
                        Lista[iCont].Remesa.Substring(68, 2) +      //Consecutivo Captura
                        sEmptyStr.PadLeft(19) +                     //FILLER
                        "00" +                                      //Valor 00
                        sEmptyStr.PadLeft(50) +                     //Descr. Resultado
                        stEjecu +                                   //Ejecutivo
                        iFlag.ToString() +                          //Flag Informativo
                        iError.ToString() +                         //Indicador de Error
                        sEmptyStr.PadLeft(62) +                     //FILLER
                        //DATOS
                        stDatos.Substring(0, 2) +                   //TipoEntidad
                        stDatos.Substring(2, 4) +                   //Clave Entidad Origen
                        Lista[iCont].Remesa.Substring(16, 16) +     //Folio Preimpreso
                        Lista[iCont].Remesa.Substring(32, 16) +     //Folio Preimpreso Final
                        "0" + Lista[iCont].Remesa.Substring(48, 4) +//Total Folios Remesas
                        stDatos.Substring(10, 10) +                 // Fecha Proceso
                        stDatos.Substring(20, 10) +                 //Fecha Ingreso a Credito
                        stDatos.Substring(30, 10) +                 //Fecha Aceptacion Credito
                        Lista[iCont].Remesa.Substring(68, 2) +      //Numero Consecutivo Remesa
                        "0000" +                                    //Causa de Rechazo          ????
                        stDatos.Substring(6, 4) +                   //Clave Codigo Promocion
                        valida_Tam(stTotErrore, 4);                 //Total Errores

                    if (!Lista[iCont].Existe_Archivo_Remesa)
                    { //no existe archivo  remesa 
                        stClvCampo = valida_Tam(stClvCampo, 4, " ");
                        stDescCampo = valida_Tam("NO SE ENCONTRO ARCHIVO DE REMESA", 40, " ");
                        stEnvio = stEnvio + stClvCampo + stDescCampo;
                    }

                    if (!Lista[iCont].Corresponde_Promotora)
                    { //no concuerda  en el registro de la remesesa empresa promotora

                        stClvCampo = valida_Tam("0", 4, "0");
                        stDescCampo = valida_Tam("NO COINCIDE EMPRESA PROMOTORA", 40, " ");
                        stEnvio = stEnvio + stClvCampo + stDescCampo;
                    }
                    if (!Lista[iCont].Corresponde_Captura)
                    {
                        //no concuerda en el registro de remesa empresa de captura 
                        stClvCampo = valida_Tam("0", 4, "0");
                        stDescCampo = valida_Tam("NO COINCIDE EMPRESA CAPTURA", 40, " ");
                        stEnvio = stEnvio + stClvCampo + stDescCampo;
                    }

                    //MODIF MAP 2011/06/09 QUE SE ESCRIBA EN BITACORA QUE PROCESO LLEVA
                    mdlComunica.gvMensaje = "*** REGISTRO DE RESULTADOS DE ARRIBO DE REMESA ***";
                    mdlGlobales.subRegBitacora("E");

                    mdlComunica.gvMensaje = stEnvio;
                    mdlGlobales.subRegBitacora("E");
                    stRespuesta = mdlComunica.funCON(stEnvio);

                    if (stRespuesta.IndexOf("SEG; VUELV") == -1)
                        if (stRespuesta.IndexOf("SEG") > -1)
                        {
                            mdlComunica.gvMensaje = stEnvio;
                            mdlGlobales.subRegBitacora("E");
                            stRespuesta = mdlComunica.funCON(stEnvio);
                        }

                    
                    if (stRespuesta == "" || stRespuesta == null)
                    {
                        MessageBox.Show("(" + "  " + ")" + "Error de comunicacion ", "Mensaje de sistema  ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }

                    if (stRespuesta.Substring(0, 4) != stClave_tran)
                    {
                        MessageBox.Show("(" + stRespuesta + ")" + "Error", "Mensaje de sistema  ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }

                    if (stRespuesta.Substring(50, 2) != "00")
                    {
                        /*Se guarda en la lista el mensaje de error al guardar la remesa del BACK */
                        //ListaOrg.Add(Lista[iCont]);
                        ListaOrg.Add(new Linea(
                            Lista[iCont].Remesa.Substring(52, 18),
                            Lista[iCont].Existe_Archivo_Remesa,
                            Lista[iCont].Corresponde_Promotora,
                            Lista[iCont].Corresponde_Captura,
                            Lista[iCont].Errores));
                        ListaOrg.Add(new Linea(stRespuesta.Substring(50, 52).Trim() + " + " + Lista[iCont].Remesa.Substring(52, 18),
                            false, false, false, 1));
                        Lista.Add(new Linea("  " + stRespuesta.Substring(52, 50).Trim() + " + " + Lista[iCont].Remesa.Substring(52, 18), false, false, false, 1));                        

                        bBandera = false;

                    }
                    else
                    {
                        // TAMBIEN SE GUARDA EL MENSAJE DE ERROR CORRECTO DEL BACK
                        ListaOrg.Add(new Linea(
                            Lista[iCont].Remesa.Substring(52, 18),
                            Lista[iCont].Existe_Archivo_Remesa,
                            Lista[iCont].Corresponde_Promotora, 
                            Lista[iCont].Corresponde_Captura,
                            Lista[iCont].Errores));
                        //ListaOrg.Add(Lista[iCont]);
                        ListaOrg.Add(new Linea(stRespuesta.Substring(50, 52).Trim() + " + " + Lista[iCont].Remesa.Substring(52, 18),
                            true, true, true, 0));
                        Lista.Add(new Linea(stRespuesta.Substring(50, 52).Trim() + " + " + Lista[iCont].Remesa.Substring(52, 18),
                            true, true, true, 0));
                    }
                }// fin if (Lista[iCont].Remesa.StartsWith("01"))
            } // fin for (int iCont = 0; iCont < Lista.Count; iCont++)
            if (bBandera)
            {                
                return ("ok"); // Si todo sale bien esta correcto  se regresa un ok                             
            }
             return (null); // existio minimo un error en el envio
        }

        public string RegistroResultadosValidacion5562_61(int iFlag, int iError, int iTotalErrores, string stDatos)
        {
            string stTrans = "5562";
            string stEmpty = " ";
            string stSubTrans = "61";
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
                stFamilia + stConsecutivo + stEmpty.PadLeft(19) + stResTrans + stDescResTrans + stEjecu + iFlag.ToString() + iError.ToString() +
                stEmpty.PadLeft(62) + iTotalErrores.ToString().PadLeft(4, '0') + "0000" + stDatos;

            mdlComunica.gvMensaje = stEnvio;
            mdlGlobales.subRegBitacora("E");
            stRespuesta = Masivos.mdlComunica.funCON(stEnvio);

            //MODIF MAP 2011/06/09 QUE SE ESCRIBA EN BITACORA QUE PROCESO LLEVA
            mdlComunica.gvMensaje = "*** REGISTRO DEL RESULTADO DE VALIDACION ***";
            mdlGlobales.subRegBitacora("E");

            if (stRespuesta.IndexOf("SEG; VUELV") == -1)
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
            if (stRespuesta.Substring(50, 2) != "00")
            {
                MessageBox.Show("Error 5562_61: " + "(" + stRespuesta.Substring(52, 50) + ")", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            else
            {
                return stRespuesta;
            }
        }

        public string ValidacionFisica5562_62(int iError, int iNumFol,string stNombre)
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
                stFamilia + stConsecutivo + stEmpty.PadLeft(19) + stResTrans + stDescResTrans + stEjecu + "0" +
                iError.ToString() + stEmpty.PadLeft(62) + iNumFol.ToString().PadLeft(8, '0') + stNombre.PadLeft(8,' ');

            //MODIF MAP 2011/06/09 QUE SE ESCRIBA EN BITACORA QUE PROCESO LLEVA
            mdlComunica.gvMensaje = "*** REGISTRO DEL RESULTADO DE CAPTURA FISICA ***";
            mdlGlobales.subRegBitacora("E");
            mdlComunica.gvMensaje = stEnvio;
            mdlGlobales.subRegBitacora("E");
            stRespuesta = Masivos.mdlComunica.funCON(stEnvio);

            if (stRespuesta.IndexOf("SEG; VUELV") == -1)
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
            if (stRespuesta.Substring(50, 2) != "00")
            {
                MessageBox.Show("Error: " + "(" + stRespuesta.Substring(52, 50) + ")", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            else
            {
                return stRespuesta;
            }
        }

        /**********************************************************************************************************************************
         * REGISTRO DEL RESULTADO DE INSPECCION - SERVICIO 5562-63
         * stRemesa       = Numero de Remesa
         * stPreimpreso   = Folio Preimpreso
         * stFlagInfo     = 0/Unico Grupo o Primer Grupo de N, 1/Grupo Existen Mas Envios
         * stIndCambios   = 0/ Sin Cambios, 1/Con Cambios
         * stTotalCambios = Numero de Cambios en Total
         * stDatos        = Cambios /Tipo Formato "00" Fin de Ocurrencia, ID_Campo "9999", Valor Nuevo del Campo //Tamaño maximo: 1721
        /**********************************************************************************************************************************/
        public bool EnviaRegistroResultadosInspeccion5562_63(string stRemesa, string stEjecutivo, string stPreimpreso, string stFlagInfo,
            string stIndCambios, string stTotalCambios, string stDatos)
        {
            string stClave_tran = "5562";
            string stSub_clave_tran = "63";
            string stSistema = "S753";
            string stEnvio;
            string stRespuesta = null;
            string sEmptyStr = " ";
            string stEspacios;

            // HEADER
            stEspacios = sEmptyStr.PadLeft(19);
            stEnvio = stClave_tran + " " + stSub_clave_tran + stSistema + stRemesa + stEspacios + "00";
            stEspacios = sEmptyStr.PadLeft(50);
            stEnvio += stEspacios + stEjecutivo + stFlagInfo + stIndCambios;
            stEspacios = sEmptyStr.PadLeft(62); //FILLER
            stEnvio += stEspacios;
            //DATOS
            stEnvio += stPreimpreso + stTotalCambios + stDatos;

            //MODIF MAP 2011/06/09 QUE SE ESCRIBA EN BITACORA QUE PROCESO LLEVA
            mdlComunica.gvMensaje = "*** REGISTRO DEL RESULTADO DE INSPECCION ***";
            mdlGlobales.subRegBitacora("E");
            
            mdlComunica.gvMensaje = stEnvio;
            mdlGlobales.subRegBitacora("E");
            stRespuesta = mdlComunica.funCON(stEnvio);

            if (stRespuesta.IndexOf("SEG; VUELV") == -1)
                if (stRespuesta.IndexOf("SEG") > -1)
                {
                    mdlComunica.gvMensaje = stEnvio;
                    mdlGlobales.subRegBitacora("E");
                    stRespuesta = mdlComunica.funCON(stEnvio);
                }

            if (stRespuesta != null && stRespuesta != "")
            {
                if (stRespuesta.Substring(0, 4) == stClave_tran)
                {
                    if (stRespuesta.Substring(50, 2) == "00")
                        return true;
                    else
                        MessageBox.Show(stRespuesta.Substring(52, 50), "S753 ARIES - Inspección de Campos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    stRespuesta = stRespuesta.Trim();
                    MessageBox.Show("ERROR: " + stRespuesta, "C753 ARIES - Inspección de Campos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return false;

        }

        /********************************************************************************************************************************** 
         * REGISTRO DE FIN DE INSPECCION - SERVICIO 5562-64
         * stRemesa       = Numero de Remesa
         *  stHeader       = Header Original Si Existe Pagineo, Null Si ya no hay mas informacion
        /**********************************************************************************************************************************/
        public string RegistroFinInspeccion5562_64(string stRemesa, string stEjecutivo)
        {
            string stClave_tran = "5562";
            string stSub_clave_tran = "64";
            string stSistema = "S753";
            string stRespuesta = null;
            string stEnvio;
            string sEmptyStr = " ";
            string stEspacios = null;

            stEspacios = sEmptyStr.PadLeft(19);
            stEnvio = stClave_tran + " " + stSub_clave_tran + stSistema + stRemesa + stEspacios + "00";
            stEspacios = sEmptyStr.PadLeft(50);
            stEnvio += stEspacios + stEjecutivo;
            stEspacios = sEmptyStr.PadLeft(64);
            stEnvio += stEspacios;

            //MODIF MAP 2011/06/09 QUE SE ESCRIBA EN BITACORA QUE PROCESO LLEVA
            mdlComunica.gvMensaje = "*** REGISTRO DE FIN DE INSPECCION ***";
            mdlGlobales.subRegBitacora("E");

            mdlComunica.gvMensaje = stEnvio;
            mdlGlobales.subRegBitacora("E");
            stRespuesta = mdlComunica.funCON(stEnvio);

            if (stRespuesta.IndexOf("SEG; VUELV") == -1)
                if (stRespuesta.IndexOf("SEG") > -1)
                {
                    mdlComunica.gvMensaje = stEnvio;
                    mdlGlobales.subRegBitacora("E");
                    stRespuesta = mdlComunica.funCON(stEnvio);
                }

            if (stRespuesta != null && stRespuesta != "")
            {
                if (stRespuesta.Substring(0, 4) == stClave_tran)
                {
                    if (stRespuesta.Substring(50, 2) == "00")                    
                        return stRespuesta.Substring(52, 50);
                    
                    else
                        MessageBox.Show(stRespuesta.Substring(52, 50), "S753 ARIES - Inspección de Folios", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    stRespuesta = stRespuesta.Trim();
                    MessageBox.Show("ERROR: " + stRespuesta, "C753 ARIES - Inspección de Folios", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return null;

        }

        /**********************************************************************************************************************************
         * REGISTRO DE ENVIO DE REMESA A ARIES - SERVICIO 5562-65
         * stRemesa       = Numero de Remesa
         * stPreimpreso   = Folio Preimpreso
         * stFlagInfo     = 0/Unico Grupo o Primer Grupo de N, 1/Grupo Existen Mas Envios
         * stIndCambios   = 0/ Sin Cambios, 1/Con Cambios
         * stTotalCambios = Numero de Cambios en Total
         * stDatos        = Cambios /Tipo Formato "00" Fin de Ocurrencia, ID_Campo "9999", Valor Nuevo del Campo //Tamaño maximo: 1721
        /**********************************************************************************************************************************/

        public bool EnviaSolicitudAries5562_65(string stRemesa, string stEjecutivo)
        {
            string stClave_tran = "5562";
            string stSub_clave_tran = "65";
            string stSistema = "S753";
            string stRespuesta = null;
            string stEnvio;
            string sEmptyStr = " ";
            string stEspacios;
            
            stEspacios = sEmptyStr.PadLeft(19);
            stEnvio = stClave_tran + " " + stSub_clave_tran + stSistema + stRemesa + stEspacios + "00";
            stEspacios = sEmptyStr.PadLeft(50);
            stEnvio += stEspacios + stEjecutivo;
            stEspacios = sEmptyStr.PadLeft(64);
            stEnvio += stEspacios;

            //MODIF MAP 2011/06/09 QUE SE ESCRIBA EN BITACORA QUE PROCESO LLEVA
            mdlComunica.gvMensaje = "*** REGISTRO DE ENVIO DE REMESA A ARIES ***";
            mdlGlobales.subRegBitacora("E");

            mdlComunica.gvMensaje = stEnvio;
            mdlGlobales.subRegBitacora("E");
            stRespuesta = mdlComunica.funCON(stEnvio);

            if (stRespuesta.IndexOf("SEG; VUELV") == -1)
                if (stRespuesta.IndexOf("SEG") > -1)
                {
                    mdlComunica.gvMensaje = stEnvio;
                    mdlGlobales.subRegBitacora("E");
                    stRespuesta = mdlComunica.funCON(stEnvio);
                }

            if (stRespuesta != null && stRespuesta != "")
            {
                if (stRespuesta.Substring(0, 4) == stClave_tran)
                {
                    if (stRespuesta.Substring(50, 2) == "00")
                    {
                        return true;
                    }
                    else
                    {                        
                        MessageBox.Show(stRespuesta.Substring(52, 50), "S753 ARIES - Registro de Remesa en ARIES", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    stRespuesta = stRespuesta.Trim();
                    MessageBox.Show("ERROR: " + stRespuesta, "C753 ARIES - Registro de Remesa en ARIES", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return false;
        }



        private string valida_Tam(string stString, int iTam)
        {
            while (stString.Length < iTam)
                stString = "0" + stString;

            return stString;
        }
        
        //Llena Cadena con cualquier Caracter Enviado al Inicio
        private string valida_Tam(string stCad, int iTam, string stCrcCompl)
        {
            if (stCrcCompl.Length != 1)            
                stCrcCompl = stCrcCompl.Substring(0, 1);
            
            int iTamano = new int();
            if (stCad.Length < iTam)
            {
                iTamano = stCad.Length;
                for (int iCfor = 0; iCfor < (iTam - iTamano); iCfor++)
                    stCad = stCrcCompl + stCad;
            }
            return stCad;
        }
    }
}
