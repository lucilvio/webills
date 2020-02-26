using System;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class RemoveExpenseRequest
    {
        public string UserId { get; set; }
        public Guid ExpenseId { get; set; }
    }
}