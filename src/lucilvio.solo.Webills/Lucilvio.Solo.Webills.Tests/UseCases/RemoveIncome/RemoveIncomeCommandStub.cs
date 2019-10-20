using Lucilvio.Solo.Webills.UseCases.Contracts.RemoveIncome;
using System;

namespace Lucilvio.Solo.Webills.Tests.UseCases.RemoveIncome
{
    internal class RemoveIncomeCommandStub : RemoveIncomeCommand
    {
        public RemoveIncomeCommandStub(Guid incomeNumber)
        {
            base.IncomeNumber = incomeNumber;
        }
    }
}