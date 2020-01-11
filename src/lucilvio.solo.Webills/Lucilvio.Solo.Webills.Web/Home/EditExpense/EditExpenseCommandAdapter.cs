using System;
using Lucilvio.Solo.Webills.Web.Shared;
using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.Contracts.EditExpense;

namespace Lucilvio.Solo.Webills.Web.Home
{
    internal class EditExpenseCommandAdapter : EditExpenseCommand
    {
        public EditExpenseCommandAdapter(EditExpenseRequest request)
        {
            if (request == null)
                return;

            base.Name = request.Name;
            this.Id = new Guid(request.Id);
            base.Date = request.Date.StringToDate();
            base.Value = new TransactionValue(request.Value.MoneyToDecimal());
            base.Category = (Category)Enum.Parse(typeof(Category), request.Category);
        }
    }
}