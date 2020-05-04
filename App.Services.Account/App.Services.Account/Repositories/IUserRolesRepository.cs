using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services.Account.Repositories
{
    public interface IUserRolesRepository
    {
        Task AssignUserToRolesAsync(Guid userId, IList<Guid> RoleIds);
    }
}
