namespace Lucilvio.Solo.Webills.Notifications
{
    public record Configurations
    {
        public string DataConnectionString { get; init; }
        public NotificationServiceConfiguration NotificationService { get; init; }

        public record NotificationServiceConfiguration
        {
            public string Source { get; init; }
            public string User { get; init; }
            public string Key { get; init; }
        }
    }
}