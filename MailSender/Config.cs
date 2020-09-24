using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender
{
    public  class Config
    {
        public  string mailFrom = "nikola20@mail.ru";
        public  string nameFrom = "Николай Герасимов";
        public  string login = "nikola20";
        public  string password = "***";
        public  string smtpServer = "smtp.mail.ru";
        public  int smtpPort = 25;
    }
}
