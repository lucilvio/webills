using System.Threading.Tasks;

namespace Lucilvio.Solo.Architecture
{
    public interface IEventPublisher
    {
        Task Publish(Event @event);
    }
}