using Lucilvio.Solo.Webills.Core.Domain.User;
using Lucilvio.Solo.Webills.Core.UseCases.RemoveIncome;
using System.Threading.Tasks;

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