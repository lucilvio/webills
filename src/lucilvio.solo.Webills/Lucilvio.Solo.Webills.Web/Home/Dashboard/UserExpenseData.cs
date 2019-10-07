using Lucilvio.Solo.Webills.Domain.User;
using System;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class UserExpenseData
    {
        public UserExpenseData(string name, DateTime date, TransactionValue value)
        {
            this.Name = name;
            this.Date = date;
            this.Value = value;
        }

        public string Name { get; set; }
        public DateTime Date { get; set; }
        public TransactionValue Value { get; set; }
    }
}