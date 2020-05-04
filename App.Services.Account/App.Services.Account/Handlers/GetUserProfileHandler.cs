using App.Services.Account.Contract.Responses;
using App.Services.Account.Queries;
using App.Services.Account.Repositories;
using Framework.CQRS.Handlers;
using System.Threading.Tasks;

namespace App.Services.Account.Handlers
{
    public class GetUserProfileHandler : IQueryHandler<UserProfileQuery, UserProfileResponse>
    {
        private readonly IUserRepository _userRepository;
        public GetUserProfileHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserProfileResponse> HandleAsync(UserProfileQuery query)
        {
            return await _userRepository.GetProfilesAsync(query.UserId);           
        }
    }
}
