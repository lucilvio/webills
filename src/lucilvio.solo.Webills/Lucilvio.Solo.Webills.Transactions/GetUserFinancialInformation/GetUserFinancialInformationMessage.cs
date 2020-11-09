using System;

namespace Lucilvio.Solo.Webills.Transactions.GetUserDashboardInfo
{
    public interface IGetUserFinancialInformationMessage
    {
        Guid UserId { get; }
    }
}