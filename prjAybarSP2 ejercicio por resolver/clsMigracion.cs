using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjAybarSP2_ejercicio_por_resolver
{
    internal class clsMigracion
    {
        public string RutaBD { get; private set; }
        public string RutaCategorias { get; private set; }
        public string RutaArticulos { get; private set; }

        private ConexionBD conexionBD;
    
        public clsMigracion()
        {
            string rutaProyecto = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", ".."));

            RutaBD = Path.Combine(rutaProyecto, "Base Datos", "bdGoodHard1.accdb");

            RutaCategorias = Path.Combine(rutaProyecto, "Base Datos", "Archivos", "Categorias.txt");
            RutaArticulos = Path.Combine(rutaProyecto, "Base Datos", "Archivos", "Articulos.txt");

            conexionBD = new ConexionBD(RutaBD);
        }

        public int MigrarCategorias()
        {
            using (OleDbConnection conexion = conexionBD.ObtenerConexion())
            {
                LimpiarTabla(conexion, "Categorias");
                string[] lineas = File.ReadAllLines(RutaCategorias, Encoding.UTF8);
                int registros = 0;

                foreach (string linea in lineas)
                {
                    if (string.IsNullOrWhiteSpace(linea)) continue;
                    string[] partes = linea.Split('|');
                    if (partes.Length >= 2)
                    {
                        InsertarCategoria(conexion, partes[0].Trim(), partes[1].Trim());
                        registros++;
                    }
                }
                return registros;
            }
        }

        public int MigrarArticulos()
        {
            using (OleDbConnection conexion = conexionBD.ObtenerConexion())
            {
                LimpiarTabla(conexion, "Articulos");
                string[] lineas = File.ReadAllLines(RutaArticulos, Encoding.UTF8);
                int registros = 0;

                foreach (string linea in lineas)
                {
                    if (string.IsNullOrWhiteSpace(linea)) continue;
                    string[] partes = linea.Split('|');
                    if (partes.Length >= 4)
                    {
                        InsertarArticulo(conexion, partes[0].Trim(), partes[1].Trim(), partes[2].Trim(), partes[3].Trim());
                        registros++;
                    }
                }
                return registros;
            }
        }

        private void InsertarCategoria(OleDbConnection cn, string id, string nombre)
        {
            string sql = "INSERT INTO Categorias (idCategoria, Nombre) VALUES (?, ?)";
            using (OleDbCommand cmd = new OleDbCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nom", nombre);
                cmd.ExecuteNonQuery();
            }
        }

        private void InsertarArticulo(OleDbConnection cn, string id, string nom, string cat, string precio)
        {
            string sql = "INSERT INTO Articulos (idArticulo, Nombre, idCategoria, precio) VALUES (?, ?, ?, ?)";
            using (OleDbCommand cmd = new OleDbCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nom", nom);
                cmd.Parameters.AddWithValue("@cat", cat);
                cmd.Parameters.AddWithValue("@pre", Convert.ToDecimal(precio));
                cmd.ExecuteNonQuery();
            }
        }

        private void LimpiarTabla(OleDbConnection cn, string tabla)
        {
            string sql = $"DELETE FROM {tabla}";
            using (OleDbCommand cmd = new OleDbCommand(sql, cn)) { cmd.ExecuteNonQuery(); }
        }
    }
}
