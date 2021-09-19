using System.Threading.Tasks;

namespace Lucilvio.Solo.Architecture
{
    public interface IHandler<TMessage> where TMessage : Message
    {
        Task Execute(TMessage message);
    }
}
