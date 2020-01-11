using System;
using System.Linq;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Core.Domain.User;
using Lucilvio.Solo.Webills.UseCases.AddNewIncome;
using Lucilvio.Solo.Webills.UseCases.Shared.Errors;
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
            var userCase = new Core.UseCases.AddNewIncome.AddNewIncome(new AddNewIncomeDataStorageWithTestUserStub());
            await userCase.Execute(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotFound))]
        public async Task ThrowsUerNotFoundExceptionWhenTheUserIsNotFound()
        {
            var userCase = new Core.UseCases.AddNewIncome.AddNewIncome(new AddNewIncomeDataStorageWithNullUserStub());
            await userCase.Execute(new AddNewIncomeCommandStub("test income", DateTime.Now, TransactionValue.Zero));
        }

        [TestMethod]
        public async Task UserAddNewIncome()
        {
            var dataStorage = new AddNewIncomeDataStorageWithTestUserStub();

            var userCase = new Core.UseCases.AddNewIncome.AddNewIncome(dataStorage);
            await userCase.Execute(new AddNewIncomeCommandStub("test income", new DateTime(2019, 10, 1), new TransactionValue(300.23m)));

            var user = await dataStorage.GetUser();
            var addedIncome = user.Incomes.First();

            Assert.AreEqual(1, user.Incomes.Count());
            Assert.AreEqual("test income", addedIncome.Name);
            Assert.AreEqual(new DateTime(2019, 10, 1), addedIncome.Date);
            Assert.AreEqual(new TransactionValue(300.23m), addedIncome.Value);
        }
    }
}