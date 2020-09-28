using System;
using System.Windows;
using System.Net;
using System.Net.Mail;
using System.Windows.Interop;
using MailSender.Models;
using MailSender.lib;

namespace MailSender
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnSendButtonClick(object Sender, RoutedEventArgs e)
        {
            if (!(SendersList.SelectedItem is Sender sender)) return;
            if (!(RecipientsList.SelectedItem is Recipient recipient)) return;
            if (!(ServersList.SelectedItem is Server server)) return;
            if (!(MessagesList.SelectedItem is Message message)) return;

            var send_service = new MailSenderService
            {
                ServerAdress = server.Address,
                ServerPort = server.Port,
                UseSSL = server.UseSSL,
                Login = server.Login,
                Password = server.Password,
            };

            try
            {
                send_service.SendMessage(sender.Address, recipient.Address, message.Subject, message.Body);
            }
            catch(SmtpException error)
            {
                MessageBox.Show("Ошибка при отправке почты" + error.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);)
            }
        }


        /*private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MailAddress to = new MailAddress(adressTo.Text);
                MailAddress from = new MailAddress(adressFrom.Text);

                MailMessage message = new MailMessage(from, to);

                message.Subject = Subject.Text;
                message.Body = massageBody.Text;

                SmtpClient client = new SmtpClient(smtpServer.Text, int.Parse(smtpPort.Text));
                client.EnableSsl = true;

                client.Credentials = new NetworkCredential
                {
                    UserName= login.Text,
                    Password = pass.Text
                    //Password = passBx.SecurePassword
                };
            
                client.Send(message);
                MessageBox.Show("ОК");
            }
            catch
            {
                MessageBox.Show("Опаньки! Что-то пошло не так!");

            }
            
        }*/
    }
}
