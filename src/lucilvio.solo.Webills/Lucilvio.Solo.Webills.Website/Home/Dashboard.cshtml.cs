using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Transactions.GetUserDashboardInfo;
using Lucilvio.Solo.Webills.Website.Shared.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lucilvio.Solo.Webills.Website.Home
{
    public class DashboardModel : PageModel
    {

        public async Task<IActionResult> OnGetAsync(DashboardInfoRequest request, [FromServices]Webills.Transactions.Module module, 
            [FromServices]IAuthService authService)
        {
            var user = authService.AuthenticatedUser();
            request.UserId = user.Id;

            this.DashboardInfo = await module.GetUserFinancialInfo(request);
            return Page();
        }
        
        public UserFinancialInformation DashboardInfo { get; private set; }

        public class DashboardInfoRequest : IGetUserFinancialInformationMessage
        {
            public Guid UserId { get; set; }
        }
    }
}
