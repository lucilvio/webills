using System;

using Lucilvio.Solo.Webills.Clients.Web.Shared.Authentication;
using Lucilvio.Solo.Webills.Clients.Web.Shared.Notification;
using Lucilvio.Solo.Webills.Savings;
using Lucilvio.Solo.Webills.Transactions;
using Lucilvio.Solo.Webills.Transactions.AddNewExpense;
using Lucilvio.Solo.Webills.Transactions.CreateUser;
using Lucilvio.Solo.Webills.UserAccount;
using Lucilvio.Solo.Webills.UserAccount.CreateUserAccount;
using Lucilvio.Solo.Webills.UserAccount.GenerateNewPassword;
using Lucilvio.Solo.Webills.UserAccount.Login;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lucilvio.Solo.Webills.Web
{
    public static class ModulesResolver
    {
        public static void AddModules(this IServiceCollection services, IConfiguration configuration,
            INotificationService notificationService, IAuthenticationService authService)
        {
            var readConnectionString = configuration.GetConnectionString("readContext");
            var transactionalConnectionString = configuration.GetConnectionString("transactionalContext");

            var transactionModule = new TransactionsModule();
            var userAccountModule = new UserAccountModule();
            var savingsModule = new SavingsModule(new SavingsModuleConfiguration(transactionalConnectionString,
                readConnectionString));

            BindSavingsModuleEvents(transactionModule, savingsModule);
            BindUserAccountModulesEvents(notificationService, authService, transactionModule, userAccountModule);

            services.AddSingleton(svc => userAccountModule);
            services.AddSingleton(svc => transactionModule);
            services.AddSingleton(svc => savingsModule);
        }

        private static void BindUserAccountModulesEvents(INotificationService notificationService, IAuthenticationService authService,
            TransactionsModule transactionsModule, UserAccountModule userAccountModule)
        {
            userAccountModule.SubscribeEvent<OnLoginInput>(async loggedUser =>
            {
                await authService.SignIn(new UserAuthCredentials(loggedUser.Id, loggedUser.Login, loggedUser.Name));
            });

            userAccountModule.SubscribeEvent<OnCreatingAccountInput>(async createdAccount =>
            {
                await transactionsModule.SendMessage(new CreateUserInput(createdAccount.Id));
            });

            userAccountModule.SubscribeEvent<OnGeneratingPasswordInput>(async generatedPassword =>
            {
                await notificationService.Send(new Notification(
                    new Notification.Sender("Webillls", "webills@mail.com"),
                    new Notification.Receiver(generatedPassword.UserName, generatedPassword.UserContact),
                    @$"Hey <b>{generatedPassword.UserName}</b><br>Here is your new password: <b>{generatedPassword.Password}</b>"));
            });
        }

        private static void BindSavingsModuleEvents(TransactionsModule transactionModule, SavingsModule savingsModule)
        {
            savingsModule.MoneySaved += async (savedMoney) =>
            {
                await transactionModule.SendMessage(new AddNewExpenseInput(savedMoney.UserId, "Saving", 0,
                    DateTime.Now, savedMoney.Value));
            };
        }
    }
}