using System;

namespace z3
{
    class Character
    {
        public string? Name;
        public string? Class;
        public int Strength;
        public int Intelligence;
        public int Agility;

        public void ShowInfo()
        {
            Console.WriteLine("Персонаж: " + Name);
            Console.WriteLine("Класс: " + Class);
            Console.WriteLine("Сила: " + Strength);
            Console.WriteLine("Интеллект: " + Intelligence);
            Console.WriteLine("Ловкость: " + Agility);
        }
    }

    interface ICharacterBuilder
    {
        void SetName(string name);
        void SetClass();
        void SetStats();
        Character GetCharacter();
    }

    class WarriorBuilder : ICharacterBuilder
    {
        private Character character;

        public WarriorBuilder()
        {
            character = new Character();
        }

        public void SetName(string name)
        {
            character.Name = name;
        }

        public void SetClass()
        {
            character.Class = "Воин";
        }

        public void SetStats()
        {
            character.Strength = 10;
            character.Intelligence = 3;
            character.Agility = 5;
        }

        public Character GetCharacter()
        {
            return character;
        }
    }

    class MageBuilder : ICharacterBuilder
    {
        private Character character;

        public MageBuilder()
        {
            character = new Character();
        }

        public void SetName(string name)
        {
            character.Name = name;
        }

        public void SetClass()
        {
            character.Class = "Маг";
        }

        public void SetStats()
        {
            character.Strength = 2;
            character.Intelligence = 10;
            character.Agility = 4;
        }

        public Character GetCharacter()
        {
            return character;
        }
    }

    class ArcherBuilder : ICharacterBuilder
    {
        private Character character;

        public ArcherBuilder()
        {
            character = new Character();
        }

        public void SetName(string name)
        {
            character.Name = name;
        }

        public void SetClass()
        {
            character.Class = "Лучник";
        }

        public void SetStats()
        {
            character.Strength = 5;
            character.Intelligence = 4;
            character.Agility = 10;
        }

        public Character GetCharacter()
        {
            return character;
        }
    }

    class CharacterDirector
    {
        private ICharacterBuilder builder;

        public CharacterDirector(ICharacterBuilder builder)
        {
            this.builder = builder;
        }

        public Character Build(string name)
        {
            builder.SetName(name);
            builder.SetClass();
            builder.SetStats();
            return builder.GetCharacter();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CharacterDirector director;

            director = new CharacterDirector(new WarriorBuilder());
            Character warrior = director.Build("Алексей");
            warrior.ShowInfo();

            Console.WriteLine();

            director = new CharacterDirector(new MageBuilder());
            Character mage = director.Build("Елена");
            mage.ShowInfo();

            Console.WriteLine();

            director = new CharacterDirector(new ArcherBuilder());
            Character archer = director.Build("Дмитрий");
            archer.ShowInfo();

            Console.ReadLine();
        }
    }
}