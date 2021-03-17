using System.Threading.Tasks;
using Lucilvio.Solo.Webills.FinancialControl.Domain;

namespace Lucilvio.Solo.Webills.FinancialControl.AddNewExpense
{
    interface IAddNewExpenseDataAccess
    {
        Task AddNewExpense(Expense expense);
    }
}