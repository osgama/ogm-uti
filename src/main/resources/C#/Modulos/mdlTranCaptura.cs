using Artinsoft.VB6.Utils;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility.VB6;
using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using VB6 = Microsoft.VisualBasic.Compatibility.VB6.Support;

namespace Masivos
{
    class mdlTranCaptura
    {


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct udtPromotora01
        { //HEADER DE ENVIO PARA DIALOGOS AL ANALISIS
            public FixedLengthString strTipoReg;
            public FixedLengthString strFolio;
            public FixedLengthString strRFCPromotor;
            public FixedLengthString strFirmaSol;
            public FixedLengthString strLugar;
            public FixedLengthString strFecSolicitud;
            public FixedLengthString strLimiteCredito;
            public FixedLengthString strNombre;
            public FixedLengthString strPaterno;
            public FixedLengthString strMaterno;
            public FixedLengthString strDomicilio;
            public FixedLengthString strColonia;
            public FixedLengthString strMunicipio;
            public FixedLengthString strEstado;
            public FixedLengthString strCodigoPostal;
            public FixedLengthString strLada;
            public FixedLengthString strTelefono;
            public FixedLengthString strFecNacimiento;
            public FixedLengthString strAnosResidir;
            public FixedLengthString strNumDependientes;
            public FixedLengthString strSexo;
            public FixedLengthString strTipoVivienda;
            public FixedLengthString strEstadoCivil;
            public FixedLengthString strEscolaridad;
            public FixedLengthString strRFC;
            public FixedLengthString strPNacimiento; //AEFS Dato Nuevo
            public FixedLengthString strPNacionalidad; //AEFS Dato Nuevo
            public FixedLengthString strFIEL; //AEFS Dato Nuevo
            //*** INI - IRP – Proy. 66008-06
            public FixedLengthString strEntFedNac; 
            //*** FIN - IRP – Proy. 66008-06
            //MODIF MAP ART.115 2016
            public FixedLengthString strIdentFis1;
            public FixedLengthString strPaisAsig1;
            public FixedLengthString strIdentFis2;
            public FixedLengthString strPaisAsig2;
            public FixedLengthString strFechaCons;
            public FixedLengthString strGiroEmpre;
            public FixedLengthString strTrabExtra;
            public FixedLengthString strTipoPerso;

            public static udtPromotora01 CreateInstance()
            {
                udtPromotora01 result = new udtPromotora01();
                result.strTipoReg = new FixedLengthString(2);
                result.strFolio = new FixedLengthString(8);
                result.strRFCPromotor = new FixedLengthString(10);
                result.strFirmaSol = new FixedLengthString(1);
                result.strLugar = new FixedLengthString(20);
                result.strFecSolicitud = new FixedLengthString(6);
                result.strLimiteCredito = new FixedLengthString(8);
                result.strNombre = new FixedLengthString(50); //AEFS Cambia de 30 a 50
                result.strPaterno = new FixedLengthString(60); //AEFS Cambia de 30 a 60
                result.strMaterno = new FixedLengthString(60); //AEFS Cambia de 30 a 60
                result.strDomicilio = new FixedLengthString(35);
                result.strColonia = new FixedLengthString(35);
                result.strMunicipio = new FixedLengthString(26);
                result.strEstado = new FixedLengthString(4);
                result.strCodigoPostal = new FixedLengthString(5);
                result.strLada = new FixedLengthString(3);
                result.strTelefono = new FixedLengthString(7);
                result.strFecNacimiento = new FixedLengthString(6);
                result.strAnosResidir = new FixedLengthString(4);
                result.strNumDependientes = new FixedLengthString(2);
                result.strSexo = new FixedLengthString(1);
                result.strTipoVivienda = new FixedLengthString(1);
                result.strEstadoCivil = new FixedLengthString(1);
                result.strEscolaridad = new FixedLengthString(2);
                result.strRFC = new FixedLengthString(13);
                result.strPNacimiento = new FixedLengthString(4); //AEFS Dato Nuevo
                result.strPNacionalidad = new FixedLengthString(4); //AEFS Dato Nuevo
                result.strFIEL = new FixedLengthString(20); //AEFS Dato Nuevo
                //*** INI - IRP – Proy. 66008-06
                result.strEntFedNac = new FixedLengthString(2);
                //*** FIN - IRP – Proy. 66008-06
                //MODIF MAP ART.115 2016               
                result.strIdentFis1 = new FixedLengthString(20);
                result.strPaisAsig1 = new FixedLengthString(4);
                result.strIdentFis2 = new FixedLengthString(20);
                result.strPaisAsig2 = new FixedLengthString(4);
                result.strFechaCons = new FixedLengthString(8);
                result.strGiroEmpre = new FixedLengthString(6);
                result.strTrabExtra = new FixedLengthString(2);
                result.strTipoPerso = new FixedLengthString(2);
                return result;
            }
        }
        static public udtPromotora01 estProm01 = udtPromotora01.CreateInstance();

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct udtPromotora03
        { //Datos Personales
            public FixedLengthString strTipoReg;
            public FixedLengthString strFolio;
            public FixedLengthString strCodigoPostal;
            public FixedLengthString strLada;
            public FixedLengthString strAntiguedad;
            public FixedLengthString strOcupacion;
            public FixedLengthString strEstado;
            public FixedLengthString strTelefono;
            public FixedLengthString strExtension;
            public FixedLengthString strNomEmpresa;
            public FixedLengthString strDomicilio;
            public FixedLengthString strColPobFracc;
            public FixedLengthString strMunicipio;
            public FixedLengthString strDeptoArea;

            public static udtPromotora03 CreateInstance()
            {
                udtPromotora03 result = new udtPromotora03();
                result.strTipoReg = new FixedLengthString(2);
                result.strFolio = new FixedLengthString(8);
                result.strCodigoPostal = new FixedLengthString(5);
                result.strLada = new FixedLengthString(3);
                result.strAntiguedad = new FixedLengthString(4);
                result.strOcupacion = new FixedLengthString(4); //AEFS cambia de 3 a 4
                result.strEstado = new FixedLengthString(4);
                result.strTelefono = new FixedLengthString(7);
                result.strExtension = new FixedLengthString(4);
                result.strNomEmpresa = new FixedLengthString(40);
                result.strDomicilio = new FixedLengthString(65);
                result.strColPobFracc = new FixedLengthString(35);
                result.strMunicipio = new FixedLengthString(26);
                result.strDeptoArea = new FixedLengthString(20);
                return result;
            }
        }
        static public udtPromotora03 estProm03 = udtPromotora03.CreateInstance();

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct udtPromotora05
        { //Datos del Conyugue
            public FixedLengthString strTipoReg;
            public FixedLengthString strFolio;
            public FixedLengthString strNombre;
            public FixedLengthString strPaterno;
            public FixedLengthString strMaterno;
            public FixedLengthString strCodigoPostal;
            public FixedLengthString strLada;
            public FixedLengthString strExtension;
            public FixedLengthString strTelefono;
            public FixedLengthString strEstado;
            public FixedLengthString strNombreEmp;
            public FixedLengthString strDomicilio;
            public FixedLengthString strColPobFracc;
            public FixedLengthString strMunicipio;

            public static udtPromotora05 CreateInstance()
            {
                udtPromotora05 result = new udtPromotora05();
                result.strTipoReg = new FixedLengthString(2);
                result.strFolio = new FixedLengthString(8);
                result.strNombre = new FixedLengthString(50); //AEFS Cambia de 30 a 50
                result.strPaterno = new FixedLengthString(60); //AEFS Cambia de 30 a 60
                result.strMaterno = new FixedLengthString(60); //AEFS Cambia de 30 a 60
                result.strCodigoPostal = new FixedLengthString(5);
                result.strLada = new FixedLengthString(3);
                result.strExtension = new FixedLengthString(4);
                result.strTelefono = new FixedLengthString(7);
                result.strEstado = new FixedLengthString(4);
                result.strNombreEmp = new FixedLengthString(40);
                result.strDomicilio = new FixedLengthString(35);
                result.strColPobFracc = new FixedLengthString(35);
                result.strMunicipio = new FixedLengthString(26);
                return result;
            }
        }
        static public udtPromotora05 estProm05 = udtPromotora05.CreateInstance();

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct udtPromotora06
        { // Referencias Familiares
            public FixedLengthString strTipoReg;
            public FixedLengthString strFolio;
            public FixedLengthString strNombre;
            public FixedLengthString strPaterno;
            public FixedLengthString strMaterno;
            public FixedLengthString strParentesco;
            public FixedLengthString strCodigoPostal;
            public FixedLengthString strTelefono;
            public FixedLengthString strLada;
            public FixedLengthString strEstado;
            public FixedLengthString strDomicilio;
            public FixedLengthString strColPobFracc;
            public FixedLengthString strMunicipio;

            public static udtPromotora06 CreateInstance()
            {
                udtPromotora06 result = new udtPromotora06();
                result.strTipoReg = new FixedLengthString(2);
                result.strFolio = new FixedLengthString(8);
                result.strNombre = new FixedLengthString(50); //AEFS Cambia de 30 a 50
                result.strPaterno = new FixedLengthString(60); //AEFS Cambia de 30 a 60
                result.strMaterno = new FixedLengthString(60); //AEFS Cambia de 30 a 60
                result.strParentesco = new FixedLengthString(1);
                result.strCodigoPostal = new FixedLengthString(5);
                result.strTelefono = new FixedLengthString(7);
                result.strLada = new FixedLengthString(3);
                result.strEstado = new FixedLengthString(4);
                result.strDomicilio = new FixedLengthString(35);
                result.strColPobFracc = new FixedLengthString(35);
                result.strMunicipio = new FixedLengthString(26);
                return result;
            }
        }
        static public udtPromotora06[] estProm06 = ArraysHelper.InitializeArray<udtPromotora06[]>(new int[] { 2 }); //Se almacenan 2 ya que el archivo puede contener 2 referencias personales

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct udtPromotora07
        { // Referencias Banamex
            public FixedLengthString strTipoReg;
            public FixedLengthString strFolio;
            public FixedLengthString strSucursal;
            public FixedLengthString strNumCuenta;
            public FixedLengthString strTipoServicio;

            public static udtPromotora07 CreateInstance()
            {
                udtPromotora07 result = new udtPromotora07();
                result.strTipoReg = new FixedLengthString(2);
                result.strFolio = new FixedLengthString(8);
                result.strSucursal = new FixedLengthString(4);
                result.strNumCuenta = new FixedLengthString(16);
                result.strTipoServicio = new FixedLengthString(2);
                return result;
            }
        }
        static public udtPromotora07 estProm07 = udtPromotora07.CreateInstance();

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct udtPromotora08
        { // Referencias Comerciales Bancarias
            public FixedLengthString strTipoReg;
            public FixedLengthString strFolio;
            public FixedLengthString strEmisor;
            public FixedLengthString strNumCuenta;
            public FixedLengthString strTipoServicio;

            public static udtPromotora08 CreateInstance()
            {
                udtPromotora08 result = new udtPromotora08();
                result.strTipoReg = new FixedLengthString(2);
                result.strFolio = new FixedLengthString(8);
                result.strEmisor = new FixedLengthString(3);
                result.strNumCuenta = new FixedLengthString(16);
                result.strTipoServicio = new FixedLengthString(2);
                return result;
            }
        }
        static public udtPromotora08[] estProm08 = ArraysHelper.InitializeArray<udtPromotora08[]>(new int[] { 2 });

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct udtPromotora09
        { // Tarjetas de Credito
            public FixedLengthString strTipoReg;
            public FixedLengthString strFolio;
            public FixedLengthString strEmisor;
            public FixedLengthString strNumCuenta;

            public static udtPromotora09 CreateInstance()
            {
                udtPromotora09 result = new udtPromotora09();
                result.strTipoReg = new FixedLengthString(2);
                result.strFolio = new FixedLengthString(8);
                result.strEmisor = new FixedLengthString(3);
                result.strNumCuenta = new FixedLengthString(16);
                return result;
            }
        }
        static public udtPromotora09[] estProm09 = ArraysHelper.InitializeArray<udtPromotora09[]>(new int[] { 2 });

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct udtPromotora10
        {
            public FixedLengthString strTipoReg;
            public FixedLengthString strFolio;
            public FixedLengthString strTipoPropiedad;
            public FixedLengthString strAño2;
            public FixedLengthString strAño1;
            public FixedLengthString strMarca2;
            public FixedLengthString strMarca1;

            public static udtPromotora10 CreateInstance()
            {
                udtPromotora10 result = new udtPromotora10();
                result.strTipoReg = new FixedLengthString(2);
                result.strFolio = new FixedLengthString(8);
                result.strTipoPropiedad = new FixedLengthString(1);
                result.strAño2 = new FixedLengthString(2);
                result.strAño1 = new FixedLengthString(2);
                result.strMarca2 = new FixedLengthString(20);
                result.strMarca1 = new FixedLengthString(20);
                return result;
            }
        }
        static public udtPromotora10 estProm10 = udtPromotora10.CreateInstance();

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct udtPromotora11
        { //Ingresos Egresos
            public FixedLengthString strTipoReg;
            public FixedLengthString strFolio;
            public FixedLengthString strIngfijos;
            public FixedLengthString strIngComisiones;
            public FixedLengthString strIngConyugue;
            public FixedLengthString strIngInversiones;
            public FixedLengthString strIngHonorarios;
            public FixedLengthString strIngOtros;
            public FixedLengthString strEgrGastoFam;
            public FixedLengthString strEgrPagoAdeudo;
            public FixedLengthString strEgrOtros;

            public static udtPromotora11 CreateInstance()
            {
                udtPromotora11 result = new udtPromotora11();
                result.strTipoReg = new FixedLengthString(2);
                result.strFolio = new FixedLengthString(8);
                result.strIngfijos = new FixedLengthString(8);
                result.strIngComisiones = new FixedLengthString(8);
                result.strIngConyugue = new FixedLengthString(8);
                result.strIngInversiones = new FixedLengthString(8);
                result.strIngHonorarios = new FixedLengthString(8);
                result.strIngOtros = new FixedLengthString(8);
                result.strEgrGastoFam = new FixedLengthString(8);
                result.strEgrPagoAdeudo = new FixedLengthString(8);
                result.strEgrOtros = new FixedLengthString(8);
                return result;
            }
        }
        static public udtPromotora11 estProm11 = udtPromotora11.CreateInstance();

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct udtPromotora12
        { //Tarjetas Adicionales
            public FixedLengthString strTipoReg;
            public FixedLengthString strFolio;
            public FixedLengthString strNombre;
            public FixedLengthString strPaterno;
            public FixedLengthString strMaterno;
            public FixedLengthString strParentesco;
            public FixedLengthString strFecNacimiento;
            public FixedLengthString strSexo;
            public FixedLengthString strFirmaAdic;

            public static udtPromotora12 CreateInstance()
            {
                udtPromotora12 result = new udtPromotora12();
                result.strTipoReg = new FixedLengthString(2);
                result.strFolio = new FixedLengthString(8);
                result.strNombre = new FixedLengthString(50); //AEFS Cambia de 30 a 50
                result.strPaterno = new FixedLengthString(60); //AEFS Cambia de 30 a 60
                result.strMaterno = new FixedLengthString(60); //AEFS Cambia de 30 a 60
                result.strParentesco = new FixedLengthString(1);
                result.strFecNacimiento = new FixedLengthString(8);
                result.strSexo = new FixedLengthString(1);
                result.strFirmaAdic = new FixedLengthString(1);
                return result;
            }
        }
        static public udtPromotora12 estProm12 = udtPromotora12.CreateInstance();

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct udtPromotora13
        { //Tarjetas Adicionales
            public FixedLengthString strTipoReg;
            public FixedLengthString strFolio;
            public FixedLengthString strSucursal;
            public FixedLengthString strNomEjecutivo;

            public static udtPromotora13 CreateInstance()
            {
                udtPromotora13 result = new udtPromotora13();
                result.strTipoReg = new FixedLengthString(2);
                result.strFolio = new FixedLengthString(8);
                result.strSucursal = new FixedLengthString(4);
                result.strNomEjecutivo = new FixedLengthString(8);
                return result;
            }
        }
        static public udtPromotora13 estProm13 = udtPromotora13.CreateInstance();

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct udtPromotora14
        { //Obligado Solidario
            public FixedLengthString strTipoReg;
            public FixedLengthString strFolio;
            public FixedLengthString strNombre;
            public FixedLengthString strPaterno;
            public FixedLengthString strMaterno;
            public FixedLengthString strDomicilio;
            public FixedLengthString strColonia;
            public FixedLengthString strMunicipio;
            public FixedLengthString strEstado;
            public FixedLengthString strFirma;
            public FixedLengthString strIngMensuales;
            public FixedLengthString strCodigoPostal;
            public FixedLengthString strTelefono;
            public FixedLengthString strLada;
            public FixedLengthString strExtension;
            public FixedLengthString strAntiguedad;
            public FixedLengthString strOcupacion;
            public FixedLengthString strNomEmpresa;

            public static udtPromotora14 CreateInstance()
            {
                udtPromotora14 result = new udtPromotora14();
                result.strTipoReg = new FixedLengthString(2);
                result.strFolio = new FixedLengthString(8);
                result.strNombre = new FixedLengthString(50); //AEFS Cambia de 30 a 50
                result.strPaterno = new FixedLengthString(60); //AEFS Cambia de 30 a 60
                result.strMaterno = new FixedLengthString(60); //AEFS Cambia de 30 a 60
                result.strDomicilio = new FixedLengthString(35);
                result.strColonia = new FixedLengthString(35);
                result.strMunicipio = new FixedLengthString(26);
                result.strEstado = new FixedLengthString(4);
                result.strFirma = new FixedLengthString(1);
                result.strIngMensuales = new FixedLengthString(8);
                result.strCodigoPostal = new FixedLengthString(5);
                result.strTelefono = new FixedLengthString(7);
                result.strLada = new FixedLengthString(3);
                result.strExtension = new FixedLengthString(4);
                result.strAntiguedad = new FixedLengthString(4);
                result.strOcupacion = new FixedLengthString(3);
                result.strNomEmpresa = new FixedLengthString(40);
                return result;
            }
        }
        static public udtPromotora14 estProm14 = udtPromotora14.CreateInstance();

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct udtPromotora15
        { //Obligado Solidario
            public FixedLengthString strTipoReg;
            public FixedLengthString strFolio;
            public FixedLengthString strCompIdentificacion;
            public FixedLengthString strCompDomicilio;
            public FixedLengthString strCompIngresos;
            public FixedLengthString strDescIdentificacion;
            public FixedLengthString strIdentCatalogo;

            public static udtPromotora15 CreateInstance()
            {
                udtPromotora15 result = new udtPromotora15();
                result.strTipoReg = new FixedLengthString(2);
                result.strFolio = new FixedLengthString(8);
                result.strCompIdentificacion = new FixedLengthString(1);
                result.strCompDomicilio = new FixedLengthString(1);
                result.strCompIngresos = new FixedLengthString(1);
                result.strDescIdentificacion = new FixedLengthString(20);
                result.strIdentCatalogo = new FixedLengthString(2);
                return result;
            }
        }
        static public udtPromotora15 estProm15 = udtPromotora15.CreateInstance();

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct udtPromotora16
        { //Datos Adicionales delSolicitante
            public FixedLengthString strTipoReg;
            public FixedLengthString strFolio;
            public FixedLengthString strNumCliente;
            public FixedLengthString strNacionalidad;
            public FixedLengthString strSector;
            public FixedLengthString strFtesOtrosIng;
            public FixedLengthString strFecNacimiento;
            public FixedLengthString strProfOfic;

            public static udtPromotora16 CreateInstance()
            {
                udtPromotora16 result = new udtPromotora16();
                result.strTipoReg = new FixedLengthString(2);
                result.strFolio = new FixedLengthString(8);
                result.strNumCliente = new FixedLengthString(12);
                result.strNacionalidad = new FixedLengthString(2);
                result.strSector = new FixedLengthString(2);
                result.strFtesOtrosIng = new FixedLengthString(1);
                result.strFecNacimiento = new FixedLengthString(8);
                result.strProfOfic = new FixedLengthString(30);
                return result;
            }
        }
        static public udtPromotora16 estProm16 = udtPromotora16.CreateInstance();

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct udtPromotora17
        { //Datos Adicionales del Obligado
            public FixedLengthString strTipoReg;
            public FixedLengthString strFolio;
            public FixedLengthString strReferencia;
            public FixedLengthString strSucursal;
            public FixedLengthString strNumCuenta;
            public FixedLengthString strRFC;
            public FixedLengthString strEmisorTarjeta1;
            public FixedLengthString strEmisorTarjeta2;
            public FixedLengthString strNumTarjeta1;
            public FixedLengthString strNumTarjeta2;
            public FixedLengthString strOtrosIngresos;
            public FixedLengthString strFecNacimiento;

            public static udtPromotora17 CreateInstance()
            {
                udtPromotora17 result = new udtPromotora17();
                result.strTipoReg = new FixedLengthString(2);
                result.strFolio = new FixedLengthString(8);
                result.strReferencia = new FixedLengthString(1);
                result.strSucursal = new FixedLengthString(2);
                result.strNumCuenta = new FixedLengthString(1);
                result.strRFC = new FixedLengthString(13);
                result.strEmisorTarjeta1 = new FixedLengthString(2);
                result.strEmisorTarjeta2 = new FixedLengthString(2);
                result.strNumTarjeta1 = new FixedLengthString(16);
                result.strNumTarjeta2 = new FixedLengthString(16);
                result.strOtrosIngresos = new FixedLengthString(8);
                result.strFecNacimiento = new FixedLengthString(8);
                return result;
            }
        }
        static public udtPromotora17 estProm17 = udtPromotora17.CreateInstance();

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct udtPromotora18
        { //Datos Adicionales del Obligado
            public FixedLengthString strTipoReg;
            public FixedLengthString strFolio;
            public FixedLengthString strProducto;
            public FixedLengthString strRFCPromotor;
            public FixedLengthString strEstado;
            public FixedLengthString strSucursal;
            public FixedLengthString strCanalVta;
            //AIS-Bug 9453 FSABORIO
            //MODIF JGC 21/07/2008 FALTO MODIFICAR ESTA LINEA
            //strMedio              As String * 3
            public FixedLengthString strMedio;

            public static udtPromotora18 CreateInstance()
            {
                udtPromotora18 result = new udtPromotora18();
                result.strTipoReg = new FixedLengthString(2);
                result.strFolio = new FixedLengthString(8);
                result.strProducto = new FixedLengthString(4);
                result.strRFCPromotor = new FixedLengthString(10);
                result.strEstado = new FixedLengthString(2);
                result.strSucursal = new FixedLengthString(4);
                result.strCanalVta = new FixedLengthString(2);
                result.strMedio = new FixedLengthString(4);
                return result;
            }
        }
        static public udtPromotora18 estProm18 = udtPromotora18.CreateInstance();

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct udtPromotora19
        { //Datos Adicionales del Obligado
            public FixedLengthString strTipoReg;
            public FixedLengthString strFolio;
            public FixedLengthString strPlazo;
            public FixedLengthString strNumCuenta;
            public FixedLengthString strCURP; //ASG El layout de promotoras lo incluye
            public FixedLengthString strCorreoE; //VAR 07 Septiembre 2005 Proyecto 20410 Promociones. Se modifica la longitud a 40 que es la que tiene el campo en la Base de Datos.
            public FixedLengthString strCvePromocion; //VAR 04 Abril 2005 proyecto 20410 promociones Modificado de 8 a 12 para 2o alcance
            public FixedLengthString strTipoComision;

            public static udtPromotora19 CreateInstance()
            {
                udtPromotora19 result = new udtPromotora19();
                result.strTipoReg = new FixedLengthString(2);
                result.strFolio = new FixedLengthString(8);
                result.strPlazo = new FixedLengthString(4);
                result.strNumCuenta = new FixedLengthString(16);
                result.strCURP = new FixedLengthString(18); //ASG El layout de promotoras lo incluye //AEFS cambia long de 13 a 18
                result.strCorreoE = new FixedLengthString(78); //VAR 07 Septiembre 2005 Proyecto 20410 Promociones. Se modifica la longitud a 40 que es la que tiene el campo en la Base de Datos. //AEFS cambia long de 40 a 78
                result.strCvePromocion = new FixedLengthString(12); //VAR 04 Abril 2005 proyecto 20410 promociones Modificado de 8 a 12 para 2o alcance
                result.strTipoComision = new FixedLengthString(2);
                return result;
            }
        }
        static public udtPromotora19 estProm19 = udtPromotora19.CreateInstance();

