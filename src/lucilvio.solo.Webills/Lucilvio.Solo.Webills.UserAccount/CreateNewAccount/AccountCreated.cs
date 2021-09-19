using Lucilvio.Solo.Webills.EventBus;

namespace Lucilvio.Solo.Webills.UserAccount.CreateNewAccount
{
    public class AccountCreated : Event
    {
        public AccountCreated(object payload) : base(nameof(AccountCreated), nameof(CreateNewAccount), payload) { }
    }
}