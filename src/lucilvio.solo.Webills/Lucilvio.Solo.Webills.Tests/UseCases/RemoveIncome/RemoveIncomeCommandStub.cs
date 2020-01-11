using System;

using Lucilvio.Solo.Webills.Core.UseCases.Contracts.RemoveIncome;

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