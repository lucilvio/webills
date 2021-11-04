using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.FinancialControl.Domain;
using Lucilvio.Solo.Webills.FinancialControl.Infraestructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.FinancialControl.EditIncome
{
    internal class EditIncomeDataAccess : IEditIncomeDataAccess
    {
        private readonly FinancialControlDataContext _context;

        public EditIncomeDataAccess(FinancialControlDataContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<Income> GetIncome(Guid id)
        {
            return this._context.Incomes.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task UpdateIncome(Income income)
        {
            await this._context.SaveChangesAsync();
        }
    }
}