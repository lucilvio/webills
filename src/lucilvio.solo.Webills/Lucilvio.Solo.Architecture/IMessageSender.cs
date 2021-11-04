using System.Threading.Tasks;

namespace Lucilvio.Solo.Architecture
{
    public interface IMessageSender
    {
        Task SendMessage(Message message);
    }
}