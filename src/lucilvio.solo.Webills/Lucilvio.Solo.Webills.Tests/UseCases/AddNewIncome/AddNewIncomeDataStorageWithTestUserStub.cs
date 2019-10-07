using Lucilvio.Solo.Webills.Domain.User;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.UseCases.AddNewIncome
{
    public class AddNewIncomeDataStorageWithTestUserStub : IAddNewIncomeDataStorage
    {
        public User GetUser()
        {
            return new User("Test User");
        }

        public async Task Persist(User user)
        {
        }
    }
}