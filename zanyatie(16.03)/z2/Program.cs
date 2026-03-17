using System;

namespace z2
{
    class Program
    {
        static void Main(string[] args)
        {
            int number;
            int first;
            int last;

            Console.Write("Введите трёхзначное число: ");
            number = Convert.ToInt32(Console.ReadLine());

            first = number / 100;
            last = number % 10;

            Console.WriteLine("Первая цифра: " + first);
            Console.WriteLine("Последняя цифра: " + last);

            Console.ReadLine();
        }
    }
}