using Artinsoft.VB6.Utils;
using Microsoft.VisualBasic;
using System;
using System.IO;
using System.Windows.Forms;
using VB6 = Microsoft.VisualBasic.Compatibility.VB6.Support;

namespace Masivos
{
    
    class mdlICEPIntelar
    {

        //MIG WXP INI JGC 20090825
        static public string strMASRuta2 = mdlRegistry.RegistryMasivos("MASRuta2");
        static public string strMASCLCCmd;
        static public string strMASCLCEquipo;
        static public string strMASCLCUsuario;
        static public string strMASCLCBuzon;
        static public string strMASICEPCmd;
        static public string strMASICEPEquipo;
        static public string strMASICEPUsuario;
        static public string strMASICEPBuzon;
        static public string strMASModo;
        //Enrique Ramírez Montes: 20131217, Declaración de variables para incluir Tectia
        static public string strMASTECTIACmd;
        static public string strMASTECTIAEquipo;

        //MIG WXP FIN JGC 20090825

        //MIG WXP INI JGC 20090825
        // SI YA NO SE VA A USAR EL ARCHIVO INI TAMPOCO ESTA LINEA
        //static public Globales.clsINIFile oleArchivoIni = null;
        //MIG WXP FIN JGC 20090825

        static public bool blnCLC_ProcesaEnvioREMESA(string strArchivo)
        {
            //Cambio realizado por Enrique Ramírez Montes
            //El primer medio de envío es TECTIA en caso contrario CLC y sólamente se envía por un medio
            bool result = false;
            //if (!blnICEP_EnviaArchivosREMESA(strArchivo))
            //if (bln_EnviaArchivosREMESA_ICEP_CLC(strArchivo, "TECTIA"))
            if(bln_EnviaArchivosREMESA_TECTIA_CLC(strArchivo, "TECTIA"))
                result = true;
                //DCL 20131217 se incluye meto de envio tectia tectia 
            else if (bln_EnviaArchivosREMESA_TECTIA_CLC(strArchivo, "CLC"))
                result = true;
            return result;
        }

        static public bool blnICEP_ProcesaEnvioREMESA(string strArchivo)
        {
            bool result = false;
            //MIG WXP INI JGC 20090825
            //if (blnICEP_AbreArchivoINI())
            //{
            //if (!blnICEP_EnviaArchivosREMESA(strArchivo))
            if (!bln_EnviaArchivosREMESA_ICEP_CLC(strArchivo, "ICEP"))
            {
                //MIG WXP INI JGC 20090825
                // ESTE MENSAJE NO DEBERIA ENVIARLO POR QUE VA A REINTENTAR CON FTP
                //MessageBox.Show("REINTENTE LA OPERACION", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //Application.DoEvents();
                //MIG WXP FIN JGC 20090825

                //MIG WXP INI JGC 20090825
                //COMO NUNCA REGRESABA ERROR ENTONCES NUNCA INTENTABA CON FTP, SOLO SIEMPRE POR ICEP
                result = false;
                //MIG WXP FIN JGC 20090825
            }
            else
            {
                //MessageBox.Show("ARCHIVOS ENVIADOS", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //MessageBox.Show("ARCHIVOS ENVIADOS (ICEP)", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                result = true;
            }
            //}
            //MIG WXP FIN JGC 20090825
            return result;
        }

        //MIG WXP INI JGC 20090825
        //static public bool blnICEP_ProcesaEnvioVCRUZADA(ref  string strArchivo)
        //{
        //    bool result = false;
        //    if (blnICEP_AbreArchivoINI())
        //    {
        //        if (!blnICEP_EnviaArchivosVCRUZADA(ref strArchivo))
        //        {
        //            MessageBox.Show("REINTENTE LA OPERACION", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //        }
        //        else
        //        {
        //            MessageBox.Show("ARCHIVOS ENVIADOS", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            result = true;
        //        }
        //    }
        //    return result;
        //}
        //MIG WXP FIN JGC 20090825

        //MIG WXP INI JGC 20090825
        //static private bool blnICEP_AbreArchivoINI()
        //{
        //    oleArchivoIni = new Globales.clsINIFile();
        //    //AIS-1893 FSABORIO
        //    oleArchivoIni.Path = mdlMain.ApplicationPath;
        //    //AIS-909 ogarcia
        //    oleArchivoIni.FileName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + ".ini";
        //    if (!oleArchivoIni.ObtenArchivo())
        //    {
        //        MessageBox.Show("NO FUE POSIBLE ABRIR EL ARCHIVO DE CONFIGURACION DE LA APLICACION", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        //        //Intenta crear el archivo de parámetros
        //        if (!mdlGlobales.funCreaMasivosIni())
        //        {
        //            MessageBox.Show("FSQ1= " + oleArchivoIni.Path + ": " + oleArchivoIni.FileName);

        //            //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
        //            string tempRefParam = "NO SE ENCUENTRA ARCHIVO DE PARÁMETROS Y NO PUDO SER CREADO (ENVIO INTELAR)";
        //            MsgBoxStyle tempRefParam2 = (MsgBoxStyle)(((int)MsgBoxStyle.Critical) + ((int)MsgBoxStyle.OkOnly));
        //            mdlGlobales.subDespErrores(ref tempRefParam, ref tempRefParam2);
        //        }

        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}
        //MIG WXP FIN JGC 20090825

