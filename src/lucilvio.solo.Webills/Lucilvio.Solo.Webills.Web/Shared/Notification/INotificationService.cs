using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Clients.Web.Shared.Notification
{
    public interface INotificationService
    {
        Task Send(Notification notification);
    }
}