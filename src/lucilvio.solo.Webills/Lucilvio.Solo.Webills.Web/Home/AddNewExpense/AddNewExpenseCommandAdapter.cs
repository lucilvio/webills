using System;

using Lucilvio.Solo.Webills.Web.Shared;
using Lucilvio.Solo.Webills.Core.Domain.User;
using Lucilvio.Solo.Webills.Core.UseCases.Contracts.AddNewExpense;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class AddNewExpenseCommandAdapter : AddNewExpenseCommand
    {
        public AddNewExpenseCommandAdapter(AddNewExpenseRequest request)
        {
            if (request == null)
                return;

            this.UserId = new Guid(request.UserId);
            this.Name = request.Name;
            this.Date = request.Date.StringToDate();
            this.Value = new TransactionValue(request.Value.MoneyToDecimal());
            this.Category = (Category)Enum.Parse(typeof(Category), request.Category);
        }
    }
}