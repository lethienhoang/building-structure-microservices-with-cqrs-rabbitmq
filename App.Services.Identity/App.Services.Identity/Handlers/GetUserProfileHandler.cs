using App.Services.Identity.Contract.Responses;
using App.Services.Identity.ExternalServicesPort;
using App.Services.Identity.Infrastructure.Constants;
using App.Services.Identity.Queries;
using Framework.CQRS.Handlers;
using Framework.Domain;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services.Identity.Handlers
{
    public class GetUserProfileHandler : IQueryHandler<UserProfileQuery, UserProfileResponse>
    {
        private readonly IAccountService _accountService;
        public GetUserProfileHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<UserProfileResponse> HandleAsync(UserProfileQuery query)
        {
            var result = await _accountService.GetUserProfileAsync(query.UserId);

            if (!result.HasResult)
            {
                new DomainException().BuildNotExistException(string.Format(MessagesCode.UserNotFound, query.UserId));
            }

            return new UserProfileResponse
            {
                Email = result.Result.Email,
                UserRoles = result.Result.UserRoles.Select(x => new UserRoleResponse
                {
                    RoleId = x.RoleId,
                    RoleName = x.RoleName
                }).ToList(),
                Id = query.UserId
            };
        }
    }
}
