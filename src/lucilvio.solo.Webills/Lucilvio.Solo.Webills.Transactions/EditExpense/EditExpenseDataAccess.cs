using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Transactions.Domain;
using Lucilvio.Solo.Webills.Transactions.Infraestructure.DataAccess;

using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.Transactions.EditExpense
{
    internal class EditExpenseDataAccess : IEditExpenseDataAccess
    {
        private readonly TransactionsContext _context;

        public EditExpenseDataAccess(TransactionsContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<User> GetUserById(Guid id)
        {
            return this._context.Users.Include(u => u.Expenses).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task Persist()
        {
            await this._context.SaveChangesAsync();
        }
    }
}