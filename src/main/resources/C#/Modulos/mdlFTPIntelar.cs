using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility.VB6;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using VB6 = Microsoft.VisualBasic.Compatibility.VB6.Support;

namespace Masivos
{
    class mdlFTPIntelar
    {

        //*******************************************************************************
        //* Identificación: mdlFTPIntelar: Módulo de envío de archivos vía FTP          *
        //* Autor:          Israel Garcés                                               *
        //* Modificaciones: Alvaro Salinas                                              *
        //* Instalación:    PRAXIS                                                      *
        //* Fecha:          1/09/2003                                                   *
        //* Versión:        1.0                                                         *
        //*******************************************************************************
        static public bool gblnEstaCorriendo = false;

        //UPGRADE_NOTE: (7001) The following declaration (TestFunc) seems to be dead code
        //private int TestFunc( int lVal)
        //{
        //this function is necessary since the value returned by Shell is an
        //unsigned integer and may exceed the limits of a VB integer
        //if ((lVal & 0x8000) == 0)
        //{
        //return lVal & 0xFFFF;
        //} else
        //{
        //return 0x8000 | (lVal & 0x7FFF);
        //}
        //}



        //MIG WXP INI JGC 20090825
        //Rutina para enviar el archivo de folios de la remesa vía FTP
        static public void subEnviaArchivoFTP(ref  string strArchivo)
        {
            string intEnviaArchivo = String.Empty;
            double intValorRet = 0;
            int lngHndl = 0;
            string strArchivoCmd = String.Empty;
            string strIP = String.Empty;
            string strPuerto = String.Empty;
            string strUsuario = String.Empty;
            string strContrasena = String.Empty;
            string strArchivoEnviar = String.Empty;
            string strArchivoBat = String.Empty;
            string strArchivoLog = String.Empty;
            string strFTPExe = String.Empty;
            string strCadenaSalida = String.Empty;
            string strRutaBiracora = String.Empty;
            string strCadMensaje = String.Empty;
            bool blnConexion = false, blnAscci = false, blnCerrar = false;
            bool blnEntro = false, blnTransferencia = false, blnMalPassword = false;
            int IntVer = 0;
            FixedLengthString strCadenaDes = new FixedLengthString(8);
            string strDatos = String.Empty;
            //CONTROL DE FTP
            int TIEMPO = 0;
            int intNumProceso = 0;
            //FIN CONTROL FTP

            string strCadEstatus = String.Empty; //Indicador de que el estatus fue cambiado
            string strDespErr = ""; //>> MR 13/Sep/2005  variable para almacenar el mensaje de la remesa

            //MIG WXP INI JGC 20090825
            string strMASRuta2;
            //MIG WXP FIN JGC 20090825

            try
            {
                mdlGlobales.subDespMensajes("LEYENDO PARAMETROS DE ENVIO...");
                // VAR 02Ago2005 Proyecto 20410 Promociones. Se obtiene del catálogo 24 el indicador de ambiente
                strDatos = mdlGlobales.funPoneCeros(strDatos, 16);
                mdlTranMasivo.gstrIndicadorAmbiente = mdlTranMasivo.gcstrPruebas;
                if (mdlTranAnalisis.funEnviaRecibe5560("5560", "42", strDatos))
                {
                    mdlTranMasivo.gstrIndicadorAmbiente = Strings.Mid(mdlTranAnalisis.gvRecive5560_42, 198, 7).Trim();
                }

                //Verifica si existe el archivo Masivos.ini en la ruta
                //if (FileSystem.Dir(mdlGlobales.gcStrMasivosIni, FileAttribute.Normal) != "")
                //{
                // VAR 02Ago2005 Proyecto 20410 Promociones. En función del ambiente se obtiene la IP para el FTP
                if (mdlTranMasivo.gstrIndicadorAmbiente == mdlTranMasivo.gcstrProduccion)
                {
                    //string tempRefParam = mdlGlobales.gcStrMasivosIni;
                    //string tempRefParam2 = "ENLACE_FTP";
                    //string tempRefParam3 = "IP3";
                    //strIP = mdlGlobales.funGetParam(ref tempRefParam, ref tempRefParam2, ref tempRefParam3);
                    //MIG WXP INI JGC 20090825
                    strIP = mdlRegistry.RegistryMasivos("MASFTPIP3");
                    //MIG WXP FIN JGC 20090825
                    //string tempRefParam4 = mdlGlobales.gcStrMasivosIni;
                    //string tempRefParam5 = "ENLACE_FTP";
                    //string tempRefParam6 = "PUERTO3";
                    //strPuerto = mdlGlobales.funGetParam(ref tempRefParam4, ref tempRefParam5, ref tempRefParam6);
                    //MIG WXP INI JGC 20090825
                    strPuerto = mdlRegistry.RegistryMasivos("MASFTPPuerto3");
                    //MIG WXP FIN JGC 20090825

                }
                else if (mdlTranMasivo.gstrIndicadorAmbiente == mdlTranMasivo.gcstrPruebas)
                {
                    //string tempRefParam7 = mdlGlobales.gcStrMasivosIni;
                    //string tempRefParam8 = "ENLACE_FTP";
                    //string tempRefParam9 = "IP2";
                    //strIP = mdlGlobales.funGetParam(ref tempRefParam7, ref tempRefParam8, ref tempRefParam9);
                    //MIG WXP INI JGC 20090825
                    strIP = mdlRegistry.RegistryMasivos("MASFTPIP2");
                    //MIG WXP FIN JGC 20090825
                    //string tempRefParam10 = mdlGlobales.gcStrMasivosIni;
                    //string tempRefParam11 = "ENLACE_FTP";
                    //string tempRefParam12 = "PUERTO2";
                    //strPuerto = mdlGlobales.funGetParam(ref tempRefParam10, ref tempRefParam11, ref tempRefParam12);
                    //MIG WXP INI JGC 20090825
                    strPuerto = mdlRegistry.RegistryMasivos("MASFTPPuerto2");
                    //MIG WXP FIN JGC 20090825
                }
                else if (mdlTranMasivo.gstrIndicadorAmbiente == mdlTranMasivo.gcstrDesarrollo)
                {
                    //string tempRefParam13 = mdlGlobales.gcStrMasivosIni;
                    //string tempRefParam14 = "ENLACE_FTP";
                    //string tempRefParam15 = "IP1";
                    //strIP = mdlGlobales.funGetParam(ref tempRefParam13, ref tempRefParam14, ref tempRefParam15);
                    //MIG WXP INI JGC 20090825
                    strIP = mdlRegistry.RegistryMasivos("MASFTPIP1");
                    //MIG WXP FIN JGC 20090825
                    //string tempRefParam16 = mdlGlobales.gcStrMasivosIni;
                    //string tempRefParam17 = "ENLACE_FTP";
                    //string tempRefParam18 = "PUERTO1";
                    //strPuerto = mdlGlobales.funGetParam(ref tempRefParam16, ref tempRefParam17, ref tempRefParam18);
                    //MIG WXP INI JGC 20090825
                    strPuerto = mdlRegistry.RegistryMasivos("MASFTPPuerto1");
                    //MIG WXP FIN JGC 20090825
                }

                //string tempRefParam19 = mdlGlobales.gcStrMasivosIni;
                //string tempRefParam20 = "ENLACE_FTP";
                //string tempRefParam21 = "USUARIO";
                //strUsuario = mdlGlobales.funGetParam(ref tempRefParam19, ref tempRefParam20, ref tempRefParam21);
                //MIG WXP INI JGC 20090825
                strUsuario = mdlRegistry.RegistryMasivos("MASFTPUsuario");
                //MIG WXP FIN JGC 20090825
                //string tempRefParam22 = mdlGlobales.gcStrMasivosIni;
                //string tempRefParam23 = "ENLACE_FTP";
                //string tempRefParam24 = "PWD";
                //strContrasena = mdlGlobales.funGetParam(ref tempRefParam22, ref tempRefParam23, ref tempRefParam24);
                //MIG WXP INI JGC 20090825
                strContrasena = mdlRegistry.RegistryMasivos("MASFTPContraseña");
                //MIG WXP FIN JGC 20090825
                //string tempRefParam25 = mdlGlobales.gcStrMasivosIni;
                //string tempRefParam26 = "ENLACE_FTP";
                //string tempRefParam27 = "FTP";
                //strFTPExe = mdlGlobales.funGetParam(ref tempRefParam25, ref tempRefParam26, ref tempRefParam27);
                //MIG WXP INI JGC 20090825
                strFTPExe = mdlRegistry.RegistryMasivos("MASFTPExe");
                //MIG WXP FIN JGC 20090825
                //string tempRefParam28 = mdlGlobales.gcStrMasivosIni;
                //string tempRefParam29 = "ENLACE_FTP";
                //string tempRefParam30 = "FILECMD";
                //strArchivoCmd = mdlGlobales.funGetParam(ref tempRefParam28, ref tempRefParam29, ref tempRefParam30);
                //MIG WXP INI JGC 20090825
                strArchivoCmd = mdlRegistry.RegistryMasivos("MASArchivoCmd");
                //MIG WXP FIN JGC 20090825
                //string tempRefParam31 = mdlGlobales.gcStrMasivosIni;
                //string tempRefParam32 = "ENLACE_FTP";
                //string tempRefParam33 = "FILEBATCH";
                //strArchivoBat = mdlGlobales.funGetParam(ref tempRefParam31, ref tempRefParam32, ref tempRefParam33);
                //MIG WXP INI JGC 20090825
                strArchivoBat = mdlRegistry.RegistryMasivos("MASArchivoBat");
                //MIG WXP FIN JGC 20090825
                //string tempRefParam34 = mdlGlobales.gcStrMasivosIni;
                //string tempRefParam35 = "ENLACE_FTP";
                //string tempRefParam36 = "FILELOG";
                //strArchivoLog = mdlGlobales.funGetParam(ref tempRefParam34, ref tempRefParam35, ref tempRefParam36);
                //MIG WXP INI JGC 20090825
                strArchivoLog = mdlRegistry.RegistryMasivos("MASArchivoLog");
                //MIG WXP FIN JGC 20090825
                //string tempRefParam37 = mdlGlobales.gcStrMasivosIni;
                //string tempRefParam38 = "ENLACE_FTP";
                //string tempRefParam39 = "RUTA";
                //strRutaBiracora = mdlGlobales.funGetParam(ref tempRefParam37, ref tempRefParam38, ref tempRefParam39);
                //MIG WXP INI JGC 20090825
                strRutaBiracora = mdlRegistry.RegistryMasivos("MASRutaBitacora");
                //MIG WXP FIN JGC 20090825

                if (strIP == "unknown" || strPuerto == "unknown" || strUsuario == "unknown" || strContrasena == "unknown" || strFTPExe == "unknown" || strArchivoCmd == "unknown" || strArchivoBat == "unknown" || strArchivoLog == "unknown" || strRutaBiracora == "unknown")
                {
                    //MIG WXP INI JGC 20090825
                    //string tempRefParam40 = "REVISE SU ARCHIVO DE PARÁMETROS (ENVIO FTP): " + mdlGlobales.gcStrMasivosIni;
                    string tempRefParam40 = "REVISE EL REGISTRY DE PARAMETROS PARA ENVIO FTP: ";
                    //MIG WXP FIN JGC 20090825

                    MsgBoxStyle tempRefParam41 = MsgBoxStyle.Information;
                    string tempRefParam42 = "PARÁMETRO DESCONOCIDO";
                    mdlGlobales.subDespErrores(ref tempRefParam40, ref tempRefParam41, ref tempRefParam42);
                    return;
                }
                //} else
                //{
                //    //Crear el archivo de parámetros
                //    if (! mdlGlobales.funCreaMasivosIni())
                //    {
                //        //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                //        string tempRefParam43 = "NO SE ENCUENTRA ARCHIVO DE PARÁMETROS Y NO PUDO SER CREADO (ENVIO FTP)";
                //        MsgBoxStyle tempRefParam44 = (MsgBoxStyle) (((int) MsgBoxStyle.Critical) + ((int) MsgBoxStyle.OkOnly));
                //        mdlGlobales.subDespErrores(ref tempRefParam43, ref tempRefParam44);
                //    }
                //    return;
                //}

                //Asignar a los archivos las rutas del temporal
                strArchivoCmd = mdlGlobales.gstrRutaTemp + "\\" + strArchivoCmd;
                strArchivoBat = mdlGlobales.gstrRutaTemp + "\\" + strArchivoBat;
                strArchivoLog = mdlGlobales.gstrRutaTemp + "\\" + strArchivoLog;

                //MIG WXP INI JGC 20090825
                //REACTIVAR ESTE CODIGO PARA DESENCRIPTAR EL PASSWORD ENCRIPTADO
                //Inicia el proceso de desencripción del usuario y el password
                //intValorRet = mdlComunica.Inicia_Encripcion("", 0);
                //strCadenaDes.Value = new string((char) 255, 8);
                //string temp = strCadenaDes.Value;
                //intValorRet = API.Encryption.D3Des(strUsuario, ref temp);
                //strUsuario = temp;
                //strCadenaDes.Value = new string((char) 255, 8);
                //intValorRet = API.Encryption.D3Des(strUsuario, ref temp);
                //strUsuario = temp;
                //intValorRet = 0;
                //MIG WXP FIN JGC 20090825

                strArchivoEnviar = strArchivo;
                intEnviaArchivo = FileSystem.FreeFile().ToString();
                //PASO 1 DE N (JB-SAS 22/nov/2006) ARMA EL ARCHIVO DE COMANDOS DE FTP CON LOS QUE SE
                //ENVIARA EL ARCHIVO DE FOLIOS DE REMESA AL BUZON DE INTELAR
                FileSystem.FileOpen(Int32.Parse(intEnviaArchivo), strArchivoCmd, OpenMode.Output, OpenAccess.Default, OpenShare.Default, -1);
                FileSystem.PrintLine(Int32.Parse(intEnviaArchivo), "open " + strIP + " " + strPuerto);
                FileSystem.PrintLine(Int32.Parse(intEnviaArchivo), strUsuario);
                FileSystem.PrintLine(Int32.Parse(intEnviaArchivo), strContrasena);
                FileSystem.PrintLine(Int32.Parse(intEnviaArchivo), "binary ");
                FileSystem.PrintLine(Int32.Parse(intEnviaArchivo), "cd /xwin");
                //FileSystem.PrintLine(Int32.Parse(intEnviaArchivo), "put " + strArchivoEnviar);

                //MIG WXP INI JGC 20090825
                string strMASParam1 = mdlRegistry.RegistryMasivos("MASParam1");
                string strMASRutaAplicacion = mdlRegistry.RegistryMasivos("MASRutaAplicacion");
                //MIG WXP FIN JGC 20090825                   

                FileSystem.PrintLine(Int32.Parse(intEnviaArchivo), "put " + strArchivoEnviar + " " + strMASParam1 + Strings.Mid(strArchivo, Strings.Len(strArchivoEnviar) - 11, Strings.Len(strArchivoEnviar)));
                FileSystem.PrintLine(Int32.Parse(intEnviaArchivo), "bye ");
                FileSystem.FileClose(Int32.Parse(intEnviaArchivo));

                //PASO 2 DE N (JB-SAS 22/nov/2006) ARMA EL ARCHIVO DE COMANDOS DE BATCH QUE AL EJECUTARSE
                //ENVIARA POR FTP EL ARCHIVO DE FOLIOS DE REMESA AL BUZON DE INTELAR
                mdlGlobales.subDespMensajes("ENVIANDO ARCHIVO " + strArchivo.Substring(strArchivo.Length - Math.Min(strArchivo.Length, 26)) + ", POR FAVOR ESPERE...");
                FileSystem.FileOpen(Int32.Parse(intEnviaArchivo), strArchivoBat, OpenMode.Output, OpenAccess.Default, OpenShare.Default, -1);

                //Modif JGC DEBIDO A LAS COMILLAS QUE NECESITA POR LO DEL NOMBRE DE CARPETA CON ESPACIOS
                //26/02/2010 J
                //FileSystem.PrintLine(Int32.Parse(intEnviaArchivo), strFTPExe + strArchivoCmd + " > " + strArchivoLog);
                FileSystem.PrintLine(Int32.Parse(intEnviaArchivo), strFTPExe + '"' + strArchivoCmd + '"' + " > " + '"' + strArchivoLog + '"');

                FileSystem.FileClose(Int32.Parse(intEnviaArchivo));

                //PASO 3 DE N (JB-SAS 22/nov/2006) EJECUTA POR SHELL EL ARCHIVO DE COMANDOS DE BATCH PARA
                //ENVIAR POR FTP EL ARCHIVO DE FOLIOS DE REMESA AL BUZON DE INTELAR
                intNumProceso = 0;
                //UPGRADE_WARNING: (7005) parameters (if any) must be set using the Arguments property of ProcessStartInfo
                ProcessStartInfo startInfo = new ProcessStartInfo(strArchivoBat);
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                //AIS-1615 FSABORIO
                Process p = Process.Start(startInfo);
                do
                {
                    p.WaitForExit(1000);
                    TIEMPO++;

                } while ((!p.HasExited) || (TIEMPO <= 13));

                //PROCESA CONTROL DE RESPUESTA DE FTP
                blnAscci = false;
                blnConexion = false;
                blnCerrar = false;
                blnTransferencia = false;
                blnEntro = false;
                blnMalPassword = false;
                mdlGlobales.subDespMensajes("VALIDANDO ENVIO ...");

                //BUSCA HASTA ENCONTRAR EL ARCHIVO DE LOG
                while (FileSystem.Dir(strArchivoLog, FileAttribute.Normal) == "")
                {

                    strArchivoLog = strArchivoLog;
                }

                //ABRE EL ARCHIVO DE LOG PARA SU REVISION
                FileSystem.FileOpen(Int32.Parse(intEnviaArchivo), strArchivoLog, OpenMode.Input, OpenAccess.Default, OpenShare.Default, -1);
                while (!FileSystem.EOF(Int32.Parse(intEnviaArchivo)))
                {

                    FileSystem.Input(Int32.Parse(intEnviaArchivo), ref strCadenaSalida);
                    if (strCadenaSalida.StartsWith("150"))
                    { // Puerto Abierto en Ascii Ok
                        blnAscci = true;
                    }
                    if (strCadenaSalida.StartsWith("220"))
                    { // Conección OK
                        blnConexion = true;
                    }
                    if (strCadenaSalida.StartsWith("221"))
                    { // Cerrar sesion Ok
                        blnCerrar = true;
                    }
                    if (strCadenaSalida.StartsWith("226"))
                    { // Transferencia Completa Ok
                        blnTransferencia = true;
                    }
                    if (strCadenaSalida.StartsWith("230"))
                    { // IP correcta Ok
                        blnEntro = true;
                    }
                    if (strCadenaSalida.StartsWith("530"))
                    { // Dentro del Server Ok
                        blnMalPassword = true;
                    }
                }
                FileSystem.FileClose(Int32.Parse(intEnviaArchivo));


                strCadMensaje = DateTime.Today.ToString("dd/MM/yyyy") + " " + DateTime.Now.AddDays(-DateTime.Now.Date.ToOADate()).ToString("HH:mm");
                if (blnAscci && blnConexion && blnCerrar && blnTransferencia && blnEntro && !blnMalPassword)
                {
                    strCadMensaje = strCadMensaje + " ACEPTADO  -> ";
                    //Ya que fue aceptada la transferencia hay que cambiar el estatus de la remesa a 02
                    if (mdlTranMasivo.funCambiaEstatusRemesa(frmProcMasivo.DefInstance.txtRemesa.Text.Substring(frmProcMasivo.DefInstance.txtRemesa.Text.Length - Math.Min(frmProcMasivo.DefInstance.txtRemesa.Text.Length, 10)), frmProcMasivo.DefInstance.txtArchivo.Text, mdlTranMasivo.gcstrEstatusRemesa))
                    {
                        strCadEstatus = "ESTATUS DE LA REMESA: GENERADA";
                        strDespErr = "LA REMESA " + frmProcMasivo.DefInstance.txtRemesa.Text + " HA SIDO GENERADA SATISFACTORIAMENTE";
                        //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                        string tempRefParam45 = "LA REMESA " + frmProcMasivo.DefInstance.txtRemesa.Text + " HA SIDO GENERADA SATISFACTORIAMENTE";
                        MsgBoxStyle tempRefParam46 = (MsgBoxStyle)(((int)MsgBoxStyle.Information) + ((int)MsgBoxStyle.OkOnly));
                        mdlGlobales.subDespErrores(ref tempRefParam45, ref tempRefParam46);
                    }
                    else
                    {
                        strCadEstatus = "ESTATUS DE LA REMESA: NO PUDO SER ACTUALIZADO EL ESTATUS DE LA REMESA";
                        strDespErr = "NO SE PUDO GENERAR LA REMESA";
                        //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                        string tempRefParam47 = "NO SE PUDO GENERAR LA REMESA";
                        MsgBoxStyle tempRefParam48 = (MsgBoxStyle)(((int)MsgBoxStyle.Exclamation) + ((int)MsgBoxStyle.OkOnly));
                        mdlGlobales.subDespErrores(ref tempRefParam47, ref tempRefParam48);
                    }
                }
                else
                {
                    strCadMensaje = strCadMensaje + " RECHAZADO -> ";
                    strCadEstatus = "ESTATUS DE LA REMESA: NO SE CAMBIO EL ESTATUS DE LA REMESA";
                    strDespErr = "NO SE PUDO GENERAR LA REMESA -PROB. DE ENVIO A INTELAR- REVISAR MENSAJES EN DIRECTORIO: ";
                    //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                    string tempRefParam49 = "NO SE PUDO GENERAR LA REMESA -PROB. DE ENVIO A INTELAR- REVISAR MENSAJES EN DIRECTORIO: " + strRutaBiracora;
                    MsgBoxStyle tempRefParam50 = (MsgBoxStyle)(((int)MsgBoxStyle.Exclamation) + ((int)MsgBoxStyle.OkOnly));
                    mdlGlobales.subDespErrores(ref tempRefParam49, ref tempRefParam50);
                }
                frmProcMasivo.DefInstance.subLimpiarDatos();

                if (blnEntro)
                {
                    strCadMensaje = strCadMensaje + "DIRECCION IP VALIDA   / ";
                }
                else
                {
                    strCadMensaje = strCadMensaje + "DIRECCION IP NO VALIDA/ ";
                }
                if (!blnMalPassword)
                {
                    strCadMensaje = strCadMensaje + "CONTRASEÑA Y USUARIO CORRECTO/ ";
                }
                else
                {
                    strCadMensaje = strCadMensaje + "CONTRASEÑA O USUARIO ERRONEO / ";
                }
                if (blnConexion)
                {
                    strCadMensaje = strCadMensaje + "CONEXION ACEPTADA / ";
                }
                else
                {
                    strCadMensaje = strCadMensaje + "CONEXION RECHAZADA/ ";
                }
                if (blnAscci)
                {
                    strCadMensaje = strCadMensaje + "ENVIO ASCII CORRECTO/ ";
                }
                else
                {
                    strCadMensaje = strCadMensaje + "ENVIO ASCII ERRONEO / ";
                }
                if (blnTransferencia)
                {
                    strCadMensaje = strCadMensaje + "TRANSFERENCIA CORRECTA/ ";
                }
                else
                {
                    strCadMensaje = strCadMensaje + "TRANSFERENCIA ERRONEA / ";
                }
                if (blnCerrar)
                {
                    strCadMensaje = strCadMensaje + "CERRAR SESION CORRECTO/ ";
                }
                else
                {
                    strCadMensaje = strCadMensaje + "CERRAR SESION ERRONEA / ";
                }

                //MIG WXP INI JGC 20090825
                strMASRuta2 = mdlRegistry.RegistryMasivos("MASRuta2");
                //strArchivo = strRutaBiracora + "\\FTP_" + mdlTranMasivo.gvstrFechaProceso + ".txt";
                //Modif 24/02/2010
                //strArchivo = strRutaBiracora + strMASRuta2 + mdlTranMasivo.gvstrFechaProceso + ".txt";
                strArchivo = strMASRutaAplicacion + strRutaBiracora + strMASRuta2 + mdlTranMasivo.gvstrFechaProceso + ".txt";
                //fin Modif 24/02/2010

                //MIG WXP INI JGC 20090825
                //QUE YA NO CHEQUE SI EXISTE EL INI
                //if (FileSystem.Dir(mdlGlobales.gcStrMasivosIni, FileAttribute.Normal) == "")
                //{
                //    //MIG WXP INI JGC 20090825
                //    //strArchivo = "\\FTP_" + mdlTranMasivo.gvstrFechaProceso + ".txt";
                //    strArchivo = strMASRuta2 + mdlTranMasivo.gvstrFechaProceso + ".txt";
                //    //MIG WXP FIN JGC 20090825
                //}
                //MIG WXP INI JGC 20090825

                strCadMensaje = strCadMensaje + strCadEstatus;
                FileSystem.FileOpen(Int32.Parse(intEnviaArchivo), strArchivo, OpenMode.Append, OpenAccess.Default, OpenShare.Default, -1);
                FileSystem.PrintLine(Int32.Parse(intEnviaArchivo), strCadMensaje);
                FileSystem.FileClose(Int32.Parse(intEnviaArchivo));
                //>> MR 13/Sep/2005 Envío a bitácora estatus de la remesa y de proceder causa de rechazo
                mdlComunica.gvMensaje = strDespErr + " Tipo de proceso de declinación: " + mdlCatalogos.gstrCatProceso + " Con clave de declinación: " + mdlGlobales.gstrClaveDeclina;
                mdlGlobales.subRegBitacora("E");
                File.Delete(strArchivoLog);
                File.Delete(strArchivoCmd);
                File.Delete(strArchivoBat);
                //>> MR 13/Sep/2005 Si la remesa tiene causa de rechazo no la borro
                if (mdlCatalogos.gstrCatProceso == "DR" || mdlCatalogos.gstrCatProceso == "DF")
                {
                }
                else
                {
                    File.Delete(strArchivoEnviar);
                }
                mdlCatalogos.gstrCatProceso = "";
                mdlGlobales.subDespMensajes("");
            }
            catch (Exception excep)
            {

                if (Information.Err().Number == 53 || Information.Err().Number == 70)
                {
                    //UPGRADE_TODO: (1065) Error handling statement (Resume) could not be converted properly. A throw statement was generated instead.
                    throw new Exception("Migration Exception: 'Resume' not supported");
                }
                else
                {
                    //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                    string tempRefParam51 = "AVISO " + Information.Err().Number.ToString() + " : " + excep.Message;
                    MsgBoxStyle tempRefParam52 = (MsgBoxStyle)(((int)MsgBoxStyle.Exclamation) + ((int)MsgBoxStyle.OkOnly));
                    mdlGlobales.subDespErrores(ref tempRefParam51, ref tempRefParam52);
                    return;
                }
                //UPGRADE_WARNING: (1041) Return has a new behavior.
                return;
            }
        }

