namespace Lucilvio.Solo.Webills.FinancialControl.GetUserDashboardInfo
{
    public record UserFinancialInformation(decimal Expenses, decimal Earns)
    {
        public static UserFinancialInformation Empty => new UserFinancialInformation(0, 0);

        public decimal Balance => this.Earns - this.Expenses;
    }
}