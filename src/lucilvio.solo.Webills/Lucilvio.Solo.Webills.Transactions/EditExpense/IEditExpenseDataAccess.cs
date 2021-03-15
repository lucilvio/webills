using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.FinancialControl.Domain;

namespace Lucilvio.Solo.Webills.FinancialControl.EditExpense
{
    internal interface IEditExpenseDataAccess
    {
        Task<Expense> GetExpense(Guid id);
        Task UpdateExpense(Expense expense);
    }
}