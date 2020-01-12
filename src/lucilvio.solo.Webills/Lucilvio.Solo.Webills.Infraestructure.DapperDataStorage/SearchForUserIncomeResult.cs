using System;
using Lucilvio.Solo.Webills.Core.Domain.User;

namespace Lucilvio.Solo.Webills.Infraestructure.DapperDataStorage
{
    public class GetUserIncomeQueryResult
    {
        public GetUserIncomeQueryResult(Income income)
        {
            if (income == null)
                return;

            this.Number = income.Id;
            this.Name = income.Name;
            this.Date = income.Date;
            this.Value = income.Value;
        }

        public Guid Number { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public TransactionValue Value { get; set; }
    }
}