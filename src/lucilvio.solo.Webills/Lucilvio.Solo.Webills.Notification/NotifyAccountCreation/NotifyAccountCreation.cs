using System.Threading.Tasks;
using Lucilvio.Solo.Architecture;
using Lucilvio.Solo.Architecture.Handler.Inbox.Component;
using Lucilvio.Solo.Webills.Notifications.Infrastructure;

namespace Lucilvio.Solo.Webills.Notifications.NotifyAccountCreation
{
    [Inbox]
    public record AccountCreatedMessage(string UserName) : Message;

    internal class NotifyAccountCreation : IMessageHandler<AccountCreatedMessage>
    {
        private readonly INotificationService _notificationService;

        public NotifyAccountCreation(INotificationService notificationService)
        {
            this._notificationService = notificationService ?? throw new System.ArgumentNullException(nameof(notificationService));
        }

        public Task Execute(AccountCreatedMessage message)
        {
            var f = message.UserName;
            return Task.CompletedTask;
        }
    }
}