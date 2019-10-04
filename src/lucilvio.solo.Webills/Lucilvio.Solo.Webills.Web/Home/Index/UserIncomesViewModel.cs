using System.Linq;
using System.Globalization;
using System.Collections.Generic;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class HomeViewModel
    {
        public UserIncomesViewModel Incomes { get; set; }
    }

    public class UserIncomesViewModel
    {
        public UserIncomesViewModel(SearchForUserIncomesResponse searchUserIncomesResponse)
        {
            if (searchUserIncomesResponse == null)
                return;

            this.Incomes = searchUserIncomesResponse.Incomes.Select(i => new UserIncomeViewModel(i)).ToList();
        }

        public IReadOnlyCollection<UserIncomeViewModel> Incomes { get; }

        public class UserIncomeViewModel
        {
            public UserIncomeViewModel(UserIncomeData income)
            {
                if (income == null)
                    return;

                this.Name = income.Name;
                this.Date = income.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                this.Value = income.Value.Value.ToString(CultureInfo.InvariantCulture);
            }

            public string Name { get; set; }
            public string Date { get; set; }
            public string Value { get; set; }
        }
    }
}