using System.Linq;
using System.Collections.Generic;

using Lucilvio.Solo.Webills.Web;
using Lucilvio.Solo.Webills.Web.Shared;
using Lucilvio.Solo.Webills.Shared.Domain;
using Lucilvio.Solo.Webills.Infraestructure.DapperDataStorage;

namespace Lucilvio.Solo.Webills.Clients.Web.Incomes
{
    public class IncomesResponse
    {
        private IncomesResponse()
        {
            this.Incomes = new List<IncomeFromList>();
        }
        
        public IncomesResponse(GetUserIncomesByFilterQueryResult result) : this()
        {
            if (result.HasIncomes)
                this.Incomes = result.Incomes.Select(i => new IncomeFromList(i));
        }

        public IEnumerable<IncomeFromList> Incomes { get; set; }

        public class IncomeFromList
        {
            public IncomeFromList(UserIncomeData income)
            {
                if (income.NotDefined())
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