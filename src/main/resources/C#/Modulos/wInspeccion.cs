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
    class wInspeccion
    {
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
                //stEspacios = sEmptyStr.PadLeft(38);
                stEspacios = sEmptyStr.PadLeft(39);
                stEnvio = stClave_tran + " " + stSub_clave_tran + stSistema + stEspacios + "00";
                stEspacios = sEmptyStr.PadLeft(50);
                stEnvio += stEspacios + stEjecutivo;
                //stEspacios = sEmptyStr.PadLeft(65);
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

            //stRespuesta = "5562 24S753                                       00 CONSULTA O.K.                                    004498658000000000000000000000                     01000120100302010101202 000020002201001191122012030000200022010030202020220300003000320100303030303203005";            
            //stRespuesta = "5562 24S753                                       00 CONSULTA O.K.                                     004498658*00000000000000000000                                           01000120100302010101202000020002201001191122012030000200022010030202020220300003000320100303030303203005                             ";

            if (stRespuesta == "" || stRespuesta == null) //Respuesta Vacia
            {
                MessageBox.Show("Error de comunicación", "S753 ARIES - Inspeccion de Remesas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            if (stRespuesta.Substring(0, 4) != stClave_tran) //No corresponde clave de Transaccion
            {
                stRespuesta = stRespuesta.Trim();
                MessageBox.Show("ERROR: " + stRespuesta, "C753 ARIES - Inspeccion de Remesas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            //if (stRespuesta.Substring(49, 2) == "00") //Respuesta Exitosa
            if (stRespuesta.Substring(50, 2) == "00") //Respuesta Exitosa
            {
                return (stRespuesta);
            }
            else
            {
                //MessageBox.Show(stRespuesta.Substring(51, 50), "S753 ARIES - Inspeccion de Remesas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(stRespuesta.Substring(52, 50), "S753 ARIES - Inspeccion de Remesas", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                //stEspacios = sEmptyStr.PadLeft(18);
                stEspacios = sEmptyStr.PadLeft(19);
                stEnvio = stClave_tran + " " + stSub_clave_tran + stSistema + stRemesa + stEspacios + "00";
                stEspacios = sEmptyStr.PadLeft(50);
                stEnvio += stEspacios + stEjecutivo;
                //stEspacios = sEmptyStr.PadLeft(65);
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

                
            if (stRespuesta == "" || stRespuesta == null)
            {
                MessageBox.Show("Error de comunicacion ", "C753 ARIES - Inspeccion de Folios", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            //stRespuesta = "5562 25S75301000120100302010101                   02 NO EXISTEN FOLIOS A INSPECCIONAR PARA LA REMESA  00044986580000000000000000                                ";

            if (stRespuesta.Substring(0, 4) != stClave_tran )
            {
                stRespuesta = stRespuesta.Trim();
                MessageBox.Show("ERROR: " + stRespuesta, "C753 ARIES - Inspeccion de Folios", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            //if (stRespuesta.Substring(49, 2) == "00")
            if (stRespuesta.Substring(50, 2) == "00")
            {
                 return (stRespuesta);
                
            }
            else
            {
                //MessageBox.Show(stRespuesta.Substring(51, 50), "C753 ARIES - Inspeccion de Folios", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(stRespuesta.Substring(52, 50) + "\n" + stRemesa, "C753 ARIES - Inspeccion de Folios", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /********************************************************************************************************************************** 
         * CONSULTA DATOS FOLIOS A INSPECCIONAR - SERVICIO 5562-26
         * stRemesa       = Numero de Remesa
         * stPreimpreso   = Folio Preimpreso
         * stHeader       = Header Original Si Existe Pagineo, Null Si ya no hay mas informacion
        /**********************************************************************************************************************************/
        public string ConsultaDatosInspeccionar5562_26(string stRemesa,string stEjecutivo, string stPreimpreso, string stHeader) 
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
                //stEspacios = sEmptyStr.PadLeft(18);
                stEspacios = sEmptyStr.PadLeft(19);
                stEnvio = stClave_tran + " " + stSub_clave_tran + stSistema + stRemesa + stEspacios + "00";
                stEspacios = sEmptyStr.PadLeft(50);
                stEnvio += stEspacios + stEjecutivo;
                //stEspacios = sEmptyStr.PadLeft(65);
                stEspacios = sEmptyStr.PadLeft(64);
                stEnvio += stEspacios;
            }
            else
                stEnvio = stHeader;
            //DATOS
            stEnvio += stPreimpreso;


            mdlComunica.gvMensaje = stEnvio;
            mdlGlobales.subRegBitacora("E");
            stRespuesta = mdlComunica.funCON(stEnvio);

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
                    //if (stRespuesta.Substring(49, 2) == "00")
                    if (stRespuesta.Substring(50, 2) == "00")
                    {
                        return stRespuesta;
                    }
                    else
                        //MessageBox.Show(stRespuesta.Substring(51, 50), "S753 ARIES - Inspeccion de Campo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        MessageBox.Show(stRespuesta.Substring(52, 50), "S753 ARIES - Inspeccion de Campo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        public string ConsultaCambiosRemesa5562_27(string stRemesa,string stEjecutivo, string stHeader)
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
                //stEspacios = sEmptyStr.PadLeft(18);
                stEspacios = sEmptyStr.PadLeft(19);
                stEnvio = stClave_tran + " " + stSub_clave_tran + stSistema + stRemesa + stEspacios + "00";
                stEspacios = sEmptyStr.PadLeft(50);
                stEnvio += stEspacios + stEjecutivo + "0";
                //stEspacios = sEmptyStr.PadLeft(64);
                stEspacios = sEmptyStr.PadLeft(63);
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

            //stRespuesta = "5562 27S75302000220100119112201                   00 CONSULTA O.K.                                       777777700000000000000000000                                            " +
            //    "00000000972327150030NUEVA DIRECCION 97232715                0000                                        0000                                        0000                                        0000                                        " +
            //    "00000000972327230015NUEVO NOMBRE                            0030NUEVA DIRECCION 459                     0050                                        0051                                        0052                                        " +
            //    "00000000972327230053N                                       0000                                        0000N                                       0000N                                       0000N                                       ";

            if (stRespuesta != null && stRespuesta != "")
            {
                if (stRespuesta.Substring(0, 4) == stClave_tran)
                {
                    //if (stRespuesta.Substring(49, 2) == "00")
                    if (stRespuesta.Substring(50, 2) == "00")
                    {
                        return stRespuesta;
                    }
                    else
                        //MessageBox.Show(stRespuesta.Substring(51, 50), "S753 ARIES - Inspeccion de Campo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        MessageBox.Show(stRespuesta.Substring(52, 50), "S753 ARIES - Inspeccion de Campo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            //stEspacios = sEmptyStr.PadLeft(18);
            stEspacios = sEmptyStr.PadLeft(19);
            stEnvio = stClave_tran + " " + stSub_clave_tran + stSistema + stRemesa + stEspacios + "00";
            stEspacios = sEmptyStr.PadLeft(50);
            stEnvio += stEspacios + stEjecutivo + stFlagInfo + stIndCambios;
            //stEspacios = sEmptyStr.PadLeft(63); //FILLER
            stEspacios = sEmptyStr.PadLeft(62); //FILLER
            stEnvio += stEspacios;
            //DATOS
            stEnvio += stPreimpreso + stTotalCambios + stDatos;

            //ENVIO A BACK


            mdlComunica.gvMensaje = stEnvio;
            mdlGlobales.subRegBitacora("E");
            stRespuesta = mdlComunica.funCON(stEnvio);

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
                    //if (stRespuesta.Substring(49, 2) == "00")
                    if (stRespuesta.Substring(50, 2) == "00")
                        return true;
                    else
                        //MessageBox.Show(stRespuesta.Substring(51, 50), "S753 ARIES - Inspeccion de Campos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        MessageBox.Show(stRespuesta.Substring(52, 50), "S753 ARIES - Inspeccion de Campos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    stRespuesta = stRespuesta.Trim();
                    MessageBox.Show("ERROR: " + stRespuesta, "C753 ARIES - Inspeccion de Campos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return false;

        }

        /********************************************************************************************************************************** 
         * REGISTRO DE FIN DE INSPECCION - SERVICIO 5562-64
         * stRemesa       = Numero de Remesa
         *  stHeader       = Header Original Si Existe Pagineo, Null Si ya no hay mas informacion
        /**********************************************************************************************************************************/
        public bool RegistroFinInspeccion5562_64(string stRemesa, string stEjecutivo)
        {
            string stClave_tran = "5562";
            string stSub_clave_tran = "64";
            string stSistema = "S753";
            string stRespuesta = null;
            string stEnvio;
            string sEmptyStr = " ";
            string stEspacios = null;

            //stEspacios = sEmptyStr.PadLeft(18);
            stEspacios = sEmptyStr.PadLeft(19);
            stEnvio = stClave_tran + " " + stSub_clave_tran + stSistema + stRemesa + stEspacios + "00";
            stEspacios = sEmptyStr.PadLeft(50);
            stEnvio += stEspacios + stEjecutivo;
            //stEspacios = sEmptyStr.PadLeft(65);
            stEspacios = sEmptyStr.PadLeft(64);
            stEnvio += stEspacios;


            mdlComunica.gvMensaje = stEnvio;
            mdlGlobales.subRegBitacora("E");
            stRespuesta = mdlComunica.funCON(stEnvio);

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
                    //if (stRespuesta.Substring(49, 2) == "00")
                    if (stRespuesta.Substring(50, 2) == "00")
                    {
                        return true;
                    }
                    else
                    {
                        //MessageBox.Show(stRespuesta.Substring(51, 50), "S753 ARIES - Inspeccion de Folios", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        MessageBox.Show(stRespuesta.Substring(52, 50), "S753 ARIES - Inspeccion de Folios", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    stRespuesta = stRespuesta.Trim();
                    MessageBox.Show("ERROR: " + stRespuesta, "C753 ARIES - Inspeccion de Folios", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return false;

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

            //stEspacios = sEmptyStr.PadLeft(18);
            stEspacios = sEmptyStr.PadLeft(19);
            stEnvio = stClave_tran + " " + stSub_clave_tran + stSistema + stRemesa + stEspacios + "00";
            stEspacios = sEmptyStr.PadLeft(50);
            stEnvio += stEspacios + stEjecutivo;
            //stEspacios = sEmptyStr.PadLeft(65);
            stEspacios = sEmptyStr.PadLeft(64);
            stEnvio += stEspacios;


            mdlComunica.gvMensaje = stEnvio;
            mdlGlobales.subRegBitacora("E");
            stRespuesta = mdlComunica.funCON(stEnvio);

            if (stRespuesta.IndexOf("SEG") > -1)
            {
                mdlComunica.gvMensaje = stEnvio;
                mdlGlobales.subRegBitacora("E");
                stRespuesta = mdlComunica.funCON(stEnvio);
            }

            //stRespuesta = "5562 65S75302000220100302020202                   00 CONSULTA O.K.                                    0004498658                         ";

            if (stRespuesta != null && stRespuesta != "")
            {
                if (stRespuesta.Substring(0, 4) == stClave_tran)
                {
                    //if (stRespuesta.Substring(49, 2) == "00")
                    if (stRespuesta.Substring(50, 2) == "00")
                    {
                        return true;
                    }
                    else
                    {
                        //MessageBox.Show(stRespuesta.Substring(51, 50), "S753 ARIES - Registro de Remesa en ARIES", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        
    }
}
