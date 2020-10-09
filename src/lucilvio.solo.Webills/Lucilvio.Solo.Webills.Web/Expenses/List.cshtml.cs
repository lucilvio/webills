using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Transactions;
using Lucilvio.Solo.Webills.Transactions.GetExpensesByFilter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lucilvio.Solo.Webills.Clients.Web.Expenses
{
    public class ListModel : PageModel
    {
        private readonly TransactionsModule _module;

        public ListModel(TransactionsModule module)
        {
            this._module = module;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var expenses = await this._module.SendMessage<GetExpensesByFilterInput, GetExpensesByFilterOutput>
                (new GetExpensesByFilterInput(Guid.NewGuid()));


            return Page();
        }
    }
}
