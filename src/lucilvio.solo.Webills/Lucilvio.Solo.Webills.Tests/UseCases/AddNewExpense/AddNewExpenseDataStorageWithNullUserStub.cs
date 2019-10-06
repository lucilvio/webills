using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.AddNewExpense;

namespace Lucilvio.Solo.Webills.Tests.UseCases.AddNewExpense
{
    public class AddNewExpenseDataStorageWithNullUserStub : IAddNewExpenseDataStorage
    {
        public User GetUser()
        {
            return null;
        }

        public void Persist(User user)
        {
        }
    }
}