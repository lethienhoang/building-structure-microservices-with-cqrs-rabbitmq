using App.Services.Identity.Domain;
using App.Services.Identity.Infrastructure.Constants;
using App.Services.Identity.Queries;
using App.Services.Identity.Repositories;
using Framework;
using Framework.Auth;
using Framework.CQRS.Handlers;
using Framework.Domain;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services.Identity.Handlers
{
    public class SignInHandler : IQueryHandler<SignInQuery, JsonWebToken>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtHandler _jwtHandler;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public SignInHandler(IUserRepository userRepository,
                             IPasswordHasher<User> passwordHasher,
                             IJwtHandler jwtHandler,
                             IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtHandler = jwtHandler;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<JsonWebToken> HandleAsync(SignInQuery query)
        {
            var user = await _userRepository.GetAsync(query.Email);
            if (user == null || !user.ValidatePassword(query.Password, _passwordHasher))
            {
                throw new DomainException(Codes.InvalidCredentials, MessagesCode.InvalidCredentials);
            }

            var rolesName = user.UserRoles != null ? user.UserRoles.Select(x => x.Role.RoleName).ToList() : new List<string>();
            var refreshToken = new RefreshToken(user, _passwordHasher);

            var claim = new Dictionary<string, string>()
            {
                { CoreConstants.OnUserIdClaimType, user.Id.ToString() },
                { CoreConstants.RoleIdClaimType, string.Join(",", rolesName) }
            };

            var jwt = _jwtHandler.CreateToken(user.Id.ToString("N"), rolesName, claim);
            jwt.RefreshToken = refreshToken.Token;

            await _refreshTokenRepository.AddAsync(refreshToken);

            return jwt;
        }
    }
}
