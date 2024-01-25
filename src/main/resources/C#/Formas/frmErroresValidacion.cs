/************************************************************************
 * Forma creada para mostrar los Errores de Validacion de Remesas       *
 * **********************************************************************
 * Forma que muestra las remesas que contiene algun error al momento de *
 * su validacion con su correspondiente descripcion                     *
 * **********************************************************************
 * INFOWARE                                                             *
 * **********************************************************************
 * Autor: Miguel Angel Garnica Angulo (Infoware, León)                  *
 *                                                                      *
 * **********************************************************************
 * Historico de cambios:                                                *
 *                                                                      *
 * Fecha de Creación:   21/10/09                                        *
 *                      Forma creada para validación de remesas.        *
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
    public partial class frmErroresValidacion : Form
    {
        public frmErroresValidacion()
        {
            InitializeComponent();
        }

        //Funcion encargada de mostrar en pantalla los errores que se produjeron durante la validacion.
        private void CargaErrores()
        {
            string stCadena = null;
            string stRespuesta = null;            
            string stEspacio = " ";
            string stHeaderBco = stEspacio.PadLeft(176);

            string stUFolio = "                ";
            string stUConsErrFolio = "  ";
            string stFlagMasInfo = "0";

            int iRegSize = 236;
            
            clsWRemesas clsErr = new clsWRemesas();

            string stFolio = "";

            //Condicion que verifica si existen mas datos por recibir y los almacena en la cadena de respuesta
            do
            {
                stCadena = clsErr.ErroresRemesa5562_23(stUFolio, stUConsErrFolio, stFlagMasInfo);
                if (stCadena != null)
                {
                    stFlagMasInfo = stCadena.Substring(112, 1);
                    stUFolio = stCadena.Substring(113, 16);
                    stUConsErrFolio = stCadena.Substring(129, 2);
                    
                    stRespuesta = stCadena.Substring(176);                    

                    int iNum = stRespuesta.Length / iRegSize;

                    for (int iCont = 0; iCont < iNum; iCont++)
                    {
                        stFolio = stRespuesta.Substring((iCont * iRegSize) + 0, 16);
                        if (stFolio.Trim() != "")
                            dgvErroresValidacion.Rows.Add(stRespuesta.Substring((iCont * iRegSize) + 0, 16),
                                stRespuesta.Substring((iCont * iRegSize) + 16, 4), stRespuesta.Substring((iCont * iRegSize) + 20, 40),
                                stRespuesta.Substring((iCont * iRegSize) + 60, 4), stRespuesta.Substring((iCont * iRegSize) + 64, 40),
                                stRespuesta.Substring((iCont * iRegSize) + 104, 4), stRespuesta.Substring((iCont * iRegSize) + 108, 40),
                                stRespuesta.Substring((iCont * iRegSize) + 148, 4), stRespuesta.Substring((iCont * iRegSize) + 152, 40),
                                stRespuesta.Substring((iCont * iRegSize) + 192, 4), stRespuesta.Substring((iCont * iRegSize) + 196, 40));
                    }
                }
                else
                {
                    dgvErroresValidacion.Enabled = false;
                    stFlagMasInfo = "0";
                }
            } while (stFlagMasInfo == "1");
        }

        //Evento del cargado de la forma Errores
        private void frmErrores_Load(object sender, EventArgs e)
        {
            mtbRemesa.Text = frmValidaRemesas.arrEmpCapt[frmValidaRemesas.iCell] +
                             frmValidaRemesas.arrEmpPromo[frmValidaRemesas.iCell] +
                             frmValidaRemesas.arrFecCapt[frmValidaRemesas.iCell] +
                             frmValidaRemesas.arrTipoTram[frmValidaRemesas.iCell] +
                             frmValidaRemesas.arrFamProd[frmValidaRemesas.iCell] +                             
                             frmValidaRemesas.arrConsCapt[frmValidaRemesas.iCell];
            mtbProceso.Text = frmValidaRemesas.arrProc[frmValidaRemesas.iCell];
            mtbStatus.Text = frmValidaRemesas.arrEsta[frmValidaRemesas.iCell];

            dgvErroresValidacion.Rows.Clear();

            CargaErrores();            
        }
                
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnArchivo_Click(object sender, EventArgs e)
        {            
            string stLineaArchivo;
            string strNombreArchivo;
            int iColumna2 = 0;
            StreamWriter myStream;
            DateTimePicker dtpGeneraArchivo = new DateTimePicker();
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

            dtpGeneraArchivo.Value = DateTime.Now;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
               strNombreArchivo = saveFileDialog1.FileName;

               if (strNombreArchivo != null)
               {
                   myStream = new StreamWriter(strNombreArchivo);
                   stLineaArchivo = "Remesa: " + mtbRemesa.Text + " ".PadLeft(10) + dtpGeneraArchivo.Value.ToString();
                   myStream.WriteLine(stLineaArchivo);                   
                   for (int iLinea = 0; iLinea < dgvErroresValidacion.RowCount; iLinea++)
                   {
                       for (int iColumna = 0; iColumna < dgvErroresValidacion.ColumnCount; iColumna++)
                       {                           
                           if (iColumna % 2 != 0)
                               iColumna2 = 1;
                           else
                               iColumna2 = iColumna;
                           switch (iColumna2)
                           {
                               case 0: stLineaArchivo = "Folio: " + dgvErroresValidacion.Rows[iLinea].Cells[iColumna].Value.ToString() + " / ";
                                   break;
                               case 1: stLineaArchivo += "Campo: " + dgvErroresValidacion.Rows[iLinea].Cells[iColumna].Value.ToString() + "  ";
                                   break;
                               default: stLineaArchivo += "Error: " + dgvErroresValidacion.Rows[iLinea].Cells[iColumna].Value.ToString() + " ";
                                   break;
                           }                                                     
                       }
                       myStream.WriteLine(stLineaArchivo); 
                   }
                   myStream.WriteLine("*** FIN DE ARCHIVO ***");
                   myStream.Close();

                   MessageBox.Show("Archivo Generado Correctamente", "C753 ARIES - Validacion Remesa",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
               }               
            }
            
        }
    }
}