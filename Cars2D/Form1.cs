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
        Car car = new Car(100, 100);
        private void panel1_Click(object sender, EventArgs e)
        {
            Pen pen = new Pen(Color.Green);
            SolidBrush sb = new SolidBrush(Color.Black);
            Graphics g = panel1.CreateGraphics();

            for (int i=0;i<100;i++)
            {
                var r = car.draw();
                g.FillEllipse(sb,r);
                Thread.Sleep(50);
                car.update();
                g.Clear(Color.White);
            }
            var ra = car.draw();
            g.FillEllipse(sb, ra);

        }
    }
}
