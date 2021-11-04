using System.Threading.Tasks;

namespace Lucilvio.Solo.Architecture
{
    public class HandlerDynamicProxy<TMessage> where TMessage : Message
    {
        public async Task Execute(IMessageHandler<TMessage> handler, dynamic message)
        {
            await handler.Execute(message);
        }
    }
}