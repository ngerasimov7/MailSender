using System;
using MailSender.lib.Interfaces;
using MailSender.lib.Service;
using MailSender.Interfaces;
using MailSender.ViewModels;
using MailSender.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MailSender
{
    public partial class App
    {
        private static IHost _Hosting;

        public static IHost Hosting => _Hosting
            ??= Host.CreateDefaultBuilder(Environment.GetCommandLineArgs())
               .ConfigureServices(ConfigureServices)
               .Build();

        public static IServiceProvider Services => Hosting.Services;

        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();

#if DEBUG
            services.AddTransient<IMailService, DebugMailService>();
#else
            services.AddTransient<IMailService, SmtpMailService>();
#endif
            // Выбираем либо этот блок
            var memory_store = new DataStorageInMemory();
            services.AddSingleton<IServerStorage>(memory_store);
            services.AddSingleton<ISendersStorage>(memory_store);
            services.AddSingleton<IRecipientsStorage>(memory_store);
            services.AddSingleton<IMessagesStorage>(memory_store);
            
            //либо этот. Один надо закомментировать, другой - раскомментировать
            //const string data_file_name = "MailSenderStorage.xml";
            //var file_storage = new DataStorageInXmlFile(data_file_name);
            //services.AddSingleton<IServerStorage>(file_storage);
            //services.AddSingleton<ISendersStorage>(file_storage);
            //services.AddSingleton<IRecipientsStorage>(file_storage);
            //services.AddSingleton<IMessagesStorage>(file_storage);

        }
    }
}