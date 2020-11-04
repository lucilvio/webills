namespace Lucilvio.Solo.Webills.UserAccount.ForgotYourPassword
{
    public class GeneratedPassword
    {
        internal GeneratedPassword(string userName, string userContact, string password)
        {
            this.UserName = userName;
            this.UserContact = userContact;
            this.Password = password;
        }

        public string UserName { get; }
        public string UserContact { get; }
        public string Password { get; }
    }
}
