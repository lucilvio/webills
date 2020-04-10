using System;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class UserIncomeData
    {
        public UserIncomeData(Guid number, string name, DateTime date, decimal value)
        {
            this.Name = name;
            this.Date = date;
            this.Value = value;
            this.Number = number;
        }

        public Guid Number { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
    }
}