using System;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class EditIncomeRequest
    {
        public string UserId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Category { get; set; }
        public string Value { get; set; }
    }
}