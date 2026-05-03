using System;
using System.Data.OleDb;
using System.IO;

namespace prjAybarSP2_ejercicio_por_resolver
{
    public class ConexionBD
    {
        private string rutaBaseDatos;

        public ConexionBD(string ruta)
        {
            rutaBaseDatos = ruta;
        }

        /// <summary>
        /// Obtiene una conexión a la base de datos Access
        /// </summary>
        public OleDbConnection ObtenerConexion()
        {
            string cadenaConexion = ObtenerCadenaConexion();
            OleDbConnection conexion = new OleDbConnection(cadenaConexion);
            conexion.Open();
            return conexion;
        }

        /// <summary>
        /// Obtiene la cadena de conexión válida
        /// </summary>
        private string ObtenerCadenaConexion()
        {
            if (!File.Exists(rutaBaseDatos))
            {
                throw new FileNotFoundException($"Base de datos no encontrada: {rutaBaseDatos}");
            }

            // Intentar con ACE.OLEDB.12.0 (Access 2007+, .accdb)
            try
            {
                string cadena = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={rutaBaseDatos}";
                TestearConexion(cadena);
                return cadena;
            }
            catch
            {
                // Fallback a Jet.OLEDB.4.0 (Access 2003, .mdb)
                try
                {
                    string cadena = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={rutaBaseDatos}";
                    TestearConexion(cadena);
                    return cadena;
                }
                catch
                {
                    throw new Exception("No se encontró un proveedor de base de datos disponible.\n" +
                        "Por favor, instala Microsoft Access Database Engine desde:\n" +
                        "https://www.microsoft.com/download/details.aspx?id=13255");
                }
            }
        }

        /// <summary>
        /// Prueba si la cadena de conexión es válida
        /// </summary>
        private void TestearConexion(string cadenaConexion)
        {
            using (OleDbConnection conexion = new OleDbConnection(cadenaConexion))
            {
                conexion.Open();
                conexion.Close();
            }
        }

        /// <summary>
        /// Ejecuta una consulta sin retornar datos
        /// </summary>
        public void EjecutarComando(string consulta, OleDbConnection conexion)
        {
            using (OleDbCommand comando = new OleDbCommand(consulta, conexion))
            {
                comando.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Verifica si la base de datos está disponible
        /// </summary>
        public bool VerificarBaseDatos()
        {
            try
            {
                using (OleDbConnection conexion = ObtenerConexion())
                {
                    return conexion.State == System.Data.ConnectionState.Open;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
