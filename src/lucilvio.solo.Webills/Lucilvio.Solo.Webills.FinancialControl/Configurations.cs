namespace Lucilvio.Solo.Webills.FinancialControl
{
    public record Configurations
    {
        internal string ModuleName { get; } = "FinancialControl";

        public string DataConnectionString { get; init; }
    }
}