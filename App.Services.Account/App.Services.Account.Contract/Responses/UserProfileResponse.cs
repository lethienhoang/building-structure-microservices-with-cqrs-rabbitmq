using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services.Account.Contract.Responses
{
    public class UserProfileResponse : ResponseBase
    {
        public string Email { get; set; }


        public IList<UserRoleResponse> UserRoles { get; set; }
    }

    public class UserRoleResponse
    {
        public Guid RoleId { get; set; }

        public string RoleName { get; set; }
    }
}
