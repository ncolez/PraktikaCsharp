using System;

namespace z2
{
    class DatabaseConnectionException : Exception
    {
        public DatabaseConnectionException(string message, Exception inner) : base(message, inner) { }
    }

    class DatabaseConnector
    {
        public void Connect(string connectionString)
        {
            throw new Exception("Ошибка подключения к базе данных");
        }
    }

    class DatabaseManager
    {
        public void OpenConnection(string connectionString)
        {
            try
            {
                DatabaseConnector connector = new DatabaseConnector();
                connector.Connect(connectionString);
            }
            catch (Exception ex)
            {
                throw new DatabaseConnectionException("Не удалось установить соединение с БД", ex);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DatabaseManager manager = new DatabaseManager();

            try
            {
                manager.OpenConnection("invalid_string");
            }
            catch (DatabaseConnectionException ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
                Console.WriteLine("Внутренняя ошибка: " + ex.InnerException.Message);
            }

            Console.ReadLine();
        }
    }
}