using System;
using Lucilvio.Solo.Webills.FinancialControl.Domain;

namespace Lucilvio.Solo.Webills.FinancialControl.EditIncome
{
    public class OnEditIncomeInput
    {
        internal OnEditIncomeInput(User user, Income income)
        {
            if (user == null || income == null)
                return;

            this.Id = income.Id;
            this.UserId = user.Id;
            this.Date = income.Date;
            this.Name = income.Name;
            this.Value = income.Value.Value;
        }

        public Guid Id { get; }
        public string Name { get; }
        public DateTime Date { get; }
        public decimal Value { get; }
        public Guid UserId { get; }
    }
}