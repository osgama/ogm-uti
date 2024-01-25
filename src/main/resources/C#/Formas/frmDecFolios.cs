using Artinsoft.VB6.Utils;
using Microsoft.VisualBasic;
using System;
using System.Globalization;
using System.Windows.Forms;
using VB6 = Microsoft.VisualBasic.Compatibility.VB6.Support;

namespace Masivos
{
    internal partial class frmDecFolios
        : System.Windows.Forms.Form
    {

        //*******************************************************************************
        //* Identificación: frmDecFolios.frm                                            *
        //* Autor:          Israel Javier Garcés Morales                                *
        //* Instalación:    PRAXIS                                                      *
        //* Fecha:          09/09/2003                                                  *
        //* Versión:        1.0                                                         *
        //* Objetivo: Declinación en el procesamiento masivo a nivel Folio (solicitud). *
        //*           Se accesa a esta pantalla si en la pantalla "Selección tipo de    *
        //*           Declinación" al regsitrar la Remesa, se selecciona la opción por  *
        //*           Folio.                                                            *
        //*******************************************************************************
        private XArrayDBObject.XArrayDB _grdDatosRemesa = null;
        XArrayDBObject.XArrayDB grdDatosRemesa
        {
            get
            {
                if (_grdDatosRemesa == null)
                {
                    _grdDatosRemesa = new XArrayDBObject.XArrayDB();
                }
                return _grdDatosRemesa;
            }
            set
            {
                _grdDatosRemesa = value;
            }
        }


        //*******************************************************************************
        //* Finalidad: Cargar datos en la Remesa
        //*******************************************************************************
        //UPGRADE_WARNING: (1041) Form_Load event was upgraded to Form_Load method and has a new behavior.
        private void Form_Load()
        {
            try
            {
                TrueDBGrid80.ValueItem Item = new TrueDBGrid80.ValueItem();
                int intCont = 0;

                mdlGlobales.subOcultaBotones();
                mdlGlobales.gstrFolInterno.Value = mdlGlobales.funZeroes(8);
                mdlGlobales.CentrarMDIHija(MDIMasivos.DefInstance, frmDecFolios.DefInstance);
                mdlGlobales.gblnDeclinaFolios = true;

                mdlTranMasivo.gvstrUltimoFolioRemesa = frmProcMasivo.DefInstance.txtFolInicial.Text;
                mdlTranMasivo.gvstrPrimerFolioRemesa = frmProcMasivo.DefInstance.txtFolFinal.Text;
                mdlTranMasivo.gvstrNumSolicitudes = frmProcMasivo.DefInstance.txtNumSolicitudes.Text;

                // Coloca la información de Remesa
                txtRemesa.Text = mdlTranMasivo.gvstrRemesa;
                txtFolInicial.Text = mdlTranMasivo.gvstrUltimoFolioRemesa;
                txtFolFinal.Text = mdlTranMasivo.gvstrPrimerFolioRemesa;
                txtNumSolicitudes.Text = mdlTranMasivo.gvstrNumSolicitudes;
                int intRows = mdlTranMasivo.estMasivo.GetUpperBound(0) - 1;

                // Carga los datos de la Remesa
                grdDatosRemesa.ReDim(0, intRows, 0, 2);
                for (intCont = 0; intCont <= intRows; intCont++)
                {
                    grdDatosRemesa.Set(intCont, 0, mdlTranMasivo.estMasivo[intCont].strFolioPreimpreso);
                    grdDatosRemesa.Set(intCont, 1, mdlTranMasivo.estMasivo[intCont].strNombreSolicitante);
                    grdDatosRemesa.Set(intCont, 2, " ");
                }
                tdbFoliosRemesa.Array = grdDatosRemesa;
                tdbFoliosRemesa.ReBind();

                intCont = 0;
                mdlComunica.OleCatalogos.setLongClave = 4;
                mdlComunica.OleCatalogos.setAlineacionClave = Catalogos.clsCatalogos.enmAlineaciones.Izquierda;
                object tempRefParam = cboCausaDec.Text;
                string tempRefParam2 = "25";
                string tempRefParam3 = String.Empty;
                string tempRefParam4 = String.Empty;
                string tempRefParam5 = String.Empty;
                string tempRefParam6 = String.Empty;
                string tempRefParam7 = "D";
                mdlComunica.OleCatalogos.LlenaCombo(ref cboCausaDec, ref tempRefParam2, ref tempRefParam3, ref tempRefParam4, ref tempRefParam5, ref tempRefParam6, ref tempRefParam7);
                mdlComunica.OleCatalogos.setLongClave = 2;
                string tempRefParam8 = "25";
                string tempRefParam9 = "D";
                mdlComunica.OleCatalogos.AbreCatalogo(ref tempRefParam8, ref tempRefParam9);
                while (!mdlComunica.OleCatalogos.EOF_Renamed())
                {

                    //UPGRADE_WARNING: (1037) Couldn't resolve default property of object Item.DisplayValue.
                    Item.DisplayValue = mdlComunica.OleCatalogos.getLlave1.Substring(mdlComunica.OleCatalogos.getLlave1.Length - Math.Min(mdlComunica.OleCatalogos.getLlave1.Length, 4)) + " " + mdlComunica.OleCatalogos.getDescripcion.Trim();
                    //UPGRADE_WARNING: (1037) Couldn't resolve default property of object Item.Value.
                    Item.Value = mdlComunica.OleCatalogos.getLlave1.Substring(mdlComunica.OleCatalogos.getLlave1.Length - Math.Min(mdlComunica.OleCatalogos.getLlave1.Length, 4)) + " " + mdlComunica.OleCatalogos.getDescripcion.Trim();
                    tdbFoliosRemesa.Columns[2].ValueItems.Add(Item);
                    tdbFoliosRemesa.Columns[2].ValueItems.Presentation = TrueDBGrid80.PresentationConstants.dbgComboBox;
                    tdbFoliosRemesa.Columns[2].ValueItems.Translate = true;
                    intCont++;
                    mdlComunica.OleCatalogos.MoveNext();
                }
                tdbFoliosRemesa.ReBind();
            }
            catch (Exception error)
            {
                Console.WriteLine(error.StackTrace);
            }
        }

        //******************************************************************************
        //* Finalidad: Guarda los folios declinados
        //******************************************************************************
        private void cmdAceptar_Click(Object eventSender, EventArgs eventArgs)
        {
            string strCausaDeclinacion = String.Empty;
            mdlGlobales.gblnDeclinaFolios = false;
            // Valida si se han declinado folios
            tdbFoliosRemesa.MoveFirst();
            for (int intCont = 0; intCont <= mdlTranMasivo.estMasivo.GetUpperBound(0); intCont++)
            {
                strCausaDeclinacion = tdbFoliosRemesa.Columns[2].Text.Substring(0, Math.Min(tdbFoliosRemesa.Columns[2].Text.Length, 6));
                if (strCausaDeclinacion.Length > 0)
                {
                    mdlGlobales.gblnDeclinaFolios = true;
                }
                mdlTranMasivo.estMasivo[intCont].strCausaDeclinacion = strCausaDeclinacion;
                tdbFoliosRemesa.MoveNext();
            }
            if (mdlGlobales.gblnDeclinaFolios)
            {
                frmRegRemesas.DefInstance.cmdDeclinar.Enabled = false;
                frmRegRemesas.DefInstance.Show();
                //AIS-1896 FSABORIO
                if (!this.SuspendFormClosing)
                    this.Close();
            }
            else
            {
                mdlGlobales.subDespMensajes("NO SE HAN DECLINADO FOLIOS..."); //, vbInformation + vbOKOnly
                tdbFoliosRemesa.Focus();
            }
        }

        //******************************************************************************
        //* Finalidad: Determina si el Usuario desea Salir y Cancelar
        //******************************************************************************
        private void cmdCancelar_Click(Object eventSender, EventArgs eventArgs)
        {
            mdlCatalogos.gstrCatProceso = "";
            // Sale del Formulario
            mdlGlobales.gblnDeclinaFolios = false;
            // Limpia Folios
            for (int intCont = 0; intCont <= mdlTranMasivo.estMasivo.GetUpperBound(0); intCont++)
            {
                mdlTranMasivo.estMasivo[intCont].strCausaDeclinacion = "";
            }
            frmRegRemesas.DefInstance.Show();
            //AIS-1896 FSABORIO
            if (!this.SuspendFormClosing)
                this.Close();
        }

        //******************************************************************************
        //* Finalidad: Guarda los folios declinados
        //******************************************************************************
        public void mnuAceptar_Click(Object eventSender, EventArgs eventArgs)
        {
            cmdAceptar_Click(cmdAceptar, new EventArgs());
        }

        //******************************************************************************
        //* Finalidad: Determina si el Usuario desea Salir Y Cancelar
        //******************************************************************************
        public void mnuCancelar_Click(Object eventSender, EventArgs eventArgs)
        {
            cmdCancelar_Click(cmdCancelar, new EventArgs());
        }

        private void tdbFoliosRemesa_KeyPressEvent(Object eventSender, AxTrueDBGrid80.TrueDBGridEvents_KeyPressEvent eventArgs)
        {
            if (tdbFoliosRemesa.Col == 2)
            {
                double dbNumericTemp = 0;
                //AIS-CASE laralar
                if (Strings.Asc(Strings.Chr(eventArgs.keyAscii).ToString().ToUpper()[0]) >= 65 && Strings.Asc(Strings.Chr(eventArgs.keyAscii).ToString().ToUpper()[0]) <= 90 || Double.TryParse(Strings.Chr(eventArgs.keyAscii).ToString(), NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp))
                {
                    //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                    ComboBox tempRefParam = cboCausaDec;
                    Keys tempRefParam2 = (Keys)eventArgs.keyAscii;
                    mdlCatalogos.subBusquedaRapidaCadena(ref tempRefParam, ref tempRefParam2);
                    cboCausaDec = tempRefParam;
                    if (mdlCatalogos.estCombos.intUltimoIndice > -1)
                    {
                        if (cboCausaDec.SelectedIndex > -1)
                        {
                            //UPGRADE_WARNING: (1068) tdbFoliosRemesa.Columns().ValueItems.Item().DisplayValue of type Variant is being forced to Scalar.
                            //UPGRADE_WARNING: (1037) Couldn't resolve default property of object tdbFoliosRemesa.Columns.Item().Value.
                            tdbFoliosRemesa.Columns[2].Value = tdbFoliosRemesa.Columns[2].ValueItems[cboCausaDec.SelectedIndex].DisplayValue;
                        }
                    }
                }
                else
                {
                    if (eventArgs.keyAscii == 27)
                    {
                        mdlCatalogos.estCombos.intUltimoIndice = -1;
                        mdlCatalogos.estCombos.strLlaveAlfa = "";
                        mdlCatalogos.estCombos.strLlaveNum = "";
                        cboCausaDec.SelectedIndex = mdlCatalogos.estCombos.intUltimoIndice;
                        //UPGRADE_WARNING: (1037) Couldn't resolve default property of object tdbFoliosRemesa.Columns().Value.
                        tdbFoliosRemesa.Columns[2].Value = "";
                    }
                }
            }
            else if (eventArgs.keyAscii != 27)
            {
                eventArgs.keyAscii = 0;
            }
        }

        private void tdbFoliosRemesa_RowColChange(Object eventSender, AxTrueDBGrid80.TrueDBGridEvents_RowColChangeEvent eventArgs)
        {
            if (eventArgs.lastCol != tdbFoliosRemesa.Col)
            {
                mdlCatalogos.estCombos.intUltimoIndice = -1;
                mdlCatalogos.estCombos.strLlaveAlfa = "";
                mdlCatalogos.estCombos.strLlaveNum = "";
                cboCausaDec.SelectedIndex = mdlCatalogos.estCombos.intUltimoIndice;
            }
        }
        private void frmDecFolios_Closed(Object eventSender, EventArgs eventArgs)
        {
            MemoryHelper.ReleaseMemory();
        }

        //AIS-1896 FSABORIO
        public bool SuspendFormClosing = false;
        private void frmDecFolios_FormClosing(object sender, FormClosingEventArgs eventArgs)
        {
            //AIS-1896 FSABORIO
            SuspendFormClosing = true;
        }
    }
}