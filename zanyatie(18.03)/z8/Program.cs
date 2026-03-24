using System;

namespace z8
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите строку: ");
            string input = Console.ReadLine() ?? "";

            string result = "";
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if (char.IsUpper(c))
                {
                    if (i > 0)
                    {
                        result = result + "_";
                    }
                    result = result + char.ToLower(c);
                }
                else
                {
                    result = result + c;
                }
            }

            Console.WriteLine("snake_case: " + result);
            Console.ReadLine();
        }
    }
}