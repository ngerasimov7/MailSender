using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace MailSender.lib
{
    public class MailSenderService
    {

        public string ServerAdress { get; set; }
        public int ServerPort { get; set; }
        public bool UseSSL   { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public void SendMessage(string SenderAdress, string RecipientAdress, string Subject, string Body)
        {
            MailAddress from = new MailAddress(SenderAdress);
            MailAddress to = new MailAddress(RecipientAdress);

            using (MailMessage message = new MailMessage(from, to))
            {
                message.Subject = Subject;
                message.Body = Body;

                using (SmtpClient client = new SmtpClient(ServerAdress, ServerPort))
                {
                    client.EnableSsl = UseSSL;
                    client.Credentials = new NetworkCredential
                    {
                        UserName = Login,
                        Password = Password
                    };

                    try
                    {
                        client.Send(message);
                    } catch(SmtpException e)
                    {
                        Trace.TraceError(e.ToString());
                        throw;
                    } 
                }
            }
        }
    }
}
