using System;

namespace z2
{
    class Person
    {
        public string Name;

        public Person(string name)
        {
            Name = name;
        }
    }

    static class ArrayUtils
    {
        public static void SortByName(Person[] persons)
        {
            for (int i = 0; i < persons.Length - 1; i++)
            {
                for (int j = i + 1; j < persons.Length; j++)
                {
                    if (string.Compare(persons[i].Name, persons[j].Name) > 0)
                    {
                        Person temp = persons[i];
                        persons[i] = persons[j];
                        persons[j] = temp;
                    }
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите имена: ");
            string? input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Ошибка: имена не введены.");
                Console.ReadLine();
                return;
            }

            string[] names = input.Split(',');

            Person[] persons = new Person[names.Length];
            for (int i = 0; i < names.Length; i++)
            {
                string name = names[i].Trim();
                persons[i] = new Person(name);
            }

            ArrayUtils.SortByName(persons);

            Console.WriteLine("\nПосле сортировки:");
            for (int i = 0; i < persons.Length; i++)
            {
                Console.WriteLine(persons[i].Name);
            }

            Console.ReadLine();
        }
    }
}