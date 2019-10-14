using System;
using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.Contracts.AddNewIncome;

namespace Lucilvio.Solo.Webills.UseCases.AddNewIncome
{
    public class AddNewIncomeCommandStub : AddNewIncomeCommand
    {
        public AddNewIncomeCommandStub(string name, DateTime date, TransactionValue value)
        {
            base.Name = name;
            base.Date = date;
            base.Value = value;
        }
    }
}