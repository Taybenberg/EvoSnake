using System;

namespace EvoSnake
{
    public static class Settings
    {
        public static Random R = new Random();

        public static Point startPoint = new Point(20, 20);

        public static int fieldWidth = 40, fieldHeight = 40;

        public static int types = 5, population = 500;

        public static int lifeDuration = 100, growBonus = 75;
        
        public static double bias = 1.0;

        public static double mutationRate = 0.01;

        public static int[] hiddenLayersSize = new int[]
        {
            36,
            24
        };

        public static int getLayerSize(int index)
        {
            if (index == 0)
                return inDirections.Length * 3;
            else if (index > hiddenLayersSize.Length)
                return outDirections.Length;
            else
                return hiddenLayersSize[index - 1];
        }

        public static Point[] inDirections = new Point[]
        {
            new Point(0, -1),
            new Point(1, -1),
            new Point(1, 0),
            new Point(1, 1),
            new Point(0, 1),
            new Point(-1, 1),
            new Point(-1, 0),
            new Point(-1, -1)
        };

        public static Point[] outDirections = new Point[]
        {
            new Point(0, -1),
            new Point(1, 0),
            new Point(0, 1),
            new Point(-1, 0)
        };

        public static double random()
        {
            if (R.Next(0, 2) > 0)
                return R.NextDouble();

            return -R.NextDouble();
        }

        public static double activationFunction(double x)
        {
            return 1 / (1 + Math.Pow(Math.E, -x));
        }

        public static double mutationFunction()
        {
            return Math.Sqrt(-2.0 * Math.Log(R.NextDouble())) * Math.Sin(2.0 * Math.PI * R.NextDouble()) / 5.0;
        }

        public static ulong fitnessFunction(ulong lifetime, ulong lenght)
        {
            if (lenght < 10)
                return (lifetime * lifetime * lenght * lenght) / 100;

            return (lifetime * lifetime * 128 * (lenght - 9)) / 100;
        }
    }
}