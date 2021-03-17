using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.FinancialControl.Domain;
using Lucilvio.Solo.Webills.FinancialControl.Infraestructure.DataAccess;

namespace Lucilvio.Solo.Webills.FinancialControl.AddNewIncome
{
    internal class AddNewIncomeDataAccess : IAddNewIncomeDataAccess
    {
        private readonly FinancialControlDataContext _context;

        public AddNewIncomeDataAccess(FinancialControlDataContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        async Task IAddNewIncomeDataAccess.AddNewIncome(Income income)
        {
            await this._context.Incomes.AddAsync(income);
            await this._context.SaveChangesAsync();
        }
    }
}