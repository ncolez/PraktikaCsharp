using System;

namespace z5
{
    class Program
    {
        static void Main(string[] args)
        {
            int n;
            int a;
            int b;
            int c;
            int m;

            Console.Write("Введите трёхзначное число: ");
            n = Convert.ToInt32(Console.ReadLine());

            a = n / 100;
            b = (n / 10) % 10;
            c = n % 10;

            m = c * 100 + b * 10 + a;

            Console.WriteLine("Число справа налево: " + m);

            Console.ReadLine();
        }
    }
}