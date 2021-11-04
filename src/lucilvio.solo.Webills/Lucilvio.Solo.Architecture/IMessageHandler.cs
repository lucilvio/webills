using System.Threading.Tasks;

namespace Lucilvio.Solo.Architecture
{
    public interface IMessageHandler<TMessage> where TMessage : Message
    {
        Task Execute(TMessage message);
    }
}