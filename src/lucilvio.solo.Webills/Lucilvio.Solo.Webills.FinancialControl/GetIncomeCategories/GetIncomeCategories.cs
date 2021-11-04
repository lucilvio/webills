using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lucilvio.Solo.Architecture;
using Lucilvio.Solo.Webills.FinancialControl.Domain;

namespace Lucilvio.Solo.Webills.FinancialControl.GetIncomeCategories
{
    public record GetIncomeCategoriesMessage() : Message<FoundIncomeCategories>;
    public record FoundIncomeCategories(IEnumerable<string> Categories);

    internal class GetIncomeCategories : IMessageHandler<GetIncomeCategoriesMessage>
    {
        public Task Execute(GetIncomeCategoriesMessage message)
        {
            var foundCategories = new FoundIncomeCategories(Enum.GetValues<Income.IncomeCategory>().Select(c => c.ToString()));
            message.SetResponse(foundCategories);

            return Task.CompletedTask;
        }
    }
}