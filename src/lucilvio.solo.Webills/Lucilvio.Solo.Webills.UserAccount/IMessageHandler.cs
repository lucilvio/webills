using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.UserAccount
{
    internal interface IMessageHandler<in TMessage>
    {
        Task<dynamic> Execute(TMessage message);
    }
}