        //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        //public struct udtPromotora21
        //{
        //    public FixedLengthString strTipoRegis;
        //    public FixedLengthString strFolioPrei;
        //    public FixedLengthString strIdentFis1;
        //    public FixedLengthString strPaisAsig1;
        //    public FixedLengthString strIdentFis2;
        //    public FixedLengthString strPaisAsig2;
        //    public FixedLengthString strFechaCons;
        //    public FixedLengthString strGiroEmpre;
        //    public FixedLengthString strTrabExtra;

        //    public static udtPromotora21 CreateInstance()
        //    {
        //        udtPromotora21 result = new udtPromotora21();
        //        result.strTipoRegis = new FixedLengthString(2);
        //        result.strFolioPrei = new FixedLengthString(8);
        //        result.strIdentFis1 = new FixedLengthString(20);
        //        result.strPaisAsig1 = new FixedLengthString(4);
        //        result.strIdentFis2 = new FixedLengthString(20); 
        //        result.strPaisAsig2 = new FixedLengthString(4); 
        //        result.strFechaCons = new FixedLengthString(8);
        //        result.strGiroEmpre = new FixedLengthString(6);
        //        result.strTrabExtra = new FixedLengthString(2);
        //        return result;
        //    }
        //}
        //static public udtPromotora21 estProm21 = udtPromotora21.CreateInstance();

     
        // ************************************
        // Header para transacción de Captura
        // ************************************
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct udtHeader5595
        {
            public FixedLengthString strHDCveTran;
            public FixedLengthString strHDFiller01;
            public FixedLengthString strHDSubTran;
            public FixedLengthString strHDFolPreimpreso;
            public FixedLengthString strHDFolInterno;
            public FixedLengthString strHDSistOrigen;
            public FixedLengthString strHDTramite;
            public FixedLengthString strHDEntOrig;
            public FixedLengthString strHDGpoEntOrig; //PRAXIS/ASG 20040204 Se aumenta la longitud de 2 a 4 y se elimina el campo numgpo
            public FixedLengthString strHDCveEntOrig;
            public FixedLengthString strHDEstatus; //MMS 11/05 Incremento en la longitud del campo (2 a 3)
            public FixedLengthString strHDCveResp;
            public FixedLengthString strHDDescResp;
            public FixedLengthString strHDNominaOper;
            public FixedLengthString strHDCvePaqEval;
            public FixedLengthString strHDCveProceso; //MMS 11/05 Incremento en la longitud del campo (2 a 3)
            public FixedLengthString strHDFlagInfo;
            public FixedLengthString strHDCveRechazo;
            public FixedLengthString strHDPantalla;
            public FixedLengthString strHDNumMapa; //MMS 11/05 Incremento en la longitud del campo (2 a 3)
            public FixedLengthString strHDProcIni; //MMS 11/05 Incremento en la longitud del campo (2 a 3)
            public FixedLengthString strHDFiller02; //MMS 11/05 Reducción del filler

            public static udtHeader5595 CreateInstance()
            {
                udtHeader5595 result = new udtHeader5595();
                result.strHDCveTran = new FixedLengthString(4);
                result.strHDFiller01 = new FixedLengthString(1);
                result.strHDSubTran = new FixedLengthString(2);
                result.strHDFolPreimpreso = new FixedLengthString(16);
                result.strHDFolInterno = new FixedLengthString(8);
                result.strHDSistOrigen = new FixedLengthString(4);
                result.strHDTramite = new FixedLengthString(2);
                result.strHDEntOrig = new FixedLengthString(2);
                result.strHDGpoEntOrig = new FixedLengthString(4); //PRAXIS/ASG 20040204 Se aumenta la longitud de 2 a 4 y se elimina el campo numgpo
                result.strHDCveEntOrig = new FixedLengthString(4);
                result.strHDEstatus = new FixedLengthString(3); //MMS 11/05 Incremento en la longitud del campo (2 a 3)
                result.strHDCveResp = new FixedLengthString(2);
                result.strHDDescResp = new FixedLengthString(50);
                result.strHDNominaOper = new FixedLengthString(10);
                result.strHDCvePaqEval = new FixedLengthString(4);
                result.strHDCveProceso = new FixedLengthString(3); //MMS 11/05 Incremento en la longitud del campo (2 a 3)
                result.strHDFlagInfo = new FixedLengthString(1);
                result.strHDCveRechazo = new FixedLengthString(4);
                result.strHDPantalla = new FixedLengthString(8);
                result.strHDNumMapa = new FixedLengthString(3); //MMS 11/05 Incremento en la longitud del campo (2 a 3)
                result.strHDProcIni = new FixedLengthString(3); //MMS 11/05 Incremento en la longitud del campo (2 a 3)
                result.strHDFiller02 = new FixedLengthString(38); //MMS 11/05 Reducción del filler
                return result;
            }
        }
        static public udtHeader5595 estHeader5595 = udtHeader5595.CreateInstance();

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct udfDatSolicitud
        { //BLOQUE 0: DATOS DE LA SOLICITUD
            public FixedLengthString strDSEtiqueta;
            public FixedLengthString strDSFecSol;
            public FixedLengthString strDSEdoPromot;
            public FixedLengthString strDSSucPromot;
            public FixedLengthString strDSCanalPromot;
            public FixedLengthString strDSEmpPromot;
            public FixedLengthString strDSProdPromot;
            public FixedLengthString strDSRFCAgente;
            public FixedLengthString strDSNominaAgente;
            public FixedLengthString strDSSucursalSolic;

            public static udfDatSolicitud CreateInstance()
            {
                udfDatSolicitud result = new udfDatSolicitud();
                result.strDSEtiqueta = new FixedLengthString(4);
                result.strDSFecSol = new FixedLengthString(8);
                result.strDSEdoPromot = new FixedLengthString(2);
                result.strDSSucPromot = new FixedLengthString(4);
                result.strDSCanalPromot = new FixedLengthString(4);
                result.strDSEmpPromot = new FixedLengthString(4);
                result.strDSProdPromot = new FixedLengthString(4);
                result.strDSRFCAgente = new FixedLengthString(10);
                result.strDSNominaAgente = new FixedLengthString(10);
                result.strDSSucursalSolic = new FixedLengthString(4);
                return result;
            }
        }
        static public udfDatSolicitud strDatSolicitud = udfDatSolicitud.CreateInstance();

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct udfDatPersonales
        { //BLOQUE 1: DATOS PERSONALES
            public FixedLengthString strDPEtiqueta;
            public FixedLengthString strDPClaveParticip;
            public FixedLengthString strDPTipoRelacion;
            public FixedLengthString strDPIndicParticip;
            public FixedLengthString strDPApPaterno;
            public FixedLengthString strDPApMaterno;
            public FixedLengthString strDPNombres;
            public FixedLengthString strDPCalleNum;
            public FixedLengthString strDPColPob;
            public FixedLengthString strDPCodPos;
            public FixedLengthString strDPDelMuni;
            public FixedLengthString strDPCveEstado;
            public FixedLengthString strDPLADA;
            public FixedLengthString strDPTelef;
            public FixedLengthString strDPExtension;
            public FixedLengthString strDPCodAreaCEL;
            public FixedLengthString strDPTelefonoCEL;
            public FixedLengthString strDPCodAreaFAX;
            public FixedLengthString strDPTelefonoFAX;
            public FixedLengthString strDPCodAreaOtro; //PRAXIS/ASG 20040204 Se agrega el código de área de otro teléfono
            public FixedLengthString strDPOtroTelef; //PRAXIS/ASG 20040204 Se agrega el campo "Otro Teléfono"
            public FixedLengthString strDPCveNac;
            public FixedLengthString strDPPais; //País de Origen  MMS 11/05 Incremento en la longitud del campo (2 a 3)
            public FixedLengthString strDPEstCiv;
            public FixedLengthString strDPFecNac;
            public FixedLengthString strDPRFC;
            public FixedLengthString strDPCURP;
            public FixedLengthString strDPSeguroSocial; //PRAXIS/ASG 20040204 Se agrega el no. de seguro social
            public FixedLengthString strDPTmpRes;
            public FixedLengthString strDPCveSexo;
            public FixedLengthString strDPCveEsc;
            public FixedLengthString strDPTpoViv;
            public FixedLengthString strDPNumDep;
            public FixedLengthString strDPTpoPers;
            public FixedLengthString strDPFirma;
            public FixedLengthString strDPFirmaBuro;
            public FixedLengthString strDPEmail;
            public FixedLengthString strDPFecAltaCte;
            public FixedLengthString strDPParentesco;
            public FixedLengthString strDPEsCliente;
            public FixedLengthString strDPLugar;
            public FixedLengthString strDPIndPartIng;
            public FixedLengthString strDPPorcAcciones;
            public FixedLengthString strDPNumClieBnx;
            public FixedLengthString strDPIngFijosMens;
            public FixedLengthString strDPOtrosIng;
            public FixedLengthString strDPEgresos;
            public FixedLengthString strDPTipoSegSoc; //Tipo Seguridad Social'  MMS 11/05 Incremento en la longitud del campo (2 a 3)
            public FixedLengthString strDPHijosMenores; //Número de Hijos menores de 18 años'  MMS 11/05  Incremento en la longitud del campo (2 a 3)
            public FixedLengthString strDPPaisNacimiento; //AEFS Dato Nuevo
            public FixedLengthString strDPFIEL; //AEFS Dato Nuevo
            //*** INI - IRP – Proy. 66008-06
            public FixedLengthString strDPEntFedNac;
            //*** FIN - IRP – Proy. 66008-06
            //MODIF MAP ART.115 2016
            public FixedLengthString strIdentFis1;
            public FixedLengthString strPaisAsig1;
            public FixedLengthString strIdentFis2;
            public FixedLengthString strPaisAsig2;
            public FixedLengthString strFechaCons;
            public FixedLengthString strGiroEmpre;
            public FixedLengthString strTrabExtra;


            public static udfDatPersonales CreateInstance()
            {
                udfDatPersonales result = new udfDatPersonales();
                result.strDPEtiqueta = new FixedLengthString(4);
                result.strDPClaveParticip = new FixedLengthString(2);
                result.strDPTipoRelacion = new FixedLengthString(2);
                result.strDPIndicParticip = new FixedLengthString(2);
                result.strDPApPaterno = new FixedLengthString(60); //AEFS Cambia de 30 a 60
                result.strDPApMaterno = new FixedLengthString(60); //AEFS Cambia de 30 a 60
                result.strDPNombres = new FixedLengthString(50); //AEFS Cambia de 30 a 50
                result.strDPCalleNum = new FixedLengthString(35);
                result.strDPColPob = new FixedLengthString(35);
                result.strDPCodPos = new FixedLengthString(5);
                result.strDPDelMuni = new FixedLengthString(26);
                result.strDPCveEstado = new FixedLengthString(2);
                result.strDPLADA = new FixedLengthString(3);
                result.strDPTelef = new FixedLengthString(7);
                result.strDPExtension = new FixedLengthString(5);
                result.strDPCodAreaCEL = new FixedLengthString(3);
                result.strDPTelefonoCEL = new FixedLengthString(7);
                result.strDPCodAreaFAX = new FixedLengthString(3);
                result.strDPTelefonoFAX = new FixedLengthString(7);
                result.strDPCodAreaOtro = new FixedLengthString(3); //PRAXIS/ASG 20040204 Se agrega el código de área de otro teléfono
                result.strDPOtroTelef = new FixedLengthString(7); //PRAXIS/ASG 20040204 Se agrega el campo "Otro Teléfono"
                result.strDPCveNac = new FixedLengthString(1);
                result.strDPPais = new FixedLengthString(4); //País de Origen  MMS 11/05 Incremento en la longitud del campo (2 a 3)
                result.strDPEstCiv = new FixedLengthString(2);
                result.strDPFecNac = new FixedLengthString(8);
                result.strDPRFC = new FixedLengthString(13);
                result.strDPCURP = new FixedLengthString(18);
                result.strDPSeguroSocial = new FixedLengthString(11); //PRAXIS/ASG 20040204 Se agrega el no. de seguro social
                result.strDPTmpRes = new FixedLengthString(2);
                result.strDPCveSexo = new FixedLengthString(1);
                result.strDPCveEsc = new FixedLengthString(2);
                result.strDPTpoViv = new FixedLengthString(2);
                result.strDPNumDep = new FixedLengthString(2);
                result.strDPTpoPers = new FixedLengthString(2);
                result.strDPFirma = new FixedLengthString(1);
                result.strDPFirmaBuro = new FixedLengthString(1);
                result.strDPEmail = new FixedLengthString(78); //AEFS Cambio de long de 40 a 78
                result.strDPFecAltaCte = new FixedLengthString(8);
                result.strDPParentesco = new FixedLengthString(2);
                result.strDPEsCliente = new FixedLengthString(2);
                result.strDPLugar = new FixedLengthString(20);
                result.strDPIndPartIng = new FixedLengthString(1);
                result.strDPPorcAcciones = new FixedLengthString(3);
                result.strDPNumClieBnx = new FixedLengthString(12);
                result.strDPIngFijosMens = new FixedLengthString(8);
                result.strDPOtrosIng = new FixedLengthString(8);
                result.strDPEgresos = new FixedLengthString(8);
                result.strDPTipoSegSoc = new FixedLengthString(1); //Tipo Seguridad Social'  MMS 11/05 Incremento en la longitud del campo (2 a 3)
                result.strDPHijosMenores = new FixedLengthString(2); //Número de Hijos menores de 18 años'  MMS 11/05  Incremento en la longitud del campo (2 a 3)
                result.strDPPaisNacimiento = new FixedLengthString(4); //AEFS Campo Nuevo
                result.strDPFIEL = new FixedLengthString(20); //AEFS Campo Nuevo
                //*** INI - IRP – Proy. 66008-06
                result.strDPEntFedNac  = new FixedLengthString(2);
                //*** FIN - IRP – Proy. 66008-06
                //MODIF MAP ART.115 2016               
                result.strIdentFis1 = new FixedLengthString(20);
                result.strPaisAsig1 = new FixedLengthString(4);
                result.strIdentFis2 = new FixedLengthString(20);
                result.strPaisAsig2 = new FixedLengthString(4);
                result.strFechaCons = new FixedLengthString(8);
                result.strGiroEmpre = new FixedLengthString(6);
                result.strTrabExtra = new FixedLengthString(2);
                return result;
            }
        }
        static public udfDatPersonales[] strDatPersonales = ArraysHelper.InitializeArray<udfDatPersonales[]>(new int[] { 3 });

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct udfDatEmpleo
        { //BLOQUE 2: DATOS DEL EMPLEO
            public FixedLengthString strDEEtiqueta;
            public FixedLengthString strDEClaveParticip;
            public FixedLengthString strDETipoRelacion;
            public FixedLengthString strDEIndicParticip;
            public FixedLengthString strDECveOcuProf;
            public FixedLengthString strDENomEmp;
            public FixedLengthString strDECalleNum;
            public FixedLengthString strDEColPob;
            public FixedLengthString strDECodPos;
            public FixedLengthString strDEDelMuni;
            public FixedLengthString strDECveEstado;
            public FixedLengthString strDELADA;
            public FixedLengthString strDETelef;
            public FixedLengthString strDEExten;
            public FixedLengthString strDECodAreaFAX;
            public FixedLengthString strDETelefonoFAX;
            public FixedLengthString strDEDepto;
            public FixedLengthString strDEGiroEmp; //PRAXIS/ASG 20040204 El campo aumenta de 2 a 6 posiciones
            public FixedLengthString strDECveSec;
            public FixedLengthString strDECvePuesto;
            public FixedLengthString strDEProfOficio;
            public FixedLengthString strDEAMAntig; //PRAXIS/ASG 20040206 El campo antigüedad aumenta en 2 posiciones para quedar en formato AAMM
            public FixedLengthString strDEFecIngreso;

            public static udfDatEmpleo CreateInstance()
            {
                udfDatEmpleo result = new udfDatEmpleo();
                result.strDEEtiqueta = new FixedLengthString(4);
                result.strDEClaveParticip = new FixedLengthString(2);
                result.strDETipoRelacion = new FixedLengthString(2);
                result.strDEIndicParticip = new FixedLengthString(2);
                result.strDECveOcuProf = new FixedLengthString(4);
                result.strDENomEmp = new FixedLengthString(40);
                result.strDECalleNum = new FixedLengthString(35);
                result.strDEColPob = new FixedLengthString(35);
                result.strDECodPos = new FixedLengthString(5);
                result.strDEDelMuni = new FixedLengthString(26);
                result.strDECveEstado = new FixedLengthString(2);
                result.strDELADA = new FixedLengthString(3);
                result.strDETelef = new FixedLengthString(7);
                result.strDEExten = new FixedLengthString(5);
                result.strDECodAreaFAX = new FixedLengthString(3);
                result.strDETelefonoFAX = new FixedLengthString(7);
                result.strDEDepto = new FixedLengthString(30);
                result.strDEGiroEmp = new FixedLengthString(6); //PRAXIS/ASG 20040204 El campo aumenta de 2 a 6 posiciones
                result.strDECveSec = new FixedLengthString(2);
                result.strDECvePuesto = new FixedLengthString(2);
                result.strDEProfOficio = new FixedLengthString(30);
                result.strDEAMAntig = new FixedLengthString(4); //PRAXIS/ASG 20040206 El campo antigüedad aumenta en 2 posiciones para quedar en formato AAMM
                result.strDEFecIngreso = new FixedLengthString(8);
                return result;
            }
        }
        static public udfDatEmpleo[] strDatEmpleo = ArraysHelper.InitializeArray<udfDatEmpleo[]>(new int[] { 3 });

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct udfHdrRefCredBancOtros
        { //BLOQUE 3: DATOS DE REFERENCIAS CREDITICIAS
            //          BANCARIAS Y OTROS CREDITOS, solo se incluye el Header
            public FixedLengthString strRCEtiqueta;
            public FixedLengthString strRCClaveParticip;
            public FixedLengthString strRCTipoRelacion;
            public FixedLengthString strRCIndicParticip;
            public FixedLengthString strRCCantRef;

            public static udfHdrRefCredBancOtros CreateInstance()
            {
                udfHdrRefCredBancOtros result = new udfHdrRefCredBancOtros();
                result.strRCEtiqueta = new FixedLengthString(4);
                result.strRCClaveParticip = new FixedLengthString(2);
                result.strRCTipoRelacion = new FixedLengthString(2);
                result.strRCIndicParticip = new FixedLengthString(2);
                result.strRCCantRef = new FixedLengthString(2);
                return result;
            }
        }
        static public udfHdrRefCredBancOtros[] strHdrRCredBancOtros = ArraysHelper.InitializeArray<udfHdrRefCredBancOtros[]>(new int[] { 2 });

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct udfRefCredBancOtros
        { //Bloque 3.0, estructura para las ocurrencias
            public FixedLengthString strRCCveProceso; //MMS 11/05 Incremento en la longitud del campo (2 a 3)
            public FixedLengthString strRCInstitOtorga;
            public FixedLengthString strRCTipoCuenta;
            public FixedLengthString strRCNumCuenta;
            public FixedLengthString strRCSaldo; //PRAXIS/ASG 20040204 Se agrega el saldo
            public FixedLengthString strRCPagoMensual; //PRAXIS/ASG 20040204 Se agrega el pago mensual
            public FixedLengthString strRCFechaApertura; //PRAXIS/ASG 20040204 Se agrega la fecha de apertura

            public static udfRefCredBancOtros CreateInstance()
            {
                udfRefCredBancOtros result = new udfRefCredBancOtros();
                result.strRCCveProceso = new FixedLengthString(3); //MMS 11/05 Incremento en la longitud del campo (2 a 3)
                result.strRCInstitOtorga = new FixedLengthString(2);
                result.strRCTipoCuenta = new FixedLengthString(2);
                result.strRCNumCuenta = new FixedLengthString(16);
                result.strRCSaldo = new FixedLengthString(9); //PRAXIS/ASG 20040204 Se agrega el saldo
                result.strRCPagoMensual = new FixedLengthString(9); //PRAXIS/ASG 20040204 Se agrega el pago mensual
                result.strRCFechaApertura = new FixedLengthString(8); //PRAXIS/ASG 20040204 Se agrega la fecha de apertura
                return result;
            }
        }
        static public udfRefCredBancOtros[,] strRCredBancOtros = ArraysHelper.InitializeArray<udfRefCredBancOtros[,]>(new int[] { +1, +1 });

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct udfRefPersonales
        { //BLOQUE 4: REFERENCIAS PERSONALES
            public FixedLengthString strRPEtiqueta;
            public FixedLengthString strRPClaveParticip;
            public FixedLengthString strRPTipoRelacion;
            public FixedLengthString strRPIndicParticip;
            public FixedLengthString strRPPaterno;
            public FixedLengthString strRPMaterno;
            public FixedLengthString strRPNombres;
            public FixedLengthString strRPCveParent;
            public FixedLengthString strRPCalleNum;
            public FixedLengthString strRPColPob;
            public FixedLengthString strRPCodPos;
            public FixedLengthString strRPDelMuni;
            public FixedLengthString strRPCveEstado;
            public FixedLengthString strRPLADA;
            public FixedLengthString strRPTelef;
            public FixedLengthString strRPFecNac;
            public FixedLengthString strRPCveSexo;
            public FixedLengthString strRPFirma;

            public static udfRefPersonales CreateInstance()
            {
                udfRefPersonales result = new udfRefPersonales();
                result.strRPEtiqueta = new FixedLengthString(4);
                result.strRPClaveParticip = new FixedLengthString(2);
                result.strRPTipoRelacion = new FixedLengthString(2);
                result.strRPIndicParticip = new FixedLengthString(2);
                result.strRPPaterno = new FixedLengthString(60); //AEFS Cambia de 30 a 60
                result.strRPMaterno = new FixedLengthString(60); //AEFS Cambia de 30 a 60
                result.strRPNombres = new FixedLengthString(50); //AEFS Cambia de 30 a 50
                result.strRPCveParent = new FixedLengthString(2);
                result.strRPCalleNum = new FixedLengthString(35);
                result.strRPColPob = new FixedLengthString(35);
                result.strRPCodPos = new FixedLengthString(5);
                result.strRPDelMuni = new FixedLengthString(26);
                result.strRPCveEstado = new FixedLengthString(2);
                result.strRPLADA = new FixedLengthString(3);
                result.strRPTelef = new FixedLengthString(7);
                result.strRPFecNac = new FixedLengthString(8);
                result.strRPCveSexo = new FixedLengthString(1);
                result.strRPFirma = new FixedLengthString(1);
                return result;
            }
        }
        static public udfRefPersonales[] strRefPersonales = ArraysHelper.InitializeArray<udfRefPersonales[]>(new int[] { 3 });

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct udfHdrComprobantes
        { //BLOQUE 5: COMPROBANTES
            public FixedLengthString strCOEtiqueta;
            public FixedLengthString strCOClaveParticip;
            public FixedLengthString strCOTipoRelacion;
            public FixedLengthString strCOIndicParticip;
            public FixedLengthString strCONumComprob;

            public static udfHdrComprobantes CreateInstance()
            {
                udfHdrComprobantes result = new udfHdrComprobantes();
                result.strCOEtiqueta = new FixedLengthString(4);
                result.strCOClaveParticip = new FixedLengthString(2);
                result.strCOTipoRelacion = new FixedLengthString(2);
                result.strCOIndicParticip = new FixedLengthString(2);
                result.strCONumComprob = new FixedLengthString(2);
                return result;
            }
        }
        static public udfHdrComprobantes strHdrComprobantes = udfHdrComprobantes.CreateInstance();

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct udfComprobantes
        {
            public FixedLengthString strCOTipoComprob;
            public FixedLengthString strCOTipoDocto;
            public FixedLengthString strCONumDocto;

            public static udfComprobantes CreateInstance()
            {
                udfComprobantes result = new udfComprobantes();
                result.strCOTipoComprob = new FixedLengthString(2);
                result.strCOTipoDocto = new FixedLengthString(2);
                result.strCONumDocto = new FixedLengthString(20);
                return result;
            }
        }
        static public udfComprobantes[] strComprobantes;// = ArraysHelper.InitializeArray<udfComprobantes[]>(new int[]{});

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct udfHdrPropiedades
        { //BLOQUE 7: DATOS DE PROPIEDADES
            public FixedLengthString strPREtiqueta;
            public FixedLengthString strPRClaveParticip;
            public FixedLengthString strPRTipoRelacion;
            public FixedLengthString strPRIndicParticip;
            public FixedLengthString strPRNumPropied;

            public static udfHdrPropiedades CreateInstance()
            {
                udfHdrPropiedades result = new udfHdrPropiedades();
                result.strPREtiqueta = new FixedLengthString(4);
                result.strPRClaveParticip = new FixedLengthString(2);
                result.strPRTipoRelacion = new FixedLengthString(2);
                result.strPRIndicParticip = new FixedLengthString(2);
                result.strPRNumPropied = new FixedLengthString(2);
                return result;
            }
        }
        static public udfHdrPropiedades strHdrPropiedades = udfHdrPropiedades.CreateInstance();

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct udfPropiedades
        {
            public FixedLengthString strPRTipoBien;
            public FixedLengthString strPRSitPagoBien;
            public FixedLengthString strPRFechaCompra;
            public FixedLengthString strPRAntiguedad;
            public FixedLengthString strPRCveMarca;
            public FixedLengthString strPRDescripcion;

            public static udfPropiedades CreateInstance()
            {
                udfPropiedades result = new udfPropiedades();
                result.strPRTipoBien = new FixedLengthString(2);
                result.strPRSitPagoBien = new FixedLengthString(2);
                result.strPRFechaCompra = new FixedLengthString(8);
                result.strPRAntiguedad = new FixedLengthString(3);
                result.strPRCveMarca = new FixedLengthString(4);
                result.strPRDescripcion = new FixedLengthString(50);
                return result;
            }
        }
        static public udfPropiedades[] strPropiedades;// = ArraysHelper.InitializeArray<udfPropiedades[]>(new int[]{1});

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct udfCSolicitado
        { //BLOQUE 8: DATOS DEL CREDITO SOLICITADO
            public FixedLengthString strCSEtiqueta;
            public FixedLengthString strCSFamiliaProd;
            public FixedLengthString strCSTipoSolicitud;
            public FixedLengthString strCSImportancia;
            public FixedLengthString strCSDestino;
            public FixedLengthString strCSMontoSolic;
            public FixedLengthString strCSLineaCredito;
            public FixedLengthString strCSTipoBien;
            public FixedLengthString strCSPlazo;
            public FixedLengthString strCStTasa;
            public FixedLengthString strCSOrigenGtia; //MMS 04/06 Se agrega el campo Origen de la garantia

            public static udfCSolicitado CreateInstance()
            {
                udfCSolicitado result = new udfCSolicitado();
                result.strCSEtiqueta = new FixedLengthString(4);
                result.strCSFamiliaProd = new FixedLengthString(2);
                result.strCSTipoSolicitud = new FixedLengthString(2);
                result.strCSImportancia = new FixedLengthString(2);
                result.strCSDestino = new FixedLengthString(4);
                result.strCSMontoSolic = new FixedLengthString(10);
                result.strCSLineaCredito = new FixedLengthString(10);
                result.strCSTipoBien = new FixedLengthString(2);
                result.strCSPlazo = new FixedLengthString(3);
                result.strCStTasa = new FixedLengthString(4);
                result.strCSOrigenGtia = new FixedLengthString(2); //MMS 04/06 Se agrega el campo Origen de la garantia
                return result;
            }
        }
        static public udfCSolicitado strCSolicitado = udfCSolicitado.CreateInstance();
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct udfArregloIndicadores
        {
            public FixedLengthString strCveIndicador;
            public FixedLengthString strValorIndicador;

            public static udfArregloIndicadores CreateInstance()
            {
                udfArregloIndicadores result = new udfArregloIndicadores();
                result.strCveIndicador = new FixedLengthString(3);
                result.strValorIndicador = new FixedLengthString(10);
                return result;
            }
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct udfIndAdicionales
        { //Bloque 12: Indicadores Adicionales del Crédito
            public FixedLengthString strIAEtiqueta;
            public FixedLengthString strIAFamiliaProd;
            public FixedLengthString strIATipoSolicitud;
            public FixedLengthString strIAOcurrencias;
            public udfArregloIndicadores[] strIAIndicador;

            public static udfIndAdicionales CreateInstance()
            {
                udfIndAdicionales result = new udfIndAdicionales();
                result.strIAEtiqueta = new FixedLengthString(4);
                result.strIAFamiliaProd = new FixedLengthString(2);
                result.strIATipoSolicitud = new FixedLengthString(2);
                result.strIAOcurrencias = new FixedLengthString(3);
                return result;
            }
        }
        static public udfIndAdicionales strIndAdicionales = udfIndAdicionales.CreateInstance();
        static public string[] gblaBloque = null;
        static private Collection _ClAceptadas = null;
        static public Collection ClAceptadas
        {
            get
            {
                if (_ClAceptadas == null)
                {
                    _ClAceptadas = new Collection();
                }
                return _ClAceptadas;
            }
            set
            {
                _ClAceptadas = value;
            }
        }

        //DEFINICIONES PARA EL LAYOUT ÚNICO DE CAPTURA/REGISTRO INFO. ADICIONAL
        //CONSTANTES GLOBALES
        public const int gcIntPorEnviar = 0;
        public const int gcIntEnviado = 1;
        public const int gcIntEnviandose = 2;
        static public FixedLengthString gcStrSistOrigen = new FixedLengthString(4, "S753");
        public const int gcIntLongitudDatos = 1746;
        //ETIQUETAS
        public const string gcstrDPSolicitante = "&BDP"; //Datos personales
        public const string gcstrDSSolicitante = "&BDS"; //Datos de la Solicitud
        public const string gcstrDESolicitante = "&BDE"; //Datos del empleo
        public const string gcstrRCSolicitante = "&BRC"; //Referencias crediticias
        public const string gcstrRPSolicitante = "&BRP"; //Referencias personales
        public const string gcstrCOSolicitante = "&BCO"; //Comprobantes
        public const string gcstrPRSolicitante = "&BPR"; //Propiedades
        public const string gcstrCSSolicitante = "&BCS"; //Crédito Solicitado
        public const string gcstrIASolicitante = "&BIA"; //Indicadores Adicionales del Crédito
        public const string gcstrFinDialogo = "&&&&"; //Fin de diálogo
        //Declaración de la longitud máxima de bloques enviados por FTP
        public const int gcIntLongitudMaxima = 3960;
        public const int gcIntLongitudConsecutivo = 5; //La longitud máxima es de 3966, se suman  gcIntLongitudMaxima(3960) + gcIntLongitudConsecutivo(5) + CR(1) = 3966
        //Declaración de los arreglos de segmento por solicitud
        static public string[] gvstrFoliosSolicitudes = null; //Registro de los folios de cada solicitud
        static public string[] gvstrDatosPersonales = null; //Bloque para los datos personales del solicitante
        static public string[] gvstrDatosSolicitud = null; //Bloque de solicitud
        static public string[] gvstrDatosPersonalesC = null; //Bloque para los datos personales del conyuge
        static public string[] gvstrDatosPersonalesO = null; //Bloque para los datos personales del obligado solidario
        static public string[] gvstrDatosDeEmpleo = null; //Bloque para los datos del empleo del solicitante
        static public string[] gvstrDatosDeEmpleoC = null; //Bloque para los datos del empleo del conyuge
        static public string[] gvstrDatosDeEmpleoO = null; //Bloque para los datos del empleo del obligado solidario
        static public string[] gvstrRefCrediticias = null; //Bloque para las referencias crediticias del solicitante
        static public string[] gvstrRefPersonales = null; //Bloque para las referencias personales del solicitante
        static public string[] gvstrComprobantes = null; //Bloque para los comprobantes
        static public string[] gvstrPropiedades = null; //Bloque para las propiedades
        static public string[] gvstrCredSolicitado = null; //Bloque del crédito solicitado
        static public string[] gvstrIndAdicionales = null; //Bloque de Indicadores Adicionales
        static public int gblnContinua = 0;

        //Armar el Header de la transacción 5595 09
        static public string funArmaHeader5595(string strEstHead, string strCveProc, string strCausaDec, string strFolioPreimp, string strDatosRemesa, string strFolInterno)
        {
            estHeader5595.strHDCveTran.Value = "5595";
            estHeader5595.strHDFiller01.Value = new String(' ', 1);
            estHeader5595.strHDSubTran.Value = "09";
            estHeader5595.strHDFolPreimpreso.Value = mdlGlobales.funZeroes(8) + strFolioPreimp;
            if (strFolInterno.Trim() != "")
            {
                estHeader5595.strHDFolInterno.Value = strFolInterno;
            }
            else
            {
                estHeader5595.strHDFolInterno.Value = mdlGlobales.funZeroes(8);
            }
            estHeader5595.strHDSistOrigen.Value = gcStrSistOrigen.Value; //ASG Se cambia el valor directo por la variable que lo almacena
            estHeader5595.strHDTramite.Value = mdlGlobales.gstrTramite.Value;
            estHeader5595.strHDEntOrig.Value = mdlGlobales.gstrEntOrig.Value.Substring(0, Math.Min(mdlGlobales.gstrEntOrig.Value.Length, 2));
            estHeader5595.strHDGpoEntOrig.Value = mdlGlobales.funPoneCeros(mdlGlobales.gstrGpoEntOrig.Value, 4);
            estHeader5595.strHDCveEntOrig.Value = mdlGlobales.funPoneCeros(mdlGlobales.gstrCveEntOrig.Value, 4);
            estHeader5595.strHDEstatus.Value = strEstHead;
            estHeader5595.strHDCveResp.Value = mdlGlobales.funZeroes(2);
            estHeader5595.strHDDescResp.Value = new String(' ', 50);
            estHeader5595.strHDNominaOper.Value = mdlGlobales.funPoneCeros(mdlGlobales.gstrNomina.Value, 10);
            estHeader5595.strHDCvePaqEval.Value = mdlGlobales.gstrCvePaquete.Value;
            estHeader5595.strHDCveProceso.Value = strCveProc;
            estHeader5595.strHDFlagInfo.Value = mdlGlobales.funZeroes(1);
            estHeader5595.strHDCveRechazo.Value = mdlGlobales.funPoneCeros(Conversion.Val(strCausaDec).ToString(), 4);
            estHeader5595.strHDPantalla.Value = mdlGlobales.funZeroes(8);
            estHeader5595.strHDNumMapa.Value = mdlGlobales.funZeroes(3); //MMS 11/05 Incremento en la longitud del campo (2 a 3)
            estHeader5595.strHDProcIni.Value = mdlGlobales.funZeroes(3); //MMS 11/05 Incremento en la longitud del campo (2 a 3)
            //MODIF MAP 2014/03/18 NO TENGO IDEA PORQUE HICIERON ESTA VALIDACION
            //if (!false)
            //{
            //    estHeader5595.strHDFiller02.Value = strDatosRemesa + new String(' ', 15); //MMS 11/05 Reducción del filler
            //}
            //else
            //{
            //    estHeader5595.strHDFiller02.Value = new String(' ', 38); //MMS 11/05 Reducción del filler
            //}
            estHeader5595.strHDFiller02.Value = strDatosRemesa + new String(' ', 15); //MMS 11/05 Reducción del filler

            return estHeader5595.strHDCveTran.Value + estHeader5595.strHDFiller01.Value + estHeader5595.strHDSubTran.Value +
            estHeader5595.strHDFolPreimpreso.Value + estHeader5595.strHDFolInterno.Value + estHeader5595.strHDSistOrigen.Value +
            estHeader5595.strHDTramite.Value + estHeader5595.strHDEntOrig.Value + estHeader5595.strHDGpoEntOrig.Value +
            estHeader5595.strHDCveEntOrig.Value + estHeader5595.strHDEstatus.Value + estHeader5595.strHDCveResp.Value +
            estHeader5595.strHDDescResp.Value + estHeader5595.strHDNominaOper.Value + estHeader5595.strHDCvePaqEval.Value +
            estHeader5595.strHDCveProceso.Value + estHeader5595.strHDFlagInfo.Value + estHeader5595.strHDCveRechazo.Value +
            estHeader5595.strHDPantalla.Value + estHeader5595.strHDNumMapa.Value + estHeader5595.strHDProcIni.Value +
            estHeader5595.strHDFiller02.Value;
        }

        static public string funArmaHeader5595(string strEstHead, string strCveProc, string strCausaDec, string strFolioPreimp, string strDatosRemesa)
        {
            return funArmaHeader5595(strEstHead, strCveProc, strCausaDec, strFolioPreimp, strDatosRemesa, "");
        }

        static public string funArmaHeader5595(string strEstHead, string strCveProc, string strCausaDec, string strFolioPreimp)
        {
            return funArmaHeader5595(strEstHead, strCveProc, strCausaDec, strFolioPreimp, "", "");
        }

        //Rutina para armar y enviar los bloques de los folios de la remesa
        static public void subEnviaBloques()
        {
            string strTipoReg = String.Empty;
            int intCont = 0;
            string strNumCuenta = String.Empty;
            string strCadenaDatos = String.Empty;
            string strAceptaPaso = String.Empty;
            int intNumReg = 0;
            string lsParaEnviar = String.Empty;
            string strCveDescCatalogo = String.Empty; //Variable para el control de claves numéricas o de descripciones
            //MODIF MAP 2014/03/18 
            //int lngFactor = 0;
            double lngVariable = 0;
            string strTipoTramite = String.Empty;
            string lsCadena = String.Empty;
            int LongitudCadena = 0;
            string lsEnviar = String.Empty;
            //MODIF MAP 2014/03/18 
            //int liChar = 0;
            int liArchivoSalida = 0;
            string strPlazo = String.Empty, strTasa = String.Empty;
            //MODIF MAP 2014/03/18 
            //int intContador = 0;
            string strArchivoEnviar = String.Empty;
            string strCadenaValida = String.Empty; //ASG Para validar los bloques de envío que salen sobrando
            FixedLengthString strCvePartReferencias = new FixedLengthString(2);
            int intContReg08 = 0;
            int intContReg09 = 0;
            int intOcurrenciasReg19 = 0;
            //* *** Inicio cambios efectuados por DTH para el proyecto 24677
            //* 2006-Mar-16, se agregan las siguiente variable para validar cuenta de cheques
            //* para ADELA PERFIL NOMINA
            string strSucCuentaCHQ = String.Empty; // Contiene la cunta de cheques de ADELA Perfil nomina en el siguiente Formato SUC(4)Cuenta(7)
            //* *** Fin de cambios efectuados por DTH para el proyecto 24677

            try
            {
                intContReg08 = 0;
                intContReg09 = 0;
                //MODIF MAP 2014/03/18 
                //liChar = 228;
                lsCadena = "";
                intNumReg = 0;
                mdlGlobales.subDespMensajes("GENERANDO BLOQUES DE ENVIO...");
                Cursor.Current = Cursors.WaitCursor;
                frmProcMasivo.DefInstance.prg_proceso.Value = 0;
                mdlTranMasivo.gvstrFechaProceso = frmProcMasivo.DefInstance.txtArchivo.Text;
                mdlGlobales.gstrRutaTemp = mdlMain.ApplicationPath;

                //MIG WXP INI JGC 20090825
                string strMASParam1 = mdlRegistry.RegistryMasivos("MASParam1");
                //strArchivoEnviar = mdlGlobales.gstrRutaTemp + "\\" + "75301e01.s753." + mdlTranMasivo.gvstrFechaProceso + ".txt";
                strArchivoEnviar = mdlGlobales.gstrRutaTemp + "\\" + "75301e01.s753." + mdlTranMasivo.gvstrFechaProceso + ".txt";

                //FileSystem.FileClose(liArchivoSalida);
                liArchivoSalida = FileSystem.FreeFile();
                FileSystem.FileOpen(liArchivoSalida, strArchivoEnviar, OpenMode.Output, OpenAccess.Default, OpenShare.Default, -1);
                //FileSystem.FileOpen(liArchivoSalida, strArchivoEnviar, OpenMode.Append, OpenAccess.Default, OpenShare.Default, -1);                

                int IntContReferencias = 0;
                int IntContComprobantes = 0;
                int IntContPropiedades = 0;

                foreach (string[] strBloque in ClAceptadas)
                {
                    mdlTranMasivo.subLimpiaVariablesDeBloque(); //Limpiar las variables de bloque del archivo
                    mdlPromotora.subLimpiaCadena(); //Limpiar la cadena de armado del archivo
                    for (intCont = 0; intCont <= strBloque.GetUpperBound(0); intCont++)
                    {
                        lsCadena = strBloque[intCont];
                        mdlTranMasivo.subArmaReg(lsCadena);
                    }
                    if ((estProm01.strFecSolicitud.Value.Trim().Length & ((Conversion.Val(estProm01.strFecSolicitud.Value) != 0) ? -1 : 0)) > 0 || estProm18.strProducto.Value.Trim().Length > 0 || estProm18.strRFCPromotor.Value.Trim().Length > 0)
                    { //Datos de la solicitud (Bloque 0)
                        strDatSolicitud.strDSEtiqueta.Value = gcstrDSSolicitante; //&BDS
                        strDatSolicitud.strDSFecSol.Value = mdlTranMasivo.funValidaFecha(estProm01.strFecSolicitud.Value);
                        strDatSolicitud.strDSSucursalSolic.Value = mdlGlobales.funZeroes(4);
                        strCadenaDatos = estProm18.strEstado.Value;
                        string tempRefParam = "19";
                        string tempRefParam2 = null;
                        string tempRefParam3 = null;
                        string tempRefParam4 = null;
                        Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam5 = Catalogos.clsCatalogos.enmCamposCatalogos.Default;
                        string tempRefParam6 = "E";
                        if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam, strCadenaDatos, tempRefParam2, tempRefParam3, ref tempRefParam4, ref tempRefParam5, ref tempRefParam6))
                        {
                            strDatSolicitud.strDSEdoPromot.Value = "99";
                        }
                        else
                        {
                            strDatSolicitud.strDSEdoPromot.Value = mdlComunica.OleCatalogos.getLlave1.Substring(mdlComunica.OleCatalogos.getLlave1.Length - Math.Min(mdlComunica.OleCatalogos.getLlave1.Length, 2));
                        }
                        if (estProm13.strSucursal.Value == "")
                        {
                            strDatSolicitud.strDSSucPromot.Value = estProm18.strSucursal.Value;
                            if (String.CompareOrdinal(strDatSolicitud.strDSSucPromot.Value, "8000") >= 0 && String.CompareOrdinal(strDatSolicitud.strDSSucPromot.Value, "8999") <= 0)
                            {
                                strDatSolicitud.strDSSucPromot.Value = "0000";
                            }
                        }
                        else
                        {
                            strDatSolicitud.strDSSucPromot.Value = estProm13.strSucursal.Value;
                        }
                        strCadenaDatos = estProm18.strCanalVta.Value;
                        string tempRefParam7 = "65";
                        string tempRefParam8 = null;
                        string tempRefParam9 = null;
                        string tempRefParam10 = null;
                        Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam11 = Catalogos.clsCatalogos.enmCamposCatalogos.Default;
                        string tempRefParam12 = "D";
                        if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam7, strCadenaDatos, tempRefParam8, tempRefParam9, ref tempRefParam10, ref tempRefParam11, ref tempRefParam12))
                        {
                            strDatSolicitud.strDSCanalPromot.Value = "0099";
                        }
                        else
                        {
                            strDatSolicitud.strDSCanalPromot.Value = mdlGlobales.funPoneCeros(estProm18.strCanalVta.Value, 4);
                        }
                        strCadenaDatos = estProm18.strMedio.Value;
                        string tempRefParam13 = "90";
                        string tempRefParam14 = null;
                        string tempRefParam15 = null;
                        string tempRefParam16 = null;
                        Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam17 = Catalogos.clsCatalogos.enmCamposCatalogos.Default;
                        string tempRefParam18 = "D";
                        if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam13, strCadenaDatos, tempRefParam14, tempRefParam15, ref tempRefParam16, ref tempRefParam17, ref tempRefParam18))
                        {
                            strDatSolicitud.strDSEmpPromot.Value = "0099";
                        }
                        else
                        {
                            strDatSolicitud.strDSEmpPromot.Value = mdlGlobales.funPoneCeros(estProm18.strMedio.Value, 4);
                        }
                        strDatSolicitud.strDSEmpPromot.Value = mdlGlobales.funPoneCeros(estProm18.strMedio.Value, 4);
                        strDatSolicitud.strDSProdPromot.Value = mdlGlobales.funPoneCeros(estProm18.strProducto.Value, 4);
                        strDatSolicitud.strDSRFCAgente.Value = estProm18.strRFCPromotor.Value;
                        strDatSolicitud.strDSNominaAgente.Value = mdlGlobales.funPoneCeros(estProm13.strNomEjecutivo.Value, 10);
                    }

                    strDatPersonales[0].strDPEtiqueta.Value = gcstrDPSolicitante; //Etiqueta &BDP
                    strDatPersonales[0].strDPClaveParticip.Value = "01";
                    strDatPersonales[0].strDPTipoRelacion.Value = "01";
                    strDatPersonales[0].strDPIndicParticip.Value = "01";
                    strDatPersonales[0].strDPApPaterno.Value = estProm01.strPaterno.Value.Trim();
                    strDatPersonales[0].strDPApMaterno.Value = estProm01.strMaterno.Value.Trim();
                    strDatPersonales[0].strDPNombres.Value = estProm01.strNombre.Value.Trim();
                    strDatPersonales[0].strDPCalleNum.Value = estProm01.strDomicilio.Value;
                    strDatPersonales[0].strDPColPob.Value = estProm01.strColonia.Value;
                    strDatPersonales[0].strDPCodPos.Value = mdlGlobales.funPoneCeros(estProm01.strCodigoPostal.Value, 5);
                    strDatPersonales[0].strDPDelMuni.Value = estProm01.strMunicipio.Value;
                    string tempRefParam19 = "19";
                    string tempRefParam20 = null;
                    string tempRefParam21 = null;
                    string tempRefParam22 = null;
                    Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam23 = Catalogos.clsCatalogos.enmCamposCatalogos.Atributos;
                    string tempRefParam24 = "E";
                    if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam19, estProm01.strEstado.Value, tempRefParam20, tempRefParam21, ref tempRefParam22, ref tempRefParam23, ref tempRefParam24))
                    {
                        strDatPersonales[0].strDPCveEstado.Value = "00";
                    }
                    else
                    {
                        strDatPersonales[0].strDPCveEstado.Value = mdlComunica.OleCatalogos.getLlave1.Substring(mdlComunica.OleCatalogos.getLlave1.Length - Math.Min(mdlComunica.OleCatalogos.getLlave1.Length, 2));
                    }
                    if (Conversion.Val(estProm01.strLada.Value) > 0 && Conversion.Val(estProm01.strTelefono.Value) == 0)
                    {
                        strDatPersonales[0].strDPLADA.Value = mdlGlobales.funZeroes(3);
                        strDatPersonales[0].strDPTelef.Value = mdlGlobales.funZeroes(7);
                    }
                    else
                    {
                        strDatPersonales[0].strDPLADA.Value = mdlGlobales.funPoneCeros(Conversion.Val(estProm01.strLada.Value).ToString(), 3);
                        strDatPersonales[0].strDPTelef.Value = mdlGlobales.funPoneCeros(Conversion.Val(estProm01.strTelefono.Value).ToString(), 7);
                    }
                    strDatPersonales[0].strDPExtension.Value = mdlGlobales.funZeroes(5);
                    strDatPersonales[0].strDPCodAreaCEL.Value = mdlGlobales.funZeroes(3);
                    strDatPersonales[0].strDPTelefonoCEL.Value = mdlGlobales.funZeroes(7);
                    strDatPersonales[0].strDPCodAreaFAX.Value = mdlGlobales.funZeroes(3);
                    strDatPersonales[0].strDPTelefonoFAX.Value = mdlGlobales.funZeroes(7);
                    strDatPersonales[0].strDPCodAreaOtro.Value = mdlGlobales.funZeroes(3);
                    strDatPersonales[0].strDPOtroTelef.Value = mdlGlobales.funZeroes(7);
                    if (Conversion.Val(estProm16.strNacionalidad.Value.Substring(estProm16.strNacionalidad.Value.Length - Math.Min(estProm16.strNacionalidad.Value.Length, 1))) != 0)
                    {
                        strDatPersonales[0].strDPCveNac.Value = estProm16.strNacionalidad.Value.Substring(estProm16.strNacionalidad.Value.Length - Math.Min(estProm16.strNacionalidad.Value.Length, 1));
                    }
                    else
                    {
                        strDatPersonales[0].strDPCveNac.Value = "0";
                    }
                    //AEFS Se asigna valor de Pais de Nacionalidad
                    //strDatPersonales[0].strDPPais.Value = mdlGlobales.funZeroes(4); //MMS 11/05 Se agrega el campo País de Origen
                    strDatPersonales[0].strDPPais.Value = estProm01.strPNacionalidad.Value;
                    strCadenaDatos = estProm01.strEstadoCivil.Value;
                    string tempRefParam25 = "20";
                    string tempRefParam26 = null;
                    string tempRefParam27 = null;
                    string tempRefParam28 = null;
                    Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam29 = Catalogos.clsCatalogos.enmCamposCatalogos.Atributos;
                    string tempRefParam30 = "D";
                    if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam25, estProm01.strEstadoCivil.Value, tempRefParam26, tempRefParam27, ref tempRefParam28, ref tempRefParam29, ref tempRefParam30))
                    {
                        strDatPersonales[0].strDPEstCiv.Value = "00";
                    }
                    else
                    {
                        strDatPersonales[0].strDPEstCiv.Value = mdlComunica.OleCatalogos.getLlave1.Substring(mdlComunica.OleCatalogos.getLlave1.Length - Math.Min(mdlComunica.OleCatalogos.getLlave1.Length, 2));
                    }
                    strDatPersonales[0].strDPFecNac.Value = mdlTranMasivo.funValidaFechaNacimiento(estProm01.strFecNacimiento.Value);
                    strDatPersonales[0].strDPRFC.Value = estProm01.strRFC.Value;
                    strDatPersonales[0].strDPCURP.Value = estProm19.strCURP.Value;
                    strDatPersonales[0].strDPSeguroSocial.Value = mdlGlobales.funZeroes(11);
                    strDatPersonales[0].strDPTmpRes.Value = mdlGlobales.funPoneCeros(estProm01.strAnosResidir.Value, 2);
                    strDatPersonales[0].strDPCveSexo.Value = estProm01.strSexo.Value;
                    string tempRefParam31 = "22";
                    string tempRefParam32 = null;
                    string tempRefParam33 = null;
                    string tempRefParam34 = null;
                    Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam35 = Catalogos.clsCatalogos.enmCamposCatalogos.Default;
                    string tempRefParam36 = "D";
                    if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam31, estProm01.strEscolaridad.Value, tempRefParam32, tempRefParam33, ref tempRefParam34, ref tempRefParam35, ref tempRefParam36))
                    {
                        strDatPersonales[0].strDPCveEsc.Value = "00";
                    }
                    else
                    {
                        strDatPersonales[0].strDPCveEsc.Value = mdlComunica.OleCatalogos.getLlave1.Substring(mdlComunica.OleCatalogos.getLlave1.Length - Math.Min(mdlComunica.OleCatalogos.getLlave1.Length, 2));
                    }
                    string tempRefParam37 = "28";
                    string tempRefParam38 = null;
                    string tempRefParam39 = null;
                    string tempRefParam40 = null;
                    Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam41 = Catalogos.clsCatalogos.enmCamposCatalogos.Atributos;
                    string tempRefParam42 = "D";
                    if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam37, estProm01.strTipoVivienda.Value, tempRefParam38, tempRefParam39, ref tempRefParam40, ref tempRefParam41, ref tempRefParam42))
                    {
                        strDatPersonales[0].strDPTpoViv.Value = "00";
                    }
                    else
                    {
                        strDatPersonales[0].strDPTpoViv.Value = mdlComunica.OleCatalogos.getLlave1.Substring(mdlComunica.OleCatalogos.getLlave1.Length - Math.Min(mdlComunica.OleCatalogos.getLlave1.Length, 2));
                    }
                    strDatPersonales[0].strDPNumDep.Value = estProm01.strNumDependientes.Value;
                    //MODIF MAP ART.115 2016 EL TIPO DE PERSONA YA SE DEBE CAPTURAR EN EL MASIVOS REGISTRO 21
                    strDatPersonales[0].strDPTpoPers.Value = estProm01.strTipoPerso.Value == "00" ? "01" : estProm01.strTipoPerso.Value;
                    
                    if (estProm01.strFirmaSol.Value == "S")
                    {
                        strDatPersonales[0].strDPFirma.Value = "1";
                        strDatPersonales[0].strDPFirmaBuro.Value = "1";
                    }
                    else
                    {
                        strDatPersonales[0].strDPFirma.Value = "0";
                        strDatPersonales[0].strDPFirmaBuro.Value = "0";
                    }
                    strDatPersonales[0].strDPEmail.Value = estProm19.strCorreoE.Value;
                    strDatPersonales[0].strDPFecAltaCte.Value = mdlGlobales.funZeroes(8);
                    strDatPersonales[0].strDPParentesco.Value = mdlGlobales.funZeroes(2);
                    if (Conversion.Val(strDatPersonales[0].strDPNumClieBnx.Value) > 0)
                    {
                        strDatPersonales[0].strDPEsCliente.Value = "01";
                    }
                    else
                    {
                        strDatPersonales[0].strDPEsCliente.Value = "00";
                    }
                    strDatPersonales[0].strDPLugar.Value = estProm01.strLugar.Value;
                    strDatPersonales[0].strDPIndPartIng.Value = mdlGlobales.funZeroes(1);
                    strDatPersonales[0].strDPPorcAcciones.Value = mdlGlobales.funZeroes(3);
                    strDatPersonales[0].strDPNumClieBnx.Value = mdlGlobales.funPoneCeros(Conversion.Val(estProm16.strNumCliente.Value).ToString(), 12);
                    double dbNumericTemp = 0;
                    if (!Double.TryParse(estProm11.strIngfijos.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp))
                    {
                        strDatPersonales[0].strDPIngFijosMens.Value = mdlGlobales.funZeroes(8);
                    }
                    else
                    {
                        strDatPersonales[0].strDPIngFijosMens.Value = mdlGlobales.funPoneCeros(estProm11.strIngfijos.Value, 8);
                    }
                    strDatPersonales[0].strDPOtrosIng.Value = StringsHelper.Format(Double.Parse(estProm11.strIngComisiones.Value) + Double.Parse(estProm11.strIngConyugue.Value) + Double.Parse(estProm11.strIngInversiones.Value) + Double.Parse(estProm11.strIngHonorarios.Value) + Double.Parse(estProm11.strIngOtros.Value), "00000000");
                    strDatPersonales[0].strDPEgresos.Value = StringsHelper.Format(Double.Parse(estProm11.strEgrGastoFam.Value) + Double.Parse(estProm11.strEgrPagoAdeudo.Value) + Double.Parse(estProm11.strEgrOtros.Value), "00000000");
                    strDatPersonales[0].strDPTipoSegSoc.Value = mdlGlobales.funZeroes(1); //MMS 11/05 Se agrega el campo Tipo Seguridad Social
                    strDatPersonales[0].strDPHijosMenores.Value = mdlGlobales.funZeroes(2); //MMS 11/05 se agrega el campo Número de Hijos menores de 18 años
                    strDatPersonales[0].strDPPaisNacimiento.Value = estProm01.strPNacimiento.Value; //AEFS Campo Nuevo
                    strDatPersonales[0].strDPFIEL.Value = estProm01.strFIEL.Value; //AEFS Campo Nuevo
                    //*** INI - IRP – Proy. 66008-06
                    strDatPersonales[0].strDPEntFedNac.Value  = estProm01.strEntFedNac.Value;

                    //MODIF MAP ART.115 2016
                    strDatPersonales[0].strIdentFis1.Value = estProm01.strIdentFis1.Value.PadRight(20,' ');
                    strDatPersonales[0].strPaisAsig1.Value = estProm01.strPaisAsig1.Value.PadLeft(4, '0');
                    strDatPersonales[0].strIdentFis2.Value = estProm01.strIdentFis2.Value.PadRight(20, ' ');
                    strDatPersonales[0].strPaisAsig2.Value = estProm01.strPaisAsig2.Value.PadLeft(4, '0');
                    strDatPersonales[0].strFechaCons.Value = estProm01.strFechaCons.Value.PadLeft(8, '0');
                    strDatPersonales[0].strGiroEmpre.Value = estProm01.strGiroEmpre.Value.PadLeft(6, '0');
                    strDatPersonales[0].strTrabExtra.Value = estProm01.strTrabExtra.Value.PadRight(2, ' ');


                    //*** FIN - IRP – Proy. 66008-06
                    if (estProm05.strPaterno.Value.Trim().Length > 0 && estProm05.strNombre.Value.Trim().Length > 0)
                    { //Validar que existan al menos el apellido paterno y el nombre
                        strDatPersonales[1].strDPEtiqueta.Value = gcstrDPSolicitante; //Etiqueta BDP
                        strDatPersonales[1].strDPClaveParticip.Value = "03";
                        strDatPersonales[1].strDPTipoRelacion.Value = "01";
                        strDatPersonales[1].strDPIndicParticip.Value = "01";
                        strDatPersonales[1].strDPApPaterno.Value = estProm05.strPaterno.Value.Trim();
                        strDatPersonales[1].strDPApMaterno.Value = estProm05.strMaterno.Value.Trim();
                        strDatPersonales[1].strDPNombres.Value = estProm05.strNombre.Value.Trim();
                        strDatPersonales[1].strDPCalleNum.Value = estProm01.strDomicilio.Value;
                        strDatPersonales[1].strDPColPob.Value = estProm01.strColonia.Value;
                        strDatPersonales[1].strDPCodPos.Value = mdlGlobales.funPoneCeros(Conversion.Val(estProm01.strCodigoPostal.Value).ToString(), 5);
                        strDatPersonales[1].strDPDelMuni.Value = estProm01.strMunicipio.Value;
                        string tempRefParam43 = "19";
                        string tempRefParam44 = null;
                        string tempRefParam45 = null;
                        string tempRefParam46 = null;
                        Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam47 = Catalogos.clsCatalogos.enmCamposCatalogos.Atributos;
                        string tempRefParam48 = "E";
                        if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam43, estProm01.strEstado.Value, tempRefParam44, tempRefParam45, ref tempRefParam46, ref tempRefParam47, ref tempRefParam48))
                        {
                            strDatPersonales[1].strDPCveEstado.Value = "00";
                        }
                        else
                        {
                            strDatPersonales[1].strDPCveEstado.Value = mdlComunica.OleCatalogos.getLlave1.Substring(mdlComunica.OleCatalogos.getLlave1.Length - Math.Min(mdlComunica.OleCatalogos.getLlave1.Length, 2));
                        }
                        if (Conversion.Val(estProm01.strLada.Value) > 0 && Conversion.Val(estProm01.strTelefono.Value) == 0)
                        {
                            strDatPersonales[1].strDPLADA.Value = mdlGlobales.funZeroes(3);
                            strDatPersonales[1].strDPTelef.Value = mdlGlobales.funZeroes(7);
                        }
                        else
                        {
                            strDatPersonales[1].strDPLADA.Value = mdlGlobales.funPoneCeros(Conversion.Val(estProm01.strLada.Value).ToString(), 3);
                            strDatPersonales[1].strDPTelef.Value = mdlGlobales.funPoneCeros(Conversion.Val(estProm01.strTelefono.Value).ToString(), 7);
                        }
                        strDatPersonales[1].strDPExtension.Value = mdlGlobales.funZeroes(5);
                        strDatPersonales[1].strDPCodAreaCEL.Value = mdlGlobales.funZeroes(3);
                        strDatPersonales[1].strDPTelefonoCEL.Value = mdlGlobales.funZeroes(7);
                        strDatPersonales[1].strDPCodAreaFAX.Value = mdlGlobales.funZeroes(3);
                        strDatPersonales[1].strDPTelefonoFAX.Value = mdlGlobales.funZeroes(7);
                        strDatPersonales[1].strDPCodAreaOtro.Value = mdlGlobales.funZeroes(3);
                        strDatPersonales[1].strDPOtroTelef.Value = mdlGlobales.funZeroes(7);
                        strDatPersonales[1].strDPCveNac.Value = mdlGlobales.funZeroes(1);
                        strDatPersonales[1].strDPPais.Value = mdlGlobales.funZeroes(4); //MMS 11/05 Se agrega el campo País de Origen
                        strDatPersonales[1] = strDatPersonales[0];
                        strDatPersonales[1].strDPFecNac.Value = mdlTranMasivo.funValidaFechaNacimiento(estProm16.strFecNacimiento.Value);
                        strDatPersonales[1].strDPRFC.Value = new String(' ', 13);
                        strDatPersonales[1].strDPSeguroSocial.Value = mdlGlobales.funZeroes(11);
                        strDatPersonales[1].strDPCURP.Value = new String(' ', 18);
                        strDatPersonales[1].strDPTmpRes.Value = mdlGlobales.funZeroes(2);
                        strDatPersonales[1].strDPCveSexo.Value = new String(' ', 1);
                        strDatPersonales[1].strDPCveEsc.Value = mdlGlobales.funZeroes(2);
                        strDatPersonales[1] = strDatPersonales[0]; //El conyuge y el solicitante comparten el tipo de vivienda
                        strDatPersonales[1].strDPNumDep.Value = estProm01.strNumDependientes.Value;
                        strDatPersonales[1].strDPTpoPers.Value = "01";
                        strDatPersonales[1].strDPFirma.Value = "0";
                        strDatPersonales[1].strDPFirmaBuro.Value = "0";
                        strDatPersonales[1].strDPEmail.Value = new String(' ', 78);//AEFS cambio de 40 a 78
                        strDatPersonales[1].strDPFecAltaCte.Value = mdlGlobales.funZeroes(8);
                        strDatPersonales[1].strDPParentesco.Value = "02";
                        strDatPersonales[1].strDPEsCliente.Value = mdlGlobales.funZeroes(2);
                        strDatPersonales[1].strDPLugar.Value = new String(' ', 20);
                        strDatPersonales[1].strDPIndPartIng.Value = mdlGlobales.funZeroes(1);
                        strDatPersonales[1].strDPPorcAcciones.Value = mdlGlobales.funZeroes(3);
                        strDatPersonales[1].strDPNumClieBnx.Value = mdlGlobales.funZeroes(12);
                        strDatPersonales[1].strDPIngFijosMens.Value = mdlGlobales.funZeroes(8);
                        strDatPersonales[1].strDPOtrosIng.Value = mdlGlobales.funZeroes(8);
                        strDatPersonales[1].strDPEgresos.Value = mdlGlobales.funZeroes(8);
                        strDatPersonales[1].strDPTipoSegSoc.Value = mdlGlobales.funZeroes(1); //MMS 11/05 Se agrega el campo Tipo Seguridad Social
                        strDatPersonales[1].strDPHijosMenores.Value = mdlGlobales.funZeroes(2); //MMS 11/05 se agrega el campo Número de Hijos menores de 18 años
                    }
                    else
                    {
                        strDatPersonales[1].strDPEtiqueta.Value = "";
                    }

                    if (estProm14.strPaterno.Value.Trim().Length > 0 && estProm14.strNombre.Value.Trim().Length > 0 && estProm14.strDomicilio.Value.Trim().Length > 0)
                    {
                        strDatPersonales[2].strDPEtiqueta.Value = gcstrDPSolicitante; //Etiqueta BDP
                        strDatPersonales[2].strDPClaveParticip.Value = "02";
                        strDatPersonales[2].strDPTipoRelacion.Value = "01";
                        strDatPersonales[2] = strDatPersonales[0]; //Consecutivo del bloque 1.0
                        strDatPersonales[2].strDPApPaterno.Value = estProm14.strPaterno.Value.Trim();
                        strDatPersonales[2].strDPApMaterno.Value = estProm14.strMaterno.Value.Trim();
                        strDatPersonales[2].strDPNombres.Value = estProm14.strNombre.Value.Trim();
                        strDatPersonales[2].strDPCalleNum.Value = estProm14.strDomicilio.Value;
                        strDatPersonales[2].strDPColPob.Value = estProm14.strColonia.Value;
                        strDatPersonales[2].strDPCodPos.Value = mdlGlobales.funPoneCeros(estProm14.strCodigoPostal.Value, 5);
                        strDatPersonales[2].strDPDelMuni.Value = estProm14.strMunicipio.Value;
                        string tempRefParam49 = "19";
                        string tempRefParam50 = null;
                        string tempRefParam51 = null;
                        string tempRefParam52 = null;
                        Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam53 = Catalogos.clsCatalogos.enmCamposCatalogos.Atributos;
                        string tempRefParam54 = "E";
                        if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam49, estProm14.strEstado.Value, tempRefParam50, tempRefParam51, ref tempRefParam52, ref tempRefParam53, ref tempRefParam54))
                        {
                            strDatPersonales[2].strDPCveEstado.Value = "00";
                        }
                        else
                        {
                            strDatPersonales[2].strDPCveEstado.Value = mdlComunica.OleCatalogos.getLlave1.Substring(mdlComunica.OleCatalogos.getLlave1.Length - Math.Min(mdlComunica.OleCatalogos.getLlave1.Length, 2));
                        }
                        strDatPersonales[2].strDPLADA.Value = mdlGlobales.funPoneCeros(Conversion.Val(estProm14.strLada.Value).ToString(), 3);
                        strDatPersonales[2].strDPTelef.Value = mdlGlobales.funPoneCeros(Conversion.Val(estProm14.strTelefono.Value).ToString(), 7);
                        strDatPersonales[2].strDPExtension.Value = mdlGlobales.funZeroes(5);
                        strDatPersonales[2].strDPCodAreaCEL.Value = mdlGlobales.funZeroes(3);
                        strDatPersonales[2].strDPTelefonoCEL.Value = mdlGlobales.funZeroes(7);
                        strDatPersonales[2].strDPCodAreaFAX.Value = mdlGlobales.funZeroes(3);
                        strDatPersonales[2].strDPTelefonoFAX.Value = mdlGlobales.funZeroes(7);
                        strDatPersonales[2].strDPCodAreaOtro.Value = mdlGlobales.funZeroes(3);
                        strDatPersonales[2].strDPOtroTelef.Value = mdlGlobales.funZeroes(7);
                        strDatPersonales[2].strDPCveNac.Value = mdlGlobales.funZeroes(1);
                        strDatPersonales[2].strDPPais.Value = mdlGlobales.funZeroes(4); //MMS 11/05 Se agrega el campo País de Origen
                        strDatPersonales[2].strDPEstCiv.Value = mdlGlobales.funZeroes(2);
                        strDatPersonales[2].strDPFecNac.Value = mdlTranMasivo.funValidaFechaNacimiento(estProm17.strFecNacimiento.Value);
                        strDatPersonales[2].strDPRFC.Value = estProm17.strRFC.Value;
                        strDatPersonales[2].strDPSeguroSocial.Value = mdlGlobales.funZeroes(11);
                        strDatPersonales[2].strDPCURP.Value = mdlGlobales.funZeroes(18);
                        strDatPersonales[2].strDPTmpRes.Value = mdlGlobales.funZeroes(2);
                        strDatPersonales[2].strDPCveSexo.Value = new String(' ', 1);
                        strDatPersonales[2].strDPCveEsc.Value = mdlGlobales.funZeroes(2);
                        strDatPersonales[2].strDPTpoViv.Value = mdlGlobales.funZeroes(2);
                        strDatPersonales[2].strDPNumDep.Value = mdlGlobales.funZeroes(2);
                        strDatPersonales[2].strDPTpoPers.Value = "01";
                        if (estProm14.strFirma.Value == "S")
                        {
                            strDatPersonales[2].strDPFirma.Value = "1";
                        }
                        else
                        {
                            strDatPersonales[2].strDPFirma.Value = "0";
                        }
                        strDatPersonales[2].strDPFirmaBuro.Value = "0";
                        strDatPersonales[2].strDPEmail.Value = new String(' ', 78);//AEFS Cambio de 40 a 78
                        strDatPersonales[2].strDPFecAltaCte.Value = mdlGlobales.funZeroes(8);
                        strDatPersonales[2].strDPParentesco.Value = mdlGlobales.funZeroes(2);
                        strDatPersonales[2].strDPEsCliente.Value = mdlGlobales.funZeroes(2);
                        strDatPersonales[2].strDPLugar.Value = new String(' ', 20);
                        strDatPersonales[2].strDPIndPartIng.Value = mdlGlobales.funZeroes(1);
                        strDatPersonales[2].strDPPorcAcciones.Value = mdlGlobales.funZeroes(3);
                        strDatPersonales[2].strDPNumClieBnx.Value = mdlGlobales.funZeroes(12);
                        strDatPersonales[2].strDPIngFijosMens.Value = mdlGlobales.funPoneCeros(Conversion.Val(estProm14.strIngMensuales.Value).ToString(), 8);
                        strDatPersonales[2].strDPOtrosIng.Value = mdlGlobales.funPoneCeros(Conversion.Val(estProm17.strOtrosIngresos.Value).ToString(), 8);
                        strDatPersonales[2].strDPEgresos.Value = mdlGlobales.funZeroes(8);
                        strDatPersonales[2].strDPTipoSegSoc.Value = mdlGlobales.funZeroes(1); //MMS 11/05 Se agrega el campo Tipo Seguridad Social
                        strDatPersonales[2].strDPHijosMenores.Value = mdlGlobales.funZeroes(2); //MMS 11/05 se agrega el campo Número de Hijos menores de 18 años
                    }
                    else
                    {
                        strDatPersonales[2].strDPEtiqueta.Value = "";
                    }

                    if ((((Conversion.Val(estProm03.strTelefono.Value) > 0 || estProm03.strNomEmpresa.Value.Trim().Length > 0) ? -1 : 0) | estProm03.strDomicilio.Value.Trim().Length | ((Conversion.Val(estProm03.strAntiguedad.Value) > 0) ? -1 : 0) | ((Conversion.Val(estProm16.strProfOfic.Value) > 0) ? -1 : 0) | ((Conversion.Val(estProm16.strProfOfic.Value) > 0) ? -1 : 0) | ((estProm03.strDeptoArea.Value.Trim().Length > 0) ? -1 : 0)) != 0)
                    {
                        strDatEmpleo[0].strDEEtiqueta.Value = gcstrDESolicitante; //Etiqueta BDE
                        strDatEmpleo[0].strDEClaveParticip.Value = "01";
                        strDatEmpleo[0].strDETipoRelacion.Value = "01";
                        strDatEmpleo[0].strDEIndicParticip.Value = "01";
                        string tempRefParam55 = "23";
                        string tempRefParam56 = null;
                        string tempRefParam57 = null;
                        string tempRefParam58 = null;
                        Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam59 = Catalogos.clsCatalogos.enmCamposCatalogos.Default;
                        string tempRefParam60 = "D";
                        if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam55, estProm03.strOcupacion.Value, tempRefParam56, tempRefParam57, ref tempRefParam58, ref tempRefParam59, ref tempRefParam60))
                        {
                            strDatEmpleo[0].strDECveOcuProf.Value = "0000";
                        }
                        else
                        {
                            strDatEmpleo[0].strDECveOcuProf.Value = mdlComunica.OleCatalogos.getLlave1.Substring(mdlComunica.OleCatalogos.getLlave1.Length - Math.Min(mdlComunica.OleCatalogos.getLlave1.Length, 4));
                        }
                        strDatEmpleo[0].strDENomEmp.Value = estProm03.strNomEmpresa.Value;
                        strDatEmpleo[0].strDECalleNum.Value = estProm03.strDomicilio.Value;
                        strDatEmpleo[0].strDEColPob.Value = estProm03.strColPobFracc.Value;
                        strDatEmpleo[0].strDECodPos.Value = estProm03.strCodigoPostal.Value;
                        strDatEmpleo[0].strDEDelMuni.Value = estProm03.strMunicipio.Value;
                        string tempRefParam61 = "19";
                        string tempRefParam62 = null;
                        string tempRefParam63 = null;
                        string tempRefParam64 = null;
                        Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam65 = Catalogos.clsCatalogos.enmCamposCatalogos.Atributos;
                        string tempRefParam66 = "E";
                        if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam61, estProm03.strEstado.Value, tempRefParam62, tempRefParam63, ref tempRefParam64, ref tempRefParam65, ref tempRefParam66))
                        {
                            strDatEmpleo[0].strDECveEstado.Value = "00";
                        }
                        else
                        {
                            strDatEmpleo[0].strDECveEstado.Value = mdlComunica.OleCatalogos.getLlave1.Substring(mdlComunica.OleCatalogos.getLlave1.Length - Math.Min(mdlComunica.OleCatalogos.getLlave1.Length, 2));
                        }
                        strDatEmpleo[0].strDELADA.Value = mdlGlobales.funPoneCeros(Conversion.Val(estProm03.strLada.Value).ToString(), 3);
                        strDatEmpleo[0].strDETelef.Value = estProm03.strTelefono.Value;
                        strDatEmpleo[0].strDEExten.Value = mdlGlobales.funPoneCeros(estProm03.strExtension.Value, 5);
                        if (Conversion.Val(estProm03.strExtension.Value) > 0 && Conversion.Val(estProm01.strTelefono.Value) == 0)
                        {
                            strDatEmpleo[0].strDELADA.Value = mdlGlobales.funZeroes(3);
                            strDatEmpleo[0].strDETelef.Value = mdlGlobales.funZeroes(7);
                            strDatEmpleo[0].strDEExten.Value = mdlGlobales.funZeroes(4);
                        }
                        else
                        {
                            strDatEmpleo[0].strDELADA.Value = mdlGlobales.funPoneCeros(estProm03.strLada.Value, 3);
                            strDatEmpleo[0].strDETelef.Value = mdlGlobales.funPoneCeros(estProm03.strTelefono.Value, 7);
                            strDatEmpleo[0].strDEExten.Value = mdlGlobales.funPoneCeros(estProm03.strExtension.Value, 5);
                        }
                        strDatEmpleo[0].strDECodAreaFAX.Value = mdlGlobales.funZeroes(3);
                        strDatEmpleo[0].strDETelefonoFAX.Value = mdlGlobales.funZeroes(7);
                        strDatEmpleo[0].strDEDepto.Value = estProm03.strDeptoArea.Value;
                        strDatEmpleo[0].strDEGiroEmp.Value = mdlGlobales.funZeroes(6); //ASG Se cambia de 2 a 6
                        strDatEmpleo[0].strDECveSec.Value = mdlGlobales.funPoneCeros(estProm16.strSector.Value, 2);
                        strDatEmpleo[0].strDECvePuesto.Value = mdlGlobales.funZeroes(2);
                        strDatEmpleo[0].strDEProfOficio.Value = estProm16.strProfOfic.Value;
                        strDatEmpleo[0].strDEAMAntig.Value = estProm03.strAntiguedad.Value; //ASG Se toma el valor completo
                        strDatEmpleo[0].strDEFecIngreso.Value = mdlGlobales.funZeroes(8);
                    }
                    else
                    {
                        strDatEmpleo[0].strDEEtiqueta.Value = "";
                    }

                    if ((((estProm14.strPaterno.Value.Trim().Length > 0 && estProm14.strNombre.Value.Trim().Length > 0) ? -1 : 0) & estProm14.strDomicilio.Value.Trim().Length & ((estProm14.strNomEmpresa.Value.Trim().Length > 0 || estProm14.strDomicilio.Value.Trim().Length > 0 || Conversion.Val(estProm14.strAntiguedad.Value) > 0) ? -1 : 0)) != 0)
                    {
                        strDatEmpleo[1].strDEEtiqueta.Value = gcstrDESolicitante; //Etiqueta BDE
                        strDatEmpleo[1].strDEClaveParticip.Value = "02";
                        strDatEmpleo[1].strDETipoRelacion.Value = "01";
                        strDatEmpleo[1].strDEIndicParticip.Value = "01";
                        string tempRefParam67 = "17";
                        string tempRefParam68 = null;
                        string tempRefParam69 = null;
                        string tempRefParam70 = null;
                        Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam71 = Catalogos.clsCatalogos.enmCamposCatalogos.Default;
                        string tempRefParam72 = "D";
                        if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam67, estProm14.strOcupacion.Value, tempRefParam68, tempRefParam69, ref tempRefParam70, ref tempRefParam71, ref tempRefParam72))
                        {
                            strDatEmpleo[1].strDECveOcuProf.Value = "0000";
                        }
                        else
                        {
                            strDatEmpleo[1].strDECveOcuProf.Value = mdlComunica.OleCatalogos.getLlave1.Substring(mdlComunica.OleCatalogos.getLlave1.Length - Math.Min(mdlComunica.OleCatalogos.getLlave1.Length, 4));
                        }
                        string tempRefParam73 = "23";
                        string tempRefParam74 = null;
                        string tempRefParam75 = null;
                        string tempRefParam76 = null;
                        Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam77 = Catalogos.clsCatalogos.enmCamposCatalogos.Default;
                        string tempRefParam78 = "D";
                        if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam73, estProm14.strOcupacion.Value, tempRefParam74, tempRefParam75, ref tempRefParam76, ref tempRefParam77, ref tempRefParam78))
                        {
                            strDatEmpleo[1].strDECveOcuProf.Value = "0000";
                        }
                        else
                        {
                            strDatEmpleo[1].strDECveOcuProf.Value = mdlComunica.OleCatalogos.getLlave1.Substring(mdlComunica.OleCatalogos.getLlave1.Length - Math.Min(mdlComunica.OleCatalogos.getLlave1.Length, 4));
                        }
                        strDatEmpleo[1].strDENomEmp.Value = estProm14.strNomEmpresa.Value;
                        strDatEmpleo[1].strDECalleNum.Value = estProm14.strDomicilio.Value;
                        strDatEmpleo[1].strDEColPob.Value = estProm14.strColonia.Value;
                        strDatEmpleo[1].strDECodPos.Value = mdlGlobales.funPoneCeros(Conversion.Val(estProm14.strCodigoPostal.Value).ToString(), 5);
                        strDatEmpleo[1].strDEDelMuni.Value = estProm14.strMunicipio.Value;
                        string tempRefParam79 = "19";
                        string tempRefParam80 = null;
                        string tempRefParam81 = null;
                        string tempRefParam82 = null;
                        Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam83 = Catalogos.clsCatalogos.enmCamposCatalogos.Atributos;
                        string tempRefParam84 = "E";
                        if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam79, estProm14.strEstado.Value, tempRefParam80, tempRefParam81, ref tempRefParam82, ref tempRefParam83, ref tempRefParam84))
                        {
                            strDatEmpleo[1].strDECveEstado.Value = "00";
                        }
                        else
                        {
                            strDatEmpleo[1].strDECveEstado.Value = mdlComunica.OleCatalogos.getLlave1.Substring(mdlComunica.OleCatalogos.getLlave1.Length - Math.Min(mdlComunica.OleCatalogos.getLlave1.Length, 2));
                        }
                        strDatEmpleo[1].strDELADA.Value = mdlGlobales.funPoneCeros(Conversion.Val(estProm14.strLada.Value).ToString(), 3);
                        strDatEmpleo[1].strDETelef.Value = mdlGlobales.funPoneCeros(Conversion.Val(estProm14.strTelefono.Value).ToString(), 7);
                        strDatEmpleo[1].strDEExten.Value = mdlGlobales.funPoneCeros(Conversion.Val(estProm14.strExtension.Value).ToString(), 5);
                        strDatEmpleo[1].strDECodAreaFAX.Value = mdlGlobales.funZeroes(3);
                        strDatEmpleo[1].strDETelefonoFAX.Value = mdlGlobales.funZeroes(7);
                        strDatEmpleo[1].strDEDepto.Value = new String(' ', 30);
                        strDatEmpleo[1].strDEGiroEmp.Value = mdlGlobales.funZeroes(6); //ASG Se cambia de 2 a 6
                        strDatEmpleo[1].strDECveSec.Value = mdlGlobales.funZeroes(2);
                        strDatEmpleo[1].strDECvePuesto.Value = mdlGlobales.funZeroes(2);
                        strDatEmpleo[1].strDEProfOficio.Value = new String(' ', 30);
                        strDatEmpleo[1].strDEAMAntig.Value = mdlGlobales.funPoneCeros(Conversion.Val(estProm14.strAntiguedad.Value).ToString(), 4); //Se toma el valor completo
                        strDatEmpleo[1].strDEFecIngreso.Value = mdlGlobales.funZeroes(8);
                    }
                    else
                    {
                        strDatEmpleo[1].strDEEtiqueta.Value = "";
                    }

                    if ((estProm05.strPaterno.Value.Trim().Length > 0 && estProm05.strNombre.Value.Trim().Length > 0) && (estProm05.strNombreEmp.Value.Trim().Length > 0 || estProm05.strDomicilio.Value.Trim().Length > 0))
                    {
                        strDatEmpleo[2].strDEEtiqueta.Value = gcstrDESolicitante; //Etiqueta BDE
                        strDatEmpleo[2].strDEClaveParticip.Value = "03";
                        strDatEmpleo[2].strDETipoRelacion.Value = "01";
                        strDatEmpleo[2].strDEIndicParticip.Value = "01";
                        strDatEmpleo[2].strDECveOcuProf.Value = mdlGlobales.funZeroes(4);
                        strDatEmpleo[2].strDENomEmp.Value = estProm05.strNombreEmp.Value;
                        strDatEmpleo[2].strDECalleNum.Value = estProm05.strDomicilio.Value;
                        strDatEmpleo[2].strDEColPob.Value = estProm05.strColPobFracc.Value;
                        strDatEmpleo[2].strDECodPos.Value = mdlGlobales.funPoneCeros(Conversion.Val(estProm05.strCodigoPostal.Value).ToString(), 5);
                        strDatEmpleo[2].strDEDelMuni.Value = estProm05.strMunicipio.Value;
                        string tempRefParam85 = "19";
                        string tempRefParam86 = null;
                        string tempRefParam87 = null;
                        string tempRefParam88 = null;
                        Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam89 = Catalogos.clsCatalogos.enmCamposCatalogos.Atributos;
                        string tempRefParam90 = "E";
                        if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam85, estProm05.strEstado.Value, tempRefParam86, tempRefParam87, ref tempRefParam88, ref tempRefParam89, ref tempRefParam90))
                        {
                            strDatEmpleo[2].strDECveEstado.Value = "00";
                        }
                        else
                        {
                            strDatEmpleo[2].strDECveEstado.Value = mdlComunica.OleCatalogos.getLlave1.Substring(mdlComunica.OleCatalogos.getLlave1.Length - Math.Min(mdlComunica.OleCatalogos.getLlave1.Length, 2));
                        }
                        strDatEmpleo[2].strDELADA.Value = mdlGlobales.funPoneCeros(Conversion.Val(estProm05.strLada.Value).ToString(), 3);
                        strDatEmpleo[2].strDETelef.Value = mdlGlobales.funPoneCeros(Conversion.Val(estProm05.strTelefono.Value).ToString(), 7);
                        strDatEmpleo[2].strDEExten.Value = mdlGlobales.funPoneCeros(Conversion.Val(estProm05.strExtension.Value).ToString(), 5);
                        strDatEmpleo[2].strDECodAreaFAX.Value = mdlGlobales.funZeroes(3);
                        strDatEmpleo[2].strDETelefonoFAX.Value = mdlGlobales.funZeroes(7);
                        strDatEmpleo[2].strDEDepto.Value = new String(' ', 30);
                        strDatEmpleo[2].strDEGiroEmp.Value = mdlGlobales.funZeroes(6); //ASG Se cambia de 2 a 6
                        strDatEmpleo[2].strDECveSec.Value = mdlGlobales.funZeroes(2);
                        strDatEmpleo[2].strDECvePuesto.Value = mdlGlobales.funZeroes(2);
                        strDatEmpleo[2].strDEProfOficio.Value = new String(' ', 30);
                        strDatEmpleo[2].strDEAMAntig.Value = mdlGlobales.funZeroes(4); //ASG Se cambia de 2 a 4
                        strDatEmpleo[2].strDEFecIngreso.Value = mdlGlobales.funZeroes(8);
                    }
                    else
                    {
                        strDatEmpleo[2].strDEEtiqueta.Value = "";
                    }

                    //Validar si se genera el bloque conque cumpla cualquiera de las condiciones para generar ocurrencias
                    if ((Conversion.Val(estProm07.strNumCuenta.Value) > 0 && Conversion.Val(estProm07.strTipoServicio.Value) > 0) || (Conversion.Val(estProm08[0].strEmisor.Value) > 0 && Conversion.Val(estProm08[0].strNumCuenta.Value) > 0 && Conversion.Val(estProm08[0].strTipoServicio.Value) > 0) || (Conversion.Val(estProm09[0].strEmisor.Value) > 0 && Conversion.Val(estProm09[0].strNumCuenta.Value) > 0) || (Conversion.Val(estProm19.strNumCuenta.Value) > 0))
                    {
                        IntContReferencias = 0;
                        strHdrRCredBancOtros[0].strRCEtiqueta.Value = gcstrRCSolicitante; //Etiqueta BRC
                        strHdrRCredBancOtros[0].strRCClaveParticip.Value = "01";
                        strHdrRCredBancOtros[0].strRCTipoRelacion.Value = "01";
                        strHdrRCredBancOtros[0].strRCIndicParticip.Value = "01";
                        //ASG El no. de referencias del bloque se llenará al final del análisis de referencias
                        strRCredBancOtros = ArraysHelper.InitializeArray<udfRefCredBancOtros[,]>(new int[] { 1, 1 }); //Inicializar las ocurrencias
                        //ASG Comenzar a analizar los datos que trae el archivo de promotora para armar las ocurrencias
                        if ((((Conversion.Val(estProm07.strNumCuenta.Value) > 0 && Conversion.Val(estProm07.strSucursal.Value) > 0 && Conversion.Val(estProm07.strTipoServicio.Value) > 0) ? -1 : 0) & Convert.ToInt32(Conversion.Val((Double.Parse(estProm07.strTipoServicio.Value) < 9).ToString()))) != 0)
                        { //Si existen los datos del registro 7, arma la ocurrencia correspondiente, validando que solo se encuentre desde el tipo de servicio 1 al 8
                            IntContReferencias++;
                            strRCredBancOtros = ArraysHelper.RedimPreserve<udfRefCredBancOtros[,]>(strRCredBancOtros, new int[] { 1, IntContReferencias + 1 });
                            strRCredBancOtros[0, IntContReferencias].strRCCveProceso.Value = "005"; //MMS 11/05 Incremento en la longitud del campo Proceso ('05' a '005')
                            strRCredBancOtros[0, IntContReferencias].strRCTipoCuenta.Value = "01";
                            switch (estProm07.strTipoServicio.Value)
                            {
                                case "01":
                                    double dbNumericTemp2 = 0;
                                    if (Double.TryParse(estProm07.strNumCuenta.Value, NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp2))
                                    {
                                        if (mdlDigver.DigVer_Valida_DigVer_cta(1, estProm07.strNumCuenta.Value) == (-1))
                                        {
                                            strRCredBancOtros[0, IntContReferencias].strRCInstitOtorga.Value = "01";
                                        }
                                        else
                                        {
                                            strRCredBancOtros[0, IntContReferencias].strRCInstitOtorga.Value = "02";
                                        }
                                    }
                                    else
                                    {
                                        strRCredBancOtros[0, IntContReferencias].strRCInstitOtorga.Value = "02";
                                    }
                                    strRCredBancOtros[0, IntContReferencias].strRCNumCuenta.Value = estProm07.strSucursal.Value + mdlGlobales.funZeroes(5) + estProm07.strNumCuenta.Value.Substring(estProm07.strNumCuenta.Value.Length - Math.Min(estProm07.strNumCuenta.Value.Length, 7));
                                    break;
                                case "02":
                                case "03":
                                case "05":
                                case "04":
                                case "06":
                                case "07":
                                case "08":
                                    strRCredBancOtros[0, IntContReferencias].strRCInstitOtorga.Value = "01";
                                    strRCredBancOtros[0, IntContReferencias].strRCNumCuenta.Value = estProm07.strNumCuenta.Value;
                                    break;
                            }
                            strRCredBancOtros[0, IntContReferencias].strRCSaldo.Value = mdlGlobales.funZeroes(9);
                            strRCredBancOtros[0, IntContReferencias].strRCPagoMensual.Value = mdlGlobales.funZeroes(9);
                            strRCredBancOtros[0, IntContReferencias].strRCFechaApertura.Value = mdlGlobales.funZeroes(8);
                        }
                        for (intContReg08 = 0; intContReg08 <= 1; intContReg08++)
                        {
                            //Validar el registro 08
                            if (Conversion.Val(estProm08[intContReg08].strEmisor.Value) > 0 && Conversion.Val(estProm08[intContReg08].strTipoServicio.Value) > 0 && Conversion.Val(estProm08[intContReg08].strNumCuenta.Value) > 0)
                            {
                                IntContReferencias++;
                                strRCredBancOtros = ArraysHelper.RedimPreserve<udfRefCredBancOtros[,]>(strRCredBancOtros, new int[] { 1, IntContReferencias + 1 });
                                strRCredBancOtros[0, IntContReferencias].strRCCveProceso.Value = "005"; //MMS 11/05 Incremento en la longitud del campo Proceso ('05' a '005')
                                strRCredBancOtros[0, IntContReferencias].strRCInstitOtorga.Value = estProm08[intContReg08].strEmisor.Value.Substring(estProm08[intContReg08].strEmisor.Value.Length - Math.Min(estProm08[intContReg08].strEmisor.Value.Length, 2));
                                strRCredBancOtros[0, IntContReferencias].strRCTipoCuenta.Value = estProm08[intContReg08].strTipoServicio.Value;
                                strRCredBancOtros[0, IntContReferencias].strRCNumCuenta.Value = estProm08[intContReg08].strNumCuenta.Value;
                                strRCredBancOtros[0, IntContReferencias].strRCSaldo.Value = mdlGlobales.funZeroes(9);
                                strRCredBancOtros[0, IntContReferencias].strRCPagoMensual.Value = mdlGlobales.funZeroes(9);
                                strRCredBancOtros[0, IntContReferencias].strRCFechaApertura.Value = mdlGlobales.funZeroes(8);
                            }
                        }
                        for (intContReg09 = 0; intContReg09 <= 1; intContReg09++)
                        {
                            //Validar el registro 09
                            if (Conversion.Val(estProm09[intContReg09].strNumCuenta.Value) > 0 && Conversion.Val(estProm09[intContReg09].strEmisor.Value) > 0)
                            {
                                IntContReferencias++;
                                strRCredBancOtros = ArraysHelper.RedimPreserve<udfRefCredBancOtros[,]>(strRCredBancOtros, new int[] { 1, IntContReferencias + 1 });
                                strRCredBancOtros[0, IntContReferencias].strRCCveProceso.Value = "005"; //MMS 11/05 Incremento en la longitud del campo Proceso ('05' a '005')
                                strRCredBancOtros[0, IntContReferencias].strRCInstitOtorga.Value = estProm09[intContReg09].strEmisor.Value.Substring(estProm09[intContReg09].strEmisor.Value.Length - Math.Min(estProm09[intContReg09].strEmisor.Value.Length, 2));
                                strRCredBancOtros[0, IntContReferencias].strRCTipoCuenta.Value = "04";
                                strRCredBancOtros[0, IntContReferencias].strRCNumCuenta.Value = mdlGlobales.funPoneCeros(estProm09[intContReg09].strNumCuenta.Value, 16);
                                strRCredBancOtros[0, IntContReferencias].strRCSaldo.Value = mdlGlobales.funZeroes(9);
                                strRCredBancOtros[0, IntContReferencias].strRCPagoMensual.Value = mdlGlobales.funZeroes(9);
                                strRCredBancOtros[0, IntContReferencias].strRCFechaApertura.Value = mdlGlobales.funZeroes(8);
                            }
                        }
                        //Validar el registro 19  para armar las 2 ultimas ocurrencias
                        if (Conversion.Val(estProm19.strNumCuenta.Value) > 0)
                        {
                            //Armar las 2 últimas referencias
                            IntContReferencias++;
                            strRCredBancOtros = ArraysHelper.RedimPreserve<udfRefCredBancOtros[,]>(strRCredBancOtros, new int[] { 1, IntContReferencias + 1 });
                            strRCredBancOtros[0, IntContReferencias].strRCCveProceso.Value = "005"; //MMS 11/05 Incremento en la longitud del campo Proceso ('05' a '005')
                            strRCredBancOtros[0, IntContReferencias].strRCInstitOtorga.Value = "01";
                            strRCredBancOtros[0, IntContReferencias].strRCTipoCuenta.Value = "04";
                            strRCredBancOtros[0, IntContReferencias].strRCNumCuenta.Value = mdlGlobales.funPoneCeros(estProm19.strNumCuenta.Value, 16);
                            strRCredBancOtros[0, IntContReferencias].strRCSaldo.Value = mdlGlobales.funZeroes(9);
                            strRCredBancOtros[0, IntContReferencias].strRCPagoMensual.Value = mdlGlobales.funZeroes(9);
                            strRCredBancOtros[0, IntContReferencias].strRCFechaApertura.Value = mdlGlobales.funZeroes(8);
                            IntContReferencias++;
                            strRCredBancOtros = ArraysHelper.RedimPreserve<udfRefCredBancOtros[,]>(strRCredBancOtros, new int[] { 1, IntContReferencias + 1 });
                            strRCredBancOtros[0, IntContReferencias].strRCCveProceso.Value = "056"; //MMS 11/05 Incremento en la longitud del campo Proceso ('05' a '005')
                            strRCredBancOtros[0, IntContReferencias].strRCInstitOtorga.Value = "01";
                            strRCredBancOtros[0, IntContReferencias].strRCTipoCuenta.Value = "04";
                            strRCredBancOtros[0, IntContReferencias].strRCNumCuenta.Value = mdlGlobales.funPoneCeros(estProm19.strNumCuenta.Value, 16);
                            strRCredBancOtros[0, IntContReferencias].strRCSaldo.Value = mdlGlobales.funZeroes(9);
                            strRCredBancOtros[0, IntContReferencias].strRCPagoMensual.Value = mdlGlobales.funZeroes(9);
                            strRCredBancOtros[0, IntContReferencias].strRCFechaApertura.Value = mdlGlobales.funZeroes(8);
                        }
                        //ASG Ya calculadas las ocurrencias se agregan a la estructura
                        strHdrRCredBancOtros[0].strRCCantRef.Value = mdlGlobales.funPoneCeros(IntContReferencias.ToString(), 2);
                    }

                    //Referencias crediticias, bancarias y otros créditos (Bloque 3.1)
                    if (estProm17.strReferencia.Value == "S")
                    { //Si el indicador de referencias del registro 17 es <> S no genera el bloque
                        IntContReferencias = 0;
                        strHdrRCredBancOtros[1].strRCEtiqueta.Value = gcstrRCSolicitante; //Etiqueta BRC
                        strHdrRCredBancOtros[1].strRCClaveParticip.Value = "02";
                        strHdrRCredBancOtros[1].strRCTipoRelacion.Value = "01";
                        strHdrRCredBancOtros[1].strRCIndicParticip.Value = "01";
                        strRCredBancOtros = ArraysHelper.RedimPreserve<udfRefCredBancOtros[,]>(strRCredBancOtros, new int[] { 2, IntContReferencias + 1 });
                        //Agregar las ocurrencias
                        if (Conversion.Val(estProm17.strSucursal.Value) > 0)
                        {
                            IntContReferencias++;
                            strRCredBancOtros = ArraysHelper.RedimPreserve<udfRefCredBancOtros[,]>(strRCredBancOtros, new int[] { 2, IntContReferencias + 1 });
                            strRCredBancOtros[1, IntContReferencias].strRCCveProceso.Value = "005"; //MMS 11/05 Incremento en la longitud del campo Proceso ('05' a '005')
                            strRCredBancOtros[1, IntContReferencias].strRCInstitOtorga.Value = "01";
                            strRCredBancOtros[1, IntContReferencias].strRCTipoCuenta.Value = "01";
                            strRCredBancOtros[1, IntContReferencias].strRCNumCuenta.Value = estProm17.strSucursal.Value + mdlGlobales.funZeroes(5) + estProm17.strNumCuenta.Value.Substring(estProm17.strNumCuenta.Value.Length - Math.Min(estProm17.strNumCuenta.Value.Length, 7));
                            strRCredBancOtros[1, IntContReferencias].strRCSaldo.Value = mdlGlobales.funZeroes(9);
                            strRCredBancOtros[1, IntContReferencias].strRCPagoMensual.Value = mdlGlobales.funZeroes(9);
                            strRCredBancOtros[1, IntContReferencias].strRCFechaApertura.Value = mdlGlobales.funZeroes(8);
                        }
                        if (Conversion.Val(estProm17.strEmisorTarjeta1.Value) > 0 && Conversion.Val(estProm17.strEmisorTarjeta1.Value) > 1)
                        {
                            IntContReferencias++;
                            strRCredBancOtros = ArraysHelper.RedimPreserve<udfRefCredBancOtros[,]>(strRCredBancOtros, new int[] { 2, IntContReferencias + 1 });
                            strRCredBancOtros[1, IntContReferencias].strRCCveProceso.Value = "005"; //MMS 11/05 Incremento en la longitud del campo Proceso ('05' a '005')
                            strRCredBancOtros[1, IntContReferencias].strRCInstitOtorga.Value = estProm17.strEmisorTarjeta1.Value;
                            strRCredBancOtros[1, IntContReferencias].strRCTipoCuenta.Value = "04";
                            strRCredBancOtros[1, IntContReferencias].strRCNumCuenta.Value = estProm17.strNumTarjeta1.Value;
                            strRCredBancOtros[1, IntContReferencias].strRCSaldo.Value = mdlGlobales.funZeroes(9);
                            strRCredBancOtros[1, IntContReferencias].strRCPagoMensual.Value = mdlGlobales.funZeroes(9);
                            strRCredBancOtros[1, IntContReferencias].strRCFechaApertura.Value = mdlGlobales.funZeroes(8);
                        }
                        strHdrRCredBancOtros[1].strRCCantRef.Value = mdlGlobales.funPoneCeros(IntContReferencias.ToString(), 2);
                    }

                    for (IntContReferencias = 0; IntContReferencias <= 1; IntContReferencias++)
                    {
                        if (IntContReferencias == 0)
                        {
                            strCvePartReferencias.Value = "06";
                        }
                        else
                        {
                            strCvePartReferencias.Value = "10";
                        }
                        if (estProm06[IntContReferencias].strNombre.Value.Trim().Length > 0 && estProm06[IntContReferencias].strPaterno.Value.Trim().Length > 0 && estProm06[IntContReferencias].strParentesco.Value.Trim().Length > 0)
                        {
                            strRefPersonales[IntContReferencias].strRPEtiqueta.Value = gcstrRPSolicitante; //Etiqueta BRP
                            strRefPersonales[IntContReferencias].strRPClaveParticip.Value = strCvePartReferencias.Value;
                            strRefPersonales[IntContReferencias].strRPTipoRelacion.Value = "01";
                            strRefPersonales[IntContReferencias].strRPIndicParticip.Value = "01";
                            strRefPersonales[IntContReferencias].strRPPaterno.Value = estProm06[IntContReferencias].strPaterno.Value.Trim();
                            strRefPersonales[IntContReferencias].strRPMaterno.Value = estProm06[IntContReferencias].strMaterno.Value.Trim();
                            strRefPersonales[IntContReferencias].strRPNombres.Value = estProm06[IntContReferencias].strNombre.Value.Trim();
                            string tempRefParam91 = "41";
                            string tempRefParam92 = null;
                            string tempRefParam93 = null;
                            string tempRefParam94 = null;
                            Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam95 = Catalogos.clsCatalogos.enmCamposCatalogos.Atributos;
                            string tempRefParam96 = "D";
                            if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam91, estProm06[IntContReferencias].strParentesco.Value, tempRefParam92, tempRefParam93, ref tempRefParam94, ref tempRefParam95, ref tempRefParam96))
                            {
                                strRefPersonales[IntContReferencias].strRPCveParent.Value = "00";
                            }
                            else
                            {
                                strRefPersonales[IntContReferencias].strRPCveParent.Value = mdlComunica.OleCatalogos.getLlave1.Substring(mdlComunica.OleCatalogos.getLlave1.Length - Math.Min(mdlComunica.OleCatalogos.getLlave1.Length, 2));
                            }
                            strRefPersonales[IntContReferencias].strRPCalleNum.Value = estProm06[IntContReferencias].strDomicilio.Value;
                            strRefPersonales[IntContReferencias].strRPColPob.Value = estProm06[IntContReferencias].strColPobFracc.Value;
                            strRefPersonales[IntContReferencias].strRPCodPos.Value = mdlGlobales.funPoneCeros(Conversion.Val(estProm06[IntContReferencias].strCodigoPostal.Value).ToString(), 5);
                            strRefPersonales[IntContReferencias].strRPDelMuni.Value = estProm06[IntContReferencias].strMunicipio.Value;
                            string tempRefParam97 = "19";
                            string tempRefParam98 = null;
                            string tempRefParam99 = null;
                            string tempRefParam100 = null;
                            Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam101 = Catalogos.clsCatalogos.enmCamposCatalogos.Atributos;
                            string tempRefParam102 = "E";
                            if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam97, estProm06[IntContReferencias].strEstado.Value, tempRefParam98, tempRefParam99, ref tempRefParam100, ref tempRefParam101, ref tempRefParam102))
                            {
                                strRefPersonales[IntContReferencias].strRPCveEstado.Value = "00";
                            }
                            else
                            {
                                strRefPersonales[IntContReferencias].strRPCveEstado.Value = mdlComunica.OleCatalogos.getLlave1.Substring(mdlComunica.OleCatalogos.getLlave1.Length - Math.Min(mdlComunica.OleCatalogos.getLlave1.Length, 2));
                            }
                            if (StringsHelper.DoubleValue(estProm06[IntContReferencias].strLada.Value.Trim()) > 0 && StringsHelper.DoubleValue(estProm06[IntContReferencias].strTelefono.Value.Trim()) == 0)
                            {
                                strRefPersonales[IntContReferencias].strRPLADA.Value = mdlGlobales.funZeroes(3);
                                strRefPersonales[IntContReferencias].strRPTelef.Value = mdlGlobales.funZeroes(7);
                            }
                            else
                            {
                                strRefPersonales[IntContReferencias].strRPLADA.Value = estProm06[IntContReferencias].strLada.Value;
                                strRefPersonales[IntContReferencias].strRPTelef.Value = estProm06[IntContReferencias].strTelefono.Value;
                            }
                            strRefPersonales[IntContReferencias].strRPFecNac.Value = mdlGlobales.funZeroes(8);
                            strRefPersonales[IntContReferencias].strRPCveSexo.Value = " ";
                            strRefPersonales[IntContReferencias].strRPFirma.Value = "0";
                        }
                        else
                        {
                            strRefPersonales[IntContReferencias].strRPEtiqueta.Value = "";
                        }
                    }

                    if (estProm12.strPaterno.Value.Trim().Length > 0 && estProm12.strNombre.Value.Trim().Length > 0)
                    {
                        strRefPersonales[2].strRPEtiqueta.Value = gcstrRPSolicitante; //Etiqueta BRP
                        strRefPersonales[2].strRPClaveParticip.Value = "05";
                        strRefPersonales[2].strRPTipoRelacion.Value = "01";
                        strRefPersonales[2].strRPIndicParticip.Value = "01";
                        strRefPersonales[2].strRPPaterno.Value = estProm12.strPaterno.Value.Trim();
                        strRefPersonales[2].strRPMaterno.Value = estProm12.strMaterno.Value.Trim();
                        strRefPersonales[2].strRPNombres.Value = estProm12.strNombre.Value.Trim();
                        string tempRefParam103 = "41";
                        string tempRefParam104 = null;
                        string tempRefParam105 = null;
                        string tempRefParam106 = null;
                        Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam107 = Catalogos.clsCatalogos.enmCamposCatalogos.Atributos;
                        string tempRefParam108 = "D";
                        if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam103, estProm12.strParentesco.Value, tempRefParam104, tempRefParam105, ref tempRefParam106, ref tempRefParam107, ref tempRefParam108))
                        {
                            strRefPersonales[2].strRPCveParent.Value = "00";
                        }
                        else
                        {
                            strRefPersonales[2].strRPCveParent.Value = mdlComunica.OleCatalogos.getLlave1.Substring(mdlComunica.OleCatalogos.getLlave1.Length - Math.Min(mdlComunica.OleCatalogos.getLlave1.Length, 2));
                        }
                        strRefPersonales[2].strRPCalleNum.Value = new String(' ', 35);
                        strRefPersonales[2].strRPColPob.Value = new String(' ', 35);
                        strRefPersonales[2].strRPCodPos.Value = mdlGlobales.funZeroes(5);
                        strRefPersonales[2].strRPDelMuni.Value = new String(' ', 26);
                        strRefPersonales[2].strRPCveEstado.Value = mdlGlobales.funZeroes(2);
                        strRefPersonales[2].strRPLADA.Value = mdlGlobales.funZeroes(3);
                        strRefPersonales[2].strRPTelef.Value = mdlGlobales.funZeroes(7);
                        strRefPersonales[2].strRPFecNac.Value = mdlTranMasivo.funValidaFechaNacimiento(estProm12.strFecNacimiento.Value);
                        strRefPersonales[2].strRPCveSexo.Value = estProm12.strSexo.Value;
                        if (estProm12.strFirmaAdic.Value == "S")
                        {
                            strRefPersonales[2].strRPFirma.Value = "1";
                        }
                        else
                        {
                            strRefPersonales[2].strRPFirma.Value = "0";
                        }
                    }
                    else
                    {
                        strRefPersonales[2].strRPEtiqueta.Value = "";
                    }

                    //Comprobantes (Bloque 5)
                    if (estProm15.strCompDomicilio.Value.Trim().Length > 0 || estProm15.strCompIdentificacion.Value.Trim().Length > 0 || estProm15.strCompIngresos.Value.Trim().Length > 0)
                    {
                        IntContComprobantes = 0;
                        strHdrComprobantes.strCOEtiqueta.Value = gcstrCOSolicitante; //Etiqueta BCO
                        strHdrComprobantes.strCOClaveParticip.Value = "01";
                        strHdrComprobantes.strCOTipoRelacion.Value = "01";
                        strHdrComprobantes.strCOIndicParticip.Value = "01";
                        strComprobantes = ArraysHelper.InitializeArray<udfComprobantes>(1);
                        //ASG Comenzar el llenado de las repeticiones de comprobantes
                        if (estProm15.strCompIngresos.Value == "S")
                        {
                            IntContComprobantes++;
                            strComprobantes = ArraysHelper.RedimPreserve<udfComprobantes[]>(strComprobantes, new int[] { IntContComprobantes + 1 });
                            strComprobantes[IntContComprobantes].strCOTipoComprob.Value = "16";
                            strComprobantes[IntContComprobantes].strCOTipoDocto.Value = "00";
                            strComprobantes[IntContComprobantes].strCONumDocto.Value = new String(' ', 20);
                        }
                        if (estProm15.strCompDomicilio.Value == "S")
                        {
                            IntContComprobantes++;
                            strComprobantes = ArraysHelper.RedimPreserve<udfComprobantes[]>(strComprobantes, new int[] { IntContComprobantes + 1 });
                            strComprobantes[IntContComprobantes].strCOTipoComprob.Value = "02";
                            strComprobantes[IntContComprobantes].strCOTipoDocto.Value = "00";
                            strComprobantes[IntContComprobantes].strCONumDocto.Value = new String(' ', 20);
                        }
                        if (estProm15.strCompIdentificacion.Value == "S")
                        {
                            IntContComprobantes++;
                            strComprobantes = ArraysHelper.RedimPreserve<udfComprobantes[]>(strComprobantes, new int[] { IntContComprobantes + 1 });
                            strComprobantes[IntContComprobantes].strCOTipoComprob.Value = "01";
                            strComprobantes[IntContComprobantes].strCOTipoDocto.Value = estProm15.strIdentCatalogo.Value;
                            strComprobantes[IntContComprobantes].strCONumDocto.Value = estProm15.strDescIdentificacion.Value;
                        }
                        //ASG Llenar la variable de ocurrencias
                        strHdrComprobantes.strCONumComprob.Value = mdlGlobales.funPoneCeros(IntContComprobantes.ToString(), 2);
                    }
                    else
                    {
                        strHdrComprobantes.strCOEtiqueta.Value = "";
                    }

                    if (Conversion.Val(estProm10.strTipoPropiedad.Value.Trim()) > 0)
                    { //Solo se arma cuando existe el tipo de propiedad
                        IntContPropiedades = 0; //Datos Propiedades (Bloque 7)
                        strHdrPropiedades.strPREtiqueta.Value = gcstrPRSolicitante; //BPR
                        strHdrPropiedades.strPRClaveParticip.Value = "01";
                        strHdrPropiedades.strPRTipoRelacion.Value = "01";
                        strHdrPropiedades.strPRIndicParticip.Value = "01";
                        strPropiedades = ArraysHelper.InitializeArray<udfPropiedades>(1);

                        //Llenar las ocurrencias
                        IntContPropiedades++;
                        strPropiedades = ArraysHelper.RedimPreserve<udfPropiedades[]>(strPropiedades, new int[] { IntContPropiedades + 1 });
                        strPropiedades[IntContPropiedades].strPRTipoBien.Value = estProm10.strTipoPropiedad.Value;
                        strPropiedades[IntContPropiedades].strPRSitPagoBien.Value = mdlGlobales.funZeroes(2);
                        strPropiedades[IntContPropiedades].strPRFechaCompra.Value = mdlGlobales.funZeroes(8);
                        strPropiedades[IntContPropiedades].strPRAntiguedad.Value = mdlGlobales.funPoneCeros(estProm10.strAño2.Value, 3);
                        strPropiedades[IntContPropiedades].strPRCveMarca.Value = estProm10.strMarca1.Value;
                        string tempRefParam110 = "12";
                        string tempRefParam111 = null;
                        string tempRefParam112 = null;
                        string tempRefParam113 = null;
                        Catalogos.clsCatalogos.enmCamposCatalogos tempRefParam114 = Catalogos.clsCatalogos.enmCamposCatalogos.Default;
                        string tempRefParam115 = "D";
                        if (!mdlComunica.OleCatalogos.BuscaCatalogo(ref tempRefParam110, estProm10.strTipoPropiedad.Value, tempRefParam111, tempRefParam112, ref tempRefParam113, ref tempRefParam114, ref tempRefParam115))
                        {
                            strPropiedades[IntContPropiedades].strPRDescripcion.Value = new String(' ', 50);
                        }
                        else
                        {
                            string tempRefParam109 = mdlComunica.OleCatalogos.getDescripcion;
                            strPropiedades[IntContPropiedades].strPRDescripcion.Value = mdlGlobales.funPonEspacios(ref tempRefParam109, 50);
                        }
                        strHdrPropiedades.strPRNumPropied.Value = mdlGlobales.funPoneCeros(IntContPropiedades.ToString(), 2);
                    }
                    else
                    {
                        strHdrPropiedades.strPREtiqueta.Value = "";
                    }

                    //Datos del crédito solicitado (Bloque 8)
                    if (Conversion.Val(estProm19.strPlazo.Value) > 0 || Conversion.Val(estProm19.strNumCuenta.Value) > 0)
                    {
                        strTipoTramite = frmProcMasivo.DefInstance.cboTipoTram.Text;
                        if (strTipoTramite.StartsWith("15"))
                        {
                            strNumCuenta = estProm19.strNumCuenta.Value;
                            if (strNumCuenta.Trim().Length == 16)
                            {
                                if (Strings.Mid(strNumCuenta, 1, 6) != "854859" && Strings.Mid(strNumCuenta, 1, 6) != "854809" && Strings.Mid(strNumCuenta, 1, 6) != "517712")
                                {
                                    //* *** Inicio cambios efectuados por DTH para el proyecto 24677
                                    //* 2006-Mar-27, se agregan las siguientes lineas a partir del "Else" hasta antes del "End If"
                                    //* Para ADELA PERFIL NOMINA, cuando la sucursal de la cuenta de cheques sea igual a 4 posiciones
                                    //strNumCuenta = funZeroes(16)  ' DTH esto es porque si no es una cuenta de pagomatico valida que sea una cuenta de cheques.
                                    intCont = strNumCuenta.Trim().Length;
                                    if (intCont > 12)
                                    {
                                        strSucCuentaCHQ = ("000" + Strings.Mid(strNumCuenta, 1, intCont - 12)).Substring(("000" + Strings.Mid(strNumCuenta, 1, intCont - 12)).Length - Math.Min(("000" + Strings.Mid(strNumCuenta, 1, intCont - 12)).Length, 4)) + strNumCuenta.Substring(strNumCuenta.Length - Math.Min(strNumCuenta.Length, 7)); //Genera formato suc(4) y cheques(7)
                                        if (~mdlDigver.DigVer_Valida_DigVer_cta(2, strSucCuentaCHQ) != 0)
                                        { // el parametro 2 es para validar cuentas de cheques
                                            strNumCuenta = mdlGlobales.funZeroes(16);
                                        }
                                    }
                                    else
                                    {
                                        strNumCuenta = mdlGlobales.funZeroes(16);
                                    }
                                }
                                if (strNumCuenta != "0000000000000000" && strNumCuenta != "8888888888888888")
                                {
                                    if (mdlDigver.DigVer_Valida_DigVer_cta(1, strNumCuenta) == (0))
                                    {
                                        //* *** Inicio cambios efectuados por DTH para el proyecto 24677
                                        //* 2006-Mar-16, se agregan las siguientes lineas a partir del "Else" hasta antes del "End If"
                                        //* Para ADELA PERFIL NOMINA, cuando la sucursal de la cuenta de cheques sea igual a 4 posiciones
                                        //strNumCuenta = ""   ' DTH esto es porque si no es una cuenta de pagomatico valida que sea una cuenta de cheques.
                                        intCont = strNumCuenta.Trim().Length;
                                        if (intCont > 12)
                                        {
                                            strSucCuentaCHQ = ("000" + Strings.Mid(strNumCuenta, 1, intCont - 12)).Substring(("000" + Strings.Mid(strNumCuenta, 1, intCont - 12)).Length - Math.Min(("000" + Strings.Mid(strNumCuenta, 1, intCont - 12)).Length, 4)) + strNumCuenta.Substring(strNumCuenta.Length - Math.Min(strNumCuenta.Length, 7)); //Genera formato suc(4) y cheques(7)
                                            if (~mdlDigver.DigVer_Valida_DigVer_cta(2, strSucCuentaCHQ) != 0)
                                            { // el parametro 2 es para validar cuentas de cheques
                                                strNumCuenta = "";
                                            }
                                        }
                                        else
                                        {
                                            strNumCuenta = "";
                                        }
                                        //* *** Fin de cambios efectuados por DTH para el proyecto 24677
                                    }
                                }
                                else
                                {
                                    strNumCuenta = "";
                                }
                                //* *** Inicio cambios efectuados por DTH para el proyecto 24677
                                //* 2006-Mar-16, se agregan las siguientes lineas a partir del "Else" hasta el "End If"
                                //* Para ADELA PERFIL NOMINA, cuando la sucursal de la cuenta de cheques sea menor a 4 posiciones
                            }
                            else
                            {
                                intCont = strNumCuenta.Trim().Length;
                                if (intCont > 12)
                                {
                                    strSucCuentaCHQ = ("000" + Strings.Mid(strNumCuenta, 1, intCont - 12)).Substring(("000" + Strings.Mid(strNumCuenta, 1, intCont - 12)).Length - Math.Min(("000" + Strings.Mid(strNumCuenta, 1, intCont - 12)).Length, 4)) + strNumCuenta.Substring(strNumCuenta.Length - Math.Min(strNumCuenta.Length, 7)); //Genera formato suc(4) y cheques(7)
                                    if (~mdlDigver.DigVer_Valida_DigVer_cta(2, strNumCuenta) != 0)
                                    {
                                        strNumCuenta = "";
                                    }
                                }
                                else
                                {
                                    strNumCuenta = "";
                                }
                            }
                            //* *** Fin de cambios efectuados por DTH para el proyecto 24677
                            //* *** el siguiente código estaba dentro del "If Len(Trim(strNumCuenta)) = 16 Then" y quedo fuera DTH
                            //MODIF MAP 2014/03/18 
                            //lngFactor = 1;
                            mdlDigver.cargamulti();
                            lngVariable = Double.Parse(estProm01.strLimiteCredito.Value);
                            strCSolicitado.strCSEtiqueta.Value = gcstrCSSolicitante; //Etiqueta BCS
                            strCSolicitado.strCSFamiliaProd.Value = frmProcMasivo.DefInstance.txtFamiliaProducto.Text;
                            strCSolicitado.strCSTipoSolicitud.Value = frmProcMasivo.DefInstance.txtTipoSolicitud.Text;
                            strCSolicitado.strCSImportancia.Value = "01";
                            strCSolicitado.strCSDestino.Value = mdlGlobales.funZeroes(4);
                            strCSolicitado.strCSLineaCredito.Value = StringsHelper.Format(lngVariable, "0000000000");
                            if (frmProcMasivo.DefInstance.cboTipoTram.Text.StartsWith("15"))
                            {
                                strCSolicitado.strCSMontoSolic.Value = strCSolicitado.strCSLineaCredito.Value;
                            }
                            else
                            {
                                strCSolicitado.strCSMontoSolic.Value = mdlGlobales.funZeroes(10);
                            }
                            strCSolicitado.strCSTipoBien.Value = mdlGlobales.funZeroes(2);
                            if (Conversion.Val(estProm19.strPlazo.Value) > 0)
                            {
                                strCSolicitado.strCSPlazo.Value = mdlGlobales.funPoneCeros(estProm19.strPlazo.Value.Substring(estProm19.strPlazo.Value.Length - Math.Min(estProm19.strPlazo.Value.Length, 3)), 3);
                            }
                            else
                            {
                                strCSolicitado.strCSPlazo.Value = "000";
                            }

                            mdlGlobales.gstrFamilia.Value = Strings.Mid(frmProcMasivo.DefInstance.txtFamiliaProducto.Text, 1, 2);
                            mdlGlobales.gstrTipoSol.Value = Strings.Mid(frmProcMasivo.DefInstance.txtTipoSolicitud.Text, 1, 2);
                            strPlazo = strCSolicitado.strCSPlazo.Value;
                            strCSolicitado.strCStTasa.Value = mdlGlobales.funZeroes(4);
                            if (Conversion.Val(strPlazo) != 0)
                            {
                                string tempRefParam116 = "60";
                                string tempRefParam117 = "E";
                                mdlComunica.OleCatalogos.AbreCatalogo(ref tempRefParam116, ref tempRefParam117, mdlGlobales.gstrTramite.Value, mdlGlobales.gstrFamilia.Value, mdlGlobales.gstrTipoSol.Value);

                                while (!mdlComunica.OleCatalogos.EOF_Renamed())
                                {
                                    if (mdlComunica.OleCatalogos.getLlave4.Substring(mdlComunica.OleCatalogos.getLlave4.Length - Math.Min(mdlComunica.OleCatalogos.getLlave4.Length, 2)) == strPlazo.Substring(strPlazo.Length - Math.Min(strPlazo.Length, 2)))
                                    {
                                        strCSolicitado.strCStTasa.Value = mdlComunica.OleCatalogos.getAtributos.Substring(0, Math.Min(mdlComunica.OleCatalogos.getAtributos.Length, 2)) + "00";
                                        break;
                                    }
                                    mdlComunica.OleCatalogos.MoveNext();
                                };
                                mdlComunica.OleCatalogos.CierraCatalogo();
                            }
                            strCSolicitado.strCSOrigenGtia.Value = mdlGlobales.funZeroes(2); //MMS 04/06 Se agrega el campo Origen de Garantia
                            //* *** Inicio cambios efectuados por DTH para el proyecto 24677
                            //* 2006-Mar-16, al siguiente "End If" se comento
                            //End If  'se subio este "End If" antes del With
                            //* *** Fin de cambios efectuados por DTH para el proyecto 24677
                        }
                        else
                        {
                            strCSolicitado.strCSEtiqueta.Value = gcstrCSSolicitante; //Etiqueta &BCS
                            strCSolicitado.strCSFamiliaProd.Value = frmProcMasivo.DefInstance.txtFamiliaProducto.Text;
                            strCSolicitado.strCSTipoSolicitud.Value = frmProcMasivo.DefInstance.txtTipoSolicitud.Text;
                            strCSolicitado.strCSImportancia.Value = "01";
                            strCSolicitado.strCSDestino.Value = mdlGlobales.funZeroes(4);
                            if (frmProcMasivo.DefInstance.cboTipoTram.Text.StartsWith("15"))
                            {
                                strCSolicitado.strCSMontoSolic.Value = mdlGlobales.funPoneCeros(estProm01.strLimiteCredito.Value, 10);
                            }
                            else
                            {
                                strCSolicitado.strCSMontoSolic.Value = mdlGlobales.funZeroes(10);
                            }
                            if (Conversion.Val(estProm01.strLimiteCredito.Value) > 0)
                            {
                                strCSolicitado.strCSLineaCredito.Value = mdlGlobales.funPoneCeros(estProm01.strLimiteCredito.Value, 10);
                            }
                            else
                            {
                                strCSolicitado.strCSLineaCredito.Value = "0000000000";
                            }
                            strCSolicitado.strCSTipoBien.Value = mdlGlobales.funZeroes(2);
                            if (Conversion.Val(estProm19.strPlazo.Value) > 0)
                            {
                                strCSolicitado.strCSPlazo.Value = mdlGlobales.funPoneCeros(estProm19.strPlazo.Value.Substring(estProm19.strPlazo.Value.Length - Math.Min(estProm19.strPlazo.Value.Length, 3)), 3);
                            }
                            else
                            {
                                strCSolicitado.strCSPlazo.Value = "000";
                            }
                            strCSolicitado.strCStTasa.Value = mdlGlobales.funZeroes(4);
                            strCSolicitado.strCSOrigenGtia.Value = mdlGlobales.funZeroes(2); //MMS 04/06 Se agrega el campo Origen de Garantia
                        }
                    }
                    else
                    {
                        strCSolicitado.strCSEtiqueta.Value = "";
                    }
                    intOcurrenciasReg19 = -1;
                    if (mdlGlobales.gbolAplicaPromo)
                    { //Indicadores Adicionales
                        strIndAdicionales.strIAEtiqueta.Value = gcstrIASolicitante; //Etiqueta BIA
                        strIndAdicionales.strIAFamiliaProd.Value = mdlCatalogos.gstrCatFamilia;
                        strIndAdicionales.strIATipoSolicitud.Value = mdlCatalogos.gstrTipSol;
                        if (estProm19.strCvePromocion.Value.Trim().Length > 0)
                        {
                            intOcurrenciasReg19++;
                            strIndAdicionales.strIAIndicador = ArraysHelper.RedimPreserve<udfArregloIndicadores[]>(strIndAdicionales.strIAIndicador, new int[] { intOcurrenciasReg19 + 1 });
                            strIndAdicionales.strIAIndicador[intOcurrenciasReg19].strCveIndicador.Value = "012";
                            strIndAdicionales.strIAIndicador[intOcurrenciasReg19].strValorIndicador.Value = mdlGlobales.funPoneCeros(Conversion.Val(estProm19.strCvePromocion.Value).ToString(), 10);
                        }
                        if (estProm19.strTipoComision.Value.Trim().Length > 0)
                        {
                            intOcurrenciasReg19++;
                            strIndAdicionales.strIAIndicador = ArraysHelper.RedimPreserve<udfArregloIndicadores[]>(strIndAdicionales.strIAIndicador, new int[] { intOcurrenciasReg19 + 1 });
                            strIndAdicionales.strIAIndicador[intOcurrenciasReg19].strCveIndicador.Value = "013";
                            strIndAdicionales.strIAIndicador[intOcurrenciasReg19].strValorIndicador.Value = mdlGlobales.funPoneCeros(Conversion.Val(estProm19.strTipoComision.Value).ToString(), 10);
                        }
                        strIndAdicionales.strIAOcurrencias.Value = mdlGlobales.funPoneCeros((intOcurrenciasReg19 + 1).ToString(), 3);
                    }
                    else
                    {
                        strIndAdicionales.strIAEtiqueta.Value = "";
                    }
                    if (mdlCatalogos.gstrCatProceso == "DF")
                    {
                        estHeader5595.strHDCveRechazo.Value = "0000";
                        foreach (mdlTranMasivo.udtMasivo estMasivo_item in mdlTranMasivo.estMasivo)
                        {
                            if (estProm01.strFolio.Value == estMasivo_item.strFolioPreimpreso)
                            {
                                if (estMasivo_item.strCausaDeclinacion != "")
                                {
                                    estHeader5595.strHDCveRechazo.Value = estMasivo_item.strCausaDeclinacion.Substring(0, Math.Min(estMasivo_item.strCausaDeclinacion.Length, 4));
                                    break;
                                }
                            }
                        }
                        if (estHeader5595.strHDCveRechazo.Value != "0000")
                        {
                            estHeader5595.strHDEstatus.Value = "005"; //MMS 11/05 Incremento en la longitud del campo Estatus ('05' a '005')
                        }
                        else
                        {
                            estHeader5595.strHDEstatus.Value = "000"; //MMS 11/05 Incremento en la longitud del campo Estatus ('00' a '000')
                        }
                    }
                    else if (mdlCatalogos.gstrCatProceso == "DR")
                    {
                        estHeader5595.strHDCveRechazo.Value = mdlGlobales.gstrClaveDeclina;
                        estHeader5595.strHDEstatus.Value = "005"; //MMS 11/05 Incremento en la longitud del campo Estatus ('05' a '005')
                    }
                    else if (mdlCatalogos.gstrCatProceso == "")
                    {
                        estHeader5595.strHDCveRechazo.Value = mdlGlobales.gstrClaveDeclina;
                        estHeader5595.strHDEstatus.Value = "000"; //MMS 11/05 Incremento en la longitud del campo Estatus ('00' a '000')
                    }
                    estHeader5595.strHDCveProceso.Value = "004"; //MMS 11/05 Incremento en la longitud del campo Proceso ('04' a '004')

                    lsCadena = "";
                    lsEnviar = "";
                    lsCadena = lsCadena + funArmaHeader5595(estHeader5595.strHDEstatus.Value, estHeader5595.strHDCveProceso.Value, estHeader5595.strHDCveRechazo.Value, estProm01.strFolio.Value);

                    gvstrFoliosSolicitudes = ArraysHelper.RedimPreserve<string[]>(gvstrFoliosSolicitudes, new int[] { intNumReg + 1 });
                    gvstrDatosPersonales = ArraysHelper.RedimPreserve<string[]>(gvstrDatosPersonales, new int[] { intNumReg + 1 });
                    gvstrDatosSolicitud = ArraysHelper.RedimPreserve<string[]>(gvstrDatosSolicitud, new int[] { intNumReg + 1 });
                    gvstrDatosPersonalesC = ArraysHelper.RedimPreserve<string[]>(gvstrDatosPersonalesC, new int[] { intNumReg + 1 });
                    gvstrDatosPersonalesO = ArraysHelper.RedimPreserve<string[]>(gvstrDatosPersonalesO, new int[] { intNumReg + 1 });
                    gvstrDatosDeEmpleo = ArraysHelper.RedimPreserve<string[]>(gvstrDatosDeEmpleo, new int[] { intNumReg + 1 });
                    gvstrDatosDeEmpleoC = ArraysHelper.RedimPreserve<string[]>(gvstrDatosDeEmpleoC, new int[] { intNumReg + 1 });
                    gvstrDatosDeEmpleoO = ArraysHelper.RedimPreserve<string[]>(gvstrDatosDeEmpleoO, new int[] { intNumReg + 1 });
                    gvstrRefCrediticias = ArraysHelper.RedimPreserve<string[]>(gvstrRefCrediticias, new int[] { intNumReg + 1 });
                    gvstrRefPersonales = ArraysHelper.RedimPreserve<string[]>(gvstrRefPersonales, new int[] { intNumReg + 1 });
                    gvstrComprobantes = ArraysHelper.RedimPreserve<string[]>(gvstrComprobantes, new int[] { intNumReg + 1 });
                    gvstrPropiedades = ArraysHelper.RedimPreserve<string[]>(gvstrPropiedades, new int[] { intNumReg + 1 });
                    gvstrCredSolicitado = ArraysHelper.RedimPreserve<string[]>(gvstrCredSolicitado, new int[] { intNumReg + 1 });
                    gvstrIndAdicionales = ArraysHelper.RedimPreserve<string[]>(gvstrIndAdicionales, new int[] { intNumReg + 1 });
                    gvstrFoliosSolicitudes[intNumReg] = mdlPromotora.funArmaCadena("FOLIOS", 0);

                    gvstrDatosPersonales[intNumReg] = mdlPromotora.funArmaCadena(gcstrDPSolicitante, 0);
                    lsCadena = lsCadena + gvstrDatosPersonales[intNumReg];

                    gvstrDatosSolicitud[intNumReg] = mdlPromotora.funArmaCadena(gcstrDSSolicitante, 0);
                    lsCadena = lsCadena + gvstrDatosSolicitud[intNumReg];

                    gvstrDatosPersonalesC[intNumReg] = mdlPromotora.funArmaCadena(gcstrDPSolicitante, 1);
                    lsCadena = lsCadena + gvstrDatosPersonalesC[intNumReg];

                    gvstrDatosPersonalesO[intNumReg] = mdlPromotora.funArmaCadena(gcstrDPSolicitante, 2);
                    lsCadena = lsCadena + gvstrDatosPersonalesO[intNumReg];

                    gvstrDatosDeEmpleo[intNumReg] = mdlPromotora.funArmaCadena(gcstrDESolicitante, 0);
                    lsCadena = lsCadena + gvstrDatosDeEmpleo[intNumReg];

                    gvstrDatosDeEmpleoO[intNumReg] = mdlPromotora.funArmaCadena(gcstrDESolicitante, 1);
                    lsCadena = lsCadena + gvstrDatosDeEmpleoO[intNumReg];

                    gvstrDatosDeEmpleoC[intNumReg] = mdlPromotora.funArmaCadena(gcstrDESolicitante, 2);
                    lsCadena = lsCadena + gvstrDatosDeEmpleoC[intNumReg];

                    gvstrRefCrediticias[intNumReg] = mdlPromotora.funArmaCadena(gcstrRCSolicitante, 0) + mdlPromotora.funArmaCadena(gcstrRCSolicitante, 1);
                    lsCadena = lsCadena + gvstrRefCrediticias[intNumReg];

                    gvstrRefPersonales[intNumReg] = mdlPromotora.funArmaCadena(gcstrRPSolicitante, 0) + mdlPromotora.funArmaCadena(gcstrRPSolicitante, 1) + mdlPromotora.funArmaCadena(gcstrRPSolicitante, 2);
                    lsCadena = lsCadena + gvstrRefPersonales[intNumReg];

                    gvstrComprobantes[intNumReg] = mdlPromotora.funArmaCadena(gcstrCOSolicitante, 0);
                    lsCadena = lsCadena + gvstrComprobantes[intNumReg];

                    gvstrPropiedades[intNumReg] = mdlPromotora.funArmaCadena(gcstrPRSolicitante, 0);
                    lsCadena = lsCadena + gvstrPropiedades[intNumReg];

                    gvstrCredSolicitado[intNumReg] = mdlPromotora.funArmaCadena(gcstrCSSolicitante, 0);
                    lsCadena = lsCadena + gvstrCredSolicitado[intNumReg];

                    gvstrIndAdicionales[intNumReg] = mdlPromotora.funArmaCadena(gcstrIASolicitante, 0);
                    lsCadena = lsCadena + gvstrIndAdicionales[intNumReg];

                    //MODIF MAP ART.115 2016 
                    //gvstrIndArt115[intNumReg] = mdlPromotora.funArmaCadena(gcstrDatosArt115, 0);
                    //lsCadena = lsCadena + gvstrIndArt115[intNumReg];

                    intNumReg++;
                    lsCadena = StringsHelper.Format(intNumReg, "000000") + lsCadena + gcstrFinDialogo; //Etiqueta &&&&

                    LongitudCadena = gcIntLongitudMaxima - lsCadena.Length + gcIntLongitudConsecutivo; //Contar las posiciones del consecutivo
                    lsCadena = lsCadena + new String(' ', LongitudCadena);
                    FileSystem.PrintLine(liArchivoSalida, lsCadena);

                    frmProcMasivo.DefInstance.prg_proceso.Value = intNumReg;
                }
                mdlGlobales.subDespMensajes("");
                FileSystem.FileClose(liArchivoSalida);
                frmProcMasivo.DefInstance.prg_proceso.Value = 0;

                if (mdlGlobales.gblnEnvioTansac)
                {
                    mdlGlobales.subDespMensajes("ENVIA EL ARCHIVO TRANSACCIONALMENTE");
                    if (funEnviaTransaccionalmente())
                    {
                        mdlGlobales.subDespMensajes("ENVIO TRANSACCIONALMENTE");
                    }
                    else
                    {
                        string tempRefParam118 = "OCURRIO ERROR EN EL ENVIO TRANSACCIONALMENTE";
                        MsgBoxStyle tempRefParam119 = (MsgBoxStyle)(((int)MsgBoxStyle.Exclamation) + ((int)MsgBoxStyle.OkOnly));
                        mdlGlobales.subDespErrores(ref tempRefParam118, ref tempRefParam119);
                    }
                }
                else
                {
                    mdlGlobales.subDespMensajes("ENVIA EL ARCHIVO A INTELAR");
                    //ANTES, POR FTP DIRECTO
                    //Call subEnviaArchivoFTP(strArchivoEnviar)

                    //(JB-SAS 22/nov/2006) NUEVO, UTILIZANDO ICEP

                    //MIG WXP INI
                    //if (!mdlICEPIntelar.blnICEP_ProcesaEnvioREMESA(strArchivoEnviar))
                    //{
                    //    //(JB-SAS 23/nov/2006) NUEVO, PROCEDIMIENTO EN CASO DE CONTINGENCIA
                    //    mdlFTPIntelar.subEnviaArchivoFTP(ref strArchivoEnviar);
                    //}
                    //MIG WXP INI
                    
                    /************************************************************************************
                     * 
                     *  SE CANCELA ENVIO POR ICEP Y FTP - INFOWARE 02 JUNIO 2010
                     * 
                     * ***********************************************************************************/


                    //if (!mdlICEPIntelar.blnCLC_ProcesaEnvioREMESA(strArchivoEnviar))
                    //{
                    //    if (!mdlICEPIntelar.blnICEP_ProcesaEnvioREMESA(strArchivoEnviar))
                    //    {
                    //        mdlFTPIntelar.subEnviaArchivoFTP(ref strArchivoEnviar);
                    //        {
                    //            MessageBox.Show("REINTENTE LA OPERACION ENVIO CLC, ICEP Y FTP NO EXITOSO", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //            Application.DoEvents();
                    //        }
                    //    }
                    //}

                    //DCL Se incluye tectia en el modo de envio
                    if (!mdlICEPIntelar.blnCLC_ProcesaEnvioREMESA(strArchivoEnviar))
                    {                        
                        MessageBox.Show("REINTENTE LA OPERACION ENVIO CLC Y/O TECTIA NO EXITOSO", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        Application.DoEvents();
                    }
                    /************************************************************************************
                     * 
                     *  SE CANCELA ENVIO POR ICEP Y FTP - INFOWARE 02 JUNIO 2010 SE DEJA SOLO ENVIO CLC
                     * 
                     * ***********************************************************************************/

                }
                mdlGlobales.gblnEnvioTansac = false;
                mdlGlobales.subDespMensajes("");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception excep)
            {
                // Se agrego esta linea para cerrar el archivo ya que si tiene un problema el archivo se quedaba 
                // abierto
                FileSystem.FileClose(liArchivoSalida);
                //ErrorEventHandler errLinea = new ErrorEventHandler();                                
                //throw new Exception("Migration Exception: 'Resume Next' not supported");
                //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                string tempRefParam120 = "ERROR AL INTENTAR ENVIAR LOS BLOQUES A TANDEM; " + Information.Err().Number.ToString() + ": " + excep.Message +"\n" +
                    excep.Source + " - " + excep.TargetSite;
                MsgBoxStyle tempRefParam121 = (MsgBoxStyle)(((int)MsgBoxStyle.Critical) + ((int)MsgBoxStyle.OkOnly));
                mdlGlobales.subDespErrores(ref tempRefParam120, ref tempRefParam121);

                //INI Modif JGC 2010/12/03
                FileSystem.FileClose(liArchivoSalida);
                tempRefParam120 = "EXISTE UN ERROR CON EL ARCHIVO ORIGINAL DE SU REMESA" + Strings.Chr(13).ToString() + "Consecutivo de Folio: " + intNumReg + 1 + Strings.Chr(13).ToString() + "Registro que no puede ser procesado: " + lsCadena;
                mdlGlobales.subDespErrores(ref tempRefParam120, ref tempRefParam121);
                //FIN Modif JGC 2010/12/03

            }
        }

        //Incrementa los nodos del arreglo controlador, devolviendo el Total de bloques
        //Si se le manda True al parámetro blnMasInfo cambia a 1 el el falg de información del bloque anterior
        static public void subAgregaElementoAControlador(ref  int intTotBloques, string StrEtiqueta, int IntIndiceOrig, bool blnMasInfo)
        {
            if (blnMasInfo)
            {
                mdlGlobales.strAControlador[intTotBloques].strFlagInform = "1";
            }
            else
            {
            }
            intTotBloques++;
            //UPGRADE_WARNING: (1042) Array strAControlador may need to have individual elements initialized.
            mdlGlobales.strAControlador = ArraysHelper.RedimPreserve<mdlGlobales.udfAControlador[]>(mdlGlobales.strAControlador, new int[] { intTotBloques + 1 });
            mdlGlobales.strAControlador[intTotBloques].strACDatos = "";
            mdlGlobales.strAControlador[intTotBloques].strACEtiqueta.Value = StrEtiqueta;
            mdlGlobales.strAControlador[intTotBloques].strACEstatus.Value = gcIntPorEnviar.ToString();
            mdlGlobales.strAControlador[intTotBloques].strFolioPreimp = gvstrFoliosSolicitudes[IntIndiceOrig];
            mdlGlobales.strAControlador[intTotBloques].strFlagInform = "0";
        }

        //Arma el arreglo controlador
        static public void subGeneraAControladorCAP()
        {
            try
            {
                //Se genera el arreglo controlador con todos sus datos
                //MODIF MAP 2014/03/18 
                //int intBLLongitud = 0;
                //MODIF MAP 2014/03/18 
                //int intContBloques = 0; //Contador de los elementos del bloque
                //MODIF MAP 2014/03/18 
                //int intTotElementos = 0; //Total de elementos por bloque
                int intTotBloques = 0; //Total de bloques por enviar
                string strDatos = String.Empty;

                intTotBloques = -1;
                for (int IntIndice = 0; IntIndice <= gvstrDatosSolicitud.GetUpperBound(0); IntIndice++)
                {
                    subAgregaElementoAControlador(ref intTotBloques, gcstrDPSolicitante, IntIndice, false); //El primer bloque siempre se envía en un nuevo controlador
                    mdlGlobales.strAControlador[intTotBloques].strACDatos = mdlGlobales.strAControlador[intTotBloques].strACDatos + gvstrDatosPersonales[IntIndice];
                    mdlGlobales.strAControlador[intTotBloques].IntACLongitud = mdlGlobales.strAControlador[intTotBloques].strACDatos.Length;
                    //Bloque de datos personales del solicitante
                    if (mdlGlobales.strAControlador[intTotBloques].IntACLongitud + gvstrDatosSolicitud[IntIndice].Length > (gcIntLongitudDatos - 4))
                    {
                        subAgregaElementoAControlador(ref intTotBloques, gcstrDSSolicitante, IntIndice, true); //Generar un nuevo bloque
                    }
                    mdlGlobales.strAControlador[intTotBloques].strACDatos = mdlGlobales.strAControlador[intTotBloques].strACDatos + gvstrDatosSolicitud[IntIndice];
                    mdlGlobales.strAControlador[intTotBloques].IntACLongitud = mdlGlobales.strAControlador[intTotBloques].strACDatos.Length;
                    //Bloque de datos personales del conyuge
                    if (mdlGlobales.strAControlador[intTotBloques].IntACLongitud + gvstrDatosPersonalesC[IntIndice].Length > (gcIntLongitudDatos - 4))
                    {
                        subAgregaElementoAControlador(ref intTotBloques, gcstrDPSolicitante, IntIndice, true); //Generar un nuevo bloque
                    }
                    mdlGlobales.strAControlador[intTotBloques].strACDatos = mdlGlobales.strAControlador[intTotBloques].strACDatos + gvstrDatosPersonalesC[IntIndice];
                    mdlGlobales.strAControlador[intTotBloques].IntACLongitud = mdlGlobales.strAControlador[intTotBloques].strACDatos.Length;
                    //Bloque de datos personales del obligado solidario
                    if (mdlGlobales.strAControlador[intTotBloques].IntACLongitud + gvstrDatosPersonalesO[IntIndice].Length > (gcIntLongitudDatos - 4))
                    {
                        subAgregaElementoAControlador(ref intTotBloques, gcstrDPSolicitante, IntIndice, true); //Generar un nuevo bloque
                    }
                    mdlGlobales.strAControlador[intTotBloques].strACDatos = mdlGlobales.strAControlador[intTotBloques].strACDatos + gvstrDatosPersonalesO[IntIndice];
                    mdlGlobales.strAControlador[intTotBloques].IntACLongitud = mdlGlobales.strAControlador[intTotBloques].strACDatos.Length;
                    //Bloque de Datos del empleo del solicitante
                    if (mdlGlobales.strAControlador[intTotBloques].IntACLongitud + gvstrDatosDeEmpleo[IntIndice].Length > (gcIntLongitudDatos - 4))
                    {
                        subAgregaElementoAControlador(ref intTotBloques, gcstrDESolicitante, IntIndice, true); //Generar un nuevo bloque
                    }
                    mdlGlobales.strAControlador[intTotBloques].strACDatos = mdlGlobales.strAControlador[intTotBloques].strACDatos + gvstrDatosDeEmpleo[IntIndice];
                    mdlGlobales.strAControlador[intTotBloques].IntACLongitud = mdlGlobales.strAControlador[intTotBloques].strACDatos.Length;
                    //Bloque de Datos del empleo del obligado solidario
                    if (mdlGlobales.strAControlador[intTotBloques].IntACLongitud + gvstrDatosDeEmpleoO[IntIndice].Length > (gcIntLongitudDatos - 4))
                    {
                        subAgregaElementoAControlador(ref intTotBloques, gcstrDESolicitante, IntIndice, true); //Generar un nuevo bloque
                    }
                    mdlGlobales.strAControlador[intTotBloques].strACDatos = mdlGlobales.strAControlador[intTotBloques].strACDatos + gvstrDatosDeEmpleoO[IntIndice];
                    mdlGlobales.strAControlador[intTotBloques].IntACLongitud = mdlGlobales.strAControlador[intTotBloques].strACDatos.Length;
                    //Bloque de Datos del empleo del conyuge
                    if (mdlGlobales.strAControlador[intTotBloques].IntACLongitud + gvstrDatosDeEmpleoC[IntIndice].Length > (gcIntLongitudDatos - 4))
                    {
                        subAgregaElementoAControlador(ref intTotBloques, gcstrDESolicitante, IntIndice, true); //Generar un nuevo bloque
                    }
                    mdlGlobales.strAControlador[intTotBloques].strACDatos = mdlGlobales.strAControlador[intTotBloques].strACDatos + gvstrDatosDeEmpleoC[IntIndice];
                    mdlGlobales.strAControlador[intTotBloques].IntACLongitud = mdlGlobales.strAControlador[intTotBloques].strACDatos.Length;
                    //Bloque de Referencias crediticias
                    if (mdlGlobales.strAControlador[intTotBloques].IntACLongitud + gvstrRefCrediticias[IntIndice].Length > (gcIntLongitudDatos - 4))
                    {
                        subAgregaElementoAControlador(ref intTotBloques, gcstrRCSolicitante, IntIndice, true); //Generar un nuevo bloque
                    }
                    mdlGlobales.strAControlador[intTotBloques].strACDatos = mdlGlobales.strAControlador[intTotBloques].strACDatos + gvstrRefCrediticias[IntIndice];
                    mdlGlobales.strAControlador[intTotBloques].IntACLongitud = mdlGlobales.strAControlador[intTotBloques].strACDatos.Length;
                    //Bloque de Referencias personales
                    if (mdlGlobales.strAControlador[intTotBloques].IntACLongitud + gvstrRefPersonales[IntIndice].Length > (gcIntLongitudDatos - 4))
                    {
                        subAgregaElementoAControlador(ref intTotBloques, gcstrRPSolicitante, IntIndice, true); //Generar un nuevo bloque
                    }
                    mdlGlobales.strAControlador[intTotBloques].strACDatos = mdlGlobales.strAControlador[intTotBloques].strACDatos + gvstrRefPersonales[IntIndice];
                    mdlGlobales.strAControlador[intTotBloques].IntACLongitud = mdlGlobales.strAControlador[intTotBloques].strACDatos.Length;
                    //Bloque de comprobantes
                    if (mdlGlobales.strAControlador[intTotBloques].IntACLongitud + gvstrComprobantes[IntIndice].Length > (gcIntLongitudDatos - 4))
                    {
                        subAgregaElementoAControlador(ref intTotBloques, gcstrCOSolicitante, IntIndice, true); //Generar un nuevo bloque
                    }
                    mdlGlobales.strAControlador[intTotBloques].strACDatos = mdlGlobales.strAControlador[intTotBloques].strACDatos + gvstrComprobantes[IntIndice];
                    mdlGlobales.strAControlador[intTotBloques].IntACLongitud = mdlGlobales.strAControlador[intTotBloques].strACDatos.Length;
                    //Bloque de Propiedades
                    if (mdlGlobales.strAControlador[intTotBloques].IntACLongitud + gvstrPropiedades[IntIndice].Length > (gcIntLongitudDatos - 4))
                    {
                        subAgregaElementoAControlador(ref intTotBloques, gcstrPRSolicitante, IntIndice, true); //Generar un nuevo bloque
                    }
                    mdlGlobales.strAControlador[intTotBloques].strACDatos = mdlGlobales.strAControlador[intTotBloques].strACDatos + gvstrPropiedades[IntIndice];
                    mdlGlobales.strAControlador[intTotBloques].IntACLongitud = mdlGlobales.strAControlador[intTotBloques].strACDatos.Length;
                    //Bloque de Crédito solicitado
                    if (mdlGlobales.strAControlador[intTotBloques].IntACLongitud + gvstrCredSolicitado[IntIndice].Length > (gcIntLongitudDatos - 4))
                    {
                        subAgregaElementoAControlador(ref intTotBloques, gcstrCSSolicitante, IntIndice, true); //Generar un nuevo bloque
                    }
                    mdlGlobales.strAControlador[intTotBloques].strACDatos = mdlGlobales.strAControlador[intTotBloques].strACDatos + gvstrCredSolicitado[IntIndice];
                    mdlGlobales.strAControlador[intTotBloques].IntACLongitud = mdlGlobales.strAControlador[intTotBloques].strACDatos.Length;
                    //Bloque de Indicadores Adicionales
                    if (mdlGlobales.strAControlador[intTotBloques].IntACLongitud + gvstrIndAdicionales[IntIndice].Length > (gcIntLongitudDatos - 4))
                    {
                        subAgregaElementoAControlador(ref intTotBloques, gcstrIASolicitante, IntIndice, true); //Generar un nuevo bloque
                    }
                    mdlGlobales.strAControlador[intTotBloques].strACDatos = mdlGlobales.strAControlador[intTotBloques].strACDatos + gvstrIndAdicionales[IntIndice];
                    mdlGlobales.strAControlador[intTotBloques].IntACLongitud = mdlGlobales.strAControlador[intTotBloques].strACDatos.Length;
                }
            }
            catch (Exception excep)
            {

                //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                string tempRefParam = "ERROR AL ARMAR EL ARREGLO CONTROLADOR, " + Information.Err().Number.ToString() + ": " + excep.Message;
                MsgBoxStyle tempRefParam2 = (MsgBoxStyle)(((int)MsgBoxStyle.Critical) + ((int)MsgBoxStyle.OkOnly));
                mdlGlobales.subDespErrores(ref tempRefParam, ref tempRefParam2);
            }
        }


        //función para enviar los folios de la remesa vía transacciones (5595 - 09)
        static public bool funEnviaTransaccionalmente()
        {
            bool result = false;
            int intContador = 0;
            int intTotalEtiquetas = 0; //Total de etiquetas a enviar
            string strDatos = String.Empty;
            int intDialogo = 0;
            string strDescOper = String.Empty; //Descripción de la operación que se está realizando
            string strRecibe = String.Empty;
            string strCveTransaccion = String.Empty;
            string strEstadoBloque = String.Empty;
            int intIntentos = 0;
            int intEnviaBloque = 0;
            string strCadPaso = String.Empty;
            string strDatosHeader = String.Empty;
            int intFreeFile = 0;
            FixedLengthString strRemesaCompleta = new FixedLengthString(22);
            bool blnFaltaBloq = false;
            string strFolInterno = String.Empty;
            string strFolPreimpreso = String.Empty;

            try
            {
                intIntentos = 0; //: gintTransApl = 0
                //Llenar la clave de remesa
                strRemesaCompleta.Value = frmProcMasivo.DefInstance.cboTipoTram.Text.Substring(0, Math.Min(frmProcMasivo.DefInstance.cboTipoTram.Text.Length, 2)) + frmProcMasivo.DefInstance.txtTipoEntidad.Text.Substring(0, Math.Min(frmProcMasivo.DefInstance.txtTipoEntidad.Text.Length, 2)) +
                                          "0001" + frmProcMasivo.DefInstance.txtEntidadOrigen.Text.Substring(0, Math.Min(frmProcMasivo.DefInstance.txtEntidadOrigen.Text.Length, 4)) + frmProcMasivo.DefInstance.txtRemesa.Text.Substring(frmProcMasivo.DefInstance.txtRemesa.Text.Length - Math.Min(frmProcMasivo.DefInstance.txtRemesa.Text.Length, 10));
                //Abrir el archivo de errores para preparar la salida en caso de haber errores
                intFreeFile = FileSystem.FreeFile();
                FileSystem.FileOpen(intFreeFile, mdlGlobales.strPathError + "\\" + "Remesa_Sin_Evaluar.txt", OpenMode.Append, OpenAccess.Default, OpenShare.Default, -1);
                //Hay que validar el ultimo registro
                strDescOper = " DEL ENVIO TRANSACCIONAL... ";
                subGeneraAControladorCAP();
                intTotalEtiquetas = mdlGlobales.strAControlador.GetUpperBound(0);
                intDialogo = 0;
                gblnContinua = 1;
                strDatos = "";
                blnFaltaBloq = false;
                while (gblnContinua == 1)
                {

                    //ARMA CADENA DE DATOS
                    strDatos = "";
                    for (intContador = 0; intContador <= intTotalEtiquetas; intContador++)
                    {
                        //SI NO HA SIDO ENVIADA LA INFORMACION
                        if (Conversion.Val(mdlGlobales.strAControlador[intContador].strACEstatus.Value) == gcIntPorEnviar)
                        {
                            if (strFolPreimpreso != mdlGlobales.funPoneCeros(mdlGlobales.strAControlador[intContador].strFolioPreimp, 16))
                            {
                                strDatosHeader = funArmaHeader5595("000", "004", "000", mdlGlobales.strAControlador[intContador].strFolioPreimp, strRemesaCompleta.Value, ""); //MMS 11/05 Incremento en la longitud del campo (2 a 3) se agregan ceros al valor
                            }
                            else
                            {
                                if (strFolInterno != "00000000")
                                {
                                    strDatosHeader = funArmaHeader5595("000", "004", "000", mdlGlobales.strAControlador[intContador].strFolioPreimp, strRemesaCompleta.Value, strFolInterno); //MMS 11/05 Incremento en la longitud del campo (2 a 3) se agregan ceros al valor
                                }
                            }
                            if (mdlGlobales.strAControlador[intContador].strFlagInform.Trim() == "1")
                            {
                                strDatosHeader = StringsHelper.MidAssignment(strDatosHeader, 120, "1"); //MMS 11/05 Posición del flag de información (118 a 120)
                                blnFaltaBloq = true;
                            }
                            else
                            {
                                strDatosHeader = StringsHelper.MidAssignment(strDatosHeader, 120, "0"); //MMS 11/05 Posición del flag de información (118 a 120)
                                blnFaltaBloq = false;
                            }
                            strDatos = strDatosHeader + mdlGlobales.strAControlador[intContador].strACDatos;
                            mdlGlobales.strAControlador[intContador].strACEstatus.Value = gcIntEnviandose.ToString();
                            intEnviaBloque = intContador;
                            break;
                        }
                        intDialogo = strDatos.Length;
                    }
                    intDialogo++;
                    //VERIFICA SI QUEDAN MÁS DATOS POR ENVIAR
                    intContador = 0;
                    gblnContinua = 0;
                    for (intContador = 0; intContador <= intTotalEtiquetas; intContador++)
                    {
                        if (Double.Parse(mdlGlobales.strAControlador[intContador].strACEstatus.Value) == gcIntPorEnviar)
                        {
                            //SI QUEDA AL MENOS UNO
                            gblnContinua = 1;
                            break;
                        }
                    }
                    if (gblnContinua == 1)
                    {
                        //SE INICIALIZA BANDERA PARA CONTROL DE DIALOGOS PARA CONTINUAR
                        mdlComunica.strQueTramite = "ENVIANDO PARTE " + Conversion.Str(intDialogo) + strDescOper;
                    }
                    else
                    {
                        //SE INICIALIZA BANDERA PARA CONTROL DE DIALOGOS A QUE YA TERMINO
                        mdlComunica.strQueTramite = "ENVIANDO ULTIMA PARTE" + strDescOper;
                    }

                    strDatos = strDatos + gcstrFinDialogo;
                    mdlGlobales.subDespMsg(ref mdlComunica.strQueTramite);
                    //EMPIEZA A ENVIAR INFO.
                    Application.DoEvents();

                    strCveTransaccion = Strings.Mid(strDatos, 47, 2);
                    intIntentos = 0;
                    strRecibe = "";

                    while (Strings.Mid(strRecibe, 1, 4) != estHeader5595.strHDCveTran.Value && (strRecibe.IndexOf("SEG;") + 1) < 1 && intIntentos < 4)
                    {

                        mdlComunica.gvMensaje = "ENVIO DE TRANSACCION DE PROCESAMIENTO MASIVO " + DateTime.Today.ToString("yyyy/MM/dd") + StringsHelper.Format(DateTime.Today, "HH:mm:ss AMPM");
                        mdlGlobales.subRegBitacora("E");
                        string tempRefParam = "POR FAVOR ESPERE RESPUESTA DE TANDEM - INTENTO No. : " + Conversion.Str(intIntentos);
                        mdlGlobales.subDespMsg(ref tempRefParam);
                        mdlComunica.gvMensaje = strDatos;
                        mdlGlobales.subRegBitacora("E");
                        strRecibe = mdlComunica.funCON(strDatos);
                        //AQUI SE VERIFICA LA RESPUESTA DEL SERVER
                        if (Strings.Mid(strRecibe, 50, 50).Trim() == "")
                        {
                            string tempRefParam2 = Strings.Mid(strRecibe, 1, 60);
                            mdlGlobales.subDespMsg(ref tempRefParam2);
                        }
                        else
                        {
                            string tempRefParam3 = Strings.Mid(strRecibe, 51, 50);
                            mdlGlobales.subDespMsg(ref tempRefParam3); //MMS 11/05 Incremento en la longitud del campo (2 a 3)
                        }

                        intIntentos++;
                    }
                    //VALIDAR LA CORRESPONDENCIA DEL DATO
                    if (Strings.Mid(strRecibe, 1, 4) != estHeader5595.strHDCveTran.Value)
                    {
                        if (Strings.Mid(strRecibe, 51, 50).Trim() == "")
                        { //MMS 11/05 Incremento en la longitud del campo (2 a 3)
                            string tempRefParam4 = "RESPUESTA ERRONEA DE TANDEM O SE ACABO EL TIEMPO DE ESPERA. POR FAVOR REINTENTE.";
                            mdlGlobales.subDespMsg(ref tempRefParam4);
                        }
                        else
                        {
                            string tempRefParam5 = Strings.Mid(strRecibe, 51, 50);
                            mdlGlobales.subDespMsg(ref tempRefParam5); //MMS 11/05 Incremento en la longitud del campo (2 a 3)
                        }
                    }

                    double dbNumericTemp = 0;
                    if (Strings.Mid(strRecibe, 1, 4) == estHeader5595.strHDCveTran.Value && Double.TryParse(Strings.Mid(strRecibe, 8, 16).Trim(), NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out dbNumericTemp) && Conversion.Val(Strings.Mid(strRecibe, 24, 8)) > 0 && Strings.Mid(strRecibe, 51, 2).Trim() == "00")
                    {

                        //Guarda folio interno obtenido en el 1er envío
                        subPoneHeader(Strings.Mid(strRecibe, 1, 176)); // 176 = Longitud del Header
                        estHeader5595.strHDFolInterno.Value = Strings.Mid(strRecibe, 24, 8);
                        result = true;

                        strFolPreimpreso = estHeader5595.strHDFolPreimpreso.Value;
                        strFolInterno = estHeader5595.strHDFolInterno.Value;

                        if (!blnFaltaBloq)
                        {
                            //VENTA EN CORTO TRANSAC. DE ENCOLAMIENTO OCT/2002 OML
                            strDatos = mdlTranConsulta.funArmaHeader542099(Strings.Mid(strRecibe, 24, 8), Strings.Mid(strRecibe, 133, 3), Strings.Mid(strRecibe, 136, 3)); //MMS 11/05 Incremento en la longitud de los campos Estatus, Mapa, Proceso
                            mdlComunica.gvMensaje = "ENVIO DE TRANSACCION DE: ENCOLAMIENTO " + mdlCatalogos.gstrTipoOp.Value + mdlComunica.strQueTramite + DateTime.Today.ToString("yyyy/MM/dd") + StringsHelper.Format(DateTime.Today, "HH:mm:ss AMPM");
                            mdlGlobales.subRegBitacora("E");
                            string tempRefParam6 = "POR FAVOR ESPERE RESPUESTA DE TANDEM - INTENTO No. : " + Conversion.Str(intIntentos);
                            mdlGlobales.subDespMsg(ref tempRefParam6);
                            mdlComunica.gvMensaje = strDatos;
                            mdlGlobales.subRegBitacora("E");
                            strRecibe = mdlComunica.funCON(strDatos);
                            //VALIDAR LA CORRESPONDENCIA DEL DATO
                            if (Strings.Mid(strRecibe, 175, 8) != mdlGlobales.strTranEncol.strTEFolio.Value)
                            {
                                if (Strings.Mid(strRecibe, 183, 2).Trim() == "")
                                {
                                    string tempRefParam7 = "RESPUESTA ERRONEA DE TANDEM O SE ACABO EL TIEMPO DE ESPERA. POR FAVOR REINTENTE.";
                                    mdlGlobales.subDespMsg(ref tempRefParam7);
                                    result = false;
                                }
                            }
                            if (Strings.Mid(strRecibe, 175, 8) == mdlGlobales.strTranEncol.strTEFolio.Value && Strings.Mid(strRecibe, 183, 2).Trim() == "00")
                            {
                                string tempRefParam8 = "LA SOLICITUD INICIO SU EVALUACION";
                                mdlGlobales.subDespMsg(ref tempRefParam8);
                            }
                            else
                            {
                                FileSystem.PrintLine(intFreeFile, mdlGlobales.funPoneCeros(mdlGlobales.strAControlador[intEnviaBloque].strFolioPreimp, 16) + " - " + mdlGlobales.funPoneCeros(Conversion.Val(estHeader5595.strHDFolInterno.Value).ToString(), 8) + " - LA SOLICITUD QUEDO REGISTRADA, PERO NO PUDO INICIAR SU EVALUACION");
                                result = false;
                            }
                            //SE CAMBIA ESTATUS A "ENVIADO"
                            intContador = 0;
                            if (Math.Floor(Double.Parse(mdlGlobales.strAControlador[intEnviaBloque].strACEstatus.Value)) == gcIntEnviandose)
                            {
                                mdlGlobales.strAControlador[intEnviaBloque].strACEstatus.Value = gcIntEnviado.ToString();
                            }
                        }
                    }
                    else
                    {
                        FileSystem.PrintLine(intFreeFile, mdlGlobales.funPoneCeros(mdlGlobales.strAControlador[intEnviaBloque].strFolioPreimp, 16) + " ERROR: LA SOLICITUD NO SE APLICO COMPLETA: " + Strings.Mid(strRecibe, 52, 50));
                        result = false;
                    }
                }
                FileSystem.FileClose(intFreeFile);
                //SI TODO SALIÓ BIEN SE CAMBIA EL ESTATUS A 04
                if (!mdlTranMasivo.funCambiaEstatusRemesa(frmProcMasivo.DefInstance.txtRemesa.Text.Substring(frmProcMasivo.DefInstance.txtRemesa.Text.Length - Math.Min(frmProcMasivo.DefInstance.txtRemesa.Text.Length, 10)), frmProcMasivo.DefInstance.txtArchivo.Text, "04"))
                {
                    //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                    string tempRefParam9 = "EL ESTATUS DE LA REMESA NO FUE ACTUALIZADO";
                    MsgBoxStyle tempRefParam10 = (MsgBoxStyle)(((int)MsgBoxStyle.Exclamation) + ((int)MsgBoxStyle.OkOnly));
                    mdlGlobales.subDespErrores(ref tempRefParam9, ref tempRefParam10);
                }
                else
                {
                    //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                    string tempRefParam11 = "LA REMESA " + frmProcMasivo.DefInstance.txtRemesa.Text + " FUE GENERADA SATISFACTORIAMENTE";
                    MsgBoxStyle tempRefParam12 = (MsgBoxStyle)(((int)MsgBoxStyle.Exclamation) + ((int)MsgBoxStyle.OkOnly));
                    mdlGlobales.subDespErrores(ref tempRefParam11, ref tempRefParam12);
                    frmProcMasivo.DefInstance.subLimpiarDatos();
                }
                return result;
            }
            catch (Exception excep)
            {
                //UPGRADE_TODO: (1065) Error handling statement (On Error Resume Next) could not be converted properly. A throw statement was generated instead.
                //throw new Exception("Migration Exception: 'On Error Resume Next' not supported");
                //UPGRADE_WARNING: (6021) Casting 'int' to Enum may cause different behaviour.
                string tempRefParam13 = "ERROR EN EL ENVÍO TRANSACCIONAL; " + Information.Err().Number.ToString() + ": " + excep.Message;
                MsgBoxStyle tempRefParam14 = (MsgBoxStyle)(((int)MsgBoxStyle.Critical) + ((int)MsgBoxStyle.OkOnly));
                mdlGlobales.subDespErrores(ref tempRefParam13, ref tempRefParam14);
                FileSystem.FileClose(intFreeFile);
                return result;
                throw new Exception("Migration Exception: 'On Error Resume Next' not supported");
            }
        }

        static public void subPoneHeader(string strDatos)
        {
            estHeader5595.strHDCveTran.Value = Strings.Mid(strDatos, 1, 4);
            estHeader5595.strHDFiller01.Value = Strings.Mid(strDatos, 5, 1);
            estHeader5595.strHDSubTran.Value = Strings.Mid(strDatos, 6, 2);
            estHeader5595.strHDFolPreimpreso.Value = Strings.Mid(strDatos, 8, 16);
            estHeader5595.strHDFolInterno.Value = Strings.Mid(strDatos, 24, 8);
            estHeader5595.strHDSistOrigen.Value = Strings.Mid(strDatos, 32, 4);
            estHeader5595.strHDTramite.Value = Strings.Mid(strDatos, 36, 2);
            estHeader5595.strHDEntOrig.Value = Strings.Mid(strDatos, 38, 2);
            estHeader5595.strHDGpoEntOrig.Value = Strings.Mid(strDatos, 40, 4);
            estHeader5595.strHDCveEntOrig.Value = Strings.Mid(strDatos, 44, 4);
            estHeader5595.strHDEstatus.Value = Strings.Mid(strDatos, 48, 3); //MMS 11/05 Se recorren posiciones debido al incremento en la longitud del campo (2 a 3)
            estHeader5595.strHDCveResp.Value = Strings.Mid(strDatos, 51, 2); //MMS 11/05 Se recorren posiciones
            estHeader5595.strHDDescResp.Value = Strings.Mid(strDatos, 53, 50); //MMS 11/05 Se recorren posiciones
            estHeader5595.strHDNominaOper.Value = Strings.Mid(strDatos, 103, 10); //MMS 11/05 Se recorren posiciones
            estHeader5595.strHDCvePaqEval.Value = Strings.Mid(strDatos, 113, 4); //MMS 11/05 Se recorren posiciones
            estHeader5595.strHDCveProceso.Value = Strings.Mid(strDatos, 117, 3); //MMS 11/05 Se recorren posiciones debido al incremento en la longitud del campo (2 a 3)
            estHeader5595.strHDFlagInfo.Value = Strings.Mid(strDatos, 120, 1); //MMS 11/05 Se recorren posiciones
            estHeader5595.strHDCveRechazo.Value = Strings.Mid(strDatos, 121, 4); //MMS 11/05 Se recorren posiciones
            estHeader5595.strHDPantalla.Value = Strings.Mid(strDatos, 125, 8); //MMS 11/05 Se recorren posiciones
            estHeader5595.strHDNumMapa.Value = Strings.Mid(strDatos, 133, 3); //MMS 11/05 Se recorren posiciones debido al incremento en la longitud del campo (2 a 3)
            estHeader5595.strHDProcIni.Value = Strings.Mid(strDatos, 136, 3); //MMS 11/05 Se recorren posiciones debido al incremento en la longitud del campo (2 a 3)
            estHeader5595.strHDFiller02.Value = Strings.Mid(strDatos, 139, 38); //MMS 11/05 Se recorren posiciones
        }
    }
}