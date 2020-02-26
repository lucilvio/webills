using System;
using Lucilvio.Solo.Webills.Transactions.CreateUser;
using Lucilvio.Solo.Webills.UserAccount.CreateUserAccount;

namespace Lucilvio.Solo.Webills.Web.SignUp
{
    internal class CreateUserInputAdapter : ICreateUserInput
    {
        private UserAccountCreated _userAccountCreated;

        public CreateUserInputAdapter(UserAccountCreated userAccountCreated)
        {
            this._userAccountCreated = userAccountCreated;
        }

        public Guid Id => this._userAccountCreated.Id;
    }
}