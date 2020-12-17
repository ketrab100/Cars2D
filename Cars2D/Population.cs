using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Cars2D
{
    class Population
    {
        Random random = new Random();
        public List<Car> population = new List<Car>();
        Vector2 target ;
        List<Car> matingPool = new List<Car>();
        float bestfitness = 0;
        Car best;

        int quantity;
        public Population(int quantity,float targetx,float targety,int startx,int starty)
        {
            this.quantity = quantity;
            target.X = targetx;
            target.Y = targety;

            for (int i = 0; i < quantity; i++)
            {
                population.Add(new Car(startx, starty,targetx,targety,200));
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
                    best = new Car(c);
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
            for (int i = 0; i < (best.fitness * 1000); i++)
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
                child.mutation();
                population[i] = child;

            }
        }
        


    }
}
