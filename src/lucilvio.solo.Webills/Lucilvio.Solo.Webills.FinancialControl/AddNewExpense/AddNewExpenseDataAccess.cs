using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.FinancialControl.Domain;
using Lucilvio.Solo.Webills.FinancialControl.Infraestructure.DataAccess;

namespace Lucilvio.Solo.Webills.FinancialControl.AddNewExpense
{
    internal class AddNewExpenseDataAccess
    {
        private readonly FinancialControlDataContext _context;

        public AddNewExpenseDataAccess(FinancialControlDataContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddNewExpense(Expense expense)
        {
            await this._context.Expenses.AddAsync(expense);
            await this._context.SaveChangesAsync();
        }
    }
}