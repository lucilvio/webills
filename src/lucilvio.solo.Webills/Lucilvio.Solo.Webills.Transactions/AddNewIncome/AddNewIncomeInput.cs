﻿using System;

namespace Lucilvio.Solo.Webills.Transactions.AddNewIncome
{
    public class AddNewIncomeInput
    {
        public AddNewIncomeInput(Guid userId, string name, DateTime date, decimal value)
        {
            this.UserId = userId;
            this.Name = name;
            this.Date = date;
            this.Value = value;
        }

        public Guid UserId { get; }
        public string Name { get; }
        public DateTime Date { get; }
        public decimal Value { get; }
    }
}