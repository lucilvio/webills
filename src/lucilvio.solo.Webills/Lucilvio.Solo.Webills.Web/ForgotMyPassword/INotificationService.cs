using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Clients.Web.ForgotMyPassword
{
    public interface INotificationService
    {
        Task Send(Notification notification);
    }

    public class Notification
    {
        public Notification(Sender from, Receiver to, string message)
        {
            this.From = from;
            this.To = to;
            this.Message = message;
        }

        public Sender From { get; }
        public Receiver To { get; }
        public string Message { get; }

        public class Receiver
        {
            public Receiver(string name, string mail)
            {
                this.Name = name;
                this.Mail = mail;
            }

            public string Name { get; }
            public string Mail { get; }
        }

        public class Sender
        {
            public Sender(string name, string mail)
            {
                this.Name = name;
                this.Mail = mail;
            }

            public string Name { get; }
            public string Mail { get; }
        }
    }

    internal class NotificationByEmailService : INotificationService
    {
        public async Task Send(Notification notification)
        {
            var host = "smtp.sendgrid.net";
            var port = 587;
            var user = "apikey";
            var password = "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";

            var smtp = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(user, password)
            };

            var email = new MailMessage("webills@mail.com", notification.To.Mail, "New Password", notification.Message)
            {
                IsBodyHtml = true
            };

            await smtp.SendMailAsync(email);
        }
    }
}