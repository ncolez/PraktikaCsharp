using System;

namespace z1
{
    class Program
    {
        static bool IsPalindrome(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return false;
            }
            string str = s.ToLower();
            for (int i = 0; i < str.Length / 2; i++)
            {
                if (str[i] != str[str.Length - 1 - i])
                {
                    return false;
                }
            }
            return true;
        }

        static void Main(string[] args)
        {
            Console.Write("Введите строку: ");
            string input = Console.ReadLine() ?? "";

            if (IsPalindrome(input))
            {
                Console.WriteLine("Строка является палиндромом");
            }
            else
            {
                Console.WriteLine("Строка не является палиндромом");
            }

            Console.ReadLine();
        }
    }
}