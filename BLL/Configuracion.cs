using System;
using System.Data;
using System.Windows.Forms;


namespace BLL
{
    public class Configuracion
    {
        public static void VerificarConexionDAL()
        {
            try
            {
                DAL.Acceso acceso = DAL.Acceso.GetInstance();
                DataTable tabla = acceso.Leer("SELECT 1", null);
                if (tabla == null || tabla.Rows.Count == 0)
                {
                    MessageBox.Show("No se obtuvo respuesta válida de la base de datos. La aplicación se cerrará.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                acceso.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo conectar a la base de datos: " + ex.Message, "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
