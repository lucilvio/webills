﻿using Lucilvio.Solo.Webills.Core.Domain.User;
using Lucilvio.Solo.Webills.Core.UseCases.EditExpense;
using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Tests.UseCases.EditExpenses
{
    internal class EditExpenseDataStorageStubWithOneUserWithExpense : IEditExpenseDataStorage
    {
        private readonly User _user;

        public EditExpenseDataStorageStubWithOneUserWithExpense()
        {
            this._user = new User("Test User");
            this._user.AddExpense("Test expanse", Category.Education, new DateTime(2000, 10, 10), TransactionValue.Zero);
        }

        public async Task<User> GetUser()
        {
            return this._user;
        }

        public async Task Persist(Guid incomeNumber, User foundUser)
        {
        }
    }
}