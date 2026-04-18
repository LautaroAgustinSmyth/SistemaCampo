using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class Acceso
    {
        private static Acceso _instance;
        private static readonly object _lock = new object();

        private readonly string cadenaConexion = "Data Source=.;Initial Catalog=Login;Integrated Security=True";
        private SqlConnection _conexion;
        private SqlConnection Conexion
        {
            get
            {
                if (_conexion == null)
                {
                    _conexion = new SqlConnection(cadenaConexion);
                }
                if (_conexion.State != ConnectionState.Open)
                {
                    _conexion.Open();
                }
                return _conexion;
            }
        }
        private Acceso()
        {
            _conexion = new SqlConnection(cadenaConexion);
        }
        public static Acceso GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Acceso();
                    }
                }
            }
            return _instance;
        }

        public DataTable Leer(string consulta, SqlParameter[] parametros)
        {
            using (SqlCommand cmd = new SqlCommand(consulta, Conexion))
            {
                if (parametros != null)
                {
                    cmd.Parameters.AddRange(parametros);
                }

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable tabla = new DataTable();
                    adapter.Fill(tabla);
                    return tabla;
                }
            }
        }

        public int Escribir(string consulta, SqlParameter[] parametros)
        {
            using (SqlCommand cmd = new SqlCommand(consulta, Conexion))
            {
                if (parametros != null)
                {
                    cmd.Parameters.AddRange(parametros);
                }
                int afectadas = cmd.ExecuteNonQuery();
                return afectadas;
            }
        }

        public void CerrarConexion()
        {
            try
            {
                if (_conexion != null && _conexion.State != ConnectionState.Closed)
                {
                    _conexion.Close();
                }
            }
            catch { }
        }
    }
}
