using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Masivos
{
    public class clsValorCatalogoOD
    {
        public string RegresaValorCatalogo(string stCatalogo, string stLlave2, string stLlave3, string stLlave4, string stLlave5)
        {
            string stClaveIni = null;

            if (mdlComunica.OleCatalogos.BuscaCatalogo(ref stCatalogo, ref stLlave2, ref stLlave3, ref stLlave4, ref stLlave5))
            {                
                stClaveIni = mdlComunica.OleCatalogos.getRangoIni;
                return stClaveIni;
            }
            return null;
        }

    }
}
