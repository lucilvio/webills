using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Notifications.Dominio;

namespace Lucilvio.Solo.Webills.Notifications.Infrastructure
{
    internal interface INotificationService
    {
        Task Send(Notification notification);
    }
}