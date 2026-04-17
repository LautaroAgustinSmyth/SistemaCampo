using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace Servicios
{
    public class SessionManager
    {
        private static SessionManager _instance;
        private static readonly object _lock = new object();
        public Usuario Usuario { get; private set; }

        private SessionManager() { }

        public static SessionManager GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new SessionManager();
                    }
                }
            }
            return _instance;
        }

        public void Login(Usuario usuario)
        {
            this.Usuario = usuario;
        }

        public void Logout()
        {
            this.Usuario = null;
        }
    }
}
