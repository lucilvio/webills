using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Bus;

namespace Lucilvio.Solo.Webills.UserAccount
{
    public class UserAccountModule
    {
        IUseCaseResolver _useCaseResolver;

        public UserAccountModule(IBus bus)
        {
            this._useCaseResolver = new UserCaseResolverBySimpleInjector(bus);
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