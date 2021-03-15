using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.FinancialControl.Domain;

namespace Lucilvio.Solo.Webills.FinancialControl.GetIncomeCategories
{
    public record GetIncomeCategoriesMessage();
    public record IncomeCategories(IEnumerable<string> Categories);
    internal class GetIncomeCategoriesMessageHandler : IMessageHandler<GetIncomeCategoriesMessage>
    {
        public async Task<dynamic> Execute(GetIncomeCategoriesMessage message)
        {
            return new IncomeCategories(Enum.GetValues<Income.IncomeCategory>().Select(c => c.ToString()));
        }
    }
}