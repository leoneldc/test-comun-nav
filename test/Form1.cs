using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{
    public partial class Form1 : Form
    {
        Conexion conexion = new Conexion();
        Controlador controlador = new Controlador();

        public Form1()
        {
            InitializeComponent();
            cargar();
        }

        void cargar()
        {
            string sql = "SELECT * FROM "+dataGridView1.Tag +";";
            OdbcDataAdapter dataTable = new OdbcDataAdapter(sql, conexion.conexion());
            DataTable table = new DataTable();
            dataTable.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dictionary<string, List<string>> valoresPorTagTabla = new Dictionary<string, List<string>>();
            Dictionary<string, List<string>> valoresPorTagColumnas = new Dictionary<string, List<string>>();

            foreach (Control control in this.Controls)
            {
                if (control is TextBox textBox && textBox.Tag != null)
                {
                    string[] datosTextBox = textBox.Tag.ToString().Split(',');
                    string tabla = datosTextBox[0];
                    string columna = datosTextBox[1];
                    string condicion = datosTextBox.Length == 3 ? datosTextBox[2] : "";
                    string valor = textBox.Text;
                    if (!valoresPorTagTabla.ContainsKey(tabla))
                    {
                        valoresPorTagTabla[tabla] = new List<string>();
                        valoresPorTagColumnas[tabla] = new List<string>();
                    }
                    valoresPorTagTabla[tabla].Add(columna);
                    valoresPorTagColumnas[tabla].Add("\'"+ valor+"\'");
                }
            }
            controlador.Guardar(valoresPorTagTabla, valoresPorTagColumnas);

            cargar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dictionary<string, List<string>> valoresPorTagTabla = new Dictionary<string, List<string>>();
            Dictionary<string, List<string>> valoresPorTagColumnas = new Dictionary<string, List<string>>();
            Dictionary<string, List<string>> valoresPorTagCondicion = new Dictionary<string, List<string>>();

            TextBox id2 = new TextBox();
            id2.Text = txt_id.Text;
            id2.Tag = "test2,dpi,primary";

            TextBox[] arreglo = { txt_id, nombre, id2, textBox1 };


            foreach (TextBox textBox in arreglo)
            {
                string[] datosTextBox = textBox.Tag.ToString().Split(',');
                string tabla = datosTextBox[0];
                string columna = datosTextBox[1];
                string valor = textBox.Text;
                if (!valoresPorTagTabla.ContainsKey(tabla))
                {
                    valoresPorTagTabla[tabla] = new List<string>();
                    valoresPorTagColumnas[tabla] = new List<string>();
                    valoresPorTagCondicion[tabla] = new List<string>();
                }
                valoresPorTagTabla[tabla].Add(columna);
                valoresPorTagColumnas[tabla].Add("\'" + valor + "\'");
                if (textBox.Tag.ToString().Contains("primary"))
                {
                    valoresPorTagCondicion[tabla].Add(columna);
                }
            }
            controlador.Actualizar(valoresPorTagTabla, valoresPorTagColumnas, valoresPorTagCondicion);

            cargar();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Dictionary<string, List<string>> valoresPorTagTabla = new Dictionary<string, List<string>>();
            Dictionary<string, List<string>> valoresPorTagColumnas = new Dictionary<string, List<string>>();
            Dictionary<string, List<string>> valoresPorTagCondicion = new Dictionary<string, List<string>>();


            TextBox id2 = new TextBox();
            id2.Text = txt_id.Text;
            id2.Tag = "test2,dpi,primary";

            TextBox[] arreglo = { txt_id, nombre, id2, textBox1 };

            foreach (TextBox textBox in arreglo)
            {
                string[] datosTextBox = textBox.Tag.ToString().Split(',');
                string tabla = datosTextBox[0];
                string columna = datosTextBox[1];
                string valor = textBox.Text;
                if (!valoresPorTagTabla.ContainsKey(tabla))
                {
                    valoresPorTagTabla[tabla] = new List<string>();
                    valoresPorTagColumnas[tabla] = new List<string>();
                    valoresPorTagCondicion[tabla] = new List<string>();
                }
                valoresPorTagTabla[tabla].Add(columna);
                valoresPorTagColumnas[tabla].Add("\'" + valor + "\'");
                if (textBox.Tag.ToString().Contains("primary"))
                {
                    valoresPorTagCondicion[tabla].Add(columna);
                }
            }
            controlador.Eliminar(valoresPorTagTabla, valoresPorTagColumnas, valoresPorTagCondicion);
            cargar();
        }
    }
}
