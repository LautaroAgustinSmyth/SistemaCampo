using System;

namespace BLL
{
    public class Usuario
    {
        private DAL.Usuario usuarioDAL = new DAL.Usuario();
        private Encriptador encriptador = new Encriptador();
        private Servicios.Bitacora bitacora = new Servicios.Bitacora();

        public bool Login(string username, string contraseña)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(contraseña))
            {
                throw new Exception("Credenciales incompletas.");
            }

            BE.Usuario usuario = usuarioDAL.ObtenerPorUsername(username);

            if (usuario == null) return false;

            bool esValido = encriptador.VerificarContraseña(contraseña, usuario.Contraseña);

            if (esValido)
            {
                SessionManager.GetInstance().Login(usuario);
                bitacora.Registrar(DateTime.Now, $"Usuario {username} autenticado.");
            }

            return esValido;
        }

        public void Alta(string username, string contraseña)
        {
            usuarioDAL.Alta(username, encriptador.Hash(contraseña));
        }
    }
}
