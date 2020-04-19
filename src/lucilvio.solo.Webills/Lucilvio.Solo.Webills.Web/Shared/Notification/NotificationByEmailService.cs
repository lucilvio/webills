﻿using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Clients.Web.Shared.Notification
{
    internal class NotificationByEmailService : INotificationService
    {
        private readonly string _host;
        private readonly int _port;
        private readonly string _user;
        private readonly string _password;

        public NotificationByEmailService(string host, int port, string user, string password)
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
                Credentials = new NetworkCredential(this._user, this._password)
            };

            var email = new MailMessage("webills@mail.com", notification.To.Mail, "New Password", notification.Message)
            {
                IsBodyHtml = true
            };

            await smtp.SendMailAsync(email);
        }
    }
}