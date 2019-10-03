namespace Lucilvio.Solo.Webills.Web.Home
{
    internal interface ISearchForUserIncomes
    {
        public UserIncomesResponse Execute();
    }

    public class SearchForUserIncomesInMemory : ISearchForUserIncomes
    {
        private readonly IUserIncomesDataStorage _dataStorage;

        public SearchForUserIncomesInMemory(IUserIncomesDataStorage dataStorage)
        {
            this._dataStorage = dataStorage;
        }

        public UserIncomesResponse Execute()
        {
            return this._dataStorage.Users.
        }
    }
}