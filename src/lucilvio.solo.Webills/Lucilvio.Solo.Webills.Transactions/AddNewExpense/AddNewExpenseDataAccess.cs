using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Transactions.Domain;
using Lucilvio.Solo.Webills.Transactions.Infraestructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.Transactions.AddNewExpense
{
    internal class AddNewExpenseDataAccess : IAddNewExpenseDataAccess
    {
        private readonly TransactionsContext _context;

        public AddNewExpenseDataAccess(TransactionsContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await this._context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task Persist()
        {
            await this._context.SaveChangesAsync();
        }
    }
}