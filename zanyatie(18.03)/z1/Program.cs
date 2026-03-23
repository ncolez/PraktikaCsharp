using System;

namespace z1
{
    class A
    {
        public int a;
        public int b;

        public A(int a, int b)
        {
            this.a = a;
            this.b = b;
        }

        public int Multiply()
        {
            return a * b;
        }

        public double Expression()
        {
            return Math.Sqrt(b) / (2 * a);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите a: ");
            int a = Convert.ToInt32(Console.ReadLine());

            Console.Write("Введите b: ");
            int b = Convert.ToInt32(Console.ReadLine());

            A obj = new A(a, b);

            Console.WriteLine("Произведение a и b: " + obj.Multiply());
            Console.WriteLine("Значение выражения √b / (2a): " + obj.Expression());

            Console.ReadLine();
        }
    }
}