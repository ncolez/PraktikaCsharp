using System;

namespace z5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите количество строк: ");
            int rows = Convert.ToInt32(Console.ReadLine() ?? "0");

            int[][] jagged = new int[rows][];
            Random rand = new Random();

            for (int i = 0; i < rows; i++)
            {
                int cols = rand.Next(2, 6);
                jagged[i] = new int[cols];
                for (int j = 0; j < cols; j++)
                {
                    jagged[i][j] = rand.Next(1, 20);
                }
            }

            Console.WriteLine("\nИсходный ступенчатый массив:");
            for (int i = 0; i < rows; i++)
            {
                Console.Write("Строка " + (i + 1) + ": ");
                for (int j = 0; j < jagged[i].Length; j++)
                {
                    Console.Write(jagged[i][j] + " ");
                }
                Console.WriteLine();
            }

            int[] sums = new int[rows];
            for (int i = 0; i < rows; i++)
            {
                int sum = 0;
                for (int j = 0; j < jagged[i].Length; j++)
                {
                    sum = sum + jagged[i][j];
                }
                sums[i] = sum;
            }

            for (int i = 0; i < rows - 1; i++)
            {
                for (int j = i + 1; j < rows; j++)
                {
                    if (sums[i] < sums[j])
                    {
                        int tempSum = sums[i];
                        sums[i] = sums[j];
                        sums[j] = tempSum;

                        int[] tempRow = jagged[i];
                        jagged[i] = jagged[j];
                        jagged[j] = tempRow;
                    }
                }
            }

            Console.WriteLine("\nМассив после сортировки по убыванию суммы:");
            for (int i = 0; i < rows; i++)
            {
                Console.Write("Строка " + (i + 1) + " (сумма=" + sums[i] + "): ");
                for (int j = 0; j < jagged[i].Length; j++)
                {
                    Console.Write(jagged[i][j] + " ");
                }
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}