using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.UserAccount
{
    internal interface IMessageResolver
    {
        Task Resolve(Module.Messages message, object input);
    }
}
