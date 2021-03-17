using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.FinancialControl.Domain;

namespace Lucilvio.Solo.Webills.FinancialControl.GetExpenseCategories
{
    public record GetExpenseCategoriesMessage();
    public record ExpenseCategories(IEnumerable<string> Categories);

    internal class GetExpenseCategoriesMessageHandler : IMessageHandler<GetExpenseCategoriesMessage>
    {
        public async Task<dynamic> Execute(GetExpenseCategoriesMessage message)
        {
            return new ExpenseCategories(Enum.GetValues<Expense.ExpenseCategory>().Select(c => c.ToString()));
        }
    }
}