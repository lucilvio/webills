namespace Lucilvio.Solo.Architecture.Handler.Authorization.Component
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class AllowedRolesAttribute : Attribute
    {
        private readonly string[] _roles;

        public AllowedRolesAttribute(params string[] roles)
        {
            this._roles = roles;
        }

        public IReadOnlyCollection<string> Roles => this._roles;
    }
}
