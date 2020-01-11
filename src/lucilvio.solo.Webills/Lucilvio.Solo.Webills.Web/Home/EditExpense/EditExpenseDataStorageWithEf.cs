using System;
using System.Linq;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Core.Domain.User;
using Lucilvio.Solo.Webills.Core.UseCases.EditExpense;
using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.Web.Home.EditExpense
{
    public class EditExpenseDataStorageWithEf : IEditExpenseDataStorage
    {
        private readonly WebillsCoreContext _context;

        public EditExpenseDataStorageWithEf(WebillsCoreContext context)
        {
            this._context = context;
        }

        public async Task<User> GetUser()
        {
            return await this._context.Users.Include(u => u.Expenses).FirstOrDefaultAsync();
        }

        public async Task Persist(Guid expenseNumber, User foundUser)
        {
            var expense = this._context.Set<Expense>().FirstOrDefault(i => i.Id == expenseNumber);
            this._context.Entry<Expense>(expense).State = EntityState.Deleted;

            await this._context.SaveChangesAsync();
        }
    }
}
