using Microsoft.VisualBasic;
using System;
using System.IO;
using System.Windows.Forms;
using VB6 = Microsoft.VisualBasic.Compatibility.VB6.Support;

namespace Masivos
{
    internal partial class MDIMasivos
        : System.Windows.Forms.Form
    {

        //*******************************************************************************
        //* Identificación: MDIMasivos                                                  *
        //* Autor:          Luis E. Aguilar; Abel Polo                                  *
        //* Instalación:    PRAXIS                                                      *
        //* Fecha:          4/09/2003                                                   *
        //* Versión:        1.0                                                         *
        //*******************************************************************************

        //*******************************************************************************
        //* Finalidad:  Subrutina para Inicializar los Valores de la Forma
        //*******************************************************************************
        private void MDIForm_Initialize_Renamed()
        {
            mdlGlobales.gblnBandCancela = false;
            mdlGlobales.gblnChecaCancela = true;
            tmrHoraBarraStatus.Enabled = true;
            //Crear las carpetas necesarias
            //string tempRefParam = mdlGlobales.gcStrMasivosIni;
            //string tempRefParam2 = "RUTA_ERROR";
            //string tempRefParam3 = "RUTA";
            //MIG WXP INI JGC 20090825
            //string strPath = mdlGlobales.funGetParam(ref tempRefParam, ref tempRefParam2, ref tempRefParam3);
            string strMASRutaAplicacion = mdlRegistry.RegistryMasivos("MASRutaAplicacion");
            string strPath = mdlRegistry.RegistryMasivos("MASRutaBitacora");
            //MIG WXP FIN JGC 20090825
            mdlGlobales.subValidaRuta(strMASRutaAplicacion + strPath);
            //string tempRefParam4 = mdlGlobales.gcStrMasivosIni;
            //string tempRefParam5 = "INTELAR";
            //string tempRefParam6 = "RUTA_BITACORA";
            //MIG WXP INI JGC 20090825
            //strPath = mdlGlobales.funGetParam(ref tempRefParam4, ref tempRefParam5, ref tempRefParam6);
            //strPath = mdlRegistry.RegistryMasivos("MASRutaBitacora");
            //mdlGlobales.subValidaRuta(strPath);
            //MIG WXP FNI JGC 20090825
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Desplegar Informacion de Fecha Actual en el MDI
        //*******************************************************************************
        //UPGRADE_WARNING: (1041) Form_Load event was upgraded to Form_Load method and has a new behavior.
        public bool closing;
        private void Form_Load()
        {
            //AIS-1899 FSABORIO
            pnlEstado.Items[3].Text = DateTime.Today.ToString("dd/MM/yyyy");
            //AIS-1899 FSABORIO
            pnlEstado.Items[1].Text = mdlGlobales.gstrNombreUsuario;
            this.lblVersion.Text = funEscribeVersion();
            this.closing = false;
        }

        private void OleAcceso_CargarCatalogos(object Sender, EventArgs e)
        {
            mdlComunica.OleCatalogos = new Catalogos.clsCatalogos();
        }

        private void OleAcceso_ConexionExitosa(object Sender, EventArgs e)
        {
            this.Show();
        }

        private void OleAcceso_ErrorConexion(object Sender, Acceso.UsrCtlAcceso.ErrorConexionEventArgs e)
        //(ref  string MensajeError, ref  MsgBoxStyle TipoError)
        {
            MsgBoxStyle tempRefParam = e.TipoError;
            string tempRefParam2 = "CONEXION S041";
            mdlGlobales.subDespErrores(ref e.MensajeError, ref tempRefParam, ref tempRefParam2);
        }

        private void tmrFtp_Tick(Object eventSender, EventArgs eventArgs)
        {
            mdlFTPIntelar.gblnEstaCorriendo = false; //Apagar la bandera de que sigue corriendo
        }

        private void tmrFTPVC_Tick(Object eventSender, EventArgs eventArgs)
        {
            mdlFTPIntelar.gblnEstaCorriendo = false; //Apagar la bandera de que sigue corriendo
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Desplegar Informacion de Hora Actual en el MDI
        //*******************************************************************************
        private void tmrHoraBarraStatus_Tick(Object eventSender, EventArgs eventArgs)
        {
            //AIS-1899 FSABORIO
            pnlEstado.Items[2].Text = (DateTime.Now.ToString("HH:mm:ss") + " ");
        }

        //*******************************************************************************
        //* Finalidad:  Subrutina para Salir de la Forma y Guardar Datos en la Bitacora
        //*******************************************************************************
        //AIS-1896 FSABORIO
        public bool SuspendFormClosing = false;
        private void MDIMasivos_FormClosing(Object eventSender, FormClosingEventArgs eventArgs)
        {
            //AIS-1896 FSABORIO
            SuspendFormClosing = true;

            int Cancel = (eventArgs.Cancel) ? 1 : 0;
            int UnloadMode = (int)eventArgs.CloseReason;
            if (this.Visible)
            {
                if (MessageBox.Show("¿ESTA SEGURO DE QUERER SALIR DE LA APLICACIÓN?", "SALIR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                {
                    Cancel = -1;
                    closing = false;
                }
                else
                {
                    bool salir = false;
                    try
                    {
                        if (frmCausasDec.m_vb6FormDefInstance != null)
                            //AIS-1896 FSABORIO
                            if (!frmCausasDec.DefInstance.SuspendFormClosing)
                                frmCausasDec.DefInstance.Close();
                        if (frmConsRemesa.m_vb6FormDefInstance != null)
                            //AIS-1896 FSABORIO
                            if (!frmConsRemesa.DefInstance.SuspendFormClosing)
                                frmConsRemesa.DefInstance.Close();
                        if (frmConsTodaRemesa.m_vb6FormDefInstance != null)
                            //AIS-1896 FSABORIO
                            if (!frmConsTodaRemesa.DefInstance.SuspendFormClosing)
                                frmConsTodaRemesa.DefInstance.Close();
                        if (frmDecFolios.m_vb6FormDefInstance != null)
                            //AIS-1896 FSABORIO
                            if (!frmDecFolios.DefInstance.SuspendFormClosing)
                                frmDecFolios.DefInstance.Close();
                        if (frmPredFolio.m_vb6FormDefInstance != null)
                            //AIS-1896 FSABORIO
                            if (!frmPredFolio.DefInstance.SuspendFormClosing)
                                frmPredFolio.DefInstance.Close();
                        if (frmProceso.m_vb6FormDefInstance != null)
                            //AIS-1896 FSABORIO
                            if (!frmProceso.DefInstance.SuspendFormClosing)
                                frmProceso.DefInstance.Close();
                        if (frmProcMasivo.m_vb6FormDefInstance != null)
                            //AIS-1896 FSABORIO
                            if (!frmProcMasivo.DefInstance.SuspendFormClosing)
                                frmProcMasivo.DefInstance.Close();
                        if (frmRegRemesas.m_vb6FormDefInstance != null)
                            //AIS-1896 FSABORIO
                            if (!frmRegRemesas.DefInstance.SuspendFormClosing)
                                frmRegRemesas.DefInstance.Close();
                        if (frmTipoDeclinacion.m_vb6FormDefInstance != null)
                            //AIS-1896 FSABORIO
                            if (!frmTipoDeclinacion.DefInstance.SuspendFormClosing)
                                frmTipoDeclinacion.DefInstance.Close();

                        //MIG WXP INI JGC 20090825                        
                        // SE ELIMINA ESTA VENTANA QUE NO SE UTLIZA
                        //if (frmVentasCruzadas.m_vb6FormDefInstance != null)
                        //    //AIS-1896 FSABORIO
                        //    if (!frmVentasCruzadas.DefInstance.SuspendFormClosing)
                        //        frmVentasCruzadas.DefInstance.Close();
                        //MIG WXP FNI JGC 20090825
                    }
                    catch (Exception error)
                    {
                        Console.WriteLine(error.StackTrace);
                    }
                    //UPGRADE_ISSUE: (2070) Constant vbSModeStandalone was not upgraded.
                    //UPGRADE_ISSUE: (2070) Constant App was not upgraded.

                    //AIS-1010 rvillalta
                    //UPGRADE_ISSUE: (2064) App property App.StartMode was not upgraded.
                    //if ( App.StartMode == ((int) vbSModeStandalone))
                    if (false)
                    {
                        subCerrarMasivos();
                    }
                    else
                    {
                        Cancel = -1;
                        this.Visible = false;
                    }
                }
                eventArgs.Cancel = Cancel != 0;
            }
            //AIS-1896 FSABORIO
            SuspendFormClosing = !eventArgs.Cancel;
        }

        public void subDesfirmaUsuario(bool blnMuestraProceso)
        {
            //AIS-1899 FSABORIO
            pnlEstado.Items[0].Text = "Cerrando sesión con Seguridad Corporativa - S041";
            if (blnMuestraProceso)
            {
                frmProceso.DefInstance.Show();
            }
            Application.DoEvents();
            string strCadena = "00410400000070030"; //Desfirma del S041
            mdlComunica.gvMensaje = strCadena;
            mdlGlobales.subRegBitacora("E");
            mdlGlobales.subDespMensajes("ESPERE...");
            string strRecibe = mdlComunica.funCON(strCadena);
            MessageBox.Show(strRecibe, "MASIVOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (blnMuestraProceso)
            {
                //AIS-1896 FSABORIO
                if (!frmProceso.DefInstance.SuspendFormClosing)
                    frmProceso.DefInstance.Close();
            }
        }

        //*********************************************************************************
        //* Finalidad:  Valida que Cuando se Seleccione este Menu, se Desactiven los Botones
        //*                  del MDI y se Muestre la Pantalla de Consulta por Folio
        //*********************************************************************************
        public void mnuConsFolio_Click(Object eventSender, EventArgs eventArgs)
        {
            if (mdlGlobales.gblnEstaSeg)
            {
                this.Cursor = Cursors.Default;
                mdlGlobales.gblnChecaCancela = false;
                // MODIF MAP 2015 FACULTAMIENTO
                if (Globales.clsFacultamiento.ValidaFacultades5421_G(Globales.clsFacultamiento.MasivosGral))
                    frmPredFolio.DefInstance.Show();
            }
            else
            {
                string tempRefParam = "NECESITA FIRMARSE AL S041 NUEVAMENTE";
                MsgBoxStyle tempRefParam2 = MsgBoxStyle.Information;
                string tempRefParam3 = "ERROR";
                mdlGlobales.subDespErrores(ref tempRefParam, ref tempRefParam2, ref tempRefParam3);
                this.Cursor = Cursors.Default;
                mdlGlobales.gblnBandCancela = true;
                mdlGlobales.gblnBandAcess = true;
                mdlGlobales.gblnEstaSeg = MDIMasivos.DefInstance.OleAcceso.FirmarS041();
            }
        }


        //*********************************************************************************
        //* Finalidad:  Valida que Cuando se Seleccione este Menu, se Desactiven los Botones
        //*                  del MDI y se Muestre la Pantalla de Consulta Toda la Remesa
        //*********************************************************************************
        public void mnuConsRemesa_Click(Object eventSender, EventArgs eventArgs)
        {
            if (mdlGlobales.gblnEstaSeg)
            {
                this.Cursor = Cursors.Default;
                mdlGlobales.gblnChecaCancela = false;
                // MODIF MAP 2015 FACULTAMIENTO
                if (Globales.clsFacultamiento.ValidaFacultades5421_G(Globales.clsFacultamiento.MasivosGral))
                    frmConsTodaRemesa.DefInstance.Show();
            }
            else
            {
                string tempRefParam = "NECESITA FIRMARSE AL S041 NUEVAMENTE";
                MsgBoxStyle tempRefParam2 = MsgBoxStyle.Information;
                string tempRefParam3 = "ERROR";
                mdlGlobales.subDespErrores(ref tempRefParam, ref tempRefParam2, ref tempRefParam3);
                this.Cursor = Cursors.Default;
                mdlGlobales.gblnBandCancela = true;
                mdlGlobales.gblnBandAcess = true;
                mdlGlobales.gblnEstaSeg = MDIMasivos.DefInstance.OleAcceso.FirmarS041();
            }
        }

        //*********************************************************************************
        //* Finalidad:  Valida que Cuando se Seleccione este Menu, se Desactiven los Botones
        //*                  del MDI y se Muestre la Pantalla de Acceso
        //*********************************************************************************
        public void mnuFirmaS041_Click(Object eventSender, EventArgs eventArgs)
        {
            this.Cursor = Cursors.Default;
            mdlGlobales.gblnChecaAcess = true;
            mdlGlobales.gblnBandAcess = true;
            mdlGlobales.gblnBandCancela = false;
            mdlGlobales.gblnEstaSeg = MDIMasivos.DefInstance.OleAcceso.FirmarS041();
        }

        //*********************************************************************************
        //* Finalidad:  Valida que Cuando se Seleccione este Menu, se Desactiven los Botones
        //*                  del MDI y se Muestre la Pantalla de Procesamiento Masivo (Procesar)
        //*********************************************************************************
        public void mnuProcesar_Click(Object eventSender, EventArgs eventArgs)
        {
            mdlCatalogos.gstrCatProceso = "";
            if (mdlGlobales.gblnEstaSeg)
            {
                this.Cursor = Cursors.Default;
                mdlGlobales.gblnChecaCancela = false;
                // MODIF MAP 2015 FACULTAMIENTO
                if (Globales.clsFacultamiento.ValidaFacultades5421_G(Globales.clsFacultamiento.MasivosGral))
                    frmProcMasivo.DefInstance.Show();
            }
            else
            {
                string tempRefParam = "NECESITA FIRMARSE AL S041 NUEVAMENTE";
                MsgBoxStyle tempRefParam2 = MsgBoxStyle.Information;
                string tempRefParam3 = "ERROR";
                mdlGlobales.subDespErrores(ref tempRefParam, ref tempRefParam2, ref tempRefParam3);
                this.Cursor = Cursors.Default;
                mdlGlobales.gblnBandCancela = true;
                mdlGlobales.gblnBandAcess = true;
                mdlGlobales.gblnEstaSeg = MDIMasivos.DefInstance.OleAcceso.FirmarS041();
            }
        }

        //*********************************************************************************
        //* Finalidad:  Valida que Cuando se Seleccione este Menu, se Desactiven los Botones
        //*                  del MDI y se Muestre la Pantalla de Consulta Estatus de la Remesa
        //*********************************************************************************
        public void mnuStatusRemesa_Click(Object eventSender, EventArgs eventArgs)
        {
            if (mdlGlobales.gblnEstaSeg)
            {
                this.Cursor = Cursors.Default;
                mdlGlobales.gblnChecaCancela = false;
                // MODIF MAP 2015 FACULTAMIENTO
                if (Globales.clsFacultamiento.ValidaFacultades5421_G(Globales.clsFacultamiento.MasivosGral))
                    frmConsRemesa.DefInstance.Show();
            }
            else
            {
                string tempRefParam = "NECESITA FIRMARSE AL S041 NUEVAMENTE";
                MsgBoxStyle tempRefParam2 = MsgBoxStyle.Information;
                string tempRefParam3 = "ERROR";
                mdlGlobales.subDespErrores(ref tempRefParam, ref tempRefParam2, ref tempRefParam3);
                mdlGlobales.gblnBandCancela = true;
                mdlGlobales.gblnBandAcess = true;
                mdlGlobales.gblnEstaSeg = MDIMasivos.DefInstance.OleAcceso.FirmarS041();
            }
        }

        //*****************************************************************************************************
        //* Finalidad:  Carga el Menu de Botones del MDI
        //****************************************************************************************************
        private void tlbMasivos_ButtonClick(Object eventSender, AxMSComctlLib.IToolbarEvents_ButtonClickEvent eventArgs)
        {
            mdlCatalogos.gstrCatProceso = "";
            mdlGlobales.gblnChecaCancela = false;
            if (mdlGlobales.gblnEstaSeg)
            {
                this.Cursor = Cursors.Default;
                switch (eventArgs.button.Key)
                {
                    case "btnProcMasivo":
                        // MODIF MAP 2015 FACULTAMIENTO
                        if (Globales.clsFacultamiento.ValidaFacultades5421_G(Globales.clsFacultamiento.MasivosGral))                                                                                
                            frmProcMasivo.DefInstance.Show();                        
                        break;
                    case "btnConsRemesa":
                        // MODIF MAP 2015 FACULTAMIENTO
                        if (Globales.clsFacultamiento.ValidaFacultades5421_G(Globales.clsFacultamiento.MasivosGral))
                            frmConsTodaRemesa.DefInstance.Show();                        
                        break;                    
                    case "btnStatusRemesa":
                        // MODIF MAP 2015 FACULTAMIENTO
                        if (Globales.clsFacultamiento.ValidaFacultades5421_G(Globales.clsFacultamiento.MasivosGral))                        
                            frmConsRemesa.DefInstance.Show();                        
                        break;
                    case "btnPredFolio":
                        // MODIF MAP 2015 FACULTAMIENTO
                        if (Globales.clsFacultamiento.ValidaFacultades5421_G(Globales.clsFacultamiento.MasivosGral))                        
                            frmPredFolio.DefInstance.Show();
                        break;

                    //MIG WXP INI JGC 20090825
                    // SE elimina esta ventana que no se utiliza
                    //case "btnVentasCruzadas":
                    //    this.Cursor = Cursors.Default;
                    //    mdlGlobales.gblnChecaCancela = false;
                    //    frmVentasCruzadas.DefInstance.Show();
                    //    break;
                    //MIG WXP INI JGC 20090825

                    /********************
                     * INFOWARE 09 INICIO
                     ********************/
                    case "btnArribo":                        
                            arriboRemesa();
                        break;
                    case "btnValidacion":                        
                            validacionRemesa();
                        break;
                    case "btnInspeccion":                        
                            inspeccionRemesa();
                        break;
                    case "btnConsulta":
                        consultaRemesa();
                        break;
                    /********************
                    * INFOWARE 09 FIN
                    ********************/
                    case "btnFirma":
                        mnuFirmaS041_Click(mnuFirmaS041, new EventArgs());
                        break;
                    case "btnSalir":
                        this.Cursor = Cursors.Default;
                        this.closing = true;
                        //AIS-1896 FSABORIO
                        if (!this.SuspendFormClosing)
                            this.Close();
                        break;
                }
            }
            else
            {
                string tempRefParam = "NECESITA FIRMARSE AL S041 NUEVAMENTE";
                MsgBoxStyle tempRefParam2 = MsgBoxStyle.Information;
                string tempRefParam3 = "ERROR";
                mdlGlobales.subDespErrores(ref tempRefParam, ref tempRefParam2, ref tempRefParam3);
                mdlGlobales.gblnBandAcess = true;
                mdlGlobales.gblnEstaSeg = MDIMasivos.DefInstance.OleAcceso.FirmarS041();
            }
        }

        public string funEscribeVersion()
        {
            //AIS-1893 FSABORIO
            string strVersion = (new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location)).LastWriteTime.ToString();
            return "Versión: " + Strings.Mid(strVersion, 7, 4) + Strings.Mid(strVersion, 4, 2) + Strings.Mid(strVersion, 1, 2) + "." + Strings.Mid(strVersion, 12, 2) + Strings.Mid(strVersion, 21, 1) + Strings.Mid(strVersion, 23, 1);
            //    Para desarrollo usar la línea siguiente
            //    funEscribeVersion = "Versión 06.01.00"
        }

        public void subCerrarMasivos()
        {
            //UPGRADE_TODO: (1069) Error handling statement (On Error Resume Next) was converted to a complex pattern which might not be equivalent to the original.
            try
            {
                mdlGlobales.gstrDatosEncripta = mdlGlobales.ENCRIP.EDV("**** SALIDA DEL SISTEMA ****");
                mdlGlobales.gstrDatosEncripta3 = mdlGlobales.ENCRIP.EDV("--- FIN DE BITACORA DEL FRONT DE ARIES ---");
                Artinsoft.VB6.Gui.ControlHelper.Print(this, mdlGlobales.gLngArchivoBitacora, mdlGlobales.gstrDatosEncripta + mdlGlobales.gstrDatosEncripta3);
                FileSystem.FileClose(mdlGlobales.gLngArchivoBitacora);
                //AIS-1896 FSABORIO
                if (!this.SuspendFormClosing)
                    this.Close();
            }
            catch (Exception exc)
            {
                throw new Exception("Migration Exception: The following exception could be handled in a different way after the conversion: " + exc.Message);
            }
        }

        /********************
        * INFOWARE 09 INICIO
        ********************/

        frmConsultaRemesas remesaConsulta = new frmConsultaRemesas();
        frmInspeccionRemesas remesaInspeccion = new frmInspeccionRemesas();
        frmValidaRemesas remesaValidacion = new frmValidaRemesas();
        frmArriboRemesas remesaArribo = new frmArriboRemesas();

        private void arriboRemesaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            arriboRemesa();
        }

        private void arriboRemesa()
        {
            // MODIF MAP 2015 FACULTAMIENTO
            if (Globales.clsFacultamiento.ValidaFacultades5421_G(Globales.clsFacultamiento.MasivosGral))
            {
                try
                {
                    //remesaArribo.ShowDialog();
                    remesaArribo.MdiParent = this;
                    remesaArribo.Show();
                }
                catch
                {
                    remesaArribo = new frmArriboRemesas();
                    //remesaArribo.ShowDialog();
                    remesaArribo.MdiParent = this;
                    remesaArribo.Show();
                }
            }
        }

        private void validacionRemesaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            validacionRemesa();
        }

        private void validacionRemesa()
        {
            // MODIF MAP 2015 FACULTAMIENTO
            if (Globales.clsFacultamiento.ValidaFacultades5421_G(Globales.clsFacultamiento.MasivosGral))
            {
                try
                {
                    //remesaValidacion.ShowDialog();
                    remesaValidacion.MdiParent = this;
                    remesaValidacion.Show();
                }
                catch
                {
                    remesaValidacion = new frmValidaRemesas();
                    //remesaValidacion.ShowDialog();
                    remesaValidacion.MdiParent = this;
                    remesaValidacion.Show();
                }
            }
        }
        private void inspeccionRemesaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inspeccionRemesa();
        }

        private void inspeccionRemesa()
        {
            // MODIF MAP 2015 FACULTAMIENTO
            if (Globales.clsFacultamiento.ValidaFacultades5421_G(Globales.clsFacultamiento.MasivosGral))
            {
                try
                {
                    remesaInspeccion.MdiParent = this;
                    remesaInspeccion.Show();
                    //remesaInspeccion.ShowDialog();
                }
                catch
                {
                    remesaInspeccion = new frmInspeccionRemesas();
                    //remesaInspeccion.ShowDialog();
                    remesaInspeccion.MdiParent = this;
                    remesaInspeccion.Show();
                }
            }
        }
        private void consultaRemesaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            consultaRemesa();
        }
        private void consultaRemesa()
        {
            // MODIF MAP 2015 FACULTAMIENTO
            if (Globales.clsFacultamiento.ValidaFacultades5421_G(Globales.clsFacultamiento.MasivosGral))
            {
                try
                {
                    //remesaConsulta.ShowDialog();
                    remesaConsulta.MdiParent = this;
                    remesaConsulta.Show();
                }
                catch
                {
                    remesaConsulta = new frmConsultaRemesas();
                    //remesaConsulta.ShowDialog();
                    remesaConsulta.MdiParent = this;
                    remesaConsulta.Show();
                }
            }
        }
        

        /********************
        * INFOWARE 09 FIN
        ********************/
    }
}