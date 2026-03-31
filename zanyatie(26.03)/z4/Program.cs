using System;
using System.IO;
using System.Threading;

namespace FileWatcherTask4
{
    class FileWatcher
    {
        private FileSystemWatcher watcher;
        private string logFile = "log.txt";
        private bool isProcessing = false;

        public FileWatcher(string path)
        {
            watcher = new FileSystemWatcher(path);
            watcher.Created += OnChanged;
            watcher.Deleted += OnChanged;
            watcher.Changed += OnChanged;
            watcher.Renamed += OnRenamed;
            watcher.EnableRaisingEvents = true;
        }

        private void Log(string message)
        {
            string logMessage = DateTime.Now + " - " + message;
            Console.WriteLine(logMessage);
            File.AppendAllText(logFile, logMessage + Environment.NewLine);
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.Name == logFile)
            {
                return;
            }

            if (isProcessing) return;
            isProcessing = true;

            Thread.Sleep(100);
            Log(e.ChangeType + ": " + e.Name);

            isProcessing = false;
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            if (e.Name == logFile || e.OldName == logFile)
            {
                return;
            }

            if (isProcessing) return;
            isProcessing = true;

            Thread.Sleep(100);
            Log("Renamed: " + e.OldName + " -> " + e.Name);

            isProcessing = false;
        }

        public void Stop()
        {
            watcher.EnableRaisingEvents = false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string directory = @".";

            string testFile = "test.txt";
            if (!File.Exists(testFile))
            {
                File.WriteAllText(testFile, "Тестовый файл");
            }
            Console.WriteLine("Тестовый файл: " + testFile);

            Console.WriteLine("Отслеживание папки: " + Path.GetFullPath(directory));
            Console.WriteLine("Попробуйте создать, удалить, изменить или переименовать файл");
            Console.WriteLine("(не трогайте файл log.txt)");

            FileWatcher watcher = new FileWatcher(directory);

            Console.WriteLine("Нажмите Enter для выхода...");
            Console.ReadLine();

            watcher.Stop();
        }
    }
}