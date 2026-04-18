using System;
using System.Data.SqlClient;

namespace DAL
{
    public class Bitacora
    {
        Acceso acceso = Acceso.GetInstance();
        public void Registrar(BE.Bitacora registro)
        {
            SqlParameter[] parametros = new SqlParameter[] {
                new SqlParameter("@fecha",registro.Fecha),
                new SqlParameter("@usuario",registro.IdUsuario),
                new SqlParameter("@modulo",registro.Modulo),
                new SqlParameter("@actividad",registro.Actividad),
                new SqlParameter("@detalle",registro.Detalle),
                new SqlParameter("criticidad",registro.Criticidad)
            };

            try
            {
                acceso.Escribir("INSERT INTO Bitacora (fecha, usuario, modulo, actividad, detalle, criticidad) VALUES (@fecha, @usuario, @modulo, @actividad, @detalle, @criticidad)", parametros);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error al Crear el registro de Bitacora", ex);
            }
        }
    }
}
