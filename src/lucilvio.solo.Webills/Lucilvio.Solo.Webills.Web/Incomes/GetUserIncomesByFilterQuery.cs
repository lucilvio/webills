using System;

namespace Lucilvio.Solo.Webills.Clients.Web.Incomes
{
    public class GetUserIncomesByFilterQuery
    {
        private Guid id;

        public GetUserIncomesByFilterQuery(Guid id)
        {
            this.id = id;
        }
    }
}