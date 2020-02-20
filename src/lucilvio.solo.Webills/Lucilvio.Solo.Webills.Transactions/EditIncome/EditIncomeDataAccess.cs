using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Transactions.Domain;
using Lucilvio.Solo.Webills.Transactions.Infraestructure.DataAccess;

using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.Transactions.EditIncome
{
    internal class EditIncomeDataAccess : IEditIncomeDataAccess
    {
        private readonly TransactionsContext _context;

        public EditIncomeDataAccess(TransactionsContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<User> GetUserById(Guid id)
        {
            return _context.Users.Include(u => u.Incomes).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task Persist()
        {
            await _context.SaveChangesAsync();
        }
    }
}