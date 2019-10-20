using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.RemoveIncome;
using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.Web
{
    internal class RemoveIncomeDataStorageWithEf : IRemoveIncomeDataStorage
    {
        private readonly WebillsContext _context;

        public RemoveIncomeDataStorageWithEf(WebillsContext context)
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