using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UserAccount.Infrastructure;

namespace Lucilvio.Solo.Webills.UserAccount.Infraestructure
{
    internal interface IUseCase<TMessage> where TMessage : Message
    {
        Task Execute(TMessage message);
    }
}
