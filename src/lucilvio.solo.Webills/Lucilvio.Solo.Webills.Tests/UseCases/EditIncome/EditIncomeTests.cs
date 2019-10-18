﻿using System;
using System.Linq;
using Lucilvio.Solo.Webills.Domain.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lucilvio.Solo.Webills.Tests.UseCases.EditIncome
{
    [TestClass]
    public class EditIncomeTests
    {
        [TestMethod]
        public void EditIncome()
        {
            var user = new User("Test User");
            var incomeNumber = user.AddIncome(new Income("Salary", DateTime.Now, new TransactionValue(2675.89m)));

            user.AlterIncome(incomeNumber, new Income("Salary2", new DateTime(2019, 1, 20), new TransactionValue(2600.80m)));

            var alteredIncome = user.Incomes.First();

            Assert.AreEqual("Salary2", alteredIncome.Name);
            Assert.AreEqual(new DateTime(2019, 1, 20), alteredIncome.Date);
            Assert.AreEqual(new TransactionValue(2600.80m), alteredIncome.Value);
        }
    }
}