using System.Threading.Tasks;
using Lucilvio.Solo.Webills.FinancialControl.GetExpenseCategories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lucilvio.Solo.Webills.Website.Transactions
{
    public class ExpenseCategoriesModel : PageModel
    {
        public async Task<JsonResult> OnGetAsync([FromServices] FinancialControl.Module module)
        {
            return new JsonResult(await module.GetExpenseCategories(new GetExpenseCategoriesMessage()));
        }
    }
}
