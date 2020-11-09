namespace Lucilvio.Solo.Webills.Transactions.GetUserDashboardInfo
{
    public class UserFinancialInformation
    {
        internal UserFinancialInformation() { }

        public UserFinancialInformation(decimal expenses, decimal earns, decimal balance)
        {
            this.Expenses = expenses;
            this.Earns = earns;
            this.Balance = balance;
        }

        public static UserFinancialInformation Empty => new UserFinancialInformation();

        public decimal Expenses { get; }
        public decimal Earns { get; }
        public decimal Balance { get; }
    }
}