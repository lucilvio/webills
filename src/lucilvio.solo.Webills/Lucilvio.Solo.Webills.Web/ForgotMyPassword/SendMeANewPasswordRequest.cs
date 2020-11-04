using Lucilvio.Solo.Webills.UserAccount.ForgotYourPassword;

namespace Lucilvio.Solo.Webills.Clients.Web.ForgotMyPassword
{
    public class SendMeANewPasswordRequest : IForgotYourPasswordMessage
    {
        public string Email { get; set; }
    }
}