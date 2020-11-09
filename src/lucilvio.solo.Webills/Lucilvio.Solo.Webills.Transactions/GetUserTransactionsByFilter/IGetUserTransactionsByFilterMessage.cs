using System;

namespace Lucilvio.Solo.Webills.Transactions.GetUserTransactionsByFilter
{
    public interface IGetUserTransactionsByFilterMessage
    {
        public Guid UserId { get; }
    }
}