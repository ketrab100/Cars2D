using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;

namespace Cars2D
{
    class Population
    {
        float mutationRate = 0.01f;
        Random random = new Random();
        public List<Car> population = new List<Car>();
        List<Car> matingPool = new List<Car>();
        public float bestfitness = 0;
        Car best;

        int quantity;
        public Population(float startx, float starty, float targetx,float targety,int quantity,int lifeTime,RectangleF collider)
        {
            
            this.quantity = quantity;

            for (int i = 0; i < quantity; i++)
            {
                population.Add(new Car(startx, starty,targetx,targety,lifeTime,collider));
            }
        }

        public void calcFitness()
        {
            foreach(Car c in population)
            {
                c.calcDistance();
                if (c.fitness >= bestfitness)
                {
                    bestfitness = c.fitness;
                    best = c;
                    Console.WriteLine(bestfitness);
                }
            }
            
        }

        public void naturalSelection()
        {
            matingPool.Clear();
            calcFitness();
            foreach(Car c in population)
            {
                for(int i = 0; i <(c.fitness*100); i++)
                {
                    matingPool.Add(c);
                }
            }
            for (int i=0;i<best.fitness*100;i++)
            {
                matingPool.Add(best);
            }
        }
        public void generate()
        {
            for(int i=0;i<quantity;i++)
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
