using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Core.Domain.User;
using Lucilvio.Solo.Webills.Core.UseCases.AddNewIncome;

namespace Lucilvio.Solo.Webills.UseCases.AddNewIncome
{
    public class AddNewIncomeDataStorageWithNullUserStub : IAddNewIncomeDataStorage
    {
        public async Task<User> GetUser()
        {
            return null;
        }

        public async Task Persist(User user)
        {
        }
    }
}