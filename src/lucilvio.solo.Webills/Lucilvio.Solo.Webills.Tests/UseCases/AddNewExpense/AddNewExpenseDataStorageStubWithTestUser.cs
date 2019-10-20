using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.AddNewExpense;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Tests.UseCases.AddNewExpense
{
    public class AddNewExpenseDataStorageStubWithTestUser : IAddNewExpenseDataStorage
    {
        private readonly User _user;

        public AddNewExpenseDataStorageStubWithTestUser()
        {
            this._user = new User("Test User");
        }

        public async Task<User> GetUser()
        {
            return this._user;
        }

        public async Task Persist(User user)
        {
            return;
        }
    }
}