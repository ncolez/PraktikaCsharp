using System;

namespace z3
{
    class Program
    {
        static void Main(string[] args)
        {
            int a;
            int b;
            int count = 0;

            Console.Write("Введите A: ");
            a = Convert.ToInt32(Console.ReadLine());

            Console.Write("Введите B (больше A): ");
            b = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Числа между A и B в порядке убывания:");

            for (int i = b - 1; i > a; i--)
            {
                Console.Write(i + " ");
                count++;
            }

            Console.WriteLine();
            Console.WriteLine("Количество чисел: " + count);

            Console.ReadLine();
        }
    }
}