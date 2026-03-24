using System;

namespace z4
{
    static class StringExtensions
    {
        public static string RemoveSpaces(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }
            string result = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] != ' ')
                {
                    result = result + s[i];
                }
            }
            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите строку: ");
            string input = Console.ReadLine() ?? "";

            string withoutSpaces = input.RemoveSpaces();
            Console.WriteLine("Строка без пробелов: " + withoutSpaces);

            Console.ReadLine();
        }
    }
}