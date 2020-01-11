using System;

namespace Lucilvio.Solo.Webills.Core.UseCases.Contracts.RemoveIncome
{
    public abstract class RemoveIncomeCommand
    {
        public Guid IncomeNumber { get; protected set; }
    }
}