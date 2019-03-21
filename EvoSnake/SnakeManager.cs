using System;
using System.Threading.Tasks;

namespace EvoSnake
{
    public class SnakeManager
    {
        private bool isAnyAlive;

        public Snake Best { get; private set; } = null;

        public Snake[] Snakes { get; private set; }

        public int Generation { get; private set; } = 0;

        public ulong Record { get; private set; } = 0ul;

        private ulong fitness;

        public ulong Fitness { get { return fitness / (ulong)Settings.population; } }

        public SnakeManager()
        {
            Snakes = new Snake[Settings.population];

            for (int i = 0; i < Settings.population; i++)
                Snakes[i] = new Snake();

            isAnyAlive = true;
        }

        public void Update()
        {
            Generation++;

            do
            {
                isAnyAlive = false;

                for (int i = 0; i < Settings.population; ++i)
                    if (Snakes[i].Alive)
                    {
                        isAnyAlive = true;
                        Snakes[i].Step();
                    }
            } while (isAnyAlive);

            SelectBest();

            NaturalSelection();
        }

        void SelectBest()
        {
            int theBest = 0;

            for (int i = 1; i < Settings.population; i++)
                if (Snakes[theBest].Fitness < Snakes[i].Fitness)
                    theBest = i;

            if (Record < Snakes[theBest].Fitness)
                Record = Snakes[theBest].Fitness;

            Best = new Snake(Snakes[theBest]);
        }

        void NaturalSelection()
        {
            fitness = 0;

            Snake[] SnakesTmp;

            for (int i = 0; i < Settings.population; i++)
                fitness += Snakes[i].Fitness;


            SnakesTmp = new Snake[Settings.population];
            SnakesTmp[0] = new Snake(Best);

            for (int j = 1; j < Settings.population; j++)
            {
                if (Settings.R.Next(0, 10) == 0)
                    SnakesTmp[j] = new Snake();
                else
                {
                    SnakesTmp[j] = new Snake(GetSnake(Snakes, fitness), GetSnake(Snakes, fitness));
                    SnakesTmp[j].mutate();
                }
            }

            Array.Copy(SnakesTmp, Snakes, Settings.population);
        }

        Snake GetSnake(Snake[] Snakes, ulong fitnessSum)
        {
            do
            {
                int
                    r = Settings.R.Next(0, (int)fitnessSum),
                    n = Settings.R.Next(0, Settings.population);

                if (Snakes[n].Fitness > (ulong)(r * 2))
                    return Snakes[n];
            } while (true);
        }
    }
}