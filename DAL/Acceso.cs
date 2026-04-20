using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public sealed class Acceso
    {
        private static Acceso _instance;
        private static readonly object _lock = new object();

        private readonly string _cadenaConexion = ConfigurationManager.ConnectionStrings["MiConexionSQL"].ConnectionString;

        private Acceso()
        { }

        public static Acceso GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new Acceso();
                }
            }
            return _instance;
        }

        public DataTable Leer(string consulta, SqlParameter[] parametros)
        {
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            using (SqlCommand cmd = new SqlCommand(consulta, conexion))
            {
                if (parametros != null)
                    cmd.Parameters.AddRange(parametros);
                conexion.Open();
                DataTable tabla = new DataTable();
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    adapter.Fill(tabla);
                return tabla;
            }
        }

        public int Escribir(string consulta, SqlParameter[] parametros)
        {
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            using (SqlCommand cmd = new SqlCommand(consulta, conexion))
            {
                if (parametros != null)
                    cmd.Parameters.AddRange(parametros);
                conexion.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public bool VerificarConexion()
        {
            try
            {
                using (var conexion = new SqlConnection(_cadenaConexion))
                {
                    conexion.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Acceso] Error: {ex.Message}");
                return false;
            }
        }
    }
}