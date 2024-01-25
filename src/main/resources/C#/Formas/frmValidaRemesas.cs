/************************************************************************
 * Forma creada para la Validación de Remesas                           *
 * **********************************************************************
 * Forma que valida que la información proporcionada sea conforme al    *
 * formato y tipo de dato.                                              *
 * **********************************************************************
 * INFOWARE                                                             *
 * **********************************************************************
 * Autor: Miguel Angel Garnica Angulo (Infoware, León)                  *
 *                                                                      *
 * **********************************************************************
 * Historico de cambios:                                                *
 *                                                                      *
 * Fecha de Creación:   19/10/09                                        *
 *                      Forma creada para validación de remesas.        *
 * Modificaciones:      03/11/09:                                       *
 *                      Modificación de funciones de validación y       *
 *                      cargado de remesas por cambio en los dialogos   *
 *                      de transacciones.                               *
 *                      14/11/09:                                       *
 *                      Modificación de funciones de validación en      *
 *                      formatos y tamaños por cambio en layouts de     *
 *                      remesas.                                        *
 * **********************************************************************
 
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
    public partial class frmValidaRemesas : Form
    {
        public frmValidaRemesas()
        {
            InitializeComponent();
        }
        #region Variables globales
        /*Variables que contienen los datos de la remesa y sus errores, limitado a 10,000 datos,
        en caso de exceder este numero se cargan hasta la siguiente vez en que se hayan reducido los existentes.*/
        public static string[] arrEmpCapt = new string[10000];
        public static string[] arrEmpPromo = new string[10000];
        public static string[] arrFecCapt = new string[10000];
        public static string[] arrTipoTram = new string[10000];
        public static string[] arrFamProd = new string[10000];
        public static string[] arrConsCapt = new string[10000];
        public static string[] arrProc = new string[10000];
        public static string[] arrEsta = new string[10000];
        public static List<string> lsFolios = new List<string>();        
        public static List<string> lsErrores = new List<string>();

        string stRutaRaiz = "C:\\S753_masivos\\unzip\\";

        public static string stNumEjec = mdlGlobales.gstrNomina.Value.ToString();        

        double dPrimerFolio;
        double dUltimoFolio;
        int iNumFolios = 0;
        public static int iCell;
        string stFolio = "";
        
        // VARIABLE PARA ENVIO DE ERRORES
        int iPrimerGrupo = 0;
        //int iTotalErrores = 0;
        ComboBox cBMandatorio = new ComboBox();

        // VARIABLES PARA BUSQUEDA DE PAQUETES
        // Se obtienen leyendo el registro 00 y el 01 para adjuntarlo al archivo de remesa
        string stTramiteAries;
        string stFamiliaProducto;
        string stTipoSolicitud;

        // VARIABLES PARA CARGA DE CATALOGOS
        ComboBox cBPuente = new ComboBox();
        //DataGridView dataGridPuente = new DataGridView();

        #endregion
        #region Funciones
        //Funcion que realiza el cargado de datos en la forma
        private void CargaForma()
        {
            dgvValRem.Rows.Clear();
            
            if (CargaRemesas())
            {
                //btnCapt.Enabled = false;
                //btnValida.Enabled = false;
                
                /*try
                {                                   
                    if(dgvValRem.CurrentRow != null)
                    {
                        if (dgvValRem.Rows[dgvValRem.CurrentRow.Index].Cells[1].Value.ToString() == "200" && dgvValRem.Rows[dgvValRem.CurrentRow.Index].Cells[2].Value.ToString() == "000")
                            btnError.Enabled = false;
                        else
                            btnError.Enabled = true;

                        if (dgvValRem.Rows[dgvValRem.CurrentRow.Index].Cells[1].Value.ToString() == "201" && dgvValRem.Rows[dgvValRem.CurrentRow.Index].Cells[2].Value.ToString() == "005")
                            btnError.Enabled = true;
                        else
                            btnError.Enabled = false;
                    }                    
                }
                catch
                {
                    MessageBox.Show("Error en cargado de remesas.\nVerificar diálogos de transferencia\no remesas por verificar inexistentes.", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }*/
            }
            else
            {
                // En caso  de q no carge remesas 
                btnCapt.Enabled   = false;
                btnError.Enabled  = false;
                btnValida.Enabled = false;                
            }
        }
        //Función encargada del cargado de las remesas disponibles para validacion
        private bool CargaRemesas()
        {
            bool bRegreso = true;
            string stCadena = null;
            string stRespuesta = null;
            string stEspacio = " ";
            string stHeaderBco = stEspacio.PadLeft(144);

            string stUltimaCapt = "  ";
            string stUltimaPromo = "    ";
            string stUltimaFechCapt = "        ";
            string stUltimoTipo = "  ";
            string stUltimaFam = "  ";
            string stUltimoConsecutivo = "  ";            

            int iCont = 0;
            int iTam = 26;
            int iNumRem = 0;
            bool bSalida = true;
            List<int> LsLong = new List<int>();
            
            clsWRemesas clsVal = new clsWRemesas();
                        
            stCadena = clsVal.ValidacionRemesasExistentes5562_21(stUltimaCapt, stUltimaPromo, stUltimaFechCapt, stUltimoTipo, stUltimaFam, stUltimoConsecutivo);
            
            LsLong.Add(2);
            LsLong.Add(4);
            LsLong.Add(8);
            LsLong.Add(3);

            //Condicion que verifica si existen mas datos por recibir y los almacena en la cadena de respuesta
            if (stCadena != null)
            {
                stRespuesta = stCadena.Substring(176);
                //Ciclo que recopila parametros mientras existan mas datos por recibir
                //pregunta por el flag de ahi mas 
                while (stCadena.Substring(112, 1) == "1")
                {
                    stUltimaCapt = stCadena.Substring(113, 2);
                    stUltimaPromo = stCadena.Substring(115, 4);
                    stUltimaFechCapt = stCadena.Substring(119, 8);
                    stUltimoTipo = stCadena.Substring(127, 2);
                    stUltimaFam = stCadena.Substring(129, 2);
                    stUltimoConsecutivo = stCadena.Substring(131, 2);
                    //stUltimoProceso =
                    //stUltimoEstatus =                    
                    
                    //Llamado de la nueva cadena y guardado de parametros en la cadena stRespuesta
                    
                    stCadena = clsVal.ValidacionRemesasExistentes5562_21(stUltimaCapt, stUltimaPromo, stUltimaFechCapt, stUltimoTipo, stUltimaFam, stUltimoConsecutivo);
                    try
                    {
                        if (stCadena.Substring(176).Length % iTam == 0)
                            stRespuesta += stCadena.Substring(176);
                        else
                            MessageBox.Show("La solicitud de campos esta incompleta ó es nula.", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch
                    {
                        bRegreso = false;
                        break;
                    }
                }
                //calcula numero de remesas 
                iNumRem = NumRemesas(stRespuesta);
                if (iNumRem == 0)
                     return false;
                
                try
                {
                    //Ciclo usado mientras existan parametros contabilizados en la cadena stRespuesta para su extracción
                    while (bSalida)
                    {
                        //Condicion que regula con el numero de respuestas contadas el numero de repeticiones de extracción de datos
                        if (iNumRem >= iCont + 1)
                        {
                            try
                            {
                                arrEmpCapt[iCont]  = stRespuesta.Substring( iCont * iTam, LsLong[0]);
                                arrEmpPromo[iCont] = stRespuesta.Substring((iCont * iTam) + LsLong[0], LsLong[1]);
                                //arrEmpPromo[iCont] = stRespuesta.Substring(iCont * iTam, LsLong[0]);
                                //arrEmpCapt[iCont]  = stRespuesta.Substring((iCont * iTam) + LsLong[0], LsLong[1]);
                                arrFecCapt[iCont]  = stRespuesta.Substring((iCont * iTam) + LsLong[0] + LsLong[1], LsLong[2]);
                                arrTipoTram[iCont] = stRespuesta.Substring((iCont * iTam) + LsLong[0] + LsLong[1] + LsLong[2], LsLong[0]);
                                arrFamProd[iCont]  = stRespuesta.Substring((iCont * iTam) + LsLong[0] + LsLong[1] + LsLong[2] + LsLong[0], LsLong[0]);
                                arrConsCapt[iCont] = stRespuesta.Substring((iCont * iTam) + LsLong[0] + LsLong[1] + LsLong[2] + LsLong[0] + LsLong[0], LsLong[0]);
                                arrProc[iCont]     = stRespuesta.Substring((iCont * iTam) + LsLong[0] + LsLong[1] + LsLong[2] + LsLong[0] + LsLong[0] + LsLong[0], LsLong[3]);
                                arrEsta[iCont]     = stRespuesta.Substring((iCont * iTam) + LsLong[0] + LsLong[1] + LsLong[2] + LsLong[0] + LsLong[0] + LsLong[0] + LsLong[3], LsLong[3]);
                                dgvValRem.Rows.Add(arrEmpCapt[iCont]+arrEmpPromo[iCont]+arrFecCapt[iCont]+arrTipoTram[iCont]+arrFamProd[iCont]+arrConsCapt[iCont], arrProc[iCont], arrEsta[iCont]);                                
                                iCont++;
                            }
                            catch
                            {
                                MessageBox.Show("El cargado de remesas sobrepasa la memoria.\nCuando la memoria sea liberada cargarán las remesas faltantes.", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                bSalida = false;
                            }
                        }
                        else                        
                            bSalida = false;
                        
                    }
                }
                catch
                {
                    bRegreso = false;
                }
            }
            else
                return false;
            return bRegreso;
        }
        //Funcion que calcula el numero de remesas         
        private int NumRemesas(string stRespuesta)
        {
            int iTam = 26;
            int iTotalCadena = stRespuesta.Length / iTam;
            bool bBandera = true;
            int iCont     = new int ();
            iCont         =-1; 
            while (bBandera)
            {
                iCont++;
                if (stRespuesta.Substring(iCont * iTam, 2) == "  " || stRespuesta.Substring(iCont * iTam, 2) == "***")
                    bBandera = false;
            }
            return (iCont);
        }


        //Función que se encarga de verificar que exista el archivo de la remesa seleccionada para validacion
        //y valida que contenga y sean congruentes los datos en él conforme su cabecera.
        private bool ExisteArchivo()
        {
            iCell = dgvValRem.SelectedCells[0].RowIndex;
            string stRemesaFile = stRutaRaiz + arrEmpPromo[iCell].Substring(2) + arrEmpCapt[iCell] +
                arrTipoTram[iCell] + arrFamProd[iCell] + arrFecCapt[iCell] + arrConsCapt[iCell] + ".txt";
            // VERIFICA LA EXISTENCIA DEL ARCHIVO FISICAMENTE EN PC                
            //if (!File.Exists(stRutaRaiz + arrEmpPromo[iCell].Substring(2) + arrEmpCapt[iCell] +
            //    arrTipoTram[iCell] + arrFamProd[iCell] + arrFecCapt[iCell] + arrConsCapt[iCell] + ".txt"))
            if (!File.Exists(stRemesaFile))
            {
                MessageBox.Show("No existe Archivo de Remesa " + stRutaRaiz + arrEmpPromo[iCell].Substring(2)
                    + arrEmpCapt[iCell] + arrTipoTram[iCell] + arrFamProd[iCell] + arrFecCapt[iCell] + arrConsCapt[iCell] + ".txt",
                    "C753 - Valida Remesa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void procesoValidacion()
        {
            string stLinea = "";
            string stIndex = null;

            int i00 = 0;
            int i99 = 0;
            lsErrores.Clear();
            lsFolios.Clear();

            bool boLinea2 = false;
            string stFolioAntes = null;
            string stFolioNuevo = null;

            // Con este arreglo se validan que vengan todos los registros en el archivo de remesa
            // Se inicializan los campos en 0, por cada existencia se cambia por un 1, y despues se verifica
            // si el campo es mandatorio
            int iSize = 22;
            string[] arrayRegistroIni = new string[iSize];
            borrarArreglo(arrayRegistroIni);

            StreamReader srVer = new StreamReader(stRutaRaiz + arrEmpPromo[iCell].Substring(2) + arrEmpCapt[iCell] + arrTipoTram[iCell] + arrFamProd[iCell] + arrFecCapt[iCell] + arrConsCapt[iCell] + ".txt");

            stLinea = srVer.ReadLine();
            while (stLinea != null)
            {

                if (boLinea2 == true)
                    stFolioNuevo = stLinea.Substring(2, 16);
                if (i00 == 1 && boLinea2 == false)
                {
                    boLinea2 = true;
                    stFolioAntes = stLinea.Substring(2, 16);
                }

                if ((stFolioAntes != stFolioNuevo) && i00 != 0 && stFolioNuevo != null)
                {
                    existenciaLineas(arrayRegistroIni, stFolioAntes);
                    stFolioAntes = stFolioNuevo;
                    borrarArreglo(arrayRegistroIni);
                }

                stIndex = stLinea.Substring(0, 2);
                switch (stIndex)
                {
                    case "00":
                        i00++;
                        if (i00 == 1)
                        {
                            Checa00(stLinea);
                            stTramiteAries = stLinea.Substring(6, 2);
                            stFamiliaProducto = stLinea.Substring(10, 2);
                        }
                        else
                            lsErrores.Add("0000000000000000" + regresaValorCatalogo("0000"));
                        break;
                    case "01":
                        Checa01(stLinea);
                        arrayRegistroIni[1] = "1";
                        break;
                    case "03":
                        Checa03(stLinea);
                        arrayRegistroIni[3] = "1";
                        break;
                    case "06":
                        Checa06(stLinea);
                        arrayRegistroIni[6] = "1";
                        break;
                    case "08":
                        Checa08(stLinea);
                        arrayRegistroIni[8] = "1";
                        break;
                    case "09":
                        Checa09(stLinea);
                        arrayRegistroIni[9] = "1";
                        break;
                    case "11":
                        Checa11(stLinea);
                        arrayRegistroIni[11] = "1";
                        break;
                    case "12":
                        Checa12(stLinea);
                        arrayRegistroIni[12] = "1";
                        break;
                    case "13":
                        Checa13(stLinea);
                        arrayRegistroIni[13] = "1";
                        break;
                    case "15":
                        Checa15(stLinea);
                        arrayRegistroIni[15] = "1";
                        break;
                    case "16":
                        Checa16(stLinea);
                        arrayRegistroIni[16] = "1";
                        break;
                    case "18":
                        Checa18(stLinea);
                        arrayRegistroIni[18] = "1";
                        break;
                    case "19":
                        Checa19(stLinea);
                        arrayRegistroIni[19] = "1";
                        break;
                    case "20":
                        Checa20(stLinea);
                        arrayRegistroIni[20] = "1";                        
                        break;
                    case "99":
                        i99++;
                        if (i99 == 1)
                        {
                            Checa99(stLinea);
                            arrayRegistroIni[21] = "*";
                        }
                        else
                            lsErrores.Add("9999999999999999" + regresaValorCatalogo("9999"));
                        break;
                    default: break;

                }

                stLinea = srVer.ReadLine();

            }
            srVer.Close();

            //Condiciones que verifican si dentro del archivo de remesa se encuentran las cabeceras 00 y 99
            if (i00 == 0)
                lsErrores.Add("0000000000000000" + regresaValorCatalogo("0000"));
            if (i99 == 0)
                lsErrores.Add("9999999999999999" + regresaValorCatalogo("9999"));

        }

        private void borrarArreglo(string[] arrayRegistro)
        {
            int iSize = 22;
            for (int iArray = 0; iArray < iSize; iArray++)
                arrayRegistro[iArray] = "0";
        }

        private void existenciaLineas(string [] arrayRegistro, string stFolioFaltante)
        {
            string stHeader;
            int iSize = 22;
            for (int iArray = 1; iArray < iSize - 1; iArray++)
            {
                if (iArray < 9)
                    stHeader = "0" + iArray.ToString() + "01";
                else
                    stHeader = iArray.ToString() + "01";

                if (esMandatorio(stHeader))
                    if (arrayRegistro[iArray] != "1")
                        lsErrores.Add(stFolioFaltante + stHeader + "DATO MANDATORIO INCOMPLETO");
            }

        }

        //Funcion que recolecta los folios y los envia para verificar si ya existen
        public bool VerificaPreimpresos()
        {            
            clsWRemesas clsValida = new clsWRemesas();
            string stFolios = "";
            string stResp;
            string stFoliosRepet = null;
            //int iRepeticiones;
            //int iInc = 0;
            int iFolios;
            //bool bSalida = true;
            int iTamFolios = 17;
            int iFoliosRestantes = lsFolios.Count;
            List<string> lsFoliosTemp = new List<string>();
            iFolios = lsFolios.Count;

            string stFolioTemp = null;
            int iFoliosPorEnvio = 50;
            //int iFoliosPorEnvio = 102;
            
            int iCiclos = lsFolios.Count / iFoliosPorEnvio;

            if (lsFolios.Count % iFoliosPorEnvio != 0)
                iCiclos++;
            
            for (int iNumCiclos = 0; iNumCiclos < iCiclos; iNumCiclos++)
            {
                for (int iNumFolioX = 0; iNumFolioX < iFoliosPorEnvio && iNumFolioX < iFoliosRestantes; iNumFolioX++)
                {
                    stFolios += lsFolios[iNumFolioX + (iFoliosPorEnvio * iNumCiclos)] + " ";
                }

                stResp = clsValida.ConsultaPreimpresoAries5562_22(stFolios);                
                
                if (stResp == null)
                    return false;
                
                stFolios = stResp.Substring(176);

                int iSize = stFolios.Length / iTamFolios;
                for (int iCont = 0; iCont < iSize; iCont++)
                {
                    stFolioTemp = stFolios.Substring((iCont * iTamFolios) + 0, iTamFolios);
                    if (stFolioTemp.Substring(16, 1) == "1")
                        stFoliosRepet += stFolioTemp;
                }
                iFoliosRestantes = iFoliosRestantes - iFoliosPorEnvio;
                stFolios = "";
                
            }

            if (stFoliosRepet != "" && stFoliosRepet != null)
            {                
                int iSize = stFoliosRepet.Length /17;
                string stFolioDup = null;
                for (int iFolioDup = 0; iFolioDup < iSize; iFolioDup++)
                {
                    stFolioDup = stFoliosRepet.Substring((iFolioDup * 17), 16);
                    lsErrores.Add(stFolioDup + "0000FOLIO DUPLICADO");
                }
            }
            return true;                                    
        }
        
        private bool registraErrores()
        {
            clsWRemesas clsValida = new clsWRemesas();
            List<string> lsErroresTemp = new List<string>();

            int iErrores;
            int iTotalErrores;
            string stDatos = null;
            string stResp = null;
            string stFolioEnvio = null;
            

            iErrores = lsErrores.Count;
            iTotalErrores = lsErrores.Count;
            //iTotalErrores += iErrores;

            while (lsErrores.Count > 0)
            {
                stFolioEnvio = lsErrores[0].ToString().Substring(0, 16);
                for (int iLineaError = 0; iLineaError < lsErrores.Count; iLineaError++)
                {
                    string stFolioEnvioTemp = lsErrores[iLineaError].ToString().Substring(0, 16);
                    //if (iLineaError < 28)
                    if (iLineaError < 28 && stFolioEnvio == stFolioEnvioTemp)                    
                        stDatos += lsErrores[iLineaError].PadRight(60, ' ');
                    else
                        lsErroresTemp.Add(lsErrores[iLineaError]);
                }

                lsErrores.Clear();

                for (int iNuevaList = 0; iNuevaList < lsErroresTemp.Count; iNuevaList++)
                    lsErrores.Add(lsErroresTemp[iNuevaList]);

                if (iErrores > 28 && iPrimerGrupo == 1)
                {
                    stResp = clsValida.RegistroResultadosValidacion5562_61(iPrimerGrupo, 1, iTotalErrores, stDatos);
                }
                if (iErrores > 28 && iPrimerGrupo == 0)
                {
                    stResp = clsValida.RegistroResultadosValidacion5562_61(iPrimerGrupo, 1, iTotalErrores, stDatos);
                    iPrimerGrupo = 1;
                }                                
                if (iErrores < 29 && iPrimerGrupo == 1)
                {
                    stResp = clsValida.RegistroResultadosValidacion5562_61(iPrimerGrupo, 1, iTotalErrores, stDatos);
                }
                if (iErrores < 29 && iPrimerGrupo == 0)
                {
                    stResp = clsValida.RegistroResultadosValidacion5562_61(iPrimerGrupo, 1, iTotalErrores, stDatos);
                    iPrimerGrupo = 1;
                }

                stDatos = null;
                lsErroresTemp.Clear();
                if(stResp == null)
                    return false;

                iErrores = lsErrores.Count;
            }
            return true;
        }

        //Funcion que valida si hubo errores y los almacena en la cadena de envio de datos
        public void OrganizaFolios()
        {
            clsWRemesas clsValida = new clsWRemesas();
            int iFoliosControl;
            string stRespuesta = null;

            for (int iFolio = 0; iFolio < lsFolios.Count - 1; iFolio++)
            {
                for (int iFolioSig = iFolio + 1; iFolioSig < lsFolios.Count; iFolioSig++)
                {
                    if (lsFolios[iFolio] == lsFolios[iFolioSig])
                    {
                        lsFolios.Remove(lsFolios[iFolioSig]);
                        iFolioSig--;
                    }
                }
            }

            if (lsFolios.Count == iNumFolios)
            {
                for (int iFolios = 0; iFolios < lsFolios.Count; iFolios++)
                {
                    try
                    {
                        if (double.Parse(lsFolios[iFolios]) >= dPrimerFolio)
                        {
                            if (double.Parse(lsFolios[iFolios]) > dUltimoFolio)
                                lsErrores.Add(lsFolios[iFolios].ToString() + "0000FOLIO NO CORRESPONDE A ARCHIVO DE REMESA");
                        }
                        else
                            lsErrores.Add(lsFolios[iFolios].ToString() + "0000FOLIO FUERA DE RANGO");

                    }
                    catch
                    {
                        MessageBox.Show("Error en rango de folios:\nVerifique si faltan primer folio o último folio.", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        lsErrores.Add(lsFolios[iFolios].ToString() + "0000FOLIO FUERA DE RANGO");
                    }
                }
            }
            else
                lsErrores.Add("0000000000000000" + "0000" + "FOLIOS NO COINCIDEN CON No DE PETICIONES");

            /********** VALIDACION DE FOLIOS EN REMESA VS FOLIOS ARCHIVO DE CONTROL **********/

            stRespuesta = clsValida.ConsultaFolios5562_20();
            
            if (stRespuesta != null)
            {
                iFoliosControl = int.Parse(stRespuesta.Substring(176, 5));
                if(iFoliosControl != lsFolios.Count || iFoliosControl != iNumFolios)
                    lsErrores.Add("0000000000000000" + "0000" + "FOLIOS NO COINCIDEN CON No DE PETICIONES");
            }

        }
        
        //Funcion que checa los datos con cabecera 00
        public void Checa00(string stCadena)
        {
            stCadena = stCadena.Trim();
            string stFolioCero = "0".PadRight(16,'0');
            if (stCadena.Length < 54)
            {
                lsErrores.Add(stFolioCero + "0000REGISTRO INCOMPLETO O EXCEDENTES");
                //lsErrores.Add(stFolioCero + regresaValorCatalogo("0000"));
            }
            else
            {
                if (Numero(stCadena.Substring(2, 2)) == true)
                {                    
                    if (esMandatorio("0002") && double.Parse(stCadena.Substring(2, 2)) == 0)
                        lsErrores.Add(stFolioCero + "0002");
                    //lsErrores.Add(stFolioCero + regresaValorCatalogo("0002"));//Clave Promotora
                }
                else
                    lsErrores.Add(stFolioCero + "0002");

                if (Numero(stCadena.Substring(4, 2)) == true)
                {
                    if (esMandatorio("0003") && double.Parse(stCadena.Substring(4, 2)) == 0)
                        lsErrores.Add(stFolioCero + "0003Clave de Captura");
                    //lsErrores.Add(stFolioCero + regresaValorCatalogo("0003"));
                }
                else
                    lsErrores.Add(stFolioCero + "0003Clave de Captura");

                if (Numero(stCadena.Substring(6, 2)))
                {
                    if (esMandatorio("0004") && double.Parse(stCadena.Substring(6, 2)) == 0)
                        lsErrores.Add(stFolioCero + "0004Clave de Tramite ARIES");
                    //lsErrores.Add(stFolioCero + regresaValorCatalogo("0004"));
                }
                else
                    lsErrores.Add(stFolioCero + "0004Clave de Tramite ARIES");

                if (Numero(stCadena.Substring(8, 4)))
                {
                    if (esMandatorio("0005") && double.Parse(stCadena.Substring(8, 4)) == 0)
                        lsErrores.Add(stFolioCero + "0005Familia de Producto");
                    //lsErrores.Add(stFolioCero + regresaValorCatalogo("0005"));
                }
                else
                    lsErrores.Add(stFolioCero + "0005Familia de Producto");

                if (!Fecha(stCadena.Substring(12, 6)))
                {
                    lsErrores.Add(stFolioCero + "0006Fecha de Creacion del Archivo");
                }

                //if (Numero(stCadena.Substring(12, 6)))
                //{
                //    if (esMandatorio("0006") && double.Parse(stCadena.Substring(12, 6)) == 0)
                //        lsErrores.Add(stFolioCero + "0006Fecha de Creacion del Archivo");
                //    //lsErrores.Add(stFolioCero + regresaValorCatalogo("0006"));
                //}
                //else
                //    lsErrores.Add(stFolioCero + "0006Fecha de Creacion del Archivo");
                
                if (Numero(stCadena.Substring(18, 2)))
                {
                    if (esMandatorio("0007") && double.Parse(stCadena.Substring(18, 2)) == 0)
                        lsErrores.Add(stFolioCero + "0007Consecutivo del Archivo");
                    //lsErrores.Add(stFolioCero + regresaValorCatalogo("0007"));
                }
                else
                    lsErrores.Add(stFolioCero + "0007Consecutivo del Archivo");

                if (Numero(stCadena.Substring(20, 16)))
                {
                    if (double.Parse(stCadena.Substring(20, 16)) == 0)
                        lsErrores.Add(stFolioCero + "0008Folio Inicial");
                    //lsErrores.Add(stFolioCero + regresaValorCatalogo("0008"));
                    else
                        dPrimerFolio = double.Parse(stCadena.Substring(20, 16));                    
                }
                else
                    lsErrores.Add(stFolioCero + "0008Folio Inicial");                
                                    
                if (Numero(stCadena.Substring(36, 16)))
                {
                    if (double.Parse(stCadena.Substring(36, 16)) == 0)
                        lsErrores.Add(stFolioCero + "0009Folio Final");
                    //lsErrores.Add(stFolioCero + regresaValorCatalogo("0009"));
                    else
                        dUltimoFolio = double.Parse(stCadena.Substring(36, 16));
                }
                else    
                    lsErrores.Add(stFolioCero + "0009Folio Final");
                
                if (Numero(stCadena.Substring(52, 2)))
                {
                    if (esMandatorio("0010") && double.Parse(stCadena.Substring(52, 2)) == 0)
                        lsErrores.Add(stFolioCero + "0010Numero de Diskette");
                    //lsErrores.Add(stFolioCero + regresaValorCatalogo("0010"));
                }
                else
                    lsErrores.Add(stFolioCero + "0010Numero de Diskette");
            }
        }
        //Funcion que checa los datos con cabecera 01, Layout con Catalogos
        public void Checa01(string stCadena)
        {
            frmCapFolFisico paquete = new frmCapFolFisico();
            //stCadena = stCadena.Trim(); // SE COMENTO PORQUE EL ULTIMO CAMPO ES LA FIEL
            stFolio = stCadena.Substring(2, 16);
            if (stCadena.Length < 441) //ABH 05/05/2011. Cambia de 333 a 441
            {
                lsErrores.Add(stFolio + "0100REGISTRO INCOMPLETO");                
            }
            else
            {
                if (Numero(stCadena.Substring(2, 16)))
                {
                    if (esMandatorio("0102") && double.Parse(stCadena.Substring(2, 16)) == 0)
                        lsErrores.Add(stFolio + regresaValorCatalogo("0102"));
                    else
                        lsFolios.Add(stFolio);
                }
                else
                
                    lsErrores.Add(stFolio + regresaValorCatalogo("0102"));
                
                if (Numero(stCadena.Substring(18, 2)))
                {
                    if (esMandatorio("0103") && double.Parse(stCadena.Substring(18, 2)) == 0)
                        lsErrores.Add(stFolio + regresaValorCatalogo("0103"));                                                           
                }
                else
                    lsErrores.Add(stFolio + regresaValorCatalogo("0103"));

                if (!RFC(stCadena.Substring(20, 10)))
                {                    
                    if (esMandatorio("0104"))
                    lsErrores.Add(stFolio + regresaValorCatalogo("0104"));
                }
                if (!Si_no(stCadena.Substring(30, 1)))
                {                    
                    if (esMandatorio("0105"))
                    lsErrores.Add(stFolio + regresaValorCatalogo("0105"));
                }
                if (!Letra_Esp(stCadena.Substring(31, 20)))
                {                    
                    if (esMandatorio("0106"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("0106"));
                }
                if (!Nac_Inter(stCadena.Substring(51, 1)))
                {                 
                    if(esMandatorio("0107"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("0107"));
                }
                //if (Numero(stCadena.Substring(52, 6)))
                if (!Fecha(stCadena.Substring(52, 6)))
                {
                    //if (esMandatorio("0108") && double.Parse(stCadena.Substring(52, 6)) == 0)
                        lsErrores.Add(stFolio + regresaValorCatalogo("0108"));
                }
                //else
                //    lsErrores.Add(stFolio + regresaValorCatalogo("0108"));

                if (Numero(stCadena.Substring(58, 8)))
                {
                    if (esMandatorio("0109") && double.Parse(stCadena.Substring(58, 8)) == 0)
                        lsErrores.Add(stFolio + regresaValorCatalogo("0109"));
                }
                else
                    lsErrores.Add(stFolio + regresaValorCatalogo("0109"));

                if (!Letra_Esp(stCadena.Substring(66,50))) //ABH 05/05/2011. Nombre. Cambia long 30 a 50
                {
                    if (esMandatorio("0110"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("0110"));
                }
                if (!Letra_Esp(stCadena.Substring(116, 60))) //ABH 05/05/2011. A.Paterno. Cambia long 30 a 60, pos 96 a 116
                {
                    if (esMandatorio("0111"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("0111"));
                }
                if (!Letra_Esp(stCadena.Substring(176, 60))) //ABH 05/05/2011. A.Materno. Cambia long 30 a 60, pos 126 a 176
                {
                    if (esMandatorio("0112"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("0112"));
                }
                if (!Alfanumerico_Esp(stCadena.Substring(236, 35))) //ABH 05/05/2011. Domicilio. Cambia pos 156 a 236
                {
                    if (esMandatorio("0113"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("0113"));
                }
                if (!Alfanumerico_Esp(stCadena.Substring(271, 35))) //ABH 05/05/2011. Colonia. Cambia pos 191 a 271
                {
                    if (esMandatorio("0114"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("0114"));
                }
                if (!Alfanumerico_Esp(stCadena.Substring(306, 26))) //ABH 05/05/2011. Ciudad. Cambia pos 226 a 306
                {
                    if (esMandatorio("0115"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("0115"));
                }
                if (!Estado(stCadena.Substring(332, 4)))    //ABH 05/05/2011. Estado. Cambia pos 252 a 332            
                {
                    if (esMandatorio("0116"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("0116"));
                }
                if (Numero(stCadena.Substring(336, 5))) //ABH 05/05/2011. CP. Cambia pos 256 a 336
                {
                    if (esMandatorio("0117") && double.Parse(stCadena.Substring(336, 5)) == 0) //ABH 05/05/2011. CP. Cambia pos 256 a 336
                        lsErrores.Add(stFolio + regresaValorCatalogo("0117"));
                }
                else
                    lsErrores.Add(stFolio + regresaValorCatalogo("0117"));

                if (Numero(stCadena.Substring(341, 3))) //ABH 05/05/2011. Lada. Cambia pos 261 a 341
                {
                    if (esMandatorio("0118") && double.Parse(stCadena.Substring(341, 3)) == 0) //ABH 05/05/2011. Lada. Cambia pos 261 a 341
                        lsErrores.Add(stFolio + regresaValorCatalogo("0118"));
                }
                else
                    lsErrores.Add(stFolio + regresaValorCatalogo("0118"));

                if (Numero(stCadena.Substring(344, 7))) //ABH 05/05/2011. Telefono. Cambia pos 264 a 344
                {
                    if (esMandatorio("0119") && double.Parse(stCadena.Substring(344, 7)) == 0) //ABH 05/05/2011. Telefono. Cambia pos 264 a 344
                        lsErrores.Add(stFolio + regresaValorCatalogo("0119"));
                }
                else
                    lsErrores.Add(stFolio + regresaValorCatalogo("0119"));

                if (Numero(stCadena.Substring(351, 2))) //ABH 05/05/2011. Edad. Cambia pos 271 a 351
                {
                    if (esMandatorio("0120") && double.Parse(stCadena.Substring(351, 2)) == 0) //ABH 05/05/2011. Edad. Cambia pos 271 a 351
                        lsErrores.Add(stFolio + regresaValorCatalogo("0120"));
                }
                else
                    lsErrores.Add(stFolio + regresaValorCatalogo("0120"));

                if (!Fecha(stCadena.Substring(353, 6))) //ABH 05/05/2011. Fecha Nac. Cambia pos 273 a 353
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("0121"));
                }

                //if (Numero(stCadena.Substring(273, 6)))
                //{
                //    if (esMandatorio("0121") && double.Parse(stCadena.Substring(273, 6)) == 0)
                //        lsErrores.Add(stFolio + regresaValorCatalogo("0121"));
                //}
                //else
                //    lsErrores.Add(stFolio + regresaValorCatalogo("0121"));

                if (!NCivil(stCadena.Substring(359, 1))) //ABH 05/05/2011. Edo Civil. Cambia pos 279 a 359
                {
                    if (esMandatorio("0122"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("0122"));
                }
                if (Numero(stCadena.Substring(360, 2))) //ABH 05/05/2011. Número de Dependientes. Cambia pos 280 a 360
                {
                    if (esMandatorio("0123") && double.Parse(stCadena.Substring(360, 2)) == 0) //ABH 05/05/2011. Número de Dependientes. Cambia pos 280 a 360
                        lsErrores.Add(stFolio + regresaValorCatalogo("0123"));
                }
                else
                    lsErrores.Add(stFolio + regresaValorCatalogo("0123"));

                if (!NVivienda(stCadena.Substring(362, 1))) //ABH 05/05/2011. Tipo Vivienda. Cambia pos 282 a 362
                {
                    if (esMandatorio("0124"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("0124"));

                }
                if (Numero(stCadena.Substring(363, 4))) //ABH 05/05/2011. Años de Residir. Cambia pos 283 a 363
                {
                    if (esMandatorio("0125") && double.Parse(stCadena.Substring(363, 4)) == 0) //ABH 05/05/2011. Años de Residir. Cambia pos 283 a 363
                        lsErrores.Add(stFolio + regresaValorCatalogo("0125"));
                }
                else
                    lsErrores.Add(stFolio + regresaValorCatalogo("0125"));

                if (!Alfanumerico_Esp(stCadena.Substring(368, 13))) //ABH 05/05/2011. RFC. Cambia pos 287 a 368
                {
                    if (esMandatorio("0126"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("0126"));
                }
                if (!Fem_masc(stCadena.Substring(380, 1))) //ABH 05/05/2011. Sexo. Cambia pos 300 a 380
                {
                    if(esMandatorio("0127"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("0127"));
                }
                if (!Casa_ofic(stCadena.Substring(381, 1))) //ABH 05/05/2011. Envío de Corresp. Cambia pos 301 a 381
                {
                    if(esMandatorio("0128"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("0128"));
                }
                if (!Casa_ofic(stCadena.Substring(382, 1))) //ABH 05/05/2011. Envío de Plasticos. Cambia pos 302 a 382
                { 
                    if(esMandatorio("0129"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("0129"));
                }
                if (!NEscolaridad(stCadena.Substring(383, 2))) //ABH 05/05/2011. Escolaridad. Cambia pos 303 a 383
                {
                    if (esMandatorio("0130"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("0130"));
                }
                //if (!Fecha(stCadena.Substring(305, 11)))
                //{
                //    lsErrores.Add(stFolio + regresaValorCatalogo("0131"));
                //}
                if (Numero(stCadena.Substring(385, 11))) //ABH 05/05/2011. Nómina Promotor . Cambia pos 305 a 385
                {
                    if (esMandatorio("0131") && double.Parse(stCadena.Substring(385, 11)) == 0) //ABH 05/05/2011. Nómina Promotor . Cambia pos 305 a 385
                        lsErrores.Add(stFolio + regresaValorCatalogo("0131"));
                }
                else
                    lsErrores.Add(stFolio + regresaValorCatalogo("0131"));

                if (!Alfanumerico_Esp(stCadena.Substring(396, 17))) //ABH 05/05/2011. Registro. Cambia pos 316 a 396
                {
                    if(esMandatorio("0132"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("0132"));
                }

                // MODIF MAP 2011/08/24 LISTAS NEGRAS 2
                if (!Numero(stCadena.Substring(413, 4)) || (esMandatorio("0133") && double.Parse(stCadena.Substring(413, 4)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("0133"));// PAIS DE NACIMIENTO
                }
                if (!Numero(stCadena.Substring(417, 4)) || (esMandatorio("0134") && double.Parse(stCadena.Substring(417, 4)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("0134")); // PAIS DE NACIONALIDAD
                }
                if (!Alfanumerico_Esp(stCadena.Substring(421, 20))) // FIEL
                {
                    if(esMandatorio("0135"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("0135"));
                }
                

                stTipoSolicitud = stCadena.Substring(18, 2); 
                string stExistePaquete = paquete.BuscaPaquete(stTramiteAries, stFamiliaProducto, stTipoSolicitud);
                if (stExistePaquete.Length > 3)
                {
                    lsErrores.Add(stFolio + "0000" + stExistePaquete);
                }

            }
        }
        
        public void Checa03Nuevo(string stCadena)
        {
            // RESULTADO: "0101Tipo de Registro                        10004A000"
            string stRegistro;
            string stDescripcion;
            string stCatalogo;
            string stTipoDato;
            string stTamanio;
            string stMandatorio;

            string stLineaCombo;

            cBPuente.Items.Clear();            
            cargaCatalogoMandatorio("03");

            for (int iLineaCombo = 0; iLineaCombo < cBPuente.Items.Count; iLineaCombo++)
            {
                stLineaCombo = cBPuente.Items[iLineaCombo].ToString();

                stRegistro = stLineaCombo.Substring(0, 4);
                stDescripcion = stLineaCombo.Substring(4, 50).Trim(); ;
                stMandatorio = stLineaCombo.Substring(54, 1);
                stTamanio = stLineaCombo.Substring(55, 4);
                stTipoDato = stLineaCombo.Substring(59, 1);
                stCatalogo = stLineaCombo.Substring(60, 3);
            }

            int iSizeRegistro = 33;
            string[] arrayRegistro = new string[iSizeRegistro];

            for (int iArray = 0; iArray < iSizeRegistro; iArray++)
                arrayRegistro[iArray] = "0";
            iSizeRegistro = 0;
            int iBand = -1;
            //string stRegistro;
            for (int iCont = 0; iBand != 0; iCont++)
            {
                if (iCont + 1 < 10)
                    stRegistro = "03" + "0" + (iCont + 1).ToString();
                else
                    stRegistro = "03" + (iCont + 1).ToString();

                iBand = regresaTamanioRegistro(stRegistro);
                iSizeRegistro += iBand;
                arrayRegistro[iCont] = iBand.ToString();
                
            }

            stCadena = stCadena.Trim();
            stFolio = stCadena.Substring(2, 16);
            
            if (stCadena.Length < 255)            
                lsErrores.Add(stFolio + "0300REGISTRO INCOMPLETO");
            
            
        }
        //Funcion que checa los datos con cabecera 03, Layout con Catalogos
        public void Checa03(string stCadena)
        {
            stCadena = stCadena.Trim();
            if (stCadena.Length < 217) //ABH 05/05/2011. Cambia de 212 a 217
            {
                lsErrores.Add(stFolio + "0300REGISTRO INCOMPLETO");
            }
            else
            {
                if (!Numero(stCadena.Substring(2, 16)) || (esMandatorio("0302") && double.Parse(stCadena.Substring(2, 16)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("0302"));
                }
                else
                {
                    stFolio = stCadena.Substring(2, 16);
                    lsFolios.Add(stFolio);
                }
                if (!NTipo_Empleo(stCadena.Substring(18, 3)))
                {
                    if (esMandatorio("0303"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("0303"));
                }

                if (!Numero(stCadena.Substring(21, 4)) || (esMandatorio("0304") && double.Parse(stCadena.Substring(21, 4)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("0304"));
                }
                if (!Alfanumerico_Esp(stCadena.Substring(25, 40)))
                {
                    if (esMandatorio("0305"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("0305"));
                }                
                if(!NPuesto(stCadena.Substring(65, 3)))
                {
                    if (esMandatorio("0306"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("0306"));
                }
                if (!NGiro_Empresa(stCadena.Substring(68, 6))) //ABH 05/05/2011. Giro. Cambia long 2 a 6
                {
                    if (esMandatorio("0307"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("0307"));
                }
                if (!NPuesto(stCadena.Substring(74, 4))) //ABH 05/05/2011. Puesto/Ocupacion. Cambia pos 70 a 74, long 3 a 4
                {
                    if (esMandatorio("0308"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("0308")); 
                }
                if (!Alfanumerico_Esp(stCadena.Substring(78, 20))) //ABH 05/05/2011. Departamento. Cambia pos 73 a 78
                {
                    if (esMandatorio("0309"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("0309"));
                }
                if (!Alfanumerico_Esp(stCadena.Substring(98, 35))) //ABH 05/05/2011. Domicilio. Cambia pos 93 a 98
                {
                    if (esMandatorio("0310"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("0310"));
                }
                if (!Alfanumerico_Esp(stCadena.Substring(133, 35))) //ABH 05/05/2011. Colonia. Cambia pos 128 a 133
                {
                    if (esMandatorio("0311"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("0311"));
                }
                if (!Alfanumerico_Esp(stCadena.Substring(168, 26))) //ABH 05/05/2011. Municipio. Cambia pos 163 a 168
                {
                    if (esMandatorio("0312"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("0312"));
                }
                if (!Estado(stCadena.Substring(194, 4))) //ABH 05/05/2011. Estado. Cambia pos 189 a 194
                {
                    if (esMandatorio("0313"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("0313"));
                }
                //ABH 05/05/2011. CP. Cambia pos 193 a 198
                if (!Numero(stCadena.Substring(198, 5)) || (esMandatorio("0314") && double.Parse(stCadena.Substring(198, 5)) == 0))
                {                    
                        lsErrores.Add(stFolio + regresaValorCatalogo("0314"));
                }
                //ABH 05/05/2011. Lada. Cambia pos 198 a 203
                if (!Numero(stCadena.Substring(203, 3)) || (esMandatorio("0315") && double.Parse(stCadena.Substring(203, 3)) == 0))
                {                    
                        lsErrores.Add(stFolio + regresaValorCatalogo("0315"));
                }
                //ABH 05/05/2011. Telefono. Cambia pos 201 a 206
                if (!Numero(stCadena.Substring(206, 7)) || (esMandatorio("0316") && double.Parse(stCadena.Substring(206, 7)) == 0))
                {
                        lsErrores.Add(stFolio + regresaValorCatalogo("0316"));
                }
                //ABH 05/05/2011. Extension. Cambia pos 208 a 213
                if (!Numero(stCadena.Substring(213, 4)) || (esMandatorio("0317") && double.Parse(stCadena.Substring(213, 4)) == 0))
                {                    
                        lsErrores.Add(stFolio + regresaValorCatalogo("0317"));
                }
            }
        }
        
        //Funcion que checa los datos con cabecera 06, Layout con Catalogos
        public void Checa06(string stCadena)
        {
            stCadena = stCadena.Trim();            
            
            if (stCadena.Length < 305) //ABH 05/05/2011. Cambia de 225 a 305
                lsErrores.Add(stFolio + "0600REGISTRO INCOMPLETO");            
            else
            {
                if (!Numero(stCadena.Substring(2, 16)) || (esMandatorio("0602") && double.Parse(stCadena.Substring(2, 16)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("0602"));
                }
                else
                {
                    stFolio = stCadena.Substring(2, 16);
                    lsFolios.Add(stFolio);
                }
                if (!Letra_Esp(stCadena.Substring(18, 50)))  //ABH 05/05/2011. Nombre. Cambia long 30 a 50
                {
                    if (esMandatorio("0603"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("0603"));
                }
                if (!Letra_Esp(stCadena.Substring(68, 60))) //ABH 05/05/2011. A.Paterno. Cambia long 30 a 60, pos 48 a 68
                {
                    if (esMandatorio("0604"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("0604"));
                }
                if (!Letra_Esp(stCadena.Substring(128, 60))) //ABH 05/05/2011. A.Materno. Cambia long 30 a 60, pos 78 a 128
                {
                    if (esMandatorio("0605"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("0605"));
                }
                if (!Alfanumerico_Esp(stCadena.Substring(188, 35))) //ABH 05/05/2011. Domicilio. Cambia pos 108 a 188
                {
                    if (esMandatorio("0606"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("0606"));
                }
                if (!Alfanumerico_Esp(stCadena.Substring(223, 35))) //ABH 05/05/2011. Colonia. Cambia pos 143 a 223
                {
                    if (esMandatorio("0607"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("0607"));
                }
                if (!Alfanumerico_Esp(stCadena.Substring(258, 26))) //ABH 05/05/2011. Municipio. Cambia pos 178 a 258
                {
                    if (esMandatorio("0608"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("0608"));
                }
                if (!Estado(stCadena.Substring(284, 4))) //ABH 05/05/2011. Estado. Cambia pos 204 a 284
                {
                    if (esMandatorio("0609"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("0609"));
                }
                //if (!Numero_Esp(stCadena.Substring(208, 5)))
                //ABH 05/05/2011. CP. Cambia pos 208 a 288
                if (!Numero(stCadena.Substring(288, 5)) || (esMandatorio("0610") && double.Parse(stCadena.Substring(288, 5)) == 0))
                {
                        lsErrores.Add(stFolio + regresaValorCatalogo("0610"));
                }
                //ABH 05/05/2011. Lada. Cambia pos 213 a 293
                if (!Numero(stCadena.Substring(293, 3)) || (esMandatorio("0611") && double.Parse(stCadena.Substring(293, 3)) == 0))
                {
                        lsErrores.Add(stFolio + regresaValorCatalogo("0611"));
                }
                //ABH 05/05/2011. Telefono. Cambia pos 216 a 296
                if (!Numero(stCadena.Substring(296, 7)) || (esMandatorio("0612") && double.Parse(stCadena.Substring(296, 7)) == 0))
                {                    
                        lsErrores.Add(stFolio + regresaValorCatalogo("0612"));
                }
                //ABH 05/05/2011. Parentesco. Cambia pos 223 a 303
                if (!NParentezco(stCadena.Substring(303, 2)))
                {
                    if (esMandatorio("0613"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("0613"));
                }
            }
        }                
        
        //Funcion que checa los datos con cabecera 08, Layout con Catalogos
        public void Checa08(string stCadena)
        {
            stCadena = stCadena.Trim();

            if (stCadena.Length < 18)
                lsErrores.Add(stFolio + "0800REGISTRO INCOMPLETO O EXCEDENTES");

            else
            {
                if (stCadena.Length > 18 && stCadena.Length < 94)
                {
                    if (!Numero(stCadena.Substring(2, 16)) || (esMandatorio("0802") && double.Parse(stCadena.Substring(2, 16)) == 0))
                    {
                        lsErrores.Add(stFolio + regresaValorCatalogo("0802"));
                    }
                    else
                    {
                        stFolio = stCadena.Substring(2, 16);
                        lsFolios.Add(stFolio);
                    }
                    if (!NEmisor(stCadena.Substring(18, 3)))
                    {
                        if (esMandatorio("0803"))
                            lsErrores.Add(stFolio + regresaValorCatalogo("0803"));
                    }
                    if (!Alfanumerico_Esp(stCadena.Substring(21, 25)))
                    {
                        if (esMandatorio("0804"))
                            lsErrores.Add(stFolio + regresaValorCatalogo("0804"));
                    }
                    if (!Alfanumerico_Esp(stCadena.Substring(46, 25)))
                    {
                        if (esMandatorio("0805"))
                            lsErrores.Add(stFolio + regresaValorCatalogo("0805"));
                    }
                    if (!Numero(stCadena.Substring(71, 4)) || (esMandatorio("0806") && double.Parse(stCadena.Substring(71, 4)) == 0))
                    {
                        lsErrores.Add(stFolio + regresaValorCatalogo("0806"));
                    }
                    if (!NTipoCredito(stCadena.Substring(75, 2)))
                    {
                        if (esMandatorio("0807"))
                            lsErrores.Add(stFolio + regresaValorCatalogo("0807"));
                    }
                    if (!Numero(stCadena.Substring(77, 16)) || (esMandatorio("0808") && double.Parse(stCadena.Substring(77, 16)) == 0))
                    {
                        lsErrores.Add(stFolio + regresaValorCatalogo("0808"));
                    }
                }
            }
        }                

        //Funcion que checa los datos con cabecera 09, Layout con Catalogos
        public void Checa09(string stCadena)
        {
            stCadena = stCadena.Trim();                        
            if (stCadena.Length < 18)            
                lsErrores.Add(stFolio + "0900REGISTRO INCOMPLETO O EXCEDENTES");            
            else
            {
                if (stCadena.Length > 18 && stCadena.Length < 38)
                {
                    if (!Numero(stCadena.Substring(2, 16)) || (esMandatorio("0902") && double.Parse(stCadena.Substring(2, 16)) == 0))
                    {                        
                            lsErrores.Add(stFolio + regresaValorCatalogo("0902"));
                    }
                    else
                    {
                        stFolio = stCadena.Substring(2, 16);
                        lsFolios.Add(stFolio);
                    }
                    if (!NEmisor(stCadena.Substring(18, 3)))
                    {
                        if (esMandatorio("0903"))
                            lsErrores.Add(stFolio + regresaValorCatalogo("0903"));
                    }
                    if (!Numero(stCadena.Substring(21, 16)) || (esMandatorio("0904") && double.Parse(stCadena.Substring(21, 16)) == 0))
                    {
                            lsErrores.Add(stFolio + regresaValorCatalogo("0904"));
                    }
                }
            }
        }        
        
        //Funcion que checa los datos con cabecera 11
        public void Checa11(string stCadena)
        {
            stCadena = stCadena.Trim();
            if (stCadena.Length < 90)
            {
                lsErrores.Add(stFolio + "1100REGISTRO INCOMPLETO");
            }
            else
            {
                if (!Numero(stCadena.Substring(2, 16)) || (esMandatorio("1102") && double.Parse(stCadena.Substring(2, 16)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("1102"));
                }
                else
                {
                    stFolio = stCadena.Substring(2, 16);
                    lsFolios.Add(stFolio);
                }
                if (!Numero(stCadena.Substring(18, 8)) || (esMandatorio("1103") && double.Parse(stCadena.Substring(18, 8)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("1103"));
                }
                if (!Numero(stCadena.Substring(26, 8)) || (esMandatorio("1104") && double.Parse(stCadena.Substring(26, 8)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("1104"));
                }
                if (!Numero(stCadena.Substring(34, 8)) || (esMandatorio("1105") && double.Parse(stCadena.Substring(34, 8)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("1105"));
                }
                if (!Numero(stCadena.Substring(42, 8)) || (esMandatorio("1106") && double.Parse(stCadena.Substring(42, 8)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("1106"));
                }
                if (!Numero(stCadena.Substring(50, 8)) || (esMandatorio("1107") && double.Parse(stCadena.Substring(50, 8)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("1107"));
                }
                if (!Numero(stCadena.Substring(58, 8)) || (esMandatorio("1108") && double.Parse(stCadena.Substring(58, 8)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("1108"));
                }
                if (!Numero(stCadena.Substring(66, 8)) || (esMandatorio("1109") && double.Parse(stCadena.Substring(66, 8)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("1109"));
                }
                if (!Numero(stCadena.Substring(74, 8)) || (esMandatorio("1110") && double.Parse(stCadena.Substring(74, 8)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("1110"));
                }
                if (!Numero(stCadena.Substring(82, 8)) || (esMandatorio("1111") && double.Parse(stCadena.Substring(82, 8)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("1111"));
                }
            }
        }
        
        //Funcion que checa los datos con cabecera 12, Nuevo Layout
        public void Checa12(string stCadena)
        {
            stCadena = stCadena.Trim();
            if (stCadena.Length < 18 || stCadena.Length < 198 && stCadena.Length != 18) //ABH 05/05/2011. Cambia de 118 a 198
            {
                lsErrores.Add(stFolio + "1200REGISTRO INCOMPLETO");
            }
            else
            {
                if (stCadena.Length > 18 && stCadena.Length < 199) //ABH 05/05/2011. Cambia de 119 a 199
                {
                    if (!Numero(stCadena.Substring(2, 16)) || (esMandatorio("1202") && double.Parse(stCadena.Substring(2, 16)) == 0))
                    {
                        lsErrores.Add(stFolio + regresaValorCatalogo("1202"));
                    }
                    else
                    {
                        stFolio = stCadena.Substring(2, 16);
                        lsFolios.Add(stFolio);
                    }
                    if (!Letra_Esp(stCadena.Substring(18, 50))) //ABH 05/05/2011. Nombre. Cambia long 30 a 50
                    {
                        if (esMandatorio("1203"))
                            lsErrores.Add(stFolio + regresaValorCatalogo("1203"));
                    }
                    if (!Letra_Esp(stCadena.Substring(68, 60))) //ABH 05/05/2011. A. Paterno. Cambia pos 48 a 68, long 30 a 60
                    {
                        if (esMandatorio("1204"))
                            lsErrores.Add(stFolio + regresaValorCatalogo("1204"));
                    }
                    if (!Letra_Esp(stCadena.Substring(128, 60))) //ABH 05/05/2011. A. Materno. Cambia pos 78 a 128, long 30 a 60
                    {
                        if (esMandatorio("1205"))
                            lsErrores.Add(stFolio + regresaValorCatalogo("1205"));
                    }
                    if (!NParentezco(stCadena.Substring(188, 2))) //ABH 05/05/2011. Parentesco. Cambia pos 108 a 188
                    {
                        if (esMandatorio("1206"))
                            lsErrores.Add(stFolio + regresaValorCatalogo("1206"));
                    }
                    //if (!Numero(stCadena.Substring(110, 6)) || (esMandatorio("1207") && double.Parse(stCadena.Substring(110, 6)) == 0))
                    //{
                    //    lsErrores.Add(stFolio + regresaValorCatalogo("1207"));
                    //}
                    if (!Fecha(stCadena.Substring(190, 6))) //ABH 05/05/2011. Fec.Nac. Cambia pos 110 a 190
                    {
                        if(esMandatorio("1207"))
                            lsErrores.Add(stFolio + regresaValorCatalogo("1207"));
                    }
                    if (!Fem_masc(stCadena.Substring(196, 1))) //ABH 05/05/2011. Sexo. Cambia pos 116 a 196
                    {
                        if (esMandatorio("1208"))
                        //if (esMandatorio("1208") || char.IsDigit(stCadena.Substring(116, 1), 0))
                            lsErrores.Add(stFolio + regresaValorCatalogo("1208"));
                    }
                    if (!Si_no(stCadena.Substring(197, 1))) //ABH 05/05/2011. Firma Ad. Cambia pos 117 a 197
                    {
                        if (esMandatorio("1209"))
                            lsErrores.Add(stFolio + regresaValorCatalogo("1209"));
                    }
                }
            }
        }
        
        //Funcion que checa los datos con cabecera 13
        public void Checa13(string stCadena)
        {
            stCadena = stCadena.Trim();
            if (stCadena.Length < 111)
                lsErrores.Add(stFolio + "1300REGISTRO INCOMPLETO O EXCEDENTES");

            else
            {
                if (!Numero(stCadena.Substring(2, 16)) || (esMandatorio("1302") && double.Parse(stCadena.Substring(2, 16)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("1302"));
                }
                else
                {
                    stFolio = stCadena.Substring(2, 16);
                    lsFolios.Add(stFolio);
                }
                if (!Alfanumerico(stCadena.Substring(18, 4)))
                {
                    if (esMandatorio("1303"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("1303"));
                }
                //if (!Alfanumerico_Esp(stCadena.Substring(22, 2)))
                //{
                //    if (esMandatorio("1304"))
                //        lsErrores.Add(stFolio + regresaValorCatalogo("1304"));
                //}
                if (!Numero(stCadena.Substring(22, 2)) || (esMandatorio("1304") && double.Parse(stCadena.Substring(22, 2)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("1304"));
                }

                if (!Alfanumerico_Esp(stCadena.Substring(24, 24)))
                {
                    if (esMandatorio("1305"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("1305"));
                }
                if (!Numero(stCadena.Substring(48, 11)) || (esMandatorio("1306") && double.Parse(stCadena.Substring(48, 11)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("1306"));
                }
                if (!RFC(stCadena.Substring(59, 10)))
                {
                    if (esMandatorio("1307"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("1307"));
                }
                if (!Letra_Esp(stCadena.Substring(69, 38)))
                {
                    if (esMandatorio("1308"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("1308"));
                }
                if (!Alfanumerico(stCadena.Substring(107, 1)))
                {
                    if (esMandatorio("1309"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("1309"));
                }
                if (!Alfanumerico(stCadena.Substring(108, 3)))
                {
                    if (esMandatorio("1310"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("1310"));
                }
            }
        }

        //Funcion que checa los datos con cabecera 15, Layout con Catalogos
        public void Checa15(string stCadena)
        {
            if (stCadena.Length < 46)
                lsErrores.Add(stFolio + "1500REGISTRO INCOMPLETO");

            else
            {
                if (!Numero(stCadena.Substring(2, 16)) || (esMandatorio("1502") && double.Parse(stCadena.Substring(2, 16)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("1502"));
                }
                else
                {
                    stFolio = stCadena.Substring(2, 16);
                    lsFolios.Add(stFolio);
                }
                if (!Si_no(stCadena.Substring(18, 1)))
                {
                    if (esMandatorio("1503"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("1503"));
                }
                if (!Si_no(stCadena.Substring(19, 1)))
                {
                    if (esMandatorio("1504"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("1504"));
                }
                if (!Si_no(stCadena.Substring(20, 1)))
                {
                    if (esMandatorio("1505"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("1505"));
                }
                if (!Si_no(stCadena.Substring(21, 1)))
                {
                    if (esMandatorio("1506"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("1506"));
                }
                if (!Si_no(stCadena.Substring(22, 1)))
                {
                    if (esMandatorio("1507"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("1507"));
                }
                if (!Si_no(stCadena.Substring(23, 1)))
                {
                    if (esMandatorio("1508"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("1508"));
                }
                if (!NIdentCatalogo(stCadena.Substring(24, 2)))
                {
                    if (esMandatorio("1509"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("1509"));
                }
                if (!Alfanumerico_Esp(stCadena.Substring(26, 20)))
                {
                    if (esMandatorio("1510"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("1510"));
                }
            }
        }
        
        //Funcion que checa los datos con cabecera 16
        public void Checa16(string stCadena)
        {
            stCadena = stCadena.Trim();
            if (stCadena.Length < 181)
            {
                lsErrores.Add(stFolio + "1600REGISTRO INCOMPLETO");
            }
            else
            {
                if (!Numero(stCadena.Substring(2, 16)) || (esMandatorio("1602") && double.Parse(stCadena.Substring(2, 16)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("1602"));
                }
                else
                {
                    stFolio = stCadena.Substring(2, 16);
                    lsFolios.Add(stFolio);
                }
                //if (!Numero_Esp(stCadena.Substring(18, 12)))
                if (!Numero(stCadena.Substring(18, 12)) || (esMandatorio("1603") && double.Parse(stCadena.Substring(18, 12)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("1603"));
                }
                //if (!Alfanumerico(stCadena.Substring(30, 2)))
                //{
                //    if (esMandatorio("1604"))
                //        lsErrores.Add(stFolio + regresaValorCatalogo("1604"));
                //}
                // EN LAYOUT APARECE COMO ALFANUMERICO, CON VALORES 01 MEX y 02 EXTR, SE VALIDA COMO NUMERICO
                if (!Numero(stCadena.Substring(30, 2)) || (esMandatorio("1604") && double.Parse(stCadena.Substring(30, 2)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("1604"));
                }
                if (!Numero(stCadena.Substring(32, 2)) || (esMandatorio("1605") && double.Parse(stCadena.Substring(32, 2)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("1605"));
                }
                if (!Numero(stCadena.Substring(34, 2)) || (esMandatorio("1606") && double.Parse(stCadena.Substring(34, 2)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("1606"));
                }
                if (!Numero(stCadena.Substring(36, 2)) || (esMandatorio("1607") && double.Parse(stCadena.Substring(36, 2)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("1607"));
                }
                if (!Numero(stCadena.Substring(38, 2)) || (esMandatorio("1608") && double.Parse(stCadena.Substring(38, 2)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("1608"));
                }
                if (!Numero(stCadena.Substring(40, 2)) || (esMandatorio("1609") && double.Parse(stCadena.Substring(40, 2)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("1609"));
                }
                //if (!Alfanumerico(stCadena.Substring(42, 2)))
                //{
                //    if (esMandatorio("1610"))
                //        lsErrores.Add(stFolio + regresaValorCatalogo("1610"));
                //}
                // EN LAYOUT APARECE COMO ALFANUMERICO, CON VALORES 01 SEC. PUBL y 02 SEC. PRIVA, SE VALIDA COMO NUMERICO
                if (!Numero(stCadena.Substring(42, 2)) || (esMandatorio("1610") && double.Parse(stCadena.Substring(42, 2)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("1610"));
                }

                if (!Alfanumerico_Esp(stCadena.Substring(44, 1)))
                {
                    if (esMandatorio("1611"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("1611"));
                }
                if (!Alfanumerico_Esp(stCadena.Substring(45, 30)))
                {
                    if (esMandatorio("1612"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("1612"));
                }
                if (!Alfanumerico_Esp(stCadena.Substring(75, 30)))
                {
                    if (esMandatorio("1613"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("1613"));
                }
                if (!Alfanumerico_Esp(stCadena.Substring(105, 30)))
                {
                    if (esMandatorio("1614"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("1614"));
                }
                if (!Alfanumerico_Esp(stCadena.Substring(135, 30)))
                {
                    if (esMandatorio("1615"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("1615"));
                }

                if (!Fecha(stCadena.Substring(165, 8)))
                {
                    if (esMandatorio("1616"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("1616"));
                }

                if (!Fecha(stCadena.Substring(173, 8)))
                {
                    if (esMandatorio("1617"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("1617"));
                }

                //if (!Numero(stCadena.Substring(165, 8)) || (esMandatorio("1616") && double.Parse(stCadena.Substring(165, 8)) == 0))
                //{
                //    lsErrores.Add(stFolio + regresaValorCatalogo("1616"));
                //}
                //if (!Numero(stCadena.Substring(173, 8)) || (esMandatorio("1617") && double.Parse(stCadena.Substring(173, 8)) == 0))
                //{
                //    lsErrores.Add(stFolio + regresaValorCatalogo("1617"));
                //}
            }
        }

        //Funcion que checa los datos con cabecera 18
        public void Checa18(string stCadena)
        {
            //stCadena = stCadena.Trim();
            if (stCadena.Length < 44)
                lsErrores.Add(stFolio + "1800REGISTRO INCOMPLETO");

            else
            {
                if (!Numero(stCadena.Substring(2, 16)) || (esMandatorio("1802") && double.Parse(stCadena.Substring(2, 16)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("1802"));
                }
                else
                {
                    stFolio = stCadena.Substring(2, 16);
                    lsFolios.Add(stFolio);
                }
                if (!Estado(stCadena.Substring(18, 2)))
                {
                    if (esMandatorio("1803"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("1803"));
                }
                //if (!Alfanumerico(stCadena.Substring(20, 4)))
                //{
                //    if (esMandatorio("1804"))
                //        lsErrores.Add(stFolio + regresaValorCatalogo("1804"));
                //}
                if (!Numero(stCadena.Substring(20, 4)) || (esMandatorio("1804") && double.Parse(stCadena.Substring(20, 4)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("1804"));
                }
                //if (!Alfanumerico_Esp(stCadena.Substring(24, 2)))
                //{
                //    if (esMandatorio("1805"))
                //        lsErrores.Add(stFolio + regresaValorCatalogo("1805"));
                //}
                if (!Numero(stCadena.Substring(24, 2)) || (esMandatorio("1805") && double.Parse(stCadena.Substring(24, 2)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("1805"));
                }
                //if (!Alfanumerico_Esp(stCadena.Substring(26, 4)))
                //{
                //    if (esMandatorio("1806"))
                //        lsErrores.Add(stFolio + regresaValorCatalogo("1806"));
                //}
                if (!Numero(stCadena.Substring(26, 4)) || (esMandatorio("1806") && double.Parse(stCadena.Substring(26, 4)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("1806"));
                }
                //if (!Alfanumerico(stCadena.Substring(30, 4)))
                //{
                //    if (esMandatorio("1807"))
                //        lsErrores.Add(stFolio + regresaValorCatalogo("1807"));
                //}
                if (!Numero(stCadena.Substring(30, 4)) || (esMandatorio("1807") && double.Parse(stCadena.Substring(30, 4)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("1807"));
                }

                if (!RFC(stCadena.Substring(34, 10)))
                {
                    if (esMandatorio("1808"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("1808"));
                }
            }
        }
        
        //Funcion que checa los datos con cabecera 19
        public void Checa19(string stCadena)
        {
            if (stCadena.Length < 157) //ABH 05/05/2011. cambia de 116 a 157
                lsErrores.Add(stFolio + "1900REGISTRO INCOMPLETO");

            else
            {
                if (!Numero(stCadena.Substring(2, 16)) || (esMandatorio("1902") && double.Parse(stCadena.Substring(2, 16)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("1902"));
                }
                else
                {
                    stFolio = stCadena.Substring(2, 16);
                    lsFolios.Add(stFolio);
                }
                //PLAZO
                if (!Numero(stCadena.Substring(18, 2)) || (esMandatorio("1903") && double.Parse(stCadena.Substring(18, 2)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("1903"));
                }
                //Número de Cuenta de su Nómina
                if (!Numero(stCadena.Substring(20, 16)) || (esMandatorio("1904") && double.Parse(stCadena.Substring(20, 16)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("1904"));
                }
                //CURP
                if (!Alfanumerico_Esp(stCadena.Substring(36, 18))) //ABH 05/05/2011. CURP. Cambia long 13 a 18
                {
                    if (esMandatorio("1905"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("1905"));
                }
                // EMAIL
                if (!Correo(stCadena.Substring(54, 78))) //ABH 05/05/2011. E-MAIL. Cambia pos 49 a 54, long 42 a 78
                {
                    if (esMandatorio("1906"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("1906"));
                }
                //if (!Numero_Esp(stCadena.Substring(91, 13))) 
                //ABH 05/05/2011. Celular. Cambia pos 91 a 132 
                //TELEFONO CELULAR
                if (!Numero(stCadena.Substring(132, 13)) || (esMandatorio("1907") && double.Parse(stCadena.Substring(132, 13)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("1907"));
                }
                if (!Alfanumerico(stCadena.Substring(145, 10))) //ABH 05/05/2011. Cve Prom. Cambia pos 104 a 145
                {
                    if (esMandatorio("1908"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("1908"));
                }
                if (!Alfanumerico(stCadena.Substring(155, 2))) //ABH 05/05/2011. Tipo Comisión. Cambia pos 114 a 155
                {
                    if (esMandatorio("1909"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("1909"));
                }
            }
        }

        //Funcion que checa los datos con cabecera 20
        public void Checa20(string stCadena)
        {
            stCadena = stCadena.Trim();
            if (stCadena.Length < 135)
                lsErrores.Add(stFolio + "2000REGISTRO INCOMPLETO O EXCEDENTES");

            else
            {
                if (!Numero(stCadena.Substring(2, 16)) || (esMandatorio("2002") && double.Parse(stCadena.Substring(2, 16)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("2002"));
                }
                else
                {
                    stFolio = stCadena.Substring(2, 16);
                    lsFolios.Add(stFolio);
                }
                if (!Num_si_no(stCadena.Substring(18, 1)))
                {
                    if (esMandatorio("2003"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("2003"));
                }
                if (!Num_si_no(stCadena.Substring(19, 1)))
                {
                    if (esMandatorio("2004"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("2004"));
                }
                if (!Num_si_no(stCadena.Substring(20, 1)))
                {
                    if (esMandatorio("2005"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("2005"));
                }
                if (!Num_si_no(stCadena.Substring(21, 1)))
                {
                    if (esMandatorio("2006"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("2006"));
                }
                if (!Num_si_no(stCadena.Substring(22, 1)))
                {
                    if (esMandatorio("2007"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("2007"));
                }
                if (!Num_si_no(stCadena.Substring(23, 1)))
                {
                    if (esMandatorio("2008"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("2008"));
                }
                if (!Numero(stCadena.Substring(24, 18)) || (esMandatorio("2009") && double.Parse(stCadena.Substring(24, 18)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("2009"));
                }
                if (!Numero(stCadena.Substring(42, 15)) || (esMandatorio("2010") && double.Parse(stCadena.Substring(42, 15)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("2010"));
                }
                if (!Num_si_no(stCadena.Substring(57, 1)))
                {
                    if (esMandatorio("2011"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("2011"));
                }
                if (!Fecha(stCadena.Substring(58, 8)))
                {
                    if (esMandatorio("2012"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("2012"));
                }
                //if (!Numero(stCadena.Substring(58, 8)) || (esMandatorio("2012") && double.Parse(stCadena.Substring(58, 8)) == 0))
                //{
                //    lsErrores.Add(stFolio + regresaValorCatalogo("2012"));
                //}
                if (!Numero(stCadena.Substring(66, 2)) || (esMandatorio("2013") && double.Parse(stCadena.Substring(66, 2)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("2013"));
                }
                if (!NBanco(stCadena.Substring(68, 2)))
                {
                    if (esMandatorio("2014"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("2014"));
                }
                if (!NBanco(stCadena.Substring(70, 2)))
                {
                    if (esMandatorio("2015"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("2015"));
                }
                if (!Numero(stCadena.Substring(72, 2)) || (esMandatorio("2016") && double.Parse(stCadena.Substring(72, 2)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("2016"));
                }
                if (!Numero(stCadena.Substring(74, 16)) || (esMandatorio("2017") && double.Parse(stCadena.Substring(74, 16)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("2017"));
                }
                if (!Numero(stCadena.Substring(90, 8)) || (esMandatorio("2018") && double.Parse(stCadena.Substring(90, 8)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("2018"));
                }
                if (!Numero(stCadena.Substring(98, 4)) || (esMandatorio("2019") && double.Parse(stCadena.Substring(98, 4)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("2019"));
                }
                if (!Numero(stCadena.Substring(102, 13)) || (esMandatorio("2020") && double.Parse(stCadena.Substring(102, 13)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("2020"));
                }
                if (!Alfanumerico_Esp(stCadena.Substring(115, 18)))
                {
                    if (esMandatorio("2021"))
                        lsErrores.Add(stFolio + regresaValorCatalogo("2021"));
                }
                if (!Numero(stCadena.Substring(133, 2)) || (esMandatorio("2022") && double.Parse(stCadena.Substring(133, 2)) == 0))
                {
                    lsErrores.Add(stFolio + regresaValorCatalogo("2022"));
                }
            }
        }
        //Funcion que checa los datos con cabecera 99
        public void Checa99(string stCadena)
        {
            stCadena = stCadena.Trim();
            string stFolioCero = "0".PadRight(16, '0');
            if (stCadena.Length < 112)
            {
                lsErrores.Add(stFolioCero + "9900REGISTRO INCOMPLETO O EXCEDENTES");
            }
            else
            {
                if (!Numero(stCadena.Substring(2, 5)) || (esMandatorio("9902") && double.Parse(stCadena.Substring(2, 5)) == 0))
                {
                    lsErrores.Add(stFolioCero + "9902Registro 01 Diskette");
                    //lsErrores.Add(stFolio + regresaValorCatalogo("9902"));
                }
                if (!Numero(stCadena.Substring(7, 5)) || (esMandatorio("9903") && double.Parse(stCadena.Substring(7, 5)) == 0))
                {
                    lsErrores.Add(stFolioCero + "9903Registro 02 Diskette");
                    //lsErrores.Add(stFolio + regresaValorCatalogo("9903"));
                }
                if (!Numero(stCadena.Substring(12, 5)) || (esMandatorio("9904") && double.Parse(stCadena.Substring(12, 5)) == 0))
                {
                    lsErrores.Add(stFolioCero + "9904Registro 03 Diskette");
                    //lsErrores.Add(stFolio + regresaValorCatalogo("9904"));
                }
                if (!Numero(stCadena.Substring(17, 5)) || (esMandatorio("9905") && double.Parse(stCadena.Substring(17, 5)) == 0))
                {
                    lsErrores.Add(stFolioCero + "9905Registro 04 Diskette");
                    //lsErrores.Add(stFolio + regresaValorCatalogo("9905"));
                }
                if (!Numero(stCadena.Substring(22, 5)) || (esMandatorio("9906") && double.Parse(stCadena.Substring(22, 5)) == 0))
                {
                    lsErrores.Add(stFolioCero + "9906Registro 05 Diskette");
                    //lsErrores.Add(stFolio + regresaValorCatalogo("9906"));
                }
                if (!Numero(stCadena.Substring(27, 5)) || (esMandatorio("9907") && double.Parse(stCadena.Substring(27, 5)) == 0))
                {
                    lsErrores.Add(stFolioCero + "9907Registro 06 Diskette");
                    //lsErrores.Add(stFolio + regresaValorCatalogo("9907"));
                }
                if (!Numero(stCadena.Substring(32, 5)) || (esMandatorio("9908") && double.Parse(stCadena.Substring(32, 5)) == 0))
                {
                    lsErrores.Add(stFolioCero + "9908Registro 07 Diskette");
                    //lsErrores.Add(stFolio + regresaValorCatalogo("9908"));
                }
                if (!Numero(stCadena.Substring(37, 5)) || (esMandatorio("9909") && double.Parse(stCadena.Substring(37, 5)) == 0))
                {
                    lsErrores.Add(stFolioCero + "9909Registro 08 Diskette");
                    //lsErrores.Add(stFolio + regresaValorCatalogo("9909"));
                }
                if (!Numero(stCadena.Substring(42, 5)) || (esMandatorio("9910") && double.Parse(stCadena.Substring(42, 5)) == 0))
                {
                    lsErrores.Add(stFolioCero + "9910Registro 09 Diskette");
                    //lsErrores.Add(stFolio + regresaValorCatalogo("9910"));
                }
                if (!Numero(stCadena.Substring(47, 5)) || (esMandatorio("9911") && double.Parse(stCadena.Substring(47, 5)) == 0))
                {
                    lsErrores.Add(stFolioCero + "9911Registro 10 Diskette");
                    //lsErrores.Add(stFolio + regresaValorCatalogo("9912"));
                }
                if (!Numero(stCadena.Substring(52, 5)) || (esMandatorio("9912") && double.Parse(stCadena.Substring(52, 5)) == 0))
                {
                    lsErrores.Add(stFolioCero + "9912Registro 11 Diskette");
                    //lsErrores.Add(stFolio + regresaValorCatalogo("9912"));
                }
                if (!Numero(stCadena.Substring(57, 5)) || (esMandatorio("9913") && double.Parse(stCadena.Substring(57, 5)) == 0))
                {
                    lsErrores.Add(stFolioCero + "9913Registro 12 Diskette");
                    //lsErrores.Add(stFolio + regresaValorCatalogo("9913"));
                }
                if (!Numero(stCadena.Substring(62, 5)) || (esMandatorio("9914") && double.Parse(stCadena.Substring(62, 5)) == 0))
                {
                    lsErrores.Add(stFolioCero + "9914Registro 13 Diskette");
                    //lsErrores.Add(stFolio + regresaValorCatalogo("9914"));
                }
                if (!Numero(stCadena.Substring(67, 5)) || (esMandatorio("9915") && double.Parse(stCadena.Substring(67, 5)) == 0))
                {
                    lsErrores.Add(stFolioCero + "9915Registro 14 Diskette");
                    //lsErrores.Add(stFolio + regresaValorCatalogo("9915"));
                }
                if (!Numero(stCadena.Substring(72, 5)) || (esMandatorio("9916") && double.Parse(stCadena.Substring(72, 5)) == 0))
                {
                    lsErrores.Add(stFolioCero + "9916Registro 15 Diskette");
                    //lsErrores.Add(stFolio + regresaValorCatalogo("9916"));
                }
                if (!Numero(stCadena.Substring(77, 5)) || (esMandatorio("9917") && double.Parse(stCadena.Substring(77, 5)) == 0))
                {
                    lsErrores.Add(stFolioCero + "9917Registro 16 Diskette");
                    //lsErrores.Add(stFolio + regresaValorCatalogo("9917"));
                }
                if (!Numero(stCadena.Substring(82, 5)) || (esMandatorio("9918") && double.Parse(stCadena.Substring(82, 5)) == 0))
                {
                    lsErrores.Add(stFolioCero + "9918Registro 17 Diskette");
                    //lsErrores.Add(stFolio + regresaValorCatalogo("9918"));
                }
                if (!Numero(stCadena.Substring(87, 5)) || (esMandatorio("9919") && double.Parse(stCadena.Substring(87, 5)) == 0))
                {
                    lsErrores.Add(stFolioCero + "9919Registro 18 Diskette");
                    //lsErrores.Add(stFolio + regresaValorCatalogo("9919"));
                }
                if (!Numero(stCadena.Substring(92, 5)) || (esMandatorio("9920") && double.Parse(stCadena.Substring(92, 5)) == 0))
                {
                    lsErrores.Add(stFolioCero + "9920Registro 19 Diskette");
                    //lsErrores.Add(stFolio + regresaValorCatalogo("9920"));
                }
                if (stCadena.Substring(97, 10).Trim() != "")
                {
                    lsErrores.Add(stFolioCero + "9921Espacios");
                    //lsErrores.Add(stFolio + regresaValorCatalogo("9921"));
                }
                if (!Numero(stCadena.Substring(107, 5)) || esMandatorio("9922") && (double.Parse(stCadena.Substring(107, 5)) == 0))
                {
                    lsErrores.Add(stFolioCero + "9922Cantidad de Solicitudes");
                    //lsErrores.Add(stFolio + regresaValorCatalogo("9922"));
                }
                else
                {
                    iNumFolios = int.Parse(stCadena.Substring(107, 5));
                }
            }
        }

        #region Verificado de Formatos
        //Funcion que checa que los datos sean numericos
        public bool Numero(string stCad)
        {                                    
            Regex rgxNumero = new Regex("[0-9]");
            MatchCollection mcCoincidencias = rgxNumero.Matches(stCad);
            if (mcCoincidencias.Count < stCad.Length)
                return false;

            return true;
        }

        public bool Fecha(string stCadena)
        {
            DateTimePicker dtpTemp = new DateTimePicker();            
            MaskedTextBox mtbTemp = new MaskedTextBox();
            mtbTemp.Mask = "9999/99/99";

            int iYear;
            if (stCadena.Trim() != "")
            {
                for(int iNdex = 0; iNdex < stCadena.Length; iNdex++)                
                    if(stCadena[iNdex] == ' ')
                        return false;

                if (stCadena.Length == 6)
                {
                    iYear = int.Parse(stCadena.Substring(0, 2));
                    if (iYear + 2000 <= DateTime.Today.Year)                        
                        stCadena = "20" + stCadena;
                    else
                        stCadena = "19" + stCadena;                                                                                    
                }
                
                mtbTemp.Text = stCadena;

                try
                {
                    dtpTemp.Text = mtbTemp.Text;
                    return true;
                }
                catch
                {
                    return false;
                }

            }
            return false;
        }

        //Funcion que checa que los datos sean conforme al catalogo banco
        public bool Banco(string stCad)
        {
            if (stCad.Trim() == "")
                return false;
            bool bResp = true;
            stCad = stCad.Trim();
            if (stCad != "")
            {
                try
                {
                    if (Convert.ToInt32(stCad) < 0 || Convert.ToInt32(stCad) > 11)
                        bResp = false;
                }
                catch
                {
                    bResp = false;
                }
            }
            return bResp;
        }
        //Funcion que checa que los datos sean conforme al catalogo estado
        public bool Estado(string stCad)
        {
            if (stCad.Trim() == "")
                return false;

            bool bResp = true;
            stCad = stCad.Trim();
            if (stCad != "")
            {
                try
                {
                    if (Convert.ToInt32(stCad) < 0 || Convert.ToInt32(stCad) > 32)
                        bResp = false;
                }
                catch
                {
                    bResp = false;
                }
            }
            return bResp;
        }
        //Funcion que checa que los datos sean conforme al catalogo estado civil
        public bool Civil(string stCad)
        {
            if (stCad.Trim() == "")
                return false;

            bool bResp = true;
            Regex rgxNumero = new Regex("[sScCvVdDuUnN\\s]");
            MatchCollection mcCoincidencias = rgxNumero.Matches(stCad);
            if (mcCoincidencias.Count < stCad.Length)
                bResp = false;
            return bResp;
        }
        //Funcion que checa que los datos sean conforme al catalogo de vivienda
        public bool Vivienda(string stCad)
        {
            if (stCad.Trim() == "")
                return false;
            bool bResp = true;
            Regex rgxNumero = new Regex("[pPrRhHfFnN\\s]");
            MatchCollection mcCoincidencias = rgxNumero.Matches(stCad);
            if (mcCoincidencias.Count < stCad.Length)
                bResp = false;
            return bResp;
        }
        //Funcion que checa que los datos sean conforme al catalogo de tipo de empleo
        public bool Tipo_Empleo(string stCad)
        {
            if (stCad.Trim() == "")
                return false;
            bool bResp = true;
            Regex rgxNumero = new Regex("[eEnNpP\\s]");
            MatchCollection mcCoincidencias = rgxNumero.Matches(stCad);
            if (mcCoincidencias.Count < stCad.Length)
                bResp = false;
            return bResp;
        }        
        //Funcion que checa que los datos sean conforme al catalogo de parentezco
        public bool Parentezco(string stCad)
        {
            if (stCad.Trim() == "")
                return false;
            bool bResp = true;
            Regex rgxNumero = new Regex("[0-9tTcChHpPaAnNoO]");
            MatchCollection mcCoincidencias = rgxNumero.Matches(stCad);
            if (mcCoincidencias.Count < stCad.Length)
                bResp = false;
            return bResp;
        }
        //Funcion que checa que los datos sean conforme al catalogo de escolaridad
        public bool Escolaridad(string stCad)
        {
            if (stCad.Trim() == "")
                return false;
            bool bResp = true;
            //Regex rgxNumero = new Regex("[0rRsSpPtTlLmMnN]");
            Regex rgxNumero = new Regex("[0-9\\s]");
            MatchCollection mcCoincidencias = rgxNumero.Matches(stCad);
            if (mcCoincidencias.Count < stCad.Length)
                bResp = false;
            return bResp;
        }

        //Funcion que checa que los datos sean conforme al catalogo 87 Ident Catalogo NUMERICO
        public bool NBanco(string stCad)
        {
            if (stCad.Trim() == "")
                return false;
            try
            {
                if (int.Parse(stCad) > 0 && int.Parse(stCad) < 99)
                    return true;
            }
            catch
            {
                return false;
            }
            return false;
        }

        //Funcion que checa que los datos sean conforme al catalogo 87 Ident Catalogo NUMERICO
        public bool NIdentCatalogo(string stCad)
        {
            if (stCad.Trim() == "")
                return false;
            try
            {
                if (int.Parse(stCad) > 0 && int.Parse(stCad) < 7)
                    return true;
            }
            catch
            {
                return false;
            }
            return false;
        }
        //Funcion que checa que los datos sean conforme al catalogo 110 Estado Civil NUMERICO
        public bool NTipoCredito(string stCad)
        {
            if (stCad.Trim() == "")
                return false;
            try
            {
                if ((int.Parse(stCad) > 0 && int.Parse(stCad) < 9) || (int.Parse(stCad) == 99))
                    return true;
            }
            catch
            {
                return false;
            }
            return false;
        }
        //Funcion que checa que los datos sean conforme al catalogo 43 Estado Civil NUMERICO
        public bool NEmisor(string stCad)
        {
            if (stCad.Trim() == "")
                return false;
            try
            {
                if (int.Parse(stCad) > 0 && int.Parse(stCad) < 5)
                    return true;
            }
            catch
            {
                return false;
            }
            return false;
        }
        //Funcion que checa que los datos sean conforme al catalogo 20 Estado Civil NUMERICO
        public bool NCivil(string stCad)
        {
            if (stCad.Trim() == "")
                return false;
            try
            {
                if ((int.Parse(stCad) > 0 && int.Parse(stCad) < 8) || int.Parse(stCad) == 9)
                    return true;
            }
            catch
            {
                return false;
            }            
            return false;
        }
        //Funcion que checa que los datos sean conforme al catalogo 28 Vivienda NUMERICO
        public bool NVivienda(string stCad)
        {
            if (stCad.Trim() == "")
                return false;
            try
            {
                if (int.Parse(stCad) > 0 && int.Parse(stCad) < 6)
                    return true;
            }            
            catch
            {
                return false;
            }
            return false;
        }
        //Funcion que checa que los datos sean conforme al catalogo 22 Escolaridad NUMERICO
        public bool NEscolaridad(string stCad)
        {
            if (stCad.Trim() == "")
                return false;
            try
            {
                if (int.Parse(stCad) > 0 && int.Parse(stCad) < 8)
                    return true;
            }
            catch
            {
                return false;
            }
            return false;
        }
        //Funcion que checa que los datos sean conforme al catalogo 23 Empleo NUMERICO
        public bool NTipo_Empleo(string stCad)
        {
            if (stCad.Trim() == "")
                return false;
            try
            {
                if (int.Parse(stCad) > 0 && int.Parse(stCad) < 40)
                    return true;
            }
            catch
            {
                return false;
            }
            return false;
        }
        //Funcion que checa que los datos sean conforme al catalogo 23 Puesto NUMERICO
        public bool NPuesto(string stCad)
        {
            if (stCad.Trim() == "")
                return false;
            try
            {
                //if ((int.Parse(stCad) > 0 && int.Parse(stCad) < 40) || (int.Parse(stCad) == 97))
                if ((int.Parse(stCad) > 0 && int.Parse(stCad) < 48)) 
                    return true;
            }
            catch
            {
                return false;
            }
            return false;
        }
        //Funcion que checa que los datos sean conforme al catalogo 45 Giro Empresa NUMERICO
        public bool NGiro_Empresa(string stCad)
        {
            ComboBox cbTemp = new ComboBox();
            string stCatGiroEmpresa = "45";            
            string tempRefParam2 = "";
            string tempRefParam3 = "";
            string tempRefParam4 = "";
            string tempRefParam5 = "";
            string tempRefParam6 = "D";
            //string strRangoCveIni = "00" + stCad.Substring(0, 2);
            //string strRangoCveFin = stCad.Substring(2, 4);

            if (stCad.Trim() == "")
                return false;
            try
            {
                cbTemp.Items.Clear();
                int iClave = mdlComunica.OleCatalogos.getLongClave;
                Catalogos.clsCatalogos.enmAlineaciones tempRefParamCatenm = Catalogos.clsCatalogos.enmAlineaciones.Izquierda;
                mdlComunica.OleCatalogos.setLongClave = 6;
                mdlComunica.OleCatalogos.setAlineacionClave = tempRefParamCatenm;
                //public void LlenaCombo(ref  ComboBox cboControl, ref  string strCatalogo, ref  string strLlave1, ref  string strLlave2, ref  string strLlave3, ref  string strLlave4, ref  string strTipoCatalogo, ref  string strRangoCveIni, ref  string strRangoCveFin, ref  bool blnLimpiaCombo)                
                mdlComunica.OleCatalogos.LlenaCombo(ref cbTemp, ref stCatGiroEmpresa, ref tempRefParam2, ref tempRefParam3,
                ref tempRefParam4, ref tempRefParam5, ref tempRefParam6);
                
                if (cbTemp.Items.Count > 0)
                    for (int iCont = 0; iCont < cbTemp.Items.Count; iCont++)
                    {
                        if (cbTemp.Items[iCont].ToString().Substring(0, 6) == stCad)
                        {
                            mdlComunica.OleCatalogos.setLongClave = iClave;
                            return true;
                        }
                    }
                    
                //string stAlgo = cbTemp.Items[0].ToString();
                //if (int.Parse(stCad) > 0 && int.Parse(stCad) < 16)                
                //    return true;
            }
            catch
            {
                return false;
            }
            return false;
        }
        //Funcion que checa que los datos sean conforme al catalogo 41 Parentezco NUMERICO
        public bool NParentezco(string stCad)
        {
            if (stCad.Trim() == "")
                return false;
            try
            {
                if ((int.Parse(stCad) > 0 && int.Parse(stCad) < 15) || (int.Parse(stCad) == 19) ||
                    (int.Parse(stCad) > 22 && int.Parse(stCad) < 25))
                    return true;
            }
            catch
            {
                return false;
            }
            return false;
        }
        
        //Funcion que checa que los datos sean numericos permitiendo espacios
        public bool Numero_Esp2(string stCad)
        {
            if (stCad.Trim() == "")
                return false;
            bool bResp = true;
            Regex rgxNumero = new Regex("[0-9\\s]");
            MatchCollection mcCoincidencias = rgxNumero.Matches(stCad);
            if (mcCoincidencias.Count < stCad.Length)
                bResp = false;
            return bResp;
        }
        //Funcion que checa que los datos sean caracteres
        public bool Letra(string stCad)
        {
            if (stCad.Trim() == "")
                return false;
            bool bResp = true;
            Regex rgxLetra = new Regex("[a-zA-Z]");
            MatchCollection mcCoincidencias = rgxLetra.Matches(stCad);
            if (mcCoincidencias.Count < stCad.Length)
                bResp = false;
            return bResp;
        }
        //Funcion que checa que los datos sean caracteres y numeros
        public bool Alfanumerico(string stCad)
        {
            if (stCad.Trim() == "")
                return false;
            bool bResp = true;
            Regex rgxLetra = new Regex("[a-zA-Z0-9 ]");
            MatchCollection mcCoincidencias = rgxLetra.Matches(stCad);
            if (mcCoincidencias.Count < stCad.Length)
                bResp = false;
            return bResp;
        }
        //Funcion que checa que los datos sean caracteres y numeros permitiendo espacios
        public bool Alfanumerico_Esp(string stCad)
        {            
            if (stCad.Trim() == "")
                return false;
            
            bool bResp = true;
            //Regex rgxLetra = new Regex("[a-zA-Z0-9\\s]");
            Regex rgxLetra = new Regex("[a-zA-Z0-9&@.#;/\\s]");
            MatchCollection mcCoincidencias = rgxLetra.Matches(stCad);
            if (mcCoincidencias.Count < stCad.Length)
                bResp = false;
            return bResp;
        }
        //Funcion que checa que los datos sean caracteres permitiendo espacios
        public bool Letra_Esp(string stCad)
        {
            if (stCad.Trim() == "")
                return false;
            bool bResp = true;
            Regex rgxLetra = new Regex("[a-zA-Z&@#\\s]");
            MatchCollection mcCoincidencias = rgxLetra.Matches(stCad);
            if (mcCoincidencias.Count < stCad.Length)
                bResp = false;
            return bResp;
        }
        //Funcion que checa que los datos sean en formato de correo electronico
        public bool Correo(string stCad)
        {
            if (stCad.Trim() == "")
                return false;
            bool bResp = true;
            stCad = stCad.Trim();
            Regex rgxLetra = new Regex(@"^(([^<>()[\]\\.,;:\s@\""]+" 
                        + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@" 
                        + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}" 
                        + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+" 
                        + @"[a-zA-Z]{2,}))$");
            bResp = rgxLetra.IsMatch(stCad);
            return bResp;
        }
        //Funcion que checa que los datos sean caracteres validos para representar si o no
        public bool Si_no(string stCad)
        {
            if (stCad.Trim() == "")
                return false;
            bool bResp = true;
            Regex rgxLetra = new Regex("[sSnN\\s]");
            MatchCollection mcCoincidencias = rgxLetra.Matches(stCad);
            if (mcCoincidencias.Count < stCad.Length)
                bResp = false;
            return bResp;
        }
        //Funcion que checa que los datos sean caracteres validos para representar si o no mediante un numero binario
        public bool Num_si_no(string stCad)
        {
            if (stCad.Trim() == "")
                return false;
            bool bResp = true;
            Regex rgxLetra = new Regex("[01]");
            MatchCollection mcCoincidencias = rgxLetra.Matches(stCad);
            if (mcCoincidencias.Count < stCad.Length)
                bResp = false;
            return bResp;
        }
        //Funcion que checa que los datos sean caracteres validos para representar femenino o masculino
        public bool Fem_masc(string stCad)
        {
            if (stCad.Trim() == "")
                return false;
            bool bResp = true;
            //Regex rgxLetra = new Regex("[fFmM\\s]");
            Regex rgxLetra = new Regex("[fFmM]");
            MatchCollection mcCoincidencias = rgxLetra.Matches(stCad);
            if (mcCoincidencias.Count < stCad.Length)
                bResp = false;
            return bResp;
        }
        //Funcion que checa que los datos sean caracteres validos para representar casa u oficina
        public bool Casa_ofic(string stCad)
        {
            if (stCad.Trim() == "")
                return false;
            bool bResp = true;
            Regex rgxLetra = new Regex("[cCoO]");
            MatchCollection mcCoincidencias = rgxLetra.Matches(stCad);
            if (mcCoincidencias.Count < stCad.Length)
                bResp = false;
            return bResp;
        }
        //Funcion que checa que los datos sean caracteres validos para representar nacional o internacional
        public bool Nac_Inter(string stCad)
        {
            if (stCad.Trim() == "")
                return false;
            bool bResp = true;
            Regex rgxLetra = new Regex("[nNiI]");
            MatchCollection mcCoincidencias = rgxLetra.Matches(stCad);
            if (mcCoincidencias.Count < stCad.Length)
                bResp = false;
            return bResp;
        }
        //Funcion que checa que los datos sean en formato de RFC
        public bool RFC(string stCad)
        {
            if (stCad.Trim() == "")
                return false;
            bool bResp = true;
            if (!Letra(stCad.Substring(0, 4)))
                return false;
            if (!Numero(stCad.Substring(4)))
                return false;
            return bResp;
        }
        
        #endregion
        
        #region Eventos y Controles
        //Boton de Error, da acceso a la forma de errores        
        private void btnError_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            frmErroresValidacion frmError = new frmErroresValidacion();
            iCell = dgvValRem.SelectedCells[0].RowIndex;

            frmError.MdiParent = Masivos.MDIMasivos.DefInstance;
            frmError.Show();
            //frmError.Show();
            
            Cursor.Current = Cursors.Default;

        }

        //Boton de Captura fisica, da acceso a la forma de validacion fisica        
        private void btnCapt_Click(object sender, EventArgs e)
        {
            iCell = dgvValRem.SelectedCells[0].RowIndex;
            frmCapFolFisico frmFolio = new frmCapFolFisico();            
            frmFolio.ShowDialog();
            
            //frmFolio.MdiParent = Masivos.MDIMasivos.DefInstance;
            //frmFolio.Show();
            
            CargaForma();
        }


        //Cargado de la forma de validación
        private void frmValidaRemesas_Load(object sender, EventArgs e)
        {
            if (cargaCatalogoMandatorio(""))
                CargaForma();
            else
            {
                MessageBox.Show("Error al Cargar Catalogo Mandatorios", "C753 ARIES - Validacion Remesa",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnValida.Enabled = btnError.Enabled = btnCapt.Enabled = false;
            }
        }
        /*
        //Evento que verifica el contenido de las celdas seleccionadas para obtener el status y proceso de la remesa seleccionada
        private void dgvValRem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Condicion que define si el boton de validacion sin fisico queda disponible o no
            if ((dgvValRem[1, dgvValRem.SelectedCells[0].RowIndex].Value.ToString() == "200" && dgvValRem[2, dgvValRem.SelectedCells[0].RowIndex].Value.ToString() == "000") 
             || (dgvValRem[1, dgvValRem.SelectedCells[0].RowIndex].Value.ToString() == "201" && dgvValRem[2, dgvValRem.SelectedCells[0].RowIndex].Value.ToString() == "005"))
            {
                btnValida.Enabled = true;
            }
            else
            {
                btnValida.Enabled = false;
            }
            //Condicion que define si el boton de captura con fisico queda disponible o no
            if ((dgvValRem[1, dgvValRem.SelectedCells[0].RowIndex].Value.ToString() == "201" && dgvValRem[2, dgvValRem.SelectedCells[0].RowIndex].Value.ToString() == "000") 
             || (dgvValRem[1, dgvValRem.SelectedCells[0].RowIndex].Value.ToString() == "202" && dgvValRem[2, dgvValRem.SelectedCells[0].RowIndex].Value.ToString() == "005"))
            {
                btnCapt.Enabled = true;
            }
            else
            {
                btnCapt.Enabled = false;
            }
        }       */
       /*Boton de validacion sin fisico, verifica que los datos en la remesa sean correctos y del formato adecuado,
       tambien si el archivo referido existe y con la nomenglatura correcta*/
        private void btnValida_Click(object sender, EventArgs e)
        {
            clsWRemesas clsValida = new clsWRemesas();
            Cursor.Current = Cursors.WaitCursor;

            //mdlGlobales.subDespMensajes("VALIDANDO ARCHIVO DE REMESA...");

            string stRespuesta  = null;
            //iTotalErrores = 0;
            iPrimerGrupo = 0;
            //Existe Archivo, verifica la existencia de archivo fisicamente en PC, y valida los registros
            if (ExisteArchivo())
            {
                lbEstatus.Text = "Validando Formatos";
                // Validacion de Campos, Numericos, Alfanumericos, y Catalogos
                procesoValidacion();

                lbEstatus.Text = "Validando Folios";
                // Verifica la cantidad de Folios en la remesa, y contra los folios que se mencionan en archivo de control
                OrganizaFolios();

                // Verifica si los Folios Preimpresos no estan duplicados
                if (VerificaPreimpresos())
                {

                    lbEstatus.Text = "Registrando Remesa";
                    if (lsErrores.Count == 0)
                    {
                        stRespuesta = clsValida.RegistroResultadosValidacion5562_61(1, 0, 0, "");
                        if (stRespuesta != null)
                        {
                            MessageBox.Show("ARCHIVO DE REMESA VALIDADO EXITOSAMENTE", "C753 ARIES - Validacion Remesa",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                    if (lsErrores.Count != 0)
                    {
                        registraErrores();

                        MessageBox.Show("REMESA VALIDADA CON ERRORES", "C753 ARIES - Validacion Remesa",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                CargaForma();
                lbEstatus.Text = "Seleccione Remesa";
            }
            Cursor.Current = Cursors.Default;
        }
        #endregion

        private void dgvValRem_CurrentCellChanged(object sender, EventArgs e)
        {
            /*ESTADOS
             * 200, 0   Valida
             * 201, 5   Valida
             * 201, 0   Captura
             * 202, 5   Captura
             * */

            if (dgvValRem.CurrentRow != null)
            {                
                if (dgvValRem.Rows[dgvValRem.CurrentRow.Index].Cells[1].Value.ToString() == "200" && dgvValRem.Rows[dgvValRem.CurrentRow.Index].Cells[2].Value.ToString() == "000")
                {
                    btnValida.Enabled = true;
                }
                else
                    btnValida.Enabled = false;

                if ((dgvValRem.Rows[dgvValRem.CurrentRow.Index].Cells[1].Value.ToString() == "201" && dgvValRem.Rows[dgvValRem.CurrentRow.Index].Cells[2].Value.ToString() == "000") ||
                    (dgvValRem.Rows[dgvValRem.CurrentRow.Index].Cells[1].Value.ToString() == "202" && dgvValRem.Rows[dgvValRem.CurrentRow.Index].Cells[2].Value.ToString() == "005"))
                {
                    btnCapt.Enabled = true;
                }
                else
                    btnCapt.Enabled = false;
                
                if (dgvValRem.Rows[dgvValRem.CurrentRow.Index].Cells[1].Value.ToString() == "200" && dgvValRem.Rows[dgvValRem.CurrentRow.Index].Cells[2].Value.ToString() == "000")
                    btnError.Enabled = false;
                else
                    btnError.Enabled = true;
                
                if(dgvValRem.Rows[dgvValRem.CurrentRow.Index].Cells[1].Value.ToString() == "201" && dgvValRem.Rows[dgvValRem.CurrentRow.Index].Cells[2].Value.ToString() == "005")
                    btnError.Enabled = true;
                else
                    btnError.Enabled = false;
            }            
        }

        private bool cargaCatalogoMandatorio(string tempRefParam3)
        {
            cBMandatorio.Items.Clear();
            string stAtributos = null;
            string stCatalogo = "267";
            
            string tempRefParam1 = stCatalogo;            
            string tempRefParam2 = "";            
            string tempRefParam4 = "";
            string tempRefParam5 = "";
            string tempRefParam6 = "E";
            int iContador = 0;

            mdlComunica.OleCatalogos.LlenaCombo(ref cBMandatorio, ref tempRefParam1, ref tempRefParam2, ref tempRefParam3,
                ref tempRefParam4, ref tempRefParam5, ref tempRefParam6);
            mdlComunica.OleCatalogos.setLongClave = 2;

            llena50caracteres();

            mdlComunica.OleCatalogos.AbreCatalogo(ref stCatalogo, ref tempRefParam6, tempRefParam2, tempRefParam3);
            mdlComunica.OleCatalogos.MoveFirst();
            while (!mdlComunica.OleCatalogos.EOF_Renamed())
            {                
                stAtributos = mdlComunica.OleCatalogos.getAtributos.Trim();
                cBMandatorio.Items[iContador] = cBMandatorio.Items[iContador].ToString() + stAtributos;
                iContador++;
                mdlComunica.OleCatalogos.MoveNext();
            }
            mdlComunica.OleCatalogos.CierraCatalogo();

            if (cBMandatorio.Items.Count > 0)
            {
                try
                {
                    arreglaCombo();
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            return false;
        }

        private void llena50caracteres()
        {
            string stLineaNuevaTemp;
            for (int iLineaCombo = 0; iLineaCombo < cBMandatorio.Items.Count; iLineaCombo++)
            {
                stLineaNuevaTemp = cBMandatorio.Items[iLineaCombo].ToString();
                stLineaNuevaTemp = stLineaNuevaTemp.PadRight(50, ' ');
                cBMandatorio.Items[iLineaCombo] = stLineaNuevaTemp;
            }
        }
        private void arreglaCombo()
        {
            string stLineaNueva, stLineaNuevaTemp, stTamanioReg;
            int iTamReg;
            //001002N0Tipo de Registro                        00001010011001
            for (int iLineaCombo = 0; iLineaCombo < cBMandatorio.Items.Count; iLineaCombo++)
            {
                stLineaNuevaTemp = cBMandatorio.Items[iLineaCombo].ToString();

                iTamReg =  int.Parse(stLineaNuevaTemp.Substring(53, 3)) - 
                           int.Parse(stLineaNuevaTemp.Substring(50, 3)) + 1;
                stTamanioReg = iTamReg.ToString();

                stLineaNueva = stLineaNuevaTemp.Substring(66, 2) + // REGISTRO
                               stLineaNuevaTemp.Substring(70, 2) + // NUMERO DE CAMPO
                               stLineaNuevaTemp.Substring(0, 50) + // DESCRIPCION
                               stLineaNuevaTemp.Substring(68, 1) + // MANDATORIO
                               stTamanioReg.PadLeft(4, '0')      + // TAMAÑO REGISTRO
                               stLineaNuevaTemp.Substring(56, 1) + // TIPO DE DATO, NUMERICO O ALFANUMERICO
                               stLineaNuevaTemp.Substring(58, 3);  // CATALOGO
                
                cBMandatorio.Items[iLineaCombo] = stLineaNueva;
                // RESULTADO: "0101Tipo de Registro                        1"
                // Resultado 13 Abril
                // RESULTADO: "0101Tipo de Registro                        10004A000"
                
                /* CAMBIO NUEVO
                 * 0,50 DESCRIPCION
                 * 50,3 POS INI
                 * 53,3 POS FIN
                 * 56,1 TIPO
                 * 57,1 IND. NULL
                 * 58,3 CATALOLGO
                 * 61,2 TRAMITE FAMILIA
                 * 63,2 FAMILIA PRODUCTO
                 * 65,3 REGISTRO   -> 66,2
                 * 68,1 MANDATORIO
                 * 69,3 N. CAMPO   -> 70,2
                 * */
            }
        }

        
        private bool esMandatorio(string stCampo)
        {
            string stTemporal = null;
            for (int iLineaCombo = 0; iLineaCombo < cBMandatorio.Items.Count; iLineaCombo++)
            {
                stTemporal = cBMandatorio.Items[iLineaCombo].ToString();
                if (stTemporal.Substring(0, 4) == stCampo)
                {
                    if (stTemporal.Substring(54, 1) == "1") // INDICADOR MANDATORIO
                        return true;                    
                }

            }
            return false;
        }

        private int regresaTamanioRegistro(string stCampo)
        {
            //"0101Tipo de Registro                        10004A"
            string stTemporal = null;
            int iTam = 0;
            for (int iLineaCombo = 0; iLineaCombo < cBMandatorio.Items.Count; iLineaCombo++)
            {
                stTemporal = cBMandatorio.Items[iLineaCombo].ToString();
                if (stTemporal.Substring(0, 4) == stCampo)
                {
                    iTam = int.Parse(stTemporal.Substring(55, 4));
                    return iTam;
                }
            }
            return iTam;
        }

        private string regresaValorCatalogo(string stCampo)
        {
            string stTemporal =null;

            if (stCampo == "0000")
                stTemporal = stCampo + "ERROR EN HEADER";
                
            if(stCampo == "9999")
                stTemporal = stCampo + "ERROR EN TRAILER";

            for (int iLineaCombo = 0; iLineaCombo < cBMandatorio.Items.Count; iLineaCombo++)
            {
                stTemporal = cBMandatorio.Items[iLineaCombo].ToString();
                if (stTemporal.Substring(0, 4) == stCampo)
                {
                    stTemporal = stTemporal.Substring(0, 44);
                    stTemporal = stTemporal.Trim();
                    return stTemporal.ToUpper();
                }
            }
            return stCampo;
        }
        
        #endregion
        
    }
}