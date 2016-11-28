using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            lblHora.Text = DateTime.Now.ToString();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            DataTable datos = CapaNegocio.NTrabajador.Login(txtUsuario.Text,txtPassword.Text);
            if (datos.Rows.Count == 0)
            {
                MessageBox.Show("Usuario no Existe prro", "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else {
                FrmPrincipal frm = new FrmPrincipal();
                frm.IdTrabajador = datos.Rows[0][0].ToString();
                frm.Apellido = datos.Rows[0][1].ToString();
                frm.Nomnbre = datos.Rows[0][2].ToString();
                frm.Acceso = datos.Rows[0][3].ToString();

                frm.Show();
                Hide();
            }
        }
    }
}
