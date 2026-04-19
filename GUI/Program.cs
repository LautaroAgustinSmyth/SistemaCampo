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
            BLL.Configuracion.VerificarConexionDAL();

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
