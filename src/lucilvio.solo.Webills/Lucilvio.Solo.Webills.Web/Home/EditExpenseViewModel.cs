using System.Globalization;

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

            this.Number = result.Number.ToString();
            this.Name = result.Name;
            this.Date = result.Date.ToString("dd-MM-yyyy");
            this.Value = result.Value.Value.ToString("N0");
        }

        public string Number { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Value { get; set; }
    }
}