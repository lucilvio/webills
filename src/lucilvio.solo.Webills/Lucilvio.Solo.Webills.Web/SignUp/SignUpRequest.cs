using Lucilvio.Solo.Webills.UserAccount.CreateAccount;

namespace Lucilvio.Solo.Webills.Web.SignUp
{
    public class SignUpRequest : ICreateNewAccountMessage
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public string Name { get; set; }
        public bool TermsAccepted { get; set; }
    }
}