using System;
using System.Linq;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Domain.User.BusinessErrors;
using Lucilvio.Solo.Webills.UseCases.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lucilvio.Solo.Webills.Tests.UseCases.RemoveIncome
{
    [TestClass]
    public class RemoveIncomeTests
    {
        [TestMethod]
        [ExpectedException(typeof(CommandNotInformed))]
        public async Task ThrowsCommandNotInformedIfCommandIsNull()
        {
            await new Webills.UseCases.RemoveIncome.RemoveIncome(new RemoveIncomeDataStorageStubWithOneUserWithIncome()).Execute(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotFound))]
        public async Task ThrowsUserNotFoundIfAnyUserWasFound()
        {
            await new Webills.UseCases.RemoveIncome.RemoveIncome(new RemoveIncomeDataStorageStubWithoutUser()).Execute(new RemoveIncomeCommandStub(Guid.Empty));
        }

        [TestMethod]
        [ExpectedException(typeof(IncomeNotFound))]
        public async Task ThrowsIncomeNotFoundIfAnyIncomeWasFound()
        {
            await new Webills.UseCases.RemoveIncome.RemoveIncome(new RemoveIncomeDataStorageStubWithOneUserWithIncome()).Execute(new RemoveIncomeCommandStub(Guid.Empty));
        }

        [TestMethod]
        public async Task UserCanRemoveIncome()
        {
            var dataStorage = new RemoveIncomeDataStorageStubWithOneUserWithIncome();
            var user = await dataStorage.GetUser();
            
            var useCase = new Webills.UseCases.RemoveIncome.RemoveIncome(dataStorage);
            await useCase.Execute(new RemoveIncomeCommandStub(user.Incomes.First().Number));

            Assert.AreEqual(0, user.Incomes.Count());
        }
    }
}