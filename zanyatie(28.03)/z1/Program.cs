using System;

namespace z1_factory
{
    interface ICharacter
    {
        void Attack();
    }

    class Mage : ICharacter
    {
        public void Attack()
        {
            Console.WriteLine("Маг атакует заклинанием!");
        }
    }

    class Warrior : ICharacter
    {
        public void Attack()
        {
            Console.WriteLine("Воин атакует мечом!");
        }
    }

    class Archer : ICharacter
    {
        public void Attack()
        {
            Console.WriteLine("Лучник атакует из лука!");
        }
    }

    abstract class CharacterFactory
    {
        public abstract ICharacter CreateCharacter();
    }

    class MageFactory : CharacterFactory
    {
        public override ICharacter CreateCharacter()
        {
            return new Mage();
        }
    }

    class WarriorFactory : CharacterFactory
    {
        public override ICharacter CreateCharacter()
        {
            return new Warrior();
        }
    }

    class ArcherFactory : CharacterFactory
    {
        public override ICharacter CreateCharacter()
        {
            return new Archer();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CharacterFactory factory = new MageFactory();
            ICharacter character = factory.CreateCharacter();
            character.Attack();

            factory = new WarriorFactory();
            character = factory.CreateCharacter();
            character.Attack();

            factory = new ArcherFactory();
            character = factory.CreateCharacter();
            character.Attack();

            Console.ReadLine();
        }
    }
}