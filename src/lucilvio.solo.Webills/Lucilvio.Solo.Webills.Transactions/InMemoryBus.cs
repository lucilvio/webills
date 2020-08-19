using System;
using System.Collections.Generic;
using System.Linq;

using Lucilvio.Solo.Webills.Transactions.AddNewExpense;
using Lucilvio.Solo.Webills.Transactions.AddNewIncome;
using Lucilvio.Solo.Webills.Transactions.EditExpense;
using Lucilvio.Solo.Webills.Transactions.EditIncome;
using Lucilvio.Solo.Webills.Transactions.RemoveExpense;
using Lucilvio.Solo.Webills.Transactions.RemoveIncome;

namespace Lucilvio.Solo.Webills.Transactions
{
    internal class InMemoryBus : IBusSender, IBusSubscriber
    {
        private event Action<OnAddIncomeInput> _onAddIncome;
        private event Action<OnEditIncomeInput> _onEditIncome;
        private event Action<OnRemoveIncomeInput> _onRemoveIncome;

        private event Action<OnAddExpenseInput> _onAddExpense;
        private event Action<OnEditedExpenseInput> _onEditExpense;
        private event Action<OnRemovedExpenseInput> _onRemoveExpense;

        private readonly EventsMap _eventsMap;

        public InMemoryBus()
        {
            this._eventsMap = new EventsMap(
                new EventMap(typeof(OnAddIncomeInput), arg => this._onAddIncome?.Invoke((OnAddIncomeInput)arg), act => this._onAddIncome += (Action<OnAddIncomeInput>)act),
                new EventMap(typeof(OnEditIncomeInput), arg => this._onEditIncome?.Invoke((OnEditIncomeInput)arg), act => this._onEditIncome += (Action<OnEditIncomeInput>)act),
                new EventMap(typeof(OnRemoveIncomeInput), arg => this._onRemoveIncome?.Invoke((OnRemoveIncomeInput)arg), act => this._onRemoveIncome += (Action<OnRemoveIncomeInput>)act),
                new EventMap(typeof(OnAddExpenseInput), arg => this._onAddExpense?.Invoke((OnAddExpenseInput)arg), act => this._onAddExpense += (Action<OnAddExpenseInput>)act),
                new EventMap(typeof(OnEditedExpenseInput), arg => this._onEditExpense?.Invoke((OnEditedExpenseInput)arg), act => this._onEditExpense += (Action<OnEditedExpenseInput>)act),
                new EventMap(typeof(OnRemovedExpenseInput), arg => this._onRemoveExpense?.Invoke((OnRemovedExpenseInput)arg), act => this._onRemoveExpense += (Action<OnRemovedExpenseInput>)act)
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