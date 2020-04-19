﻿using System;

using Lucilvio.Solo.Webills.Clients.Web.Shared.Notification;
using Lucilvio.Solo.Webills.Dashboard;
using Lucilvio.Solo.Webills.Dashboard.AddExpense;
using Lucilvio.Solo.Webills.Dashboard.EditTransaction;
using Lucilvio.Solo.Webills.Dashboard.RemoveTransaction;
using Lucilvio.Solo.Webills.Savings;
using Lucilvio.Solo.Webills.Transactions;
using Lucilvio.Solo.Webills.Transactions.AddNewExpense;
using Lucilvio.Solo.Webills.Transactions.CreateUser;
using Lucilvio.Solo.Webills.UserAccount;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lucilvio.Solo.Webills.Web
{
    public static class ModulesResolver
    {
        public static void AddModules(this IServiceCollection services, IConfiguration configuration,
            INotificationService notificationService)
        {
            var dashboardModule = new DashboardModule();
            var transactionModule = new TransactionsModule();
            var userAccountModule = new UserAccountModule();
            var savingsModule = new SavingsModule();

            BindTransactionModuleEvents(dashboardModule, transactionModule);
            BindSavingsModuleEvents(transactionModule, savingsModule);
            BindUserAccountModulesEvents(notificationService, transactionModule, userAccountModule);

            services.AddSingleton(svc => dashboardModule);
            services.AddSingleton(svc => userAccountModule);
            services.AddSingleton(svc => transactionModule);
            services.AddSingleton(svc => savingsModule);
        }

        private static void BindUserAccountModulesEvents(INotificationService notificationService, TransactionsModule transactionModule,
            UserAccountModule userAccountModule)
        {
            userAccountModule.UserAccountCreated += async (createdAccount) =>
            {
                await transactionModule.CreateUser(new CreateUserInput(createdAccount.Id));
            };

            userAccountModule.PasswordGenerated += async (generatedPassword) =>
            {
                await notificationService.Send(new Notification(
                    new Notification.Sender("Webillls", "webills@mail.com"),
                    new Notification.Receiver(generatedPassword.UserName, generatedPassword.UserContact),
                    @$"Hey <b>{generatedPassword.UserName}</b><br>Here is your new password: <b>{generatedPassword.Password}</b>"));
            };
        }

        private static void BindSavingsModuleEvents(TransactionsModule transactionModule, SavingsModule savingsModule)
        {
            savingsModule.MoneySaved += async (savedMoney) =>
            {
                await transactionModule.AddNewExpense(new AddNewExpenseInput(savedMoney.UserId, "Saving", 0,
                    DateTime.Now, savedMoney.Value));
            };
        }

        private static void BindTransactionModuleEvents(DashboardModule dashboardModule, TransactionsModule transactionModule)
        {
            transactionModule.IncomeAdded += async (addedIncome) =>
            {
                await dashboardModule.AddTransaction(AddTransactionInput.Income(addedIncome.UserId,
                    addedIncome.Id, addedIncome.Name, addedIncome.Date, addedIncome.Value));
            };

            transactionModule.IncomeEdited += async (editedIncome) =>
            {
                await dashboardModule.EditTransaction(EditTransactionInput.Income(editedIncome.UserId,
                    editedIncome.Id, editedIncome.Name, editedIncome.Date, editedIncome.Value));
            };

            transactionModule.IncomeRemoved += async (removedIncome) =>
            {
                await dashboardModule.RemoveTransaction(new RemoveTransactionInput(removedIncome.UserId, removedIncome.Id));
            };

            transactionModule.ExpenseAdded += async (addedExpense) =>
            {
                await dashboardModule.AddTransaction(AddTransactionInput.Expense(addedExpense.UserId,
                    addedExpense.Id, addedExpense.Name, addedExpense.Date, addedExpense.Category,
                    addedExpense.CategoryName, addedExpense.Value));
            };

            transactionModule.ExpenseEdited += async (editedExpense) =>
            {
                await dashboardModule.EditTransaction(EditTransactionInput.Expense(editedExpense.UserId, editedExpense.Id,
                    editedExpense.Name, editedExpense.Category, editedExpense.Date, editedExpense.Value));
            };

            transactionModule.ExpenseRemoved += async (removedExpense) =>
            {
                await dashboardModule.RemoveTransaction(new RemoveTransactionInput(removedExpense.UserId, removedExpense.Id));
            };
        }
    }
}