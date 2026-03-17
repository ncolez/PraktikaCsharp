using System;

namespace z6
{
    class Program
    {
        static void Main(string[] args)
        {
            double x = 0.7;

            double e = Math.Exp(x);
            double cos_e = Math.Cos(e);
            double ln_cos = Math.Log(cos_e);
            double part1 = 20 * ln_cos;

            double sin_pi = Math.Sin(3.14);
            double sin3 = sin_pi * sin_pi * sin_pi;
            double mod = Math.Abs(1 - x * x);
            double sqrt = Math.Sqrt(sin3 + mod);
            double part2 = 2 / sqrt;

            double y = part1 - part2;

            Console.WriteLine("x = " + x);
            Console.WriteLine("y = " + y);

            Console.ReadLine();
        }
    }
}