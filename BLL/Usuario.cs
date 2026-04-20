using System;

namespace BLL
{
    public class Usuario
    {
        private readonly DAL.Usuario _usuarioDAL = new DAL.Usuario();
        private readonly Servicios.Bitacora _bitacora = new Servicios.Bitacora();

        public bool IniciarSesion(string modulo, string nombreUsuario, string contraseña)
        {
            if (string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(contraseña))
                throw new Exception("Credenciales incompletas.");

            BE.Usuario usuario = _usuarioDAL.ObtenerPorNombreUsuario(nombreUsuario);
            if (usuario == null) return false;

            if (usuario.Bloqueado)
                throw new InvalidOperationException("El usuario se encuentra bloqueado.");

            bool esValido = Seguridad.Encriptador.Verificar(contraseña, usuario.Contraseña);

            if (esValido)
            {
                Seguridad.SessionManager.GetInstance().Login(usuario);
                RegistrarUltimoInicio(usuario.IdUsuario);
                _bitacora.Registrar(modulo, "Inicio Sesion", BE.Criticidad.Baja);
            }
            else
            {
                RegistrarIntentoFallido(nombreUsuario);
            }

            return esValido;
        }

        public void CerrarSesion(string modulo)
        {
            _bitacora.Registrar(modulo, "Cierre Sesion", BE.Criticidad.Baja);
            Seguridad.SessionManager.GetInstance().Logout();
        }

        public void Alta(string nombreUsuario, string contraseña)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                throw new ArgumentException("El nombre de usuario no puede estar vacío.", nameof(nombreUsuario));
            if (string.IsNullOrWhiteSpace(contraseña))
                throw new ArgumentException("La contraseña no puede estar vacía.", nameof(contraseña));

            string hashContraseña = Seguridad.Encriptador.Hash(contraseña);
            _usuarioDAL.Alta(nombreUsuario, hashContraseña);
        }

        private void RegistrarUltimoInicio(int idUsuario)
        {
            try { _usuarioDAL.ActualizarUltimoInicio(idUsuario); }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[BLL.Usuario] No se pudo actualizar último inicio: {ex.Message}");
            }
        }

        private void RegistrarIntentoFallido(string nombreUsuario)
        {
            try { _usuarioDAL.IncrementarIntentosFallidos(nombreUsuario); }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[BLL.Usuario] No se pudo incrementar intentos fallidos: {ex.Message}");
            }
        }
    }
}