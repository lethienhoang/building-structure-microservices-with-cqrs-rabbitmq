using App.Services.Account.Contract.Responses;
using App.Services.Account.Domain;
using App.Services.Account.Handlers;
using App.Services.Account.Infrastructure.Constants;
using Framework.Domain;
using Framework.MongoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services.Account.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoRepository<User> _repository;
        public UserRepository(IMongoRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(User user)
        {
            await _repository.AddAsync(user);
        }

        public async Task ChangePasswordAsync(Guid userId, string newPassword)
        {
            var user = await _repository.GetAsync(userId);

            if (user == null)
            {
                throw new DomainException(Codes.UserNotFound, string.Format(MessagesCode.UserNotFound, userId));
            }

            user.UpdatePassword(newPassword);
        }

        public async Task<UserProfileResponse> GetProfilesAsync(Guid userId)
        {
            var user = await _repository.GetAsync(userId);

            if (user == null)
            {
                throw new DomainException(Codes.UserNotFound, string.Format(MessagesCode.UserNotFound, userId));
            }

            return new UserProfileResponse
            {
                Email = user.Email,
                Id = user.Id,
                UserRoles = user.UserRoles.Select(x => new UserRoleResponse
                {
                    RoleId = x.RoleId,
                    RoleName = x.Role.RoleName
                }).ToList()
            };
        }

        public async Task UpdateAsync(User user)
        {
            await _repository.UpdateAsync(user);
        }
    }
}
