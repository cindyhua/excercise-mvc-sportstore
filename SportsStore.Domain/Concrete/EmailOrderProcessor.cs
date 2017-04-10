using System.Net.Mail;
using System.Text;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System.Net;

namespace SportsStore.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "";
        public string MailFromAddress = "";
        public bool UseSsl = true;
        public string UserName = "";
        public string Password = "";
        public string ServerName = "";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"\";
    }

    public class EmailOrderProcessor:IOrderProcessor
    {
        public EmailSettings emailsettings;

        public EmailOrderProcessor(EmailSettings settings)
        {
            emailsettings = settings;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingDetails)
        {
            using (var smtpClien = new SmtpClient())
            {

            }
        }
    }
}
