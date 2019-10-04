using Lucilvio.Solo.Webills.Tests;
using System;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public abstract class AddNewExpenseCommand
    {
        public string Name { get; protected set; }
        public DateTime Date { get; protected set; }
        public TransactionValue Value { get; protected set; }
    }
}