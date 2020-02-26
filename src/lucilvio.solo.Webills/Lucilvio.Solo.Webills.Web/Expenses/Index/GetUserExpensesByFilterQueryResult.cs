namespace Lucilvio.Solo.Webills.Clients.Web.Expenses.Index
{
    public class GetUserExpensesByFilterQueryResult
    {
        public bool HasExpenses { get; internal set; }
        public System.Collections.Generic.IEnumerable<UserExpenseData> Expenses { get; internal set; }
    }
}