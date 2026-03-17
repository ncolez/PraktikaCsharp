using System;

namespace z1
{
    class Program
    {
        static void Main(string[] args)
        {
            double r;
            double h;
            double s;

            Console.WriteLine("Вычисление площади поверхности цилиндра.");
            Console.WriteLine("Введите исходные данные:");

            Console.Write("Радиус основания (см) ---> ");
            r = Convert.ToDouble(Console.ReadLine());

            Console.Write("Высота цилиндра (см) ---> ");
            h = Convert.ToDouble(Console.ReadLine());

            s = 2 * 3.14 * r * (r + h);

            Console.WriteLine("Площадь поверхности цилиндра: " + s + " кв.см.");

            Console.ReadLine();
        }
    }
}