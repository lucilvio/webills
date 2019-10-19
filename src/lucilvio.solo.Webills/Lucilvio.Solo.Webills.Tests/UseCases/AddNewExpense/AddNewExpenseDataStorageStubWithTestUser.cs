using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.AddNewExpense;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Tests.UseCases.AddNewExpense
{
    public class AddNewExpenseDataStorageStubWithTestUser : IAddNewExpenseDataStorage
    {
        public async Task<User> GetUser()
        {
            return new User("Test User");
        }

        public async Task Persist(User user)
        {
            return;
        }
    }
}