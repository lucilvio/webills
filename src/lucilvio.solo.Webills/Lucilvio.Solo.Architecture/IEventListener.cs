using System.Threading.Tasks;

namespace Lucilvio.Solo.Architecture
{
    public interface IEventListener
    {
        Task ListenEvent(Event @event);
    }
}
