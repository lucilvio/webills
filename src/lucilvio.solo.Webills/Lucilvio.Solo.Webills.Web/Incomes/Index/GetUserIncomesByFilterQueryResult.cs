using System.Collections.Generic;
using Lucilvio.Solo.Webills.Web.Home;

namespace Lucilvio.Solo.Webills.Clients.Web.Incomes
{
    public class GetUserIncomesByFilterQueryResult
    {
        public bool HasIncomes { get; internal set; }
        public IEnumerable<UserIncomeData> Incomes { get; internal set; }
    }
}