        //MIG WXP FIN JGC 20090825


        // RUTINA PARA ENVIO DE REMESAS LISTAS NEGRAS
        // MODIF MAP 2010/10/06
        static public bool subEnviaArchivoFTP_LN(ref  string strArchivo)
        {
            string intEnviaArchivo = String.Empty;
            //MODIF MAP 2014/03/19
            //double intValorRet = 0;
            //int lngHndl = 0;
            string strArchivoCmd = String.Empty;
            string strIP = String.Empty;
            string strPuerto = String.Empty;
            string strUsuario = String.Empty;
            string strContrasena = String.Empty;
            string strArchivoEnviar = String.Empty;
            string strArchivoBat = String.Empty;
            string strArchivoLog = String.Empty;
            string strFTPExe = String.Empty;
            string strCadenaSalida = String.Empty;
            string strRutaBiracora = String.Empty;
            string strCadMensaje = String.Empty;
            bool blnConexion = false, blnAscci = false, blnCerrar = false;
            bool blnEntro = false, blnTransferencia = false, blnMalPassword = false;
            //MODIF MAP 2014/03/19
            //int IntVer = 0;
            FixedLengthString strCadenaDes = new FixedLengthString(8);
            string strDatos = String.Empty;
            //CONTROL DE FTP
            int TIEMPO = 0;
            //MODIF MAP 2014/03/19
            //int intNumProceso = 0;
            //FIN CONTROL FTP

            string strCadEstatus = String.Empty; //Indicador de que el estatus fue cambiado
            //MODIF MAP 2014/03/19
            //string strDespErr = ""; //>> MR 13/Sep/2005  variable para almacenar el mensaje de la remesa

            //MIG WXP INI JGC 20090825
            string strMASRuta2;
            //MIG WXP FIN JGC 20090825

            bool boTransacionExitosa = false;

            try
            {
                mdlGlobales.subDespMensajes("LEYENDO PARAMETROS DE ENVIO...");
                // VAR 02Ago2005 Proyecto 20410 Promociones. Se obtiene del catálogo 24 el indicador de ambiente
                strDatos = mdlGlobales.funPoneCeros(strDatos, 16);
                mdlTranMasivo.gstrIndicadorAmbiente = mdlTranMasivo.gcstrPruebas;
                if (mdlTranAnalisis.funEnviaRecibe5560("5560", "42", strDatos))
                {
                    mdlTranMasivo.gstrIndicadorAmbiente = Strings.Mid(mdlTranAnalisis.gvRecive5560_42, 198, 7).Trim();
                }
                
                if (mdlTranMasivo.gstrIndicadorAmbiente == mdlTranMasivo.gcstrProduccion)
                {
                    strIP = mdlRegistry.RegistryMasivos("MASFTPIP3");                    
                    strPuerto = mdlRegistry.RegistryMasivos("MASFTPPuerto3");                    
                }
                else if (mdlTranMasivo.gstrIndicadorAmbiente == mdlTranMasivo.gcstrPruebas)
                {                 
                    strIP = mdlRegistry.RegistryMasivos("MASFTPIP2");                 
                    strPuerto = mdlRegistry.RegistryMasivos("MASFTPPuerto2");                 
                }
                else if (mdlTranMasivo.gstrIndicadorAmbiente == mdlTranMasivo.gcstrDesarrollo)
                {                    
                    strIP = mdlRegistry.RegistryMasivos("MASFTPIP1");                 
                    strPuerto = mdlRegistry.RegistryMasivos("MASFTPPuerto1");                    
                }
                
                strUsuario = mdlRegistry.RegistryMasivos("MASFTPUsuario");
                strContrasena = mdlRegistry.RegistryMasivos("MASFTPContraseña");
                strFTPExe = mdlRegistry.RegistryMasivos("MASFTPExe");
                strArchivoCmd = mdlRegistry.RegistryMasivos("MASArchivoCmd");
                strArchivoBat = mdlRegistry.RegistryMasivos("MASArchivoBat");
                strArchivoLog = mdlRegistry.RegistryMasivos("MASArchivoLog");
                strRutaBiracora = mdlRegistry.RegistryMasivos("MASRutaBitacora");

                if (strIP == "unknown" || strPuerto == "unknown" || strUsuario == "unknown" || strContrasena == "unknown" || strFTPExe == "unknown" || strArchivoCmd == "unknown" || strArchivoBat == "unknown" || strArchivoLog == "unknown" || strRutaBiracora == "unknown")
                {
                    string tempRefParam40 = "REVISE EL REGISTRY DE PARAMETROS PARA ENVIO FTP: ";

                    MsgBoxStyle tempRefParam41 = MsgBoxStyle.Information;
                    string tempRefParam42 = "PARÁMETRO DESCONOCIDO";
                    mdlGlobales.subDespErrores(ref tempRefParam40, ref tempRefParam41, ref tempRefParam42);
                    return boTransacionExitosa;
                }

                //Asignar a los archivos las rutas del temporal
                strArchivoCmd = mdlGlobales.gstrRutaTemp + "\\" + strArchivoCmd;
                strArchivoBat = mdlGlobales.gstrRutaTemp + "\\" + strArchivoBat;
                strArchivoLog = mdlGlobales.gstrRutaTemp + "\\" + strArchivoLog;

                strArchivoEnviar = strArchivo;
                intEnviaArchivo = FileSystem.FreeFile().ToString();
                //PASO 1 DE N (JB-SAS 22/nov/2006) ARMA EL ARCHIVO DE COMANDOS DE FTP CON LOS QUE SE
                //ENVIARA EL ARCHIVO DE FOLIOS DE REMESA AL BUZON DE INTELAR
                FileSystem.FileOpen(Int32.Parse(intEnviaArchivo), strArchivoCmd, OpenMode.Output, OpenAccess.Default, OpenShare.Default, -1);
                FileSystem.PrintLine(Int32.Parse(intEnviaArchivo), "open " + strIP + " " + strPuerto);
                FileSystem.PrintLine(Int32.Parse(intEnviaArchivo), strUsuario);
                FileSystem.PrintLine(Int32.Parse(intEnviaArchivo), strContrasena);
                FileSystem.PrintLine(Int32.Parse(intEnviaArchivo), "binary ");
                FileSystem.PrintLine(Int32.Parse(intEnviaArchivo), "cd /xwin");

                //MIG WXP INI JGC 20090825
                string strMASParam1 = mdlRegistry.RegistryMasivos("MASParam1");
                string strMASRutaAplicacion = mdlRegistry.RegistryMasivos("MASRutaAplicacion");
                //MIG WXP FIN JGC 20090825                   

                FileSystem.PrintLine(Int32.Parse(intEnviaArchivo), "put " + strArchivoEnviar + " " + strMASParam1 + Strings.Mid(strArchivo, Strings.Len(strArchivoEnviar) - 11, Strings.Len(strArchivoEnviar)));
                FileSystem.PrintLine(Int32.Parse(intEnviaArchivo), "bye ");
                FileSystem.FileClose(Int32.Parse(intEnviaArchivo));

                //PASO 2 DE N (JB-SAS 22/nov/2006) ARMA EL ARCHIVO DE COMANDOS DE BATCH QUE AL EJECUTARSE
                //ENVIARA POR FTP EL ARCHIVO DE FOLIOS DE REMESA AL BUZON DE INTELAR
                mdlGlobales.subDespMensajes("ENVIANDO ARCHIVO " + strArchivo.Substring(strArchivo.Length - Math.Min(strArchivo.Length, 26)) + ", POR FAVOR ESPERE...");
                FileSystem.FileOpen(Int32.Parse(intEnviaArchivo), strArchivoBat, OpenMode.Output, OpenAccess.Default, OpenShare.Default, -1);

                //Modif JGC DEBIDO A LAS COMILLAS QUE NECESITA POR LO DEL NOMBRE DE CARPETA CON ESPACIOS
                //26/02/2010 J
                FileSystem.PrintLine(Int32.Parse(intEnviaArchivo), strFTPExe + '"' + strArchivoCmd + '"' + " > " + '"' + strArchivoLog + '"');

                FileSystem.FileClose(Int32.Parse(intEnviaArchivo));

                //PASO 3 DE N (JB-SAS 22/nov/2006) EJECUTA POR SHELL EL ARCHIVO DE COMANDOS DE BATCH PARA
                //ENVIAR POR FTP EL ARCHIVO DE FOLIOS DE REMESA AL BUZON DE INTELAR
                //MODIF MAP 2014/03/19
                //intNumProceso = 0;
                //UPGRADE_WARNING: (7005) parameters (if any) must be set using the Arguments property of ProcessStartInfo
                ProcessStartInfo startInfo = new ProcessStartInfo(strArchivoBat);
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                //AIS-1615 FSABORIO
                Process p = Process.Start(startInfo);
                do
                {
                    p.WaitForExit(1000);
                    TIEMPO++;

                } while ((!p.HasExited) || (TIEMPO <= 13));

                //PROCESA CONTROL DE RESPUESTA DE FTP
                blnAscci = false;
                blnConexion = false;
                blnCerrar = false;
                blnTransferencia = false;
                blnEntro = false;
                blnMalPassword = false;
                mdlGlobales.subDespMensajes("VALIDANDO ENVIO ...");

                //BUSCA HASTA ENCONTRAR EL ARCHIVO DE LOG
                while (FileSystem.Dir(strArchivoLog, FileAttribute.Normal) == "")
                {
                    strArchivoLog = strArchivoLog;
                }

                //ABRE EL ARCHIVO DE LOG PARA SU REVISION
                FileSystem.FileOpen(Int32.Parse(intEnviaArchivo), strArchivoLog, OpenMode.Input, OpenAccess.Default, OpenShare.Default, -1);
                while (!FileSystem.EOF(Int32.Parse(intEnviaArchivo)))
                {
                    FileSystem.Input(Int32.Parse(intEnviaArchivo), ref strCadenaSalida);
                    if (strCadenaSalida.StartsWith("150"))                    
                        blnAscci = true; // Puerto Abierto en Ascii Ok
                    
                    if (strCadenaSalida.StartsWith("220"))                     
                        blnConexion = true; // Conección OK
                    
                    if (strCadenaSalida.StartsWith("221"))                    
                        blnCerrar = true; // Cerrar sesion Ok
                    
                    if (strCadenaSalida.StartsWith("226"))                    
                        blnTransferencia = true; // Transferencia Completa Ok
                    
                    if (strCadenaSalida.StartsWith("230"))                    
                        blnEntro = true; // IP correcta Ok
                    
                    if (strCadenaSalida.StartsWith("530"))                    
                        blnMalPassword = true; // Dentro del Server Ok                    
                }
                FileSystem.FileClose(Int32.Parse(intEnviaArchivo));

                strCadMensaje = DateTime.Today.ToString("dd/MM/yyyy") + " " + DateTime.Now.AddDays(-DateTime.Now.Date.ToOADate()).ToString("HH:mm");

                if (blnAscci && blnConexion && blnCerrar && blnTransferencia && blnEntro && !blnMalPassword)
                    boTransacionExitosa = true;
                else
                    boTransacionExitosa = false;
                
                   

                //if (blnAscci && blnConexion && blnCerrar && blnTransferencia && blnEntro && !blnMalPassword)
                //{

                    //strCadMensaje = strCadMensaje + " ACEPTADO  -> ";
                    ////Ya que fue aceptada la transferencia hay que cambiar el estatus de la remesa a 02
                    //if (mdlTranMasivo.funCambiaEstatusRemesa(frmProcMasivo.DefInstance.txtRemesa.Text.Substring(frmProcMasivo.DefInstance.txtRemesa.Text.Length - Math.Min(frmProcMasivo.DefInstance.txtRemesa.Text.Length, 10)), frmProcMasivo.DefInstance.txtArchivo.Text, mdlTranMasivo.gcstrEstatusRemesa))
                    //{
                    //    strCadEstatus = "ESTATUS DE LA REMESA: GENERADA";
                    //    strDespErr = "LA REMESA " + frmProcMasivo.DefInstance.txtRemesa.Text + " HA SIDO GENERADA SATISFACTORIAMENTE";
                    //    //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                    //    string tempRefParam45 = "LA REMESA " + frmProcMasivo.DefInstance.txtRemesa.Text + " HA SIDO GENERADA SATISFACTORIAMENTE";
                    //    MsgBoxStyle tempRefParam46 = (MsgBoxStyle)(((int)MsgBoxStyle.Information) + ((int)MsgBoxStyle.OkOnly));
                    //    mdlGlobales.subDespErrores(ref tempRefParam45, ref tempRefParam46);
                    //}
                    //else
                    //{
                    //    strCadEstatus = "ESTATUS DE LA REMESA: NO PUDO SER ACTUALIZADO EL ESTATUS DE LA REMESA";
                    //    strDespErr = "NO SE PUDO GENERAR LA REMESA";
                    //    //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                    //    string tempRefParam47 = "NO SE PUDO GENERAR LA REMESA";
                    //    MsgBoxStyle tempRefParam48 = (MsgBoxStyle)(((int)MsgBoxStyle.Exclamation) + ((int)MsgBoxStyle.OkOnly));
                    //    mdlGlobales.subDespErrores(ref tempRefParam47, ref tempRefParam48);
                    //}
                //}
                //else
                //{
                //    strCadMensaje = strCadMensaje + " RECHAZADO -> ";
                //    strCadEstatus = "ESTATUS DE LA REMESA: NO SE CAMBIO EL ESTATUS DE LA REMESA";
                //    strDespErr = "NO SE PUDO GENERAR LA REMESA -PROB. DE ENVIO A INTELAR- REVISAR MENSAJES EN DIRECTORIO: ";
                //    //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                //    string tempRefParam49 = "NO SE PUDO GENERAR LA REMESA -PROB. DE ENVIO A INTELAR- REVISAR MENSAJES EN DIRECTORIO: " + strRutaBiracora;
                //    MsgBoxStyle tempRefParam50 = (MsgBoxStyle)(((int)MsgBoxStyle.Exclamation) + ((int)MsgBoxStyle.OkOnly));
                //    mdlGlobales.subDespErrores(ref tempRefParam49, ref tempRefParam50);
                //}
                //frmProcMasivo.DefInstance.subLimpiarDatos();

                if (blnEntro)                
                    strCadMensaje = strCadMensaje + "DIRECCION IP VALIDA   / ";                
                else                
                    strCadMensaje = strCadMensaje + "DIRECCION IP NO VALIDA/ ";
                
                if (!blnMalPassword)                
                    strCadMensaje = strCadMensaje + "CONTRASEÑA Y USUARIO CORRECTO/ ";                
                else                
                    strCadMensaje = strCadMensaje + "CONTRASEÑA O USUARIO ERRONEO / ";
                
                if (blnConexion)                
                    strCadMensaje = strCadMensaje + "CONEXION ACEPTADA / ";                
                else                
                    strCadMensaje = strCadMensaje + "CONEXION RECHAZADA/ ";
                
                if (blnAscci)                
                    strCadMensaje = strCadMensaje + "ENVIO ASCII CORRECTO/ ";                
                else                
                    strCadMensaje = strCadMensaje + "ENVIO ASCII ERRONEO / ";
                
                if (blnTransferencia)                
                    strCadMensaje = strCadMensaje + "TRANSFERENCIA CORRECTA/ ";                
                else                
                    strCadMensaje = strCadMensaje + "TRANSFERENCIA ERRONEA / ";
                
                if (blnCerrar)                
                    strCadMensaje = strCadMensaje + "CERRAR SESION CORRECTO/ ";                
                else                
                    strCadMensaje = strCadMensaje + "CERRAR SESION ERRONEA / ";
                

                //MIG WXP INI JGC 20090825
                strMASRuta2 = mdlRegistry.RegistryMasivos("MASRuta2");
                //strArchivo = strRutaBiracora + "\\FTP_" + mdlTranMasivo.gvstrFechaProceso + ".txt";
                //Modif 24/02/2010
                //strArchivo = strRutaBiracora + strMASRuta2 + mdlTranMasivo.gvstrFechaProceso + ".txt";
                strArchivo = strMASRutaAplicacion + strRutaBiracora + strMASRuta2 + mdlTranMasivo.gvstrFechaProceso + ".txt";
                //fin Modif 24/02/2010

                //MIG WXP INI JGC 20090825
                //QUE YA NO CHEQUE SI EXISTE EL INI
                //if (FileSystem.Dir(mdlGlobales.gcStrMasivosIni, FileAttribute.Normal) == "")
                //{
                //    //MIG WXP INI JGC 20090825
                //    //strArchivo = "\\FTP_" + mdlTranMasivo.gvstrFechaProceso + ".txt";
                //    strArchivo = strMASRuta2 + mdlTranMasivo.gvstrFechaProceso + ".txt";
                //    //MIG WXP FIN JGC 20090825
                //}
                //MIG WXP INI JGC 20090825

                strCadMensaje = strCadMensaje + strCadEstatus;
                FileSystem.FileOpen(Int32.Parse(intEnviaArchivo), strArchivo, OpenMode.Append, OpenAccess.Default, OpenShare.Default, -1);
                FileSystem.PrintLine(Int32.Parse(intEnviaArchivo), strCadMensaje);
                FileSystem.FileClose(Int32.Parse(intEnviaArchivo));
                //>> MR 13/Sep/2005 Envío a bitácora estatus de la remesa y de proceder causa de rechazo
                //mdlComunica.gvMensaje = strDespErr + " Tipo de proceso de declinación: " + mdlCatalogos.gstrCatProceso + " Con clave de declinación: " + mdlGlobales.gstrClaveDeclina;
                //mdlGlobales.subRegBitacora("E");
                File.Delete(strArchivoLog);
                File.Delete(strArchivoCmd);
                File.Delete(strArchivoBat);
                //>> MR 13/Sep/2005 Si la remesa tiene causa de rechazo no la borro
                //if (mdlCatalogos.gstrCatProceso == "DR" || mdlCatalogos.gstrCatProceso == "DF")
                //{
                //}
                //else
                //{
                //    //File.Delete(strArchivoEnviar);
                //}
                mdlCatalogos.gstrCatProceso = "";
                mdlGlobales.subDespMensajes("");
            }
            catch (Exception excep)
            {

                if (Information.Err().Number == 53 || Information.Err().Number == 70)
                {
                    //UPGRADE_TODO: (1065) Error handling statement (Resume) could not be converted properly. A throw statement was generated instead.
                    throw new Exception("Migration Exception: 'Resume' not supported");
                }
                else
                {
                    //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                    string tempRefParam51 = "AVISO " + Information.Err().Number.ToString() + " : " + excep.Message;
                    MsgBoxStyle tempRefParam52 = (MsgBoxStyle)(((int)MsgBoxStyle.Exclamation) + ((int)MsgBoxStyle.OkOnly));
                    mdlGlobales.subDespErrores(ref tempRefParam51, ref tempRefParam52);
                    return boTransacionExitosa;
                }
                //UPGRADE_WARNING: (1041) Return has a new behavior.
                return boTransacionExitosa;
            }
            return boTransacionExitosa;
        }





