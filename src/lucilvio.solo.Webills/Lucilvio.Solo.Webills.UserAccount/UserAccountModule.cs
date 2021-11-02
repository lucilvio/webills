using Lucilvio.Solo.Architecture;

namespace Lucilvio.Solo.Webills.UserAccount
{
    internal class UserAccountModule : Module, IUserAccountModule
    {
        public UserAccountModule(IModuleResolver<IUserAccountModule> resolver) 
            : base(resolver)
        {
        }
    }
}