using System.Collections.Generic;
using MailSender.Models;


namespace MailSender.Interfaces
{
    public interface IStorage<T>
    {
        ICollection<T> Items { get; }
        // Метод понадобится для того чтобы считать данные из файла/БД
        void Load();
        // Метод понадобится для того чтобы записать данные в файл/БД
        void SaveChanges();
    }
    public interface IServerStorage : IStorage<Server> { }
    public interface ISendersStorage : IStorage<Sender> { }
    public interface IRecipientsStorage : IStorage<Recipient> { }
    public interface IMessagesStorage : IStorage<Message> { }
}
