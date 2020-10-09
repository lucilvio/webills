using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.UserAccount
{
    internal interface IEventBus
    {
        void Subscibe(Module.Events @event, Func<object, Task> reaction);
        void Publish(Module.Events @event, object onLoginInput);
    }
}