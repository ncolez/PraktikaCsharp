using System;
using System.Collections.Generic;

namespace z3
{
    interface ISettings<T>
    {
        void Set(string key, T value);
        T Get(string key);
        bool ContainsKey(string key);
    }

    class SettingsStorage<T> : ISettings<T>
    {
        private Dictionary<string, T> settings;

        public SettingsStorage()
        {
            settings = new Dictionary<string, T>();
        }

        public void Set(string key, T value)
        {
            settings[key] = value;
            Console.WriteLine("Настройка сохранена: " + key + " = " + value);
        }

        public T Get(string key)
        {
            if (settings.ContainsKey(key))
            {
                return settings[key];
            }
            else
            {
                return default(T)!;
            }
        }

        public bool ContainsKey(string key)
        {
            return settings.ContainsKey(key);
        }
    }

    class SettingsManager<T>
    {
        private SettingsStorage<T> storage;

        public SettingsManager()
        {
            storage = new SettingsStorage<T>();
        }

        public void SetSetting(string key, T value)
        {
            storage.Set(key, value);
        }

        public T GetSetting(string key)
        {
            return storage.Get(key);
        }

        public void ShowAllSettings()
        {
            Console.WriteLine("Все настройки:");
            if (storage.ContainsKey("theme")) Console.WriteLine("theme = " + storage.Get("theme"));
            if (storage.ContainsKey("language")) Console.WriteLine("language = " + storage.Get("language"));
            if (storage.ContainsKey("volume")) Console.WriteLine("volume = " + storage.Get("volume"));
        }

        public void RemoveSetting(string key)
        {
            if (storage.ContainsKey(key))
            {
                storage.Set(key, default(T)!);
                Console.WriteLine("Настройка удалена: " + key);
            }
            else
            {
                Console.WriteLine("Настройка не найдена: " + key);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            SettingsManager<string> manager = new SettingsManager<string>();

            manager.SetSetting("theme", "dark");
            manager.SetSetting("language", "russian");
            manager.SetSetting("volume", "high");

            manager.ShowAllSettings();

            Console.WriteLine("\nПолучение theme: " + manager.GetSetting("theme"));

            manager.RemoveSetting("language");
            manager.ShowAllSettings();

            Console.ReadLine();
        }
    }
}