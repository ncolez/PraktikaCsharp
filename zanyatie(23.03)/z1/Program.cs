using System;

namespace z1
{
    delegate void NotificationHandler(string message);

    class EmailNotifier
    {
        public void SendEmail(string message)
        {
            Console.WriteLine("Email: " + message);
        }
    }

    class SmsNotifier
    {
        public void SendSms(string message)
        {
            Console.WriteLine("SMS: " + message);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            EmailNotifier email = new EmailNotifier();
            SmsNotifier sms = new SmsNotifier();

            NotificationHandler handler;

            handler = email.SendEmail;
            handler("Это тестовое сообщение");

            handler = sms.SendSms;
            handler("Ваш код подтверждения: 1234");

            Console.ReadLine();
        }
    }
}