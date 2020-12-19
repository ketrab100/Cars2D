using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cars2D
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        float targetX=250;
        float targetY=10;
        int popSize=100;
        int lifeTime = 200;
        float starty = 115;
        float startx = 115;
        RectangleF collider;
        float cx,cy,ch,cw;
        private void panel1_Click(object sender, EventArgs e)
        {

        }
        Population1 population;
        private void go ()
        {
            List<RectangleF> tor = new List<RectangleF>();
            tor.Add(new RectangleF(100, 100, 200, 50));
            tor.Add(new RectangleF(100, 100, 50, 200));
            tor.Add(new RectangleF(250, 100, 50, 200));
            tor.Add(new RectangleF(100, 250, 200, 50));
            cx = 150;
            cy = 200;
            cw = 200;
            ch = 20;
            collider = new RectangleF(cx, cy, cw, ch);
            SolidBrush sbr = new SolidBrush(Color.Red);
            SolidBrush sb = new SolidBrush(Color.FromArgb(100, Color.Black));
            Graphics g = panel1.CreateGraphics();
            population = new Population1(startx,starty,targetX,targetY,popSize,lifeTime,tor);
            while (true)
            {


                population.naturalSelection();
                population.generate();
                for (int i = 0; i <lifeTime; i++)
                {
                    g.Clear(Color.White);
                    sbr.Color = Color.Blue;
                    foreach (RectangleF re in tor)
                    {
                        g.FillRectangle(sbr, re);
                    }
                    foreach (Car c in population.population)
                    {
                        
                        sbr.Color = Color.Red;
                        g.FillEllipse(sbr, targetX, targetY, 10, 10);
                        var r = c.draw();
                        g.FillEllipse(sb, r);
                        c.move();


                        /*
                        sbr.Color = Color.Blue;
                        g.FillRectangle(sbr, cx, cy, cw, ch);
                        */
                    }
                }
            }
        }
        public  void showFitness()
        {
            while (true)
            {
                maxFitnessTextBox.Text = population.bestfitness.ToString();
                maxFitnessTextBox.Focus();
                Thread.Sleep(500);
            }

        }

        private delegate void Mydelegate();
        private void button1_Click(object sender, EventArgs e)
        {
            cx = 150;
            cy = 200;
            cw = 200;
            ch = 20;
            collider = new RectangleF(cx, cy, cw, ch);

            Graphics g = panel1.CreateGraphics();
            g.Clear(Color.White);
            if (targetXTextBox.Text == "")
            {
                //targetX = 10;
            }
            else
            {
                targetX = (float)Convert.ToDouble(targetXTextBox.Text);
            }
            
            if (targetYTextBox.Text == "")
            {
                //targetY = 10;
            }
            else
            {
                targetY = (float)Convert.ToDouble(targetYTextBox.Text);
            }
            if (popSizeTextBox.Text == "")
            {
                //popSize = 50;
            }
            else
            {
                popSize = Convert.ToInt32(popSizeTextBox.Text);
            }

            SolidBrush sb = new SolidBrush(Color.Red);
            g.FillEllipse(sb,targetX,targetY,10,10);
            sb.Color = Color.Blue;
            g.FillRectangle(sb, cx, cy, cw ,ch);
        }
        Thread t;
        private void button2_Click(object sender, EventArgs e)
        {
            t = new Thread(go);
            t.Start();
        }

        private void Stopbutton_Click(object sender, EventArgs e)
        {
            t.Abort();
        }
    }
}
