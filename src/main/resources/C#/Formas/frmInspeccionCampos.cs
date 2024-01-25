using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility.VB6;
using VB6 = Microsoft.VisualBasic.Compatibility.VB6.Support;
using Microsoft.Win32;

namespace Masivos
{
    public partial class frmInspeccionCampos : Form
    {
        
        /********************************/
        string stRemesaRef;
        string stPreimpresoRef;
        string stProcesoStatusRef;
        string stNominaRef;
        DataGridView dataGridFoliosRef = new DataGridView();
        int iDataGridIndexRef;
        Button btnInspeccionFolioRef = new Button();
        /********************************/

        protected static string stMensajeFull; // ALMACENA EL TOTAL DE DATOS POR OCURRENCIAS
        int iClose = 0;
        bool bolExito = false;
        
        DataGridView dataGridMandatorios = new DataGridView();
        ComboBox cBPuente = new ComboBox();

        DataGridView dataGridCambios = new DataGridView();
        List<string> ListaCambios = new List<string>();
        List<string> ListaCampos = new List<string>();
        string stNumCambios = "0000";                
        string stCatalogoMantadatorio = "267";

        private Object newCellValue; //COMBOS

        /*************************/
        int iRegistroDGV = 0;
        int iConsecutivoDGV = 1;
        int iIDCampoDGV = 2;
        int iCampoDGV = 3;
        int iValorDGV = 4;
        int iValorOriginalDGV = 5;
        /*************************/

        public frmInspeccionCampos()
        {
            InitializeComponent();
        }

        public void getSet(string stRemesa, string stPreimpreso, string stProcesoStatus, ref DataGridView dataGridFolios, int iDataGridIndex,
            ref Button btnInspeccionFolio, string stNomina)
        {
            stRemesaRef = stRemesa;
            stPreimpresoRef = stPreimpreso;
            stProcesoStatusRef = stProcesoStatus;
            dataGridFoliosRef = dataGridFolios;
            iDataGridIndexRef = iDataGridIndex;
            btnInspeccionFolioRef = btnInspeccionFolio;
            stNominaRef = stNomina;
        }

