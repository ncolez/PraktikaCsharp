using System;

namespace z6
{
    class Program
    {
        static bool IsLeapYear(int Y)
        {
            return (Y % 4 == 0 && Y % 100 != 0) || (Y % 400 == 0);
        }

        static void Main(string[] args)
        {
            int[] years = new int[5];

            for (int i = 0; i < 5; i++)
            {
                Console.Write("Введите год " + (i + 1) + ": ");
                years[i] = Convert.ToInt32(Console.ReadLine() ?? "0");
            }

            Console.WriteLine("\nРезультаты:");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(years[i] + " - " + IsLeapYear(years[i]));
            }

            Console.ReadLine();
        }
    }
}