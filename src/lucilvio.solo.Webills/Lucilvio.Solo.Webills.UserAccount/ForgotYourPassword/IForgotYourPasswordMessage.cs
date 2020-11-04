namespace Lucilvio.Solo.Webills.UserAccount.ForgotYourPassword
{
    public interface IForgotYourPasswordMessage : IMessage
    {
        public string Email { get; }
    }
}