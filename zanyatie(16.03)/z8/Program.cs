using System;

namespace z8
{
    class Program
    {
        static void Main(string[] args)
        {
            double x = 5.2;

            double x2 = x * x;
            double v = x2 + 5;
            double v2 = v * v;
            double sin_v = Math.Sin(v2);
            double sin3 = sin_v * sin_v * sin_v;

            double k = x / 4;
            double kor = Math.Sqrt(k);

            double y = sin3 - kor;

            Console.WriteLine("x = " + x);
            Console.WriteLine("y = " + y);

            Console.ReadLine();
        }
    }
}