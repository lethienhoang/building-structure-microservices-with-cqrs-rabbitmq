using Framework.Domain;
using System;

namespace App.Services.Account.Domain
{
    public class UserRoles : EntityBase
    {
        public Guid UserId { get; private set; }

        public Guid RoleId { get; private set; }

        public Role Role { get; private set; }

        public bool IsDeleted { get; private set; }

        protected UserRoles() { }

        public UserRoles(Guid userId, Guid roleId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            RoleId = roleId;
        }

        public void RemoveRoleForUser()
        {
            IsDeleted = true;
        }
    }
}
