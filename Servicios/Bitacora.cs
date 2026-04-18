using BE;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;
using DAL;

namespace Servicios
{
    public class Bitacora
    {
        DAL.Bitacora bitacora;
        public void Grabar(Form formulario, string actividad, Criticidad criticidad)
        {
            BE.Bitacora registro = new BE.Bitacora();
            registro.Fecha = DateTime.Now;
            registro.Usuario = SessionManager.GetInstance().Usuario;
            registro.Modulo = formulario.Text;
            registro.Actividad = actividad;
            registro.Criticidad = criticidad;

            bitacora.Registrar(registro);
        }
    }
}
