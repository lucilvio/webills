using System;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class SearchForUserExpenseByNumberQuery
    {
        public SearchForUserExpenseByNumberQuery(Guid number)
        {
            this.Number = number;
        }

        public Guid Number { get; set; }
    }
}