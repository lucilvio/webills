namespace Lucilvio.Solo.Webills.Web.Home.Index
{
    public class TodayExpensesResponse
    {
        public TodayExpensesResponse(TodayExpensesData expense)
        {
            if (expense == null)
                return;

            this.Name = expense.Name;
            this.Id = expense.Id.ToString();
            this.Category = expense.Category.ToString();
            this.Value = expense.Value.DecimalToMoney();
        }

        public string Id { get;  }
        public string Category { get;  }
        public string Name { get; }
        public string Date { get; }
        public string Value { get; }
    }
}