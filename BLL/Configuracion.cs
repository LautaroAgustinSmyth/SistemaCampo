namespace BLL
{
    public static class Configuracion
    {
        public static bool VerificarConexionDAL()
        {
            return DAL.Acceso.GetInstance().VerificarConexion();
        }
    }
}