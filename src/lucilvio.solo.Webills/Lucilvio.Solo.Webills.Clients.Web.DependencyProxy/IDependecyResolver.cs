namespace Lucilvio.Solo.Webills.Clients.Web.DependencyProxy
{
    internal interface IDependecyResolver
    {
        void Resolve(ServiceCollection services, IConfiguration configuration);
    }
}