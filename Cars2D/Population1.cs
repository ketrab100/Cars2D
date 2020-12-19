using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;

namespace Cars2D
{
    class Population1
    {
        float mutationRate = 0.05f;
        Random random = new Random();
        public List<Car> population = new List<Car>();
        List<Car> matingPool = new List<Car>();
        public float bestfitness = 0;
        Car best;

        int quantity;
        public Population1(float startx, float starty, float targetx, float targety, int quantity, int lifeTime, List<RectangleF> tor)
        {

            this.quantity = quantity;

            for (int i = 0; i < quantity; i++)
            {
                population.Add(new Car(startx, starty, targetx, targety, lifeTime, tor));
            }
        }

        public void calcFitness()
        {
            foreach (Car c in population)
            {
                //c.calcDistance();
                if (c.distanece >= bestfitness)
                {
                    bestfitness = c.distanece;
                    best = c;
                    Console.WriteLine(bestfitness);
                }
            }

        }

        public void naturalSelection()
        {
            matingPool.Clear();
            calcFitness();
            foreach (Car c in population)
            {
                for (int i = 0; i < (c.distanece * 100)+1; i++)
                {
                    matingPool.Add(c);
                }
            }
            for (int i = 0; i < (best.fitness * 1000)+1; i++)
            {
                matingPool.Add(best);
            }
        }
        public void generate()
        {
            for (int i = 0; i < quantity; i++)
            {
                int a = random.Next() % matingPool.Count;
                int b = random.Next() % matingPool.Count;
                Car parent1 = matingPool[a];
                Car parent2 = matingPool[b];
                Car child = parent1.CrossOver(parent2);
                child.mutation(mutationRate);
                population[i] = child;

            }
        }


    }
}
