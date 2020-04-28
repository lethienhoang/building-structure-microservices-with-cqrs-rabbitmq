using System;
using System.Collections.Generic;

namespace App.Services.Identity.Contract.Responses.ExternalResponses
{
    public class UserProfileExternalResponse
    {
        public string Email { get; set; }


        public IList<UserRoleExternalResponse> UserRoles { get; set; }
    }

    public class UserRoleExternalResponse
    {
        public Guid RoleId { get; set; }

        public string RoleName { get; set; }
    }
}

