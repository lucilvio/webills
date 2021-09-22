using System.Threading.Tasks;
using Lucilvio.Solo.Architecture;

namespace Lucilvio.Solo.Webills.Notification.NotifyAccountCreation
{
    public record AccountCreatedMessage(string UserName) : Message;

    internal class NotifyAccountCreation : IHandler<AccountCreatedMessage>
    {
        public async Task Execute(AccountCreatedMessage message)
        {
            var f = message.UserName;
            return;
        }
    }
}