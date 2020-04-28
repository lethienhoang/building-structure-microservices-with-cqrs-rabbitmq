using App.Services.Identity.Domain;
using System;
using System.Threading.Tasks;

namespace App.Services.Identity.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(string email);

        Task<User> GetAsync(Guid userId);
    }
}
