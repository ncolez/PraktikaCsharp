using System;
using System.IO;

namespace FileOperations
{
    class FileManager
    {
        public void CreateFile(string path, string content)
        {
            File.WriteAllText(path, content);
            Console.WriteLine("Файл создан: " + path);
        }

        public void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
                Console.WriteLine("Файл удален: " + path);
            }
            else
            {
                Console.WriteLine("Файл не найден: " + path);
            }
        }

        public void CopyFile(string source, string dest)
        {
            if (File.Exists(source))
            {
                File.Copy(source, dest, true);
                Console.WriteLine("Файл скопирован: " + source + " -> " + dest);
            }
            else
            {
                Console.WriteLine("Исходный файл не найден");
            }
        }

        public void MoveFile(string source, string dest)
        {
            if (File.Exists(source))
            {
                File.Move(source, dest);
                Console.WriteLine("Файл перемещен: " + source + " -> " + dest);
            }
            else
            {
                Console.WriteLine("Исходный файл не найден");
            }
        }

        public void RenameFile(string oldPath, string newPath)
        {
            MoveFile(oldPath, newPath);
        }

        public void DeleteFilesByPattern(string directory, string pattern)
        {
            string[] files = Directory.GetFiles(directory, pattern);
            foreach (string file in files)
            {
                File.Delete(file);
                Console.WriteLine("Удален: " + file);
            }
        }

        public void ListFiles(string directory)
        {
            string[] files = Directory.GetFiles(directory);
            Console.WriteLine("Файлы в директории " + directory + ":");
            foreach (string file in files)
            {
                Console.WriteLine(Path.GetFileName(file));
            }
        }

        public void SetReadOnly(string path, bool isReadOnly)
        {
            FileInfo info = new FileInfo(path);
            info.IsReadOnly = isReadOnly;
            Console.WriteLine("Права доступа изменены для: " + path);
        }
    }

    class FileInfoProvider
    {
        public void GetInfo(string path)
        {
            if (File.Exists(path))
            {
                FileInfo info = new FileInfo(path);
                Console.WriteLine("Файл: " + path);
                Console.WriteLine("Размер: " + info.Length + " байт");
                Console.WriteLine("Создан: " + info.CreationTime);
                Console.WriteLine("Изменен: " + info.LastWriteTime);
            }
            else
            {
                Console.WriteLine("Файл не найден");
            }
        }

        public bool CompareBySize(string file1, string file2)
        {
            if (File.Exists(file1) && File.Exists(file2))
            {
                FileInfo info1 = new FileInfo(file1);
                FileInfo info2 = new FileInfo(file2);
                return info1.Length == info2.Length;
            }
            return false;
        }

        public void CheckPermissions(string path)
        {
            Console.WriteLine("Проверка прав для: " + path);
            Console.WriteLine("Существует: " + File.Exists(path));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "Builova.ii";
            string copyName = "Builova_copy.ii";

            FileManager fm = new FileManager();
            FileInfoProvider fip = new FileInfoProvider();

            fm.CreateFile(fileName, "Hello World!");
            string content = File.ReadAllText(fileName);
            Console.WriteLine("Содержимое файла: " + content);

            if (File.Exists(fileName))
            {
                Console.WriteLine("Файл существует");
            }

            fip.GetInfo(fileName);

            fm.CopyFile(fileName, copyName);

            fm.MoveFile(copyName, "moved_" + copyName);

            fm.RenameFile("moved_" + copyName, "Builova.io");

            fm.DeleteFile("notexist.ii");

            bool equal = fip.CompareBySize(fileName, "Builova.io");
            Console.WriteLine("Файлы одинакового размера: " + equal);

            fm.DeleteFilesByPattern(".", "*.ii");

            fm.ListFiles(".");

            fm.CreateFile("test.txt", "test");
            fm.SetReadOnly("test.txt", true);

            fip.CheckPermissions("test.txt");

            Console.ReadLine();
        }
    }
}