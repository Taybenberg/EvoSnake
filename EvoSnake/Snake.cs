﻿using System.Collections.Generic;

namespace EvoSnake
{
    public class Snake
    {
        private NeuralNet brain;

        private Point direction;

        public bool Alive { get; private set; }

        public Point Food { get; private set; }
        public Point Head { get; private set; }
        public List<Point> Tail { get; private set; }

        public int Age { get; private set; }
        public int TimeLeft { get; private set; }

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
            brain = new NeuralNet();

            Alive = true;

            Age = 0;
            TimeLeft = Settings.lifeDuration;

            Head = new Point(Settings.startPoint);           

            Tail = new List<Point>();

            direction = new Point(0, 0);

            NewFood();
        }

        public Snake(Snake parent)
        {
            brain = new NeuralNet(parent.brain);

            Alive = true;

            Age = 0;
            TimeLeft = Settings.lifeDuration;

            Head = new Point(Settings.startPoint);

            Tail = new List<Point>();

            direction = new Point(0, 0);

            NewFood();
        }

        public Snake(Snake parent1, Snake parent2)
        {
            brain = new NeuralNet(parent1.brain, parent2.brain);

            Alive = true;

            Age = 0;
            TimeLeft = Settings.lifeDuration;

            Head = new Point(Settings.startPoint);

            Tail = new List<Point>();

            direction = new Point(0, 0);

            NewFood();
        }

        ~Snake()
        {
            Tail.Clear();
        }

        public void mutate()
        {
            brain.mutate();
        }

        public void Step()
        {
            Age++;
            TimeLeft--;

            double[] directions = brain.output(Look());

            int max = 0;

            for (int i = 1; i < Settings.outDirections.Length; i++)
                if (directions[i] > directions[max])
                    max = i;

            this.direction = new Point(Settings.outDirections[max]);

            Move();
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

        void Move()
        {
            CheckSafety();

            if (Length > 0)
            {
                for (int i = Length - 1; i > 0; --i)
                    Tail[i].Set(Tail[i - 1]);

                Tail[0].Set(Head);
            }

            Head += direction;

            TryEat();
        }

        void CheckSafety()
        {
            if (IsOnTail(Head) || IsOutside(Head) || TimeLeft <= 0)
                Alive = false;
        }

        void NewFood()
        {
            do
            {
                Food = new Point(Settings.R.Next(0, Settings.fieldWidth), Settings.R.Next(0, Settings.fieldHeight));
            } while (IsOnHead(Food) || IsOnTail(Food));
        }

        void TryEat()
        {
            if (IsOnFood(Head))
            {
                Tail.Add(new Point(Head));

                NewFood();

                TimeLeft += Settings.growBonus;
            }
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