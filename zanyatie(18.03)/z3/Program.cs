using System;

namespace z3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите размер матрицы N (N<10): ");
            int N = Convert.ToInt32(Console.ReadLine() ?? "0");

            Console.Write("Введите нижнюю границу a: ");
            int a = Convert.ToInt32(Console.ReadLine() ?? "0");

            Console.Write("Введите верхнюю границу b: ");
            int b = Convert.ToInt32(Console.ReadLine() ?? "0");

            Console.Write("Введите C: ");
            int C = Convert.ToInt32(Console.ReadLine() ?? "0");

            Console.Write("Введите D: ");
            int D = Convert.ToInt32(Console.ReadLine() ?? "0");

            int[,] matrix = new int[N, N];
            Random rand = new Random();

            Console.WriteLine("\nИсходная матрица:");
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    matrix[i, j] = rand.Next(a, b + 1);
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }

            long product = 1;
            bool hasInRange = false;

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (matrix[i, j] >= C && matrix[i, j] <= D)
                    {
                        product = product * matrix[i, j];
                        hasInRange = true;
                    }
                }
            }

            if (hasInRange)
            {
                Console.WriteLine("\nПроизведение чисел в диапазоне [" + C + ", " + D + "]: " + product);
            }
            else
            {
                Console.WriteLine("\nНет чисел в диапазоне [" + C + ", " + D + "]");
            }

            Console.WriteLine("\nСумма элементов каждого столбца:");
            for (int j = 0; j < N; j++)
            {
                int sum = 0;
                for (int i = 0; i < N; i++)
                {
                    sum = sum + matrix[i, j];
                }
                Console.WriteLine("Столбец " + (j + 1) + ": " + sum);
            }

            Console.ReadLine();
        }
    }
}