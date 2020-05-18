namespace Lucilvio.Solo.Webills.UserAccount.GenerateNewPassword
{
    public class OnGeneratingPasswordInput
    {
        internal OnGeneratingPasswordInput(string userName, string userContact, string password)
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
