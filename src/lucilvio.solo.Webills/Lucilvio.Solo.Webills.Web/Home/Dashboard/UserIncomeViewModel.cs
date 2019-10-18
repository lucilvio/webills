﻿using System.Globalization;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class UserIncomeViewModel
    {
        public UserIncomeViewModel(UserIncomeData income)
        {
            if (income == null)
                return;

            this.Name = income.Name;
            this.Number = income.Number.ToString();
            this.Date = income.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            this.Value = income.Value.Value.DecimalToMoney();
        }


        public string Number { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Value { get; set; }
    }

}