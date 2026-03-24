using System;

namespace z6
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите строку: ");
            string input = Console.ReadLine() ?? "";

            char bestChar = ' ';
            int bestLength = 0;
            int currentLength = 1;

            if (input.Length > 0)
            {
                bestChar = input[0];
                bestLength = 1;

                for (int i = 1; i < input.Length; i++)
                {
                    if (input[i] == input[i - 1])
                    {
                        currentLength++;
                    }
                    else
                    {
                        if (currentLength > bestLength)
                        {
                            bestLength = currentLength;
                            bestChar = input[i - 1];
                        }
                        currentLength = 1;
                    }
                }

                if (currentLength > bestLength)
                {
                    bestLength = currentLength;
                    bestChar = input[input.Length - 1];
                }
            }

            Console.WriteLine("Символ: '" + bestChar + "', длина: " + bestLength);
            Console.ReadLine();
        }
    }
}