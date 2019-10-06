using System;
using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.AddNewIncome;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lucilvio.Solo.Webills.Tests.UseCases.AddNewIncome
{
    [TestClass]
    public class AddNewIncomeTests
    {
        [TestMethod]
        [ExpectedException(typeof(CommandNotInformed))]
        public void ThrowsCommandNotInformedWhenCommandIsNull()
        {
            var userCase = new Webills.UseCases.AddNewIncome.AddNewIncome(new AddNewIncomeDataStorageWithTestUserStub());
            userCase.Execute(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotFound))]
        public void ThrowsUerNotFoundExceptionWhenTheUserIsNotFound()
        {
            var userCase = new Webills.UseCases.AddNewIncome.AddNewIncome(new AddNewIncomeDataStorageWithNullUserStub());
            userCase.Execute(new AddNewIncomeCommandStub("test income", DateTime.Now, TransactionValue.Zero));
        }
    }
}