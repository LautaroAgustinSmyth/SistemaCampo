using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BLL.Usuario usuarioBLL = new BLL.Usuario();
            usuarioBLL.Logout(this);
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
