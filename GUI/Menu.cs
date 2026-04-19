using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class Menu : Form
    {
        private readonly BLL.Usuario _usuarioBLL = new BLL.Usuario();

        public Menu()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _usuarioBLL.Logout(this);
            Application.Restart();
        }

        private void bitacoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitacora hijo = new Bitacora();
            hijo.MdiParent = this;
            hijo.Show();
        }
    }
}