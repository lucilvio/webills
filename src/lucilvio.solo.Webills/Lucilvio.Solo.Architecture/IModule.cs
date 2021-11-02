using System.Threading.Tasks;

namespace Lucilvio.Solo.Architecture
{
    public interface IModule
    {
        Task SendMessage(Message message);
        Task HandleEvent(Event @event);
    }

    public class Module : IModule
    {
        private readonly IModuleResolver _moduleResolver;

        public Module(IModuleResolver moduleResolver)
        {
            this._moduleResolver = moduleResolver;
        }

        public virtual Task SendMessage(Message message) =>
            this._moduleResolver.ResolveMessage(message);

        public virtual Task HandleEvent(Event @event) =>
            this._moduleResolver.ResolveEvent(@event);
    }
}