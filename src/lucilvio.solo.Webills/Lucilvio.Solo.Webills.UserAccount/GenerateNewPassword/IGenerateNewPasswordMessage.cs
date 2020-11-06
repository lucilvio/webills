namespace Lucilvio.Solo.Webills.UserAccount.GenerateNewPassword
{
    public interface IGenerateNewPasswordMessage : IMessage
    {
        public string Email { get; }
    }
}