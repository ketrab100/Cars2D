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
        static Target target = new Target(250,0);
        float targetX=0;
        float targetY=0;
        int popSize=50;
        private void panel1_Click(object sender, EventArgs e)
        {

        }
        Population population;
        private void go ()
        {
            population = new Population(popSize, targetX, targetY, 250, 250);
            while (true)
            {
                SolidBrush sbr = new SolidBrush(Color.Red);
                SolidBrush sb = new SolidBrush(Color.FromArgb(100, Color.Black));
                Graphics g = panel1.CreateGraphics();

                population.naturalSelection();
                population.generate();
                for (int i = 0; i < 200; i++)
                {
                    //Thread.Sleep(10);

                    g.Clear(Color.White);
                    foreach (Car c in population.population)
                    {

                        g.FillEllipse(sbr, targetX, targetY, 10, 10);
                        var r = c.draw();
                        g.FillEllipse(sb, r);
                        c.update();
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
                //Thread.Sleep(50);
            }

        }

        private delegate void Mydelegate();
        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g = panel1.CreateGraphics();
            g.Clear(Color.White);
            if (targetXTextBox.Text == "")
            {
                targetX = 10;
            }
            else
            {
                targetX = (float)Convert.ToDouble(targetXTextBox.Text);
            }
            
            if (targetYTextBox.Text == "")
            {
                targetY = 10;
            }
            else
            {
                targetY = (float)Convert.ToDouble(targetYTextBox.Text);
            }
            if (popSizeTextBox.Text == "")
            {
                popSize = 50;
            }
            else
            {
                popSize = Convert.ToInt32(popSizeTextBox.Text);
            }

            SolidBrush sb = new SolidBrush(Color.Red);
            g.FillEllipse(sb,targetX,targetY,10,10);
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
