using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.FinancialControl.Domain;

namespace Lucilvio.Solo.Webills.FinancialControl.RemoveExpense
{
    internal interface IRemoveExpenseDataAccess
    {
        Task<Expense> GetExpense(Guid id);
        Task RemoveExpense(Expense expense);
    }
}