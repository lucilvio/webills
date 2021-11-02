using Lucilvio.Solo.Architecture;

namespace Lucilvio.Solo.Webills.Notifications
{
    public interface INotificationModule : IModule { }

    internal class NotificationsModule : Module, INotificationModule
    {
        public NotificationsModule(IModuleResolver<INotificationModule> resolver) : base(resolver) 
        { 
        }
    }
}