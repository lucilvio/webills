using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lucilvio.Solo.Webills.Website.CreateNewAccount
{
    [AllowAnonymous]
    public class CreateNewAccountModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
