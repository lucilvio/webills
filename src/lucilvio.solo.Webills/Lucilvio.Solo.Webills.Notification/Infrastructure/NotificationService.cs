using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Notifications.Dominio;

namespace Lucilvio.Solo.Webills.Notifications.Infrastructure
{
    internal class NotificationService : INotificationService
    {
        public Task Send(Notification notification)
        {
            return Task.CompletedTask;
        }
    }
}
