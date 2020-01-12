using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Lucilvio.Solo.Webills.Core.Domain.User;
using Lucilvio.Solo.Webills.Core.UseCases.RemoveIncome;

namespace Lucilvio.Solo.Webills.Infraestructure.EFDataStorage.Core
{
    public class RemoveIncomeDataStorageWithEf : IRemoveIncomeDataStorage
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