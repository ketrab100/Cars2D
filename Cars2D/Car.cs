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
        double mutationRate;
        public float fitness;
        int lifeTime;
        int time=0;
        List<Vector2> dna = new List<Vector2>();
        public Vector2 target=new Vector2(0,0);
        public Vector2 position = new Vector2(0,0);
        Vector2 velocity = new Vector2();
        Vector2 accerelation = new Vector2();
        Vector2 force = new Vector2();
        static Random random = new Random();
        private static readonly object syncLock = new object();
        float xmax, ymax, xmin, ymin;

        public Car(float x,float y,float targetx, float targety,int lifeTime)
        {
            xmax = 490;
            ymax = 490;
            xmin = 0;
            ymin = 0;
            mutationRate = 0.001;
            this.lifeTime = lifeTime;
            this.target = new Vector2(targetx,targety);
            position.X = 250;
            position.Y = 250;
            RandomInitialization();
        }
        public Car(Car c)
        {

            this.mutationRate = c.mutationRate;
            this.fitness = c.fitness;
            this.lifeTime = c.lifeTime;
            this.dna = c.dna;
            this.target = c.target;
            this.xmax = c.xmax;
            this.xmin = c.xmin;
            this.ymax = c.ymax;
            this.ymin = c.ymin;

        }

        public void update()
        {
            if(position.X<=xmax && position.X >= xmin && position.Y <= ymax && position.Y >= ymin)
            {
                force = dna[time];
                position = Vector2.Add(position, velocity);
                velocity =  Vector2.Add(velocity, accerelation);
                accerelation = Vector2.Add(accerelation, force);
                /*
                position.X = Math.Min(position.X , 490);
                position.Y = Math.Min(position.Y , 490);
                position.X = Math.Max(position.X, 0);
                position.Y = Math.Max(position.Y, 0);
                */
               
            }
            time++;

        }
        public void calcDistance()
        {
            double distance = 0;
            Vector2 w = new Vector2(position.X-target.X, position.Y-target.Y);
            distance = Math.Sqrt(Math.Pow(w.X, 2) + Math.Pow(w.Y, 2));
            fitness = 1/((float)(distance+0.01));
            fitness =(float) Math.Pow(fitness, 0.5);
            fitness = (float)Math.Sqrt(fitness);
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
            Car child= new Car(250,480,target.X,target.Y,lifeTime);
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

        public void mutation()
        {
            for (int i = 0; i < lifeTime; i++)
            {
                
                double a = random.NextDouble();
                if (a<mutationRate)
                {
                    float x = (float)random.NextDouble() / 100;
                    float y = (float)random.NextDouble() / 100;
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
