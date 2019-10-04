using System;
using Lucilvio.Solo.Webills.Tests;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public abstract class AddNewIncomeCommand
    {
        public string Name { get; protected set; }
        public DateTime Date { get; protected set; }
        public TransactionValue Value { get; protected set; }
    }
}