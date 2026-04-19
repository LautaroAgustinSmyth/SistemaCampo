using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class Acceso
    {
        private static Acceso _instance;
        private static readonly object _lock = new object();

        private readonly string _cadenaConexion = "Data Source=.;Initial Catalog=ProyectoFinal;Integrated Security=True";

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
                var tabla = Leer("SELECT 1", null);
                return tabla != null && tabla.Rows.Count > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}