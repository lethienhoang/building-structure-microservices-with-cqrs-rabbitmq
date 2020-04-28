using System;
using System.Collections.Generic;

namespace App.Services.Identity.Contract.Responses
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
