using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.EditExpense;

namespace Lucilvio.Solo.Webills.Tests.UseCases.EditExpenses
{
    internal class EditExpenseDataStorageStubWithOneUserWithExpense : IEditExpenseDataStorage
    {
        private readonly User _user;

        public EditExpenseDataStorageStubWithOneUserWithExpense()
        {
            this._user = new User("Test User", new Login("user@mail.com"), new Password("123456"));
            this._user.AddExpense("Test expanse", Category.Education, new DateTime(2000, 10, 10), TransactionValue.Zero);
        }

        public async Task<User> GetUser()
        {
            return this._user;
        }

        public async Task Persist(Guid incomeNumber, User foundUser)
        {
        }
    }
}