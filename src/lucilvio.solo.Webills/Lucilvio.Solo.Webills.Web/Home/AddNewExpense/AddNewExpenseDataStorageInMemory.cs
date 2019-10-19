using System.Linq;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.AddNewExpense;
using Lucilvio.Solo.Webills.Web.Home;

namespace Lucilvio.Solo.Webills.Web
{
    public class AddNewExpenseDataStorageInMemory : IAddNewExpenseDataStorage
    {
        private readonly DataStorageContext _dataStorage;

        public AddNewExpenseDataStorageInMemory(DataStorageContext dataStorage)
        {
            this._dataStorage = dataStorage;
        }

        public async Task<User> GetUser()
        {
            return this._dataStorage.Users.FirstOrDefault();
        }

        public async Task Persist(User user)
        {
            return;
        }
    }
}