using System.Threading.Tasks;

namespace Lucilvio.Solo.Architecture.Handler.Inbox
{
    public interface IInbox<in TMessage> where TMessage : Message
    {
        Task Execute(TMessage message, Event @event);
    }
}