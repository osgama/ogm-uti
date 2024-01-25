/************************************************************************
 * Clase de la pantalla de Arribo de Remesas                            *
 * **********************************************************************
 * Clase que trabaja la cadena de datos de empresas.                    *
 * **********************************************************************
 * INFOWARE                                                             *
 * **********************************************************************
 * Autor: Miguel Angel Garnica Angulo (Infoware, León)                  *
 *        Adrian Azades Hernandez Belmonte ( Infoware, Leon Mexico)     *
 * **********************************************************************
 * Historico de cambios:                                                *
 *                                                                      *
 * Fecha de Creación:   05/10/09                                        *
 *                      Clase que trabaja la cadena de registro de      *
 *                      remesas.                                        *
 * Modificaciones:                                                      *
 *                                                                      *
 * **********************************************************************
 
 */
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
    class clsArriRemesas
    {
        
        /***********************************************************************************************************/
        // FUNCION:                  GuardaRemesas5562_60()
        // PARAMETROS ENTRADA:       ref List <Linea> Lista
        // PARAMETROS SALIDA:        string 
        //					
        // RESULTADO:	              
        //               
        // CREACION:                  10/2009
        // DESCRIPCION:                Metodo  que guarada la informacion de las Remesas
        //                           
        //                             
        // DESARROLLADO POR:          AAHB (Infoware )
        // ULTIMA MODIFICACION:
        // DESCRIPCION:
        //
        /**********************************************************************************************************/
       public string GuardaRemesas5562_60(ref List <Linea> Lista, string stDatos)
       {
           string stClave_tran = "5562";
           string stSub_clave_tran = "60";
           string stSistema = "S753";
           string stRespuesta = null;
           string stEnvio;
           string sEmptyStr = " ";
            
           //string stFamilia = "  ";
           //string stConsecutivo = "  ";
           //string stResTrans = "  ";
           string stEjecu = mdlGlobales.gstrNomina.Value.ToString();
           //string stEjecu = "   ";
           int iFlag = new int ();
           int iError = new int();   
           //string sTipoEntidad = "02"; 
           //string sClvEntOrigen = "0342";
           //string stCausaRech = "    "; 
           //string stClvCodProm = "    ";
           string stClvCampo = "1234";
           string stDescCampo;
           //stDatosNuevos = stTipoEntidadOrigen(2) + stEntidadOrigen(4) + stPromocion(4) + stFechaProceso(10) + 
           //stFechaIngresoCredito(10) + stFechaAceptacionCredito(10);
            iFlag = 0;

            string stHeader;
            string stTotErrore;

            stEjecu = stEjecu.Trim();
            stEjecu = valida_Tam(stEjecu, 10);
           
            stHeader = stClave_tran + sEmptyStr + stSub_clave_tran + stSistema;
            for (int iCont = 0; iCont < Lista.Count; iCont++)
            {
                if (Lista[iCont].Remesa.StartsWith("01"))
                {

                    if (Lista[iCont].Errores != 0)
                        iError = 0;
                    else
                        iError = 1;
                    stTotErrore = Lista[iCont].Errores.ToString();

                    if (Lista[iCont].Remesa.Length != 150)
                    { 
                    // registro 01 no corresponde 
                    }
                    string cad = stDatos.Substring(30, 10);
                    //11222233334444-44-445555-55-556666-66-66
                    stEnvio = stHeader +                        //Transaccion-Filler-Subtransaccion-Sistema
                        Lista[iCont].Remesa.Substring(96, 2) +  //Empresa Captura
                        Lista[iCont].Remesa.Substring(98, 2) +  //Empresa Promotora
                        Lista[iCont].Remesa.Substring(88, 8) +  //Fecha Entrega Captura
                        Lista[iCont].Remesa.Substring(100, 2) + //Tipo Tramite
                        Lista[iCont].Remesa.Substring(102, 3) + //Familia Producto
                        Lista[iCont].Remesa.Substring(69, 2) +  //Consecutivo Captura
                        //sEmptyStr.PadLeft(18) +                 //FILLER
                        sEmptyStr.PadLeft(19) +                 //FILLER
                        "00" +                                  //Valor 00
                        sEmptyStr.PadLeft(50) +                 //Descr. Resultado
                        //sEmptyStr.PadLeft(10) +                 //
                        stEjecu +                               //Ejecutivo
                        iFlag.ToString() +                      //Flag Informativo
                        iError.ToString() +                     //Indicador de Error
                        //sEmptyStr.PadLeft(63) +                 //FILLER
                        sEmptyStr.PadLeft(62) +                 //FILLER
                        //DATOS
                        stDatos.Substring(0, 2) +               //TipoEntidad
                        stDatos.Substring(2, 4) +               //Clave Entidad Origen
                        Lista[iCont].Remesa.Substring(17, 16) + //Folio Preimpreso
                        Lista[iCont].Remesa.Substring(33, 16) + //Folio Preimpreso Final
                        "0" + Lista[iCont].Remesa.Substring(49, 4) +  //Total Folios Remesas
                        stDatos.Substring(10, 10) +             // Fecha Proceso
                        stDatos.Substring(20, 10) +             //Fecha Ingreso a Credito
                        stDatos.Substring(30, 10) +             //Fecha Aceptacion Credito
                        Lista[iCont].Remesa.Substring(68, 2) +  //Numero Consecutivo Remesa
                        "0000" +                                //Causa de Rechazo          ????
                        stDatos.Substring(6, 4) +               //Clave Codigo Promocion
                        valida_Tam(stTotErrore, 4);             //Total Errores

                    if (!Lista[iCont].Existe)
                    { //no existe archivo  remesa 
                        stClvCampo = valida_Tam(stClvCampo, 4, " ");
                        stDescCampo = valida_Tam("NO SE ENCONTRO ARCHIVO DE REMESA", 40, " ");
                        stEnvio = stEnvio + stClvCampo + stDescCampo;
                    }

                    if (!Lista[iCont].Promotora)
                    { //no concuerda  en el registro de la remesesa empresa promotora
                        stClvCampo = valida_Tam(stClvCampo, 4, " ");
                        stDescCampo = valida_Tam("NO COINCIDE EMPRESA PROMOTORA", 40, " ");
                        stEnvio = stEnvio + stClvCampo + stDescCampo;
                    }
                    if (!Lista[iCont].Captura)
                    {
                        stClvCampo = valida_Tam(stClvCampo, 4, " ");
                        stDescCampo = valida_Tam("NO COINCIDE EMPRESA CAPTURA", 40, " ");
                        stEnvio = stEnvio + stClvCampo + stDescCampo;
                    }
                    /*
                     hace llamado a funcon
                     */

                    mdlComunica.gvMensaje = stEnvio;
                    mdlGlobales.subRegBitacora("E");
                    stRespuesta = mdlComunica.funCON(stEnvio);
                    if (stRespuesta.IndexOf("SEG") > -1)
                    {
                        mdlComunica.gvMensaje = stEnvio;
                        mdlGlobales.subRegBitacora("E");
                        stRespuesta = mdlComunica.funCON(stEnvio);
                    }
                    

                    //stRespuesta = "9999 XXC753                    00                                                                                                               ";
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
                    //if (stRespuesta.Substring(50, 2) != "00")
                    if (stRespuesta.Substring(51, 2) != "00")
                    {
                        /*Error al guardar la remesa */
                        //Lista.Add(new Linea("  " + stRespuesta.Substring(52, 50) + Lista[iCont].Remesa.Substring(53, 18), false, false, false, 1));
                        Lista.Add(new Linea("  " + stRespuesta.Substring(53, 50) + Lista[iCont].Remesa.Substring(53, 18), false, false, false, 1));
                    }
                }// fin if (Lista[iCont].Remesa.StartsWith("01"))
            } // fin for (int iCont = 0; iCont < Lista.Count; iCont++)
            return ("ok"); // Si todo sale bien esta correcto  se regresa un ok             
        }

        // lleno con ceros antes
        private string valida_Tam(string stCad, int iTam)
        {
            int iTamano = new int();
            if (stCad.Length < iTam)
            {
                iTamano = stCad.Length;
                for (int iCfor = 0; iCfor < (iTam - iTamano); iCfor++)
                    stCad = "0" + stCad;
            }
            return stCad;
        }
        //leno con lo que me pasen 
        private string valida_Tam(string stCad, int iTam, string stCrcCompl)
        {
            if (stCrcCompl.Length != 1)
            {
                stCrcCompl = stCrcCompl.Substring(0, 1);
            
            }
            
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
