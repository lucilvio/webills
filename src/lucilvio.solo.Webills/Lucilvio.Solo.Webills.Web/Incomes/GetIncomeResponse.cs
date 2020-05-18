using System;
using Lucilvio.Solo.Webills.Clients.Web.Shared.DataFormaters;
using Lucilvio.Solo.Webills.Transactions.GetIncomeById;

namespace Lucilvio.Solo.Webills.Clients.Web.Incomes
{
    internal class GetIncomeResponse
    {
        public GetIncomeResponse(GetIncomeByIdOutput income)
        {
            if (income == null)
                return;

            this.Id = income.Id;
            this.Name = income.Name;
            this.Date = income.Date.ToDateString();
            this.Value = income.Value.DecimalToMoney();
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Date { get; }
        public string Value { get; }
    }
}