using System;

namespace z3
{
    class EmptyStringException : Exception
    {
        public EmptyStringException(string message) : base(message) { }
    }

    class StringProcessor
    {
        public void ValidateInput(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new EmptyStringException("Строка не может быть пустой или null");
            }
            Console.WriteLine("Строка корректна: " + input);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            StringProcessor processor = new StringProcessor();

            try
            {
                processor.ValidateInput("");
            }
            catch (EmptyStringException ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }

            try
            {
                processor.ValidateInput("Привет");
            }
            catch (EmptyStringException ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }

            Console.ReadLine();
        }
    }
}