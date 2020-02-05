using System;

namespace Lucilvio.Solo.Webills.Infraestructure.DapperDataStorage
{
    public class GetUserIncomesByFilterQuery
    {
        public GetUserIncomesByFilterQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}