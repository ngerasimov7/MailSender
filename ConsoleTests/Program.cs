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

            message.Subject = "Заголовок письма от " + DateTime.Now;
            message.Body = "Текст тестового письма + " + DateTime.Now;

            SmtpClient client = new SmtpClient("smtp.mail.ru", 25);
            client.EnableSsl = true;

            client.Credentials = new NetworkCredential
            {
                UserName = "nikola20",
                Password = "********"
            };

            client.Send(message);
        }
    }
}   
