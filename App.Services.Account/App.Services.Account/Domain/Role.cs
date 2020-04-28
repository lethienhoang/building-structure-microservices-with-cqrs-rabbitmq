using Framework.Domain;
using System;

namespace App.Services.Account.Domain
{
    public class Role : EntityBase
    {
        public string RoleName { get; private set; }

        protected Role() { }

        public Role(Guid id, string roleName)
        {
            Id = id;
            RoleName = roleName;
        }

        public void UpdateRole(string roleName)
        {
            RoleName = roleName;
        }
    }
}
