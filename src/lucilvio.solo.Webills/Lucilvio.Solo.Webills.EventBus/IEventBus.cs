using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.EventBus
{
    public interface IEventBus
    {
        void Subscibe(string @event, Func<dynamic, Task> reaction);
        void Publish(string @event, object onLoginInput);
    }
}