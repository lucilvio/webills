using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Notifications.Dominio;
using Lucilvio.Solo.Webills.Notifications.Infrastructure;

namespace Lucilvio.Solo.Webills.Notifications.NotifyAccountCreation
{
    internal class NotificationServiceByEmail : INotificationService
    {
        private readonly string _host;
        private readonly int _port;
        private readonly string _user;
        private readonly string _password;

        public NotificationServiceByEmail(string host, int port, string user, string password)
        {
            this._host = host;
            this._port = port;
            this._user = user;
            this._password = password;
        }

        public async Task Send(Notification notification)
        {
            var smtp = new SmtpClient(this._host, this._port)
            {
                Credentials = new NetworkCredential(this._user, this._password),
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