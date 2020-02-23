using System;

namespace Lucilvio.Solo.Webills.Dashboard.MainDashboard
{
    public class GetUserDashboardQueryResult
    {
        class Transaction
        {
            public Guid UserId { get; set; }
            public string Name { get; set; }
            public DateTime Date { get; set; }
            public decimal Value { get; set; }
            public string Category { get; set; }
            public int MyProperty { get; set; }
        }
    }
}