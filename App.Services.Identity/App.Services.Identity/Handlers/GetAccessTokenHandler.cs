using App.Services.Identity.Domain;
using App.Services.Identity.Infrastructure.Constants;
using App.Services.Identity.Queries;
using App.Services.Identity.Repositories;
using Framework.Auth;
using Framework.CQRS.Handlers;
using Framework.Domain;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services.Identity.Handlers
{
    public class GetAccessTokenHandler : IQueryHandler<AccessTokenQuery, JsonWebToken>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtHandler _jwtHandler;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public GetAccessTokenHandler(IUserRepository userRepository,
                             IJwtHandler jwtHandler,
                             IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _jwtHandler = jwtHandler;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<JsonWebToken> HandleAsync(AccessTokenQuery query)
        {
            var refreshToken = await _refreshTokenRepository.GetAsync(query.Token);
            if (refreshToken == null)
            {
                throw new DomainException(Codes.RefreshTokenNotFound, MessagesCode.RefreshTokenNotFound);
            }

            if (refreshToken.Revoked)
            {
                throw new DomainException(Codes.RefreshTokenAlreadyRevoked, string.Format(MessagesCode.RefreshTokenAlreadyRevoked, refreshToken.Id));
            }

            var user = await _userRepository.GetAsync(refreshToken.UserId);
            if (user == null)
            {
                throw new DomainException(Codes.UserNotFound, string.Format(MessagesCode.UserNotFound, refreshToken.UserId));
            }

            var rolesName = user.UserRoles.Select(x => x.Role.RoleName).ToList();
            var jwt = _jwtHandler.CreateToken(user.Id.ToString("N"), rolesName);
            jwt.RefreshToken = refreshToken.Token;

            return jwt;
        }
    }
}
