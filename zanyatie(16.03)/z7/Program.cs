using System;

namespace z7
{
    class Program
    {
        static void Main(string[] args)
        {
            double L;
            double D;
            double S;

            Console.Write("Введите длину окружности: ");
            L = Convert.ToDouble(Console.ReadLine());

            D = L / 3.14;
            S = (3.14 / 4) * D * D;

            Console.WriteLine("Площадь круга: " + S);

            Console.ReadLine();
        }
    }
}