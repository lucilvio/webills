using Lucilvio.Solo.Webills.Domain.User;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.UseCases.AddNewIncome
{
    public class AddNewIncomeDataStorageWithNullUserStub : IAddNewIncomeDataStorage
    {
        public User GetUser()
        {
            return null;
        }

        public async Task Persist(User user)
        {
        }
    }
}