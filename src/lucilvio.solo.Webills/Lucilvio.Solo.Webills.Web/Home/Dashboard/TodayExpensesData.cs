using Lucilvio.Solo.Webills.Domain.User;
using System;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class TodayExpensesData
    {
        public Guid Number { get;  set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public int Category { get; set; }
    }
}