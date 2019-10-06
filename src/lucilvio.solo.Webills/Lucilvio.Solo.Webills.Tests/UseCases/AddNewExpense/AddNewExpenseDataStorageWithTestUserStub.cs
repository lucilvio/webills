using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.AddNewExpense;

namespace Lucilvio.Solo.Webills.Tests.UseCases.AddNewExpense
{
    public class AddNewExpenseDataStorageWithTestUserStub : IAddNewExpenseDataStorage
    {
        public User GetUser()
        {
            return new User("Test User");
        }

        public void Persist(User user)
        {
        }
    }
}