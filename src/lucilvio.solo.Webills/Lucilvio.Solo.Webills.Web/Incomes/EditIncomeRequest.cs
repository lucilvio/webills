using System;

namespace Lucilvio.Solo.Webills.Clients.Web.Incomes
{
    public class EditIncomeRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Value { get; set; }
    }
}