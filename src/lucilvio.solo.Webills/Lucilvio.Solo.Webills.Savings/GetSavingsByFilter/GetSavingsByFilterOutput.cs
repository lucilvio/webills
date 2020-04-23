namespace Lucilvio.Solo.Webills.Savings.GetSavingsByFilter
{
    public class GetSavingsByFilterOutput
    {
        internal GetSavingsByFilterOutput(decimal total)
        {
            this.Value = total;
        }

        public decimal Value { get; internal set; }
    }
}