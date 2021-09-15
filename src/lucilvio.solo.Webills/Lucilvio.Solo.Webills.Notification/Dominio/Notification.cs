namespace Lucilvio.Solo.Webills.Website.Shared.Notification
{
    internal class Notification
    {
        public Notification(Sender from, Receiver to, string subject, string message)
        {
            this.From = from;
            this.To = to;
            this.Subject = subject;
            this.Message = message;
        }

        public Sender From { get; }
        public Receiver To { get; }
        public string Subject { get; }
        public string Message { get; }

        public class Receiver
        {
            public Receiver(string name, string mail)
            {
                this.Name = name;
                this.Mail = mail;
            }

            public string Name { get; }
            public string Mail { get; }
        }

        public class Sender
        {
            public Sender(string name, string mail)
            {
                this.Name = name;
                this.Mail = mail;
            }

            public string Name { get; }
            public string Mail { get; }
        }
    }
}