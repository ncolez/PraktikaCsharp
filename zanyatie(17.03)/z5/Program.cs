using System;

namespace z5
{
    class Program
    {
        static void Main(string[] args)
        {
            int num;
            bool isOdd;

            Console.Write("Введите целое число: ");
            num = Convert.ToInt32(Console.ReadLine());

            isOdd = num % 2 != 0;

            Console.WriteLine("Число нечетное: " + isOdd);

            Console.ReadLine();
        }
    }
}