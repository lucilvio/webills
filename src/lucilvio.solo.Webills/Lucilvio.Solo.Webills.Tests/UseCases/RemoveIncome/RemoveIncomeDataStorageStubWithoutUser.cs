using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.RemoveIncome;

namespace Lucilvio.Solo.Webills.Tests.UseCases.RemoveIncome
{
    internal class RemoveIncomeDataStorageStubWithoutUser : IRemoveIncomeDataStorage
    {
        public async Task<User> GetUser()
        {
            return null;
        }

        public async Task Persist()
        {
        }
    }
}