using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.Tests;
using System;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class UserIncomeData
    {
        public UserIncomeData(string name, DateTime date, TransactionValue value)
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