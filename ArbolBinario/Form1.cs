using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArbolBinario
{
    public partial class Form1 : Form
    {
        ArbolBinarioBusqueda arbol = new ArbolBinarioBusqueda();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtValor.Text, out int v))
            {
                arbol.Insertar(v);
                DibujarArbol();
                MessageBox.Show("Valor insertado correctamente.");
            }
            else
            {
                MessageBox.Show("Ingrese un número válido.");
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtValor.Text, out int v))
            {
                bool encontrado = arbol.Buscar(v);
                MessageBox.Show(encontrado ? "Valor encontrado." : "Valor NO encontrado.");
            }
        }

        private void btnInOrden_Click(object sender, EventArgs e)
        {
            lstResultado.Items.Clear();
            List<int> lista = new List<int>();
            arbol.InOrden(arbol.Raiz, lista);

            foreach (var n in lista)
                lstResultado.Items.Add(n);
        }

        private void btnNiveles_Click(object sender, EventArgs e)
        {
            lstResultado.Items.Clear();
            var niveles = arbol.PorNiveles();

            foreach (var n in niveles)
                lstResultado.Items.Add(n);
        }

        private void btnHojas_Click(object sender, EventArgs e)
        {
            int hojas = arbol.ContarHojas(arbol.Raiz);
            MessageBox.Show("Número de nodos hoja: " + hojas);
        }

        private void btnAltura_Click(object sender, EventArgs e)
        {
            int h = arbol.Altura(arbol.Raiz);
            MessageBox.Show("Altura del árbol: " + h);
        }
        private void DibujarNodo(Graphics g, Nodo nodo, int x, int y, int separacion)
        {
            if (nodo == null)
                return;

            Pen pen = new Pen(Color.Black, 2);

            // 🎨 COLORES
            Brush fillRaiz = Brushes.Gold;              // 💛 raíz
            Brush fillHoja = Brushes.LightGreen;        // 💙 hojas
            Brush fillNormal = Brushes.LightBlue;       // nodos intermedios
            Brush textBrush = Brushes.Black;

            int radio = 30;

            // DIBUJAR RAMAS
            if (nodo.Izquierdo != null)
            {
                g.DrawLine(pen, x, y, x - separacion, y + 80);
                DibujarNodo(g, nodo.Izquierdo, x - separacion, y + 80, separacion / 2);
            }

            if (nodo.Derecho != null)
            {
                g.DrawLine(pen, x, y, x + separacion, y + 80);
                DibujarNodo(g, nodo.Derecho, x + separacion, y + 80, separacion / 2);
            }

            // ---------- 🎨 SELECCIÓN DEL COLOR DEL NODO ----------
            Brush colorNodo;

            if (nodo == arbol.Raiz)
                colorNodo = fillRaiz;                          // 💛 raíz
            else if (nodo.Izquierdo == null && nodo.Derecho == null)
                colorNodo = fillHoja;                          // 💙 hoja
            else
                colorNodo = fillNormal;                        // nodo normal

            // DIBUJAR NODO (CÍRCULO)
            g.FillEllipse(colorNodo, x - radio, y - radio, radio * 2, radio * 2);
            g.DrawEllipse(pen, x - radio, y - radio, radio * 2, radio * 2);

            // TEXTO CENTRADO
            StringFormat formato = new StringFormat();
            formato.Alignment = StringAlignment.Center;
            formato.LineAlignment = StringAlignment.Center;
            g.DrawString(nodo.Valor.ToString(), new Font("Arial", 12, FontStyle.Bold), textBrush, x, y, formato);
        }

        private void DibujarArbol()
        {
            Bitmap bmp = new Bitmap(pictureBoxArbol.Width, pictureBoxArbol.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);

                if (arbol.Raiz != null)
                {
                    int separacionInicial = pictureBoxArbol.Width / 4;
                    DibujarNodo(g, arbol.Raiz, pictureBoxArbol.Width / 2, 50, separacionInicial);
                }
            }

            pictureBoxArbol.Image = bmp;
        }
    }
}
