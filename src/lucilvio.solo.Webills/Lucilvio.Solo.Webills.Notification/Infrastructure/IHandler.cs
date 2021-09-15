using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Notification.Infrastructure
{
    internal interface IHandler<TMessage> where TMessage : Message
    {
        Task Execute(TMessage message);
    }
}
