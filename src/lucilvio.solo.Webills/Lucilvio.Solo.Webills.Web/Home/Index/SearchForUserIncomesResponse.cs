using System.Collections.Generic;
using System.Linq;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class SearchForUserIncomesResponse
    {
        public SearchForUserIncomesResponse(IEnumerable<UserIncomeData> incomes)
        {
            if (incomes == null)
                return;

            this.Incomes = incomes.ToList();
        }

        public IReadOnlyCollection<UserIncomeData> Incomes { get; }
    }
}