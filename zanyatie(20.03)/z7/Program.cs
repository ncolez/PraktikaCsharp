using System;

namespace z7
{
    class Program
    {
        static double ConvertToFahrenheit(double celsius)
        {
            return celsius * 9 / 5 + 32;
        }

        static double ConvertToFahrenheit(double kelvin, bool isKelvin)
        {
            double celsius = kelvin - 273.15;
            return celsius * 9 / 5 + 32;
        }

        static void Main(string[] args)
        {
            Console.Write("Введите температуру в Цельсиях: ");
            double celsius = Convert.ToDouble(Console.ReadLine() ?? "0");
            Console.WriteLine(celsius + "°C = " + ConvertToFahrenheit(celsius) + "°F");

            Console.Write("Введите температуру в Кельвинах: ");
            double kelvin = Convert.ToDouble(Console.ReadLine() ?? "0");
            Console.WriteLine(kelvin + "K = " + ConvertToFahrenheit(kelvin, true) + "°F");

            Console.ReadLine();
        }
    }
}