        //Rutina para enviar el archivo de VENTAS CRUZADAS vía FTP MEB 10-ENE-2005
        //MIG WXP INI JGC 20090825
        //ESTA RUTINA YA NO SE DEBERIA EMPLEAR, DADO QUE NO SE DEBEN HACER ENVIOS FTP
        //static public void subEnviaArchivoVCFTP(ref  string strArchivo)
        //{
        //    string intEnviaArchivo = String.Empty;
        //    double intValorRet = 0;
        //    int lngHndl = 0;
        //    string strArchivoCmd = String.Empty;
        //    string strIP = String.Empty;
        //    string strPuerto = String.Empty;
        //    string strUsuario = String.Empty;
        //    string strContrasena = String.Empty;
        //    string strArchivoEnviar = String.Empty;
        //    string strArchivoBat = String.Empty;
        //    string strArchivoLog = String.Empty;
        //    string strFTPExe = String.Empty;
        //    string strCadenaSalida = String.Empty;
        //    string strRutaBiracora = String.Empty;
        //    string strCadMensaje = String.Empty;
        //    bool blnConexion = false, blnAscci = false, blnCerrar = false;
        //    bool blnEntro = false, blnTransferencia = false, blnMalPassword = false;
        //    int IntVer = 0;
        //    FixedLengthString strCadenaDes = new FixedLengthString(8);
        //    string strDatos = String.Empty;
        //    //CONTROL FTP
        //    int TIEMPO = 0;
        //    int intNumProceso = 0;
        //    //FIN CONTROL FTP

