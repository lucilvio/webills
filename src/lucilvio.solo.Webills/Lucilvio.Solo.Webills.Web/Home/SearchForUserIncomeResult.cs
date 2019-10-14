using System.Linq;
using System.Collections.Generic;
using Lucilvio.Solo.Webills.Domain.User;
using System;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class SearchForUserIncomeByNumberResult
    {
        public SearchForUserIncomeByNumberResult(Income income)
        {
            if (income == null)
                return;

            this.Number = income.Number;
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