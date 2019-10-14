using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.AddNewIncome;
using Lucilvio.Solo.Webills.UseCases.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lucilvio.Solo.Webills.Tests.UseCases.AddNewIncome
{
    [TestClass]
    public class AddNewIncomeTests
    {
        [TestMethod]
        [ExpectedException(typeof(CommandNotInformed))]
        public async Task ThrowsCommandNotInformedWhenCommandIsNull()
        {
            var userCase = new Webills.UseCases.AddNewIncome.AddNewIncome(new AddNewIncomeDataStorageWithTestUserStub());
            await userCase.Execute(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotFound))]
        public async Task ThrowsUerNotFoundExceptionWhenTheUserIsNotFound()
        {
            var userCase = new Webills.UseCases.AddNewIncome.AddNewIncome(new AddNewIncomeDataStorageWithNullUserStub());
            await userCase.Execute(new AddNewIncomeCommandStub("test income", DateTime.Now, TransactionValue.Zero));
        }
    }
}