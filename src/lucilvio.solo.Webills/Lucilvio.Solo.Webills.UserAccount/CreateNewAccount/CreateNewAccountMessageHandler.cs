﻿using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UserAccount.Domain;
using Lucilvio.Solo.Webills.UserAccount.CreateUserAccount;
using Lucilvio.Solo.Webills.EventBus;

namespace Lucilvio.Solo.Webills.UserAccount.CreateAccount
{
    internal class CreateNewAccountMessageHandler : IMessageHandler<ICreateNewAccountMessage>
    {
        private readonly IEventBus _eventBus;
        private readonly ICreateNewAccountDataAccess _dataAccess;

        public CreateNewAccountMessageHandler(ICreateNewAccountDataAccess dataAccess, IEventBus eventBus)
        {
            this._eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            this._dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }
        
        public async Task<dynamic> Execute(ICreateNewAccountMessage message)
        {
            var user = new User(new Name(message.Name), new Email(message.Email));

            var userWithTheSameLogin = await this._dataAccess.GetUserByLogin(new Domain.Login(message.Email));

            user.CreateAccount(
                new Domain.Login(message.Email),
                new Sha1EncryptedPassword(new ComplexPassword(new Password(message.Password))),
                new Sha1EncryptedPassword(new Password(message.PasswordConfirmation)),
                message.TermsAccepted,
                userWithTheSameLogin);

            await this._dataAccess.Persist(user);

            var createdAccount = new CreatedAccount(user);
            this._eventBus.Publish(Events.UserAccountCreated.ToString(), createdAccount);

            return createdAccount;
        }
    }
}