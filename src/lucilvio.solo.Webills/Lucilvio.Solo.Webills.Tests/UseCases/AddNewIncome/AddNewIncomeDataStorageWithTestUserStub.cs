using Lucilvio.Solo.Webills.Domain.User;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.UseCases.AddNewIncome
{
    public class AddNewIncomeDataStorageWithTestUserStub : IAddNewIncomeDataStorage
    {
        private readonly User _user;

        public AddNewIncomeDataStorageWithTestUserStub()
        {
            this._user = new User("Test user", new Domain.User.Login("user@mail.com"), new Password("123456"));
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