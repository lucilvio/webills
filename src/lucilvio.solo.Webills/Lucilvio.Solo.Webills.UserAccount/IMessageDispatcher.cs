using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.UserAccount
{
    internal interface IMessageDispatcher
    {
        Task<dynamic> Dispatch(object message, Configurations configurations);
    }
}