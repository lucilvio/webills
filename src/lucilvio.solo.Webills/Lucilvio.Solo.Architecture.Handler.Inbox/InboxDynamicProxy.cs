using System.Threading.Tasks;

namespace Lucilvio.Solo.Architecture.Handler.Inbox
{
    public class InboxDynamicProxy<TMessage> where TMessage : Message
    {
        public async Task Execute(IInbox<TMessage> inbox, dynamic message, dynamic @event)
        {
            await inbox.Execute(message, @event);
        }
    }
}
