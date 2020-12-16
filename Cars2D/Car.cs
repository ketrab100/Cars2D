using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace Cars2D
{
    class Car
    {
        public Vector2 position = new Vector2();
        Vector2 velocity = new Vector2(2,2);
        Vector2 accerelation = new Vector2();
        Vector2 force = new Vector2();
        public Car(int x, int y)
        {
            position.X = x;
            position.Y = y;
        }

        public void update()
        {
            position = Vector2.Add(position, velocity);
            velocity =  Vector2.Add(velocity, accerelation);
            accerelation = Vector2.Add(accerelation, force);
        }

        public Rectangle draw()
        {
            Rectangle r = new Rectangle((int)position.X,(int) position.Y, 10, 10);
            return r;
        }
    }
}
