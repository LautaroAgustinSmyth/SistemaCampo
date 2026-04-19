using BE;
using System;
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
                Modulo = formulario.Text,
                Actividad = actividad,
                Criticidad = criticidad,
                Detalle = $"El usuario '{sesion.Usuario.NombreUsuario}' realizó '{actividad}' " +
                             $"en el módulo '{formulario?.Text}' (criticidad: {criticidad})."
            };
            _bitacoraDAL.Registrar(registro);
        }
    }
}
