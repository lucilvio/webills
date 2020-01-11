using Lucilvio.Solo.Webills.Core.Domain.User;
using Lucilvio.Solo.Webills.Core.UseCases.EditExpense;
using System;
using System.Threading.Tasks;

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