        //    try
        //    {
        //        mdlGlobales.subDespMensajes("LEYENDO PARAMETROS DE ENVIO...");
        //        // VAR 02Ago2005 Proyecto 20410 Promociones. Se obtiene del catálogo 24 el indicador de ambiente
        //        strDatos = mdlGlobales.funPoneCeros(strDatos, 16);
        //        mdlTranMasivo.gstrIndicadorAmbiente = mdlTranMasivo.gcstrPruebas;
        //        if (mdlTranAnalisis.funEnviaRecibe5560("5560", "42", strDatos))
        //        {
        //            mdlTranMasivo.gstrIndicadorAmbiente = Strings.Mid(mdlTranAnalisis.gvRecive5560_42, 198, 7).Trim();
        //        }

        //        //Verifica si existe el archivo Masivos.ini en la ruta
        //        if (FileSystem.Dir(mdlGlobales.gcStrMasivosIni, FileAttribute.Normal) != "")
        //        {
        //            // VAR 02Ago2005 Proyecto 20410 Promociones. En función del ambiente se obtiene la IP para el FTP
        //            if (mdlTranMasivo.gstrIndicadorAmbiente == mdlTranMasivo.gcstrProduccion)
        //            {
        //                string tempRefParam = mdlGlobales.gcStrMasivosIni;
        //                string tempRefParam2 = "ENLACE_FTP";
        //                string tempRefParam3 = "IP3";
        //                strIP = mdlGlobales.funGetParam(ref tempRefParam, ref tempRefParam2, ref tempRefParam3);
        //                string tempRefParam4 = mdlGlobales.gcStrMasivosIni;
        //                string tempRefParam5 = "ENLACE_FTP";
        //                string tempRefParam6 = "PUERTO3";
        //                strPuerto = mdlGlobales.funGetParam(ref tempRefParam4, ref tempRefParam5, ref tempRefParam6);
        //            }
        //            else if (mdlTranMasivo.gstrIndicadorAmbiente == mdlTranMasivo.gcstrPruebas)
        //            {
        //                string tempRefParam7 = mdlGlobales.gcStrMasivosIni;
        //                string tempRefParam8 = "ENLACE_FTP";
        //                string tempRefParam9 = "IP2";
        //                strIP = mdlGlobales.funGetParam(ref tempRefParam7, ref tempRefParam8, ref tempRefParam9);
        //                string tempRefParam10 = mdlGlobales.gcStrMasivosIni;
        //                string tempRefParam11 = "ENLACE_FTP";
        //                string tempRefParam12 = "PUERTO2";
        //                strPuerto = mdlGlobales.funGetParam(ref tempRefParam10, ref tempRefParam11, ref tempRefParam12);
        //            }
        //            else if (mdlTranMasivo.gstrIndicadorAmbiente == mdlTranMasivo.gcstrDesarrollo)
        //            {
        //                string tempRefParam13 = mdlGlobales.gcStrMasivosIni;
        //                string tempRefParam14 = "ENLACE_FTP";
        //                string tempRefParam15 = "IP1";
        //                strIP = mdlGlobales.funGetParam(ref tempRefParam13, ref tempRefParam14, ref tempRefParam15);
        //                string tempRefParam16 = mdlGlobales.gcStrMasivosIni;
        //                string tempRefParam17 = "ENLACE_FTP";
        //                string tempRefParam18 = "PUERTO1";
        //                strPuerto = mdlGlobales.funGetParam(ref tempRefParam16, ref tempRefParam17, ref tempRefParam18);
        //            }

