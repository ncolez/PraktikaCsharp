using System;

namespace z4
{
    class Program
    {
        static void Main(string[] args)
        {
            double x;
            double y;

            Console.Write("Введите x: ");
            x = Convert.ToDouble(Console.ReadLine());

            if (x < 1.3)
            {
                y = Math.Sqrt(x * x - 7 / Math.Sqrt(Math.Abs(x)));
            }
            else
            {
                y = 3 * x - Math.Cos(x) * Math.Cos(x);
            }

            Console.WriteLine("y = " + y);

            Console.ReadLine();
        }
    }
}