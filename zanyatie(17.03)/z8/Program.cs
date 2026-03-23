using System;

namespace z8
{
    class Program
    {
        static void Main(string[] args)
        {
            int N;
            int sum = 0;

            Console.Write("Введите N (от 1 до 10): ");
            N = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Квадраты чисел от 1 до " + N + ":");

            for (int i = 1; i <= N; i++)
            {
                sum = sum + (2 * i - 1);
                Console.WriteLine(i + "^2 = " + sum);
            }

            Console.ReadLine();
        }
    }
}