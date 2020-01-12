using System;
using Lucilvio.Solo.Webills.Core.Domain.User;

namespace Lucilvio.Solo.Webills.Infraestructure.DapperDataStorage
{
    public class GetUserExpensesQueryResult
    {
        public GetUserExpensesQueryResult(Expense expense)
        {
            if (expense == null)
                return;

            this.Id = expense.Id;
            this.Name = expense.Name;
            this.Date = expense.Date;
            this.Value = expense.Value;
            this.Category = expense.Category;
        }

        public Guid Id { get; }
        public string Name { get; }
        public DateTime Date { get; }
        public TransactionValue Value { get; }
        public Category Category { get; }
    }
}