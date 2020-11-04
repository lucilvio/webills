﻿namespace Lucilvio.Solo.Webills.UserAccount.CreateAccount
{
    public interface ICreateAccountMessage : IMessage
    {
        public string Name { get; }
        public string Email { get; }
        public string Password { get; }
        public string PasswordConfirmation { get; }
        public bool TermsAccepted { get; }
    }
}
