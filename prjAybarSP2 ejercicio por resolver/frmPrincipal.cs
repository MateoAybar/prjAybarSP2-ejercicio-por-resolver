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





        private void Form1_Load(object sender, EventArgs e) 
        { 
        }
        private clsMigracion objMigrador;

        public frmPrincipal()
        {
            InitializeComponent();
            objMigrador = new clsMigracion();

        }

        private void btnMigra_Click(object sender, EventArgs e)
        {
            RealizarMigracion();
        }

        private void RealizarMigracion()
        {
            txtInfo.Clear();
            Log("════════════ INICIANDO MIGRACIÓN ════════════\r\n");

            try
            {
                // Paso 1: Validar BD
                Log("• Verificando BD...");
                if (!File.Exists(objMigrador.RutaBD)) { Log(" ✗ No encontrada\r\n"); return; }
                Log(" ✓ OK\r\n");

                // Paso 2: Categorías
                Log("• Migrando Categorías...");
                if (File.Exists(objMigrador.RutaCategorias))
                {
                    int cant = objMigrador.MigrarCategorias();
                    Log($" ✓ {cant} registradas\r\n");
                }
                else { Log(" ✗ Archivo no encontrado\r\n"); }

                // Paso 3: Artículos
                Log("• Migrando Artículos...");
                if (File.Exists(objMigrador.RutaArticulos))
                {
                    int cant = objMigrador.MigrarArticulos();
                    Log($" ✓ {cant} registrados\r\n");
                }
                else { Log(" ✗ Archivo no encontrado\r\n"); }

                Log("\r\n════════════ MIGRACIÓN EXITOSA ════════════");
                MessageBox.Show("¡Proceso terminado!", "Éxito");
            }
            catch (Exception ex)
            {
                Log($"\r\n✗ ERROR: {ex.Message}");
                MessageBox.Show("Error crítico: " + ex.Message);
            }
        }

        private void Log(string msj)
        {
            txtInfo.AppendText(msj + "\r\n");
        }


        private void txtInfo_TextChanged(object sender, EventArgs e)
        {
            




        }
        private void lblInfo_Click(object sender, EventArgs e)
        {
           

        }

    }
}