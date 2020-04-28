using System;

namespace App.Services.Identity.Domain
{
    public class UserRoles
    {
        public Guid UserId { get; private set; }

        public Guid RoleId { get; private set; }

        public Role Role { get; private set; }

        protected UserRoles() { }
    }
}
