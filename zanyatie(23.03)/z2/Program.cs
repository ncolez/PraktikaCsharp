using System;

namespace z2
{
    delegate bool NumberCheck(int number);

    class Program
    {
        static bool IsEven(int number)
        {
            return number % 2 == 0;
        }

        static bool IsOdd(int number)
        {
            return number % 2 != 0;
        }

        static void FilterNumbers(int[] numbers, NumberCheck check)
        {
            Console.WriteLine("Результат фильтрации:");
            for (int i = 0; i < numbers.Length; i++)
            {
                if (check(numbers[i]))
                {
                    Console.Write(numbers[i] + " ");
                }
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            Console.WriteLine("Исходный массив:");
            for (int i = 0; i < numbers.Length; i++)
            {
                Console.Write(numbers[i] + " ");
            }
            Console.WriteLine();

            FilterNumbers(numbers, IsEven);
            FilterNumbers(numbers, IsOdd);

            Console.ReadLine();
        }
    }
}