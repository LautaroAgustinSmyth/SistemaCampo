using System;
using System.Windows.Forms;

namespace GUI
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!BLL.Configuracion.VerificarConexionDAL())
            {
                Application.Exit();
                return;
            }

            Login frmLogin = new Login();
            if (frmLogin.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new Menu());
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
