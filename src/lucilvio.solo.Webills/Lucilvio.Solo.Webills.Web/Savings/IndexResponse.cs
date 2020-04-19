using Lucilvio.Solo.Webills.Web;

namespace Lucilvio.Solo.Webills.Clients.Web.Savings
{
    public class IndexResponse
    {
        public IndexResponse(decimal value)
        {
            this.Value = value.DecimalToMoney();
        }

        public string Value { get; set; }
    }
}