using System.Threading.Tasks;

namespace Lucilvio.Solo.Architecture
{
    public class HandlerDynamicProxy<TMessage> where TMessage : Message
    {
        public async Task Execute(IHandler<TMessage> handler, dynamic message)
        {
            await handler.Execute(message);
        }
    }
}