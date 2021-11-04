using System.Threading.Tasks;
using Lucilvio.Solo.Architecture;

namespace Lucilvio.Solo.Webills.UserAccount
{
    internal class UserAccountModule : IUserAccountModule
    {
        private readonly IModuleResolver<IUserAccountModule> _moduleResolver;

        public UserAccountModule(IModuleResolver<IUserAccountModule> moduleResolver)
        {
            this._moduleResolver = moduleResolver;
        }

        public async Task SendMessage(Message message)
        {
            await this._moduleResolver.Resolve(message);
        }
    }
}