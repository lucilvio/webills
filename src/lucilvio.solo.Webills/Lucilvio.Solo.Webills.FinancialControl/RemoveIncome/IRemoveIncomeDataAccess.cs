using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.FinancialControl.Domain;

namespace Lucilvio.Solo.Webills.FinancialControl.RemoveIncome
{
    internal interface IRemoveIncomeDataAccess
    {
        Task<Income> GetIncome(Guid id);
        Task RemoveIncome(Income income);
    }
}