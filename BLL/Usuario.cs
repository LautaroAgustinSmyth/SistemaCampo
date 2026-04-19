using Seguridad;
using Servicios;
using System;
using System.Windows.Forms;

namespace BLL
{
    public class Usuario
    {
        private readonly DAL.Usuario _usuarioDAL = new DAL.Usuario();
        private readonly Bitacora _bitacora = new Bitacora();

        public bool Login(Form formulario, string nombreUsuario, string contraseña)
        {
            if (string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(contraseña))
                throw new Exception("Credenciales incompletas.");

            BE.Usuario usuario = _usuarioDAL.ObtenerPorNombreUsuario(nombreUsuario);
            if (usuario == null) return false;

            bool esValido = Encriptador.VerificarContraseña(contraseña, usuario.Contraseña);

            if (esValido)
            {
                SessionManager.GetInstance().Login(usuario);
                _bitacora.Registrar(formulario, "Inicio Sesion", BE.Criticidad.Baja);
            }

            return esValido;
        }

        public void Logout(Form formulario)
        {
            _bitacora.Registrar(formulario, "Cierre Sesion", BE.Criticidad.Baja);
            SessionManager.GetInstance().Logout();
        }

        public void Alta(string nombreUsuario, string contraseña)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario) || string.IsNullOrWhiteSpace(contraseña))
                throw new ArgumentException("Usuario o contraseña no pueden estar vacíos.");

            _usuarioDAL.Alta(nombreUsuario, Encriptador.Hash(contraseña));
        }
    }
}