using System;
using Lucilvio.Solo.Architecture;
using Lucilvio.Solo.Architecture.Handler.Authorization.Component;
using Lucilvio.Solo.Webills.FinancialControl.Domain;
using Lucilvio.Solo.Webills.FinancialControl.GetUserDashboardInfo;

namespace Lucilvio.Solo.Webills.FinancialControl.GetUserFinancialInformation
{
    [AllowedRoles(Roles.GetFinancialInformation)]
    public record GetUserFinancialInformationMessage : AuthorizedMessage<UserFinancialInformation>
    {
        public GetUserFinancialInformationMessage(Guid userId, string[] userRoles) : base(userRoles)
        {
            this.UserId = userId;
        }

        public Guid UserId { get; }
    }
}