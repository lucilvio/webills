using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Core.Domain.User;
using Lucilvio.Solo.Webills.Core.UseCases.AddNewIncome;

namespace Lucilvio.Solo.Webills.UseCases.AddNewIncome
{
    public class AddNewIncomeDataStorageWithTestUserStub : IAddNewIncomeDataStorage
    {
        private readonly User _user;

        public AddNewIncomeDataStorageWithTestUserStub()
        {
            this._user = new User("Test user");
        }

        public async Task<User> GetUser()
        {
            return this._user;
        }

        public async Task Persist(User user)
        {
        }
    }
}