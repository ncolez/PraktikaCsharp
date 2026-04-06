using System;

namespace z2_decorator
{
    interface IWeapon
    {
        string GetDescription();
        int GetDamage();
    }

    class BasicWeapon : IWeapon
    {
        public string GetDescription()
        {
            return "Обычное оружие";
        }

        public int GetDamage()
        {
            return 10;
        }
    }

    abstract class WeaponDecorator : IWeapon
    {
        protected IWeapon _weapon;

        public WeaponDecorator(IWeapon weapon)
        {
            _weapon = weapon;
        }

        public abstract string GetDescription();
        public abstract int GetDamage();
    }

    class FireEnhancementDecorator : WeaponDecorator
    {
        public FireEnhancementDecorator(IWeapon weapon) : base(weapon) { }

        public override string GetDescription()
        {
            return _weapon.GetDescription() + " с огненной атакой";
        }

        public override int GetDamage()
        {
            return _weapon.GetDamage() + 15;
        }
    }

    class IceEnhancementDecorator : WeaponDecorator
    {
        public IceEnhancementDecorator(IWeapon weapon) : base(weapon) { }

        public override string GetDescription()
        {
            return _weapon.GetDescription() + " с ледяной атакой";
        }

        public override int GetDamage()
        {
            return _weapon.GetDamage() + 12;
        }
    }

    class CriticalHitDecorator : WeaponDecorator
    {
        public CriticalHitDecorator(IWeapon weapon) : base(weapon) { }

        public override string GetDescription()
        {
            return _weapon.GetDescription() + " с крит. ударом";
        }

        public override int GetDamage()
        {
            return _weapon.GetDamage() + 20;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IWeapon weapon = new BasicWeapon();
            Console.WriteLine(weapon.GetDescription() + " урон: " + weapon.GetDamage());

            weapon = new FireEnhancementDecorator(weapon);
            Console.WriteLine(weapon.GetDescription() + " урон: " + weapon.GetDamage());

            weapon = new CriticalHitDecorator(weapon);
            Console.WriteLine(weapon.GetDescription() + " урон: " + weapon.GetDamage());

            Console.ReadLine();
        }
    }
}