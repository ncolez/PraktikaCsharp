using System;

namespace z1
{
    class InsufficientFundsException : Exception
    {
        public InsufficientFundsException() { }

        public InsufficientFundsException(string message) : base(message) { }

        public InsufficientFundsException(string message, Exception inner) : base(message, inner) { }
    }

    class BankAccount
    {
        private decimal balance;

        public BankAccount(decimal initialBalance)
        {
            balance = initialBalance;
        }

        public void Withdraw(decimal amount)
        {
            if (amount > balance)
            {
                throw new InsufficientFundsException("Недостаточно средств на счете");
            }
            balance = balance - amount;
            Console.WriteLine("Снято: " + amount + ", Остаток: " + balance);
        }

        public decimal GetBalance()
        {
            return balance;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите начальный баланс: ");
            decimal balance = Convert.ToDecimal(Console.ReadLine());
            BankAccount account = new BankAccount(balance);

            Console.Write("Введите сумму для снятия: ");
            decimal amount = Convert.ToDecimal(Console.ReadLine());

            try
            {
                account.Withdraw(amount);
            }
            catch (InsufficientFundsException ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }

            Console.ReadLine();
        }
    }
}