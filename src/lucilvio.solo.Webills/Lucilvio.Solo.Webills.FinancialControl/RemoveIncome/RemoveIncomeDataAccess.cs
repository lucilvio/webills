using System;
using System.Linq;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.FinancialControl.Domain;
using Lucilvio.Solo.Webills.FinancialControl.Infraestructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.FinancialControl.RemoveIncome
{
    internal class RemoveIncomeDataAccess : IRemoveIncomeDataAccess
    {
        private readonly FinancialControlDataContext _context;

        public RemoveIncomeDataAccess(FinancialControlDataContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Income> GetIncome(Guid id)
        {
            return await this._context.Incomes.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task RemoveIncome(Income income)
        {
            this._context.Entry(this._context.Set<Income>().FirstOrDefault(i => i.Id == income.Id)).State = EntityState.Deleted;

            await this._context.SaveChangesAsync();
        }
    }
}