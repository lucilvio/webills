using System;

namespace Lucilvio.Solo.Webills.UseCases.Contracts.RemoveIncome
{
    public abstract class RemoveIncomeCommand
    {
        public Guid IncomeNumber { get; protected set; }
    }
}