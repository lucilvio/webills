using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.UserAccount.Infraestructure
{
    internal interface IHandler<TMessage> where TMessage : Message
    {
        Task Execute(TMessage message);
    }
}
