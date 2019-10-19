using System.Linq;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.AddNewIncome;
using Lucilvio.Solo.Webills.Web.Home;

namespace Lucilvio.Solo.Webills.Web
{
    internal class AddNewIcomeDataStorageInMemory : IAddNewIncomeDataStorage
    {
        private readonly DataStorageContext _context;

        public AddNewIcomeDataStorageInMemory(DataStorageContext context)
        {
            this._context = context;
        }

        public async Task<User> GetUser()
        {
            return this._context.Users.FirstOrDefault();
        }

        public async Task Persist(User user)
        {
            return;
        }
    }
}