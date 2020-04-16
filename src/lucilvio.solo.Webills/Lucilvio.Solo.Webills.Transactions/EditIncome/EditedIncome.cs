using System;
using Lucilvio.Solo.Webills.Transactions.Domain;

namespace Lucilvio.Solo.Webills.Transactions.EditIncome
{
    public class EditedIncome
    {
        internal EditedIncome(User user, Income income)
        {
            if(user == null || income == null)
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