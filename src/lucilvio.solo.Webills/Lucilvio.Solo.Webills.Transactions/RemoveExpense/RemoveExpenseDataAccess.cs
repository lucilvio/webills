using System;
using System.Linq;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Transactions.Domain;
using Lucilvio.Solo.Webills.Transactions.Infraestructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.Transactions.RemoveExpense
{
    internal class RemoveExpenseDataAccess : IRemoveIncomeDataAccess
    {
        private readonly TransactionsContext _context;

        public RemoveExpenseDataAccess(TransactionsContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<User> GetUserById(Guid id)
        {
            return this._context.Users.Include(u => u.Expenses).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task Persist(Guid expenseId)
        {
            this._context.Entry(this._context.Set<Expense>().FirstOrDefault(e => e.Id == expenseId)).State = EntityState.Deleted;

            await this._context.SaveChangesAsync();
        }
    }
}