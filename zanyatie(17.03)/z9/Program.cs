using System;

namespace z9
{
    class Program
    {
        static void Main(string[] args)
        {
            double A = Math.PI / 3;
            double B = 2 * Math.PI / 3;
            int M = 20;
            double H;
            double x;
            double y;

            H = (B - A) / M;
            x = A;

            Console.WriteLine("x\t\tcos(x)");

            for (int i = 1; i <= M; i++)
            {
                y = Math.Cos(x);
                Console.WriteLine(x.ToString("F4") + "\t\t" + y.ToString("F4"));
                x = x + H;
            }

            Console.ReadLine();
        }
    }
}