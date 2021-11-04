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

    internal class GetExpenseCategories : IMessageHandler<GetExpenseCategoriesMessage>
    {
        public Task Execute(GetExpenseCategoriesMessage message)
        {
            message.SetResponse(new FoundExpenseCategories(Enum.GetValues<Expense.ExpenseCategory>().Select(c => c.ToString())));

            return Task.CompletedTask;
        }
    }
}