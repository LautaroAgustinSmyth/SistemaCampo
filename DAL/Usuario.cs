using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class Usuario
    {
        private readonly Acceso _acceso = Acceso.GetInstance();

        public void Alta(string username, string contraseñaHash)
        {
            SqlParameter[] parametros = new SqlParameter[] {
                new SqlParameter("@username", username),
                new SqlParameter("@contraseña", contraseñaHash)
            };
            _acceso.Escribir(
                "INSERT INTO Usuario (Username, Contraseña) VALUES (@username, @contraseña)", parametros);
        }

        public BE.Usuario ObtenerPorUsername(string username)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@Username", username)
            };

            try
            {
                DataTable tabla = _acceso.Leer("SELECT Id, Username, Contraseña FROM Usuario WHERE Username = @Username", parametros);

                if (tabla != null && tabla.Rows.Count > 0)
                {
                    DataRow row = tabla.Rows[0];
                    return new BE.Usuario()
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Username = row["Username"].ToString(),
                        Contraseña = row["Contraseña"].ToString()
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el usuario desde la base de datos.", ex);
            }
        }
    }
}
