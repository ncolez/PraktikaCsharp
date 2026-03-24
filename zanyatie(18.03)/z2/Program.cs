using System;

namespace z2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = new int[]
            {
                -30, -89, -26, 79, 14, 30, -29, -1, -30, 14,
                76, 75, -6, -99, -50, 62, -83, -25, 54, 90,
                84, 90, 52, 73, 46, 49, 73, 63, 58, 21,
                39, 65, 13, 10, 16, 60, 83, 44, 54, 74,
                69, 70, 33, 92, 62, 94, 66, 17, 7, 40,
                97, 85, 58, 55, 12, 3, 30, 33, 9, 67,
                76, 96, 100, 87, 26, -86, -41, -78, -58, -11,
                -69, -21, -72, -89, -79, -61, -89, -68, -89, -2,
                -84, -88, -45, -38, -97, -31, -33, -5, -76, -63,
                -58, -29, -16, -94, -84, -2, -48, -20, -75, -9
            };

            Console.WriteLine("Исходный массив (первые 20 из 100):");
            for (int i = 0; i < 20; i++)
            {
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine("...");

            Console.WriteLine("\nОтрицательные числа:");
            for (int i = 0; i < 100; i++)
            {
                if (arr[i] < 0)
                {
                    Console.Write(arr[i] + " ");
                }
            }

            Console.WriteLine("\n\nОстальные числа:");
            for (int i = 0; i < 100; i++)
            {
                if (arr[i] >= 0)
                {
                    Console.Write(arr[i] + " ");
                }
            }

            Console.ReadLine();
        }
    }
}