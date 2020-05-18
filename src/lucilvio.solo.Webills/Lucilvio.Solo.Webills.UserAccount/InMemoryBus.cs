using System;
using System.Collections.Generic;
using System.Linq;
using Lucilvio.Solo.Webills.UserAccount.CreateUserAccount;
using Lucilvio.Solo.Webills.UserAccount.GenerateNewPassword;
using Lucilvio.Solo.Webills.UserAccount.Login;

namespace Lucilvio.Solo.Webills.UserAccount
{
    internal class InMemoryBus : IBusSender, IBusSubscriber
    {
        public event Action<OnLoginInput> _onLogin;
        public event Action<OnCreatingAccountInput> _onCreatingAccount;
        public event Action<OnGeneratingPasswordInput> _onGeneratingPassword;

        private readonly EventsMap _eventsMap;

        public InMemoryBus()
        {
            this._eventsMap = new EventsMap(
                new EventMap(typeof(OnLoginInput), arg => this._onLogin.Invoke((OnLoginInput)arg), act => this._onLogin += (Action<OnLoginInput>)act),
                new EventMap(typeof(OnCreatingAccountInput), arg => this._onCreatingAccount.Invoke((OnCreatingAccountInput)arg), act => this._onCreatingAccount += (Action<OnCreatingAccountInput>)act),
                new EventMap(typeof(OnGeneratingPasswordInput), arg => this._onGeneratingPassword.Invoke((OnGeneratingPasswordInput)arg), act => this._onGeneratingPassword += (Action<OnGeneratingPasswordInput>)act)
            );
        }

        public void SendEvent(object eventArgs) => this._eventsMap.Send(eventArgs);
        public void Subscribe<TEvent>(Action<TEvent> action) => this._eventsMap.Subscribe(action);
    }

    internal class EventsMap
    {
        private readonly IList<EventMap> _map;

        private EventsMap()
        {
            this._map = new List<EventMap>();
        }

        public EventsMap(params EventMap[] eventMap) : this()
        {
            if (eventMap != null)
                this._map = eventMap;
        }

        internal void Send(object eventArgs)
        {
            if (eventArgs == null)
                return;

            var @event = this._map.FirstOrDefault(e => e.Type == eventArgs.GetType());
            @event?.Sender?.Invoke(eventArgs);
        }

        internal void Subscribe<TEvent>(Action<TEvent> action)
        {
            if (action == null)
                return;

            var @event = this._map.FirstOrDefault(e => e.Type == typeof(TEvent));
            @event?.Subscriber?.Invoke(action);
        }
    }

    internal class EventMap
    {
        public EventMap(Type type, Action<object> sender, Action<object> subscriber)
        {
            this.Type = type;
            this.Sender = sender;
            this.Subscriber = subscriber;
        }

        public Type Type { get; }
        public Action<object> Sender { get; }
        public Action<object> Subscriber { get; }
    }
}