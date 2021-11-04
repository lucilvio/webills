using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.FinancialControl.Domain;
using Lucilvio.Solo.Webills.FinancialControl.Infraestructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.FinancialControl.EditExpense
{
    internal class EditExpenseDataAccess : IEditExpenseDataAccess
    {
        private readonly FinancialControlDataContext _context;

        public EditExpenseDataAccess(FinancialControlDataContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Expense> GetExpense(Guid id)
        {
            return await this._context.Expenses.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateExpense(Expense expense)
        {
            await this._context.SaveChangesAsync();
        }
    }
}