using BE;
using System;
using System.Net;
using System.Net.Sockets;

namespace Servicios
{
    public class Bitacora
    {
        private readonly DAL.Bitacora _bitacoraDAL = new DAL.Bitacora();

        public void Registrar(string modulo, string actividad, Criticidad criticidad)
        {
            Seguridad.SessionManager sesion = Seguridad.SessionManager.GetInstance();

            BE.Usuario usuario = sesion.Usuario;

            if (sesion.Usuario == null) return;

            BE.Bitacora registro = new BE.Bitacora
            {
                Fecha = DateTime.Now,
                IdUsuario = usuario.IdUsuario,
                NombreUsuario = usuario.NombreUsuario,
                Modulo = modulo ?? string.Empty,
                Actividad = actividad,
                Criticidad = criticidad,
                Detalle = $"El usuario '{usuario.NombreUsuario}' realizó '{actividad}' " +
                                 $"en el módulo '{modulo}' (criticidad: {criticidad}).",
                IP = ObtenerIPLocal()
            };
            _bitacoraDAL.Registrar(registro);
        }

        public static string ObtenerIPLocal()
        {
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                        return ip.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo obtener la dirección IP local.", ex);
            }
            return "IP desconocida";
        }
    }
}