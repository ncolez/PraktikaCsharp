using System;

namespace z3
{
    class Program
    {
        static void Main(string[] args)
        {
            double a;
            double z1;
            double z2;

            Console.Write("Введите угол a в градусах: ");
            a = Convert.ToDouble(Console.ReadLine());

            a = a * 3.14 / 180;

            double sin4a = Math.Sin(4 * a);
            double cos4a = Math.Cos(4 * a);
            double cos2a = Math.Cos(2 * a);

            z1 = (sin4a / (1 + cos4a)) * (cos2a / (1 + cos2a));

            double argument = (3 * 3.14 / 2) - a;
            z2 = 1 / Math.Tan(argument);

            Console.WriteLine("z1 = " + z1);
            Console.WriteLine("z2 = " + z2);

            Console.ReadLine();
        }
    }
}