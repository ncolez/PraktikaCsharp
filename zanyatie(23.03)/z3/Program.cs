using System;

namespace z3
{
    delegate void TemperatureHandler(double temperature);

    class TemperatureSensor
    {
        public event TemperatureHandler? TemperatureChanged;

        public void SetTemperature(double temp)
        {
            Console.WriteLine("Температура изменилась: " + temp);
            if (TemperatureChanged != null)
            {
                TemperatureChanged(temp);
            }
        }
    }

    class CoolingSystem
    {
        public void TurnOn(double temp)
        {
            if (temp > 25)
            {
                Console.WriteLine("Кондиционер включен");
            }
            else
            {
                Console.WriteLine("Кондиционер выключен");
            }
        }
    }

    class WarningSystem
    {
        public void Warn(double temp)
        {
            if (temp > 30)
            {
                Console.WriteLine("Внимание! перегрев!");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            TemperatureSensor sensor = new TemperatureSensor();
            CoolingSystem cooling = new CoolingSystem();
            WarningSystem warning = new WarningSystem();

            sensor.TemperatureChanged += cooling.TurnOn;
            sensor.TemperatureChanged += warning.Warn;

            sensor.SetTemperature(22);
            sensor.SetTemperature(28);
            sensor.SetTemperature(32);

            Console.ReadLine();
        }
    }
}