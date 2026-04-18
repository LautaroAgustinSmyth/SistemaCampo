using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Bitacora
    {
        public string Id { get; set; }
        public DateTime Fecha { get; set; }
        public BE.Usuario Usuario { get; set; }
        public string Modulo { get; set; }
        public string Actividad { get; set; }
        public string Detalle { get; set; }
        public Criticidad Criticidad { get; set; }
    }
}
