using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.EditExpense;

namespace Lucilvio.Solo.Webills.Tests.UseCases.EditExpenses
{
    internal class EditExpenseDataStorageStubWithoutUser : IEditExpenseDataStorage
    {
        public async Task<User> GetUser()
        {
            return null;
        }

        public async Task Persist(Guid incomeNumber, User foundUser)
        {
        }
    }
}