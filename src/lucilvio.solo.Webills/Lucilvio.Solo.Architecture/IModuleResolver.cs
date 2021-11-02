using System.Threading.Tasks;

namespace Lucilvio.Solo.Architecture
{

    public interface IModuleResolver
    {
        Task ResolveMessage(Message message);
        Task ResolveEvent(Event @event);
    }

    public interface IModuleResolver<TModule> : IModuleResolver where TModule : IModule { }
}