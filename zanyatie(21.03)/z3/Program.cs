using System;

namespace z3
{
    class Instrument
    {
        public string Name;

        public Instrument(string name)
        {
            Name = name;
        }
    }

    interface IStringInstrument
    {
        void PlayString();
    }

    interface IPercussionInstrument
    {
        void PlayPercussion();
    }

    class Guitar : Instrument, IStringInstrument
    {
        public Guitar(string name) : base(name) { }

        public void PlayString()
        {
            Console.WriteLine(Name + " - струнный инструмент");
        }
    }

    class Drum : Instrument, IPercussionInstrument
    {
        public Drum(string name) : base(name) { }

        public void PlayPercussion()
        {
            Console.WriteLine(Name + " - ударный инструмент");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Instrument[] instruments = new Instrument[2];
            instruments[0] = new Guitar("Гитара");
            instruments[1] = new Drum("Барабан");

            Console.WriteLine("Струнные инструменты:");
            for (int i = 0; i < instruments.Length; i++)
            {
                if (instruments[i] is IStringInstrument)
                {
                    ((IStringInstrument)instruments[i]).PlayString();
                }
            }

            Console.ReadLine();
        }
    }
}