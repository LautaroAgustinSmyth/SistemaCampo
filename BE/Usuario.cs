using System;

namespace BE
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaUltimoInicio { get; set; }
        public bool Activo { get; set; }
        public bool Bloqueado { get; set; }
        public int IntentosFallidos { get; set; }
    }
}