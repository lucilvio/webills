namespace Lucilvio.Solo.Webills.UserAccount.GenerateNewPassword
{
    public class GenerateNewPasswordInput
    {
        public GenerateNewPasswordInput(string email)
        {
            Email = email;
        }

        internal string Email { get; }
    }
}