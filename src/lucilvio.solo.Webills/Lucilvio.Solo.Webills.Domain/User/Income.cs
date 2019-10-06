﻿using Lucilvio.Solo.Webills.Domain.User.BusinessErrors;
using System;

namespace Lucilvio.Solo.Webills.Domain.User
{
    public class Income
    {
        public Income(string name, DateTime date, TransactionValue value)
        {
            this.Name = name;
            this.Date = date;

            if (value == null)
                throw new IncomeTransactionValueCannotBeNull();

            this.Value = value;
        }

        public string Name { get; }
        public DateTime Date { get; }
        public TransactionValue Value { get; }
    }
}