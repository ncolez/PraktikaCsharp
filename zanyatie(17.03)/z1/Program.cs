using System;

namespace z1
{
    class Program
    {
        static void Main(string[] args)
        {
            int kg;
            int ton;

            Console.Write("Введите массу в килограммах: ");
            kg = Convert.ToInt32(Console.ReadLine());

            ton = kg / 1000;

            Console.WriteLine("Полных тонн: " + ton);

            Console.ReadLine();
        }
    }
}