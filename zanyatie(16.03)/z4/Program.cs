using System;

namespace z4
{
    class Program
    {
        static void Main(string[] args)
        {
            int a;
            int b;
            int c;
            int sum;

            Console.Write("a = ");
            a = Convert.ToInt32(Console.ReadLine());

            Console.Write("b = ");
            b = Convert.ToInt32(Console.ReadLine());

            Console.Write("c = ");
            c = Convert.ToInt32(Console.ReadLine());

            sum = a + b + c;

            Console.WriteLine(a + " + " + b + " + " + c + " = " + sum);

            Console.ReadLine();
        }
    }
}