using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lucilvio.Solo.Webills.FinancialControl.GetIncomeCategories;
using Lucilvio.Solo.Webills.FinancialControl.GetExpenseCategories;
using System;
using Lucilvio.Solo.Webills.FinancialControl.DeleteIncome;
using Lucilvio.Solo.Webills.Website.Shared.Authorization;
using Lucilvio.Solo.Webills.FinancialControl.GetUserTransactionsByFilter;
using Microsoft.AspNetCore.Authorization;
using Lucilvio.Solo.Webills.FinancialControl.AddNewExpense;
using Lucilvio.Solo.Webills.FinancialControl.AddNewIncome;
using Lucilvio.Solo.Webills.FinancialControl.AddNewRecurrentExpense;
using Lucilvio.Solo.Webills.FinancialControl.AddNewRecurrentIncome;

namespace Lucilvio.Solo.Webills.Website.Transactions
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionsApi : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddTransaction([FromServices] FinancialControl.Module module,
            [FromServices]IAuthService authService, [FromBody]NewTransactionRequest request)
        {
            var user = authService.AuthenticatedUser();

            request.UserId = user.Id;

            if (request.Type == "income")
            {
                if (request.Recurrent)
                    await module.AddNewRecurrentIncome(new AddNewRecurrentIncomeMessage(request.UserId, request.Name, request.Category, request.Date, request.Value, request.Until, request.Frequency));
                else
                    await module.AddNewIncome(new AddNewIncomeMessage(request.UserId, request.Name, request.Category, request.Date, request.Value));
            }
            else if (request.Type == "expense")
            {
                if (request.Recurrent)
                    await module.AddNewRecurrentExpense(new AddNewRecurrentExpenseMessage(request.UserId, request.Name, request.Category, request.Date, request.Value, request.Until, request.Frequency));
                else
                    await module.AddNewExpense(new AddNewExpenseMessage(request.UserId, request.Name, request.Category, request.Date, request.Value));
            }

            return this.Ok();
        }

        [HttpPost]
        [Route("delete/{type}/{id}")]
        public async Task<IActionResult> DeleteTransaction([FromServices] FinancialControl.Module module, string type, Guid id)
        {
            if (type.Equals("income", StringComparison.InvariantCultureIgnoreCase))
                await module.DeleteIncome(new DeleteIncomeMessage(id));
            //else if(type.Equals("expense", StringComparison.InvariantCultureIgnoreCase))
            //    await module.DeleteExpense(new DeleteIncomeMessage(id));

            return this.Ok(new { message = "Transaction successfully deleted." });
        }

        [HttpGet]
        [Route("income/categories")]
        public async Task<IActionResult> GetIncomeCategories([FromServices] FinancialControl.Module module)
        {
            return this.Ok(await module.GetIncomeCategories(new GetIncomeCategoriesMessage()));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserTransactions([FromServices] FinancialControl.Module module,
            [FromServices] IAuthService authService)
        {
            try
            {
                var user = authService.AuthenticatedUser();
                return this.Ok(await module.GetUserTransactionsByFilter(new GetUserTransactionsByFilterMessage(user != null ? user.Id : Guid.NewGuid())));
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);                
            }
        }

        [HttpGet]
        [Route("expense/categories")]
        public async Task<IActionResult> GetExpenseCategories([FromServices] FinancialControl.Module module)
        {
            return this.Ok(await module.GetExpenseCategories(new GetExpenseCategoriesMessage()));
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
            public DateTime Until { get; set; }
            public int Frequency { get; set; }
        }
    }
}
