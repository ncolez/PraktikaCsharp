using System;

namespace z1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = new int[15];
            Random rand = new Random();

            Console.WriteLine("Исходный массив:");
            for (int i = 0; i < 15; i++)
            {
                arr[i] = rand.Next(-50, 51);
                Console.Write(arr[i] + " ");
            }

            Console.WriteLine("\n\nПорядковые номера нечетных элементов:");
            for (int i = 0; i < 15; i++)
            {
                if (arr[i] % 2 != 0)
                {
                    Console.Write((i + 1) + " ");
                }
            }

            Console.ReadLine();
        }
    }
}