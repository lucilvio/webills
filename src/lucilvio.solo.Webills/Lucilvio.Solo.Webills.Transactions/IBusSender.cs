namespace Lucilvio.Solo.Webills.Transactions
{
    internal interface IBusSender
    {
        void SendEvent(object addedExpense);
    }
}