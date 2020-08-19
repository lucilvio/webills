namespace Lucilvio.Solo.Webills.Transactions.GetUserDashboardInfo
{
    public class GetDashboardInfoByFilterOutput
    {
        internal GetDashboardInfoByFilterOutput() { }

        public static GetDashboardInfoByFilterOutput Empty => new GetDashboardInfoByFilterOutput();

        public decimal TotalSpent { get; internal set; }
        public decimal TotalEarns { get; internal set; }
        public decimal Balance { get; internal set; }
    }
}