using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.FinancialControl;
using Lucilvio.Solo.Webills.FinancialControl.AddNewExpense;
using Lucilvio.Solo.Webills.FinancialControl.AddNewIncome;
using Lucilvio.Solo.Webills.FinancialControl.AddNewRecurrentExpense;
using Lucilvio.Solo.Webills.FinancialControl.GetUserTransactionsByFilter;
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
            this.UserTransactions = await module.GetUserTransactionsByFilter(new GetUserTransactionsByFilterMessage(filter.UserId));
        }

        public async Task<IActionResult> OnPostNewTransaction(NewTransactionRequest request,
            [FromServices] Webills.FinancialControl.Module module, [FromServices] IAuthService authService)
        {
            var user = authService.AuthenticatedUser();

            request.UserId = user.Id;
            
            if (request.Type == "income")
            {
                await module.AddNewIncome(new AddNewIncomeMessage(request.UserId, request.Name, request.Category, request.Date, request.Value));
            }
            else if (request.Type == "expense")
            {
                if (request.Recurrent)
                    await module.AddNewRecurrentExpense(new AddNewRecurrentExpenseMessage(request.UserId, request.Name, request.Category, request.Date, request.Value, request.Until, request.Frequency));
                else
                    await module.AddNewExpense(new AddNewExpenseMessage(request.UserId, request.Name, request.Category, request.Date, request.Value));
            }

            this.SendSuccessMessage("Transaction successfully added.");

            return this.RedirectToPage("/Transactions/Transactions");
        }

        public class TransactionsListFilter
        {
            public Guid UserId { get; set; }
            public int Month { get; set; }
            public int Year { get; set; }
        }

        public class NewTransactionRequest
        {
            public Guid UserId { get; set; }
            public string Name { get; set; }
            public DateTime Date { get; set; }
            public decimal Value { get; set; }
            public string Type { get; set; }
            public string Category { get; set; }
            public string RecurrentCheck { get; set; }
            public DateTime Until { get; set; }
            public int Frequency { get; set; }

            public bool Recurrent => !string.IsNullOrEmpty(this.RecurrentCheck) && this.RecurrentCheck.Equals("on", StringComparison.InvariantCultureIgnoreCase);
        }

        public UserTransactions UserTransactions { get; set; }
    }
}