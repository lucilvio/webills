namespace Lucilvio.Solo.Webills.UserAccount.GenerateNewPassword
{
    public class GeneratedPassword
    {
        internal GeneratedPassword(string userName, string userContact, string password)
        {
            UserName = userName;
            UserContact = userContact;
            Password = password;
        }

        public string UserName { get; }
        public string UserContact { get; }
        public string Password { get; }
    }
}
