using System.Collections.Generic;
using System.Linq;

using Lucilvio.Solo.Webills.Transactions.GetIncomesByFilter;
using Lucilvio.Solo.Webills.Web;
using Lucilvio.Solo.Webills.Web.Shared;

namespace Lucilvio.Solo.Webills.Clients.Web.Incomes
{
    public class IncomesResponse
    {
        private IncomesResponse()
        {
            this.Incomes = new List<IncomeFromList>();
        }

        public IncomesResponse(GetIncomesByFilterOutput result) : this()
        {
            if (result != null)
                this.Incomes = result.Incomes.Select(i => new IncomeFromList(i));
        }

        public IEnumerable<IncomeFromList> Incomes { get; set; }

        public class IncomeFromList
        {
            public IncomeFromList(GetIncomesByFilterOutput.Income income)
            {
                if (income == null)
                    return;

                this.Id = income.Id.ToString();
                this.Name = income.Name;
                this.Date = income.Date.ToDateString();
                this.Value = income.Value.DecimalToMoney();
            }

            public object Id { get; }
            public string Name { get; }
            public string Date { get; }
            public string Value { get; }
        }
    }
}