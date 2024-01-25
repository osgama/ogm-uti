/************************************************************************
 * Forma creada para el registro del Arribo de Remesas                  *
 * **********************************************************************
 * Forma que registra el Arribo de Remesas y valida su existencia y     *
 * congruencia con su archivo de control.                               *
 * **********************************************************************
 * INFOWARE                                                             *
 * **********************************************************************
 * Autor: Miguel Angel Garnica Angulo (Infoware, León)                  *
 *        Adrian Azades Hernandez Belmonte (Infoware, León) (AAHB)      *
 * **********************************************************************
 * Historico de cambios:                                                *
 *                                                                      *
 * Fecha de Creación:   01/10/09                                        *
 *                      Forma creada para registro de remesas.          *
 * Modificaciones:      29/01/10                                        *
 *                      Nuevos ComboBox y Manejo de Fechas              *
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
using System.IO;
using Microsoft.Win32;

namespace Masivos
{
    public partial class frmArriboRemesas : Form
    {        
        List<string> LsRemesas = new List<string>();        
        List<int >   LsContErr = new List<int> ();
        List<Linea>  LsRemComp = new List<Linea> ();

        List<Linea> ListaOrg = new List<Linea>(); // LISTA NUEVA QUE ORGANIZA REMESA Y MENSAJES DEL SERVER
        string stDatosNuevos = null;

        string stTramite = null, stFamilia = null, stTipoSolicitud = null;

        string stRutaRemesas = @"C:\S753_masivos\unzip\";
        /*string stRutaRemesas = System.Environment.GetEnvironmentVariable("HOMEDRIVE") +
                                  System.Environment.GetEnvironmentVariable("HOMEPATH") +
                                  "\\Masivos" + "\\Remesas";*/

        /***********************************************************************************************************/
        // FUNCION:                  frmArriboRemesas
        // PARAMETROS ENTRADA:       
        // PARAMETROS SALIDA:         void
        //					
        // RESULTADO:	              
        //               
        // CREACION:                  10/2009
        // DESCRIPCION:                constructor de la classe 
        //                           
        //                             
        // DESARROLLADO POR:          AAHB (Infoware )
        // ULTIMA MODIFICACION:
        // DESCRIPCION:
        //
        /**********************************************************************************************************/
        public frmArriboRemesas()
        {
            InitializeComponent();
        }
        /***********************************************************************************************************/
        // FUNCION:                  frmArriboRemesas_Load
        // PARAMETROS ENTRADA:       
        // PARAMETROS SALIDA:        (object sender, EventArgs e)
        //					
        // RESULTADO:	              
        //               
        // CREACION:                  10/2009
        // DESCRIPCION:               Metodo Load
        //                           
        //                             
        // DESARROLLADO POR:          AAHB (Infoware )
        // ULTIMA MODIFICACION:
        // DESCRIPCION:
        //
        /**********************************************************************************************************/
        private void frmArriboRemesas_Load(object sender, EventArgs e)
        {
            txbArchControl.Text = null;            
            btnRegistrar.Enabled = false;

            // LA CARGA DE COMBOS DE E. CAPTURA Y PROMOTORA SON LA BASE DE LAS VALIDACIONES
            if (!CargaCombos())
            {
                MessageBox.Show("CATALOGOS INEXISTENTES PARA EMPRESA DE CAPTURA O PROMOTORA", "C753 - Arribo Remesas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbEmpCaptura.Enabled = false;
                cmbPromotora.Enabled = false;
                btnArchControl.Enabled = true;             
            }
          }
          /***********************************************************************************************************/
          // FUNCION:                  btnArchControl_Click
          // PARAMETROS ENTRADA:       
          // PARAMETROS SALIDA:        (object sender, EventArgs e)
          //					
          // RESULTADO:	              
          //               
          // CREACION:                  10/2009
          // DESCRIPCION:               metodo que llama a un open dialog para seleccionar el  archivo  de 
          //                           control  entrada 
          //                             
          // DESARROLLADO POR:          AAHB (Infoware )
          // ULTIMA MODIFICACION:
          // DESCRIPCION:
          //
          /**********************************************************************************************************/
        private void btnArchControl_Click(object sender, EventArgs e)
        {
            opnfdArchivoControl.RestoreDirectory = true;
            opnfdArchivoControl.Title = " Abrir archivo control ";

            
           
            DirectoryInfo DIR = new DirectoryInfo(stRutaRemesas);
            if (!DIR.Exists)
            {
                DIR.Create();
            }

            opnfdArchivoControl.InitialDirectory = stRutaRemesas;
            //opnfdArchivoControl.InitialDirectory = @"C:\S753_masivos\unzip ";
            opnfdArchivoControl.Dispose();
            opnfdArchivoControl.ShowDialog();            

        }      

        /***********************************************************************************************************/
        // FUNCION:                  Compara_archivo()
        // PARAMETROS ENTRADA:       
        // PARAMETROS SALIDA:        bool
        //					
        // RESULTADO:	              
        //               
        // CREACION:                  10/2009
        // DESCRIPCION:               compara el archivo  regresa true si  la comparacion es exitosa
        //                           
        //                             
        // DESARROLLADO POR:          AAHB (Infoware )
        // ULTIMA MODIFICACION:
        // DESCRIPCION:
        //
        /**********************************************************************************************************/
        private bool Compara_archivo()
        {
  
            string stlinea = null;
            int iLineaVacia = 0, iNumLineas = 0;
            int resl = new int(); ;
            string stRes = null;

            string stPromR1 = null;
            string stCaptR1 = null;

            bool band00 = false;//, band99 = false;
            if (txbArchControl.Text == null)
                return  false;
            if (cmbPromotora.Text == "" || cmbEmpCaptura.Text == "")
            {
                MessageBox.Show("SELECCIONAR EMPRESA PROMOTORA / CAPTURA", "C753 - Arribo Remesas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }            

            LsRemesas.Clear();
            LsContErr.Clear();
            LsRemComp.Clear();
            ListaOrg.Clear();

            // Regresa el Numero de Registro que falta, "**" si esta completo
            string stRegistro = revisaRegistros(txbArchControl.Text);

            if (stRegistro == null)
                return false;
            //if (!archivoControlCorrecto())
            //    return false;

            StreamReader rd = new StreamReader(txbArchControl.Text);
            stlinea = rd.ReadLine();
            int iNumRemesas = new int();
            string stArchivo = txbArchControl.Text.Substring(txbArchControl.Text.LastIndexOf("\\") + 1);

            if (stRegistro == "**")//Si Existen Todos los Registros
            {                                
                while ((stlinea != null))
                {
                    iNumLineas++;
                    if (stlinea != "")
                    {
                        switch (stlinea.Substring(0, 2))
                        {
                            case "00":                                                                
                                band00 = true;
                                
                                stPromR1 = cmbPromotora.SelectedItem.ToString().Substring(0, 2);                                
                                //VERIFICA EMPRESA PROMOTORA DE LA PANTALLA VS ARCHIVO CONTROL
                                if (stlinea.Substring(2, 2) != stPromR1)
                                {
                                    MessageBox.Show("EMPRESA PROMOTORA NO CORRESPONDE A LA EMPRESA SELECCIONADA\n"
                                    + "VERIFICAR ARCHIVO DE CONTROL", "C753 - Arribo Remesas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    rd.Close();
                                    return false;
                                }
                                
                                stCaptR1 = cmbEmpCaptura.SelectedItem.ToString().Substring(0, 2);                                
                                //VERIFICA EMPRESA CAPTURA DE LA PANTALLA VS ARCHIVO CONTROL
                                if (stlinea.Substring(4, 2) != stCaptR1)
                                {
                                    MessageBox.Show("EMPRESA CAPTURA NO CORRESPONDE A LA EMPRESA SELECCIONADA\n"
                                    + "VERIFICAR ARCHIVO DE CONTROL", "C753 - Arribo Remesas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    rd.Close();
                                    return false;
                                }

                                break;
                            case "01":
                                if (band00 == true)
                                {
                                    if (stlinea.Length >= 105)
                                    {
                                        if (stlinea.Substring(48, 4) == "0000")
                                        {
                                            MessageBox.Show("CANTIDAD DE SOLICITUDES EN CERO", "C753 - Arribo Remesas",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            rd.Close();
                                            return false;
                                        }
                                        // Verifica Tramite Del Archivo con el del Final del Registro
                                        if (stlinea.Substring(56, 2) != stlinea.Substring(99, 2))
                                        {
                                            MessageBox.Show("CLAVE DE TRAMITE NO COINCIDE, VERIFICAR ARCHIVO DE CONTROL", "C753 - Arribo Remesas",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            rd.Close();
                                            return false;
                                        }
                                        // Verifica Familia Del Archivo con el del Final del Registro
                                        if (stlinea.Substring(58, 2) != stlinea.Substring(103, 2))
                                        {
                                            MessageBox.Show("CLAVE DE FAMILIA-PRODUCTO NO COINCIDE, VERIFICAR ARCHIVO DE CONTROL", "C753 - Arribo Remesas",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            rd.Close();
                                            return false;
                                        }
                                        // Verifica Empresa Promotora: NombreArchivo, Final Linea, Header
                                        if ((stlinea.Substring(95, 2) != stlinea.Substring(52, 2)) || (stlinea.Substring(95, 2) != stPromR1) || (stlinea.Substring(52, 2) != stPromR1))
                                        {
                                            MessageBox.Show("EMPRESA PROMOTORA NO COINCIDE, VERIFICAR ARCHIVO DE CONTROL", "C753 - Arribo Remesas",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            rd.Close();
                                            return false;
                                        }
                                        // Verifica Empresa Captrua: NombreArchivo, Final Linea, Header
                                        if ((stlinea.Substring(97, 2) != stlinea.Substring(54, 2)) || (stlinea.Substring(97, 2) != stCaptR1) || (stlinea.Substring(54, 2) != stCaptR1))
                                        {
                                            MessageBox.Show("EMPRESA CAPTURA NO COINCIDE, VERIFICAR ARCHIVO DE CONTROL", "C753 - Arribo Remesas",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            rd.Close();
                                            return false;
                                        }
                                        // MODIF MAP 11/08/2010
                                        // Verifica Que El Numero Consecutivo de la Remesa no sobrepase un Consecutivo 35
                                        // 0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ
                                        if (int.Parse(stlinea.Substring(68, 2)) > 35)
                                        {
                                            MessageBox.Show("EL NUMERO CONSECUTIVO DEL ARCHIVO NO ES VALIDO, MAX. 35 CONSECUTIVOS", "C753 - Arribo Remesas",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            rd.Close();
                                            return false;

                                        }
                                        
                                        LsRemesas.Add(stlinea);
                                        stTramite = stlinea.Substring(99, 2);
                                        stFamilia = stlinea.Substring(103, 2);
                                    }
                                    else
                                    {
                                        MessageBox.Show("ERROR EN TAMAÑO DEL REGISRO 01", "C753 - Arribo Remesas",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        rd.Close();
                                        return false;
                                    }

                                }
                                break;
                            case "99":
                                //band99 = true;
                                if (stlinea.Substring(0, 2) == "99")
                                    iNumRemesas = int.Parse(stlinea.Substring(2, 5));
                                break;
                            case "  ":
                                iLineaVacia++;
                                break;
                            default:
                                MessageBox.Show("REGISTRO \"" + stlinea.Substring(0, 2) + "\" INVALIDO EN ARCHIVO DE CONTROL", "C753 - Arribo Remesas",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                rd.Close();
                                return false;
                        }
                    }
                    else
                        iLineaVacia++;


                    stlinea = rd.ReadLine();
                }

            }
            else
            {
                MessageBox.Show("NO EXISTE REGISTRO" + stRegistro + "EN ARCHIVO DE CONTROL", "C753 - Arribo Remesas",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                rd.Close();
                return false;
            }

            /*
            if (band00 == false)
            {
                MessageBox.Show("NO EXISTE REGISTRO 00 EN ARCHIVO DE CONTROL", "C753 - Arribo Remesas",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                rd.Close();
                return false;
            }
            if (band99 == false)
            {
                MessageBox.Show("NO EXISTE REGISTRO 99 EN ARCHIVO DE CONTROL", "C753 - Arribo Remesas",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                rd.Close();
                return false;
            }*/
            if (iLineaVacia == iNumLineas)
            {
                MessageBox.Show("ARCHIVO DE CONTROL VACIO", "C753 - Arribo Remesas",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                rd.Close();
                return false;
            }
            rd.Close();

            if (LsRemesas.Count == 0)
            { // Archivo  sin remesas                 
                LsRemComp.Add(new Linea("NO SE ENCONTRO ARCHIVOs DE REMESA " + stArchivo , false,false,false, 1));
                MessageBox.Show("NO SE ENCONTRARON REMESA(S) EN ARCHIVO DE CONTROL  \n" +
                                "VERIFICAR ARCHIVO DE CONTROL", "C753 - Arribo Remesas", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; 
            }
            if (iNumRemesas != LsRemesas.Count)
            {
                //no correspone numero  de remesas, registros 01, contra ultimo campo del registro 99
                LsRemComp.Add(new Linea("FOLIOS NO CORRESPONDEN CTRL VS REMESA" + stArchivo, false, false, false, 1 ));
                MessageBox.Show("FOLIOS NO CORRESPONDE CTRL VS REMESA  \n"+
                                "VERIFICAR ARCHIVO DE CONTROL", "C753 - Arribo Remesas", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;           
        }

        
        private string revisaRegistros(string stArchivoControl)
        {
            StreamReader rd = new StreamReader(stArchivoControl);
            string stlineaR = rd.ReadLine();
            bool bo00=false, bo01= false, bo99 = false;
            
            while (stlineaR != null)
            {
                if (stlineaR.Trim() != "" && stlineaR.Length > 1)
                {
                    switch (stlineaR.Substring(0, 2))
                    {
                        case "00":
                            if (stlineaR.Length != 14)
                            {
                                MessageBox.Show("Archivo de Control Incorrecto Registro 00", "C753 - Arribo Remesas",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return null;
                            }
                            bo00 = true; 
                            break;
                        case "01":
                            if (stlineaR.Length != 105)
                            {
                                MessageBox.Show("Archivo de Control Incorrecto Registro 01", "C753 - Arribo Remesas",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return null;
                            }
                            bo01 = true; 
                            break;
                        case "99":
                            if (stlineaR.Length != 7)
                            {
                                MessageBox.Show("Archivo de Control Incorrecto Registro 99", "C753 - Arribo Remesas",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return null;
                            }
                            bo99 = true; 
                            break;
                        default: break;
                    }
                }
                stlineaR = rd.ReadLine();
            }
            rd.Close();

            if (!bo00)
                return "00";
            if (!bo01)
                return "01";
            if (!bo99)
                return "99";
            
            //if (bo00)
            //{
            //    if (bo01)
            //    {
            //        if (!bo99)
            //            return "99";
            //    }
            //    else
            //        return "01";
            //}
            //else
            //    return "00";

            return "**";

        }


        /***********************************************************************************************************/
        // FUNCION:                  validaRemesas()
        // PARAMETROS ENTRADA:       
        // PARAMETROS SALIDA:        bool
        //					
        // RESULTADO:	              
        //               
        // CREACION:                  10/2009
        // DESCRIPCION:               compara el archivo  regresa true si  la comparacion es exitosa
        //                           
        //                             
        // DESARROLLADO POR:          AAHB (Infoware )
        // ULTIMA MODIFICACION:
        // DESCRIPCION:
        //
        /**********************************************************************************************************/
        private bool validaRemesas()
        {
            bool bExiste = true;
            bool bPromotora = true;
            bool bCaptura = true;
            int iContErr = 0;
            int iCont = 0;
            bool bBand = true;
            string stPat = "";


            while (iCont < LsRemesas.Count)
            {
                // VERIFICA SI EXISTE FISICAMENTE ARCHIVO DE REMESA
                stPat = txbArchControl.Text.Substring(0, txbArchControl.Text.LastIndexOf("\\"));

                if (stPat != stRutaRemesas && stPat + "\\" != stRutaRemesas)
                {
                    MessageBox.Show("LA RUTA PARA LOS ARCHIVOS DE REMESAS ES " + stRutaRemesas, "C753 - Arribo Remesas",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                // Verificancia de la existencia de los archivos de remesa
                if (!File.Exists(stPat + "\\" + LsRemesas[iCont].Substring(52, 18) + ".txt"))
                {
                    bExiste = false;
                    bBand = false;
                    iContErr++;
                }
                else
                {
                    //Se Mapea el Archivo de remesa quitandole los acentos y caracteres raros
                    ModificacionArchivoRemesa(stPat + "\\" + LsRemesas[iCont].Substring(52, 18) + ".txt");
                }

                // VERIFICA EN LA REMESA LA EMPRESA PROMOTORA VS COMBO DE PANTALLA
                string stVerfica = cmbPromotora.SelectedItem.ToString().Substring(0, 2);
                //if (int.Parse(LsRemesas[iCont].Substring(95, 2)) != stVerfica)
                if (LsRemesas[iCont].Substring(95, 2) != stVerfica || LsRemesas[iCont].Substring(52, 2) != stVerfica)
                {
                    bPromotora = false;
                    iContErr++;
                    bBand = false;
                }
                // VERIFICA EN LA REMESA LA EMPRESA CAPTURA VS COMBO DE PANTALLA
                stVerfica = cmbEmpCaptura.SelectedItem.ToString().Substring(0, 2);
                if (LsRemesas[iCont].Substring(97, 2) != stVerfica || LsRemesas[iCont].Substring(54, 2) != stVerfica)
                {
                    bCaptura = false;
                    iContErr++;
                    bBand = false;
                }

                // GUARDA EN LISTA LA REMESA, EXISTENCIA DEL ARCHIVO, LA CORRESPONDENCIA DE EMPRESA PROMOTORA
                // Y DE CAPTURA, Y EL NUMERO DE ERRORES DE LA MISMA
                LsRemComp.Add(new Linea(LsRemesas[iCont], bExiste, bPromotora, bCaptura, iContErr));
                iCont++;
                iContErr = 0;
                bCaptura = bPromotora = bExiste = true;
            }
            return (bBand);
        }

        private void ModificacionArchivoRemesa(string stArchivoRemesa)
        {
            string stArchivoTemp = stRutaRemesas + "temp" + ".txt";
            string stArchivoBackup = stRutaRemesas + "temp2" + ".txt";
            StreamReader sR = new StreamReader(stArchivoRemesa, System.Text.ASCIIEncoding.Default, true);
            StreamWriter sW = new StreamWriter(stArchivoTemp);
            string stLineaArchivo;

            stLineaArchivo = sR.ReadLine();
            while (stLineaArchivo != null)
            {
                stLineaArchivo = QuitaAcentos(stLineaArchivo);
                sW.WriteLine(stLineaArchivo);
                stLineaArchivo = sR.ReadLine();
            }
            sR.Close();
            sW.Close();

            File.Replace(stArchivoTemp, stArchivoRemesa, stArchivoBackup);
            File.Delete(stArchivoBackup);
        }

    /***********************************************************************************************************/
    // FUNCION:                  CargaCombos()
    // PARAMETROS ENTRADA:       
    // PARAMETROS SALIDA:        bool
    //					
    // RESULTADO:	              
    //               
    // CREACION:                  10/2009
    // DESCRIPCION:               Carga combos en la pantalla
    //                           
    //                             
    // DESARROLLADO POR:          AAHB (Infoware )
    // ULTIMA MODIFICACION:
    // DESCRIPCION:
    //
    /**********************************************************************************************************/
        private bool CargaCombos()
        {
            
            string tempRefParam1, tempRefParam2, tempRefParam3, tempRefParam4, tempRefParam5, tempRefParam6;
            
            //Llena Combo Empresa Promotora
            cmbPromotora.Items.Clear();
            mdlComunica.OleCatalogos.setLongClave = 2;
            Catalogos.clsCatalogos.enmAlineaciones tempRefParamCatenm = Catalogos.clsCatalogos.enmAlineaciones.Izquierda;            
            mdlComunica.OleCatalogos.setAlineacionClave = tempRefParamCatenm;
            tempRefParam1 = "213"; //EMPRESA PROMOTORA                 
            tempRefParam2 = String.Empty;
            tempRefParam3 = String.Empty;
            tempRefParam4 = String.Empty;
            tempRefParam5 = String.Empty;
            //tempRefParam6 = "D";
            tempRefParam6 = "E";
            mdlComunica.OleCatalogos.LlenaCombo(ref cmbPromotora, ref tempRefParam1, ref tempRefParam2, ref tempRefParam3, ref tempRefParam4, ref tempRefParam5, ref tempRefParam6);
            mdlComunica.OleCatalogos.setLongClave = 2;
            //acomoda_combo(cmbPromotora);

            
            //Llena Combo Empresa Captura            
            cmbEmpCaptura.Items.Clear();
            //mdlComunica.OleCatalogos.setLongClave = 2;
            tempRefParam1 = "255"; //EMPRESA CAPTURA            
            tempRefParam2 = String.Empty;
            tempRefParam3 = String.Empty;
            tempRefParam4 = String.Empty;
            tempRefParam5 = String.Empty;
            tempRefParam6 = "D";
            mdlComunica.OleCatalogos.LlenaCombo(ref cmbEmpCaptura, ref tempRefParam1, ref tempRefParam2, ref tempRefParam3, ref tempRefParam4, ref tempRefParam5, ref tempRefParam6);
            //mdlComunica.OleCatalogos.setLongClave = 2;
            //acomoda_combo(cmbEmpCaptura);

            //Llena Combo Tipo Entidad Origen
            cmbTipoEntidad.Items.Clear();
            //mdlComunica.OleCatalogos.setLongClave = 2;
            tempRefParam1 = "66";
            tempRefParam2 = String.Empty;
            tempRefParam3 = String.Empty;
            tempRefParam4 = String.Empty;
            tempRefParam5 = String.Empty;
            tempRefParam6 = "E";
            mdlComunica.OleCatalogos.LlenaCombo(ref cmbTipoEntidad, ref tempRefParam1, ref tempRefParam2, ref tempRefParam3, ref tempRefParam4, ref tempRefParam5, ref tempRefParam6);                        
            //acomoda_combo(cmbTipoEntidad);
            //mdlComunica.OleCatalogos.setLongClave = 2;

            ////Llena Combo Entidad Origen
            //cmbEntidad.Items.Clear();
            ////mdlComunica.OleCatalogos.setLongClave = 2;
            ////mdlComunica.OleCatalogos.setLongClave = 4;
            //tempRefParam1 = "13";
            //tempRefParam2 = "2";
            //tempRefParam3 = "1";
            //tempRefParam4 = String.Empty;
            //tempRefParam5 = String.Empty;
            //tempRefParam6 = "E";
            //mdlComunica.OleCatalogos.LlenaCombo(ref cmbEntidad, ref tempRefParam1, ref tempRefParam2, ref tempRefParam3, ref tempRefParam4, ref tempRefParam5, ref tempRefParam6);            
            ////mdlComunica.OleCatalogos.setLongClave = 2;
            //acomoda_combo(cmbEntidad);
            ////mdlComunica.OleCatalogos.setLongClave = 2;
            
            //Llena Combo Promoción
            cmbPromocion.Items.Clear();
            //mdlComunica.OleCatalogos.setLongClave = 2;
            tempRefParam1 = "64";
            tempRefParam2 = String.Empty;
            tempRefParam3 = String.Empty;
            tempRefParam4 = String.Empty;
            tempRefParam5 = String.Empty;
            tempRefParam6 = "D";
            mdlComunica.OleCatalogos.LlenaCombo(ref cmbPromocion, ref tempRefParam1, ref tempRefParam2, ref tempRefParam3, ref tempRefParam4, ref tempRefParam5, ref tempRefParam6);
            //acomoda_combo(cmbPromocion);
            //mdlComunica.OleCatalogos.setLongClave = 2;
                        
            return true;
        }

        private void cargaComboEntidadOrigen(ComboBox cmbEntidad)
        {
            
            string tempRefParam1, tempRefParam2, tempRefParam3, tempRefParam4, tempRefParam5, tempRefParam6;
            //int iLlaveTipoEntidad = cmbTipoEntidad.SelectedIndex + 1;
            string stLlaveTipoEntidad = cmbTipoEntidad.SelectedItem.ToString();
             //Llena Combo Entidad Origen
            cmbEntidad.Items.Clear();
            
                //mdlComunica.OleCatalogos.setLongClave = 2;
                mdlComunica.OleCatalogos.setLongClave = 4;
                tempRefParam1 = "13";
                tempRefParam2 = stLlaveTipoEntidad.Substring(0, 2);
                tempRefParam3 = "1";
                tempRefParam4 = String.Empty;
                tempRefParam5 = String.Empty;
                tempRefParam6 = "E";
                mdlComunica.OleCatalogos.LlenaCombo(ref cmbEntidad, ref tempRefParam1, ref tempRefParam2, ref tempRefParam3, ref tempRefParam4, ref tempRefParam5, ref tempRefParam6);
                //mdlComunica.OleCatalogos.setLongClave = 2;                
                mdlComunica.OleCatalogos.setLongClave = 2;
            
            
        }
                
        /***********************************************************************************************************/
        // FUNCION:                  opnfdArchivoControl_FileOk()
        // PARAMETROS ENTRADA:       
        // PARAMETROS SALIDA:        
        //					
        // RESULTADO:	              
        //               
        // CREACION:                  10/2009
        // DESCRIPCION:               
        //                           
        //                             
        // DESARROLLADO POR:          AAHB (Infoware )
        // ULTIMA MODIFICACION:
        // DESCRIPCION:
        //
        /**********************************************************************************************************/
        private void opnfdArchivoControl_FileOk(object sender, CancelEventArgs e)
        {            
            txbArchControl.Text = opnfdArchivoControl.FileName.Trim();            
            btnRegistrar.Enabled = true;            
        }
        /***********************************************************************************************************/
        // FUNCION:                  btnRegistrar_Click()
        // PARAMETROS ENTRADA:       object sender, EventArgs e
        // PARAMETROS SALIDA:        
        //					
        // RESULTADO:	              
        //               
        // CREACION:                  10/2009
        // DESCRIPCION:               Lleva a cabo el proceso  de registro 
        //                           
        //                             
        // DESARROLLADO POR:          AAHB (Infoware )
        // ULTIMA MODIFICACION:
        // DESCRIPCION:
        //
        /**********************************************************************************************************/
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (validarPantalla())
            {
                if (txbArchControl.Text != null)
                {
                    if (Compara_archivo())
                    {
                        //leePaquete();
                        if (validaRemesas()) // VALIDA E. CAPTURA, PROMOTORA, EXISTE ARCHIVO y GUARDA LISTA
                        {
                            //llenaNuevosDatos();
                            //Registra_Arribo();

                            //Muestra_error();
                        }
                        llenaNuevosDatos();
                        if (Registra_Arribo())
                        {
   
                        }
                        Muestra_error();
                        //else
                        //Muestra_error();
                    }
                    
                }
                else
                {
                    MessageBox.Show("FAVOR DE SELECCIONAR ARCHIVO DE CONTROL", "C753 - Arribo Remesas", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            this.Cursor = Cursors.Default;
        }

        //private void leePaquete()
        //{            
        //    stTipoSolicitud = "02";
        //    mdlGlobales.gstrTipoEntOrig.Value = cmbTipoEntidad.Text.Substring(0, cmbTipoEntidad.Text.IndexOf(' '));
        //    mdlGlobales.gstrTipoEntOrig.Value = mdlGlobales.gstrTipoEntOrig.Value.Trim();
        //    //mdlGlobales.gstrTipoEntOrig.Value = "0" +(cmbTipoEntidad.SelectedIndex + 1).ToString();
        //    //mdlGlobales.gstrTipoEntOrig.Value = mdlGlobales.gstrTipoEntOrig.Value.Trim();
        //    mdlTranMasivo.funLeePaquete(stTramite, stFamilia, stTipoSolicitud);
        //}

        /***********************************************************************************************************/
        // FUNCION:                  Registra_Arribo()
        // PARAMETROS ENTRADA:       
        // PARAMETROS SALIDA:        
        //					
        // RESULTADO:	              
        //               
        // CREACION:                  10/2009
        // DESCRIPCION:               registra el arribo  
        //                           
        //                             
        // DESARROLLADO POR:          AAHB (Infoware )
        // ULTIMA MODIFICACION:
        // DESCRIPCION:
        //
        /**********************************************************************************************************/
        private bool Registra_Arribo()
        {
            // REGISTRA EL ARRIBO DE LA REMESA
            clsWRemesas clsArr = new clsWRemesas();

            if (clsArr.GuardaRemesas5562_60(ref LsRemComp, stDatosNuevos, ref ListaOrg) != null)                            
                return true;
            
            return false; 
        }
        /***********************************************************************************************************/
        // FUNCION:                  Muestra_error()
        // PARAMETROS ENTRADA:       
        // PARAMETROS SALIDA:        bool
        //					
        // RESULTADO:	              
        //               
        // CREACION:                  10/2009
        // DESCRIPCION:               Carga combos en la pantalla
        //                           
        //                             
        // DESARROLLADO POR:          AAHB (Infoware )
        // ULTIMA MODIFICACION:
        // DESCRIPCION:
        //
        /**********************************************************************************************************/        
        private void Muestra_error()
        {
            //frmErroresArribo errores = new frmErroresArribo(LsRemComp);
            frmErroresArribo errores = new frmErroresArribo(ListaOrg);

            errores.MdiParent = Masivos.MDIMasivos.DefInstance;
            errores.Show();
            //errores.ShowDialog();
        }

        /***********************************************************************************************************/
        // FUNCION:                  validarPantalla()
        // PARAMETROS ENTRADA:       
        // PARAMETROS SALIDA:        bool
        //					
        // RESULTADO:	              
        //               
        // CREACION:                  01/2010
        // DESCRIPCION:               Control y Validaciones del Llenado de Pantalla
        //                           
        //                             
        // DESARROLLADO POR:          MAP (Infoware )
        // ULTIMA MODIFICACION:
        // DESCRIPCION:
        //
        /**********************************************************************************************************/
        private bool validarPantalla()
        {
            if (cmbPromotora.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccionar Empresa Promotora", "Error - Arribo Remesas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbPromotora.Focus();
                return false;
            }
            if (cmbEmpCaptura.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccionar Empresa de Captura", "Error - Arribo Remesas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbEmpCaptura.Focus();
                return false;
            }
            if (cmbTipoEntidad.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccionar Tipo de Entidad Origen", "Error - Arribo Remesas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbTipoEntidad.Focus();
                return false;
            }
            if (cmbEntidad.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccionar Entidad Origen", "Error - Arribo Remesas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbEntidad.Focus();
                return false;
            }
            if (cmbPromocion.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccionar Promoción", "Error - Arribo Remesas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbPromocion.Focus();
                return false;
            }
            /******************** Validación de Fechas ********************/
            if (dtpFechaIngreso.Value.Date > dtpFechaProceso.Value.Date)
            {
                MessageBox.Show("La Fecha de Ingreso del Credito No Puede Ser Mayor a la Fecha de Proceso", 
                    "Error - Arribo Remesas", MessageBoxButtons.OK, MessageBoxIcon.Error);                
                return false;                
            }
            if ((dtpFechaProceso.Value - dtpFechaIngreso.Value).TotalDays > 15)
            {
                MessageBox.Show("La Fecha de Ingreso del Credito No Puede Tener Más de 15 Días Naturales\r" +
                    "de Diferencia con la Fecha de Proceso", "Error - Arribo Remesas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;                
            }

            if (dtpFechaAceptacion.Value.Date > dtpFechaProceso.Value.Date)
            {
                MessageBox.Show("La Fecha de Aceptación de Credito No Puede Ser Mayor a la Fecha de Proceso",
                    "Error - Arribo Remesas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpFechaAceptacion.Focus();
                return false;                
            }
            if (dtpFechaAceptacion.Value.Date < dtpFechaIngreso.Value.Date)
            {
                MessageBox.Show("La Fecha de Aceptación de Credito No Puede Ser Menor a la Fecha de Ingreso del Credito",
                    "Error - Arribo Remesas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;                
            }
            return true;
        }

        private void dtpFechaProceso_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFechaProceso.Value > DateTime.Now)
                dtpFechaProceso.Value = DateTime.Now;
        }
        private void dtpFechaIngreso_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFechaIngreso.Value > DateTime.Now)
                dtpFechaIngreso.Value = DateTime.Now;
        }
        private void dtpFechaAceptacion_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFechaAceptacion.Value > DateTime.Now)
                dtpFechaAceptacion.Value = DateTime.Now;
        }

        private void llenaNuevosDatos()
        {
            string stTipoEntidadOrigen, stEntidadOrigen, stPromocion, stFechaProceso, stFechaIngresoCredito, stFechaAceptacionCredito;            
            //stTipoEntidadOrigen = (cmbTipoEntidad.SelectedIndex + 1).ToString();
            stTipoEntidadOrigen = cmbTipoEntidad.Text.Substring(0, cmbTipoEntidad.Text.IndexOf(' '));
            //stTipoEntidadOrigen = llena_ceros(stTipoEntidadOrigen, 2);
            //stEntidadOrigen = (cmbEntidad.SelectedIndex + 1).ToString();
            stEntidadOrigen = cmbEntidad.Text.Substring(0, cmbEntidad.Text.IndexOf(' '));
            stEntidadOrigen = llena_ceros(stEntidadOrigen, 4);            

            //stPromocion = (cmbPromocion.SelectedIndex + 1).ToString();
            stPromocion = cmbPromocion.Text.Substring(0, cmbPromocion.Text.IndexOf(' '));
            stPromocion = llena_ceros(stPromocion, 4);

            stFechaProceso = dtpFechaProceso.Value.ToString();
            stFechaProceso = stFechaProceso.Substring(6, 4) + "-" + stFechaProceso.Substring(3, 2) + "-" + stFechaProceso.Substring(0, 2);
            stFechaIngresoCredito = dtpFechaIngreso.Value.ToString();
            stFechaIngresoCredito = stFechaIngresoCredito.Substring(6, 4) + "-" + stFechaIngresoCredito.Substring(3, 2) + "-" + stFechaIngresoCredito.Substring(0, 2);
            stFechaAceptacionCredito = dtpFechaAceptacion.Value.ToString();
            stFechaAceptacionCredito = stFechaAceptacionCredito.Substring(6, 4) + "-" + stFechaAceptacionCredito.Substring(3, 2) + "-" + stFechaAceptacionCredito.Substring(0, 2);

            stDatosNuevos = stTipoEntidadOrigen + stEntidadOrigen + stPromocion + stFechaProceso + stFechaIngresoCredito + stFechaAceptacionCredito;
        }

        private string llena_ceros(string stString, int iLongitud)
        {
            if (stString.Length > iLongitud)            
                stString = stString.Substring(stString.Length - iLongitud);
            

            while (stString.Length < iLongitud)            
                stString = "0" + stString;

            return stString;
        }

        private void cmbTipoEntidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargaComboEntidadOrigen(cmbEntidad);
        }

        private void frmArriboRemesas_FormClosing(object sender, FormClosingEventArgs e)
        {
            //mdlComunica.OleCatalogos.setLongClave = 0;
        }

        private string QuitaAcentos(string stLineaAcentos)
        {
            string stConSignos = "áàäéèëíìïóòöúùuñÁÀÄÉÈËÍÌÏÓÒÖÚÙÜÑçÇ";
            string stSinSignos = "aaaeeeiiiooouuu@AAAEEEIIIOOOUUU@cC";
            for (int iCont = 0; iCont < stSinSignos.Length; iCont++)
            {
                string stTempI = stConSignos.Substring(iCont, 1);
                string stTempJ = stSinSignos.Substring(iCont, 1);
                stLineaAcentos = stLineaAcentos.Replace(stTempI, stTempJ);
            }
            return stLineaAcentos.ToUpper();
        }

 
    }    
}