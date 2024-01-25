/************************************************************************
 * Forma creada para el registro del Arribo de Remesas                  *
 * **********************************************************************
 * Forma que registra el Arribo de Remesas y valida su existencia y     *
 * congruencia con su archivo de control.                               *
 * **********************************************************************
 * INFOWARE                                                             *
 * **********************************************************************
 * Autor: Miguel Angel Garnica Angulo (Infoware, León)                  *
 *        Adrian Azades Hernandez Belmonte (Infoware, León) (AAHB)      *
 * **********************************************************************
 * Historico de cambios:                                                *
 *                                                                      *
 * Fecha de Creación:   01/10/09                                        *
 *                      Forma creada para registro de remesas.          *
 * Modificaciones:                                                      *
 *                                                                      *
 * **********************************************************************
 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Masivos
{
    public partial class frmErroresArribo : Form
    {

        //List<Linea> LsNuevaTemp = new List<Linea>();
        //List<Linea> LsNueva = new List<Linea>();

        /// <summary>
        /// contructor de la cale
        /// 
        /// </summary>
        public frmErroresArribo(List<Linea> LsError)
        {
            InitializeComponent();
            //dataGridView1.DataSource = LsError;
            //dataGridView1.Columns[3].DisplayIndex = 0;
            //dataGridView1.Columns[2].DisplayIndex = 1;
            //LsNuevaTemp = LsError;
            dataGridView1.DataSource = LsError;
        }

                

        private void btnArchivo_Click(object sender, EventArgs e)
        {
            string stLinea;
            string strNombreARchivo;
             int iCont = new int ();
            StreamWriter myStream;
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
               strNombreARchivo = saveFileDialog1.FileName;

               if (strNombreARchivo != null)
               {
                   myStream = new StreamWriter(strNombreARchivo);
                   while (iCont < dataGridView1.Rows.Count)
                   {
                        stLinea = dataGridView1.Rows[iCont].ToString();
                        myStream.WriteLine(stLinea); 
                   }                                
                   myStream.Close();                 
               }
            }
        }

        private void frmErrores_Load(object sender, EventArgs e)
        {
            

            
        }
    }
} 