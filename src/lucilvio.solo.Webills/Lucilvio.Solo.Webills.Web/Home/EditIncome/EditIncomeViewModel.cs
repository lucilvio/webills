﻿using System;
using System.Globalization;

namespace Lucilvio.Solo.Webills.Web.Home.EditIncome
{
    public class EditIncomeViewModel
    {
        public EditIncomeViewModel()
        {
        }

        public EditIncomeViewModel(SearchForUserIncomeByNumberResult result)
        {
            if (result == null || result.Number == Guid.Empty)

                return;

            this.Number = result.Number.ToString();
            this.Name = result.Name;
            this.Date = result.Date.ToString("yyyy-MM-dd");
            this.Value = result.Value.Value.ToString();
        }

        public string Number { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Value { get; set; }
    }
}
