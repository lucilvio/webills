using System.Threading.Tasks;
using Lucilvio.Solo.Architecture;

namespace Lucilvio.Solo.Webills.Notifications
{
    public interface INotificationModule : IEventListener { }

    internal class NotificationsModule : INotificationModule
    {
        private readonly IModuleResolver<INotificationModule> _resolver;

        public NotificationsModule(IModuleResolver<INotificationModule> resolver)
        {
            this._resolver = resolver;
        }

        public async Task ListenEvent(Event @event)
        {
            await this._resolver.Resolve(@event);
        }
    }
}