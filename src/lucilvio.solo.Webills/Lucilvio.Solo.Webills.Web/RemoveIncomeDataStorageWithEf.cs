using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.RemoveIncome;
using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.Web
{
    internal class RemoveIncomeDataStorageWithEf : IRemoveIncomeDataStorage
    {
        private readonly WebillsCoreContext _context;

        public RemoveIncomeDataStorageWithEf(WebillsCoreContext context)
        {
            this._context = context;
        }

        public async Task<User> GetUser()
        {
            return await this._context.Users.Include(u => u.Incomes).FirstOrDefaultAsync();
        }

        public async Task Persist()
        {
            await this._context.SaveChangesAsync();
        }
    }
}