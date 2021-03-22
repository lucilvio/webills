using System;
using System.Collections.Generic;

namespace Lucilvio.Solo.Webills.FinancialControl.Domain
{
    internal class RecurrentIncome
    {
        private readonly List<Income> _incomes;

        private RecurrentIncome()
        {
            this.Id = Guid.NewGuid();
            this._incomes = new List<Income>();
        }

        public RecurrentIncome(Guid userId, string name, string category, DateTime date, TransactionValue value,
            DateTime until, int frequency) : this()
        {
            this.Recurrency = new Recurrency(Frequency.FromValue(frequency), until);

            this._incomes.Add(Income.WithRecurrency(userId, name, category, date, value, this.Id));

            foreach (var nextDate in this.Recurrency.DatesUntilRecurrencyEndsByFrequency(date))
            {
                this._incomes.Add(Income.WithRecurrency(userId, name, category, nextDate, value, this.Id));
            }
        }

        public Guid Id { get; }
        public Recurrency Recurrency { get; }
        public IEnumerable<Income> Incomes => this._incomes;
    }
}