using System.Collections.Generic;

namespace EvoSnake
{
    public class Snake
    {
        public NeuralNet Brain { get; set; }

        public bool Alive { get; set; } = true;

        public Point Food { get; set; }
        public Point Head { get; set; } = new Point(Settings.startPoint);
        public List<Point> Tail { get; set; } = new List<Point>();

        public int Age { get; set; } = 0;
        public int TimeLeft { get; set; } = Settings.lifeDuration;

        public int Length
        {
            get { return Tail.Count; }
        }

        public ulong Fitness
        {
            get { return Settings.fitnessFunction((ulong)Age, (ulong)Length); }
        }

        public static Snake crossover(Snake parent1, Snake parent2)
        {
            return new Snake(parent1, parent2);
        }

        public Snake()
        {
            Brain = new NeuralNet();

            Tail.Add(new Point(Head.X - 1, Head.Y));
            Tail.Add(new Point(Head.X - 1, Head.Y));

            NewFood();
        }

        public Snake(Snake parent)
        {
            Brain = new NeuralNet(parent.Brain);

            Tail.Add(new Point(Head.X - 1, Head.Y));
            Tail.Add(new Point(Head.X - 1, Head.Y));

            NewFood();
        }

        public Snake(Snake parent1, Snake parent2)
        {
            Brain = new NeuralNet(parent1.Brain, parent2.Brain);

            Tail.Add(new Point(Head.X - 1, Head.Y));
            Tail.Add(new Point(Head.X - 1, Head.Y));

            NewFood();
        }

        ~Snake()
        {
            Tail.Clear();
        }

        public void mutate()
        {
            Brain.mutate();
        }

        public void Step()
        {
            Age++;
            TimeLeft--;

            double[] directions = Brain.output(Look());

            int max = 0;
            for (int i = 1; i < Settings.outDirections.Length; i++)
                if (directions[i] > directions[max])
                    max = i;

            var direction = new Point(Settings.outDirections[max]);

            Move(direction);
        }

        bool IsOnHead(Point Point)
        {
            return (Point == Head);
        }

        bool IsOnFood(Point Point)
        {
            return (Point == Food);
        }

        bool IsOnTail(Point Point)
        {
            foreach (var segment in Tail)
                if (Point == segment)
                    return true;

            return false;
        }

        bool IsOutside(Point Point)
        {
            return (Point.X < 0 || Point.X >= Settings.fieldWidth
                || Point.Y < 0 || Point.Y >= Settings.fieldHeight);
        }

        void Move(Point direction)
        {
            if (Length > 0)
            {
                for (int i = Length - 1; i > 0; --i)
                    Tail[i].Set(Tail[i - 1]);

                Tail[0].Set(Head);
            }

            Head += direction;

            Alive = !(IsOnTail(Head) || IsOutside(Head) || TimeLeft <= 0);

            if (IsOnFood(Head))
            {
                Tail.Add(new Point(Head));

                NewFood();

                TimeLeft += Settings.growBonus;
            }
        }

        void NewFood()
        {
            do
            {
                Food = new Point(Settings.R.Next(0, Settings.fieldWidth), Settings.R.Next(0, Settings.fieldHeight));
            } while (IsOnHead(Food) || IsOnTail(Food));
        }

        double[] Look()
        {
            double[] vision = new double[Settings.inDirections.Length * 3];

            int counter = 0;

            foreach (var dir in Settings.inDirections)
            {
                Point tmp = new Point(Head);

                int distance = 0;
                bool f = true, b = true;

                while (!IsOutside(tmp))
                {
                    if (f && IsOnFood(tmp))
                    {
                        vision[counter] = 1.0;
                        f = false;
                    }

                    if (b && IsOnTail(tmp))
                    {
                        vision[counter + 1] = 1.0 / distance;
                        b = false;
                    }

                    ++distance;

                    tmp += dir;
                }
                vision[counter + 2] = 1.0 / distance;

                counter += 3;
            }

            return vision;
        }
    }
}