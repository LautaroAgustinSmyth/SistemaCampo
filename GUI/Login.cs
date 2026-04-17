using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;

namespace GUI
{
    public partial class Login : Form
    {
        private Usuario usuarioBLL = new Usuario();

        public Login()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                bool esValido = usuarioBLL.Login(txtUsuario.Text, txtContraseña.Text);
                if (esValido)
                {
                    lblError.Text = "Usuario Logueado";
                }
                else
                {
                    lblError.Text = "Usuario o contraseña incorrectos.";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
    }
}
