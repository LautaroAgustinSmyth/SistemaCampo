using BE;
using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Servicios
{
    public class Bitacora
    {
        private readonly DAL.Bitacora _bitacoraDAL = new DAL.Bitacora();

        public void Registrar(Form formulario, string actividad, Criticidad criticidad)
        {
            Seguridad.SessionManager sesion = Seguridad.SessionManager.GetInstance();

            if (sesion.Usuario == null) return;

            BE.Bitacora registro = new BE.Bitacora
            {
                Fecha = DateTime.Now,
                IdUsuario = sesion.Usuario.IdUsuario,
                NombreUsuario = sesion.Usuario.NombreUsuario,
                Modulo = formulario.Text,
                Actividad = actividad,
                Criticidad = criticidad,
                Detalle = $"El usuario '{sesion.Usuario.NombreUsuario}' realizó '{actividad}' " +
                             $"en el módulo '{formulario?.Text}' (criticidad: {criticidad}).",
                IP = ObtenerIPLocal()
            };
            _bitacoraDAL.Registrar(registro);
        }
        public static string ObtenerIPLocal()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}