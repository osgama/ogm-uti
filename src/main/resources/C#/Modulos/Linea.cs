using System;
using System.Collections.Generic;
using System.Text;

namespace Masivos
{
    public class Linea
     {
        private string remesa;
        public string Remesa { get { return remesa; } set { remesa = value; } }
        private bool existe;
        public bool Existe_Archivo_Remesa { get { return existe; } set { existe = value; } }
        private bool promotora;
        public bool Corresponde_Promotora { get { return promotora; } set { promotora = value; } }
        private bool captura;
        public bool Corresponde_Captura { get { return captura; } set { captura = value; } }
        private int errores;
        public int Errores { get { return errores; } set { errores = value; } }


        public Linea(string remesa, bool existe, bool promotora, bool captura, int errores)
        {
            this.Remesa = remesa;
            this.Existe_Archivo_Remesa = existe;
            this.Corresponde_Promotora = promotora;
            this.Corresponde_Captura = captura;
            this.Errores = errores; 
        }
    }// fin de la clase 
}
