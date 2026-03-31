using System;
using System.IO;
using System.Collections.Generic;

namespace FileReadTask3
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

    class LogFileReader
    {
        private string filePath;

        public LogFileReader(string path)
        {
            filePath = path;
        }

        public List<LogEntry> ReadLogs()
        {
            List<LogEntry> logs = new List<LogEntry>();

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split('|');
                    if (parts.Length == 2)
                    {
                        DateTime date = DateTime.Parse(parts[0]);
                        logs.Add(new LogEntry(date, parts[1]));
                    }
                }
            }
            return logs;
        }
    }

    class LogProcessor
    {
        public List<LogEntry> FilterLogsByDate(List<LogEntry> logs, DateTime fromDate, DateTime toDate)
        {
            List<LogEntry> result = new List<LogEntry>();
            foreach (LogEntry log in logs)
            {
                if (log.Date >= fromDate && log.Date <= toDate)
                {
                    result.Add(log);
                }
            }
            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            LogFileReader reader = new LogFileReader("file.data");
            LogProcessor processor = new LogProcessor();

            List<LogEntry> logs = reader.ReadLogs();

            DateTime from = DateTime.Now.AddDays(-1);
            DateTime to = DateTime.Now.AddDays(1);

            List<LogEntry> filtered = processor.FilterLogsByDate(logs, from, to);

            Console.WriteLine("Отфильтрованные логи:");
            foreach (LogEntry log in filtered)
            {
                Console.WriteLine(log.Date + " - " + log.Message);
            }

            Console.ReadLine();
        }
    }
}