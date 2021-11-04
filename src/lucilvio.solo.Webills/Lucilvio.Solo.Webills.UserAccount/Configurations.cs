namespace Lucilvio.Solo.Webills.UserAccount
{
    public record Configurations
    {
        public string DataConnectionString { get; init; }
        public DefaultUserAccount DefaultAccount { get; init; }

        internal string ModuleName { get; } = "UserAccount";
        internal bool IsDefaultUserAccountConfigured => this.DefaultAccount != null;

        public record DefaultUserAccount
        {
            public string Name { get; init; }
            public string Email { get; init; }
            public string Password { get; init; }
        }
    }
}