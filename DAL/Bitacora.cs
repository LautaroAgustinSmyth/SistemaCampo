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
                new SqlParameter("@fecha",registro.Fecha),
                new SqlParameter("@usuario",registro.IdUsuario),
                new SqlParameter("@modulo",registro.Modulo),
                new SqlParameter("@actividad",registro.Actividad),
                new SqlParameter("@criticidad",(int)registro.Criticidad),
                new SqlParameter("@detalle",registro.Detalle)
            };

            try
            {
                _acceso.Escribir("INSERT INTO Bitacora (fecha, usuario, modulo, actividad, criticidad, detalle) VALUES (@fecha, @usuario, @modulo, @actividad, @criticidad, @detalle)", parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar en Bitácora.", ex);
            }
        }
    }
}