using System;

namespace EvoSnake
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point() { }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point(Point point)
        {
            X = point.X;
            Y = point.Y;
        }

        public void Add(int x, int y)
        {
            X += x;
            Y += y;
        }

        public void Add(Point point)
        {
            X += point.X;
            Y += point.Y;
        }

        public void Set(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Set(Point point)
        {
            X = point.X;
            Y = point.Y;
        }

        public static Point operator +(Point point1, Point point2)
        {
            return new Point(point1.X + point2.X, point1.Y + point2.Y);
        }

        public static Point operator -(Point point1, Point point2)
        {
            return new Point(Math.Abs(point1.X - point2.X), Math.Abs(point1.Y - point2.Y));
        }

        public static bool operator ==(Point point1, Point point2)
        {
            return (point1.X == point2.X && point1.Y == point2.Y);
        }

        public static bool operator !=(Point point1, Point point2)
        {
            return (point1.X != point2.X || point1.Y != point2.Y);
        }
    }
}