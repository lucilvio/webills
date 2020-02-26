using System;

namespace Lucilvio.Solo.Webills.Transactions.GetIncome
{
    public interface IGetIncomeInput
    {
        public Guid UserId { get; }
        public Guid Id { get; }
    }
}