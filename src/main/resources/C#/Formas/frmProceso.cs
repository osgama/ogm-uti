using Artinsoft.VB6.Utils;
using System;
using System.IO;
using System.Windows.Forms;
using VB6 = Microsoft.VisualBasic.Compatibility.VB6.Support;
//MIG WXP INI JGC 20090825
using Microsoft.VisualBasic;
//MIG WXP FIN JGC 20090825

namespace Masivos
{
    internal partial class frmProceso
        : System.Windows.Forms.Form
    {

        //*******************************************************************************
        //* Identificación: Proceso                                                     *
        //* Autor:                                                                      *
        //* Instalación:    PRAXIS                                                      *
        //* Fecha:          15/09/2003                                                  *
        //* Versión:        1.0                                                         *
        //*******************************************************************************


        //*****************************************************************************************************
        //* Finalidad:  Subrutina para preparar la pantalla que indica la ejecución de un proceso
        //****************************************************************************************************
        //UPGRADE_WARNING: (1041) Form_Load event was upgraded to Form_Load method and has a new behavior.
        private void Form_Load()
        {
            mdlGlobales.CentrarMDIHija(MDIMasivos.DefInstance, frmProceso.DefInstance);
            switch (mdlComunica.strQueTramite)
            {
                case "tlbDictamina":
                    this.Text = "Transacción en Proceso, por favor espere..";
                    break;
                default:
                    this.Text = mdlComunica.strQueTramite;
                    break;
            }
            ToolTip1.SetToolTip(aniTransfer, this.Text);
            //AIS-1893 FSABORIO
            //MIG WXP INI JGC 20090825
            string strMASArchivo1 = mdlRegistry.RegistryMasivos("MASArchivo1");
            if (FileSystem.Dir(mdlMain.ApplicationPath + strMASArchivo1, FileAttribute.Normal) == "")
            {
                //File.Copy(Path.GetDirectoryName(Application.ExecutablePath) + "\\Masivos\\Masivos\\Filecopy.avi", mdlMain.ApplicationPath + strMASArchivo1 + "\\Filecopy.avi");
                //File.Copy("C:\\Archivos de Programa\\Banamex\\C753_002\\Masivos" + "\\Filecopy.avi", mdlMain.ApplicationPath + strMASArchivo1);
                File.Copy(Path.GetDirectoryName(Application.ExecutablePath) + "\\Masivos\\Filecopy.avi", mdlMain.ApplicationPath + strMASArchivo1);
                
            }

            //MIG WXP FIN JGC 20090825
            //aniTransfer.Open(mdlMain.ApplicationPath + "\\Filecopy.avi");
            aniTransfer.Open(mdlMain.ApplicationPath + strMASArchivo1);
        }
        private void frmProceso_Closed(Object eventSender, EventArgs eventArgs)
        {
            MemoryHelper.ReleaseMemory();
        }

        //AIS-1896 FSABORIO
        public bool SuspendFormClosing = false;
        private void frmProceso_FormClosing(object sender, FormClosingEventArgs eventArgs)
        {
            //AIS-1896 FSABORIO
            SuspendFormClosing = true;
        }
    }
}