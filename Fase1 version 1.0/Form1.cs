using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fase1_version_1._0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //variables globales
        string causaerror, texto, ruta = "";
        Analizador analize;

        private void btanalizar_Click(object sender, EventArgs e)
        {
            analize = new Analizador();
            if (TextoArchivo.Text != "")
            {
                TextoArchivo.SelectAll();
                texto = TextoArchivo.Text.ToLower();
                textoerror.Text = "";
                textoerror.Text = "";
                textoerror.BackColor = Color.Black;
                analize.inicio(texto);
                if (analize.causaerror == "")
                {
                    textoerror.BackColor = Color.Green;
                    textoerror.Text = "Archivo Correcto";
                }
                else
                {
                    textoerror.BackColor = Color.Red;
                    this.causaerror = analize.causaerror;
                    textoerror.Text = analize.causaerror;
                }
            }
            else
            {
                MessageBox.Show("Ingrese un archivo ");
            }
        }

        private void btsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btExaminar_Click(object sender, EventArgs e)
        {
            TextoArchivo.Text = "";
            String aux;
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Filter = "Text files (*.txt)|*.txt";
            abrir.ShowDialog();
            ruta = abrir.FileName;
            if (ruta != "")
            {
                TextoArchivo.Text = "";
                btanalizar.Enabled = true;
                StreamReader leer = new StreamReader(ruta, Encoding.Default);
                while ((aux = leer.ReadLine()) != null)
                {
                    TextoArchivo.Text += aux + "\n";
                }
                TextoArchivo.Text = TextoArchivo.Text.Substring(0, TextoArchivo.Text.Length - 1);
                leer.Close();
            }
        }
    }
}
