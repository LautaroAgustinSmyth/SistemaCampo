using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class Usuario
    {
        Acceso acceso = Acceso.GetInstance();

        public void Alta(string username, string contraseña)
        {
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("username", username),
                new SqlParameter("contraseña", contraseña)
            };
            acceso.Escribir("INSERT INTO Usuario (Username, Contraseña) VALUES (@username, @contraseña)", sp);
        }

        public BE.Usuario ObtenerPorUsername(string username)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@Username", username)
            };

            try
            {
                DataTable tabla = acceso.Leer("SELECT Id, Username, Contraseña FROM Usuario WHERE Username = @Username", parametros);

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
