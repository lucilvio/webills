using System;
using System.Linq;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.FinancialControl.Domain;
using Lucilvio.Solo.Webills.FinancialControl.Infraestructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.FinancialControl.RemoveExpense
{
    internal class RemoveExpenseDataAccess : IRemoveExpenseDataAccess
    {
        private readonly FinancialControlDataContext _context;

        public RemoveExpenseDataAccess(FinancialControlDataContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Expense> GetExpense(Guid id)
        {
            return await this._context.Expenses.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task RemoveExpense(Expense expense)
        {
            this._context.Entry(this._context.Set<Expense>().FirstOrDefault(e => e.Id == expense.Id)).State = EntityState.Deleted;

            await this._context.SaveChangesAsync();
        }
    }
}