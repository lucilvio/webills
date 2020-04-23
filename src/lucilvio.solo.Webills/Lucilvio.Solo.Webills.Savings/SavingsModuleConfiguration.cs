namespace Lucilvio.Solo.Webills.Savings
{
    public class SavingsModuleConfiguration
    {
        public SavingsModuleConfiguration(string transactionalContextConnectionString, string readContextConnectionString)
        {
            this.TransactionalContextConnectionString = transactionalContextConnectionString 
                ?? throw new System.ArgumentNullException(nameof(transactionalContextConnectionString));

            this.ReadContextConnectionString = readContextConnectionString 
                ?? throw new System.ArgumentNullException(nameof(readContextConnectionString));
        }

        internal string TransactionalContextConnectionString { get; private set; }
        internal string ReadContextConnectionString { get; private set; }
    }
}