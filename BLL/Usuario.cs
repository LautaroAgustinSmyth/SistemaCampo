using Seguridad;
using Servicios;
using System;
using System.Windows.Forms;

namespace BLL
{
    public class Usuario
    {
        private DAL.Usuario usuarioDAL = new DAL.Usuario();
        private Servicios.Bitacora bitacora = new Servicios.Bitacora();

        public bool Login(Form formulario, string username, string contraseña)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(contraseña))
            {
                throw new Exception("Credenciales incompletas.");
            }

            BE.Usuario usuario = usuarioDAL.ObtenerPorUsername(username);

            if (usuario == null) return false;

            bool esValido = Encriptador.VerificarContraseña(contraseña, usuario.Contraseña);

            if (esValido)
            {
                SessionManager.GetInstance().Login(usuario);
                bitacora.Registrar(formulario, "Inicio Sesion", 0);
            }

            return esValido;
        }

        public void Logout(Form formulario)
        {
            bitacora.Registrar(formulario, "Cierre Sesion", 0);
            usuarioDAL.Logout();
            SessionManager.GetInstance().Logout();
        }

        public void Alta(string username, string contraseña)
        {
            usuarioDAL.Alta(username, Encriptador.Hash(contraseña));
        }
    }
}
