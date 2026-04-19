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
                new SqlParameter("@contraseña", contraseña),
                new SqlParameter("@fechaRegistro", DateTime.Now),
                new SqlParameter("@fechaUltimoInicio", DBNull.Value),
                new SqlParameter("@activo", 1),
                new SqlParameter("@bloqueado", 0),
                new SqlParameter("@intentosFallidos", 0)
            };

            _acceso.Escribir(
                "INSERT INTO Usuario (NombreUsuario, Contraseña, FechaRegistro, FechaUltimoInicio, Activo, Bloqueado, IntentosFallidos) " +
                "VALUES (@nombreUsuario, @contraseña, @fechaRegistro, @fechaUltimoInicio, @activo, @bloqueado, @intentosFallidos)", parametros);
        }

        public void ActualizarUltimoInicio(int idUsuario)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@idUsuario", idUsuario),
                new SqlParameter("@fechaUltimoInicio", DateTime.Now),
                new SqlParameter("@intentosFallidos", 0)
            };

            try
            {
                _acceso.Escribir("UPDATE Usuario SET FechaUltimoInicio = @fechaUltimoInicio, IntentosFallidos = @intentosFallidos WHERE IdUsuario = @idUsuario", parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el último inicio del usuario.", ex);
            }
        }

        public void IncrementarIntentosFallidos(string nombreUsuario)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@nombreUsuario", nombreUsuario)
            };

            try
            {
                _acceso.Escribir("UPDATE Usuario SET IntentosFallidos = ISNULL(IntentosFallidos,0) + 1 WHERE NombreUsuario = @nombreUsuario", parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al incrementar intentos fallidos.", ex);
            }
        }

        public BE.Usuario ObtenerPorNombreUsuario(string nombreUsuario)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@nombreUsuario", nombreUsuario)
            };

            try
            {

                DataTable tabla = _acceso.Leer(
                    "SELECT IdUsuario, NombreUsuario, Contraseña, FechaRegistro, FechaUltimoInicio, Activo, Bloqueado, IntentosFallidos " +
                    "FROM Usuario WHERE NombreUsuario = @nombreUsuario", parametros);

                if (tabla != null && tabla.Rows.Count > 0)
                {
                    DataRow row = tabla.Rows[0];
                    return new BE.Usuario()
                    {
                        IdUsuario = Convert.ToInt32(row["IdUsuario"]),
                        NombreUsuario = row["NombreUsuario"].ToString(),
                        Contraseña = row["Contraseña"].ToString(),
                        FechaRegistro = Convert.ToDateTime(row["FechaRegistro"]),
                        FechaUltimoInicio = row["FechaUltimoInicio"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["FechaUltimoInicio"]),
                        Activo = Convert.ToBoolean(row["Activo"]),
                        Bloqueado = Convert.ToBoolean(row["Bloqueado"]),
                        IntentosFallidos = Convert.ToInt32(row["IntentosFallidos"])
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