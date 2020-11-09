using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Transactions;
using Lucilvio.Solo.Webills.Transactions.AddNewExpense;
using Lucilvio.Solo.Webills.Transactions.AddNewIncome;
using Lucilvio.Solo.Webills.Transactions.GetUserTransactionsByFilter;
using Lucilvio.Solo.Webills.Website.Shared;
using Lucilvio.Solo.Webills.Website.Shared.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lucilvio.Solo.Webills.Website.Transactions
{
    public class TransactionsModel : PageModel
    {
        public async Task OnGetAsync([FromQuery] TransactionsListFilter filter, [FromServices] Module module,
            [FromServices] IAuthService authService)
        {
            var user = authService.AuthenticatedUser();

            filter.UserId = user.Id;
            this.UserTransactions = await module.GetUserTransactionsByFilter(filter);
        }

        public class TransactionsListFilter : IGetUserTransactionsByFilterMessage
        {
            public Guid UserId { get; set; }
        }

        public async Task<IActionResult> OnPostAsync(NewTransactionRequest request,
            [FromServices] Webills.Transactions.Module module, [FromServices] IAuthService authService)
        {
            var user = authService.AuthenticatedUser();

            request.UserId = user.Id;

            if (request.Type == "income")
                await module.AddNewIncome(request);
            else if (request.Type == "expense")
                await module.AddNewExpense(request);

            this.SendSuccessMessage("Transaction successfully added.");

            return this.RedirectToPage("/Transactions/Transactions");
        }

        public class NewTransactionRequest : IAddNewIncomeMessage, IAddNewExpenseMessage
        {
            public Guid UserId { get; set; }
            public string Name { get; set; }
            public DateTime Date { get; set; }
            public decimal Value { get; set; }
            public string Type { get; set; }
            public string Category { get; set; }
        }

        public UserTransactions UserTransactions { get; set; }
    }
}
