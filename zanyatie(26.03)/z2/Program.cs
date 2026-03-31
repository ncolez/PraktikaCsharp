using System;
using System.IO;
using System.Collections.Generic;

namespace FileWriteTask3
{
    class LogEntry
    {
        public DateTime Date;
        public string Message;

        public LogEntry(DateTime date, string message)
        {
            Date = date;
            Message = message;
        }
    }

    class LogFileWriter
    {
        private string filePath;

        public LogFileWriter(string path)
        {
            filePath = path;
        }

        public void AppendLogEntry(LogEntry entry)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файл не существует, создаем новый");
            }

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(entry.Date.ToString("yyyy-MM-dd HH:mm:ss") + "|" + entry.Message);
            }
            Console.WriteLine("Запись добавлена: " + entry.Message);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            LogFileWriter writer = new LogFileWriter("file.data");

            writer.AppendLogEntry(new LogEntry(DateTime.Now, "Первая запись"));
            writer.AppendLogEntry(new LogEntry(DateTime.Now, "Вторая запись"));

            Console.ReadLine();
        }
    }
}