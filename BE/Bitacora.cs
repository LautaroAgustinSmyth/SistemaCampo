using System;

namespace BE
{
    public class Bitacora
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int IdUsuario { get; set; }
        public string Modulo { get; set; }
        public string Actividad { get; set; }
        public Criticidad Criticidad { get; set; }
        public string Detalle { get; set; }
    }
}
