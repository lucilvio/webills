using System;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class SearchForUserIncomeByNumberQuery
    {
        public SearchForUserIncomeByNumberQuery(Guid number)
        {
            Number = number;
        }

        public Guid Number { get; set; }
    }
}