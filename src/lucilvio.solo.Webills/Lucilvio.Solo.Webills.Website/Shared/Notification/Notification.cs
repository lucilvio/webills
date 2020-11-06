namespace Lucilvio.Solo.Webills.Website.Shared.Notification
{
    public class Notification
    {
        public Notification(Sender from, Receiver to, string subject, string message)
        {
            From = from;
            To = to;
            Subject = subject;
            Message = message;
        }

        public Sender From { get; }
        public Receiver To { get; }
        public string Subject { get; }
        public string Message { get; }

        public class Receiver
        {
            public Receiver(string name, string mail)
            {
                Name = name;
                Mail = mail;
            }

            public string Name { get; }
            public string Mail { get; }
        }

        public class Sender
        {
            public Sender(string name, string mail)
            {
                Name = name;
                Mail = mail;
            }

            public string Name { get; }
            public string Mail { get; }
        }
    }
}