        //            string tempRefParam19 = mdlGlobales.gcStrMasivosIni;
        //            string tempRefParam20 = "ENLACE_FTPVC";
        //            string tempRefParam21 = "USUARIO";
        //            strUsuario = mdlGlobales.funGetParam(ref tempRefParam19, ref tempRefParam20, ref tempRefParam21);
        //            string tempRefParam22 = mdlGlobales.gcStrMasivosIni;
        //            string tempRefParam23 = "ENLACE_FTPVC";
        //            string tempRefParam24 = "PWD";
        //            strContrasena = mdlGlobales.funGetParam(ref tempRefParam22, ref tempRefParam23, ref tempRefParam24);
        //            string tempRefParam25 = mdlGlobales.gcStrMasivosIni;
        //            string tempRefParam26 = "ENLACE_FTPVC";
        //            string tempRefParam27 = "FTP";
        //            strFTPExe = mdlGlobales.funGetParam(ref tempRefParam25, ref tempRefParam26, ref tempRefParam27);
        //            string tempRefParam28 = mdlGlobales.gcStrMasivosIni;
        //            string tempRefParam29 = "ENLACE_FTPVC";
        //            string tempRefParam30 = "FILECMD";
        //            strArchivoCmd = mdlGlobales.funGetParam(ref tempRefParam28, ref tempRefParam29, ref tempRefParam30);
        //            string tempRefParam31 = mdlGlobales.gcStrMasivosIni;
        //            string tempRefParam32 = "ENLACE_FTPVC";
        //            string tempRefParam33 = "FILEBATCH";
        //            strArchivoBat = mdlGlobales.funGetParam(ref tempRefParam31, ref tempRefParam32, ref tempRefParam33);
        //            string tempRefParam34 = mdlGlobales.gcStrMasivosIni;
        //            string tempRefParam35 = "ENLACE_FTPVC";
        //            string tempRefParam36 = "FILELOG";
        //            strArchivoLog = mdlGlobales.funGetParam(ref tempRefParam34, ref tempRefParam35, ref tempRefParam36);
        //            string tempRefParam37 = mdlGlobales.gcStrMasivosIni;
        //            string tempRefParam38 = "ENLACE_FTPVC";
        //            string tempRefParam39 = "RUTA";
        //            strRutaBiracora = mdlGlobales.funGetParam(ref tempRefParam37, ref tempRefParam38, ref tempRefParam39);

