namespace Lucilvio.Solo.Webills.FinancialControl.GetUserDashboardInfo
{
    public record UserFinancialInformation
    {
        internal UserFinancialInformation(decimal expenses, decimal earns)
        {
            this.Expenses = expenses;
            this.Earns = earns;
        }

        public static UserFinancialInformation Empty => new UserFinancialInformation(0, 0);
        
        public decimal Expenses { get; internal set; }
        public decimal Earns { get; internal set; }

        public decimal Balance => this.Earns - this.Expenses;
    }
}