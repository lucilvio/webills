﻿using System;
using Lucilvio.Solo.Webills.FinancialControl.Domain;

namespace Lucilvio.Solo.Webills.FinancialControl.EditExpense
{
    public class OnEditedExpenseInput
    {
        internal OnEditedExpenseInput(User user, Expense expense)
        {
            if (user == null || expense == null)
                return;

            this.Id = expense.Id;
            this.UserId = user.Id;
            this.Date = expense.Date;
            this.Name = expense.Name;
            this.Value = expense.Value.Value;
            this.Category = (int)expense.Category;
            this.CategoryName = expense.ToString();
        }

        public Guid Id { get; }
        public string Name { get; }
        public string CategoryName { get; }
        public int Category { get; }
        public DateTime Date { get; }
        public decimal Value { get; }
        public Guid UserId { get; }
    }
}