using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Lucilvio.Solo.Webills.Web;
using Lucilvio.Solo.Webills.Core.Domain.User;
using Lucilvio.Solo.Webills.Core.UseCases.RemoveExpense;

namespace Lucilvio.Solo.Webills.UseCases.RemoveExpense
{
    public class RemoveExpenseDataStorageWithEf : IRemoveExpenseDataStorage
    {
        private readonly WebillsCoreContext _context;

        public RemoveExpenseDataStorageWithEf(WebillsCoreContext context)
        {
            this._context = context;
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await this._context.Users.Include(u => u.Expenses).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task Persist(Guid expenseId)
        {
            this._context.Entry(this._context.Set<Expense>().FirstOrDefault(e => e.Id == expenseId)).State = EntityState.Deleted;

            await this._context.SaveChangesAsync();
        }
    }
}