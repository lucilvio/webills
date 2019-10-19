using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.Web;
using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.UseCases.RemoveExpense
{
    public class RemoveExpenseDataStorageWithEf : IRemoveExpenseDataStorage
    {
        private readonly WebillsContext _context;

        public RemoveExpenseDataStorageWithEf(WebillsContext context)
        {
            this._context = context;
        }

        public async Task<User> GetUser()
        {
            return await this._context.Users.Include(u => u.Expenses).FirstOrDefaultAsync();
        }

        public async Task Persist()
        {
            await this._context.SaveChangesAsync();
        }
    }
}