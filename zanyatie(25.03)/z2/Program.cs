using System;

namespace z2
{
    class MyQueue<T>
    {
        private T[] items;
        private int head;
        private int tail;
        private int count;

        public MyQueue()
        {
            items = new T[4];
            head = 0;
            tail = 0;
            count = 0;
        }

        public void Enqueue(T item)
        {
            if (count == items.Length)
            {
                T[] newItems = new T[items.Length * 2];
                for (int i = 0; i < count; i++)
                {
                    newItems[i] = items[(head + i) % items.Length];
                }
                items = newItems;
                head = 0;
                tail = count;
            }

            items[tail] = item;
            tail = (tail + 1) % items.Length;
            count++;
        }

        public T Dequeue()
        {
            if (count == 0)
            {
                throw new InvalidOperationException("Очередь пуста");
            }

            T item = items[head];
            items[head] = default(T)!;
            head = (head + 1) % items.Length;
            count--;
            return item;
        }

        public T Peek()
        {
            if (count == 0)
            {
                throw new InvalidOperationException("Очередь пуста");
            }

            return items[head];
        }

        public int Count()
        {
            return count;
        }
    }

    class QueueProcessor<T>
    {
        private MyQueue<T> queue;

        public QueueProcessor()
        {
            queue = new MyQueue<T>();
        }

        public void AddTask(T task)
        {
            queue.Enqueue(task);
            Console.WriteLine("Задача добавлена: " + task);
        }

        public void ProcessTask()
        {
            if (queue.Count() > 0)
            {
                T task = queue.Dequeue();
                Console.WriteLine("Задача обработана: " + task);
            }
            else
            {
                Console.WriteLine("Нет задач для обработки");
            }
        }

        public void ShowTasks()
        {
            Console.WriteLine("Осталось задач: " + queue.Count());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            QueueProcessor<string> processor = new QueueProcessor<string>();

            processor.AddTask("Задача 1");
            processor.AddTask("Задача 2");
            processor.AddTask("Задача 3");

            processor.ShowTasks();

            processor.ProcessTask();
            processor.ProcessTask();

            processor.ShowTasks();

            Console.ReadLine();
        }
    }
}