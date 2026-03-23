using System;

namespace z6
{
    class Program
    {
        static void Main(string[] args)
        {
            int age;

            Console.Write("Введите возраст мужчины: ");
            age = Convert.ToInt32(Console.ReadLine());

            if (age < 1)
            {
                Console.WriteLine("младенец");
            }
            else if (age >= 1 && age <= 11)
            {
                Console.WriteLine("ребенок");
            }
            else if (age >= 12 && age <= 15)
            {
                Console.WriteLine("подросток");
            }
            else if (age >= 16 && age <= 25)
            {
                Console.WriteLine("юноша");
            }
            else if (age >= 26 && age <= 70)
            {
                Console.WriteLine("мужчина");
            }
            else
            {
                Console.WriteLine("старик");
            }

            Console.ReadLine();
        }
    }
}