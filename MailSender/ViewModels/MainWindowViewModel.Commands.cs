using System.Linq;
using System.Windows;
using System.Windows.Input;
using MailSender.Infrastructure.Commands;
using MailSender.lib.Models;

namespace MailSender.ViewModels
{
    partial class MainWindowViewModel
    {
        #region Команды
        #region Команды сервера
        #region CreateNewServerCommand

        private ICommand _CreateNewServerCommand;

        public ICommand CreateNewServerCommand => _CreateNewServerCommand
            ??= new LambdaCommand(OnCreateNewServerCommandExecuted, CanCreateNewServerCommandExecute);

        private bool CanCreateNewServerCommandExecute(object p) => true;

        private void OnCreateNewServerCommandExecuted(object p)
        {
            // Основное действие, выполняемое командой, описывается здесь!!!

            MessageBox.Show("Создание нового сервера!", "Управление серверами");
        }

        #endregion

        #region EditServerCommand

        private ICommand _EditServerCommand;

        public ICommand EditServerCommand => _EditServerCommand
            ??= new LambdaCommand(OnEditServerCommandExecuted, CanEditServerCommandExecute);

        private bool CanEditServerCommandExecute(object p) => p is Server || SelectedServer != null;

        private void OnEditServerCommandExecuted(object p)
        {
            var server = p as Server ?? SelectedServer;
            if (server is null) return;

            MessageBox.Show($"Редактирование сервера {server.Address}!", "Управление серверами");
        }

        #endregion

        #region DeleteServerCommand

        private ICommand _DeleteServerCommand;

        public ICommand DeleteServerCommand => _DeleteServerCommand
            ??= new LambdaCommand(OnDeleteServerCommandExecuted, CanDeleteServerCommandExecute);

        private bool CanDeleteServerCommandExecute(object p) => p is Server || SelectedServer != null;

        private void OnDeleteServerCommandExecuted(object p)
        {
            var server = p as Server ?? SelectedServer;
            if (server is null) return;

            Servers.Remove(server);
            SelectedServer = Servers.FirstOrDefault();
        }

        #endregion
        #endregion

        #region Команды Отправителей
        #region CreateNewSenderCommand

        private ICommand _CreateNewSenderCommand;

        public ICommand CreateNewSenderCommand => _CreateNewSenderCommand
            ??= new LambdaCommand(OnCreateNewSenderCommandExecuted, CanCreateNewSenderCommandExecute);

        private bool CanCreateNewSenderCommandExecute(object p) => true;

        private void OnCreateNewSenderCommandExecuted(object p)
        {
            // Основное действие, выполняемое командой, описывается здесь!!!

            MessageBox.Show("Создание нового Отправителя!", "Управление Отправителями");
        }

        #endregion

        #region EditSenderCommand

        private ICommand _EditSenderCommand;

        public ICommand EditSenderCommand => _EditSenderCommand
            ??= new LambdaCommand(OnEditSenderCommandExecuted, CanEditSenderCommandExecute);

        private bool CanEditSenderCommandExecute(object p) => p is Sender || SelectedSender != null;

        private void OnEditSenderCommandExecuted(object p)
        {
            var sender = p as Sender ?? SelectedSender;
            if (sender is null) return;

            MessageBox.Show($"Редактирование Отправителя {sender.Name}!", "Управление Отправителями");
        }

        #endregion

        #region DeleteSenderCommand

        private ICommand _DeleteSenderCommand;

        public ICommand DeleteSenderCommand => _DeleteSenderCommand
            ??= new LambdaCommand(OnDeleteSenderCommandExecuted, CanDeleteSenderCommandExecute);

        private bool CanDeleteSenderCommandExecute(object p) => p is Sender || SelectedSender != null;

        private void OnDeleteSenderCommandExecuted(object p)
        {
            var sender = p as Sender ?? SelectedSender;
            if (sender is null) return;

            Senders.Remove(sender);
            SelectedSender = Senders.FirstOrDefault();
        }

        #endregion
        #endregion

        #region Команды Получателей
        #region CreateNewRecipientCommand

        private ICommand _CreateNewRecipientCommand;

        public ICommand CreateNewRecipientCommand => _CreateNewRecipientCommand
            ??= new LambdaCommand(OnCreateNewRecipientCommandExecuted, CanCreateNewRecipientCommandExecute);

        private bool CanCreateNewRecipientCommandExecute(object p) => true;

        private void OnCreateNewRecipientCommandExecuted(object p)
        {
            // Основное действие, выполняемое командой, описывается здесь!!!

            MessageBox.Show("Создание нового Получателя!", "Управление Получателями");
        }

        #endregion

        #region EditRecipientCommand

        private ICommand _EditRecipientCommand;

        public ICommand EditRecipientCommand => _EditRecipientCommand
            ??= new LambdaCommand(OnEditRecipientCommandExecuted, CanEditRecipientCommandExecute);

        private bool CanEditRecipientCommandExecute(object p) => p is Recipient || SelectedRecipient != null;

        private void OnEditRecipientCommandExecuted(object p)
        {
            var recipient = p as Recipient ?? SelectedRecipient;
            if (recipient is null) return;

            MessageBox.Show($"Редактирование Получателя {recipient.Name}!", "Управление Получателями");
        }

        #endregion

        #region DeleteRecipientCommand

        private ICommand _DeleteRecipientCommand;

        public ICommand DeleteRecipientCommand => _DeleteRecipientCommand
            ??= new LambdaCommand(OnDeleteRecipientCommandExecuted, CanDeleteRecipientCommandExecute);

        private bool CanDeleteRecipientCommandExecute(object p) => p is Recipient || SelectedRecipient != null;

        private void OnDeleteRecipientCommandExecuted(object p)
        {
            var recipient = p as Recipient ?? SelectedRecipient;
            if (recipient is null) return;

            Recipients.Remove(recipient);
            SelectedRecipient = Recipients.FirstOrDefault();
        }

        #endregion
        #endregion

        #region Command SendMailCommand - Отправка почты

        /// <summary>Отправка почты</summary>
        private ICommand _SendMailCommand;

        /// <summary>Отправка почты</summary>
        public ICommand SendMailCommand => _SendMailCommand
            ??= new LambdaCommand(OnSendMailCommandExecuted, CanSendMailCommandExecute);

        /// <summary>Проверка возможности выполнения - Отправка почты</summary>
        private bool CanSendMailCommandExecute(object p)
        {
            if (SelectedServer is null) return false;
            if (SelectedSender is null) return false;
            if (SelectedRecipient is null) return false;
            if (SelectedMessage is null) return false;
            return true;
        }

        /// <summary>Логика выполнения - Отправка почты</summary>
        private void OnSendMailCommandExecuted(object p)
        {
            var server = SelectedServer;
            var sender = SelectedSender;
            var recipient = SelectedRecipient;
            var message = SelectedMessage;

            var mail_sender = _MailService.GetSender(server.Address, server.Port, server.UseSSL, server.Login, server.Password);
            mail_sender.Send(sender.Address, recipient.Address, message.Subject, message.Body);

            Statistic.MessageSended();
        }

        #endregion

        #endregion
    }
}