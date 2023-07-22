using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using CapaNegocio;


namespace Sistema_Petty_Cash
{
    public partial class Menu : Form
    {
        private bool Editar;
        private string idRoc = null;
        CN_Recibo objCN = new CN_Recibo();
        public Menu()
        {
            InitializeComponent();
            tabControl1.TabPages.Remove(tabPage2);
            tabControl1.TabPages.Remove(tabPage3);
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            MostrarRecibos();
        }
        private void MostrarRecibos()
        {
            CN_Recibo objeto = new CN_Recibo();
            dgvRec.DataSource = objeto.MostrarRec();
        }
        private void btnRegistrarR_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabPage1);
            tabControl1.TabPages.Add(tabPage2);
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabPage2);
            tabControl1.TabPages.Add(tabPage1);
            // Insertar Datos
            if (Editar == false)
            {
                try
                {
                    objCN.InsertarRec(txtRoc.Text, txtFecha.Text, txtSolicitante.Text, txtMonto.Text, txtConcepto.Text, txtCantidad.Text);
                    MessageBox.Show("El recibo se agregó correctamente");
                    MostrarRecibos();
                    LimpiarCampos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo insertar los datos por " + ex);
                }
            }
            // Editar Datos
            if(Editar == true)
            {
                try
                {
                    objCN.EditarRec(txtRoc.Text, txtFecha.Text, txtSolicitante.Text, txtMonto.Text, txtConcepto.Text, txtCantidad.Text);
                    MessageBox.Show("Se edito correctamente");
                    MostrarRecibos();
                    LimpiarCampos();
                    Editar = false;
                }
                catch(Exception ex) {
                    MessageBox.Show("No se lograron editar los datos por " + ex);
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabPage1);
            tabControl1.TabPages.Add(tabPage2);

            if (dgvRec.SelectedRows.Count > 0)
            {   
                txtRoc.Text = dgvRec.CurrentRow.Cells["ROC"].Value.ToString();
                txtFecha.Text = dgvRec.CurrentRow.Cells["FECHA_RECIBO"].Value.ToString();
                txtSolicitante.Text = dgvRec.CurrentRow.Cells["SOLICITANTE"].Value.ToString();
                txtMonto.Text = dgvRec.CurrentRow.Cells["MONTO"].Value.ToString();
                txtConcepto.Text = dgvRec.CurrentRow.Cells["CONCEPTO"].Value.ToString();
                txtCantidad.Text = dgvRec.CurrentRow.Cells["CANTIDAD"].Value.ToString();
                Editar = true;
            }
            else
            {
                MessageBox.Show("Seleccione una fila para realizar este proceso");
            }
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvRec.SelectedRows.Count > 0)
            {
                idRoc = dgvRec.CurrentRow.Cells["ROC"].Value.ToString();
                objCN.EliminarRec(idRoc);
                MessageBox.Show("Eliminado correctamente");
                MostrarRecibos();
            }
            else
            {
                MessageBox.Show("Seleccione una fila para realizar este proceso");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            tabControl1.TabPages.Remove(tabPage2);
            tabControl1.TabPages.Add(tabPage1);
        }


        // Métodos para cambiar el tamaño del formulario
        private void btnMa_Click(object sender, EventArgs e)
        {
            btnMa.Visible = false;
            btnMax.Visible = true;

            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
        }

        private void btnMid_Click(object sender, EventArgs e)
        {
            btnMax.Visible = false;
            btnMa.Visible = true;

            if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea salir de la aplicación?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Minimized;
            }
            else if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Minimized;
            }
        }

        private void LimpiarCampos()
        {
            txtRoc.Clear();
            txtFecha.Clear();
            txtSolicitante.Clear();
            txtMonto.Clear();
            txtConcepto.Clear();
            txtCantidad.Clear();
        }

        private void btnArq_Click(object sender, EventArgs e)
        {
           Arqueo arqueo = new Arqueo();    
            arqueo.Show();
        }
    }
}
