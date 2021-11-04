using System.Threading.Tasks;
using Lucilvio.Solo.Architecture;

namespace Lucilvio.Solo.Webills.Notifications
{
    public interface INotificationModule : IMessageSender { }

    internal class NotificationsModule : INotificationModule
    {
        public NotificationsModule(IModuleResolver<INotificationModule> resolver)
        {
        }

        public Task SendMessage(Message message)
        {
            throw new System.NotImplementedException();
        }
    }
}