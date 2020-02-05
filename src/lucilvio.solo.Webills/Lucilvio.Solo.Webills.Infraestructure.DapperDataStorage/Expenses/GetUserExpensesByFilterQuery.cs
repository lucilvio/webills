using System;

namespace Lucilvio.Solo.Webills.Infraestructure.DapperDataStorage
{
    public class GetUserExpensesByFilterQuery
    {
        public GetUserExpensesByFilterQuery(Guid userId)
        {
            this.UserId = userId;
        }

        public Guid UserId { get; }
    }
}