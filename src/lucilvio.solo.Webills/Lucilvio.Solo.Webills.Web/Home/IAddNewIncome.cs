namespace Lucilvio.Solo.Webills.Web.Home
{
    public interface IAddNewIncome
    {
        void Execute(NewIncomeCommand command);
    }
}