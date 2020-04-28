using App.Services.Account.Domain;
using System;
using System.Threading.Tasks;

namespace App.Services.Account.Repositories
{
    public interface IUserRepository
    {
        Task UpdateAsync(User user);

        Task AddAsync(User user);

        Task ChangePasswordAsync(Guid userId, string newPassword);
    }
}