        //static private bool blnICEP_ValidaParametros()
        //{
        //    bool result = false;
        //    if (!oleArchivoIni.Sections.ExisteSection("INTELAR"))
        //    {
        //        MessageBox.Show("NO ESTA DEFINIDA LA SECCION CORRESPONDIENTE A LOS PARAMETROS DE INTELAR EN EL ARCHIVO INI", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //        return result;
        //    }
        //    else
        //    {
        //        //Valida parametros
        //        //Valida el server de conexion
        //        if (!oleArchivoIni.Sections["INTELAR"].Keys.ExisteKey("COMANDO_ICEP"))
        //        {
        //            MessageBox.Show("NO ESTA DEFINIDO EL COMANDO ICEP", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //            return result;
        //        }
        //        //Valida el puerto
        //        if (!oleArchivoIni.Sections["INTELAR"].Keys.ExisteKey("EQUIPO"))
        //        {
        //            MessageBox.Show("NO ESTA DEFINIDO EL EQUIPO DE CONEXION", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //            return result;
        //        }
        //        //Valida el usuario
        //        if (!oleArchivoIni.Sections["INTELAR"].Keys.ExisteKey("USUARIO"))
        //        {
        //            MessageBox.Show("NO ESTA DEFINIDO EL USUARIO DE INTELAR EN EL ARCHIVO DE CONFIGURACION", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //            return result;
        //        }
        //        //Valida el password
        //        if (!oleArchivoIni.Sections["INTELAR"].Keys.ExisteKey("BUZON"))
        //        {
        //            MessageBox.Show("NO ESTA DEFINIDO EL BUZON DE ARRIBO A INTELAR", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //            return result;
        //        }
        //        //Valida el modo de envio
        //        if (!oleArchivoIni.Sections["INTELAR"].Keys.ExisteKey("MODO"))
        //        {
        //            MessageBox.Show("NO ESTA DEFINIDO EL BUZON DE ARRIBO A INTELAR", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //            return result;
        //        }
        //        else
        //        {
        //            if ((("01,02").IndexOf(StringsHelper.Format(Conversion.Val(oleArchivoIni.Sections["INTELAR"].Keys["MODO"].Valor), "00")) + 1) == 0)
        //            {
        //                MessageBox.Show("EL MODO DE ENVIO DEBE SER ASCII (1) O BINARIO (2)", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //                return result;
        //            }
        //        }
        //    }
        //    return true;
        //}
        //static private bool blnICEP_ValidaParametrosICEP()
        static private bool bln_ValidaParametros_ICEP_CLC(string indicaTipoEnvio)
        {
            bool result = false;
            //DCL 20131217 se incluye tectia en el modo de transaferencia de archivos ini
            if (indicaTipoEnvio == "TECTIA")
            {
                strMASTECTIACmd = mdlRegistry.RegistryMasivos("MASTECTIACmd");
                if (strMASCLCCmd == "")
                {
                    MessageBox.Show("NO ESTA DEFINIDO EL COMANDO TECTIA", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return result;
                }
                //Valida el Equipo
                strMASTECTIAEquipo = mdlRegistry.RegistryMasivos("MASTECTIAEquipo");
                if (strMASICEPEquipo == "")
                {
                    MessageBox.Show("NO ESTA DEFINIDO EL EQUIPO DE CONEXION TECTIA", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return result;
                }
                //Valida el usuario
                strMASCLCUsuario = mdlRegistry.RegistryMasivos("MASCLCUsuario");
                if (strMASCLCUsuario == "")
                {
                    MessageBox.Show("NO ESTA DEFINIDO EL USUARIO DE INTELAR EN EL ARCHIVO DE CONFIGURACION", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return result;
                }
                //Valida el password
                strMASCLCBuzon = mdlRegistry.RegistryMasivos("MASCLCBuzon");
                if (strMASCLCBuzon == "")
                {
                    MessageBox.Show("NO ESTA DEFINIDO EL BUZON DE ARRIBO A INTELAR", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return result;
                }
            }
            else if (indicaTipoEnvio == "CLC")
            {
                //CUANDO ES CLC
                //Valida parametros
                //Valida el server de conexion

                strMASCLCCmd = mdlRegistry.RegistryMasivos("MASCLCCmd");
                if (strMASCLCCmd == "")
                {
                    MessageBox.Show("NO ESTA DEFINIDO EL COMANDO ICEP", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return result;
                }
                //Valida el Equipo
                strMASCLCEquipo = mdlRegistry.RegistryMasivos("MASCLCEquipo");
                if (strMASICEPEquipo == "")
                {
                    MessageBox.Show("NO ESTA DEFINIDO EL EQUIPO DE CONEXION", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return result;
                }
                //Valida el usuario
                strMASCLCUsuario = mdlRegistry.RegistryMasivos("MASCLCUsuario");
                if (strMASCLCUsuario == "")
                {
                    MessageBox.Show("NO ESTA DEFINIDO EL USUARIO DE INTELAR EN EL ARCHIVO DE CONFIGURACION", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return result;
                }
                //Valida el password
                strMASCLCBuzon = mdlRegistry.RegistryMasivos("MASCLCBuzon");
                if (strMASCLCBuzon == "")
                {
                    MessageBox.Show("NO ESTA DEFINIDO EL BUZON DE ARRIBO A INTELAR", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return result;
                }
             }
             else
             {
                 //CUANDO ES ICEP
                 //Valida parametros
                 //Valida el server de conexion

                 strMASICEPCmd = mdlRegistry.RegistryMasivos("MASICEPCmd");
                 if (strMASICEPCmd == "")
                 {
                     MessageBox.Show("NO ESTA DEFINIDO EL COMANDO ICEP", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                     return result;
                 }
                 //Valida el Equipo
                 strMASICEPEquipo = mdlRegistry.RegistryMasivos("MASICEPEquipo");
                 if (strMASICEPEquipo == "")
                 {
                     MessageBox.Show("NO ESTA DEFINIDO EL EQUIPO DE CONEXION", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                     return result;
                 }
                 //Valida el usuario
                 strMASICEPUsuario = mdlRegistry.RegistryMasivos("MASICEPUsuario");
                 if (strMASICEPUsuario == "")
                 {
                     MessageBox.Show("NO ESTA DEFINIDO EL USUARIO DE INTELAR EN EL ARCHIVO DE CONFIGURACION", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                     return result;
                 }
                 //Valida el password
                 strMASICEPBuzon = mdlRegistry.RegistryMasivos("MASICEPBuzon");
                 if (strMASICEPBuzon == "")
                 {
                     MessageBox.Show("NO ESTA DEFINIDO EL BUZON DE ARRIBO A INTELAR", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                     return result;
                 }
            }
            //Valida el modo de envio
            strMASModo = mdlRegistry.RegistryMasivos("MASModo");
            if (strMASModo == "")
            {
                 MessageBox.Show("NO ESTA DEFINIDO EL BUZON DE ARRIBO A INTELAR", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                 return result;
            }
            else
            {
                result = true;
                return result;
            }
        }

        static private bool bln_EnviaArchivosREMESA_TECTIA_CLC(String strArchivo, String indicaTipoEnvio)
        {            
            Globales.clsICEP oleICEP = new Globales.clsICEP();
            bool blnRegreso = false;
            string strRutaBitacora = String.Empty;
            string strCadMensaje = String.Empty;
            string strCadEstatus = String.Empty; //Indicador de que el estatus fue cambiado
            string strArchivoEnviar = String.Empty;
            string intEnviaArchivo = String.Empty;

            string strDespErr = ""; //>> MR 13/Sep/2005  variable para almacenar el mensaje de la remesa
            
            mdlGlobales.subDespMensajes("PREPARANDO ENVIO, FAVOR DE ESPERAR...");

            oleICEP.Usuario = strMASCLCUsuario = mdlRegistry.RegistryMasivos("MASCLCUsuario");
            oleICEP.Buzon = strMASCLCBuzon = mdlRegistry.RegistryMasivos("MASCLCBuzon");
            if (indicaTipoEnvio == "TECTIA")
            {
                oleICEP.ExeCommand = strMASTECTIACmd = mdlRegistry.RegistryMasivos("MASTECTIACmd");
                oleICEP.Equipo = strMASTECTIAEquipo = Globales.clsFuncionesShell.subObtieneIndicadorAmbiente(indicaTipoEnvio);            
            }
            if (indicaTipoEnvio == "CLC")
            {
                oleICEP.ExeCommand = strMASCLCCmd = mdlRegistry.RegistryMasivos("MASCLCCmd");
                oleICEP.Equipo = strMASCLCEquipo = Globales.clsFuncionesShell.subObtieneIndicadorAmbiente(indicaTipoEnvio);                
            }
            
            oleICEP.ModoEnvio = (Globales.clsICEP.udtModosEnvio)Convert.ToInt32(Conversion.Val(mdlRegistry.RegistryMasivos("MASModo")));
            string strMASRutaAplicacion = mdlRegistry.RegistryMasivos("MASRutaAplicacion");
            strRutaBitacora = mdlRegistry.RegistryMasivos("MASRutaBitacora");

            mdlGlobales.subDespMensajes("ENVIANDO ARCHIVO " + strArchivo.Substring(strArchivo.Length - Math.Min(strArchivo.Length, 26)) + ", POR FAVOR ESPERE...");
            oleICEP.ArchivoOrigen = strArchivo;
            
            oleICEP.ArchivoDestino = mdlRegistry.RegistryMasivos("MASCLCPrefijoArcDestino") + Strings.Mid(strArchivo, strArchivo.Length - 11, 8);

            strCadMensaje = DateTime.Today.ToString("dd/MM/yyyy") + " " + DateTime.Now.AddDays(-DateTime.Now.Date.ToOADate()).ToString("HH:mm");

            if (oleICEP.EjecutaICEP(indicaTipoEnvio))
            {
                //Ya que fue aceptada la transferencia hay que cambiar el estatus de la remesa a 02
                strCadMensaje = strCadMensaje + " ACEPTADO  -> ";
                if (mdlTranMasivo.funCambiaEstatusRemesa(frmProcMasivo.DefInstance.txtRemesa.Text.Substring(frmProcMasivo.DefInstance.txtRemesa.Text.Length - Math.Min(frmProcMasivo.DefInstance.txtRemesa.Text.Length, 10)), frmProcMasivo.DefInstance.txtArchivo.Text, mdlTranMasivo.gcstrEstatusRemesa))
                {
                    strCadEstatus = "ESTATUS DE LA REMESA: GENERADA";
                    strDespErr = "LA REMESA " + frmProcMasivo.DefInstance.txtRemesa.Text + " HA SIDO GENERADA SATISFACTORIAMENTE";
                    //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                    string tempRefParam3 = "LA REMESA " + frmProcMasivo.DefInstance.txtRemesa.Text + " HA SIDO GENERADA SATISFACTORIAMENTE";
                    MsgBoxStyle tempRefParam4 = (MsgBoxStyle)(((int)MsgBoxStyle.Information) + ((int)MsgBoxStyle.OkOnly));
                    mdlGlobales.subDespErrores(ref tempRefParam3, ref tempRefParam4);
                    blnRegreso = true;
                }
                else
                {
                    strCadEstatus = "ESTATUS DE LA REMESA: NO PUDO SER ACTUALIZADO EL ESTATUS DE LA REMESA";
                    strDespErr = "NO SE PUDO GENERAR LA REMESA";
                    //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                    string tempRefParam5 = "NO SE PUDO GENERAR LA REMESA";
                    MsgBoxStyle tempRefParam6 = (MsgBoxStyle)(((int)MsgBoxStyle.Exclamation) + ((int)MsgBoxStyle.OkOnly));
                    mdlGlobales.subDespErrores(ref tempRefParam5, ref tempRefParam6);
                }
                frmProcMasivo.DefInstance.subLimpiarDatos();
            }
            else
                blnRegreso = false;

            String archivoBitacora = mdlRegistry.RegistryMasivos("MASRutaAplicacion") + strRutaBitacora + strMASRuta2 + mdlTranMasivo.gvstrFechaProceso + ".txt";
            strCadMensaje += strCadEstatus;
            intEnviaArchivo = FileSystem.FreeFile().ToString();
            FileSystem.FileOpen(Int32.Parse(intEnviaArchivo), archivoBitacora, OpenMode.Append, OpenAccess.Default, OpenShare.Default, -1);
            FileSystem.PrintLine(Int32.Parse(intEnviaArchivo), strCadMensaje);
            FileSystem.FileClose(Int32.Parse(intEnviaArchivo));
            mdlComunica.gvMensaje = strDespErr + " Tipo de proceso de declinación: " + mdlCatalogos.gstrCatProceso + " Con clave de declinación: " + mdlGlobales.gstrClaveDeclina +
                " Ambiente Envio: " + oleICEP.Equipo;
            mdlGlobales.subRegBitacora("E");

            oleICEP = null;
            return blnRegreso;
        }
        
        static private bool bln_EnviaArchivosREMESA_ICEP_CLC(string strArchivo, string indicaTipoEnvio)
        {
            bool result = false;
            Globales.clsICEP oleICEP = null;
            bool blnRegreso = false;
            string strRutaBitacora = String.Empty;
            string strCadMensaje = String.Empty;
            string strCadEstatus = String.Empty; //Indicador de que el estatus fue cambiado
            string strArchivoEnviar = String.Empty;
            string intEnviaArchivo = String.Empty;

            string strDespErr = ""; //>> MR 13/Sep/2005  variable para almacenar el mensaje de la remesa

            try
            {
                blnRegreso = true;
                mdlGlobales.subDespMensajes("PREPARANDO ENVIO, FAVOR DE ESPERAR...");

                //Busca los parametros de conexion a INTELAR, si no los encuentra aborta la rutina
                //Globales.clsSection withVar = oleArchivoIni.Sections["INTELAR"];
                //if (blnICEP_ValidaParametrosICEP())
                if (bln_ValidaParametros_ICEP_CLC(indicaTipoEnvio))
                {
                    //Crea el objeto de ICEP
                    oleICEP = new Globales.clsICEP();
                    //Obtiene los comandos de envio por ICEP a INTELAR

                    //MIG WXP INI JGC 20090825
                    //mdlGlobales.subDespMensajes("LEYENDO PARAMETROS DE ENVIO...");

                    //oleICEP.ExeCommand = withVar.Keys["COMANDO_ICEP"].Valor;
                    //oleICEP.Equipo = withVar.Keys["EQUIPO"].Valor;
                    //oleICEP.Usuario = withVar.Keys["USUARIO"].Valor;
                    //oleICEP.Buzon = withVar.Keys["BUZON"].Valor;
                    //if (indicaTipoEnvio == "ICEP")
                    //{
                    //    //POR ICEP
                    //    oleICEP.ExeCommand = strMASICEPCmd;
                    //    oleICEP.Equipo = strMASICEPEquipo;
                    //    oleICEP.Usuario = strMASICEPUsuario;
                    //    oleICEP.Buzon = strMASICEPBuzon;
                    //}
                    //    //DCL 20131217 se incluye meto de envio de TECTIA
                    //else if (indicaTipoEnvio == "TECTIA")
                    if (indicaTipoEnvio == "TECTIA")
                    {
                        oleICEP.ExeCommand = strMASTECTIACmd;
                        oleICEP.Equipo = strMASTECTIAEquipo;
                        oleICEP.Usuario = strMASCLCUsuario;
                        oleICEP.Buzon = strMASCLCBuzon;
                    }
                    if (indicaTipoEnvio == "CLC")
                    {
                        //POR CLC
                        oleICEP.ExeCommand = strMASCLCCmd;
                        oleICEP.Equipo = strMASCLCEquipo;
                        oleICEP.Usuario = strMASCLCUsuario;
                        oleICEP.Buzon = strMASCLCBuzon;
                    }

                    //MIG WXP FIN JGC 20090825
                    //AIS-96 rvillalta
                    //oleICEP.Comando = Globales.udtComandos.intEnviar;
                    //UPGRADE_WARNING: (6021) Casting 'double' to Enum may cause different behaviour.

                    //MIG WXP INI JGC 20090825
                    //oleICEP.ModoEnvio = (Globales.clsICEP.udtModosEnvio)Convert.ToInt32(Conversion.Val(withVar.Keys["MODO"].Valor));
                    oleICEP.ModoEnvio = (Globales.clsICEP.udtModosEnvio)Convert.ToInt32(Conversion.Val(strMASModo));
                    //strRutaBitacora = withVar.Keys["RUTA_BITACORA"].Valor;
                    string strMASRutaAplicacion = mdlRegistry.RegistryMasivos("MASRutaAplicacion");
                    strRutaBitacora = mdlRegistry.RegistryMasivos("MASRutaBitacora");
                    //strRutaBitacora = strMASRutaAplicacion + strRutaBitacora;
                    //MIG WXP FIN JGC 20090825

                    //-----------------------------
                    //Envia el archivo por INTELAR
                    //-----------------------------
                    strArchivoEnviar = strArchivo;
                    mdlGlobales.subDespMensajes("ENVIANDO ARCHIVO " + strArchivoEnviar.Substring(strArchivoEnviar.Length - Math.Min(strArchivoEnviar.Length, 26)) + ", POR FAVOR ESPERE...");
                    oleICEP.ArchivoOrigen = strArchivoEnviar;

                    //Modif JGC 24/02/2010

                    //oleICEP.ArchivoDestino = strArchivoEnviar.Substring(strArchivoEnviar.Length - Math.Min(strArchivoEnviar.Length, 26));
                    string strMASCLCPrefijoArcDestino = mdlRegistry.RegistryMasivos("MASCLCPrefijoArcDestino");

                    if (indicaTipoEnvio == "ICEP")
                    {
                        //oleICEP.ArchivoDestino = strMASCLCPrefijoArcDestino + strArchivoEnviar.Substring(strArchivoEnviar.Length - Math.Min(strArchivoEnviar.Length, 26));
                        oleICEP.ArchivoDestino = strArchivoEnviar.Substring(strArchivoEnviar.Length - Math.Min(strArchivoEnviar.Length, 26));
                    }
                    else
                    {
                        oleICEP.ArchivoDestino = strMASCLCPrefijoArcDestino + Strings.Mid(strArchivoEnviar, strArchivoEnviar.Length - 11, 8);
                    }

                    strCadMensaje = DateTime.Today.ToString("dd/MM/yyyy") + " " + DateTime.Now.AddDays(-DateTime.Now.Date.ToOADate()).ToString("HH:mm");

                    if (!oleICEP.EjecutaICEP(indicaTipoEnvio))
                    {
                        //MIG WXP INI JGC 20090825
                        // ESTE MENSAJE SE ELIMINA YA QUE POSTERIORMENTE VA A INTENTAR VIA FTP EN AUTOMATICO ANTES DE AVISAR QUE NO SE PUDO TRANSFERIR

                        //MessageBox.Show("NO PUDO SER ENVIADO EL ARCHIVO POR INTELAR, FAVOR DE VERIFICAR EL SERVICIO: " + oleICEP.DescripcionError, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //strCadMensaje = strCadMensaje + " RECHAZADO -> ";
                        //strCadEstatus = "ESTATUS DE LA REMESA: NO SE CAMBIO EL ESTATUS DE LA REMESA";
                        //strDespErr = "NO SE PUDO GENERAR LA REMESA -PROB. DE ENVIO A INTELAR- REVISAR MENSAJES EN DIRECTORIO: ";
                        //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                        //string tempRefParam = "NO SE PUDO GENERAR LA REMESA -PROB. DE ENVIO A INTELAR- REVISAR MENSAJES EN DIRECTORIO: " + strRutaBitacora;
                        //MsgBoxStyle tempRefParam2 = (MsgBoxStyle)(((int)MsgBoxStyle.Exclamation) + ((int)MsgBoxStyle.OkOnly));
                        //mdlGlobales.subDespErrores(ref tempRefParam, ref tempRefParam2);
                        //strCadMensaje = strCadMensaje + " ERROR: " + oleICEP.CodigoError.ToString() + ". " + oleICEP.DescripcionError;

                        blnRegreso = false;

                        //MIG WXP FIN JGC 20090825
                    }
                    else
                    {
                        //Ya que fue aceptada la transferencia hay que cambiar el estatus de la remesa a 02
                        strCadMensaje = strCadMensaje + " ACEPTADO  -> ";
                        if (mdlTranMasivo.funCambiaEstatusRemesa(frmProcMasivo.DefInstance.txtRemesa.Text.Substring(frmProcMasivo.DefInstance.txtRemesa.Text.Length - Math.Min(frmProcMasivo.DefInstance.txtRemesa.Text.Length, 10)), frmProcMasivo.DefInstance.txtArchivo.Text, mdlTranMasivo.gcstrEstatusRemesa))
                        {
                            strCadEstatus = "ESTATUS DE LA REMESA: GENERADA";
                            strDespErr = "LA REMESA " + frmProcMasivo.DefInstance.txtRemesa.Text + " HA SIDO GENERADA SATISFACTORIAMENTE";
                            //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                            string tempRefParam3 = "LA REMESA " + frmProcMasivo.DefInstance.txtRemesa.Text + " HA SIDO GENERADA SATISFACTORIAMENTE";
                            MsgBoxStyle tempRefParam4 = (MsgBoxStyle)(((int)MsgBoxStyle.Information) + ((int)MsgBoxStyle.OkOnly));
                            mdlGlobales.subDespErrores(ref tempRefParam3, ref tempRefParam4);
                        }
                        else
                        {
                            strCadEstatus = "ESTATUS DE LA REMESA: NO PUDO SER ACTUALIZADO EL ESTATUS DE LA REMESA";
                            strDespErr = "NO SE PUDO GENERAR LA REMESA";
                            //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                            string tempRefParam5 = "NO SE PUDO GENERAR LA REMESA";
                            MsgBoxStyle tempRefParam6 = (MsgBoxStyle)(((int)MsgBoxStyle.Exclamation) + ((int)MsgBoxStyle.OkOnly));
                            mdlGlobales.subDespErrores(ref tempRefParam5, ref tempRefParam6);
                        }
                    }
                    if (blnRegreso)
                        frmProcMasivo.DefInstance.subLimpiarDatos();

                    //MIG WXP INI JGC 20090825
                    //strArchivo = strRutaBitacora + "\\FTP_" + mdlTranMasivo.gvstrFechaProceso + ".txt";
                    //MIG WXP INI JGC 20090825
                    strMASRutaAplicacion = mdlRegistry.RegistryMasivos("MASRutaAplicacion");
                    strArchivo = strMASRutaAplicacion + strRutaBitacora + strMASRuta2 + mdlTranMasivo.gvstrFechaProceso + ".txt";
                    //MIG WXP FIN JGC 20090825

                    //MIG WXP INI JGC 20090825
                    //YA NO DEBE CHECAR SI EXISTE EL ARCHIVO INI
                    //if (FileSystem.Dir(mdlGlobales.gcStrMasivosIni, FileAttribute.Normal) == "")
                    //{
                    //    //MIG WXP INI JGC 20090825
                    //    //no deberia volver a planchar la ruta de la bitacora, ya la obtuvo arriba
                    //    //strArchivo = "\\FTP_" + mdlTranMasivo.gvstrFechaProceso + ".txt";
                    //strArchivo = strMASRuta2 + mdlTranMasivo.gvstrFechaProceso + ".txt";
                    //    //MIG WXP FIN JGC 20090825
                    //}

                    strCadMensaje = strCadMensaje + strCadEstatus;
                    intEnviaArchivo = FileSystem.FreeFile().ToString();
                    FileSystem.FileOpen(Int32.Parse(intEnviaArchivo), strArchivo, OpenMode.Append, OpenAccess.Default, OpenShare.Default, -1);
                    FileSystem.PrintLine(Int32.Parse(intEnviaArchivo), strCadMensaje);
                    FileSystem.FileClose(Int32.Parse(intEnviaArchivo));

                    //>> MR 13/Sep/2005 Envío a bitácora estatus de la remesa y de proceder causa de rechazo
                    mdlComunica.gvMensaje = strDespErr + " Tipo de proceso de declinación: " + mdlCatalogos.gstrCatProceso + " Con clave de declinación: " + mdlGlobales.gstrClaveDeclina;
                    mdlGlobales.subRegBitacora("E");
                    //>> MR 13/Sep/2005 Si la remesa tiene causa de rechazo no la borro
                    if (mdlCatalogos.gstrCatProceso == "DR" || mdlCatalogos.gstrCatProceso == "DF")
                    {
                    }
                    else
                    {
                        //MIG WXP INI JGC 20090825
                        //QUE LO BORRE SOLO SI ESTA EN LA SEGUNADA VUELTA CON ICEP, POR QUE SI LO BORRA DESDE LA 1RA VUELTA ENTONCES YA NO PUEDE ENVIAR CON ICEP
                        //QUE YA NO LO BORRE PARA QUE LO PUEDA INTENTAR VIA FTP, PERO HAY QUE ASEGURARSE QUE DESPUES DE FTP SI SE BORRE
                        //File.Delete(strArchivoEnviar);
                        //if (indicaTipoEnvio == "ICEP")
                        //{
                        //    File.Delete(strArchivoEnviar);
                        //}
                        //MIG WXP FIN JGC 20090825
                    }
                    mdlCatalogos.gstrCatProceso = "";
                    mdlGlobales.subDespMensajes("");

                    //Destruye el ICEP
                    if (oleICEP != null)
                    {
                        oleICEP.Dispose();
                        oleICEP = null;
                    }
                } //ValidaParametros

                if (!blnRegreso)
                { //Terminar todas las variables si estaban activas
                    //Destruye el ICEP
                    if (oleICEP != null)
                    {
                        oleICEP.Dispose();
                        oleICEP = null;
                    }
                }
                result = blnRegreso;
                mdlGlobales.subDespMensajes("LISTO");
                return result;
            }
            catch (Exception excep)
            {


                if (Information.Err().Number == 5)
                {
                    //UPGRADE_TODO: (1065) Error handling statement (Resume Next) could not be converted properly. A throw statement was generated instead.
                    throw new Exception("Migration Exception: 'Resume Next' not supported");
                }
                mdlGlobales.subDespMensajes("LISTO PERO CON ERRORES");
                oleICEP = null;
                if (Information.Err().Number != 99999)
                {
                    MessageBox.Show("ERROR AL ENVIAR ARCHIVO: " + Information.Err().Number.ToString() + "-" + excep.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                return result;
            }
        }


        //MIG WXP INI JGC 20090825
        //static private bool blnICEP_EnviaArchivosVCRUZADA(ref  string strArchivo)
        //{
        //    bool result = false;
        //    Globales.clsICEP oleICEP = null;
        //    bool blnRegreso = false;
        //    string strRutaBitacora = String.Empty;
        //    string strCadMensaje = String.Empty;
        //    string strCadEstatus = String.Empty; //Indicador de que el estatus fue cambiado
        //    string strArchivoEnviar = String.Empty;
        //    string intEnviaArchivo = String.Empty;

        //    string strDespErr = ""; //>> MR 13/Sep/2005  variable para almacenar el mensaje de la remesa

        //    try
        //    {

        //        blnRegreso = true;
        //        mdlGlobales.subDespMensajes("PREPARANDO ENVIO, FAVOR DE ESPERAR...");

        //        //Busca los parametros de conexion a INTELAR, si no los encuentra aborta la rutina
        //        Globales.clsSection withVar = oleArchivoIni.Sections["INTELAR"];
        //        if (blnICEP_ValidaParametros())
        //        {
        //            //Crea el objeto de ICEP
        //            oleICEP = new Globales.clsICEP();
        //            //Obtiene los comandos de envio por ICEP a INTELAR
        //            mdlGlobales.subDespMensajes("LEYENDO PARAMETROS DE ENVIO...");

        //            oleICEP.ExeCommand = withVar.Keys["COMANDO_ICEP"].Valor;
        //            oleICEP.Equipo = withVar.Keys["EQUIPO"].Valor;
        //            oleICEP.Usuario = withVar.Keys["USUARIO"].Valor;
        //            oleICEP.Buzon = withVar.Keys["BUZON"].Valor;
        //            //AIS-96 rvillalta
        //            //oleICEP.Comando = Globales.udtComandos.intEnviar;
        //            //UPGRADE_WARNING: (6021) Casting 'double' to Enum may cause different behaviour.
        //            oleICEP.ModoEnvio = (Globales.clsICEP.udtModosEnvio)Convert.ToInt32(Conversion.Val(withVar.Keys["MODO"].Valor));
        //            strRutaBitacora = withVar.Keys["RUTA_BITACORA"].Valor;

        //            //-----------------------------
        //            //Envia el archivo por INTELAR
        //            //-----------------------------
        //            strArchivoEnviar = strArchivo;
        //            mdlGlobales.subDespMensajes("ENVIANDO ARCHIVO " + strArchivoEnviar.Substring(strArchivoEnviar.Length - Math.Min(strArchivoEnviar.Length, 26)) + ", POR FAVOR ESPERE...");
        //            oleICEP.ArchivoOrigen = strArchivoEnviar;
        //            oleICEP.ArchivoDestino = strArchivoEnviar.Substring(strArchivoEnviar.Length - Math.Min(strArchivoEnviar.Length, 26));
        //            strCadMensaje = DateTime.Today.ToString("dd/MM/yyyy") + " " + DateTime.Now.AddDays(-DateTime.Now.Date.ToOADate()).ToString("HH:mm");

        //            if (!oleICEP.EjecutaICEP())
                    
        //            {
        //                MessageBox.Show("NO PUDO SER ENVIADO EL ARCHIVO POR INTELAR, FAVOR DE VERIFICAR EL SERVICIO: " + oleICEP.DescripcionError, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //                strCadMensaje = strCadMensaje + " RECHAZADO -> ";
        //                strCadEstatus = "ESTATUS DE LA REMESA: NO SE CAMBIO EL ESTATUS DE LA REMESA";
        //                strDespErr = "NO SE PUDO GENERAR LA REMESA -PROB. DE ENVIO A INTELAR- REVISAR MENSAJES EN DIRECTORIO: ";
        //                //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
        //                string tempRefParam = "NO SE PUDO GENERAR LA REMESA -PROB. DE ENVIO A INTELAR- REVISAR MENSAJES EN DIRECTORIO: " + strRutaBitacora;
        //                MsgBoxStyle tempRefParam2 = (MsgBoxStyle)(((int)MsgBoxStyle.Exclamation) + ((int)MsgBoxStyle.OkOnly));
        //                mdlGlobales.subDespErrores(ref tempRefParam, ref tempRefParam2);
        //                strCadMensaje = strCadMensaje + " ERROR: " + oleICEP.CodigoError.ToString() + ". " + oleICEP.DescripcionError;
        //                blnRegreso = false;
        //            }
        //            else
        //            {
        //                //Ya que fue aceptada la transferencia hay que cambiar el estatus de la remesa a 02
        //                strCadMensaje = strCadMensaje + " ACEPTADO  -> ";
        //                if (mdlTranMasivo.funCambiaEstatusRemesa(frmProcMasivo.DefInstance.txtRemesa.Text.Substring(frmProcMasivo.DefInstance.txtRemesa.Text.Length - Math.Min(frmProcMasivo.DefInstance.txtRemesa.Text.Length, 10)), frmProcMasivo.DefInstance.txtArchivo.Text, mdlTranMasivo.gcstrEstatusRemesa))
        //                {
        //                    strCadEstatus = "ESTATUS DE LA REMESA: GENERADA";
        //                    strDespErr = "LA REMESA " + frmProcMasivo.DefInstance.txtRemesa.Text + " HA SIDO GENERADA SATISFACTORIAMENTE";
        //                    //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
        //                    string tempRefParam3 = "LA REMESA " + frmProcMasivo.DefInstance.txtRemesa.Text + " HA SIDO GENERADA SATISFACTORIAMENTE";
        //                    MsgBoxStyle tempRefParam4 = (MsgBoxStyle)(((int)MsgBoxStyle.Information) + ((int)MsgBoxStyle.OkOnly));
        //                    mdlGlobales.subDespErrores(ref tempRefParam3, ref tempRefParam4);
        //                }
        //                else
        //                {
        //                    strCadEstatus = "ESTATUS DE LA REMESA: NO PUDO SER ACTUALIZADO EL ESTATUS DE LA REMESA";
        //                    strDespErr = "NO SE PUDO GENERAR LA REMESA";
        //                    //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
        //                    string tempRefParam5 = "NO SE PUDO GENERAR LA REMESA";
        //                    MsgBoxStyle tempRefParam6 = (MsgBoxStyle)(((int)MsgBoxStyle.Exclamation) + ((int)MsgBoxStyle.OkOnly));
        //                    mdlGlobales.subDespErrores(ref tempRefParam5, ref tempRefParam6);
        //                }
        //            }

        //            frmProcMasivo.DefInstance.subLimpiarDatos();

        //            //MIG WXP INI JGC 20090825
        //            //strArchivo = strRutaBitacora + "\\FTP_" + mdlTranMasivo.gvstrFechaProceso + ".txt";
        //            strArchivo = strRutaBitacora + strMASRuta2 + mdlTranMasivo.gvstrFechaProceso + ".txt";
        //            //MIG WXP FIN JGC 20090825

        //            if (FileSystem.Dir(mdlGlobales.gcStrMasivosIni, FileAttribute.Normal) == "")
        //            {
        //                //MIG WXP INI JGC 20090825
        //                //strArchivo = "\\FTP_" + mdlTranMasivo.gvstrFechaProceso + ".txt";
        //                strArchivo = strMASRuta2 + mdlTranMasivo.gvstrFechaProceso + ".txt";
        //                //MIG WXP FIN JGC 20090825
        //            }

        //            strCadMensaje = strCadMensaje + strCadEstatus;
        //            intEnviaArchivo = FileSystem.FreeFile().ToString();
        //            FileSystem.FileOpen(Int32.Parse(intEnviaArchivo), strArchivo, OpenMode.Append, OpenAccess.Default, OpenShare.Default, -1);
        //            FileSystem.PrintLine(Int32.Parse(intEnviaArchivo), strCadMensaje);
        //            FileSystem.FileClose(Int32.Parse(intEnviaArchivo));

        //            //>> MR 13/Sep/2005 Envío a bitácora estatus de la remesa y de proceder causa de rechazo
        //            mdlComunica.gvMensaje = strDespErr + " Tipo de proceso de declinación: " + mdlCatalogos.gstrCatProceso + " Con clave de declinación: " + mdlGlobales.gstrClaveDeclina;
        //            mdlGlobales.subRegBitacora("E");
        //            //>> MR 13/Sep/2005 Si la remesa tiene causa de rechazo no la borro
        //            if (mdlCatalogos.gstrCatProceso == "DR" || mdlCatalogos.gstrCatProceso == "DF")
        //            {
        //            }
        //            else
        //            {
        //                File.Delete(strArchivoEnviar);
        //            }
        //            mdlCatalogos.gstrCatProceso = "";
        //            mdlGlobales.subDespMensajes("");

        //            //Destruye el ICEP
        //            oleICEP = null;
        //        } //ValidaParametros

        //        if (!blnRegreso)
        //        { //Terminar todas las variables si estaban activas
        //            oleICEP = null;
        //        }
        //        result = blnRegreso;
        //        mdlGlobales.subDespMensajes("LISTO");
        //        return result;
        //    }
        //    catch (Exception excep)
        //    {


        //        if (Information.Err().Number == 5)
        //        {
        //            //UPGRADE_TODO: (1065) Error handling statement (Resume Next) could not be converted properly. A throw statement was generated instead.
        //            throw new Exception("Migration Exception: 'Resume Next' not supported");
        //        }
        //        mdlGlobales.subDespMensajes("LISTO PERO CON ERRORES");
        //        oleICEP = null;
        //        if (Information.Err().Number != 99999)
        //        {
        //            MessageBox.Show("ERROR AL ENVIAR ARCHIVO: " + Information.Err().Number.ToString() + "-" + excep.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //        }
        //        return result;
        //    }
        //}
        //MIG WXP FIN JGC 20090825

        // Se intrega envio de remesas para pantallas negras  
        // AAHB infoware 
        static public bool EnviaArchivosREMESA_LN(string strArchivo, string indicaTipoEnvio)
        {
            //string indicaTipoEnvio = "CLC";
            bool result = false;
            Globales.clsICEP oleICEP = null;
            bool blnRegreso = false;
            string strRutaBitacora = String.Empty;
            string strCadMensaje = String.Empty;
            string strCadEstatus = String.Empty; //Indicador de que el estatus fue cambiado
            string strArchivoEnviar = String.Empty;
            string intEnviaArchivo = String.Empty;

            //string strDespErr = ""; 

            try
            {
                blnRegreso = true;
                mdlGlobales.subDespMensajes("PREPARANDO ENVIO, FAVOR DE ESPERAR...");

                //Busca los parametros de conexion a INTELAR, si no los encuentra aborta la rutina
                
                if (bln_ValidaParametros_ICEP_CLC(indicaTipoEnvio))
                {
                    //Crea el objeto de ICEP
                    oleICEP = new Globales.clsICEP();
                    //Obtiene los comandos de envio por ICEP a INTELAR
                    //POR clc
                    if (indicaTipoEnvio == "ICEP")
                    {
                        //POR ICEP
                        oleICEP.ExeCommand = strMASICEPCmd;
                        oleICEP.Equipo = strMASICEPEquipo;
                        oleICEP.Usuario = strMASICEPUsuario;
                        oleICEP.Buzon = strMASICEPBuzon;
                    }
                    else
                    {
                        //POR CLC
                        oleICEP.ExeCommand = strMASCLCCmd;
                        oleICEP.Equipo = strMASCLCEquipo;
                        oleICEP.Usuario = strMASCLCUsuario;
                        oleICEP.Buzon = strMASCLCBuzon;
                    }
                   
                    //modo de envio 
                    oleICEP.ModoEnvio = (Globales.clsICEP.udtModosEnvio)Convert.ToInt32(Conversion.Val(strMASModo));
                    
                    string strMASRutaAplicacion = mdlRegistry.RegistryMasivos("MASRutaAplicacion");
                    strRutaBitacora = mdlRegistry.RegistryMasivos("MASRutaBitacora");

                    //-----------------------------
                    //Envia el archivo por INTELAR
                    //-----------------------------
                    strArchivoEnviar = strArchivo;
                    mdlGlobales.subDespMensajes("ENVIANDO ARCHIVO " + strArchivoEnviar.Substring(strArchivoEnviar.Length - Math.Min(strArchivoEnviar.Length, 26)) + ", POR FAVOR ESPERE...");
                    oleICEP.ArchivoOrigen = strArchivoEnviar;
                    //oleICEP.ArchivoDestino = strArchivoEnviar.Substring(strArchivoEnviar.Length - Math.Min(strArchivoEnviar.Length, 26));
                    string strMASCLCPrefijoArcDestino = mdlRegistry.RegistryMasivos("MASCLCPrefijoArcDestino");
                    //oleICEP.ArchivoDestino = strMASCLCPrefijoArcDestino + strArchivoEnviar.Substring(strArchivoEnviar.Length - Math.Min(strArchivoEnviar.Length, 26));
                    string stArchivodds = strArchivo.Substring(strArchivo.LastIndexOf("\\")+1 );
                    oleICEP.ArchivoDestino = strMASCLCPrefijoArcDestino + stArchivodds;
                    
                    strCadMensaje = DateTime.Today.ToString("dd/MM/yyyy") + " " + DateTime.Now.AddDays(-DateTime.Now.Date.ToOADate()).ToString("HH:mm");

                    if (!oleICEP.EjecutaICEP(indicaTipoEnvio))
                    {
                        blnRegreso = false;

                    }


                    //Destruye el ICEP
                    if (oleICEP != null)
                    {
                        oleICEP.Dispose();
                        oleICEP = null;
                    }
                } //ValidaParametros

                if (!blnRegreso)
                { //Terminar todas las variables si estaban activas
                    //Destruye el ICEP
                    if (oleICEP != null)
                    {
                        oleICEP.Dispose();
                        oleICEP = null;
                    }
                }
                result = blnRegreso;
                mdlGlobales.subDespMensajes("LISTO");
                return result;
            }
            catch (Exception excep)
            {


                if (Information.Err().Number == 5)
                {
                    //UPGRADE_TODO: (1065) Error handling statement (Resume Next) could not be converted properly. A throw statement was generated instead.
                    throw new Exception("Migration Exception: 'Resume Next' not supported");
                }
                mdlGlobales.subDespMensajes("LISTO PERO CON ERRORES");
                oleICEP = null;
                if (Information.Err().Number != 99999)
                {
                    MessageBox.Show("ERROR AL ENVIAR ARCHIVO: " + Information.Err().Number.ToString() + "-" + excep.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                return result;
            }
        }



    }
}