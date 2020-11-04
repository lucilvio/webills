﻿using Lucilvio.Solo.Webills.UserAccount.CreateUserAccount;

namespace Lucilvio.Solo.Webills.Web.SignUp
{
    public class SignUpRequest : ICreateUserAccountMessage
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public string Name { get; set; }
        public bool TermsAccepted { get; set; }
    }
}