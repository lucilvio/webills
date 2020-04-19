using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Clients.Web.Shared.Authentication;
using Lucilvio.Solo.Webills.Savings;
using Lucilvio.Solo.Webills.Savings.SaveMoney;
using Lucilvio.Solo.Webills.Web;

using Microsoft.AspNetCore.Mvc;

namespace Lucilvio.Solo.Webills.Clients.Web.Savings
{
    [Route("Savings")]
    public class SavingsController : Controller
    {
        private readonly IAuthenticationService _auth;
        private readonly SavingsModule _savingsModule;

        public SavingsController(IAuthenticationService auth, SavingsModule savingsModule)
        {
            this._auth = auth;
            this._savingsModule = savingsModule;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post(SaveMoneyRequest request)
        {
            await this._savingsModule.SaveMoney(new SaveMoneyInput(this._auth.User().Id,
                request.Value.MoneyToDecimal()));

            return this.RedirectToAction("Index", "Home");
        }
    }
}