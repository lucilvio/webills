using Lucilvio.Solo.Webills.Web.Shared;
using System;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class EditExpenseRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Category { get; set; }
        public string Value { get; set; }
    }
}