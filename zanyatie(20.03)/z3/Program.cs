using System;

namespace z3
{
    class Program
    {
        static bool IsPalindrome(string s, int left, int right)
        {
            if (string.IsNullOrEmpty(s))
            {
                return false;
            }
            if (left >= right)
            {
                return true;
            }
            if (char.ToLower(s[left]) != char.ToLower(s[right]))
            {
                return false;
            }
            return IsPalindrome(s, left + 1, right - 1);
        }

        static void Main(string[] args)
        {
            Console.Write("Введите строку: ");
            string input = Console.ReadLine() ?? "";

            if (IsPalindrome(input, 0, input.Length - 1))
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