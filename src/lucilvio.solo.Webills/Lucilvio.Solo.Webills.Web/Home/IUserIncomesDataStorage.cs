using System.Collections.Generic;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public interface IUserIncomesDataStorage
    {
        private DataStorageContext _context;

        IEnumerable<UserIncomesData> SearchForUserIncomes();

        public IUserIncomesDataStorage(DataStorageContext context)
        {
            this._context = context;
        }
    }

    public class UserIncomesDataStorageInMemory
    {

    }
}