using System;

namespace z5
{
    abstract class Device
    {
        public abstract void TurnOn();

        public virtual void TurnOff()
        {
            Console.WriteLine("Устройство выключается");
        }
    }

    class Smartphone : Device
    {
        public override void TurnOn()
        {
            Console.WriteLine("Смартфон включается");
        }

        public override void TurnOff()
        {
            Console.WriteLine("Смартфон выключается");
        }
    }

    class Laptop : Device
    {
        public override void TurnOn()
        {
            Console.WriteLine("Ноутбук включается");
        }

        public override void TurnOff()
        {
            Console.WriteLine("Ноутбук выключается");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Device smartphone = new Smartphone();
            Device laptop = new Laptop();

            smartphone.TurnOn();
            smartphone.TurnOff();

            laptop.TurnOn();
            laptop.TurnOff();

            Console.ReadLine();
        }
    }
}