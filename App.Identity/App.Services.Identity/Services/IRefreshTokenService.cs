using App.Framework.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services.Identity.Services
{
    public interface IRefreshTokenService
    {
        Task AddAsync(Guid userId);

        Task<JsonWebToken> CreateAccessTokenAsync(string refreshToken);

        Task RevokeAsync(string refreshToken, Guid userId);
    }
}
