using System;
using Lucilvio.Solo.Architecture;

namespace Lucilvio.Solo.Webills.FinancialControl.AddNewIncome
{
    public record AddNewIncomeMessage : Message
    {
        public AddNewIncomeMessage(Guid userId, string name, string category, DateTime date,
            decimal value, IncomeRecurrency recurrency = null)
        {
            this.UserId = userId;
            this.Name = name;
            this.Category = category;
            this.Date = date;
            this.Value = value;
            this.Recurrency = recurrency;
        }

        public Guid UserId { get; }
        public string Name { get; }
        public string Category { get; }
        public DateTime Date { get; }
        public decimal Value { get; }
        public IncomeRecurrency Recurrency { get; }

        public bool IsRecurrent => this.Recurrency != null;

        public record IncomeRecurrency(DateTime RepeatUntil, int Frequency);
    }
}