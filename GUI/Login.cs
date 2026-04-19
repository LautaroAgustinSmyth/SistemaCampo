using BLL;
using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class Login : Form
    {
        private Usuario usuarioBLL = new Usuario();

        public Login()
        {
            InitializeComponent();
            txtContraseña.PasswordChar = '*';
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            lblError.Text = string.Empty;
            try
            {
                bool esValido = usuarioBLL.Login(this, txtUsuario.Text, txtContraseña.Text);
                if (esValido)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    lblError.Text = "Usuario o contraseña incorrectos.";
                    txtContraseña.Clear();
                    txtContraseña.Focus();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
    }
}