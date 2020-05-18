using Lucilvio.Solo.Webills.Clients.Web.Shared.DataFormaters;
using Lucilvio.Solo.Webills.Savings.GetSavingsByFilter;

namespace Lucilvio.Solo.Webills.Clients.Web.Savings
{
    public class IndexResponse
    {
        public IndexResponse(GetSavingsByFilterOutput output)
        {
            this.Value = output.Value.DecimalToMoney() ?? 0m.DecimalToMoney();
        }

        public string Value { get; }
    }
}