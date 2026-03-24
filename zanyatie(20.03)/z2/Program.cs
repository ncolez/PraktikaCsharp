using System;

namespace z2
{
    class Program
    {
        static void TrianglePS(double a, out double P, out double S)
        {
            P = 3 * a;
            S = Math.Sqrt(3) / 4 * a * a;
        }

        static void Main(string[] args)
        {
            double[] sides = new double[3];

            for (int i = 0; i < 3; i++)
            {
                Console.Write("Введите сторону " + (i + 1) + "-го треугольника: ");
                sides[i] = Convert.ToDouble(Console.ReadLine());
            }

            Console.WriteLine("\nРезультаты:");
            for (int i = 0; i < 3; i++)
            {
                double P, S;
                TrianglePS(sides[i], out P, out S);
                Console.WriteLine("Треугольник " + (i + 1) + ": сторона=" + sides[i] + ", P=" + P + ", S=" + S);
            }

            Console.ReadLine();
        }
    }
}