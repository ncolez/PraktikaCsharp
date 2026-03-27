using System;
using System.Collections;

namespace z1
{
    class Cache
    {
        private Hashtable cache;

        public Cache()
        {
            cache = new Hashtable();
        }

        public void AddToCache(string key, object value)
        {
            cache[key] = value;
            Console.WriteLine("Добавлено: " + key + " = " + value);
        }

        public object? GetFromCache(string key)
        {
            return cache[key];
        }

        public void RemoveFromCache(string key)
        {
            cache.Remove(key);
            Console.WriteLine("Удалено: " + key);
        }

        public void ShowAll()
        {
            Console.WriteLine("Содержимое кэша:");
            foreach (DictionaryEntry entry in cache)
            {
                Console.WriteLine(entry.Key + " = " + entry.Value);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Cache cache = new Cache();

            cache.AddToCache("user1", "Иван");
            cache.AddToCache("user2", "Петр");
            cache.AddToCache("age", 25);

            cache.ShowAll();

            Console.WriteLine("\nПолучение user1: " + cache.GetFromCache("user1"));

            cache.RemoveFromCache("user2");
            cache.ShowAll();

            Console.ReadLine();
        }
    }
}