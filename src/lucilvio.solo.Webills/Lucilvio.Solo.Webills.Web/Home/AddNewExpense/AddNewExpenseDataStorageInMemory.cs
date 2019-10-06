using System.Linq;
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

        public User GetUser()
        {
            return this._dataStorage.Users.FirstOrDefault();
        }

        public void Persist(User user)
        {
            return;
        }
    }
}