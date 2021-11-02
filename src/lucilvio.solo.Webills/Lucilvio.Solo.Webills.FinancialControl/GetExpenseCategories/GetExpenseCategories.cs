using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lucilvio.Solo.Architecture;
using Lucilvio.Solo.Webills.FinancialControl.Domain;

namespace Lucilvio.Solo.Webills.FinancialControl.GetExpenseCategories
{
    public record GetExpenseCategoriesMessage : Message<FoundExpenseCategories>;
    public record FoundExpenseCategories(IEnumerable<string> Categories);

    internal class GetExpenseCategories : IHandler<GetExpenseCategoriesMessage>
    {
        public async Task Execute(GetExpenseCategoriesMessage message)
        {
            message.SetResponse(new FoundExpenseCategories(Enum.GetValues<Expense.ExpenseCategory>().Select(c => c.ToString())));
        }
    }
}