        //            if (strIP == "unknown" || strPuerto == "unknown" || strUsuario == "unknown" || strContrasena == "unknown" || strFTPExe == "unknown" || strArchivoCmd == "unknown" || strArchivoBat == "unknown" || strArchivoLog == "unknown" || strRutaBiracora == "unknown")
        //            {
        //                string tempRefParam40 = "REVISE SU ARCHIVO DE PARÁMETROS (ENVIO FTP): " + mdlGlobales.gcStrMasivosIni;
        //                MsgBoxStyle tempRefParam41 = MsgBoxStyle.Information;
        //                string tempRefParam42 = "PARÁMETRO DESCONOCIDO";
        //                mdlGlobales.subDespErrores(ref tempRefParam40, ref tempRefParam41, ref tempRefParam42);
        //                return;
        //            }
        //        }
        //        else
        //        {
        //            //Crear el archivo de parámetros
        //            if (!mdlGlobales.funCreaMasivosIni())
        //            {
        //                //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
        //                string tempRefParam43 = "NO SE ENCUENTRA ARCHIVO DE PARÁMETROS Y NO PUDO SER CREADO (ENVIO FTP)";
        //                MsgBoxStyle tempRefParam44 = (MsgBoxStyle)(((int)MsgBoxStyle.Critical) + ((int)MsgBoxStyle.OkOnly));
        //                mdlGlobales.subDespErrores(ref tempRefParam43, ref tempRefParam44);
        //            }
        //            return;
        //        }

