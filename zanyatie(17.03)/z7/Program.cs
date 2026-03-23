using System;

namespace z7
{
    class Program
    {
        static void Main(string[] args)
        {
            double kg;

            // while
            Console.WriteLine("способ 1 (while):");
            int funt1 = 1;
            while (funt1 <= 100)
            {
                kg = funt1 * 0.453;
                Console.WriteLine(funt1 + " фунтов = " + kg.ToString("F3") + " кг");
                funt1++;
            }

            Console.WriteLine();

            //do while
            Console.WriteLine("способ 2 (do while):");
            int funt2 = 1;
            do
            {
                kg = funt2 * 0.453;
                Console.WriteLine(funt2 + " фунтов = " + kg.ToString("F3") + " кг");
                funt2++;
            }
            while (funt2 <= 100);

            Console.WriteLine();

            //for
            Console.WriteLine("способ 3 (for):");
            for (int funt3 = 1; funt3 <= 100; funt3++)
            {
                kg = funt3 * 0.453;
                Console.WriteLine(funt3 + " фунтов = " + kg.ToString("F3") + " кг");
            }

            Console.ReadLine();
        }
    }
}