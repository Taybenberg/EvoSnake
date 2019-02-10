using System;
using System.Threading.Tasks;

namespace EvoSnake
{
    public class SnakeManager
    {
        private bool isAnyAlive;

        public Snake[][] Snakes { get; private set; }
        public Snake[] Best { get; private set; }

        public uint Generation { get; private set; }

        public ulong[] Record { get; private set; }

        public ulong fitness;

        public ulong Fitness
        {
            get { return fitness / (ulong)(Settings.population * Settings.types); }
        }

        public SnakeManager()
        {
            Record = new ulong[Settings.types];
            Best = new Snake[Settings.types];
            Snakes = new Snake[Settings.types][];

            for (int i = 0; i < Settings.types; i++)
            {
                Best[i] = null;
                Record[i] = 0;

                Snakes[i] = new Snake[Settings.population];

                for (int j = 0; j < Settings.population; j++)
                    Snakes[i][j] = new Snake();
            }

            isAnyAlive = true;
        }

        public void Update()
        {
            Generation++;

            do
            {
                isAnyAlive = false;

                for (int i = 0; i < Settings.types; ++i)
                    for (int j = 0; j < Settings.population; ++j)
                        if (Snakes[i][j].Alive)
                        {
                            isAnyAlive = true;
                            Snakes[i][j].Step();
                        }
            } while (isAnyAlive);
        }

        public Snake SelectBest()
        {
            int theBest;

            for (int i = 0; i < Settings.types; i++)
            {
                theBest = 0;

                for (int j = 1; j < Settings.population; j++)
                    if (Snakes[i][theBest].Fitness < Snakes[i][j].Fitness)
                        theBest = j;

                Best[i] = new Snake(Snakes[i][theBest]);

                if (Record[i] < Snakes[i][theBest].Fitness)
                    Record[i] = Snakes[i][theBest].Fitness;
            }

            theBest = 0;

            for (int i = 1; i < Settings.types; i++)
                if (Best[i].Fitness > Best[theBest].Fitness)
                    theBest = i;

            return Best[theBest];
        }

        public void NaturalSelection()
        {
            fitness = 0;

            Snake[] SnakesTmp;

            for (int i = 0; i < Snakes.Length; i++)
            {
                ulong fitnessSum = 0;

                for (int j = 0; j < Snakes[i].Length; j++)
                    fitnessSum += Snakes[i][j].Fitness;

                fitness += fitnessSum;

                SnakesTmp = new Snake[Settings.population];
                SnakesTmp[0] = new Snake(Best[i]);

                for (int j = 1; j < Snakes[i].Length; j++)
                {
                    SnakesTmp[j] = new Snake(GetSnake(Snakes[i], fitnessSum), GetSnake(Snakes[i], fitnessSum));
                    SnakesTmp[j].mutate();
                }

                Snakes[i] = new Snake[Settings.population];
                for (int j = 0; j < Settings.population; j++)
                    Snakes[i][j] = new Snake(SnakesTmp[j]);
            }
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