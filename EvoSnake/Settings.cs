using System;

namespace EvoSnake
{
    public static class Settings
    {
        public static Random R = new Random();

        public static Point startPoint = new Point(15, 15);

        public static int fieldWidth = 30, fieldHeight = 30;

        public static int population = 400;

        public static int lifeDuration = 110, growBonus = 80;
        
        public static double bias = 1.0;

        public static double mutationRate = 0.25;

        public static int[] hiddenLayersSize = new int[]
        {
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
            if (lenght < 10ul)
                return (lifetime * lifetime * lenght * lenght) / 100ul;

            return (lifetime * lifetime * 128ul * (lenght - 9ul)) / 100ul;
        }
    }
}