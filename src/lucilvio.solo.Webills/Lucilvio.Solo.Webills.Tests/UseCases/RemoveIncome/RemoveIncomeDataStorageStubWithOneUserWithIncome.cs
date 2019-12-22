using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.RemoveIncome;

namespace Lucilvio.Solo.Webills.Tests.UseCases.RemoveIncome
{
    internal class RemoveIncomeDataStorageStubWithOneUserWithIncome : IRemoveIncomeDataStorage
    {
        private readonly User _user;

        public RemoveIncomeDataStorageStubWithOneUserWithIncome()
        {
            this._user = new User("Test User", new Login("user@mail.com"), new Password("123456"));
            this._user.AddIncome("Test Income", DateTime.Now, TransactionValue.Zero);
        }

        public async Task<User> GetUser()
        {
            return this._user;
        }

        public async Task Persist()
        {
        }
    }
}