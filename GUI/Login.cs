using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class Login : Form
    {
        private readonly BLL.Usuario _usuarioBLL = new BLL.Usuario();

        public Login()
        {
            InitializeComponent();
            txtContraseña.PasswordChar = '*';
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            lblError.Text = string.Empty;
            try
            {
                bool esValido = _usuarioBLL.IniciarSesion(this.Text, txtUsuario.Text, txtContraseña.Text);
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
            catch (InvalidOperationException ex)
            {
                lblError.Text = ex.Message;
            }
            catch (ArgumentException ex)
            {
                lblError.Text = ex.Message;
            }
            catch (Exception ex)
            {
                lblError.Text = "Error inesperado. Intente nuevamente.";
                System.Diagnostics.Debug.WriteLine($"[Login] {ex}");
            }
            finally
            {
                txtContraseña.Clear();
            }
        }
    }
}