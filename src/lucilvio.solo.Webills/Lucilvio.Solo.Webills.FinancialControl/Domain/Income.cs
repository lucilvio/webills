using System;

namespace Lucilvio.Solo.Webills.FinancialControl.Domain
{
    internal class Income
    {
        private Income()
        {
            this.Id = Guid.NewGuid();
        }

        public Income(Guid userId, string name, string category, DateTime date, TransactionValue value) : this()
        {
            if (string.IsNullOrEmpty(name))
                throw new Error.IncomeMustHaveName();

            this.Name = name;
            this.Date = date;
            this.UserId = userId;

            if (value == null)
                throw new Error.IncomeTransactionValueCannotBeNull();

            this.Value = value;

            if (!Enum.TryParse(typeof(IncomeCategory), category, out var categoryEnum))
                categoryEnum = IncomeCategory.Other;

            this.Category = (IncomeCategory)categoryEnum;
        }

        internal static Income WithRecurrency(Guid userId, string name, string category, DateTime date,
            TransactionValue value, Guid recurrentIncomeId)
        {
            var newIncome = new Income(userId, name, category, date, value);
            newIncome.RecurrentIncomeId = recurrentIncomeId;

            return newIncome;
        }

        public Guid Id { get; }
        public Guid UserId { get; }
        public string Name { get; private set; }
        public DateTime Date { get; private set; }
        public IncomeCategory Category { get; set; }
        public TransactionValue Value { get; private set; }
        public Guid? RecurrentIncomeId { get; private set; }

        public bool IsRecurrent => this.RecurrentIncomeId.HasValue;

        public void Update(string name, DateTime date, TransactionValue value)
        {
            this.Name = name;
            this.Date = date;
            this.Value = value;
        }

        public enum IncomeCategory
        {
            Salary = 1,
            Investments = 2,
            Other = 3
        }

        class Error
        {
            internal class IncomeMustHaveName : Exception { }
            internal class IncomeTransactionValueCannotBeNull : Exception { }
        }
    }
}