using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Bitacora
    {
        Acceso acceso = Acceso.GetInstance();
        public void Registrar(DateTime fecha, string usuario, string detalle)
        {
            SqlParameter[] parametros = new SqlParameter[] {
                new SqlParameter("@fecha",fecha),
                new SqlParameter("@usuario",usuario),
                new SqlParameter("@detalle",detalle)
            };

            try
            {
                acceso.Escribir("INSERT INTO Bitacora (fecha, usuario, detalle) VALUES (@fecha, @usuario, @detalle)", parametros);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error al Crear el registro de Bitacora", ex);
            }
        }
    }
}
