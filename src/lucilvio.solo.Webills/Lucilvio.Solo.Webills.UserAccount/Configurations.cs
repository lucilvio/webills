namespace Lucilvio.Solo.Webills.UserAccount
{
    public class Configurations
    {
        public Configurations()
        {
            this.DefaultAccount = new DefaultUserAccount();
        }

        public string DataConnection { get; set; }
        public bool CreateDefaultUserAccount { get; set; }
        public DefaultUserAccount DefaultAccount { get; set; }


        public class DefaultUserAccount
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}