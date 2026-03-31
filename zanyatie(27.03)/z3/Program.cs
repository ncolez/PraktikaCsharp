using System;
using System.Collections.Generic;

namespace z4
{
    interface IChatUser
    {
        void Update(string message);
        string GetName();
    }

    class User : IChatUser
    {
        private string name;

        public User(string name)
        {
            this.name = name;
        }

        public void Update(string message)
        {
            Console.WriteLine(name + " получил: " + message);
        }

        public string GetName()
        {
            return name;
        }
    }

    class ChatRoom
    {
        private List<IChatUser> users;

        public ChatRoom()
        {
            users = new List<IChatUser>();
        }

        public void Subscribe(IChatUser user)
        {
            users.Add(user);
            Console.WriteLine(user.GetName() + " подписался");
        }

        public void Unsubscribe(IChatUser user)
        {
            users.Remove(user);
            Console.WriteLine(user.GetName() + " отписался");
        }

        public void SendMessage(string message)
        {
            Console.WriteLine("\nСообщение: " + message);
            for (int i = 0; i < users.Count; i++)
            {
                users[i].Update(message);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ChatRoom chat = new ChatRoom();

            User user1 = new User("Анна");
            User user2 = new User("Петр");
            User user3 = new User("Иван");

            chat.Subscribe(user1);
            chat.Subscribe(user2);
            chat.Subscribe(user3);

            chat.SendMessage("Всем привет!");

            chat.Unsubscribe(user2);

            chat.SendMessage("Как дела?");

            Console.ReadLine();
        }
    }
}