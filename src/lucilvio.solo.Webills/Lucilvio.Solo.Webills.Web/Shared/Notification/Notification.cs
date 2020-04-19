namespace Lucilvio.Solo.Webills.Clients.Web.Shared.Notification
{
    public class Notification
    {
        public Notification(Sender from, Receiver to, string message)
        {
            this.From = from;
            this.To = to;
            this.Message = message;
        }

        public Sender From { get; }
        public Receiver To { get; }
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