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
        bool crashed;
        bool onTarget;
        float startx, starty;
        float xmax, ymax, xmin, ymin;
        public float fitness;
        int lifeTime;
        int time=0;
        RectangleF collider;
        List<Vector2> dna = new List<Vector2>();
        public Vector2 target=new Vector2(0,0);
        public Vector2 position = new Vector2(0,0);
        public Vector2 velocity = new Vector2();
        public Vector2 accerelation = new Vector2();
        public Vector2 force = new Vector2();

        static Random random = new Random();
        private static readonly object syncLock = new object();
        

        public Car(float startx,float starty,float targetx, float targety,int lifeTime,RectangleF collider)
        {
            crashed = false;
            xmax = 490;
            ymax = 490;
            xmin = 0;
            ymin = 0;
            this.collider = collider;
            this.lifeTime = lifeTime;
            this.target = new Vector2(targetx,targety);
            this.startx = startx;
            this.starty = starty;
            position.X = startx;
            position.Y = starty;
            RandomInitialization();
        }

        public void update()
        {
            if (position.X > xmax || position.X < xmin || position.Y > ymax || position.Y < ymin)
            {
                crashed = true;
            }
            if (collider.Contains(position.X, position.Y))
            {
                crashed = true;
            }
            if(!crashed)
            {
                force = dna[time];
                position = Vector2.Add(position, velocity);
                velocity =  Vector2.Add(velocity, accerelation);
                accerelation = Vector2.Add(accerelation, force);             
            }
            time++;
        }
        public void calcDistance()
        {
            double distance = 0;
            Vector2 w = new Vector2(position.X-target.X, position.Y-target.Y);
            distance = Math.Sqrt(Math.Pow(w.X, 2) + Math.Pow(w.Y, 2));
            if (distance <= 2)
            {
                fitness = 1f*10;
                onTarget = true;
            }
            else
            {
                if (crashed)
                {
                    fitness = 1 / ((float)(distance));
                    fitness = (float)Math.Sqrt(fitness)/10;
                }
                else
                {
                    fitness = 1 / ((float)(distance));
                    fitness = (float)Math.Sqrt(fitness);
                }

            }

        }
        public Rectangle draw()
        {
            Rectangle r = new Rectangle((int)position.X,(int) position.Y, 10, 10);
            return r;
        }
        public static int RandomNumber(int min, int max)
        {
            lock (syncLock)
            {
                return random.Next(min, max);
            }
        }
        public void RandomInitialization()
        {
            for (int i = 0; i < lifeTime; i++)
            {
                float x = (float)random.NextDouble()/500;
                float y = (float)random.NextDouble()/500;
                double a = random.NextDouble();
                double b = random.NextDouble();
                if (a < 0.5)
                {
                    x *= -1;
                }
                if (b < 0.5)
                {
                    y *= -1;
                }
                dna.Add(new Vector2(x, y));
            }
        }

        public Car CrossOver(Car parent)
        {
            Car child= new Car(this.startx,this.starty,target.X,target.Y,lifeTime,collider);
            int mid = RandomNumber(1, lifeTime - 1);
            for (int i = 0;i < lifeTime;i++)
            {
                if (i > mid)
                {
                    child.dna[i] = this.dna[i];
                }
                else
                {
                    child.dna[i] = parent.dna[i];
                }
            }
            return child;
        }

        public void mutation(float mutationRate)
        {
            for (int i = 0; i < lifeTime; i++)
            {
                
                double a = random.NextDouble();
                if (a<mutationRate)
                {
                    float x = (float)random.NextDouble() / 500;
                    float y = (float)random.NextDouble() / 500;
                    double c = random.NextDouble();
                    double b = random.NextDouble();
                    if (c < 0.5)
                    {
                        x *= -1;
                    }
                    if (b < 0.5)
                    {
                        y *= -1;
                    }
                    dna[i] = (new Vector2(x, y));
                }
            }
        }
    }
}
