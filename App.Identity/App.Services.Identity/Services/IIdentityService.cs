using App.Framework.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services.Identity.Services
{
    public interface IIdentityService
    {
        Task SignUpAsync(Guid id, string email, string password, string role);

        Task<JsonWebToken> SignInAsync(string email, string password);

        Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
    }
}
