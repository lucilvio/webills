namespace Lucilvio.Solo.Webills.UserAccount
{
    internal interface IBusSender
    {
        void SendEvent(object addedExpense);
    }
}