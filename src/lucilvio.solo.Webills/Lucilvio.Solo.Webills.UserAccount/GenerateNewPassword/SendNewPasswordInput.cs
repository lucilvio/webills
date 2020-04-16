namespace Lucilvio.Solo.Webills.UserAccount.GenerateNewPassword
{
    public class SendNewPasswordInput
    {
        public SendNewPasswordInput(string email)
        {
            Email = email;
        }

        internal string Email { get; }
    }
}