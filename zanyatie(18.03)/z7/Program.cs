using System;

namespace z7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите строку: ");
            string text = Console.ReadLine() ?? "";

            Console.Write("Введите подстроку для поиска: ");
            string search = Console.ReadLine() ?? "";

            int index = text.IndexOf(search);

            if (index >= 0)
            {
                Console.WriteLine("Первое вхождение подстроки на индексе: " + index);
            }
            else
            {
                Console.WriteLine("Подстрока не найдена");
            }

            Console.ReadLine();
        }
    }
}