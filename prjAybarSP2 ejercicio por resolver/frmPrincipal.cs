using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjAybarSP2_ejercicio_por_resolver
{
    public partial class frmPrincipal : Form
    {
        private string rutaBaseDatos;
        private string rutaCategoriasArchivo;
        private string rutaArticulosArchivo;
        private ConexionBD conexionBD;

        private void ConfigurarRutas()
        {
            // Obtener la ruta del proyecto (subir dos niveles desde bin\Debug o bin\Release)
            string rutaProyecto = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..");
            rutaProyecto = Path.GetFullPath(rutaProyecto);

            rutaBaseDatos = Path.Combine(rutaProyecto, "Base Datos", "bdGoodHard1.accdb");
            rutaCategoriasArchivo = Path.Combine(rutaProyecto, "Categorias.txt");
            rutaArticulosArchivo = Path.Combine(rutaProyecto, "Articulos.txt");

            conexionBD = new ConexionBD(rutaBaseDatos);
        }

        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConfigurarRutas();
            btnMigra.Click += BtnMigra_Click;
        }

        private void BtnMigra_Click(object sender, EventArgs e)
        {
            RealizarMigracion();
        }

        private void RealizarMigracion()
        {
            txtInfo.Clear();
            AgregarInfo("════════════════════════════════════════\r\n");
            AgregarInfo("  INICIANDO MIGRACIÓN\r\n");
            AgregarInfo("════════════════════════════════════════\r\n\r\n");

            try
            {
                // Paso 1: Verificar que la base de datos existe
                AgregarInfo("• Verificando base de datos...\r\n");
                if (!File.Exists(rutaBaseDatos))
                {
                    AgregarInfo("✗ Error: Base de datos no encontrada\r\n");
                    return;
                }
                AgregarInfo("✓ Base de datos encontrada\r\n\r\n");

                // Paso 2: Migrar Categorías
                AgregarInfo("• Buscando archivo de categorías...\r\n");
                if (File.Exists(rutaCategoriasArchivo))
                {
                    AgregarInfo("✓ Archivo encontrado\r\n");
                    AgregarInfo("• Transfiriendo datos de categorías...\r\n");
                    int registrosCat = MigrarCategorias();
                    AgregarInfo($"✓ {registrosCat} categorías migradas\r\n\r\n");
                }
                else
                {
                    AgregarInfo("✗ Archivo de categorías no encontrado\r\n\r\n");
                }

                // Paso 3: Migrar Artículos
                AgregarInfo("• Buscando archivo de artículos...\r\n");
                if (File.Exists(rutaArticulosArchivo))
                {
                    AgregarInfo("✓ Archivo encontrado\r\n");
                    AgregarInfo("• Transfiriendo datos de artículos...\r\n");
                    int registrosArt = MigrarArticulos();
                    AgregarInfo($"✓ {registrosArt} artículos migrados\r\n\r\n");
                }
                else
                {
                    AgregarInfo("✗ Archivo de artículos no encontrado\r\n\r\n");
                }

                // Resumen final
                AgregarInfo("════════════════════════════════════════\r\n");
                AgregarInfo("  ✓ MIGRACIÓN COMPLETADA\r\n");
                AgregarInfo("════════════════════════════════════════\r\n");

                MessageBox.Show("Migración completada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                AgregarInfo($"\n✗ Error: {ex.Message}\r\n");
                AgregarInfo($"\n📌 IMPORTANTE:\r\n");
                AgregarInfo($"Debes instalar Microsoft Access Database Engine\r\n");
                AgregarInfo($"\nDescárgalo desde:\r\n");
                AgregarInfo($"https://www.microsoft.com/download/details.aspx?id=13255\r\n");
                MessageBox.Show($"Error durante la migración: {ex.Message}\n\nInstala Microsoft Access Database Engine desde:\nhttps://www.microsoft.com/download/details.aspx?id=13255", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int MigrarCategorias()
        {
            using (OleDbConnection conexion = conexionBD.ObtenerConexion())
            {
                try
                {
                    LimpiarTabla(conexion, "Categorias");

                    string[] lineas = File.ReadAllLines(rutaCategoriasArchivo, Encoding.UTF8);

                    int registrosInsertados = 0;

                    foreach (string linea in lineas)
                    {
                        if (string.IsNullOrWhiteSpace(linea))
                            continue;

                        try
                        {
                            string[] partes = linea.Split('|');
                            if (partes.Length >= 2)
                            {
                                string idCategoria = partes[0].Trim();
                                string nombreCategoria = partes[1].Trim();

                                InsertarCategoria(conexion, idCategoria, nombreCategoria);
                                registrosInsertados++;
                            }
                        }
                        catch (Exception ex)
                        {
                            AgregarInfo($"Advertencia: Error procesando línea - {ex.Message}\r\n");
                        }
                    }

                    return registrosInsertados;
                }
                catch (Exception ex)
                {
                    AgregarInfo($"✗ Error al migrar categorías: {ex.Message}\r\n");
                    throw;
                }
            }
        }

        private int MigrarArticulos()
        {
            using (OleDbConnection conexion = conexionBD.ObtenerConexion())
            {
                try
                {
                    LimpiarTabla(conexion, "Articulos");

                    string[] lineas = File.ReadAllLines(rutaArticulosArchivo, Encoding.UTF8);

                    int registrosInsertados = 0;

                    foreach (string linea in lineas)
                    {
                        if (string.IsNullOrWhiteSpace(linea))
                            continue;

                        try
                        {
                            string[] partes = linea.Split('|');
                            if (partes.Length >= 4)
                            {
                                string idArticulo = partes[0].Trim();
                                string nombreArticulo = partes[1].Trim();
                                string idCategoria = partes[2].Trim();
                                string precio = partes[3].Trim();

                                InsertarArticulo(conexion, idArticulo, nombreArticulo, idCategoria, precio);
                                registrosInsertados++;
                            }
                        }
                        catch (Exception ex)
                        {
                            AgregarInfo($"Advertencia: Error procesando línea - {ex.Message}\r\n");
                        }
                    }

                    return registrosInsertados;
                }
                catch (Exception ex)
                {
                    AgregarInfo($"✗ Error al migrar artículos: {ex.Message}\r\n");
                    throw;
                }
            }
        }

        private void InsertarCategoria(OleDbConnection conexion, string idCategoria, string Nombre)
        {
            string consulta = "INSERT INTO Categorias (idCategoria, Nombre) VALUES (?, ?)";

            using (OleDbCommand comando = new OleDbCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@idCategoria", idCategoria);
                comando.Parameters.AddWithValue("@Nombre", Nombre);
                comando.ExecuteNonQuery();
            }
        }

        private void InsertarArticulo(OleDbConnection conexion, string idArticulo, string nombreArticulo, string idCategoria, string precio)
        {
            string consulta = "INSERT INTO Articulos (idArticulo, Nombre, idCategoria, precio) VALUES (?, ?, ?, ?)";

            using (OleDbCommand comando = new OleDbCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@idArticulo", idArticulo);
                comando.Parameters.AddWithValue("@Nombre", nombreArticulo);
                comando.Parameters.AddWithValue("@idCategoria", idCategoria);
                comando.Parameters.AddWithValue("@precio", Convert.ToDecimal(precio));
                comando.ExecuteNonQuery();
            }
        }

        private void LimpiarTabla(OleDbConnection conexion, string nombreTabla)
        {
            try
            {
                string consulta = $"DELETE FROM {nombreTabla}";
                using (OleDbCommand comando = new OleDbCommand(consulta, conexion))
                {
                    comando.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        private void AgregarInfo(string mensaje)
        {
            txtInfo.AppendText(mensaje);
        }

        private void btnMigra_Click_1(object sender, EventArgs e)
        {

        }
    }
}
