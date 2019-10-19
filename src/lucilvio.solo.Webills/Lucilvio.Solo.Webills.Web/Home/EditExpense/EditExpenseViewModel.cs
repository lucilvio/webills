using Lucilvio.Solo.Webills.Web.Shared;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class EditExpenseViewModel
    {
        public EditExpenseViewModel()
        {
        }

        public EditExpenseViewModel(SearchForUserExpenseByNumberResult result)
        {
            if (result == null)
                return;

            this.Name = result.Name;
            this.Number = result.Number.ToString();
            this.Date = result.Date.ToDateString();
            this.Value = result.Value.Value.DecimalToMoney();
        }

        public string Number { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Value { get; set; }
    }
}