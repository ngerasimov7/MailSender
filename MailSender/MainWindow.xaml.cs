using System;
using System.Windows;
using System.Net;
using System.Net.Mail;
using System.Windows.Interop;

namespace MailSender
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Config cfg = new Config();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
            
        }
    }
}