        //        //Asignar a los archivos las rutas del temporal
        //        strArchivoCmd = mdlGlobales.gstrRutaTemp + "\\" + strArchivoCmd;
        //        strArchivoBat = mdlGlobales.gstrRutaTemp + "\\" + strArchivoBat;
        //        strArchivoLog = mdlGlobales.gstrRutaTemp + "\\" + strArchivoLog;

        //        //Inicia el proceso de desencripción del usuario y el password
        //        intValorRet = mdlComunica.Inicia_Encripcion("", 0);
        //        strCadenaDes.Value = new string((char)255, 8);
        //        intValorRet = mdlComunica.D3Des(strUsuario, strCadenaDes.Value);
        //        strUsuario = strCadenaDes.Value;
        //        strCadenaDes.Value = new string((char)255, 8);
        //        intValorRet = mdlComunica.D3Des(strContrasena, strCadenaDes.Value);
        //        strContrasena = strCadenaDes.Value;
        //        intValorRet = 0;

        //        strArchivoEnviar = strArchivo;
        //        intEnviaArchivo = FileSystem.FreeFile().ToString();

        //        //PASO 1 DE 3 (JB-SAS 22/nov/2006) ARMA EL ARCHIVO DE COMANDOS DE FTP CON LOS QUE SE
        //        //ENVIARA EL ARCHIVO DE VENTAS CRUZADAS AL BUZON DE INTELAR
        //        FileSystem.FileOpen(Int32.Parse(intEnviaArchivo), strArchivoCmd, OpenMode.Output, OpenAccess.Default, OpenShare.Default, -1);
        //        FileSystem.PrintLine(Int32.Parse(intEnviaArchivo), "open " + strIP + " " + strPuerto);
        //        FileSystem.PrintLine(Int32.Parse(intEnviaArchivo), strUsuario);
        //        FileSystem.PrintLine(Int32.Parse(intEnviaArchivo), strContrasena);
        //        FileSystem.PrintLine(Int32.Parse(intEnviaArchivo), "binary ");
        //        FileSystem.PrintLine(Int32.Parse(intEnviaArchivo), "put " + strArchivoEnviar); //armar antes del llamado
        //        FileSystem.PrintLine(Int32.Parse(intEnviaArchivo), "bye ");
        //        FileSystem.FileClose(Int32.Parse(intEnviaArchivo));

        //        //PASO 2 DE 3 (JB-SAS 22/nov/2006) ARMA EL ARCHIVO DE COMANDOS DE BATCH QUE AL EJECUTARSE
        //        //ENVIARA POR FTP EL ARCHIVO DE VENTAS CRUZADAS AL BUZON DE INTELAR
        //        mdlGlobales.subDespMensajes("ENVIANDO ARCHIVO " + strArchivo.Substring(strArchivo.Length - Math.Min(strArchivo.Length, 26)) + ", POR FAVOR ESPERE...");
        //        FileSystem.FileOpen(Int32.Parse(intEnviaArchivo), strArchivoBat, OpenMode.Output, OpenAccess.Default, OpenShare.Default, -1);
        //        FileSystem.PrintLine(Int32.Parse(intEnviaArchivo), strFTPExe + strArchivoCmd + " > " + strArchivoLog);
        //        FileSystem.FileClose(Int32.Parse(intEnviaArchivo));

        //        //PASO 3 DE 3 (JB-SAS 22/nov/2006) EJECUTA POR SHELL EL ARCHIVO DE COMANDOS DE BATCH PARA
        //        //ENVIAR POR FTP EL ARCHIVO DE VENTAS CRUZADAS AL BUZON DE INTELAR
        //        intNumProceso = 0;
        //        //UPGRADE_WARNING: (7005) parameters (if any) must be set using the Arguments property of ProcessStartInfo
        //        ProcessStartInfo startInfo = new ProcessStartInfo(strArchivoBat);
        //        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //        intValorRet = Process.Start(startInfo).Id;

        //        //MIG WXP INI JGC 20090825
        //        string strMASArchivo2 = mdlRegistry.RegistryGlobales("MASArchivo2");
        //        //MIG WXP FIN JGC 20090825
        //        //intNumProceso = mdlGlobales.FindWindow("", "C:\\WINNT\\SYSTEM32\\CMD.EXE");
        //        intNumProceso = mdlGlobales.FindWindow("", strMASArchivo2);

        //        //CONTROL DE RESPUESTA DE FTP (NO MAYOR A 13 SEGUNDOS...???)

        //        while (intNumProceso == 0 || TIEMPO > 13000)
        //        {
        //            if (TIEMPO > 13000)
        //            {
        //                break;
        //            }
        //            Application.DoEvents();

