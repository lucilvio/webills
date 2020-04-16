namespace Lucilvio.Solo.Webills.Dashboard.GetUserDashboardInfo
{
    public class GetDashboardInfoByFilterOutput
    {
        internal GetDashboardInfoByFilterOutput() { }

        public decimal TotalSpent { get; internal set; }
        public decimal TotalEarns { get; internal set; }
        public decimal Balance { get; internal set; }
    }
}