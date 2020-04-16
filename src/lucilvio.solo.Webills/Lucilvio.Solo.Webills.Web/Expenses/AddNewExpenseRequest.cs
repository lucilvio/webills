using System;

namespace Lucilvio.Solo.Webills.Clients.Web.Expenses
{
    public class AddNewExpenseRequest
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Value { get; set; }
        public int Category { get; set; }
    }
}