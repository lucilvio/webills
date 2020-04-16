using System;
using Lucilvio.Solo.Webills.Transactions.Domain;

namespace Lucilvio.Solo.Webills.Transactions.AddNewIncome
{
    public class CreatedIncome
    {
        internal CreatedIncome(User user, Income income)
        {
            if (user == null || income == null)
                return;

            this.Id = income.Id;
            this.UserId = user.Id;
            this.Name = income.Name;
            this.Date = income.Date;
            this.Value = income.Value.Value;
        }

        public Guid Id { get; }
        public Guid UserId { get; }
        public string Name { get; }
        public DateTime Date { get; }
        public decimal Value { get; }
    }
}
