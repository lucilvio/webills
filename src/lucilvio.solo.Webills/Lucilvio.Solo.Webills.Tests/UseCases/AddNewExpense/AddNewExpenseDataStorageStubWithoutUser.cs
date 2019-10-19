using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.AddNewExpense;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Tests.UseCases.AddNewExpense
{
    public class AddNewExpenseDataStorageStubWithoutUser : IAddNewExpenseDataStorage
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