using System.Linq;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.AddNewIncome;
using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.Web
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