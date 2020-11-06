using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Website.Shared.Notification
{
    internal class NotificationByEmailService : INotificationService
    {
        private readonly string _host;
        private readonly int _port;
        private readonly string _user;
        private readonly string _password;

        public NotificationByEmailService(string host, int port, string user, string password)
        {
            _host = host;
            _port = port;
            _user = user;
            _password = password;
        }

        public async Task Send(Notification notification)
        {
            var smtp = new SmtpClient(_host, _port)
            {
                Credentials = new NetworkCredential(_user, _password),
                DeliveryMethod = SmtpDeliveryMethod.Network,                
                EnableSsl = true
            };

            var email = new MailMessage(notification.From.Mail, notification.To.Mail, notification.Subject, notification.Message)
            {
                IsBodyHtml = true                
            };

            await smtp.SendMailAsync(email);
        }
    }
}