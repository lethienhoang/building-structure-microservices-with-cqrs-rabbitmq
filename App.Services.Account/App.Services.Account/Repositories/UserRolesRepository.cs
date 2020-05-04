using App.Services.Account.Domain;
using Framework.MongoDb;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Services.Account.Repositories
{
    public class UserRolesRepository : IUserRolesRepository
    {
        private readonly IMongoRepository<UserRoles> _userRolerepository;
        public UserRolesRepository(IMongoRepository<UserRoles> userRolerepository)
        {
            _userRolerepository = userRolerepository;
        }

        public async Task AssignUserToRolesAsync(Guid userId, IList<Guid> RoleIds)
        {
            foreach(var roleId in RoleIds)
            {
                var userRoles = new UserRoles(userId, roleId);

                await _userRolerepository.AddAsync(userRoles);
            }
        }
    }
}
