using BE;
using System;
using System.Windows.Forms;

namespace Servicios
{
    public class Bitacora
    {
        DAL.Bitacora bitacora;
        public void Registrar(Form formulario, string actividad, Criticidad criticidad)
        {
            BE.Bitacora registro = new BE.Bitacora();
            registro.Fecha = DateTime.Now;
            registro.IdUsuario = Seguridad.SessionManager.GetInstance().Usuario.Id;
            registro.Modulo = formulario.Text;
            registro.Actividad = actividad;
            registro.Criticidad = criticidad;

            bitacora.Registrar(registro);
        }
    }
}
