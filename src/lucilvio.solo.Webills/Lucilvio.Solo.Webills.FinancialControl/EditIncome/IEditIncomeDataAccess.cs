using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.FinancialControl.Domain;

namespace Lucilvio.Solo.Webills.FinancialControl.EditIncome
{
    internal interface IEditIncomeDataAccess
    {
        Task<Income> GetIncome(Guid id);
        Task UpdateIncome(Income income);
    }
}