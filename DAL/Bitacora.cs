using System;
using System.Data.SqlClient;

namespace DAL
{
    public class Bitacora
    {
        private readonly Acceso _acceso = Acceso.GetInstance();

        public void Registrar(BE.Bitacora registro)
        {
            SqlParameter[] parametros = new SqlParameter[] {
                new SqlParameter("@fecha", registro.Fecha),
                new SqlParameter("@idUsuario", registro.IdUsuario),
                new SqlParameter("@nombreUsuario", registro.NombreUsuario),
                new SqlParameter("@modulo", registro.Modulo),
                new SqlParameter("@actividad", registro.Actividad),
                new SqlParameter("@criticidad", (int)registro.Criticidad),
                new SqlParameter("@detalle", registro.Detalle),
                new SqlParameter("@ip", registro.IP)
            };

            try
            {
                _acceso.Escribir(
                    "INSERT INTO Bitacora (fecha, idUsuario, nombreUsuario, modulo, actividad, criticidad, detalle, ip) " +
                    "VALUES (@fecha, @idUsuario, @nombreUsuario, @modulo, @actividad, @criticidad, @detalle, @ip)", parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar en Bitácora.", ex);
            }
        }
    }
}