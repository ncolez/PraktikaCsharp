using System;

namespace z8
{
    class Program
    {
        static void CompareNumbers(in double a, in double b, out string result)
        {
            if (a > b)
            {
                result = "больше";
            }
            else if (a < b)
            {
                result = "меньше";
            }
            else
            {
                result = "равно";
            }
        }

        static void CompareNumbers(in int a, in int b, out string result)
        {
            if (a > b)
            {
                result = "больше";
            }
            else if (a < b)
            {
                result = "меньше";
            }
            else
            {
                result = "равно";
            }
        }

        static void Main(string[] args)
        {
            Console.Write("Введите первое число: ");
            double a = Convert.ToDouble(Console.ReadLine() ?? "0");

            Console.Write("Введите второе число: ");
            double b = Convert.ToDouble(Console.ReadLine() ?? "0");

            CompareNumbers(in a, in b, out string result);
            Console.WriteLine(a + " " + result + " " + b);

            Console.ReadLine();
        }
    }
}