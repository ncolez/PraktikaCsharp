using System;
using System.Text;

namespace z9
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите строку: ");
            string input = Console.ReadLine() ?? "";

            StringBuilder sb = new StringBuilder(input);

            Console.Write("Введите строку для вставки: ");
            string insertText = Console.ReadLine() ?? "";

            int middle = sb.Length / 2;
            sb.Insert(middle, insertText);

            Console.WriteLine("Результат: " + sb.ToString());
            Console.ReadLine();
        }
    }
}