        //            //MIG WXP INI JGC 20090825
        //            //intNumProceso = mdlGlobales.FindWindow("", "C: WINNT\\SYSTEM32\\CMD.EXE");
        //            intNumProceso = mdlGlobales.FindWindow("", strMASArchivo2);
        //            //MIG WXP FIN JGC 20090825

        //            TIEMPO++;
        //        };

        //        while (intNumProceso > 0)
        //        {

        //            //MIG WXP INI JGC 20090825
        //            //intNumProceso = mdlGlobales.FindWindow("", "C:\\WINNT\\SYSTEM32\\CMD.EXE");
        //            intNumProceso = mdlGlobales.FindWindow("", strMASArchivo2);
        //            //MIG WXP FIN JGC 20090825

        //            Application.DoEvents();
        //        }

        //        //PROCESA CONTROL DE RESPUESTA DE FTP
        //        blnAscci = false;
        //        blnConexion = false;
        //        blnCerrar = false;
        //        blnTransferencia = false;
        //        blnEntro = false;
        //        blnMalPassword = false;
        //        mdlGlobales.subDespMensajes("VALIDANDO ENVIO ...");

        //        //BUSCA HASTA ENCONTRAR EL ARCHIVO DE LOG
        //        while (FileSystem.Dir(strArchivoLog, FileAttribute.Normal) == "")
        //        {

        //            strArchivoLog = strArchivoLog;
        //        }

        //        //ABRE EL ARCHIVO DE LOG PARA SU REVISION
        //        FileSystem.FileOpen(Int32.Parse(intEnviaArchivo), strArchivoLog, OpenMode.Input, OpenAccess.Default, OpenShare.Default, -1);
        //        while (!FileSystem.EOF(Int32.Parse(intEnviaArchivo)))
        //        {

        //            FileSystem.Input(Int32.Parse(intEnviaArchivo), ref strCadenaSalida);
        //            if (strCadenaSalida.StartsWith("150"))
        //            { // Puerto Abierto en Ascii Ok
        //                blnAscci = true;
        //            }
        //            if (strCadenaSalida.StartsWith("220"))
        //            { // Conección OK
        //                blnConexion = true;
        //            }
        //            if (strCadenaSalida.StartsWith("221"))
        //            { // Cerrar sesion Ok
        //                blnCerrar = true;
        //            }
        //            if (strCadenaSalida.StartsWith("226"))
        //            { // Transferencia Completa Ok
        //                blnTransferencia = true;
        //            }
        //            if (strCadenaSalida.StartsWith("230"))
        //            { // IP correcta Ok
        //                blnEntro = true;
        //            }
        //            if (strCadenaSalida.StartsWith("530"))
        //            { // Dentro del Server Ok
        //                blnMalPassword = true;
        //            }
        //        }
        //        FileSystem.FileClose(Int32.Parse(intEnviaArchivo));


        //        strCadMensaje = DateTime.Today.ToString("dd/MM/yyyy") + " " + DateTime.Now.AddDays(-DateTime.Now.Date.ToOADate()).ToString("HH:mm");
        //        mdlGlobales.subDespMensajes("  ");
        //        if (blnAscci && blnConexion && blnCerrar && blnTransferencia && blnEntro && !blnMalPassword)
        //        {
        //            strCadMensaje = strCadMensaje + " ACEPTADO  -> ";
        //            //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
        //            string tempRefParam45 = "EL ARCHIVO " + frmVentasCruzadas.DefInstance.txtArchivo.Text + " HA SIDO ENVIADO SATISFACTORIAMENTE";
        //            MsgBoxStyle tempRefParam46 = (MsgBoxStyle)(((int)MsgBoxStyle.Information) + ((int)MsgBoxStyle.OkOnly));
        //            mdlGlobales.subDespErrores(ref tempRefParam45, ref tempRefParam46);
        //        }
        //        else
        //        {
        //            strCadMensaje = strCadMensaje + " RECHAZADO -> ";
        //            //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
        //            string tempRefParam47 = "NO SE PUDO ENVIAR ARCHIVO -PROB. DE ENVIO A INTELAR- REVISAR MENSAJES EN DIRECTORIO: " + strRutaBiracora;
        //            MsgBoxStyle tempRefParam48 = (MsgBoxStyle)(((int)MsgBoxStyle.Exclamation) + ((int)MsgBoxStyle.OkOnly));
        //            mdlGlobales.subDespErrores(ref tempRefParam47, ref tempRefParam48);
        //        }
        //        frmVentasCruzadas.DefInstance.subConfigIniVentasCruzadas();
        //        if (blnEntro)
        //        {
        //            strCadMensaje = strCadMensaje + "DIRECCION IP VALIDA   / ";
        //        }
        //        else
        //        {
        //            strCadMensaje = strCadMensaje + "DIRECCION IP NO VALIDA/ ";
        //        }
        //        if (!blnMalPassword)
        //        {
        //            strCadMensaje = strCadMensaje + "CONTRASEÑA Y USUARIO CORRECTO/ ";
        //        }
        //        else
        //        {
        //            strCadMensaje = strCadMensaje + "CONTRASEÑA O USUARIO ERRONEO / ";
        //        }
        //        if (blnConexion)
        //        {
        //            strCadMensaje = strCadMensaje + "CONEXION ACEPTADA / ";
        //        }
        //        else
        //        {
        //            strCadMensaje = strCadMensaje + "CONEXION RECHAZADA/ ";
        //        }
        //        if (blnAscci)
        //        {
        //            strCadMensaje = strCadMensaje + "ENVIO ASCII CORRECTO/ ";
        //        }
        //        else
        //        {
        //            strCadMensaje = strCadMensaje + "ENVIO ASCII ERRONEO / ";
        //        }
        //        if (blnTransferencia)
        //        {
        //            strCadMensaje = strCadMensaje + "TRANSFERENCIA CORRECTA/ ";
        //        }
        //        else
        //        {
        //            strCadMensaje = strCadMensaje + "TRANSFERENCIA ERRONEA / ";
        //        }
        //        if (blnCerrar)
        //        {
        //            strCadMensaje = strCadMensaje + "CERRAR SESION CORRECTO/ ";
        //        }
        //        else
        //        {
        //            strCadMensaje = strCadMensaje + "CERRAR SESION ERRONEA / ";
        //        }

        //        //MIG WXP INI JGC 20090825
        //        string strMASArchivo3 = mdlRegistry.RegistryGlobales("MASArchivo3");
        //        //MIG WXP FIN JGC 20090825
        //        //strArchivo = strRutaBiracora + "\\FTP_VC_" + mdlTranMasivo.gvstrFechaProceso + ".txt";
        //        strArchivo = strRutaBiracora + strMASArchivo3 + mdlTranMasivo.gvstrFechaProceso + ".txt";

        //        if (FileSystem.Dir(mdlGlobales.gcStrMasivosIni, FileAttribute.Normal) == "")
        //        {
        //            //MIG WXP INI JGC 20090825
        //            //strArchivo = "\\FTP_VC_" + mdlTranMasivo.gvstrFechaProceso + ".txt";
        //            strArchivo = strMASArchivo3 + mdlTranMasivo.gvstrFechaProceso + ".txt";
        //            //MIG WXP FIN JGC 20090825

        //        }
        //        strCadMensaje = strCadMensaje;
        //        FileSystem.FileOpen(Int32.Parse(intEnviaArchivo), strArchivo, OpenMode.Append, OpenAccess.Default, OpenShare.Default, -1);
        //        FileSystem.PrintLine(Int32.Parse(intEnviaArchivo), strCadMensaje);
        //        FileSystem.FileClose(Int32.Parse(intEnviaArchivo));
        //        mdlGlobales.subDespMensajes("");
        //    }
        //    catch (Exception excep)
        //    {

        //        if (Information.Err().Number == 53 || Information.Err().Number == 70)
        //        {
        //            //UPGRADE_TODO: (1065) Error handling statement (Resume) could not be converted properly. A throw statement was generated instead.
        //            throw new Exception("Migration Exception: 'Resume' not supported");
        //        }
        //        else
        //        {
        //            //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
        //            string tempRefParam49 = "AVISO " + Information.Err().Number.ToString() + " : " + excep.Message;
        //            MsgBoxStyle tempRefParam50 = (MsgBoxStyle)(((int)MsgBoxStyle.Exclamation) + ((int)MsgBoxStyle.OkOnly));
        //            mdlGlobales.subDespErrores(ref tempRefParam49, ref tempRefParam50);
        //            return;
        //        }
        //        //UPGRADE_WARNING: (1041) Return has a new behavior.
        //        return;
        //    }
        //}
    }
}