using Lucilvio.Solo.Architecture;

namespace Lucilvio.Solo.Webills.UserAccount.CreateNewAccount
{
    public class AccountCreated : Event
    {
        public AccountCreated(object payload) : base(nameof(AccountCreated), nameof(CreateNewAccount), payload) { }
    }
}