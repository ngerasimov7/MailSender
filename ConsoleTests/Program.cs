using System;
using System.Net;
using System.Net.Mail;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            MailAddress to = new MailAddress("nikola20296@gmail.com", "Герасимов Николай");
            MailAddress from = new MailAddress("nikola20@mail.ru", "Герасимов Николай");

            MailMessage message = new MailMessage(from, to);
            //var msg = new MailAddress("user@server.ru", "qwe@ASD.ru");

            message.Subject = "Заголовок письма от " + DateTime.Now;
            message.Body = "Текст тестового письма + " + DateTime.Now;

            var client = new SmtpClient("smtp.mail.ru", 25);
            client.EnableSsl = true;

            client.Credentials = new NetworkCredential
            {
                UserName = "nikola20",
                Password = "Celebrate1509"
            };

            client.Send(message);

            Console.WriteLine("Hello World!");

        }
    }
}
