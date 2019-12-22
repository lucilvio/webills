﻿namespace Lucilvio.Solo.Webills.Web.Home.Index
{
    public class TodayExpensesResponse
    {
        public TodayExpensesResponse(TodayExpensesData expense)
        {
            if (expense == null)
                return;

            this.Name = expense.Name;
            this.Number = expense.Number.ToString();
            this.Category = expense.Category.ToString();
            this.Value = expense.Value.DecimalToMoney();
        }

        public string Number { get;  }
        public string Category { get;  }
        public string Name { get; }
        public string Date { get; }
        public string Value { get; }
    }
}