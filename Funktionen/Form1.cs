using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Funktionen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Graphics g;
        Pen p = new Pen(Color.Black, 1);
        Font f = new Font("Comic Sans MS", 8);

        private void endeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void DrawCoordinateSystem()
        {
            g = CreateGraphics();
            g.DrawLine(p, 50, 300, 550, 300);
            g.DrawLine(p, 300, 50, 300, 550);

            //Markierungen für Einheiten zeichnen
            int a = 50;
            int b = -4;
            int bb = 4;
            for (int i = 0; i < 9; i++)
            {
                a += 50;
                g.DrawLine(p, a, 305, a, 295);
                g.DrawLine(p, 305, a, 295, a);

                if (b != 0)
                {
                    g.DrawString(b.ToString(), f, Brushes.Black, a - 5, 305);
                    g.DrawString(bb.ToString(), f, Brushes.Black, 305, a - 9);
                }
                b += 1;
                bb -= 1;
            }
        }
        private void DrawFunctionGraph(float x1, float y1, float x2 ,float y2)
        {
            float m = (y2 - y1) / (x2 - x1);
            float n = y1 - (x1 * m);

            float X1 = -250;
            float Y1 = X1 * m + n * 50;
            float X2 = 250;
            float Y2 = X2*m+n*50;
            
            Graphics g;
            Pen p = new Pen(Color.Red, 3);

            do
            {
                Y1 = X1 * m + n * 50;
                X1 += 1;
            }
            while (Y1 >= 250 || Y1 <= -250);

            do
            {
                Y2 = X2 * m + n * 50;
                X2 -= 1;
            }
            while (Y2 >= 250 || Y2 <= -250);

            g = CreateGraphics();
            g.TranslateTransform(300, 300);
            g.DrawLine(p, X1, -1*Y1, X2, -1*Y2);
        }
        private void CalcFunction(Double x1, Double y1, Double x2, Double y2)
        {
            Double m, n, x0;

            m = (y2 - y1) / (x2 - x1);
            n = y1 - x1 * m;
            x0 = -n / m;

            if (y1 == y2)
            {
                lblFktGl.Text = "f(x)= " + Math.Round(Convert.ToDouble(txtbxY1.Text), 2).ToString();
                lblSy.Text = "Sy(0 / " + Math.Round(Convert.ToDouble(txtbxY1.Text), 2).ToString() + " )";
                lblSx.Text = "-";
                lblM.Text = "m = -";
            }
            else if (x1 == x2)
            {
            }
            else
            {
                lblFktGl.Text = "f(x)= " + Math.Round(m, 2).ToString() + "x + " + Math.Round(n, 2).ToString();
                lblSy.Text = "Sy(0 / " + Math.Round(n, 2).ToString() + " )";
                lblSx.Text = "Sx( " + Math.Round(x0, 2).ToString() + " / 0 )";
                lblM.Text = "m = " + Math.Round(m, 2).ToString();
            }
        }
        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawCoordinateSystem(); //Koorddinatensystem zeichnen
            CalcFunction(Convert.ToDouble(txtbxX1.Text), Convert.ToDouble(txtbxY1.Text), Convert.ToDouble(txtbxX2.Text), Convert.ToDouble(txtbxY2.Text));
            //Sx, Sy, Anstieg und Funktionsgleichung berechnen
            Thread.Sleep(500);

            if (float.Parse(txtbxX1.Text) == float.Parse(txtbxX2.Text)) // Fehlermneldung wenn beide Punkte dasselbe X haben
            {
                if (float.Parse(txtbxY1.Text) == float.Parse(txtbxY2.Text)){
                    MessageBox.Show("Die Punkte dürfen nicht identisch sein", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }   //Fehlermeldung wenn Punkte identisch sind
                else{
                    MessageBox.Show("Die Punkte müssen verschiedene X-Werte haben", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } }  // Fehlermneldung wenn beide Punkte dasselbe X haben
            else
            {
                DrawFunctionGraph(float.Parse(txtbxX1.Text), float.Parse(txtbxY1.Text), float.Parse(txtbxX2.Text), float.Parse(txtbxY2.Text));
            }   //Funktion zeichnen [-4,5; 4,5]
        }
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            g.Clear(BackColor);
        }
    }
}