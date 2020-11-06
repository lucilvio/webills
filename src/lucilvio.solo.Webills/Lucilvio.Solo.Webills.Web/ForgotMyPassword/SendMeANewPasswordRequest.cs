using Lucilvio.Solo.Webills.UserAccount.GenerateNewPassword;

namespace Lucilvio.Solo.Webills.Clients.Web.ForgotMyPassword
{
    public class SendMeANewPasswordRequest : IGenerateNewPasswordMessage
    {
        public string Email { get; set; }
    }
}