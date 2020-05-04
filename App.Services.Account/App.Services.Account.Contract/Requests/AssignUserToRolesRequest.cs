using System;
using System.Collections.Generic;

namespace App.Services.Account.Contract.Requests
{
    public class AssignUserToRolesRequest
    {
        public Guid UserId { get; set; }

        public IList<Guid> RoleIds { get; set; }
    }
}
