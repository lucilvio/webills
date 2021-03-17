using System.Threading.Tasks;

using Lucilvio.Solo.Webills.FinancialControl.Domain;

namespace Lucilvio.Solo.Webills.FinancialControl.AddNewIncome
{
    internal interface IAddNewIncomeDataAccess
    {
        Task AddNewIncome(Income income);
    }
}