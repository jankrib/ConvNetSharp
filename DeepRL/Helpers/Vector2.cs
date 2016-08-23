using System;

namespace DeepRL.Helpers
{
    public class Vector2
    {
        public Vector2(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }

        public double Length
        {
            get { return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2)); }
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2 operator *(Vector2 a, double f)
        {
            return new Vector2(a.X * f, a.Y * f);
        }

        public void Scale(double f)
        {
            X *= f;
            Y *= f;
        }

        public void Normalize()
        {
            Scale(1/Length);
        }

        public Vector2 Rotate(double a)
        {
            return new Vector2(
                X * Math.Cos(a) + Y * Math.Sin(a),
                -X * Math.Sin(a) + Y * Math.Cos(a));
        }

        public static double Distance(Vector2 a, Vector2 b)
        {
            return (a - b).Length;
        }

    }
}
