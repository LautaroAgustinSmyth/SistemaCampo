using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class Usuario
    {
        private readonly Acceso _acceso = Acceso.GetInstance();

        public void Alta(string nombreUsuario, string contraseña)
        {
            SqlParameter[] parametros = new SqlParameter[] {
                new SqlParameter("@nombreUsuario", nombreUsuario),
                new SqlParameter("@contraseña", contraseña)
            };
            _acceso.Escribir(
                "INSERT INTO Usuario (NombreUsuario, Contraseña) VALUES (@nombreUsuario, @contraseña)", parametros);
        }

        public BE.Usuario ObtenerPorNombreUsuario(string nombreUsuario)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@nombreUsuario", nombreUsuario)
            };

            try
            {
                DataTable tabla = _acceso.Leer("SELECT IdUsuario, NombreUsuario, Contraseña FROM Usuario WHERE NombreUsuario = @nombreUsuario", parametros);

                if (tabla != null && tabla.Rows.Count > 0)
                {
                    DataRow row = tabla.Rows[0];
                    return new BE.Usuario()
                    {
                        IdUsuario = Convert.ToInt32(row["IdUsuario"]),
                        NombreUsuario = row["NombreUsuario"].ToString(),
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