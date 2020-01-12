using System;

namespace Lucilvio.Solo.Webills.Infraestructure.DapperDataStorage
{
    public class GetUserExpensesByNumberQuery
    {
        public GetUserExpensesByNumberQuery(int userId, Guid number)
        {
            this.UserId = userId;
            this.Number = number;
        }

        public int UserId { get; }
        public Guid Number { get; }
    }
}