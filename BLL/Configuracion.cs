using System.Windows.Forms;

namespace BLL
{
    public static class Configuracion
    {
        public static bool VerificarConexionDAL()
        {
            bool conectado = DAL.Acceso.GetInstance().VerificarConexion();

            if (!conectado)
            {
                MessageBox.Show(
                    "No se pudo conectar a la base de datos. Verifique la configuración y vuelva a intentarlo.",
                    "Error de conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            return conectado;
        }
    }
}