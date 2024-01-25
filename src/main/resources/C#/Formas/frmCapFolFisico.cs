/************************************************************************
 * Forma creada para la validacion del fisico de las remesas            *
 * **********************************************************************
 * Forma que captura y valida el fisico de las remesas que estan listas *
 * *************************************************************************
 * INFOWARE                                                                *
 * *************************************************************************
 * Autor: Miguel Angel Garnica Angulo (Infoware, León)                     *
 *        Adrian Azades HErnandez Belmonte  ( Infoware , León)                                                               *
 * *************************************************************************
 * Historico de cambios:                                                   *
 *                                                                         *
 * Fecha de Creación:   26/10/09                                           *
 *                      Clase creada para mostrar enviar archivo a INTELAR.*
 * Modificaciones:                                                         *
 *                                                                         *
 * *************************************************************************
 
 */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace Masivos
{
    public partial class frmCapFolFisico : Form
    {
        public frmCapFolFisico()
        {
            InitializeComponent();
        }

        public void getSet(ref string stRemesa)
        {


        }

        //Evento de cargado de la forma donde muestra la remesa a validar con fisico que fue seleccionada.
        private void frmCapFolFisico_Load(object sender, EventArgs e)
        {
            //maskedTextBox1.Text = frmValidaRemesas.arrConsCapt[frmValidaRemesas.iCell];
            mtbRemesa.Text = frmValidaRemesas.arrEmpCapt[frmValidaRemesas.iCell] +
                             frmValidaRemesas.arrEmpPromo[frmValidaRemesas.iCell] +
                             frmValidaRemesas.arrFecCapt[frmValidaRemesas.iCell] +
                             frmValidaRemesas.arrTipoTram[frmValidaRemesas.iCell] +
                             frmValidaRemesas.arrFamProd[frmValidaRemesas.iCell] +                             
                             frmValidaRemesas.arrConsCapt[frmValidaRemesas.iCell];

            mtbFolios.Focus();
        }
        //Evento de verificacion de remesa con fisico
        private void button1_Click(object sender, EventArgs e)
        {
            int iNumRem = 0;
            string stResp = "";
            //Try que verifica si los datos introducidos son del tipo correcto
            try
            {
                iNumRem = Convert.ToInt32(mtbFolios.Text);
            }
            catch
            {
                mtbFolios.Clear();
                return;
            }
            //clsCapFolFisico clsFisico = new clsCapFolFisico();
            clsWRemesas clsFisico = new clsWRemesas();
            stResp = clsFisico.ConsultaFolios5562_20();
            //Try que verifica si la respuesta no esta vacia
            try
            {
                //Condicion que verifica si el numero introducido corresponde al numero de folios en la remesa
                string stCaptura = frmValidaRemesas.arrEmpCapt[frmValidaRemesas.iCell];
                string stPromotora = frmValidaRemesas.arrEmpPromo[frmValidaRemesas.iCell];
                string stFechCapt = frmValidaRemesas.arrFecCapt[frmValidaRemesas.iCell];
                string stTipoTramite = frmValidaRemesas.arrTipoTram[frmValidaRemesas.iCell];
                string stFamilia = frmValidaRemesas.arrFamProd[frmValidaRemesas.iCell];
                string stConsecutivo = frmValidaRemesas.arrConsCapt[frmValidaRemesas.iCell];
                string strRemesa = stPromotora.Substring(2, 2) + stCaptura + stTipoTramite + stFamilia + stFechCapt + stConsecutivo;

                // MODIF MAP 13/08/2010
                // El nombre del archivo se sigue duplicando, la estrategia es usara un nombre compuesto
                // B + DIA (1) + HORA (1) + MIN (2) + SEG (2) + CONS (1)
                string stEnvio = "B" + RegresaValorChar(DateTime.Now.Day.ToString()) + RegresaValorChar(DateTime.Now.Hour.ToString());
                string stTemp = DateTime.Now.Minute.ToString();
                stTemp = stTemp.PadLeft(2, '0');
                stEnvio += stTemp;
                stTemp = DateTime.Now.Second.ToString();
                stTemp = stTemp.PadLeft(2, '0');
                stEnvio += stTemp + RegresaValorChar(strRemesa.Substring(16, 2));
                //"BDC31541" - 13/AGO_12:31:37
                //"BDD42051" 13/AGO_ 13:41:06

                ////string stEnvio = "B" + strRemesa.Substring(2, 2) + strRemesa.Substring(12, 2)
                ////            + strRemesa.Substring(14, 2);
                //string stEnvio = "B" + stCaptura + stPromotora.Substring(2, 2);

                //// REGRESA EL MES EN UN CARACTER
                //stEnvio += RegresaValorChar(stFechCapt.Substring(4, 2));

                //// REGRESA EL DIA EN UN CARACTER
                //stEnvio += RegresaValorChar(stFechCapt.Substring(6, 2));

                //// REGRESA EL CONSECUTIVO EN UN CARACTER
                //stEnvio += RegresaValorChar(strRemesa.Substring(16, 2));

                ////if (int.Parse(stFechCapt.Substring(4, 2)) > 9)
                ////    stEnvio += Convert.ToChar(int.Parse(stFechCapt.Substring(4, 2)) + 55).ToString();
                ////else
                ////    stEnvio += int.Parse(stFechCapt.Substring(4, 2)).ToString();

                
                
                ////if (int.Parse(stFechCapt.Substring(6, 2)) > 9)
                ////    stEnvio += Convert.ToChar(int.Parse(stFechCapt.Substring(6, 2)) + 55).ToString();
                ////else
                ////    stEnvio += int.Parse(stFechCapt.Substring(6, 2)).ToString();
                
                ////// REGRESA EL CONSECUTIVO EN UN CARACTER
                ////if (int.Parse(strRemesa.Substring(16, 2)) > 9)
                ////    stEnvio += Convert.ToChar(int.Parse(strRemesa.Substring(18, 2)) + 55).ToString();
                ////else
                ////    stEnvio += int.Parse(strRemesa.Substring(16, 2)).ToString();

                if (int.Parse(stResp.Substring(176, 5)) == iNumRem)
                {
                    /** MAP. CODIGO PARA VERIFICAR SI EXISTE ARCHIVO DE REMESA PARA SEGUIR EL FLUJO **/
                    string stPatTemp = @"C:\S753_masivos\unzip\";
                    string stArchivoTemp = stPatTemp + strRemesa + ".txt";
                    
                    if (File.Exists(stArchivoTemp))
                    {
                        if(Envio_remesa(strRemesa, stEnvio))
                        {
                            stResp = clsFisico.ValidacionFisica5562_62(0, iNumRem, stEnvio);
                            if (stResp != null)
                            {                                
                                MessageBox.Show("REMESA ENVIADA CON EXITO A INTELAR", "C753 ARIES - Captura Folio Fisico",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                            }

                        }                             
                    }
                    else
                    {
                        MessageBox.Show("ARCHIVO INEXISTENTE FISICAMENTE EN PC", "C753 ARIES - Captura Folio Fisico",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    
                    MessageBox.Show("ERROR EN VALIDADO CON FISICO \nNO. FOLIOS INCORRECTO", "C753 ARIES - Captura Folio Fisico",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                    mtbFolios.Clear();                    
                }
            }
            catch
            {
                mtbFolios.Clear();
            }
        }

        private string RegresaValorChar(string stValor)
        {
            string stValorTemp = "";

            if (int.Parse(stValor) > 9)
                stValorTemp = Convert.ToChar(int.Parse(stValor) + 55).ToString();
            else
                stValorTemp = int.Parse(stValor).ToString();

            //if (stValorTemp.Length != iLongitud)
            //    stValorTemp = stValorTemp.PadLeft('0', iLongitud);

            return stValorTemp;
        }


        //Envio a intelar 
        private bool Envio_remesa(string stRemesa, string stEnvio)
        {
            string stPat = @"C:\S753_masivos\unzip\";
            //string stEnvio = "B" + stRemesa.Substring(2, 2) + stRemesa.Substring(12, 2)
            //                 + stRemesa.Substring(14, 2);

            //if (int.Parse(stRemesa.Substring(16, 2)) > 9)
            //    stEnvio += Convert.ToChar(int.Parse(stRemesa.Substring(18, 2)) + 55).ToString();
            //else
            //    stEnvio += int.Parse(stRemesa.Substring(16, 2)).ToString();

            string stArchivo = stPat + stEnvio;
            // Si archivo de Remesa existe, se cambia el nombre a formato B****
            if (File.Exists(stPat + stRemesa + ".txt"))
            {
                if (File.Exists(stPat + stEnvio))
                    File.Delete(stPat + stEnvio);

                File.Copy(stPat + stRemesa + ".txt", stPat + stEnvio);
            }
            else
            {
                if (!File.Exists(stArchivo))
                {
                    MessageBox.Show("ARCHIVO INEXISTENTE FISICAMENTE EN PC", "C753 ARIES - Captura Folio Fisico",
                         MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            //por cuestiones de permisos se hace una copia temporal a una carpeta del usuario 
            //donde se pondra el archivo  para posteriormente borrarlo
            string stpatUsers = System.Environment.GetEnvironmentVariable("HOMEDRIVE") + System.Environment.GetEnvironmentVariable("HOMEPATH");
            string stTempRemesas = stpatUsers + @"\tempRemesas\";

            //moviendo a carpeta temporal 
            // se crea directorio en el home del usuario para posteriormente borrarlo             
            //string stArchivoTemp = stTempRemesas + stEnvio;              // METERLE ALGUN CARACTER EXTRA PARA DIFERENCIA
            string stArchivoTemp = stTempRemesas + stEnvio + "temp";              // METERLE ALGUN CARACTER EXTRA PARA DIFERENCIA
            DirectoryInfo DIR = new DirectoryInfo(stTempRemesas);
            if (!DIR.Exists)
            {
                DIR.Create();
            }
            File.Copy(stArchivo, stArchivoTemp);//Se copia archivo de ruta Anterior a Ruta Temporal

            //AQUI METER CODIGO PARA LLENAR ARCHIVO CON 600 CARACTERES
            // SE INSERTA EL TIPO DE PAQUETE EN EL REGISTRO 01 DE CADA SOLICITUD

            string stLinea;
            string stNewFile = stTempRemesas + stEnvio;
            string stPaquete = null, stTramiteAries = null, stFamiliaProd = null, stTipoSolicitud = null;

            StreamReader rd = new StreamReader(stArchivoTemp);
            StreamWriter sw = new StreamWriter(stNewFile);

            stLinea = rd.ReadLine();

            while (stLinea != null)
            {
                if (stLinea.Substring(0, 2) == "00")
                {
                    stTramiteAries = stLinea.Substring(6, 2);
                    stFamiliaProd = stLinea.Substring(10, 2);
                }

                if (stLinea.Substring(0, 2) == "01")
                {
                    stTipoSolicitud = stLinea.Substring(18, 2);
                    stPaquete = BuscaPaquete(stTramiteAries, stFamiliaProd, stTipoSolicitud);
                    // MODIF MAP 2011/10/04 EL PAQUETE NO QUEDA AL FINAL DEL REGISTRO SINO ENMEDIO
                    //stLinea += stPaquete;                    
                    string stLineaPart1 = stLinea.Substring(0, 413);
                    string stLineaPart2 = stLinea.Substring(413);
                    stLinea = stLineaPart1 + stPaquete + stLineaPart2;
                }
                stLinea = stLinea.PadRight(600, ' ');
                sw.WriteLine(stLinea);
                stLinea = rd.ReadLine();
            }
            rd.Close();
            sw.Close();

            //HASTA AQUI

            /*************************************************************************************/
            //GUARDAR ARCHIVO QUE SE ENVIA POR INTELAR, PARA REVISION
            string stRevisionArchivo = stpatUsers + @"\Masivos\Archivo\";
            DirectoryInfo DIR2 = new DirectoryInfo(stRevisionArchivo);
            if (!DIR2.Exists)
            {
                DIR2.Create();
            }
            string stDistintivo = DateTime.Now.Hour.ToString() + "." + DateTime.Now.Minute.ToString() + "." + DateTime.Now.Second.ToString();
            File.Copy(stNewFile, stRevisionArchivo + stEnvio + "." + stDistintivo + ".txt");//Se copia archivo de ruta Anterior a Ruta Temporal

            /*************************************************************************************/

            //MODIF MAP 05/OCT/2010
            // ENVIO ARCHIVO INTELAR
            bool boEnviado = false;
            if (mdlICEPIntelar.EnviaArchivosREMESA_LN(stNewFile, "CLC"))
                boEnviado = true;
            // SE CANCELA ENVIO POR FTP
            //else
            //{
            //    mdlGlobales.gstrRutaTemp = @System.Environment.GetEnvironmentVariable("HOMEDRIVE") +
            //                                System.Environment.GetEnvironmentVariable("HOMEPATH") +
            //                                "\\Masivos";
            //    string stNewFileTemp = stNewFile;
            //    mdlFTPIntelar.subEnviaArchivoFTP_LN(ref stNewFileTemp);
            //    {
            //        boEnviado = true;

            //    }
            //    mdlGlobales.gstrRutaTemp = "";
            //}
                        
            if (boEnviado == false)
            {
                MessageBox.Show("ERROR ENVIO DE ARCHIVO DE REMESA A INTELAR", "C753 ARIES - Captura Folio Fisico",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Se borra el directorio y el archivo temporal
                File.Delete(stArchivoTemp);
                File.Delete(stNewFile);// SE BORRA ARCHIVO DE 600 CARACTERES
                DIR.Delete(true);
                File.Delete(stPat + stEnvio);

                return false;
            }

            // se borra el directorio y el archivo temporal            
            File.Delete(stArchivoTemp);
            File.Delete(stNewFile); // SE BORRA ARCHIVO DE 600 CARACTERES
            File.Delete(stArchivo);
            //File.Delete(stPat + stRemesa + ".txt");
            DIR.Delete(true);

            return true;
        }

        private void mtbFolios_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Delete || e.KeyChar == (char)Keys.Left ||
                                e.KeyChar == (char)Keys.Right || IsNumber(e.KeyChar.ToString()))
            {
                if (e.KeyChar == '.' || e.KeyChar == '%' || e.KeyChar == '&' || e.KeyChar == '-' ||
                    e.KeyChar == '*' || e.KeyChar == Convert.ToChar("'"))
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        public bool IsNumber(string inputvalue)
        {
            Regex isnumber = new Regex(@"^?[0-9]$");
            return isnumber.IsMatch(inputvalue);
        }

        public string BuscaPaquete(string stTramiteAriesK1, string stFamiliaProductoK2, string stTipoSolicitudK3)
        {
            string stCatalogo = "269";
            string stLlave4 = "0";
            string stClavePaquete = null;

            if (mdlComunica.OleCatalogos.BuscaCatalogo(ref stCatalogo, ref stTramiteAriesK1, ref stFamiliaProductoK2, ref stTipoSolicitudK3, ref stLlave4))
            {
                stClavePaquete = mdlGlobales.funPoneCeros(mdlComunica.OleCatalogos.getAtributos.Substring(0, Math.Min(mdlComunica.OleCatalogos.getAtributos.Length, 3)), 3);                
            }
            else
            {
                stClavePaquete = "NO EXISTE CVE DE PAQUETE PARA T-F-S";
            }
            return stClavePaquete;
        }

    }     
}