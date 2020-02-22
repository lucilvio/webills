using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.UserAccount
{
    public class UserAccountModule
    {
        IUseCaseResolver _useCaseResolver;

        public UserAccountModule()
        {
            this._useCaseResolver = new UserCaseResolverBySimpleInjector();
        }

        public async Task ExecuteCommand(ICommand command)
        {
            if (command == null)
                throw new Error.CommandNotInformed();

            await this._useCaseResolver.Resolve(command);
        }

        internal class Error
        {
            public class CommandNotInformed : Exception { }
        }
    }
}