using System;

namespace Lucilvio.Solo.Webills.Clients.Web.Expenses
{
    public class GetUserExpensesByFilterQuery
    {
        private Guid id;

        public GetUserExpensesByFilterQuery(Guid id)
        {
            this.id = id;
        }
    }
}