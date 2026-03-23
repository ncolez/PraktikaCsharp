using System;

namespace z2
{
    class Program
    {
        static void Main(string[] args)
        {
            double x;
            double y;
            bool result;

            Console.Write("Введите x: ");
            x = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введите y: ");
            y = Convert.ToDouble(Console.ReadLine());

            result = (x > 0 && y > 0) || (x < 0 && y < 0);

            Console.WriteLine("Точка находится в I или III четверти: " + result);

            Console.ReadLine();
        }
    }
}