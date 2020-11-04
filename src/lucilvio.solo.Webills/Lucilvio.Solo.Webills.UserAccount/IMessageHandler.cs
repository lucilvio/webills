using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.UserAccount
{
    internal interface IMessageHandler<TMessage> where TMessage : IMessage
    {
        Task<dynamic> Execute(TMessage message);
    }
}
