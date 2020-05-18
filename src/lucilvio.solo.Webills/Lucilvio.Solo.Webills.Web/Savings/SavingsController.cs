using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Clients.Web.Shared.Authentication;
using Lucilvio.Solo.Webills.Clients.Web.Shared.DataFormaters;
using Lucilvio.Solo.Webills.Savings;
using Lucilvio.Solo.Webills.Savings.GetSavingsByFilter;
using Lucilvio.Solo.Webills.Savings.SaveMoney;

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

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var output = await this._savingsModule.GetSavingsByFilter(new GetSavingsByFilterInput(this._auth.User().Id));

            return this.View(new IndexResponse(output));
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