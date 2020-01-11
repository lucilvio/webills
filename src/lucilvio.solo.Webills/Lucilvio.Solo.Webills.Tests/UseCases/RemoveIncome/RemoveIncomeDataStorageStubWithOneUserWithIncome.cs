using Lucilvio.Solo.Webills.Core.Domain.User;
using Lucilvio.Solo.Webills.Core.UseCases.RemoveIncome;
using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Tests.UseCases.RemoveIncome
{
    internal class RemoveIncomeDataStorageStubWithOneUserWithIncome : IRemoveIncomeDataStorage
    {
        private readonly User _user;

        public RemoveIncomeDataStorageStubWithOneUserWithIncome()
        {
            this._user = new User("Test User");
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