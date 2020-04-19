using System;

namespace Lucilvio.Solo.Webills.Savings.GetSavingsByFilter
{
    public class GetSavingsByFilterInput
    {
        public GetSavingsByFilterInput(Guid userId)
        {
            this.UserId = userId;
        }

        internal Guid UserId { get; }
    }
}
