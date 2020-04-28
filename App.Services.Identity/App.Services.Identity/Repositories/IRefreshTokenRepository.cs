using App.Services.Identity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services.Identity.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetAsync(string token);

        Task AddAsync(RefreshToken token);

        Task UpdateAsync(RefreshToken token);
    }
}
