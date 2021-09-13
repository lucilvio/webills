using Autofac;
using Lucilvio.Solo.Webills.UserAccount.GenerateNewPassword;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess;

namespace Lucilvio.Solo.Webills.UserAccount.Infraestructure.Injection
{
    internal class GenerateNewPasswordAutofacModule : Autofac.Module
    {
        private readonly Module.Configurations _configurations;

        public GenerateNewPasswordAutofacModule(Module.Configurations configurations)
        {
            this._configurations = configurations;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new UserAccountDataContext(this._configurations)).AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<GenerateNewPasswordDataAccess>().As<IGenerateNewPasswordDataAccess>().InstancePerLifetimeScope();
            builder.RegisterType<GenerateNewPassword.GenerateNewPassword>().As<IHandler<GenerateNewPasswordMessage>>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
