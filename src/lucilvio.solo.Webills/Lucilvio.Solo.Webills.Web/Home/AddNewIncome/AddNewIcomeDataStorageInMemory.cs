using System.Linq;
using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.Tests;
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

        public User GetUser()
        {
            return this._context.Users.FirstOrDefault();
        }

        public void Persist(User user)
        {
            return;
        }
    }
}