        private void frmInspeccionCampos_Load(object sender, EventArgs e)
        {
            stMensajeFull = null;

            tbRemesa.Text = stRemesaRef;
            tbPreimpreso.Text = stPreimpresoRef;
            tbProcesoStatus.Text = stProcesoStatusRef;

            dataGridCampos.Rows.Clear();

            try
            {
                if (cargaCatalogoMandatorios())
                {
                    /* REGISTRO | ID CAMPO | CAMPO | CONSECUTIVO | VALOR */
                    if (pideCampos() == false)
                    {
                        btnGuardar.Enabled = false;
                        iClose = 1;
                    }
                }
            }
            catch
            {
                //MessageBox.Show("Verificar Catalogo \"Mandatorios\"", "C753 - Inspección Campos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }                            
        }
        
        private bool pideCampos()
        {            
            clsWRemesas wInsp = new clsWRemesas();
            string stHeader = null;
            string stMensaje = null;
            string stFlag = "0";
            ListaCampos.Clear();

            do
            {
                stMensaje = wInsp.ConsultaDatosInspeccionar5562_26(stRemesaRef, stNominaRef, stPreimpresoRef, stHeader);
                //PAGINEO
                if (stMensaje == null || stMensaje == "") // MENSAJE CON HEADER Y DATOS
                    return false;
                                               
                stFlag = stMensaje.Substring(112, 1);
                if (stFlag == "1")
                {
                    stHeader = stMensaje.Substring(0, 176);
                }
                
                //GUARDAR MENSAJE QUITANDOLE *** DEL FINAL
                int iPosicionAsterisco = stMensaje.IndexOf("***");
                if (iPosicionAsterisco != -1)
                {
                    if (stMensaje.Substring(176, iPosicionAsterisco - 176).Trim() == "")
                    {
                        MessageBox.Show("RESPUESTA INCOMPLETA", "S753 ARIES - Inspección de Campos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else
                    {
                        ListaCampos.Add(stMensaje.Substring(176, iPosicionAsterisco - 176));
                        stMensajeFull += stMensaje.Substring(176, iPosicionAsterisco - 176);
                    }
                }
                else
                {
                    ListaCampos.Add(stMensaje.Substring(176));
                    stMensajeFull += stMensaje.Substring(176);
                }
                
            } while (stFlag == "1");

            llenaValoresGridCampos();
            return true;
        }

        private void llenaValoresGridCampos2()
        {


        }

        private void llenaValoresGridCampos()
        {
            /* CAMPOS DATAGRIDMANDATORIOS: REGISTRO_ID CAMPO | POS INI  | POS FIN | LONGITUD | CAMPO       | CATALOGO       | TIPO DATO | CONSECUTIVO */
            /*      CAMPOS DATAGRIDCAMPOS: REGISTRO          | ID CAMPO | CAMPO   | VALOR    | CONSECUTIVO | VALOR ORIGINAL */
            string stRegistroCampos, stIdCampos, stCampos, stValor, stRegisID, stLongitud = "0", stConsecutivo;
            string stIDConsecutivo;
            int iFilaCombo = 0;// COMBOS
            string stLineaCombo = null;// COMBOS
            DataGridViewComboBoxCell CellComboType = new DataGridViewComboBoxCell(); // COMBOS
           
            for (int iRows = 0; iRows < dataGridMandatorios.RowCount - 1; iRows++)
            {
                stRegisID = dataGridMandatorios.Rows[iRows].Cells[0].Value.ToString();
                stRegistroCampos = stRegisID.Substring(0, 3);
                stIdCampos = stRegisID.Substring(3);
                stCampos = dataGridMandatorios.Rows[iRows].Cells[4].Value.ToString().Trim();
                                
                stIDConsecutivo = "01";
                if (stCampos.IndexOf("1") > -1)
                    stIDConsecutivo = "01";                
                if (stCampos.IndexOf("2") > -1)
                    stIDConsecutivo = "02";
                if (stCampos.IndexOf("3") > -1)
                    stIDConsecutivo = "03";
                if (stCampos.IndexOf("4") > -1)
                    stIDConsecutivo = "04";

                //stIDConsecutivo = dataGridMandatorios.Rows[iRows].Cells[7].Value.ToString();
                                
                if (stIdCampos != "001" && stIdCampos != "002")
                {                    
                    for (int iCont = 0; iCont < dataGridMandatorios.RowCount - 1; iCont++)
                    {
                        if (dataGridMandatorios.Rows[iCont].Cells[0].Value.ToString() == stRegisID)
                        {
                            stLongitud = dataGridMandatorios.Rows[iCont].Cells[3].Value.ToString();                            
                            break;
                        }                        
                    }
                    //MODIF MAP 2010/08/13
                    //stRegisID = stRegisID.Substring(1, 2) + "0" + stRegisID.Substring(3, 3);                    
                    //stRegisID = stRegisID.Substring(1, 2) + stIDConsecutivo + "0" + stRegisID.Substring(3, 3);
                    stRegisID = stRegisID.Substring(1, 2) + stIDConsecutivo + "0" + stRegisID.Substring(3, 3);
                    /*
                    //Llena Linea del Grid con campos textbox0
                    //stValor = regresaValorMensajeFull(stRegisID, int.Parse(stLongitud));                    
                    ****************************
                    //stValor = stValor.Trim(); //Sin Espacios
                    //stValor = stValor.ToUpper(); // Todo A Mayusculas
                    ****************************
                    //dataGridCampos.Rows.Add(stRegistroCampos, stIdCampos, stCampos, stValor, stValor);                    
                    */

                    // Llena Linea del Grid con Combos INICIO                    
                    string stCatalogoTemp = dataGridMandatorios.Rows[iRows].Cells[5].Value.ToString();
                    //stCatalogoTemp = "0" + stCatalogoTemp;      

                    //if (stRegisID == "12010008")
                    //    stRegisID = stRegisID;
                    stValor = regresaValorMensajeFull(stRegisID, int.Parse(stLongitud), stCatalogoTemp);
                    stValor = stValor.Trim();
                    //dataGridCampos.Rows.Add(stRegistroCampos, stIdCampos, stCampos.ToUpper(), stIDConsecutivo);
                    dataGridCampos.Rows.Add(stRegistroCampos, stIDConsecutivo, stIdCampos, stCampos.ToUpper());
                    //dataGridCampos.Rows[iFilaCombo].Cells[4].Value = stValor;
                    //dataGridCampos.Rows[iFilaCombo].Cells[5].Value = stValor;
                    dataGridCampos.Rows[iFilaCombo].Cells[iValorOriginalDGV].Value = stValor;
                    // Valor sin Abrir Catalogo
                    if (stCatalogoTemp == "000")
                    {
                        //stValor = stValor.Trim();
                        //CellComboType = (DataGridViewComboBoxCell)dataGridCampos.Rows[iFilaCombo].Cells[3];
                        //CellComboType = (DataGridViewComboBoxCell)dataGridCampos.Rows[iFilaCombo].Cells[4];
                        CellComboType = (DataGridViewComboBoxCell)dataGridCampos.Rows[iFilaCombo].Cells[iValorDGV];
                        CellComboType.Items.Add(stValor);
                        CellComboType.Value = CellComboType.Items[CellComboType.Items.IndexOf(stValor)];
                    }
                    else
                    {
                        LlenaComboCatalogos(cBPuente, stCatalogoTemp);

                        //CellComboType = (DataGridViewComboBoxCell)dataGridCampos.Rows[iFilaCombo].Cells[3];
                        //CellComboType = (DataGridViewComboBoxCell)dataGridCampos.Rows[iFilaCombo].Cells[4];
                        CellComboType = (DataGridViewComboBoxCell)dataGridCampos.Rows[iFilaCombo].Cells[iValorDGV];

                        //AQUI PUEDO AGREGAR UNA LINEA MAS AL COMBO
                        CellComboType.Items.Add("");

                        for (int iLineaCombo = 0; iLineaCombo < cBPuente.Items.Count; iLineaCombo++)
                        {
                            stLineaCombo = cBPuente.Items[iLineaCombo].ToString();
                            CellComboType.Items.Add(stLineaCombo);                                                      
                        }
                        if (stValor.Substring(0, 1) != " ")
                        {
                            //CellComboType.Value = CellComboType.Items[CellComboType.Items.IndexOf(stValor)];

                            if (char.IsNumber(stValor[0]))
                            {
                                //int iCboIndex = int.Parse(stValor) - 1;
                                int iCboIndex = int.Parse(stValor);
                                CellComboType.Value = CellComboType.Items[iCboIndex];
                            }
                            if (char.IsLetter(stValor, 0))
                            {
                                string stLineaCombo2 = null;
                                for (int iCelCombo = 0; iCelCombo < CellComboType.Items.Count; iCelCombo++)
                                {
                                    stLineaCombo2 = CellComboType.Items[iCelCombo].ToString();
                                    if (stLineaCombo2.IndexOf(stValor[0]) > -1)
                                    {
                                        CellComboType.Value = CellComboType.Items[iCelCombo];
                                        break;
                                    }
                                }
                            }
                            
                        }                        
                    }
                    iFilaCombo++;
                    // Llena Linea del Grid con Combos FIN                    
                }
            }            
        }

        
        
        private string regresaValorMensajeFull(string stRegisID, int iLongitud, string stCatalogo)
        {
            string stLineaMensaje = null;
            string stValorTemp = " ";
            int istartIndex;
            for (int iLineaMensaje = 0; iLineaMensaje < ListaCampos.Count; iLineaMensaje++)
            {
                stLineaMensaje = ListaCampos[iLineaMensaje];
                istartIndex = stLineaMensaje.IndexOf(stRegisID);
                if (istartIndex != -1)
                {
                    //stValorTemp = stLineaMensaje.Substring(istartIndex + 6, iLongitud);
                    stValorTemp = stLineaMensaje.Substring(istartIndex + 8, iLongitud);
                    return stValorTemp;
                }

            }

            if (stCatalogo != "000" && stValorTemp == " ")
                stValorTemp = "0".PadLeft(iLongitud,'0');                

            return stValorTemp;

            /*string stValorTemp = " ";
            int istartIndex = stMensajeFull.IndexOf(stRegisID);
                      
            if(istartIndex == -1)
                return stValorTemp;
            stValorTemp = stMensajeFull.Substring(istartIndex + 6, iLongitud);
            return stValorTemp;*/
        }

        private bool cargaCatalogoMandatorios()
        {            
            /* CAMPOS DATAGRIDMANDATORIOS: REGISTRO_ID CAMPO | POS INI | POS FIN | LONGITUD | CAMPO | CATALOGO | TPO DATO*/
            /*      CAMPOS DATAGRIDCAMPOS: REGISTRO          | ID CAMPO | CAMPO | VALOR | VALOR ORIGINAL*/
            
            dataGridMandatorios.Columns.Add("colRegistroID", "Registro_IDCampo");
            dataGridMandatorios.Columns.Add("colPosIni", "Posicion Inicial");
            dataGridMandatorios.Columns.Add("colPosFin", "Posicion Inicial");
            dataGridMandatorios.Columns.Add("colLongitud", "Longitud");
            dataGridMandatorios.Columns.Add("colNombre", "Nombre");
            dataGridMandatorios.Columns.Add("colCatalogo", "Catalogo");
            dataGridMandatorios.Columns.Add("colTipoDato", "Tipo Dato");
            dataGridMandatorios.Columns.Add("colConsFormato", "Consecutivo Formato");

            LlenaComboCatalogos(cBPuente, stCatalogoMantadatorio); //HABILITAR
            //LlenaComboCatalogoMandatorio(ref cBPuente, stCatalogoMantadatorio, "", "", "", "", "", "", true);

            string stRegistro, stMandatorio, stNumCampo, stPosIni, stPosFin, stNombreCampo, stCatalogo = " ", stTipoDato, stConsFormato = "01";
            string stLineaCombo;
            int iLongitud;
            dataGridMandatorios.DataSource = null;
            dataGridMandatorios.Rows.Clear();
            if (cBPuente.Items.Count == 0)
            {
                MessageBox.Show("Error al Cargar Catalogo Mandatorios", "C753 - Inspección Campos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnGuardar.Enabled = false;
                iClose = 1;
                return false;
            }
            for (int iCont = 0; iCont < cBPuente.Items.Count; iCont++)
            {
                stLineaCombo = cBPuente.Items[iCont].ToString();
                
                stMandatorio = stLineaCombo.Substring(68, 1);
                //if (stMandatorio != "3") //BORRAR ESTA LINEA SE CARGAN TODOS LOS DATOS
                //if (stMandatorio == "1") 
                if(stMandatorio == "2") // SOLO ALGUNOS DATOS PARA INSPECCION
                {                                        
                    stNombreCampo = stLineaCombo.Substring(0, 50);
                    stPosIni = stLineaCombo.Substring(50, 3);
                    stPosFin = stLineaCombo.Substring(53, 3);
                    stTipoDato = stLineaCombo.Substring(56, 1);
                    //IND NULL = stLineaCombo.Substring(57, 1);
                    stCatalogo = stLineaCombo.Substring(58, 3);
                    //TRAMITE = stLineaCombo.Substring(61, 2);
                    //FAMILIA = stLineaCombo.Substring(63, 2);
                    stRegistro = stLineaCombo.Substring(65, 3);
                    //stMandatorio = stLineaCombo.Substring(68, 1);
                    stNumCampo = stLineaCombo.Substring(69, 3);
                    //MODIF MAP 10/08/2010 
                    stConsFormato = stLineaCombo.Substring(72, 2);

                    
                    
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
                 * 72,2 CONSECUTIVO DE FORMATO
                 * */

                    stNombreCampo = stNombreCampo.Trim();

                    iLongitud = (int.Parse(stPosFin) - int.Parse(stPosIni)) + 1;

                    //dataGridMandatorios.Rows.Add(stRegistro + stNumCampo, stPosIni, stPosFin, iLongitud.ToString(),
                    //    stNombreCampo, stCatalogo, stTipoDato);
                    dataGridMandatorios.Rows.Add(stRegistro + stNumCampo, stPosIni, stPosFin, iLongitud.ToString(),
                        stNombreCampo, stCatalogo, stTipoDato, stConsFormato);
                }                                
            }
            return true;
        }

                

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                buscarCambios();
                if (envioTransaccion())
                {
                    iClose = 1;
                    MessageBox.Show("REALIZADO CON EXITO", "C753 ARIES - Inspección de Campos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "C753 ARIES - Inspección de Campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void buscarCambios()
        {
            int iNumCambios = 0;
            string stCambioTemp, stTamañoString = "0", stRegistroIDcambios, stTipoFormato;

            string stValorCambio, stValorOriginal;
            string stValorOriginalCombo = null;
            string stConsecutivo;
            ComboBox cbCambiosTemp = new ComboBox();

            dataGridCambios.Rows.Clear();

            dataGridCambios.Columns.Add("colTipoFormato", "Tipo Formato");
            dataGridCambios.Columns.Add("colConsFormato", "Consecutivo Formato");
            dataGridCambios.Columns.Add("colDICampo", "IDCampo");
            dataGridCambios.Columns.Add("colNewValor", "Nuevo Valor");

            // MODIF MAP 2010/08/16
            for (int iRows = 0; iRows < dataGridCampos.Rows.Count; iRows++)
            //for (int iRows = 0; iRows < dataGridCampos.Rows.Count - 1; iRows++)
            {                
                //stValorCambio = dataGridCampos.Rows[iRows].Cells[3].Value.ToString();
                //stValorOriginal = dataGridCampos.Rows[iRows].Cells[4].Value.ToString();
                //stValorCambio = dataGridCampos.Rows[iRows].Cells[4].Value.ToString();
                //stValorOriginal = dataGridCampos.Rows[iRows].Cells[5].Value.ToString();
                stValorCambio = dataGridCampos.Rows[iRows].Cells[iValorDGV].Value.ToString();
                stValorOriginal = dataGridCampos.Rows[iRows].Cells[iValorOriginalDGV].Value.ToString();

                // Valida el campo que sea igual aunque tenga espacios
                stValorCambio = stValorCambio.Trim();
                stValorOriginal = stValorOriginal.Trim();

                //Valida el campo que sea igual no importando mayusculas y minusculas
                stValorCambio = stValorCambio.ToUpper();
                stValorOriginal = stValorOriginal.ToUpper();
                

                //if (dataGridCampos.Rows[iRows].Cells[3].Value != dataGridCampos.Rows[iRows].Cells[4].Value)
                int iCont = 0, iBand = 0;
                
                if (stValorCambio != stValorOriginal)
                {
                    
                    //stConsecutivo = dataGridCampos.Rows[iRows].Cells[3].Value.ToString();
                    //stConsecutivo = dataGridCampos.Rows[iRows].Cells[1].Value.ToString();
                    stConsecutivo = dataGridCampos.Rows[iRows].Cells[iConsecutivoDGV].Value.ToString();

                    //stRegistroIDcambios = dataGridCampos.Rows[iRows].Cells[0].Value.ToString() + dataGridCampos.Rows[iRows].Cells[1].Value.ToString();
                    //stRegistroIDcambios = dataGridCampos.Rows[iRows].Cells[0].Value.ToString() + dataGridCampos.Rows[iRows].Cells[2].Value.ToString();
                    stRegistroIDcambios = dataGridCampos.Rows[iRows].Cells[iRegistroDGV].Value.ToString() + dataGridCampos.Rows[iRows].Cells[iIDCampoDGV].Value.ToString();

                    //MODIF MAP 2011/NOV/14                    
                    if (stRegistroIDcambios.Substring(0, 3) == "009" && stValorOriginal == "")
                    {

                    }
                    else
                    {
                        do
                        {
                            if (dataGridMandatorios.Rows[iCont].Cells[0].Value.ToString() == stRegistroIDcambios)
                            {
                                stTamañoString = dataGridMandatorios.Rows[iCont].Cells[3].Value.ToString();

                                // Si el valor esta tomado de un Indice del Combo
                                if (dataGridMandatorios.Rows[iCont].Cells[5].Value.ToString() != "000")
                                {
                                    LlenaComboCatalogos(cbCambiosTemp, dataGridMandatorios.Rows[iCont].Cells[5].Value.ToString());

                                    cbCambiosTemp.Items.Insert(0, "");

                                    if (char.IsNumber(stValorOriginal, 0))
                                    {
                                        //int iCboIndex = int.Parse(stValorOriginal) - 1;
                                        int iCboIndex = int.Parse(stValorOriginal);
                                        stValorOriginalCombo = cbCambiosTemp.Items[iCboIndex].ToString();
                                    }
                                    if (char.IsLetter(stValorOriginal, 0))
                                    {
                                        string stLineaCombo2 = null;
                                        for (int iCelCombo = 0; iCelCombo < cbCambiosTemp.Items.Count; iCelCombo++)
                                        {
                                            stLineaCombo2 = cbCambiosTemp.Items[iCelCombo].ToString();
                                            if (stLineaCombo2.IndexOf(stValorOriginal[0]) > -1)
                                            {
                                                stValorOriginalCombo = cbCambiosTemp.Items[iCelCombo].ToString();
                                                break;
                                            }
                                        }
                                    }
                                    // Compara los valores del Combo Visible, con el Valor Original Equivalente en Combo
                                    //      Strings, NO indices
                                    if (stValorCambio != stValorOriginalCombo)
                                    {
                                        //MODIF MAP 2011/NOV/15
                                        if (stRegistroIDcambios.Substring(0, 3) == "009" && stValorOriginalCombo == "")
                                        {

                                        }
                                        else
                                        {

                                            iNumCambios++;

                                            //stCambioTemp = dataGridCampos.Rows[iRows].Cells[3].Value.ToString();
                                            stCambioTemp = "0";
                                            for (int iLineaCombo = 0; iLineaCombo < cbCambiosTemp.Items.Count; iLineaCombo++)
                                            {
                                                if (cbCambiosTemp.Items[iLineaCombo].ToString() == stValorCambio)
                                                {
                                                    stCambioTemp = iLineaCombo.ToString();
                                                    break;
                                                }
                                            }

                                            /*for (int iRows2 = 0; iRows2 < dataGridMandatorios.RowCount - 1; iRows2++)
                                            {
                                                if (dataGridMandatorios.Rows[iRows2].Cells[0].Value.ToString() == stRegistroIDcambios)
                                                {
                                                    if (dataGridMandatorios.Rows[iRows2].Cells[6].Value.ToString() == "A")
                                                        stCambioTemp = valida_Tam_String(stCambioTemp, int.Parse(stTamañoString));
                                                    else
                                                        stCambioTemp = valida_Tam_Numerico(stCambioTemp, int.Parse(stTamañoString));
                                                }

                                            }*/
                                            stCambioTemp = valida_Tam_Numerico(stCambioTemp, int.Parse(stTamañoString));
                                            //stRegistroIDcambios = stRegistroIDcambios.Substring(1, 2) + "0" + stRegistroIDcambios.Substring(3, 3);
                                            stTipoFormato = stRegistroIDcambios.Substring(1, 2);
                                            stRegistroIDcambios = "0" + stRegistroIDcambios.Substring(3, 3);
                                            //dataGridCambios.Rows.Add(stRegistroIDcambios, stCambioTemp);
                                            dataGridCambios.Rows.Add(stTipoFormato, stConsecutivo, stRegistroIDcambios, stCambioTemp);

                                            iBand = 1;
                                        
                                        }
                                    }
                                }
                                // Si existe un cambio y el cambio no depende del Indice de un Combo
                                else
                                {
                                    iNumCambios++;

                                    //stCambioTemp = dataGridCampos.Rows[iRows].Cells[3].Value.ToString();
                                    //stCambioTemp = dataGridCampos.Rows[iRows].Cells[4].Value.ToString();
                                    stCambioTemp = dataGridCampos.Rows[iRows].Cells[iValorDGV].Value.ToString();

                                    for (int iRows2 = 0; iRows2 < dataGridMandatorios.RowCount - 1; iRows2++)
                                    {
                                        if (dataGridMandatorios.Rows[iRows2].Cells[0].Value.ToString() == stRegistroIDcambios)
                                        {
                                            if (dataGridMandatorios.Rows[iRows].Cells[6].Value.ToString() == "A")
                                                stCambioTemp = valida_Tam_String(stCambioTemp, int.Parse(stTamañoString));
                                            else
                                                stCambioTemp = valida_Tam_Numerico(stCambioTemp, int.Parse(stTamañoString));
                                            iRows2 = dataGridMandatorios.RowCount;
                                        }
                                    }
                                    //stRegistroIDcambios = stRegistroIDcambios.Substring(1, 2) + "0" + stRegistroIDcambios.Substring(3, 3);
                                    stTipoFormato = stRegistroIDcambios.Substring(1, 2);
                                    stRegistroIDcambios = "0" + stRegistroIDcambios.Substring(3, 3);
                                    //dataGridCambios.Rows.Add(stRegistroIDcambios, stCambioTemp.ToUpper());
                                    //dataGridCambios.Rows.Add(stConsecutivo, stRegistroIDcambios, stCambioTemp.ToUpper());
                                    dataGridCambios.Rows.Add(stTipoFormato, stConsecutivo, stRegistroIDcambios, stCambioTemp.ToUpper());

                                    iBand = 1;
                                }
                                iBand = 1;
                            }
                            iCont++;
                        } while (iBand == 0 && iCont < dataGridMandatorios.RowCount - 1);
                    }                                        
                }
            }
            if (iNumCambios > 0)
            {
                stNumCambios = valida_Tam_Numerico(iNumCambios.ToString(), 4);
                LeeGridCambiosParaEnvio();
            }
        }

        private void LeeGridCambiosParaEnvio()
        {
            ListaCambios.Clear();
            string stLinea, stNuevoCampo;
            int iLineaLista = 0;

            //stLinea = dataGridCambios.Rows[0].Cells[0].Value.ToString() + dataGridCambios.Rows[0].Cells[1].Value.ToString();
            stLinea = dataGridCambios.Rows[0].Cells[0].Value.ToString() + dataGridCambios.Rows[0].Cells[1].Value.ToString() + dataGridCambios.Rows[0].Cells[2].Value.ToString() + dataGridCambios.Rows[0].Cells[3].Value.ToString();
            ListaCambios.Add(stLinea);
            for (int iRows = 1; iRows < dataGridCambios.Rows.Count - 1; iRows++)
            {
                //stNuevoCampo = dataGridCambios.Rows[iRows].Cells[0].Value.ToString() + dataGridCambios.Rows[iRows].Cells[1].Value.ToString();
                //stNuevoCampo = dataGridCambios.Rows[iRows].Cells[0].Value.ToString() + dataGridCambios.Rows[iRows].Cells[1].Value.ToString() + dataGridCambios.Rows[iRows].Cells[2].Value.ToString();
                stNuevoCampo = dataGridCambios.Rows[iRows].Cells[0].Value.ToString() + dataGridCambios.Rows[iRows].Cells[1].Value.ToString() + dataGridCambios.Rows[iRows].Cells[2].Value.ToString() + dataGridCambios.Rows[iRows].Cells[3].Value.ToString();
                if (stLinea.Length + stNuevoCampo.Length < 1700)
                {                           
                    ListaCambios[iLineaLista] += stNuevoCampo;                    
                }
                else
                {
                    stLinea = stNuevoCampo;
                    ListaCambios.Add(stLinea);                    
                    iLineaLista++;
                }

            }

        }

        // METODO QUE ENVIA A BACK CUANDO SE HA TERMINADO DE HACER CAMBIOS A LOS DATOS
        private bool envioTransaccion() 
        {
            //wInspeccion wInsp = new wInspeccion();
            clsWRemesas wInsp = new clsWRemesas();
            string stLineaLista = "00";
            //bool bolExito = false;
            // Si existen cambios entra al FOR
            for (int iLinea = 0; iLinea < ListaCambios.Count; iLinea++)
            {
                stLineaLista = ListaCambios[iLinea] + stLineaLista;
                if (ListaCambios.Count == 1)
                {
                    if (wInsp.EnviaRegistroResultadosInspeccion5562_63(stRemesaRef, stNominaRef, stPreimpresoRef, "0", "1", stNumCambios, stLineaLista))
                    {
                        dataGridFoliosRef.Rows[iDataGridIndexRef].Cells[2].Value = "Inspeccionado Sin Terminar Muestra";
                        btnInspeccionFolioRef.Enabled = false;
                        return bolExito = true;
                    }
                    else
                        return false;
                }
                else
                {
                    if (wInsp.EnviaRegistroResultadosInspeccion5562_63(stRemesaRef, stNominaRef, stPreimpresoRef, "1", "1", stNumCambios, stLineaLista))
                        bolExito = true;
                    else
                        return false;
                }
            }

            if (wInsp.EnviaRegistroResultadosInspeccion5562_63(stRemesaRef, stNominaRef, stPreimpresoRef, "0", "0", stNumCambios, stLineaLista))
            {
                dataGridFoliosRef.Rows[iDataGridIndexRef].Cells[2].Value = "Inspeccionado Sin Terminar Muestra";
                btnInspeccionFolioRef.Enabled = false;
                return true;
            }
            else
                return false;

        }

        //METODO QUE REGRESA LA CADENA EN UN TAMAÑO DETERMINADO CON FORMATO "00...0DATO"
        private string valida_Tam_Numerico(string stCad, int iTam)
        {
            int iTamano = new int();
            if (stCad.Length < iTam)
            {
                //iTamano = stCad.Length;
                //for (int iCfor = 0; iCfor < (iTam - iTamano); iCfor++)
                //    stCad = "0" + stCad;

                stCad = stCad.PadLeft(iTam, '0');

            }
            else
                stCad = stCad.Substring(0, iTam);
            return stCad;
        }

        //METODO QUE REGRESA LA CADENA EN UN TAMAÑO DETERMINADO CON FORMATO "DATO       "
        private string valida_Tam_String(string stCad, int iTam)
        {
            int iTamano = new int();
            if (stCad.Length < iTam)
            {
                //iTamano = stCad.Length;
                //for (int iCfor = 0; iCfor < (iTam - iTamano); iCfor++)
                //    stCad = stCad + " ";

                stCad = stCad.PadRight(iTam, ' ');
            }
            else
                stCad = stCad.Substring(0, iTam);
            return stCad;
        }

        private void dataGridCampos_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridCampos.CurrentCell.IsInEditMode)
                {
                    if (dataGridCampos.CurrentCell.GetType() == typeof(DataGridViewComboBoxCell))
                    {
                        DataGridViewCell cell = dataGridCampos.Rows[e.RowIndex].Cells[e.ColumnIndex];
                        cell.Value = newCellValue;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("[" + ex.Source + "] " + ex.Message);
            }

        }

        private void dataGridCampos_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {                
                if (dataGridCampos.CurrentCell.IsInEditMode)
                {
                    if (dataGridCampos.CurrentCell.GetType() == typeof(DataGridViewComboBoxCell))
                    {
                        DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)dataGridCampos.Rows[e.RowIndex].Cells[e.ColumnIndex];

                        if (!cell.Items.Contains(e.FormattedValue))
                        {
                            cell.Items.Add(e.FormattedValue);
                            cell.Value = e.FormattedValue;
                            cell.Items.RemoveAt(0);
                        }
                        newCellValue = e.FormattedValue;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // COMPARAR ID CAMPO
        private void dataGridCampos_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (((DataGridViewComboBoxCell)dataGridCampos.CurrentCell).Items.Count <= 1)
                {
                    if (e.Control.GetType() == typeof(DataGridViewComboBoxEditingControl))
                    {
                        ((ComboBox)e.Control).DropDownStyle = ComboBoxStyle.DropDown;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta Cerrando Esta Ventana No Se Guardara Ningun Cambio\n\n ¿Desea Continuar?",
                "S753 ARIES - Inspeccion de Remesas", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                iClose = 1;
                this.Close();
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if(iClose == 0)
                if (MessageBox.Show("Esta Cerrando Esta Ventana No Se Guardara Ningun Cambio\n\n ¿Desea Continuar?",
                    "S753 ARIES - Inspeccion de Remesas", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) != DialogResult.OK)                    
                    e.Cancel = true;

            mdlComunica.OleCatalogos.setLongClave = 0;
        }
                
        /*********************************************************************************************
         * Metodo que Llama al Metodo LlamaCombo, para carga de Catalogos de Mandatorios
         * 
         * 
        /*********************************************************************************************/
        private void LlenaComboCatalogos(ComboBox cbPuenteTemp, string stCatalogoTemp)
        {
            /* CATALOGOS: 
             * "19"  - ESTADOS REPUBLICA
             * "20"  - ESTADO CIVIL
             * "22"  - ESCOLARIDAD
             * "23"  - OCUPACION
             * "28"  - TIPO VIVIENDA
             * "45"  - GIRO EMPRESA
             * "75"  - TIPO DE EMPLEO ID / PUESTO EN LA EMPRESA DES
             * "87"  - IDENTIFICACION DE CATALOGO
             * "263" - MANDATORIOS */
            string stCatalogo = int.Parse(stCatalogoTemp).ToString();
            string stAtributos = null;
            int iContador = 0;
            
            cbPuenteTemp.Items.Clear();

            //if (stCatalogoTemp != "267")
            //    cbPuenteTemp.Items.Add("     ");
            
            string tempRefParam1;
            string tempRefParam2;
            string tempRefParam3;
            string tempRefParam4;
            string tempRefParam5;
            string tempRefParam6;

            if (stCatalogo == "19" || stCatalogo == stCatalogoMantadatorio)
                tempRefParam6 = "E";
            else
                tempRefParam6 = "D";

            tempRefParam1 = stCatalogo;
            tempRefParam2 = "";
            tempRefParam3 = "";
            tempRefParam4 = "";
            tempRefParam5 = "";

            mdlComunica.OleCatalogos.setLongClave = 0; //REVISAR PARA QUE SIRVE ESTA LINEA
            Catalogos.clsCatalogos.enmAlineaciones tempRefParamCatenm = Catalogos.clsCatalogos.enmAlineaciones.Sin_Clave;
            mdlComunica.OleCatalogos.setAlineacionClave = tempRefParamCatenm;
            mdlComunica.OleCatalogos.LlenaCombo(ref cbPuenteTemp, ref tempRefParam1, ref tempRefParam2, ref tempRefParam3,
                ref tempRefParam4, ref tempRefParam5, ref tempRefParam6);            

            if (stCatalogo == stCatalogoMantadatorio)
            {
                llena50caracteres();

                mdlComunica.OleCatalogos.AbreCatalogo(ref stCatalogo);
                mdlComunica.OleCatalogos.MoveFirst();
                while (!mdlComunica.OleCatalogos.EOF_Renamed())
                {                    
                    stAtributos = mdlComunica.OleCatalogos.getAtributos.Trim();
                    cbPuenteTemp.Items[iContador] = cbPuenteTemp.Items[iContador].ToString() + stAtributos;
                    iContador++;
                    mdlComunica.OleCatalogos.MoveNext();
                }
                mdlComunica.OleCatalogos.CierraCatalogo();                
            }
            
        }

        private void llena50caracteres()
        {
            string stLineaNuevaTemp;
            for (int iLineaCombo = 0; iLineaCombo < cBPuente.Items.Count; iLineaCombo++)
            {
                stLineaNuevaTemp = cBPuente.Items[iLineaCombo].ToString();
                stLineaNuevaTemp = stLineaNuevaTemp.PadRight(50, ' ');
                cBPuente.Items[iLineaCombo] = stLineaNuevaTemp;
            }
        }

        private void firmaS041ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.Cursor = Cursors.Default;
            mdlGlobales.gblnChecaAcess = true;
            mdlGlobales.gblnBandAcess = true;
            mdlGlobales.gblnBandCancela = false;
            mdlGlobales.gblnEstaSeg = MDIMasivos.DefInstance.OleAcceso.FirmarS041();
        }
                        
    }
}