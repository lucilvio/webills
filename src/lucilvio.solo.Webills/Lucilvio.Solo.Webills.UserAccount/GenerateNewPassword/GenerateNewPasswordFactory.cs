using Autofac;
using Lucilvio.Solo.Architecture;
using Lucilvio.Solo.Webills.UserAccount.GenerateNewPassword;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess;
using Lucilvio.Solo.Webills.UserAccount.Infrastructure.Injection;

namespace Lucilvio.Solo.Webills.UserAccount.Infraestructure.Injection
{
    internal class GenerateNewPasswordFactory : AutofacFactory
    {
        public GenerateNewPasswordFactory(Module.Configurations configurations) : base(configurations) { }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new UserAccountDataContext(this._configurations)).AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterDecorator<TransactionScopedHandler<GenerateNewPasswordMessage>, IHandler<GenerateNewPasswordMessage>>();
            builder.RegisterType<GenerateNewPasswordDataAccess>().As<IGenerateNewPasswordDataAccess>().InstancePerLifetimeScope();
            builder.RegisterType<GenerateNewPassword.GenerateNewPassword>().As<IHandler<GenerateNewPasswordMessage>>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
