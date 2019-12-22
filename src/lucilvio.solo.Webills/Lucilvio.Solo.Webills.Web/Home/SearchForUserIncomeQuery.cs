using System;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class GetUserIncomesQueryByNumber
    {
        public GetUserIncomesQueryByNumber(int userId, Guid number)
        {
            this.UserId = userId;
            this.Number = number;
        }

        public int UserId { get; }
        public Guid Number { get; }
    }
}