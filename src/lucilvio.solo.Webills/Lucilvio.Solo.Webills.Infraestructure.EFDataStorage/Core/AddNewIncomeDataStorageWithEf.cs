using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Core.Domain.User;
using Lucilvio.Solo.Webills.Core.UseCases.AddNewIncome;
using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.Infraestructure.EFDataStorage.Core
{
    public class AddNewIncomeDataStorageWithEf : IAddNewIncomeDataStorage
    {
        private readonly WebillsCoreContext _context;

        public AddNewIncomeDataStorageWithEf(WebillsCoreContext context)
        {
            this._context = context;
        }

        public async Task<User> GetUser()
        {
            return await this._context.Users.FirstOrDefaultAsync();
        }

        public async Task Persist(User user)
        {
            await this._context.SaveChangesAsync();
        }
    }
}