using System;

namespace z4
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] salary = new int[18, 12];
            Random rand = new Random();

            for (int i = 0; i < 18; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    salary[i, j] = rand.Next(10000, 100000);
                }
            }

            int totalFirst = 0;
            for (int j = 0; j < 12; j++)
            {
                totalFirst = totalFirst + salary[0, j];
            }

            Console.Write("Введите число для сравнения: ");
            int number = Convert.ToInt32(Console.ReadLine() ?? "0");

            Console.WriteLine("\nГодовой доход первого человека: " + totalFirst);

            if (totalFirst > number)
            {
                Console.WriteLine("Верно: годовой доход первого человека больше " + number);
            }
            else
            {
                Console.WriteLine("Неверно: годовой доход первого человека не больше " + number);
            }

            Console.ReadLine();
        }
    }
}