using System;
using System.Collections;

namespace z1
{
    class CacheManager
    {
        private static CacheManager? instance;
        private Hashtable cache;

        private CacheManager()
        {
            cache = new Hashtable();
        }

        public static CacheManager GetInstance()
        {
            if (instance == null)
            {
                instance = new CacheManager();
            }
            return instance;
        }

        public void AddToCache(string key, object value)
        {
            cache[key] = value;
            Console.WriteLine("Добавлено в кэш: " + key + " = " + value);
        }

        public object? GetFromCache(string key)
        {
            return cache[key];
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CacheManager cache = CacheManager.GetInstance();

            cache.AddToCache("user", "Иван");
            cache.AddToCache("age", 25);

            Console.WriteLine("Получение user: " + cache.GetFromCache("user"));
            Console.WriteLine("Получение age: " + cache.GetFromCache("age"));

            Console.ReadLine();
        }
    }
}