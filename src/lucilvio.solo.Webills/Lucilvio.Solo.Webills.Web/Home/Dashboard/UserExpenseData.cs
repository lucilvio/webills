using Lucilvio.Solo.Webills.Domain.User;
using System;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class UserExpenseData
    {
        public UserExpenseData(Guid number, string name, Category category, DateTime date, TransactionValue value)
        {
            this.Name = name;
            this.Date = date;
            this.Value = value;
            this.Number = number;
            this.Category = category;
        }

        public Guid Number { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public TransactionValue Value { get; set; }
        public Category Category { get; set; }
    }
}