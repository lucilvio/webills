using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.FinancialControl;
using Lucilvio.Solo.Webills.FinancialControl.DeleteIncome;
using Lucilvio.Solo.Webills.FinancialControl.GetExpenseCategories;
using Lucilvio.Solo.Webills.FinancialControl.GetIncomeCategories;
using Lucilvio.Solo.Webills.FinancialControl.GetUserTransactionsByFilter;
using Lucilvio.Solo.Webills.Website.Shared.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lucilvio.Solo.Webills.Website.Transactions
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionsApi : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddTransaction([FromBody] NewTransactionRequest request,
            [FromServices] IFinancialControlModule module, [FromServices] IAuthService authService)
        {
            var user = authService.AuthenticatedUser();

            request.UserId = user.Id;

            //if (request.Type == "income")
            //    await module.SendMessage(new AddNewRecurrentIncomeMessage(request.UserId, request.Name, request.Category, request.Date, request.Value, request.RepeatUntil, request.Frequency));
            //else if (request.Type == "expense")
            //    await module.SendMessage(new AddNewRecurrentExpenseMessage(request.UserId, request.Name, request.Category, request.Date, request.Value, request.RepeatUntil, request.Frequency));

            return this.Ok();
        }

        [HttpPost]
        [Route("delete/{type}/{id}")]
        public async Task<IActionResult> DeleteTransaction([FromServices] IFinancialControlModule module, string type, Guid id)
        {
            if (type.Equals("income", StringComparison.InvariantCultureIgnoreCase))
                await module.SendMessage(new DeleteIncomeMessage(id));
            //else if(type.Equals("expense", StringComparison.InvariantCultureIgnoreCase))
            //    await module.DeleteExpense(new DeleteIncomeMessage(id));

            return this.Ok(new { message = "Transaction successfully deleted." });
        }

        [HttpGet]
        [Route("income/categories")]
        public async Task<IActionResult> GetIncomeCategories([FromServices] IFinancialControlModule module)
        {
            var message = new GetIncomeCategoriesMessage();
            await module.SendMessage(message);

            return this.Ok(message.Response);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserTransactions([FromServices] IFinancialControlModule module,
            [FromServices] IAuthService authService)
        {
            try
            {
                var user = authService.AuthenticatedUser();
                var message = new GetUserTransactionsByFilterMessage(user.Id);
                await module.SendMessage(message);

                return this.Ok(message.Response);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("expense/categories")]
        public async Task<IActionResult> GetExpenseCategories([FromServices] IFinancialControlModule module)
        {
            var message = new GetExpenseCategoriesMessage();
            await module.SendMessage(message);

            return this.Ok(message.Response);
        }

        public class NewTransactionRequest
        {
            public Guid UserId { get; set; }
            public string Name { get; set; }
            public DateTime Date { get; set; }
            public decimal Value { get; set; }
            public string Type { get; set; }
            public string Category { get; set; }
            public bool Recurrent { get; set; }
            public DateTime RepeatUntil { get; set; }
            public int Frequency { get; set; }
        }
    }
}
