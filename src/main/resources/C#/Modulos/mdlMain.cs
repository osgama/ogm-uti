using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Masivos
{
    class mdlMain
    {
        //AIS-1893 FSABORIO
        public static string ApplicationPath = string.Empty;

        static mdlMain()
        {
            //MIG WXP INI JGC 20090825
            //string strMASRuta1 = mdlRegistry.RegistryMasivos("MASRuta1");
            ApplicationPath = mdlRegistry.RegistryMasivos("MASRutaAplicacion");
            //MIG WXP FIN JGC 20090825
            //ApplicationPath = Path.GetDirectoryName(Application.ExecutablePath) + "\\Masivos";
            //ApplicationPath = Path.GetDirectoryName(Application.ExecutablePath) + strMASRuta1;
        }
    }
}
