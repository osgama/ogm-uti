using Microsoft.VisualBasic;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using VB6 = Microsoft.VisualBasic.Compatibility.VB6.Support;

namespace Masivos
{
    [ProgId("clsProcMasivo_NET.clsProcMasivo")]
    public class clsProcMasivo
    {

        //*******************************************************************************
        //* Identificaci�n: clsProcMasivo                                               *
        //* Autor:          Alvaro Daniel Salinas Garc�a                                *
        //* Instalaci�n:    PRAXIS                                                      *
        //* Fecha:          29/01/2004                                                  *
        //* Versi�n:        1.0                                                         *
        //*******************************************************************************

        //********************************** Propiedades *************************************
        //Propiedad de s�lo escritura para asignar el objeto de conexi�n a TANDEM
        public COMDRV32.TcpServer Conexion
        {
            set
            {
                mdlComunica.objProxy = value;
            }
        }


        public edvddv.Cedvddv Encripcion
        {
            set
            {
                mdlGlobales.ENCRIP = value;
            }
        }


        //Propiedad para especificar qu� n�mina se est� utilizando para conectarse al sistema

        public string Nomina
        {
            get
            {
                return mdlGlobales.gstrNomina.Value;
            }
            set
            {
                mdlGlobales.gstrNomina.Value = value;
            }
        }


        //Propiedad para especificar el nombre del usuario que se conecta al sistema

        public string NombreUsuario
        {
            get
            {
                return mdlGlobales.gstrNombreUsuario;
            }
            set
            {
                mdlGlobales.gstrNombreUsuario = value;
            }
        }


        //Propiedad que indica si el usuario esta o no conectado al S041

        public bool EstaSeg
        {
            get
            {
                return mdlGlobales.gblnEstaSeg;
            }
            set
            {
                mdlGlobales.gblnEstaSeg = value;
            }
        }



        public string Permisos
        {
            get
            {
                //Falta definir el manejo de los permisos a las aplicaciones
                return String.Empty;
            }
            set
            {
                //Falta definir el manejo de los permisos a las aplicaciones
            }
        }


        //************************************* M�todos **************************************
        private bool ValidaMasivos()
        {
            bool result = false;
            if (mdlComunica.objProxy == null)
            {
                throw new Exception("10001, XMasivos, No se ha iniciado el objeto de conexi�n");
                return result;
            }
            if (Nomina.Trim() == "" || Conversion.Val(Nomina) == 0)
            {
                throw new Exception("10002, XMasivos, Debe especificar la n�mina del usuario que se conecta al sistema");
                return result;
            }
            return true;
        }

        //M�todo para iniciar la aplicaci�n
        private bool InicializaMasivos()
        {
            bool result = false;
            if (!ValidaMasivos())
            {
                return result;
            }
            mdlGlobales.gbolEstaIniciado = true;
            //Abrir las bit�coras de registro
            mdlGlobales.gLngArchivoBitacora = FileSystem.FreeFile();
            mdlGlobales.subAbreBitacora();
            mdlGlobales.subObtenCaracteresInvalidos();
            //Cargar los cat�logos que se cargan en la autentificaci�n
            mdlComunica.OleCatalogos = new Catalogos.clsCatalogos();
            //Preparar el m�dulo de acceso para restringir que nadie diferente del usuario conectado al Shell se firme desde aqu�
            MDIMasivos.DefInstance.OleAcceso.Conexion = mdlComunica.objProxy;
            MDIMasivos.DefInstance.OleAcceso.NominaRequerida = true;
            MDIMasivos.DefInstance.OleAcceso.Nomina = mdlGlobales.gstrNomina.Value;
            return true;
        }

        //M�todo para mostrar el formulario principal, invocado desde el cliente
        public void Mostrar()
        {
            if (!mdlGlobales.gblnEstaSeg)
            {
                throw new Exception("10005, XMasivos, No est� conectado");
                return;
            }
            if (!mdlGlobales.gbolEstaIniciado)
            {
                if (!InicializaMasivos())
                {
                    return;
                }
            }
            MDIMasivos.DefInstance.Show();
        }

        public bool Activo()
        {
            return MDIMasivos.DefInstance.Visible;
        }

        public bool EnviaFolioPreDictaminacion(ref  string strTramite, string strFolioPreimpreso)
        {
            bool result = false;
            object tempRefParam = "btnPredFolio";
            if (!MDIMasivos.DefInstance.tlbMasivos.Buttons.get_ControlDefault(ref tempRefParam).Enabled)
            {
                return result;
            }
            object tempRefParam2 = "btnPredFolio";
            if (!MDIMasivos.DefInstance.tlbMasivos.Buttons.get_ControlDefault(ref tempRefParam2).Visible)
            {
                return result;
            }
            //UPGRADE_TODO: (1067) Member DefInstance is not defined in type MDIMasivos.
            MDIMasivos.DefInstance.mnuConsFolio_Click(MDIMasivos.DefInstance.mnuConsFolio, new EventArgs());
            frmPredFolio.DefInstance.txtFolioPreimpreso.Text = strFolioPreimpreso;
            frmPredFolio.DefInstance.cboTipoTram.SelectedIndex = mdlComunica.OleCatalogos.ObtenIdxCombo(frmPredFolio.DefInstance.cboTipoTram, strTramite);
            //UPGRADE_TODO: (1067) Member DefInstance is not defined in type frmPredFolio.
            int tempRefParam3 = 13;
            frmPredFolio.DefInstance.txtFolioPreimpreso_KeyPress(frmPredFolio.DefInstance.txtFolioPreimpreso, new KeyPressEventArgs(Convert.ToChar(tempRefParam3)));
            return true;
        }

        //M�todo para cerrar la aplicaci�n y terminar el proceso
        private void Terminar()
        {
            MDIMasivos.DefInstance.subCerrarMasivos(); //Cierra todos los archivos abiertos
            mdlGlobales.gstrTramite.Value = "";
            mdlGlobales.gbolEstaIniciado = false;
        }

        public clsProcMasivo()
        {
            //Inicializaci�n de las variables globales de control del sistema
            mdlGlobales.gblnEstaSeg = false;
            mdlGlobales.gbolEstaIniciado = false;
            mdlGlobales.gstrNomina.Value = "";
            mdlComunica.objProxy = null;
        }
        //AIS-1770 mjimenez
        public void Dispose()
        {
            if (mdlGlobales.gbolEstaIniciado)
            {
                Terminar();
            }
        }
    }
}
