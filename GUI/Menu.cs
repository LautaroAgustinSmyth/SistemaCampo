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

        private void CerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _usuarioBLL.CerrarSesion(this.Text);
            Application.Restart();
        }

        private void BitacoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitacora hijo = new Bitacora();
            hijo.MdiParent = this;
            hijo.Show();
        }
    }
}