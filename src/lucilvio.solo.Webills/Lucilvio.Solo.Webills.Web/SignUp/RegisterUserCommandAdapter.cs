using Lucilvio.Solo.Webills.Domain.Profile.User;
using Lucilvio.Solo.Webills.UseCases.Contracts.RegisterUser;

namespace Lucilvio.Solo.Webills.Web.SignUp
{
    public class RegisterUserCommandAdapter : RegisterUserCommand
    {
        public RegisterUserCommandAdapter(RegisterUserRequest request)
            : base(new Login(request.Login), new Password(request.Password), new Password(request.PasswordConfirmation), request.Name, request.TermsAccepted)
        {
        }
    }
}