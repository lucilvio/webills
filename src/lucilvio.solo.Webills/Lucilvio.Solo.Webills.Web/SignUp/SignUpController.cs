using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Lucilvio.Solo.Webills.Web.Logon;
using Lucilvio.Solo.Webills.Profile.UseCases.Contracts.RegisterUser;

namespace Lucilvio.Solo.Webills.Web.SignUp
{
    [AllowAnonymous]
    public class SignUpController : Controller
    {
        private readonly IRegisterUser _registerUser;

        public SignUpController(IRegisterUser registerUser)
        {
            this._registerUser = registerUser;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserRequest request)
        {
            await this._registerUser.Execute(new RegisterUserCommandAdapter(request));

            return RedirectToAction(nameof(LogonController.Index), "Logon");
        }
    }
}
