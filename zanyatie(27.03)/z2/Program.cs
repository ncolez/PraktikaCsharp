using System;

namespace z2
{
    interface IPaymentStrategy
    {
        void Pay(double amount);
    }

    class CreditCardPayment : IPaymentStrategy
    {
        public void Pay(double amount)
        {
            Console.WriteLine("Оплата кредитной картой: " + amount + " руб.");
        }
    }

    class PayPalPayment : IPaymentStrategy
    {
        public void Pay(double amount)
        {
            Console.WriteLine("Оплата через PayPal: " + amount + " руб.");
        }
    }

    class BitcoinPayment : IPaymentStrategy
    {
        public void Pay(double amount)
        {
            Console.WriteLine("Оплата Bitcoin: " + amount + " руб.");
        }
    }

    class PaymentProcessor
    {
        private IPaymentStrategy? strategy;

        public void SetStrategy(IPaymentStrategy strategy)
        {
            this.strategy = strategy;
        }

        public void ProcessPayment(double amount)
        {
            if (strategy != null)
            {
                strategy.Pay(amount);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PaymentProcessor processor = new PaymentProcessor();

            processor.SetStrategy(new CreditCardPayment());
            processor.ProcessPayment(1000);

            processor.SetStrategy(new PayPalPayment());
            processor.ProcessPayment(2500);

            processor.SetStrategy(new BitcoinPayment());
            processor.ProcessPayment(500);

            Console.ReadLine();
        }
    }
}